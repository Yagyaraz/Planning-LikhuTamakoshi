using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using PlanningCore.Data;
using PlanningCore.Utilities;
using static PlanningCore.Models.DashboardViewModel;
using PlanningCore.Models;
using System.Threading.Tasks.Dataflow;

namespace PlanningCore.Dashboard
{
    public class DashboardRepository : IDashboard
    {
        private readonly PlanningContext context;
        private readonly IUtility utility;
        public DashboardRepository(PlanningContext _context, IUtility _utility)
        {
            context = _context;
            utility = _utility;
        }
        public async Task<DashboardViewModel> GetDashAllDataForDashboard()
        {

            var model = new DashboardViewModel();

           
            var fiscalyearid = await utility.GetCurrentFiscalYear();
            var getWardId = await utility.GetWardNoForLogin_Role_User();
            var samjhauta = 0;
            var totalsamiti = 0;
            var total = 0;
            decimal totalAmount = 0;
            //var WardId = await (from upabhoktaSamiti in context.UpabhoktaSamitiDetail
            //                       join upabhoktaMitiYojanas in context.UpabhoktaSamitiDetailYojanas
            //                       on upabhoktaSamiti.UpabhoktaSamitiDetailId equals upabhoktaMitiYojanas.UpabhoktaSamitiDetailId
            //                       join yojanaSetup in context.YojanaSetup
            //                       on upabhoktaMitiYojanas.YojanaId equals yojanaSetup.YojanaSetupId
            //                       select yojanaSetup.WardId).FirstOrDefaultAsync();

            if (getWardId != null && getWardId > 0)
            {

                samjhauta = await (
       from p in context.PlanningSamjhauta
       join ups in context.UpabhoktaSamitiDetail on p.SamitiDetailId equals ups.UpabhoktaSamitiDetailId into upabhokta
       from ups in upabhokta.DefaultIfEmpty()
       join upsy in context.UpabhoktaSamitiDetailYojanas on ups.UpabhoktaSamitiDetailId equals upsy.UpabhoktaSamitiDetailId into upabhokyaYojana
       from upsy in upabhokyaYojana.DefaultIfEmpty()
       join yojana in context.YojanaSetup on upsy.YojanaId equals yojana.YojanaSetupId into yoj
       from yojana in yoj.DefaultIfEmpty()
       where yojana != null && (yojana.WardId == null || yojana.WardId == getWardId)
             && p.Status == true
       select p
   ).CountAsync();

                totalAmount = await (from p in context.PlanningSamjhauta
                                   join ups in context.UpabhoktaSamitiDetail on p.SamitiDetailId equals ups.UpabhoktaSamitiDetailId into upabhokta
                                   from ups in upabhokta.DefaultIfEmpty()
                                   join upsy in context.UpabhoktaSamitiDetailYojanas on ups.UpabhoktaSamitiDetailId equals upsy.UpabhoktaSamitiDetailId into upabhokyaYojana
                                   from upsy in upabhokyaYojana.DefaultIfEmpty()
                                   join yojana in context.YojanaSetup on upsy.YojanaId equals yojana.YojanaSetupId into yoj
                                   from yojana in yoj.DefaultIfEmpty()
                                   where yojana.WardId == null || yojana.WardId == getWardId && p.Status == true
                                   select p.Total_Amount
                                         ).SumAsync();
                totalsamiti = await (from ups in context.UpabhoktaSamitiDetail                                
                                     join upsy in context.UpabhoktaSamitiDetailYojanas on ups.UpabhoktaSamitiDetailId equals upsy.UpabhoktaSamitiDetailId into upabhokyaYojana
                                     from upsy in upabhokyaYojana.DefaultIfEmpty()
                                     join yojana in context.YojanaSetup on upsy.YojanaId equals yojana.YojanaSetupId into yoj
                                     from yojana in yoj.DefaultIfEmpty()
                                     where yojana.WardId == null || yojana.WardId == getWardId && ups.Status == true
                                     select ups
                                           ).CountAsync();
               total = await context.YojanaSetup.Where(x => x.IsDeleted == false &&  x.WardId == getWardId).CountAsync();

            }
            else
            {
                samjhauta = await context.PlanningSamjhauta.Where(x => x.Status == true).CountAsync();
                totalAmount = await context.PlanningSamjhauta.Where(x => x.Status == true ).SumAsync(x => x.Total_Amount);
                totalsamiti = await context.UpabhoktaSamitiDetail.Where(x => x.Status == true).CountAsync();
                total = await context.YojanaSetup.Where(x => x.IsDeleted == false).CountAsync();
            }

                   
            var nonsamjhauta = (total-samjhauta);
          
           
            var totaltolbikashsamiti = await context.TolBikashSanstha.Where(x => x.Status == true && (getWardId == null || x.WardNo == getWardId)).CountAsync();
            var thekka = await context.Con_Samjhauta.Where(x => x.IsDeleted == false && (getWardId == null || x.YojanaSetup.WardId == getWardId)).CountAsync();

            var thekkayojana = await context.Con_Yojana.Where(x => x.IsDeleted == false && (getWardId == null || x.WardId == getWardId)).CountAsync();
            var thekkasamjhauta = await context.Con_Samjhauta.Where(x => x.IsDeleted == false && (getWardId == null || x.YojanaSetup.WardId == getWardId)).CountAsync();
            var thekkanonsamjhauta = (thekkayojana - thekkasamjhauta);
            var totalThekkaAmount = await context.Con_Samjhauta.Where(x => x.IsDeleted == false && (getWardId == null || x.YojanaSetup.WardId == getWardId)).SumAsync(x => x.ContractAmtEclVat);
            var totalConsultant = await context.Con_Consultant.Where(x => x.Status == true).CountAsync();

            model.list.Add(new DashboardDataViewModel()
            {
                Amount = totalAmount.ToString(),
                TotalYojana = total.ToString(),
                TotalSamjhauta = samjhauta.ToString(),
                NonSamjhauta = nonsamjhauta.ToString(),
                TotalTolBikashSamiti = totaltolbikashsamiti.ToString(),
                TotalSamiti = totalsamiti.ToString(),
                TotalThekka = thekka.ToString(),
                TotalThekkaAmount = totalThekkaAmount.ToString(),
                //TotalThekka = thekkayojana.ToString(),
                TotalThekkaSamjhauta = thekkasamjhauta.ToString(),
                TotalConsultant = totalConsultant.ToString(),
                ThekkaNonSamjhauta = thekkanonsamjhauta.ToString(),
            });
            return model;
        }


    }
}
