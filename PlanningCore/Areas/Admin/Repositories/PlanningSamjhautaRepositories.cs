using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Drawing.Printing;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanningCore.Areas.Admin.Repositories
{

	public class PlanningSamjhautaRepositories : IPlanningSamjhauta
	{
		private readonly PlanningContext _context;
		private readonly string _userId = null;
		private readonly IUtility _utility = null;
		public PlanningSamjhautaRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, IUtility utility)
		{
			_context = context;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			_utility = utility;
		}
		#region Delete
		public async Task<bool> DeletePlanningSamjhautaById(int id)
		{
			var data = await _context.PlanningSamjhauta.Where(x => x.PlanningSamjhautaId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				data.Status = false;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}
        #endregion Delete Samjhauta
        #region GetById
        public async Task<PlanningSamjhautaViewModel> GetPlanningSamjhautaById(int id)
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
                              select new PlanningSamjhautaViewModel()
                              {
                                  PlanningSamjhautaId = p.PlanningSamjhautaId,
                                  FiscalYearId = p.FiscalYearId,
                                  Contegency_Amount = p.Contegency_Amount,
                                  MarmatSambhar_Amount = p.MarmatSambhar_Amount,
                                  Total_Amount = p.Total_Amount,
                                  Contegency_Percentage = p.Contegency_Percentage,
                                  Status = true,
                                  Samjhauta_Acceptance = p.Samjhauta_Acceptance,
                                  SamitiDetailId = p.SamitiDetailId,
                                  SartaSetupId = p.SartaSetupId,
                                  Peski_Katti = p.Peski_Katti,
                                  TolBikashSansthaId = p.BidhyalayaBewasthapanSansthaId,
                                  NewTolBikashSansthaId = p.TolBikashSansthaId,
                                  //Samjhauta_Org_Name = _context.UpabhoktaSamitiDetail.Where(sn => sn.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(sn => sn.Name).FirstOrDefault(),
                                  Samjhauta_Org_Name = pe.WorkTypeId == 1 ? _context.UpabhoktaSamitiDetail.Where(sn => sn.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(sn => sn.Name).FirstOrDefault() :
                                                       pe.WorkTypeId == 2 ? _context.TolBikashSanstha.Where(sn => sn.TolBikashSansthaId == p.BidhyalayaBewasthapanSansthaId).Select(sn => sn.TolBikashSansthaName).FirstOrDefault() :
                                                       pe.WorkTypeId == 3 ? _context.NewToleBikash.Where(sn => sn.Id == p.TolBikashSansthaId).Select(sn => sn.Name).FirstOrDefault() : "",
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
                                  SartaDiscription = _context.SartaSetup.Where(s => s.SartaSetupId == p.SartaSetupId).Select(s => s.Description).FirstOrDefault(),
                              }).FirstOrDefaultAsync() ?? new PlanningSamjhautaViewModel();

            data.PlanningSamjhautaKistaFirstDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "पहिलो").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
            {
                Payment_Records_Id = x.Payment_Records_Id,
                Kista_Kram = x.Kista_Rakam,

                Payment_Date = x.Payment_Date,
                Kista_Rakam = x.Kista_Rakam,
                Nirmarn_Samagri = x.Nirmarn_Samagri,
                Remarks = x.Remarks,
            }).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel();
            data.PlanningSamjhautaKistaSecondDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "दोश्राे").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
            {
                Payment_Records_Id = x.Payment_Records_Id,
                Kista_Kram = x.Kista_Rakam,
                Payment_Date = x.Payment_Date,
                Kista_Rakam = x.Kista_Rakam,
                Nirmarn_Samagri = x.Nirmarn_Samagri,
                Remarks = x.Remarks,
            }).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel();
            data.PlanningSamjhautaKistaThirdDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "तेश्रो").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
            {
                Payment_Records_Id = x.Payment_Records_Id,
                Kista_Kram = x.Kista_Rakam,
                Payment_Date = x.Payment_Date,
                Kista_Rakam = x.Kista_Rakam,
                Nirmarn_Samagri = x.Nirmarn_Samagri,
                Remarks = x.Remarks,
            }).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel();
            if (data.WorkTypeId == 1)
            {
                data.samitiView = _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId).Select(x => new UpavoktaSamitiDetailViewModel()
                {
                    UpabhoktaSamitiDetailId = x.UpabhoktaSamitiDetailId,
                    //SamitiDate = x.SamitiDate,
                    Samiti_Estd_Date = x.Samiti_Estd_Date,
                    NepaliSamitiEstdDate = x.NepaliSamitiEstdDate,
                    Name = x.Name,
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
                data.YojanaNames = string.Join(", ", await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToListAsync());
                data.YojanaWards = string.Join(", ", await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToListAsync());
                data.YojanaAddress = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId).Select(x => x.UpabhoktaSamitiDetail.Address).FirstOrDefaultAsync();
                data.WardAddress = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Address).FirstOrDefault()).FirstOrDefaultAsync();
            }
            else if (data.WorkTypeId == 2)
            {
                var tolBikashSanstha = await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId)
                    .Select(x => new
                    {
                        x.TolBikashSansthaId,
                        x.TolSamitiEstdDate,
                        x.TolBikashSansthaName,
                        x.Beneficiaries_Attendance,
                        x.Beneficiaries_Absent,
                        x.NibedanMiti,
                        x.Female_Present,
                        x.Status,
                        x.Address,
                        x.BankName,
                        x.AccountNumber
                    })
                    .FirstOrDefaultAsync();
                if (tolBikashSanstha == null)
                {
                    data.samitiView = new UpavoktaSamitiDetailViewModel();
                }
                else
                {
                    var memberDetails = await _context.TolBikashSansthaMember
                        .Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId)
                        .Select(z => new UpavoktaSamitiMemberDetailViewModel
                        {
                            UpabhoktaSamitiMemberDetailId = z.TolBikashSansthaMemberId,
                            SamitiPostId = z.SchoolPostId,
                            PadaName = _context.SchoolPost.Where(p => p.Id == z.SchoolPostId).Select(q => q.Name).FirstOrDefault(),
                            MemberName = z.Name,
                            Address = z.Address,
                            FatherName = z.FatherName,
                            GrandFatherName = z.GrandFatherName,
                            PhoneNo = z.PhoneNumber,
                            CitizenshipNumber = z.NagariktaNumber
                        })
                        .ToListAsync();

                    data.samitiView = new UpavoktaSamitiDetailViewModel
                    {
                        UpabhoktaSamitiDetailId = tolBikashSanstha.TolBikashSansthaId,
                        NepaliSamitiEstdDate = tolBikashSanstha.TolSamitiEstdDate,
                        Name = tolBikashSanstha.TolBikashSansthaName,
                        Beneficiaries_Attendance = Decimal.TryParse(tolBikashSanstha.Beneficiaries_Attendance, out var attendance) ? attendance : 0,
                        Beneficiaries_Absent = Decimal.TryParse(tolBikashSanstha.Beneficiaries_Absent, out var absent) ? absent : 0,
                        NibedanMiti = tolBikashSanstha.NibedanMiti,
                        Female_Present = tolBikashSanstha.Female_Present,
                        Status = tolBikashSanstha.Status,
                        BankName = tolBikashSanstha.BankName,
                        AccountNumber = tolBikashSanstha.AccountNumber,
                        Adakshya = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.MemberName,
                        Address = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.Address,
                        ContactNo = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.PhoneNo,
                        Kosadakshya = memberDetails.FirstOrDefault(m => m.SamitiPostId == 2)?.MemberName,
                        Sachib = memberDetails.FirstOrDefault(m => m.SamitiPostId == 3)?.MemberName,
                        samitiMemberDetaillist = memberDetails
                    };

                }
                data.YojanaNames = string.Join(", ", await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId).Select(x => x.yojanaSetup.YojanaName).ToListAsync());
                data.YojanaWards = string.Join(", ", await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.yojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToListAsync());
                data.YojanaAddress = await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId).Select(x => x.Address).FirstOrDefaultAsync();
                data.WardAddress = await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == data.TolBikashSansthaId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.yojanaSetup.WardId).Select(w => w.Address).FirstOrDefault()).FirstOrDefaultAsync();

            }
            else if (data.WorkTypeId == 3)
            {
                var tolBikashSanstha = await _context.NewToleBikash.Where(x => x.Id == data.NewTolBikashSansthaId)
                    .Select(x => new
                    {
                        x.Id,
                        x.NepaliSamitiEstdDate,
                        x.Name,
                        x.Beneficiaries_Attendance,
                        x.Beneficiaries_Absent,
                        x.NibedanMiti,
                        x.Female_Present,
                        x.Male_Present,
                        x.Status,
                        x.BankId,
                        x.AccountNumber
                    })
                    .FirstOrDefaultAsync();
                if (tolBikashSanstha == null)
                {
                    data.samitiView = new UpavoktaSamitiDetailViewModel();
                }
                else
                {
                    var memberDetails = await _context.NewToleBikashMemberDetail
                        .Where(x => x.NewToleBikashlId == data.NewTolBikashSansthaId)
                        .Select(z => new UpavoktaSamitiMemberDetailViewModel
                        {
                            UpabhoktaSamitiMemberDetailId = z.Id,
                            SamitiPostId = z.SamitiPostId,
                            PadaName = _context.SamitiPost.Where(p => p.Id == z.SamitiPostId).Select(q => q.Name).FirstOrDefault(),
                            MemberName = z.MemberName,
                            Address = z.Address,
                            PhoneNo = z.PhoneNo,
                            Status = z.Status,
                            DOB = z.DOB,
                            CitizenshipNumber = z.CitizenshipNumber
                        })
                        .ToListAsync();

                    data.samitiView = new UpavoktaSamitiDetailViewModel
                    {
                        UpabhoktaSamitiDetailId = tolBikashSanstha.Id,
                        NepaliSamitiEstdDate = tolBikashSanstha.NepaliSamitiEstdDate,
                        Name = tolBikashSanstha.Name,
                        Beneficiaries_Attendance = tolBikashSanstha.Beneficiaries_Attendance,
                        Beneficiaries_Absent = tolBikashSanstha.Beneficiaries_Absent,
                        NibedanMiti = tolBikashSanstha.NibedanMiti,
                        Female_Present = tolBikashSanstha.Female_Present,
                        Status = tolBikashSanstha.Status,
                        BankName = _context.Class_A_Bank_List.Where(b => b.Class_A_Bank_List_Id == tolBikashSanstha.BankId).Select(bn => bn.BankName_Nep).FirstOrDefault(),
                        AccountNumber = tolBikashSanstha.AccountNumber,
                        Adakshya = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.MemberName,
                        Address = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.Address,
                        ContactNo = memberDetails.FirstOrDefault(m => m.SamitiPostId == 1)?.PhoneNo,
                        Kosadakshya = memberDetails.FirstOrDefault(m => m.SamitiPostId == 2)?.MemberName,
                        Sachib = memberDetails.FirstOrDefault(m => m.SamitiPostId == 3)?.MemberName,
                        samitiMemberDetaillist = memberDetails
                    };

                }
                data.YojanaNames = string.Join(", ", await _context.NewToleBikashYojanas.Where(x => x.NewToleBikashId == data.NewTolBikashSansthaId).Select(x => x.YojanaSetup.YojanaName).ToListAsync());
                data.YojanaWards = string.Join(", ", await _context.NewToleBikashYojanas.Where(x => x.NewToleBikashId == data.NewTolBikashSansthaId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToListAsync());
                data.YojanaAddress = await _context.NewToleBikashYojanas.Where(x => x.NewToleBikashId == data.NewTolBikashSansthaId).Select(x => x.NewToleBikash.Address).FirstOrDefaultAsync();
                data.WardAddress = await _context.NewToleBikashYojanas.Where(x => x.NewToleBikashId == data.NewTolBikashSansthaId)
                    .Select(x => _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Address).FirstOrDefault()).FirstOrDefaultAsync();

            }
            data.DocumentsList = _context.DocumentUploaded.Where(x => x.PlanningSamjhuataId == data.PlanningSamjhautaId).Select(x => new PlanningDocumentUploadedViewModel()
            {
                Id = x.Id,
                PlanningSamjhuataId = x.PlanningSamjhuataId,
                DocumentTypeName = _context.DocumentType.Where(c => c.DocumentTypeId == x.DocumentTypeId).Select(c => c.DocumentTypeName).FirstOrDefault(),
                ImagePath = x.ImagePath,
            }).ToList();

            return data ?? new PlanningSamjhautaViewModel();
        }
        #endregion GetById
        #region SamjhautaList
        public async Task<List<PlanningSamjhautaViewModel>> GetPlanningSamjhautaList()
		{
			var data = await _context.PlanningSamjhauta.Where(x => x.IsDeleted == false).Select(x => new PlanningSamjhautaViewModel()
			{
				PlanningSamjhautaId = x.PlanningSamjhautaId,
				FiscalYearId = x.FiscalYearId,
				FiscalYearName = _context.FiscalYear.Where(y => y.Id == x.FiscalYearId).Select(y => y.Name).FirstOrDefault(),
				SerialNo = _context.PlanningEntry.Where(z => z.PlanningSamjhautaId == x.PlanningSamjhautaId).Select(z => z.SerialNo).FirstOrDefault(),

				Contegency_Amount = x.Contegency_Amount,
				MarmatSambhar_Amount = x.MarmatSambhar_Amount,
				Total_Amount = x.Total_Amount,
				Contegency_Percentage = x.Contegency_Percentage,
				Status = true,
				Samjhauta_Acceptance = x.Samjhauta_Acceptance,

				Project_Name = x.YojanaSetup.YojanaName,
				Project_Start_Date = _context.ProjectEntryDetail.Where(z => z.PlanningSamjhautaId == x.PlanningSamjhautaId).Select(z => z.Project_Start_Date).FirstOrDefault(),
				Project_End_Date = _context.ProjectEntryDetail.Where(z => z.PlanningSamjhautaId == x.PlanningSamjhautaId).Select(z => z.Project_End_Date).FirstOrDefault(),
				SamitiDetailId = x.SamitiDetailId,
				SartaSetupId = x.SartaSetupId,
				Peski_Katti = x.Peski_Katti,
				TolBikashSansthaId = x.TolBikashSansthaId,

			}).ToListAsync() ?? new List<PlanningSamjhautaViewModel>();

			return data;
		}

        public async Task<List<PlanningSamjhautaViewModel>> GetPlanningSamjhautaAllList(int? wardId, string name)
        {
            wardId = (wardId == null || wardId == 0) ? (await _utility.GetWardNoForLogin_Role_User() ?? 0) : wardId;

            var data = new List<PlanningSamjhautaViewModel>();

            var upavokta = await (from p in _context.PlanningSamjhauta
                                  join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                                  join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                                  join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                                  from ps in projectSource.DefaultIfEmpty()
                                  join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                                  from ped in projectEntry.DefaultIfEmpty()
                                  where p.IsDeleted == false && (wardId == 0 || uy.YojanaSetup.WardId == wardId)
                                  && (string.IsNullOrWhiteSpace(name) || uy.YojanaSetup.YojanaName.Contains(name))
                                  select new PlanningSamjhautaViewModel()
                                  {
                                      PlanningSamjhautaId = p.PlanningSamjhautaId,
                                      Project_estimated_Amount = ps.Project_estimated_Amount,
                                      Total_Amount_Source = ps.Total_Amount_Source,
                                      Samjhauta_Org_Name = u.Name,
                                      Project_Start_Date = ped.Project_Start_Date,
                                      Project_End_Date = ped.Project_End_Date,
                                      Status = p.Status,
                                      Project_Complete_Date=ped.Project_Complete_Date,
                                      YojanaNames = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                      YojanaWards = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                      IsAnugaman = _context.AnugamanPartibdeans.Any(x => x.PlanningSamjhautaId == p.PlanningSamjhautaId),
                                  }).ToListAsync();
            if (upavokta != null)
            {
                data.AddRange(upavokta);
            }

            var bidhyalaya = await (from p in _context.PlanningSamjhauta
                                    join u in _context.TolBikashSanstha on p.BidhyalayaBewasthapanSansthaId equals u.TolBikashSansthaId
                                    //join uy in _context.TolBikashSansthaMember on u.TolBikashSansthaId equals uy.TolBikashSansthaId
                                    join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                                    from ps in projectSource.DefaultIfEmpty()
                                    join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                                    from ped in projectEntry.DefaultIfEmpty()
                                    where p.IsDeleted == false && (wardId == 0 || u.yojanaSetup.WardId == wardId)
                                    && (string.IsNullOrWhiteSpace(name) || u.yojanaSetup.YojanaName.Contains(name))
                                    select new PlanningSamjhautaViewModel()
                                    {
                                        PlanningSamjhautaId = p.PlanningSamjhautaId,
                                        Project_estimated_Amount = ps.Project_estimated_Amount,
                                        Total_Amount_Source = ps.Total_Amount_Source,
                                        Samjhauta_Org_Name = u.TolBikashSansthaName,
                                        Project_Start_Date = ped.Project_Start_Date,
                                        Project_End_Date = ped.Project_End_Date,
                                        Status = p.Status,
                                        YojanaNames = string.Join(",<br/> ", _context.TolBikashSanstha.Where(u => u.TolBikashSansthaId == p.BidhyalayaBewasthapanSansthaId).Select(x => _context.YojanaSetup.Where(y => y.YojanaSetupId == u.YojanaId).Select(yn => yn.YojanaName).FirstOrDefault()).ToList()),
                                        YojanaWards = string.Join(",<br/> ", _context.TolBikashSanstha.Where(u => u.TolBikashSansthaId == p.BidhyalayaBewasthapanSansthaId).Select(u => _context.Ward.Where(w => w.Id == _context.YojanaSetup.Where(y => y.YojanaSetupId == u.YojanaId).Select(yn => yn.WardId).FirstOrDefault()).Select(w => w.Name).FirstOrDefault()).ToList()),
                                    }).ToListAsync();
            if (bidhyalaya != null)
            {
                data.AddRange(bidhyalaya);
            }

            var tolBikas = await (from p in _context.PlanningSamjhauta
                                  join u in _context.NewToleBikash on p.TolBikashSansthaId equals u.Id
                                  join uy in _context.NewToleBikashMemberDetail on u.Id equals uy.NewToleBikashlId
                                  join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                                  from ps in projectSource.DefaultIfEmpty()
                                  join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                                  from ped in projectEntry.DefaultIfEmpty()
                                  join nt in _context.NewToleBikashYojanas on u.Id equals nt.YojanaId into newTolYojana
                                  from nt in newTolYojana.DefaultIfEmpty()
                                  where p.IsDeleted == false && (wardId == 0 || nt.YojanaSetup.WardId == wardId)
                                  && (string.IsNullOrWhiteSpace(name) || nt.YojanaSetup.YojanaName.Contains(name))
                                  select new PlanningSamjhautaViewModel()
                                  {
                                      PlanningSamjhautaId = p.PlanningSamjhautaId,
                                      Project_estimated_Amount = ps.Project_estimated_Amount,
                                      Total_Amount_Source = ps.Total_Amount_Source,
                                      Samjhauta_Org_Name = u.Name,
                                      Project_Start_Date = ped.Project_Start_Date,
                                      Project_End_Date = ped.Project_End_Date,
                                      Status = p.Status,
                                      YojanaNames = string.Join(",<br/> ", _context.NewToleBikashYojanas.Where(u => u.NewToleBikashId == p.TolBikashSansthaId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                      YojanaWards = string.Join(",<br/> ", _context.NewToleBikashYojanas.Where(u => u.NewToleBikashId == p.TolBikashSansthaId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  }).ToListAsync();

            if (tolBikas != null)
            {
                data.AddRange(tolBikas);
            }

            return data;

        }
        #endregion SamjhautaList
        #region SamjhautaCreate
        public async Task<int> InsertPlanningSamjhauta(PlanningSamjhautaViewModel model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var pId = 0;
                var fiscalYearId = await _context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefaultAsync();
                var yojanaId = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == model.SamitiDetailId).Select(x => x.YojanaId).FirstOrDefaultAsync();
                try
                {
                    if (model.PlanningSamjhautaId > 0)
                    {
                        var data = await _context.PlanningSamjhauta.Where(x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (data != null)
                        {
                            data.FiscalYearId = model.FiscalYearId;
                            data.Contegency_Amount = model.Contegency_Amount;
                            data.MarmatSambhar_Amount = model.MarmatSambhar_Amount;
                            data.Total_Amount = model.Total_Amount;
                            data.Contegency_Percentage = model.Contegency_Percentage;
                            data.Samjhauta_Acceptance = model.Samjhauta_Acceptance;
                            data.SamitiDetailId = model.SamitiDetailId;
                            data.SartaSetupId = model.SartaSetupId;
                            data.Peski_Katti = model.Peski_Katti;
                            data.TolBikashSansthaId = model.NewTolBikashSansthaId;
                            data.BidhyalayaBewasthapanSansthaId = model.TolBikashSansthaId;
                            data.Status = model.Status;
                            data.ModifiedBy = _userId;
                            data.ModifiedDate = DateTime.Now;
                            _context.Entry(data).State = EntityState.Modified;
                        }
                        var organizationRepresentative = await _context.OrganizationRepresentative.Where(/*x => x.Organization_Representative_Id == model.Organization_Representative_Id &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (organizationRepresentative != null)
                        {
                            organizationRepresentative.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            organizationRepresentative.RepresentativeAddress = model.RepresentativeAddress;
                            organizationRepresentative.RepresentativeNameId = model.RepresentativeNameId;
                            organizationRepresentative.RepresentativePostId = model.RepresentativePostId;

                            organizationRepresentative.Status = true;
                            organizationRepresentative.UpdatedBy = _userId;
                            organizationRepresentative.UpdatedDate = DateTime.Now;

                            _context.Entry(organizationRepresentative).State = EntityState.Modified;
                        }


                        var projectEntryDetail = await _context.ProjectEntryDetail.Where(/*x => x.ProjectEntryDetail_Id == model.ProjectEntryDetailId &&*/ x => x.ProjectEntryDetail_Id == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (projectEntryDetail != null)
                        {
                            projectEntryDetail.PlanningSamjhautaId = model.PlanningSamjhautaId;

                            projectEntryDetail.Project_Place = model.Project_Place;
                            projectEntryDetail.Project_Objective = model.Project_Objective;
                            projectEntryDetail.Project_Acceptance_By = model.Project_Acceptance_By;
                            projectEntryDetail.Project_Start_Date = model.Project_Start_Date;
                            projectEntryDetail.Project_End_Date = model.Project_End_Date;
                            projectEntryDetail.Status = true;
                            projectEntryDetail.UpdatedBy = _userId;
                            projectEntryDetail.UpdatedDate = DateTime.Now;

                            _context.Entry(projectEntryDetail).State = EntityState.Modified;
                        }

                        var projectSourceDetail = await _context.ProjectSourceDetail.Where(/*x => x.ProjectSourceDetailId == model.ProjectSourceDetailId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (projectSourceDetail != null)
                        {
                            projectSourceDetail.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            projectSourceDetail.Project_estimated_Amount = model.Project_estimated_Amount;
                            projectSourceDetail.Nepal_Government = model.Nepal_Government;
                            projectSourceDetail.Municipality = model.Municipality;
                            projectSourceDetail.State = model.State;
                            projectSourceDetail.NGO_INGO = model.NGO_INGO;
                            projectSourceDetail.Community_Org = model.Community_Org;
                            projectSourceDetail.Foreign_Org = model.Foreign_Org;
                            projectSourceDetail.Public_Community = model.Public_Community;
                            projectSourceDetail.Loan_Grant = model.Loan_Grant;
                            projectSourceDetail.Other_Source = model.Other_Source;
                            projectSourceDetail.Total_Amount_Source = model.Total_Amount_Source;
                            projectSourceDetail.Status = true;

                            projectSourceDetail.CreatedDate = DateTime.Now;

                            _context.Entry(projectSourceDetail).State = EntityState.Modified;
                        }

                        var beneficiariesGroup = await _context.BeneficiariesGroup.Where(/*x => x.BeneficiariesGroupId == model.BeneficiariesGroupId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (beneficiariesGroup != null)
                        {
                            beneficiariesGroup.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            beneficiariesGroup.Total_House = model.Total_House;
                            beneficiariesGroup.Total_Female = model.Total_Female;
                            beneficiariesGroup.Total_Male = model.Total_Male;
                            beneficiariesGroup.Community = model.Community;
                            beneficiariesGroup.Other = model.Other;
                            beneficiariesGroup.Status = true;
                            beneficiariesGroup.UpdatedBy = _userId;
                            beneficiariesGroup.UpdatedDate = DateTime.Now;

                            _context.Entry(beneficiariesGroup).State = EntityState.Modified;
                        }

                        var planningPravidik = await _context.PlanningPravidikDetails.Where(/*x => x.PlanningPravidikDetailId == model.PlanningPravidikDetailId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (planningPravidik != null)
                        {
                            planningPravidik.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            planningPravidik.FiscalYearId = model.FiscalYearId;
                            planningPravidik.ChettraId = model.ChettraId;
                            planningPravidik.UpaChetraId = model.UpaChetraId;
                            planningPravidik.UpaChetraDetailId = model.UpaChetraDetailId;
                            planningPravidik.IsDeleted = false;
                            planningPravidik.EsDetail = model.EsDetail;
                            planningPravidik.Detail = model.Detail;
                            planningPravidik.UnitId = model.UnitId;
                            planningPravidik.Pariman = model.Pariman;
                            planningPravidik.ModifiedBy = _userId;
                            planningPravidik.ModifiedDate = DateTime.Now;

                            _context.Entry(planningPravidik).State = EntityState.Modified;
                        }
                        else
                        {
                            planningPravidik = new PlanningPravidikDetails()
                            {
                                PlanningSamjhautaId = model.PlanningSamjhautaId,
                                FiscalYearId = model.FiscalYearId,
                                ChettraId = model.ChettraId,
                                UpaChetraId = model.UpaChetraId,
                                UpaChetraDetailId = model.UpaChetraDetailId,
                                IsDeleted = false,
                                EsDetail = model.EsDetail,
                                Detail = model.Detail,
                                UnitId = model.UnitId,
                                Pariman = model.Pariman,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.PlanningPravidikDetails.AddAsync(planningPravidik);
                            await _context.SaveChangesAsync();
                        }

                        var planningEntry = await _context.PlanningEntry.Where(/*x => x.PlanningEntryId == model.PlanningEntryId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (planningEntry != null)
                        {
                            planningEntry.FiscalYearId = model.FiscalYearId;
                            planningEntry.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            planningEntry.Work_Details = model.Work_Details;
                            planningEntry.BudgetSourceId = model.BudgetSourceId;
                            planningEntry.PlanningTypeId = model.PlanningTypeId;
                            planningEntry.Amount_Estimate = model.Amount_Estimate;
                            planningEntry.WorkTypeId = model.WorkTypeId;
                            planningEntry.WorkAreaId = model.WorkAreaId;
                            planningEntry.SerialNo = model.SerialNo;
                            planningEntry.Planning_Type = model.Planning_Type;
                            planningEntry.Status = true;
                            planningEntry.CreatedBy = _userId;
                            planningEntry.CreatedDate = DateTime.Now;
                            planningEntry.BudgetSirshakNo = model.BudgetSirshakNo;
                            planningEntry.BudgetSirshak = model.BudgetSirshak;
                            planningEntry.KharchaSirshak = model.KharchaSirshak;
                            planningEntry.KharchaSirshakNo = model.KharchaSirshakNo;
                            planningEntry.RujuGarnePadId = model.RujuGarnePadId;

                            planningEntry.RujuGarneId = model.RujuGarneId;
                            planningEntry.SifarisGarnePadId = model.SifarisGarnePadId;
                            planningEntry.SifarisGarneId = model.SifarisGarneId;
                            planningEntry.SwikritGarnePadId = model.SwikritGarnePadId;
                            planningEntry.SwikritGarneId = model.SwikritGarneId;
                            planningEntry.TayarGarnePadId = model.TayarGarnePadId;
                            planningEntry.TayarGarneId = model.TayarGarneId;
                            planningEntry.PravidhikEmployeePadId = model.PravidhikEmployeePadId;
                            planningEntry.PravidhikEmployeeId = model.PravidhikEmployeeId;
                            planningEntry.PlanningSanketNo = model.PlanningSanketNo;
                            planningEntry.WardSifarishId = model.WardSifarishId;
                            planningEntry.WardSifarishPadId = model.WardSifarishPadId;
                            planningEntry.WardSwikritPadId = model.WardSwikritPadId;
                            planningEntry.WardSwikritId = model.WardSwikritId;

                            _context.Entry(planningEntry).State = EntityState.Modified;
                        }

                        var municipalitySamitiManjuriPatraEntity = await _context.MunicipalitySamitiManjuriPatra.Where(/*x => x.MunicipalitySamitiManjuriPatraId == model.MunicipalitySamitiManjuriPatraId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (municipalitySamitiManjuriPatraEntity != null)
                        {
                            municipalitySamitiManjuriPatraEntity.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            municipalitySamitiManjuriPatraEntity.Municipality_Manjuri_Date = model.Municipality_Manjuri_Date;
                            municipalitySamitiManjuriPatraEntity.Status = true;
                            municipalitySamitiManjuriPatraEntity.UpdatedBy = _userId;
                            municipalitySamitiManjuriPatraEntity.UpdatedDate = DateTime.Now;

                            _context.Entry(municipalitySamitiManjuriPatraEntity).State = EntityState.Modified;
                        }

                        var aayojanaMaintainance = await _context.AayojanaMaintainance.Where(/*x => x.AayojanaMaintainanceId == model.AayojanaMaintainanceId &&*/ x => x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (aayojanaMaintainance != null)
                        {
                            aayojanaMaintainance.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            aayojanaMaintainance.ResponsibleOrg = model.ResponsibleOrg;
                            aayojanaMaintainance.Janashram = model.Janashram;
                            aayojanaMaintainance.SewaSulka = model.SewaSulka;
                            aayojanaMaintainance.DasturChanda = model.DasturChanda;
                            aayojanaMaintainance.LagatAnudhan = model.LagatAnudhan;
                            aayojanaMaintainance.InterestSaving = model.InterestSaving;
                            aayojanaMaintainance.Status = true;
                            aayojanaMaintainance.UpdatedBy = _userId;
                            aayojanaMaintainance.UpdatedDate = DateTime.Now;

                            _context.Entry(aayojanaMaintainance).State = EntityState.Modified;
                        }

                        var amanatKism = await _context.AmanatDetail.Where(x => /*x.AmanatDetailId == model.AmanatDetailId &&*/ x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                        if (amanatKism != null)
                        {
                            amanatKism.AmanatName = model.AmanatName;
                            amanatKism.Darja = model.Darja;
                            amanatKism.AmantSahiDate = model.AmantSahiDate;
                            amanatKism.PlanningSamjhautaId = model.PlanningSamjhautaId;
                            amanatKism.Status = true;
                            amanatKism.CreatedBy = _userId;
                            amanatKism.CreatedDate = DateTime.Now;

                            _context.Entry(amanatKism).State = EntityState.Modified;
                        }

                        if (model.PlanningSamjhautaKistaFirstDetailsList != null)
                        {

                            var paymentRecords = await _context.PaymentRecord.Where(x => /*x.Payment_Records_Id == model.PlanningSamjhautaKistaFirstDetailsList.Payment_Records_Id &&*/ x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                            if (paymentRecords != null)
                            {
                                paymentRecords.PlanningSamjhautaId = model.PlanningSamjhautaId;
                                //  paymentRecords.Kista_Kram = item.Kista_Kram;
                                paymentRecords.Kista_Kram = "पहिलो";
                                paymentRecords.Kista_Rakam = model.PlanningSamjhautaKistaFirstDetailsList.Kista_Rakam;
                                paymentRecords.Payment_Date = model.PlanningSamjhautaKistaFirstDetailsList.Payment_Date;
                                paymentRecords.Nirmarn_Samagri = model.PlanningSamjhautaKistaFirstDetailsList.Nirmarn_Samagri;
                                paymentRecords.Remarks = model.PlanningSamjhautaKistaFirstDetailsList.Remarks;
                                paymentRecords.Status = true;
                                paymentRecords.CreatedBy = _userId;
                                paymentRecords.CreatedDate = DateTime.Now;

                                _context.Entry(paymentRecords).State = EntityState.Modified;
                            }

                        }

                        if (model.PlanningSamjhautaKistaSecondDetailsList != null)
                        {

                            var paymentRecords = await _context.PaymentRecord.Where(x => /*x.Payment_Records_Id == model.PlanningSamjhautaKistaSecondDetailsList.Payment_Records_Id &&*/ x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                            if (paymentRecords != null)
                            {
                                paymentRecords.PlanningSamjhautaId = model.PlanningSamjhautaId;
                                paymentRecords.Kista_Kram = "दोश्रो";
                                paymentRecords.Kista_Rakam = model.PlanningSamjhautaKistaSecondDetailsList.Kista_Rakam;
                                paymentRecords.Payment_Date = model.PlanningSamjhautaKistaSecondDetailsList.Payment_Date;
                                paymentRecords.Nirmarn_Samagri = model.PlanningSamjhautaKistaSecondDetailsList.Nirmarn_Samagri;
                                paymentRecords.Remarks = model.PlanningSamjhautaKistaSecondDetailsList.Remarks;
                                paymentRecords.Status = true;
                                paymentRecords.CreatedBy = _userId;
                                paymentRecords.CreatedDate = DateTime.Now;

                                _context.Entry(paymentRecords).State = EntityState.Modified;
                            }

                        }

                        if (model.PlanningSamjhautaKistaThirdDetailsList != null)
                        {

                            var paymentRecords = await _context.PaymentRecord.Where(x =>/* x.Payment_Records_Id == model.PlanningSamjhautaKistaThirdDetailsList.Payment_Records_Id &&*/ x.PlanningSamjhautaId == model.PlanningSamjhautaId).FirstOrDefaultAsync();
                            if (paymentRecords != null)
                            {
                                paymentRecords.PlanningSamjhautaId = model.PlanningSamjhautaId;
                                paymentRecords.Kista_Kram = "तेश्रो";
                                paymentRecords.Kista_Rakam = model.PlanningSamjhautaKistaThirdDetailsList.Kista_Rakam;
                                paymentRecords.Payment_Date = model.PlanningSamjhautaKistaThirdDetailsList.Payment_Date;
                                paymentRecords.Nirmarn_Samagri = model.PlanningSamjhautaKistaThirdDetailsList.Nirmarn_Samagri;
                                paymentRecords.Remarks = model.PlanningSamjhautaKistaThirdDetailsList.Remarks;
                                paymentRecords.Status = true;
                                paymentRecords.CreatedBy = _userId;
                                paymentRecords.CreatedDate = DateTime.Now;

                                _context.Entry(paymentRecords).State = EntityState.Modified;
                            }

                        }
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return model.PlanningSamjhautaId;
                    }
                    else
                    {
                        if (model.WorkTypeId == 1)
                        {
                            model.SamitiDetailId = model.SamitiDetailId;
                            model.TolBikashSansthaId = null;
                            model.NewTolBikashSansthaId = null;
                        }
                        else if (model.WorkTypeId == 2)
                        {
                            model.SamitiDetailId = null;
                            model.TolBikashSansthaId = model.TolBikashSansthaId;
                            model.NewTolBikashSansthaId = null;
                        }
                        else if (model.WorkTypeId == 3)
                        {
                            model.SamitiDetailId = null;
                            model.TolBikashSansthaId = null;
                            model.NewTolBikashSansthaId = model.NewTolBikashSansthaId;
                        }
                        var planningSamjhauta = new PlanningSamjhauta()
                        {
                            FiscalYearId = fiscalYearId,
                            Contegency_Amount = model.Contegency_Amount,
                            MarmatSambhar_Amount = model.MarmatSambhar_Amount,
                            Total_Amount = model.Total_Amount,
                            Contegency_Percentage = model.Contegency_Percentage,
                            Status = model.Status,
                            Samjhauta_Acceptance = model.Samjhauta_Acceptance,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                            SamitiDetailId = model.SamitiDetailId,
                            TolBikashSansthaId = model.NewTolBikashSansthaId,
                            BidhyalayaBewasthapanSansthaId = model.TolBikashSansthaId,
                            SartaSetupId = model.SartaSetupId,
                            //Peski_Katti = model.Peski_Katti,
                            //TolBikashSansthaId = model.TolBikashSansthaId,
                            IsDeleted = false,
                            //needs to updates

                        };
                        await _context.PlanningSamjhauta.AddAsync(planningSamjhauta);
                        _context.SaveChanges();
                        pId = planningSamjhauta.PlanningSamjhautaId;
                        var organizationRepresentativeEntitity = new OrganizationRepresentative()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            RepresentativeAddress = model.RepresentativeAddress,
                            RepresentativeNameId = model.RepresentativeNameId,
                            RepresentativePostId = model.RepresentativePostId,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.OrganizationRepresentative.AddAsync(organizationRepresentativeEntitity);

                        var projectEntryDetailEntity = new ProjectEntryDetail()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            //Project_Name = model.Project_Name,
                            Project_Place = model.Project_Place,
                            Project_Objective = model.Project_Objective,
                            Project_Acceptance_By = model.Project_Acceptance_By,
                            Project_Start_Date = model.Project_Start_Date,
                            Project_End_Date = model.Project_End_Date,
                            //Project_estimated_Amount = model.Project_estimated_Amount,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.ProjectEntryDetail.AddAsync(projectEntryDetailEntity);

                        var projectSourceDetailEntity = new ProjectSourceDetail()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            Project_estimated_Amount = model.Project_estimated_Amount,
                            Nepal_Government = model.Nepal_Government,
                            Municipality = model.Municipality,
                            State = model.State,
                            NGO_INGO = model.NGO_INGO,
                            Community_Org = model.Community_Org,
                            Foreign_Org = model.Foreign_Org,
                            Public_Community = model.Public_Community,
                            Loan_Grant = model.Loan_Grant,
                            Other_Source = model.Other_Source,
                            Total_Amount_Source = model.Total_Amount_Source,
                            Status = true,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.ProjectSourceDetail.AddAsync(projectSourceDetailEntity);

                        var beneficiariesGroupEntity = new BeneficiariesGroup()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            Total_House = model.Total_House,
                            Total_Female = model.Total_Female,
                            Total_Male = model.Total_Male,
                            Community = model.Community,
                            Other = model.Other,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.BeneficiariesGroup.AddAsync(beneficiariesGroupEntity);

                        var planningPravidik = new PlanningPravidikDetails()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            FiscalYearId = fiscalYearId,
                            ChettraId = model.ChettraId,
                            UpaChetraId = model.UpaChetraId,
                            UpaChetraDetailId = model.UpaChetraDetailId,
                            IsDeleted = false,
                            EsDetail = model.EsDetail,
                            Detail = model.Detail,
                            UnitId = model.UnitId,
                            Pariman = model.Pariman,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now
                        };
                        await _context.PlanningPravidikDetails.AddAsync(planningPravidik);

                        var planningEntry = new PlanningEntry()
                        {
                            FiscalYearId = model.FiscalYearId,
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            //PlanningName = model.PlanningName,
                            //PlanningStartDate = model.PlanningStartDate,
                            //PlanningEndDate = model.PlanningEndDate,
                            //Work_Details = model.Work_Details,
                            BudgetSourceId = model.BudgetSourceId,
                            PlanningTypeId = model.PlanningTypeId,
                            //Amount_Estimate = model.Amount_Estimate,
                            WorkTypeId = model.WorkTypeId,
                            WorkAreaId = model.WorkAreaId,
                            SerialNo = model.SerialNo,
                            Planning_Type = model.Planning_Type,
                            //UpaBhoktaSamiti_HeadName = model.UpaBhoktaSamiti_HeadName,
                            //Contractor_Name = model.Contractor_Name,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                            BudgetSirshakNo = model.BudgetSirshakNo,
                            BudgetSirshak = model.BudgetSirshak,
                            KharchaSirshak = model.KharchaSirshak,
                            KharchaSirshakNo = model.KharchaSirshakNo,
                            //RujuGarneId = model.RujuGarneId,
                            SifarisGarneId = model.SifarisGarneId,
                            SwikritGarneId = model.SwikritGarneId,
                            //TayarGarneId = model.TayarGarneId,
                            //PravidhikEmployeeId = model.PravidhikEmployeeId,
                            PlanningSanketNo = model.PlanningSanketNo,
                            //TayarGarnePadId = model.TayarGarnePadId,
                            SifarisGarnePadId = model.SifarisGarnePadId,
                            SwikritGarnePadId = model.SwikritGarnePadId,
                            WardSwikritId = model.WardSwikritId,
                            WardSwikritPadId = model.WardSwikritPadId,
                            WardSifarishId = model.WardSifarishId,
                            WardSifarishPadId = model.WardSifarishPadId,
                        };
                        await _context.PlanningEntry.AddAsync(planningEntry);

                        var municipalitySamitiManjuriPatraEntity = new MunicipalitySamitiManjuriPatra()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            Municipality_Manjuri_Date = model.Municipality_Manjuri_Date,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.MunicipalitySamitiManjuriPatra.AddAsync(municipalitySamitiManjuriPatraEntity);

                        var aayojanaMaintainanceEntity = new AayojanaMaintainance()
                        {
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            ResponsibleOrg = model.ResponsibleOrg,
                            Janashram = model.Janashram,
                            SewaSulka = model.SewaSulka,
                            DasturChanda = model.DasturChanda,
                            LagatAnudhan = model.LagatAnudhan,
                            InterestSaving = model.InterestSaving,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.AayojanaMaintainance.AddAsync(aayojanaMaintainanceEntity);

                        var amanatKism = new AmanatDetail()
                        {
                            AmanatName = model.AmanatName,
                            Darja = model.Darja,
                            AmantSahiDate = model.AmantSahiDate,
                            PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                            Status = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.AmanatDetail.AddAsync(amanatKism);

                        if (model.PlanningSamjhautaKistaFirstDetailsList != null)
                        {

                            var paymentRecordsEntity = new PaymentRecord()
                            {
                                PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                                // Kista_Kram = item.Kista_Kram,
                                Kista_Kram = "पहिलो",
                                Kista_Rakam = model.PlanningSamjhautaKistaFirstDetailsList.Kista_Rakam,
                                Payment_Date = model.PlanningSamjhautaKistaFirstDetailsList.Payment_Date,
                                Nirmarn_Samagri = model.PlanningSamjhautaKistaFirstDetailsList.Nirmarn_Samagri,
                                Remarks = model.PlanningSamjhautaKistaFirstDetailsList.Remarks,
                                Status = true,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.PaymentRecord.AddAsync(paymentRecordsEntity);


                        }
                        if (model.PlanningSamjhautaKistaSecondDetailsList != null)
                        {

                            var paymentRecordsEntity = new PaymentRecord()
                            {
                                PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                                //  Kista_Kram = item.Kista_Kram,
                                Kista_Kram = "दोश्रो",
                                Kista_Rakam = model.PlanningSamjhautaKistaSecondDetailsList.Kista_Rakam,
                                Payment_Date = model.PlanningSamjhautaKistaSecondDetailsList.Payment_Date,
                                Nirmarn_Samagri = model.PlanningSamjhautaKistaSecondDetailsList.Nirmarn_Samagri,
                                Remarks = model.PlanningSamjhautaKistaSecondDetailsList.Remarks,
                                Status = true,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.PaymentRecord.AddAsync(paymentRecordsEntity);

                        }
                        if (model.PlanningSamjhautaKistaThirdDetailsList != null)
                        {

                            var paymentRecordsEntity = new PaymentRecord()
                            {
                                PlanningSamjhautaId = planningSamjhauta.PlanningSamjhautaId,
                                // Kista_Kram = item.Kista_Kram,
                                Kista_Kram = "तेश्रो",
                                Kista_Rakam = model.PlanningSamjhautaKistaThirdDetailsList.Kista_Rakam,
                                Payment_Date = model.PlanningSamjhautaKistaThirdDetailsList.Payment_Date,
                                Nirmarn_Samagri = model.PlanningSamjhautaKistaThirdDetailsList.Nirmarn_Samagri,
                                Remarks = model.PlanningSamjhautaKistaThirdDetailsList.Remarks,
                                Status = true,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.PaymentRecord.AddAsync(paymentRecordsEntity);

                        }
                        if (yojanaId>0)
                        {
                            var yojanaSetup = await _context.YojanaSetup.FirstOrDefaultAsync(x => x.YojanaSetupId==yojanaId);
                            yojanaSetup.Status = "Samjhauta";
                            _context.Entry(yojanaSetup).State = EntityState.Modified;
                        }

                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return pId;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return 0;
                }
            }
        }

        #endregion SamjhautaCreate

        #region ProjkectComplete
        public async Task<bool> UpdateProjectDate(int? id, string date, string detail, string ekai, string pariman, string kaifiyat)
        {
            try
            {
                DateTime edate = DateTime.Now;
                var data = await _context.ProjectEntryDetail.FirstOrDefaultAsync(x => x.PlanningSamjhautaId == id);
                //var yojanaId= await _context.
                if (data != null)
                {
                    data.Project_Complete_Date = date;
                    _context.Entry(data).State = EntityState.Modified;
                }
                var chhetradata = await _context.PlanningPravidikDetails.FirstOrDefaultAsync(x => x.PlanningSamjhautaId == id);
                if (chhetradata != null)
                {
                    chhetradata.Detail = detail;
                    chhetradata.Pariman = pariman;
                    chhetradata.Kaifiyat = kaifiyat;
                    _context.Entry(data).State = EntityState.Modified;
                }
                var samitiId = await _context.PlanningSamjhauta.Where(x => x.PlanningSamjhautaId == id).Select(x => x.SamitiDetailId).FirstOrDefaultAsync();
                var yojanaId = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == samitiId).Select(x => x.YojanaId).FirstOrDefaultAsync();
                if (yojanaId > 0)
                {
                    var yojanaSetup = await _context.YojanaSetup.FirstOrDefaultAsync(x => x.YojanaSetupId == yojanaId);
                    yojanaSetup.Status = "Completed";
                    _context.Entry(yojanaSetup).State = EntityState.Modified;
                };
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region Bhuktani
        public async Task<PlanningBhuktaniViewModel> GetPlanningBhuktaniByBhuktaniId(int id)
		{
			var data = await _context.PlanningBhuktani.Where(model => model.PlanningBhuktaniId == id && model.Status == true).Select(model => new PlanningBhuktaniViewModel()
			{
				PlanningBhuktaniId = model.PlanningBhuktaniId,
				FiscalYearId = model.FiscalYearId,
				Nirman_Upabhokta = model.Nirman_Upabhokta,
				Aayojana_Karyakram = model.Aayojana_Karyakram,
				Kul_La_Ie = model.Kul_La_Ie,
				NaPa_Binayajit = model.NaPa_Binayajit,
				Others = model.Others,
				Jana_Sahabagita = model.Jana_Sahabagita,
				Peski = model.Peski,
				Technical_Amount = model.Technical_Amount,
				Kantigenci = model.Kantigenci,
				Remaining_Bhuktani_Amount = model.Remaining_Bhuktani_Amount,
				Agrim_Shulka = model.Agrim_Shulka,
				Bahal_Kar = model.Bahal_Kar,
				MarmatShmar = model.MarmatShmar,
				Katti_Rakam = model.Katti_Rakam,
				Aanya_Raaya = model.Aanya_Raaya,
				AdakshyaName = model.AdakshyaName,
				SamjhautaDate = model.SamjhautaDate,
				Farchot_Amount = model.Farchot_Amount,
				Running_Bhuktani = model.Running_Bhuktani,
				Samajik_Surekchya = model.Samajik_Surekchya,
				Parishramik = model.Parishramik,
				Dhuwani = model.Dhuwani,
				Royality = model.Royality,
				PlanningSamjhautaId = model.PlanningSamjhautaId,
				BhuktaniTypeId = model.BhuktaniTypeId,
				IsBhuktaniApproval = model.IsBhuktaniApproval,
				Status = model.Status,

			}).FirstOrDefaultAsync() ?? new PlanningBhuktaniViewModel();
			return data;
		}

		public async Task<List<PlanningBhuktaniViewModel>> GetPlanningBhuktaniListByPlanningSamjhautaId(int id)
		{
			var data = await _context.PlanningBhuktani.Where(model => model.PlanningSamjhautaId == id && model.Status == true).Select(model => new PlanningBhuktaniViewModel()
			{
				PlanningBhuktaniId = model.PlanningBhuktaniId,
				FiscalYearId = model.FiscalYearId,
				Nirman_Upabhokta = model.Nirman_Upabhokta,
				Aayojana_Karyakram = model.Aayojana_Karyakram,
				Kul_La_Ie = model.Kul_La_Ie,
				NaPa_Binayajit = model.NaPa_Binayajit,
				Others = model.Others,
				Jana_Sahabagita = model.Jana_Sahabagita,
				Peski = model.Peski,
				Technical_Amount = model.Technical_Amount,
				Kantigenci = model.Kantigenci,
				Remaining_Bhuktani_Amount = model.Remaining_Bhuktani_Amount,
				Agrim_Shulka = model.Agrim_Shulka,
				Bahal_Kar = model.Bahal_Kar,
				MarmatShmar = model.MarmatShmar,
				Katti_Rakam = model.Katti_Rakam,
				Aanya_Raaya = model.Aanya_Raaya,
				AdakshyaName = model.AdakshyaName,
				SamjhautaDate = model.SamjhautaDate,
				Farchot_Amount = model.Farchot_Amount,
				Running_Bhuktani = model.Running_Bhuktani,
				Samajik_Surekchya = model.Samajik_Surekchya,
				Parishramik = model.Parishramik,
				Dhuwani = model.Dhuwani,
				Royality = model.Royality,
				PlanningSamjhautaId = model.PlanningSamjhautaId,
				BhuktaniTypeId = model.BhuktaniTypeId,
				IsBhuktaniApproval = model.IsBhuktaniApproval,
				Status = model.Status,

			}).ToListAsync() ?? new List<PlanningBhuktaniViewModel>();
			return data;
		}

		public async Task<bool> InsertUpdatePlanningBhuktani(PlanningBhuktaniViewModel model)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					if (model.PlanningBhuktaniId > 0)
					{
						var data = await _context.PlanningBhuktani.Where(model => model.PlanningBhuktaniId == model.PlanningBhuktaniId).FirstOrDefaultAsync();
						if (data != null)
						{
							data.PlanningBhuktaniId = model.PlanningBhuktaniId;
							data.FiscalYearId = model.FiscalYearId;
							data.Nirman_Upabhokta = model.Nirman_Upabhokta;
							data.Aayojana_Karyakram = model.Aayojana_Karyakram;
							data.Kul_La_Ie = model.Kul_La_Ie;
							data.NaPa_Binayajit = model.NaPa_Binayajit;
							data.Others = model.Others;
							data.Jana_Sahabagita = model.Jana_Sahabagita;
							data.Peski = model.Peski;
							data.Technical_Amount = model.Technical_Amount;
							data.Kantigenci = model.Kantigenci;
							data.Remaining_Bhuktani_Amount = model.Remaining_Bhuktani_Amount;
							data.Agrim_Shulka = model.Agrim_Shulka;
							data.Bahal_Kar = model.Bahal_Kar;
							data.MarmatShmar = model.MarmatShmar;
							data.Katti_Rakam = model.Katti_Rakam;
							data.Aanya_Raaya = model.Aanya_Raaya;
							data.AdakshyaName = model.AdakshyaName;
							data.SamjhautaDate = model.SamjhautaDate;
							data.Farchot_Amount = model.Farchot_Amount;
							data.Running_Bhuktani = model.Running_Bhuktani;
							data.Samajik_Surekchya = model.Samajik_Surekchya;
							data.Parishramik = model.Parishramik;
							data.Dhuwani = model.Dhuwani;
							data.Royality = model.Royality;
							data.PlanningSamjhautaId = model.PlanningSamjhautaId;
							data.BhuktaniTypeId = model.BhuktaniTypeId;
							data.IsBhuktaniApproval = model.IsBhuktaniApproval;
							data.Status = true;

							_context.Entry(data).State = EntityState.Modified;
						}
						else
						{
							return false;
						}
					}
					else
					{
						var bhuktani = new PlanningBhuktani()
						{
							PlanningBhuktaniId = model.PlanningBhuktaniId,
							FiscalYearId = model.FiscalYearId,
							Nirman_Upabhokta = model.Nirman_Upabhokta,
							Aayojana_Karyakram = model.Aayojana_Karyakram,
							Kul_La_Ie = model.Kul_La_Ie,
							NaPa_Binayajit = model.NaPa_Binayajit,
							Others = model.Others,
							Jana_Sahabagita = model.Jana_Sahabagita,
							Peski = model.Peski,
							Technical_Amount = model.Technical_Amount,
							Kantigenci = model.Kantigenci,
							Remaining_Bhuktani_Amount = model.Remaining_Bhuktani_Amount,
							Agrim_Shulka = model.Agrim_Shulka,
							Bahal_Kar = model.Bahal_Kar,
							MarmatShmar = model.MarmatShmar,
							Katti_Rakam = model.Katti_Rakam,
							Aanya_Raaya = model.Aanya_Raaya,
							AdakshyaName = model.AdakshyaName,
							SamjhautaDate = model.SamjhautaDate,
							Farchot_Amount = model.Farchot_Amount,
							Running_Bhuktani = model.Running_Bhuktani,
							Samajik_Surekchya = model.Samajik_Surekchya,
							Parishramik = model.Parishramik,
							Dhuwani = model.Dhuwani,
							Royality = model.Royality,
							PlanningSamjhautaId = model.PlanningSamjhautaId,
							BhuktaniTypeId = model.BhuktaniTypeId,
							IsBhuktaniApproval = model.IsBhuktaniApproval,
							Status = true,

							CreatedDate = DateTime.Now,

						};
						await _context.PlanningBhuktani.AddAsync(bhuktani);
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
		#endregion

		#region Docs

		public async Task<List<PlanningDocumentUploadedViewModel>> GetPlanningDocumentById(int id)
		{
			var data = await _context.DocumentUploaded.Where(x => x.PlanningSamjhuataId == id).
				   Select(p => new PlanningDocumentUploadedViewModel()
				   {
					   DocumentTypeId = p.DocumentTypeId,
					   ImagePath = p.ImagePath,
					   PlanningSamjhuataId = p.PlanningSamjhuataId,
					   DocumentTypeName = _context.DocumentType.Where(x => x.DocumentTypeId == p.DocumentTypeId).Select(x => x.DocumentTypeName).FirstOrDefault(),
				   }).ToListAsync();
			return data ?? new List<PlanningDocumentUploadedViewModel>();
		}
		public async Task<int> InsertUpdatePlanningDocument(PlanningDocumentUploadedViewModel model)
		{
			var pId = 0;
			using (var transaction = _context.Database.BeginTransaction())
			{
				var planningsamjhautaid = model.PlanningSamjhuataId;
				var photoPath = "";
				try
				{
					if (model.DocumentsList.Count > 0)
					{
						foreach (var item in model.DocumentsList)
						{
							if (item.Id > 0)
							{
								var data = await _context.DocumentUploaded.FirstOrDefaultAsync(x => x.Id == item.Id) ?? new DocumentUploaded();

								if (data != null)
								{
									data.DocumentTypeId = item.DocumentTypeId;
									data.PlanningSamjhuataId = planningsamjhautaid;

									data.ImagePath = photoPath == "" ? data.ImagePath : photoPath;
									data.IsDeleted = false;
									data.ModifiedBy = _userId;
									data.ModifiedDate = DateTime.Now;
									_context.Entry(data).State = EntityState.Modified;
								}
								else
								{
									return 0;
								}
							}
							else
							{

								if (item.Image != null)
								{
									var other = await _utility.UploadImgReturnPathAndName("PlanningDocuments", item.Image, "Samjhauta-UploadedDocs");

									var document = new DocumentUploaded()
									{
										DocumentTypeId = item.DocumentTypeId,
										PlanningSamjhuataId = planningsamjhautaid,

										ImagePath = other.FilePath,
										IsDeleted = false,
										CreatedBy = _userId,
										CreatedDate = DateTime.Now,
									};
									await _context.DocumentUploaded.AddAsync(document);
								}

							}
						}
						await _context.SaveChangesAsync();
						await transaction.CommitAsync();
					}
					return 1;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return 0;
				}
			}
		}

		public async Task<bool> DeletePlanningDocumentId(int id)
		{
			try
			{
				if (id != 0)
				{
					var pics = _context.DocumentUploaded.Where(x => x.Id == id).FirstOrDefault();

					_context.Remove(pics);
					_context.SaveChanges();

					//await _utility.RemoveFileFormServer(pics.ImagePath);
				}
				else
					return false;
			}
			catch (Exception ex)
			{

			}

			return true;
		}
		public async Task<PlanningDocumentUploadedViewModel> GetDocsfromPlanningSamjhauta(int? id)
		{
			PlanningDocumentUploadedViewModel model = new PlanningDocumentUploadedViewModel();
			model.DocumentsList = await _context.DocumentUploaded.Where(x => x.PlanningSamjhuataId == id).Select(x => new PlanningDocumentUploadedViewModel()
			{
				Id = x.Id,
				PlanningSamjhuataId = x.PlanningSamjhuataId,
				DocumentTypeName = _context.DocumentType.Where(c => c.DocumentTypeId == x.DocumentTypeId).Select(c => c.DocumentTypeName).FirstOrDefault(),
				ImagePath = x.ImagePath,
			}).ToListAsync();
			return model;
			//        var data = await _context.DocumentUploaded
			//            .Where(x => x.PlanningSamjhuataId == id)
			//            .Select(x => new PlanningDocumentUploadedViewModel
			//            {
			//                PlanningSamjhuataId = x.PlanningSamjhuataId,
			//	ImagePath = x.ImagePath,
			//	DocumentTypeName = _context.DocumentType.Where(x => x.DocumentTypeId == x.DocumentTypeId).Select(x => x.DocumentTypeName).FirstOrDefault(),

			//}).FirstOrDefaultAsync() ?? new PlanningDocumentUploadedViewModel();

			//        return data;
		}
		public async Task <List<PlanningTaxViewModel>> GetPlanningKarKatti()
		{
			
			List<PlanningTaxViewModel> list = await _context.KarKatti.Select(x => new PlanningTaxViewModel()
			{
				Id = x.KarKattiId,
				MarmatSambar = x.MarmatSambhar,
				Kantigenci = x.Contigency,
			}).ToListAsync();
			return list;
		}

		#endregion

		#region PragatiPratibedan
		public async Task<PlanningSamjhautaViewModel> GetPlanningAllDataList(int id)
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
							  where p.PlanningSamjhautaId == id
							  //  join pr in _context.PaymentRecord on p.PlanningSamjhautaId equals pr.PlanningSamjhautaId into payment
							  //  from pr in payment.DefaultIfEmpty()
							  select new PlanningSamjhautaViewModel()
							  {
								  PlanningSamjhautaId = p.PlanningSamjhautaId,
								  FiscalYearId = p.FiscalYearId,
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
								  Samjhauta_Org_Name = _context.UpabhoktaSamitiDetail.Where(sn => sn.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(sn => sn.Name).FirstOrDefault(),
								  //WardName = _context.Ward.Where(x => x.Id == p.WardId).Select(x => x.Name).FirstOrDefault(),
								  //OrganisationRepresentative
								  Organization_Representative_Id = o.Organization_Representative_Id,

								  RepresentativeAddress = o.RepresentativeAddress,
								  RepresentativeNameId = o.RepresentativeNameId,
								  RepresentativePostId = o.RepresentativePostId,
								  RepresentativeName = _context.UpabhoktaSamitiMemberDetail.Where(sn => sn.UpabhoktaSamitiMemberDetailId == o.RepresentativeNameId).Select(sn => sn.MemberName).FirstOrDefault(),
								  RepresentativeDesignition = _context.SamitiPost.Where(sn => sn.Id == o.RepresentativePostId).Select(sn => sn.Name).FirstOrDefault(),


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
								  PlanningPravidikDetailId = pd.PlanningPravidikDetailId,
								  ChettraId = pd.ChettraId,
								  UpaChetraId = pd.UpaChetraId,
								  UpaChetraDetailId = pd.UpaChetraDetailId,
								  Detail = pd.Detail,
								  EsDetail = pd.EsDetail,
								  Kaifiyat = pd.Kaifiyat,
								  UnitId = pd.UnitId,
								  Pariman = pd.Pariman,

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
								  WardSifarishName = _context.Employee.Where(x => x.Id == pe.SwikritGarneId).Select(x => x.Name).FirstOrDefault(),
								  WardSifarishPost = _context.Pada.Where(x => x.Id == pe.SwikritGarnePadId).Select(x => x.Name).FirstOrDefault(),

								  //MunicipalitySamitiManjuriPatra
								  MunicipalitySamitiManjuriPatraId = ms.MunicipalitySamitiManjuriPatraId,

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
								  ChetraName = _context.Chettra.Where(y => y.ChettraId == pd.ChettraId).Select(q => q.ChettraName).FirstOrDefault(),
								  UpaChetraName = _context.UpaChetra.Where(y => y.UpaChettraId == pd.UpaChetraId).Select(q => q.UpaChettra).FirstOrDefault(),
								  UpaChetraDetailName = _context.UpaChetraDetail.Where(y => y.UpaChetraDetailId == pd.UpaChetraDetailId).Select(q => q.Name).FirstOrDefault(),
								  SartaName = _context.SartaSetup.Where(s => s.SartaSetupId == p.SartaSetupId).Select(s => s.Name).FirstOrDefault(),
								  SartaDiscription = _context.SartaSetup.Where(s => s.SartaSetupId == p.SartaSetupId).Select(s => s.Description).FirstOrDefault(),

							  }).FirstOrDefaultAsync() ?? new PlanningSamjhautaViewModel();
			data.PlanningSamjhautaKistaFirstDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "पहिलो").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
			{
				Payment_Records_Id = x.Payment_Records_Id,
				Kista_Kram = x.Kista_Rakam,

				Payment_Date = x.Payment_Date,
				Kista_Rakam = x.Kista_Rakam,
				Nirmarn_Samagri = x.Nirmarn_Samagri,
				Remarks = x.Remarks,
			}).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel();
			data.PlanningSamjhautaKistaSecondDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "दोश्राे").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
			{
				Payment_Records_Id = x.Payment_Records_Id,
				Kista_Kram = x.Kista_Rakam,
				Payment_Date = x.Payment_Date,
				Kista_Rakam = x.Kista_Rakam,
				Nirmarn_Samagri = x.Nirmarn_Samagri,
				Remarks = x.Remarks,
			}).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel(); ;
			data.PlanningSamjhautaKistaThirdDetailsList = _context.PaymentRecord.Where(x => x.PlanningSamjhautaId == id && x.Kista_Kram == "तेश्रो").Select(x => new PlanningSamjhautaPaymentRecordViewModel()
			{
				Payment_Records_Id = x.Payment_Records_Id,
				Kista_Kram = x.Kista_Rakam,
				Payment_Date = x.Payment_Date,
				Kista_Rakam = x.Kista_Rakam,
				Nirmarn_Samagri = x.Nirmarn_Samagri,
				Remarks = x.Remarks,
			}).FirstOrDefault() ?? new PlanningSamjhautaPaymentRecordViewModel(); ;
			data.samitiView = _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == data.SamitiDetailId).Select(x => new UpavoktaSamitiDetailViewModel()
			{
				UpabhoktaSamitiDetailId = x.UpabhoktaSamitiDetailId,
				//SamitiDate = x.SamitiDate,
				Samiti_Estd_Date = x.Samiti_Estd_Date,
				NepaliSamitiEstdDate = x.NepaliSamitiEstdDate,
				Name = x.Name,
				//ContactNo = x.ContactNo,
				//SahiDate = x.SahiDate,
				Beneficiaries_Attendance = x.Beneficiaries_Attendance,
				Beneficiaries_Absent = x.Beneficiaries_Absent,
				//AnugamanMember = x.AnugamanMember,
				NibedanMiti = x.NibedanMiti,
				Female_Present = x.Female_Present,
				Male_Present = x.Male_Present,
				Status = x.Status,
				BankName = x.BankName,
				AccountNumber = x.AccountNumber,
				samitiMemberDetaillist = _context.UpabhoktaSamitiMemberDetail.Where(y => y.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(z => new UpavoktaSamitiMemberDetailViewModel()
				{

					UpabhoktaSamitiMemberDetailId = z.UpabhoktaSamitiMemberDetailId,
					//PadaId = z.PadaId,
					SamitiPostId = z.SamitiPostId,
					PadaName = _context.SamitiPost.Where(q => q.Id == z.SamitiPostId).Select(q => q.Name).FirstOrDefault(),
					MemberName = z.MemberName,
					Address = z.Address,
					FatherName = z.FatherName,
					GrandFatherName = z.GrandFatherName,
					Age = z.Age,
					DOB = z.DOB,
					PhoneNo = z.PhoneNo,
					//ImagePath = z.ImagePath,
					Status = z.Status,
					Adakshya = _context.UpabhoktaSamitiMemberDetail.Where(q => q.SamitiPostId == 1).Select(q => q.MemberName).FirstOrDefault(),
					Kosadakshya = _context.SamitiPost.Where(q => q.Id == 2).Select(q => q.Name).FirstOrDefault(),
					Sachib = _context.SamitiPost.Where(q => q.Id == 3).Select(q => q.Name).FirstOrDefault(),

				}).ToList() ?? new List<UpavoktaSamitiMemberDetailViewModel>(),
			}).FirstOrDefault() ?? new UpavoktaSamitiDetailViewModel();

			return data ?? new PlanningSamjhautaViewModel();
		}
		#endregion


		#region NonSamjhauta
		public async Task<PlanningSamjhautaViewModel> GetNonSamjhauta()
		{
			PlanningSamjhautaViewModel model = new PlanningSamjhautaViewModel();		
			//var nonsamjhautadata = _context.PlanningSamjhauta.Select(x => x.YojanaId).ToList();

			//model.yojanalist = await _context.YojanaSetup.Where(x => nonsamjhautadata.All(y => y.Value != x.YojanaSetupId))
			//	.Select(z => new YojanaSetupViewModel()
			//	{
			//		YojanaSetupId = z.YojanaSetupId,
			//		YojanaName = z.YojanaName,
			//		EstimatedAmount = z.EstimatedAmount,
			//		WardName = _context.Ward.Where(x => x.Id == z.WardId).Select(x => x.Name).FirstOrDefault(),
			//	}).ToListAsync();
			return model;

		}

		#endregion


		#region BhautikPragati
		#region ProjkectComplete
		#endregion
		public async Task<PragatiBibaranViewModel> GetPragatiById(int id)
		{
			return await _context.PragatiBibaran.Where(x => x.PlanningSamjhautaId == id)
				.Select(x => new PragatiBibaranViewModel()
				{
					Id = x.Id,
					BittiyaPragati=x.BittiyaPragati,
					BhautikPragati=x.BittiyaPragati,
					Remarks=x.Remarks,
				}).FirstOrDefaultAsync() ?? new PragatiBibaranViewModel();
		}
		public async Task<bool> UpdateBhautikPragati(int? praId, decimal BittiyaPragati, decimal BhautikPragati, string Remarks)
		{		
			try
			{
				var data = await _context.PragatiBibaran.FirstOrDefaultAsync(x => x.PlanningSamjhautaId == praId);
				if (data != null)
				{
					data.BhautikPragati = BhautikPragati;
					data.BittiyaPragati = BittiyaPragati;
					data.Remarks = Remarks;
					_context.Entry(data).State = EntityState.Modified;
				}
				else
				{
					var newdata = new PragatiBibaran()
					{
						PlanningSamjhautaId = praId??0,
						BhautikPragati = BhautikPragati,
						BittiyaPragati = BittiyaPragati,
						Remarks = Remarks,
					};
					await _context.PragatiBibaran.AddAsync(newdata);
				}
				await _context.SaveChangesAsync();
				return true;

			}
			catch (Exception ex)
			{
				return false;
			}
		}
		#endregion
		
	}
}
