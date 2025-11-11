using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Security.Claims;

namespace PlanningCore.Areas.Admin.Repositories
{
    public class FormatRepository : IFormat
    {
        private readonly PlanningContext _context;
        private readonly string _userId = null;
        private readonly IUtility _utility;
        public FormatRepository(PlanningContext context, IHttpContextAccessor httpContextAccessor, IUtility utility)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _utility = utility;
        }
        public async Task<FormatViewModel> GetAllDataForFormat(int? id)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join o in _context.OrganizationRepresentative on p.PlanningSamjhautaId equals o.PlanningSamjhautaId into org
                              from o in org.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join bg in _context.BeneficiariesGroup on p.PlanningSamjhautaId equals bg.PlanningSamjhautaId into beneGroup
                              from bg in beneGroup.DefaultIfEmpty()
                              join pd in _context.PlanningPravidikDetails on p.PlanningSamjhautaId equals pd.PlanningSamjhautaId into pravidikDetail
                              from pd in pravidikDetail.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ms in _context.MunicipalitySamitiManjuriPatra on p.PlanningSamjhautaId equals ms.PlanningSamjhautaId into municipality
                              from ms in municipality.DefaultIfEmpty()
                              join am in _context.AayojanaMaintainance on p.PlanningSamjhautaId equals am.PlanningSamjhautaId into aayojana
                              from am in aayojana.DefaultIfEmpty()
                              join ad in _context.AmanatDetail on p.PlanningSamjhautaId equals ad.PlanningSamjhautaId into amanat
                              from ad in amanat.DefaultIfEmpty()
                              join doc in _context.DocumentUploaded on p.PlanningSamjhautaId equals doc.PlanningSamjhuataId into docs
                              from doc in docs.DefaultIfEmpty()
                              where p.PlanningSamjhautaId == id
                              select new FormatViewModel()
                              {
                                  PlanningSamjhauta = new PlanningSamjhautaViewModel
                                  {
                                      FiscalYearId = p.FiscalYearId,
                                      Samjhauta_Org_Name = _context.UpabhoktaSamitiDetail.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => u.Name).FirstOrDefault(),
                                      PlanningSamjhautaId = p.PlanningSamjhautaId,
                                      Contegency_Amount = p.Contegency_Amount,
                                      MarmatSambhar_Amount = p.MarmatSambhar_Amount,
                                      Total_Amount = p.Total_Amount,
                                      Contegency_Percentage = p.Contegency_Percentage,
                                      Status = true,
                                      Samjhauta_Acceptance = p.Samjhauta_Acceptance,
                                      SamitiDetailId = p.SamitiDetailId,
                                      SartaSetupId = p.SartaSetupId,
                                      Peski_Katti = p.Peski_Katti,
                                      TolBikashSansthaId = p.TolBikashSansthaId,
                                      //WardName = _context.Ward.Where(x => x.Id == p.WardId).Select(x => x.Name).FirstOrDefault(),
                                      //OrganisationRepresentative
                                      Organization_Representative_Id = o == null ? 0 : o.Organization_Representative_Id,

                                      RepresentativeAddress = o == null ? "" : o.RepresentativeAddress,
                                      RepresentativeNameId = o == null ? 0 : o.RepresentativeNameId,
                                      RepresentativePostId = o == null ? 0 : o.RepresentativePostId,
                                      RepresentativeName = _context.UpabhoktaSamitiMemberDetail.Where(sn => sn.UpabhoktaSamitiMemberDetailId == o.RepresentativeNameId).Select(sn => sn.MemberName).FirstOrDefault(),
                                      RepresentativeDesignition = pe.WorkTypeId == 1 ? _context.SamitiPost.Where(sn => sn.Id == o.RepresentativePostId).Select(sn => sn.Name).FirstOrDefault() : _context.SchoolPost.Where(sn => sn.Id == o.RepresentativePostId).Select(sn => sn.Name).FirstOrDefault(),
                                      //ProjectEntryDetail
                                      ProjectEntryDetailId = ped.ProjectEntryDetail_Id,
                                      Project_Place = ped.Project_Place,
                                      Project_Objective = ped.Project_Objective,
                                      Project_Acceptance_By = ped.Project_Acceptance_By,
                                      Project_Start_Date = ped.Project_Start_Date,
                                      Project_End_Date = ped.Project_End_Date,
                                      Project_Complete_Date = ped.Project_Complete_Date,
                                      Project_estimated_Amount = ps.Project_estimated_Amount,
                                      Total_Amount_Source = ps.Total_Amount_Source,
                                      Total_Use_Amount = ped.Total_Use_Amount,
                                      ProjectAllocatedAmount = ped.ProjectAllocatedAmount,
                                      Project_Extend_End_Date = ped.Project_Extend_End_Date,
                                      Project_Working_Status = ped.Project_Working_Status,

                                      //ProjectSourceDetail
                                      ProjectSourceDetailId = ps.ProjectSourceDetailId,
                                      Nepal_Government = ps.Nepal_Government,
                                      Municipality = ps.Municipality,
                                      State = ps.State,
                                      NGO_INGO = ps.NGO_INGO,
                                      Community_Org = ps.Community_Org,
                                      Foreign_Org = ps.Foreign_Org,
                                      Public_Community = ps.Public_Community,
                                      Loan_Grant = ps.Loan_Grant,
                                      Other_Source = ps.Other_Source,

                                      //BeneficiariesGroup
                                      BeneficiariesGroupId = bg.BeneficiariesGroupId,
                                      Total_Female = bg.Total_Female,
                                      Total_House = bg.Total_House,
                                      Total_Male = bg.Total_Male,
                                      Community = bg.Community,
                                      Other = bg.Other,

                                      //PlanningPravidikDetails
                                      PlanningPravidikDetailId = pd == null ? 0 : pd.PlanningPravidikDetailId,
                                      ChettraId = pd == null ? 0 : pd.ChettraId,
                                      UpaChetraId = pd == null ? 0 : pd.UpaChetraId,
                                      UpaChetraDetailId = pd == null ? 0 : pd.UpaChetraDetailId,
                                      Detail = pd == null ? null : pd.Detail,
                                      Kaifiyat = pd == null ? null : pd.Kaifiyat,
                                      UnitId = pd == null ? 0 : pd.UnitId,
                                      EsDetail = pd == null ? null : pd.EsDetail,
                                      Pariman = pd == null ? null : pd.Pariman,
                                      UnitName = pd == null ? null : pd.Unit.Name,
                                      //PlanningEntry
                                      PlanningEntryId = pe.PlanningEntryId,

                                      Work_Details = pe.Work_Details,
                                      Amount_Estimate = pe.Amount_Estimate,
                                      WorkTypeId = pe.WorkTypeId,

                                      WorkAreaId = pe.WorkAreaId,
                                      SerialNo = pe.SerialNo,
                                      BudgetSourceId = pe.BudgetSourceId,
                                      Planning_Type = pe.Planning_Type,
                                      //UpaBhoktaSamiti_HeadName = pe.UpaBhoktaSamiti_HeadName,
                                      //Contractor_Name = pe.Contractor_Name,
                                      BudgetSirshakNo = pe.BudgetSirshakNo,
                                      BudgetSirshak = pe.BudgetSirshak,
                                      KharchaSirshak = pe.KharchaSirshak,
                                      KharchaSirshakNo = pe.KharchaSirshakNo,
                                      TayarGarneId = pe.TayarGarneId,
                                      SifarisGarneId = pe.SifarisGarneId,
                                      RujuGarneId = pe.RujuGarneId,
                                      SwikritGarneId = pe.SwikritGarneId,
                                      PravidhikEmployeeId = pe.PravidhikEmployeeId,
                                      PlanningTypeId = pe.PlanningTypeId,
                                      PlanningSanketNo = pe.PlanningSanketNo,
                                      SifarisGarnePadId = pe.SifarisGarnePadId,
                                      SwikritGarnePadId = pe.SwikritGarnePadId,
                                      PravidhikEmployeePadId = pe.PravidhikEmployeePadId,
                                      RujuGarnePadId = pe.RujuGarnePadId,
                                      TayarGarnePadId = pe.TayarGarnePadId,
                                      WardSifarishId = pe.WardSifarishId,
                                      WardSifarishPadId = pe.WardSifarishPadId,
                                      WardSwikritId = pe.WardSwikritId,
                                      WardSwikritPadId = pe.WardSwikritPadId,
                                      SifarishGarneName = _context.Employee.Where(x => x.Id == pe.SifarisGarneId).Select(x => x.Name).FirstOrDefault(),
                                      SifarishGarnePost = _context.Pada.Where(x => x.Id == pe.SifarisGarnePadId).Select(x => x.Name).FirstOrDefault(),
                                      SwikritGarneName = _context.Employee.Where(x => x.Id == pe.SwikritGarneId).Select(x => x.Name).FirstOrDefault(),
                                      SwikritGarnePost = _context.Pada.Where(x => x.Id == pe.SwikritGarnePadId).Select(x => x.Name).FirstOrDefault(),
                                      WardSifarishName = _context.Employee.Where(x => x.Id == pe.WardSifarishId).Select(x => x.Name).FirstOrDefault(),
                                      WardSifarishPost = _context.Pada.Where(x => x.Id == pe.WardSifarishPadId).Select(x => x.Name).FirstOrDefault(),
                                      WardSwikritName = _context.Employee.Where(x => x.Id == pe.WardSwikritId).Select(x => x.Name).FirstOrDefault(),
                                      WardSwikitPost = _context.Pada.Where(x => x.Id == pe.WardSwikritPadId).Select(x => x.Name).FirstOrDefault(),

                                      //MunicipalitySamitiManjuriPatra
                                      Municipality_Manjuri_Date = ms.Municipality_Manjuri_Date,

                                      //AayojanaMaintainance
                                      AayojanaMaintainanceId = am.AayojanaMaintainanceId,
                                      ResponsibleOrg = am.ResponsibleOrg,
                                      Janashram = am.Janashram,
                                      SewaSulka = am.SewaSulka,
                                      DasturChanda = am.DasturChanda,
                                      LagatAnudhan = am.LagatAnudhan,
                                      InterestSaving = am.InterestSaving,

                                      //AmanatDetail
                                      AmanatDetailId = ad.AmanatDetailId,
                                      AmanatName = ad.AmanatName,
                                      Darja = ad.Darja,
                                      AmantSahiDate = ad.AmantSahiDate,
                                      FiscalYearName = _context.FiscalYear.Where(y => y.Id == p.FiscalYearId).Select(q => q.Name).FirstOrDefault(),
                                      //Project_Name = _context.YojanaSetup.Where(y => y.YojanaSetupId == p.YojanaId).Select(q => q.YojanaName).FirstOrDefault(),
                                      BudgetSourceName = _context.BudgetSource.Where(y => y.BudgetSourceId == pe.BudgetSourceId).Select(q => q.BudgetSourceName).FirstOrDefault(),
                                      ChetraName = pd == null ? null : _context.Chettra.Where(y => y.ChettraId == pd.ChettraId).Select(q => q.ChettraName).FirstOrDefault(),
                                      UpaChetraName = pd == null ? null : _context.UpaChetra.Where(y => y.UpaChettraId == pd.UpaChetraId).Select(q => q.UpaChettra).FirstOrDefault(),
                                      UpaChetraDetailName = pd == null ? null : _context.UpaChetraDetail.Where(y => y.UpaChetraDetailId == pd.UpaChetraDetailId).Select(q => q.Name).FirstOrDefault(),
                                      SartaName = _context.SartaSetup.Where(s => s.SartaSetupId == p.SartaSetupId).Select(s => s.Name).FirstOrDefault(),
                                  },

                              }).FirstOrDefaultAsync() ?? new FormatViewModel();
            data.UpavoktaSamiti = _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId).Select(x => new UpavoktaSamitiDetailViewModel()
            {
                UpabhoktaSamitiDetailId = x.UpabhoktaSamitiDetailId,
                //SamitiDate = x.SamitiDate,
                Samiti_Estd_Date = x.Samiti_Estd_Date,
                NepaliSamitiEstdDate = x.NepaliSamitiEstdDate,
                Name = x.Name??"",
                //ContactNo = x.ContactNo,
                //SahiDate = x.SahiDate,
                Beneficiaries_Attendance = x.Beneficiaries_Attendance,
                Beneficiaries_Absent = x.Beneficiaries_Absent,
                //AnugamanMember = x.AnugamanMember,
                NibedanMiti = x.NibedanMiti,
                Female_Present = x.Female_Present,
                Male_Present = x.Male_Present,
                Status = x.Status,
                BankId = x.BankId,
                BankName = x.class_A_Bank_List.BankName_Nep,
                BankAddress = x.class_A_Bank_List.Address,

                AccountNumber = x.AccountNumber,

                Adakshya = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 1 && q.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(q => q.MemberName).FirstOrDefault(),
                Address = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 1 && q.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(q => q.Address).FirstOrDefault(),
                ContactNo = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 1 && q.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(q => q.PhoneNo).FirstOrDefault(),
                Kosadakshya = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 2 && q.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(q => q.MemberName).FirstOrDefault(),
                Sachib = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 3 && q.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(q => q.MemberName).FirstOrDefault(),

                samitiMemberDetaillist = _context.UpabhoktaSamitiMemberDetail.Where(y => y.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId)
                .Select(z => new UpavoktaSamitiMemberDetailViewModel()
                {
                    UpabhoktaSamitiMemberDetailId = z.UpabhoktaSamitiMemberDetailId,
                    SamitiPostId = z.SamitiPostId,
                    PadaName = _context.SamitiPost.Where(q => q.Id == z.SamitiPostId).Select(q => q.Name).FirstOrDefault(),
                    MemberName = z.MemberName,
                    Address = z.Address,
                    FatherName = z.FatherName,
                    GrandFatherName = z.GrandFatherName,
                    Age = z.Age,
                    DOB = z.DOB,
                    PhoneNo = z.PhoneNo,
                    CitizenshipNumber = z.CitizenshipNumber,
                    //ImagePath = z.ImagePath,
                    Status = z.Status,
                }).ToList() ?? new List<UpavoktaSamitiMemberDetailViewModel>(),
               
            }).FirstOrDefault() ?? new UpavoktaSamitiDetailViewModel();
            //data.PlanningSamjhauta.AnugumanmemberList = _context.AnugamanMember.Where(a => a.UpabhoktaSamitiDetailId == data.UpavoktaSamiti.UpabhoktaSamitiDetailId)
            //   .Select(a => new AnugamanViewModel()
            //   {

            //   }).ToListAsync()?? new List<AnugamanViewModel>();
            var aa = _context.UpabhoktaSamitiDetailYojanas.Where(b => b.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId).Select(u => u.YojanaSetup.WardId).FirstOrDefault();

            data.PlanningSamjhauta.AnugamanSamitiSetup.AnugamanSamitiList = _context.AnugamanSamitiMember.Where(member => member.AnugamanSamitiSetup.WardId == aa).Select(member => new AnugamanSamitiMemberViewModel()
            {
                AnugamanMemberId = member.AnugamanMemberId,
                PostId = member.PostId,
                PostName = _context.Pada.Where(y => y.Id == member.PostId).Select(z => z.Name).FirstOrDefault(),
                Name = member.Name,
            }).ToList() ?? new List<AnugamanSamitiMemberViewModel>();
            data.PlanningSamjhauta.AnugamanSamitiSetup.AnugamanSamitiListPalika = _context.AnugamanSamitiMember.Where(member => member.AnugamanSamitiSetup.WardId == null).Select(member => new AnugamanSamitiMemberViewModel()
            {
                AnugamanMemberId = member.AnugamanMemberId,
                PostId = member.PostId,
                PostName = _context.Pada.Where(y => y.Id == member.PostId).Select(z => z.Name).FirstOrDefault(),
                Name = member.Name,
            }).ToList() ?? new List<AnugamanSamitiMemberViewModel>();
            data.PlanningSamjhauta.YojanaNames = string.Join(", ", await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToListAsync());
            data.PlanningSamjhauta.YojanaWards = string.Join(", ", await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId)
                .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToListAsync());
            data.PlanningSamjhauta.YojanaAddress = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId).Select(x => x.UpabhoktaSamitiDetail.Address).FirstOrDefaultAsync();
            data.PlanningSamjhauta.WardAddress = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.PlanningSamjhauta.SamitiDetailId)
                .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Address).FirstOrDefault()).FirstOrDefaultAsync();
            return data ?? new FormatViewModel();

        }

        public async Task<bool> InsertUpdateYojanaKarya(YojanaKaryakramChecklistViewModel model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.Id > 0)
                    {
                        var data = await _context.YojanaKaryakramChecklist.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                        if (data != null)
                        { 
                            data.FemalePercentage=model.FemalePercentage;
                            data.Rahobhar=model.Rahobhar;
                            data.AnugamanSamiti = model.AnugamanSamiti;
                            data.AtleastTwoFemale=model.AtleastTwoFemale;
                            data.Bhelabata = model.Bhelabata;
                            data.MahilaPratinidhi = model.MahilaPratinidhi;
                            data.FarfarakBaki=model.FarfarakBaki;
                            data.KanunBamojim = model.FarfarakBaki;
                            data.GathanNirnaya = model.GathanNirnaya;
                            data.LagatAnuman=model.LagatAnuman;
                            data.Citizenship = model.Citizenship;
                            data.PhotoOfworkingArea=model.PhotoOfworkingArea;
                            data.EkpariwarKobadiSadshya = model.EkpariwarKobadiSadshya;
                            data.WardSifarish=model.WardSifarish;
                            _context.Entry(data).State = EntityState.Modified;
                        }
                        return true;
                    }
                    else
                    {
                        var data = new YojanaKaryakramChecklist()
                        {
                            PlanningSamjhuataId=model.PlanningSamjhuataId,
                             FemalePercentage = model.FemalePercentage,
                             Rahobhar = model.Rahobhar,
                             AnugamanSamiti = model.AnugamanSamiti,
                             AtleastTwoFemale = model.AtleastTwoFemale,
                             Bhelabata = model.Bhelabata,
                             MahilaPratinidhi = model.MahilaPratinidhi,
                             FarfarakBaki = model.FarfarakBaki,
                             KanunBamojim = model.FarfarakBaki,
                             GathanNirnaya = model.GathanNirnaya,
                             LagatAnuman = model.LagatAnuman,
                             Citizenship = model.Citizenship,
                             PhotoOfworkingArea = model.PhotoOfworkingArea,
                             EkpariwarKobadiSadshya = model.EkpariwarKobadiSadshya,
                             WardSifarish = model.WardSifarish,
                        };
                        await _context.YojanaKaryakramChecklist.AddRangeAsync(data);
                     
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }

                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return false;
                }                
            }
        }
        public async Task<YojanaKaryakramChecklistViewModel>GetYojanaKaryakramById(int PlanningSamjhautaId)
        {
            var data = await _context.YojanaKaryakramChecklist.Where(x => /*x.Id == Id &&*/ x.PlanningSamjhuataId==PlanningSamjhautaId).Select(model => new YojanaKaryakramChecklistViewModel()
            {
                Bhelabata=model.Bhelabata,
                MahilaPratinidhi=model.MahilaPratinidhi,
                FemalePercentage=model.FemalePercentage,
                Rahobhar=model.Rahobhar,
                KanunBamojim=model.KanunBamojim,
                AtleastTwoFemale=model.AtleastTwoFemale,
                AnugamanSamiti=model.AnugamanSamiti,
                GathanNirnaya=model.GathanNirnaya,
                Citizenship=model.Citizenship,
                LagatAnuman=model.LagatAnuman,
                PhotoOfworkingArea=model.PhotoOfworkingArea,
                FarfarakBaki=model.FarfarakBaki,
                EkpariwarKobadiSadshya=model.EkpariwarKobadiSadshya,
                WardSifarish=model.WardSifarish
            }).FirstOrDefaultAsync();
            return data;
        }
    }
}
