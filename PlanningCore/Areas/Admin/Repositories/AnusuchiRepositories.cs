using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Planning.Models.PlanningAnusuchiViewModel;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Repositories
{
    public class AnusuchiRepositories : IAnusuchi
    {
        private readonly PlanningContext _context;
        private readonly string _userId = null;
        private readonly IUtility _utility;
        public AnusuchiRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<AnusuchiRepositories> logger, IUtility utility)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            utility = _utility;
        }
		#region Anusuchi3
		public async Task<PlanningAnusuchi3ViewModel> GetAnushuchi3List(int? id)
		{
			var data = await _context.Anusuchi3.Where(x => x.PlanningSamjhautaId == id).Select(x => new PlanningAnusuchi3ViewModel()
			{
				Anusuchi3Id = x.Anusuchi3Id,
				PlanningSamjhautaId = x.PlanningSamjhautaId,
				ProjectName = x.ProjectName,
				ProjectPlace = x.ProjectPlace,
				EstimatedAmount = x.EstimatedAmount,
				ProjectStartDate_Nep = x.ProjectStartDate_Nep,
				ProjectStartDate_Eng = x.ProjectStartDate_Eng,
				ProjectEndDate_Nep = x.ProjectEndDate_Nep,
				ProjectEndDate_Eng = x.ProjectEndDate_Eng,
				UpabhoktaSamitiName = x.UpabhoktaSamitiName,
				AdakshyaName = x.AdakshyaName,
				TotalMember = x.TotalMember,
				TotalMaleNo = x.TotalMaleNo,
				TotalFemaleNo = x.TotalFemaleNo,
				TotalBenificialNo = x.TotalBenificialNo,
				RohabarName = x.RohabarName,
				RohabarPostion = x.RohabarPostion,
				RohabarDate = x.RohabarDate,
				Status = x.Status,
				//PlanningAnusuchi3ExpensesList = _context.Anusuchi3Expenses.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3ExpensesViewModel()
				//{
				//	AnuSuchi3ExpensesId = y.AnuSuchi3ExpensesId,
				//	Anusuchi3ExpenseTypeId = y.Anusuchi3ExpenseTypeId,
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	ExpensesDetails = y.ExpensesDetails,
				//	ExpensesQuantity = y.ExpensesQuantity,
				//	ExpensesRate = y.ExpensesRate,
				//	ExpensesTotal = y.ExpensesTotal,
				//	Anusuchi3ExpenseTypeName = _context.Anusuchi3ExpensesType.Where(z => z.Id == y.Anusuchi3ExpenseTypeId).Select(z => z.Name).FirstOrDefault(),
				//}).ToList(),
				//PlanningAnusuchi3IncomeList = _context.Anusuchi3Income.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3IncomeViewModel()
				//{
				//	AmountQuantity = y.AmountQuantity,
				//	Anusuchi3IncomeId = y.Anusuchi3IncomeId,
				//	IncomeRemarks = y.IncomeRemarks,
				//	IncomeSource = y.IncomeSource,
				//}).ToList(),
				//PlanningAnusuchi3BhuktaniList = _context.Anusuchi3Bhuktani.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3BhuktaniViewModel()
				//{
				//	Anusuchi3BhuktaniId = y.Anusuchi3BhuktaniId,
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	BhuktaniAmount = y.BhuktaniAmount,
				//	BhuktaniDetail = y.BhuktaniDetail,
				//}).ToList(),
				//PlanningAnusuchi3MaujatList = _context.Anusuchi3Maujat.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3MaujatViewModel()
				//{
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	Anusuchi3MaujatId = y.Anusuchi3MaujatId,
				//	MaujatAmount = y.MaujatAmount,
				//	MaujatDetail = y.MaujatDetail,
				//	MaujatRemarks = y.MaujatRemarks,
				//}).ToList(),
				//PlanningAnusuchi3WorkDivisionList = _context.Anusuchi3WorkDivision.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3WorkDivisionViewModel()
				//{
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	Anusuchi3WorkDivisionId = y.Anusuchi3WorkDivisionId,
				//	MemberName = y.MemberName,
				//	PadaId = y.PadaId,
				//	PadaName = _context.Post.Where(q => q.Id == y.PadaId).Select(q => q.Name).FirstOrDefault(),
				//	WorkDescription = y.WorkDescription,
				//	Kaifiyat = y.Kaifiyat
				//}).ToList(),
				//PlanningAnusuchi3ProjectWorkDetailList = _context.Anusuchi3ProjectWorkDetail.Where(y => y.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3ProjectWorkDetail()
				//{
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	Anusuchi3ProjectWorkDetailId = y.Anusuchi3ProjectWorkDetailId,
				//	WorkDetail = y.WorkDetail,
				//	WorkPlan = y.WorkPlan,
				//	WorkProgress = y.WorkProgress,
				//}).ToList(),
				//PlanningAnusuchi3MemberList = _context.Anusuchi3Member.Where(w => w.Anusuchi3Id == x.Anusuchi3Id).Select(y => new PlanningAnusuchi3MemberViewModel()
				//{
				//	Anusuchi3Id = y.Anusuchi3Id,
				//	Anusuchi3MemberId = y.Anusuchi3MemberId,
				//	MemberName = y.MemberName
				//}).ToList(),
			}).FirstOrDefaultAsync();
			return data ?? new PlanningAnusuchi3ViewModel();
		}
		#endregion
		public async Task<bool> InsertUpdateAnusuchi3(PlanningAnusuchi3ViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.Anusuchi3Id > 0)
					{
						var data = await _context.Anusuchi3.Where(x => x.Anusuchi3Id == model.Anusuchi3Id).FirstOrDefaultAsync();
						if (data != null)
						{
							data.Anusuchi3Id = model.Anusuchi3Id;
							data.PlanningSamjhautaId = model.PlanningSamjhautaId;
							data.ProjectName = model.ProjectName;
							data.ProjectPlace = model.ProjectPlace;
							data.EstimatedAmount = model.EstimatedAmount;
							data.ProjectStartDate_Nep = model.ProjectStartDate_Nep;
							data.ProjectStartDate_Eng = model.ProjectStartDate_Eng;
							data.ProjectEndDate_Nep = model.ProjectEndDate_Nep;
							data.ProjectEndDate_Eng = model.ProjectEndDate_Eng;
							data.UpabhoktaSamitiName = model.UpabhoktaSamitiName;
							data.AdakshyaName = model.AdakshyaName;
							data.TotalBenificialNo = model.TotalBenificialNo;
							data.TotalMember = model.TotalMember;
							data.TotalMaleNo = model.TotalMaleNo;
							data.TotalFemaleNo = model.TotalFemaleNo;
							data.RohabarDate = model.RohabarDate;
							data.RohabarPostion = model.RohabarPostion;
							data.RohabarName = model.RohabarName;
							data.Status = true;
							_context.Entry(data).State = EntityState.Modified;
							await _context.SaveChangesAsync();

						}
						else
						{
							var anusuchidata = new Anusuchi3()
							{
								Anusuchi3Id = model.Anusuchi3Id,
								AdakshyaName = model.AdakshyaName,
								EstimatedAmount = model.EstimatedAmount,
								PlanningSamjhautaId = model.PlanningSamjhautaId,
								ProjectEndDate_Eng = model.ProjectEndDate_Eng,
								ProjectEndDate_Nep = model.ProjectEndDate_Nep,
								ProjectName = model.ProjectName,
								ProjectStartDate_Eng = model.ProjectStartDate_Eng,
								ProjectStartDate_Nep = model.ProjectStartDate_Nep,
								ProjectPlace = model.ProjectPlace,
								RohabarDate = model.RohabarDate,
								RohabarName = model.RohabarName,
								RohabarPostion = model.RohabarPostion,
								Status = true,
								TotalBenificialNo = model.TotalBenificialNo,
								TotalFemaleNo = model.TotalFemaleNo,
								TotalMaleNo = model.TotalMaleNo,
								TotalMember = model.TotalMember,
								UpabhoktaSamitiName = model.UpabhoktaSamitiName,
							};
							await _context.Anusuchi3.AddAsync(anusuchidata);
							await _context.SaveChangesAsync();
						}
					}

					return true;


				}

				catch (Exception ex)
				{
					transaction.Rollback();
					return false;
				}
			}

		}
	
	}
}

