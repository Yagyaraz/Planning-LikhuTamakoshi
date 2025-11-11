using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using Mono.TextTemplating;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
//using PlanningCore.Migrations;
using PlanningCore.Utilities;
using SQLitePCL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PlanningCore.Areas.Contract.Repository
{
	public class ContractSamjhautaRepository : IContractSamjhauta
	{
		private readonly PlanningContext context;
		private readonly ILogger<ContractSamjhautaRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public ContractSamjhautaRepository(PlanningContext _context, IHttpContextAccessor httpContextAccessor, ILogger<ContractSamjhautaRepository> logger, IUtility utility)
		{
			context = _context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			utility = _utility;
		}
        #region Karkkatti

        public async Task<List<Con_KarKattiViewModel>> GetAllThekkaKarkatti()
        {
            return await context.Con_KarKatti.Where(x => x.Status == true)
             .Select(x => new Con_KarKattiViewModel()
             {
                 Con_katkattiId = x.Con_katkattiId,
                 Name = x.Name,
                 Value = x.Value,
                 Status = x.Status,

             }).ToListAsync() ?? new List<Con_KarKattiViewModel>();
        }
        public async Task<Con_KarKattiViewModel> GetContractKarkatttiById(int id)
        {
            var data = await context.Con_KarKatti.Where(x => x.Con_katkattiId == id)
			 .Select(x => new Con_KarKattiViewModel()
             {

                 Con_katkattiId = x.Con_katkattiId,
                 Name = x.Name,
                 Value = x.Value,
             }).FirstOrDefaultAsync() ?? new Con_KarKattiViewModel();
            return data;
        }
        public async Task<bool> CreateContractKarkatti(Con_KarKattiViewModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var detail = await context.Con_KarKatti.FirstOrDefaultAsync(x => x.Con_katkattiId == model.Con_katkattiId);
                    if (detail != null)
                    {
                        detail.Name = model.Name;
                        detail.Value = model.Value;
                        detail.Status = true;
                        context.Entry(detail).State = EntityState.Modified;
                    }
                    else
                    {
                        detail = await context.Con_KarKatti.FirstOrDefaultAsync(x => x.Name.Trim().Equals(model.Name));
                        if (detail != null)
                        {
							detail.Value = model.Value;
                            detail.Status = true;
                            context.Entry(detail).State = EntityState.Modified;
                        }
                        else
                        {
                            var kar = new Con_KarKatti()
                        {
                            Name = model.Name,
                            Value = model.Value,
                            Status = true,
                        };
                        await context.Con_KarKatti.AddAsync(kar);
                    }
                    }
                    await context.SaveChangesAsync();
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
        public async Task<bool> DeleteContractKarkatti(int id)
        {
            var data = await context.Con_KarKatti.Where(x => x.Con_katkattiId == id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Status = false;
                data.ModifiedBy = _userId;
                data.ModifiedDate = DateTime.Now;
                context.Entry(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        #endregion Karkatti
        #region contract
        public async Task<List<ContractSamjhautaViewModel>> GetAllContract()
		{
			var data = await (from s in context.Con_Samjhauta
							  join csd in context.Con_SamjhautaDetails on s.Id equals csd.Con_SamjhautaId into sdetails
							  from csd in sdetails.DefaultIfEmpty()
							  select new ContractSamjhautaViewModel()
							  {
								  Id = s.Id,
								  ThekkaTypeId = s.ThekkaTypeId,
								  ContractTypeId = s.ContractTypeId,
								  YojanaId = s.YojanaId,
								  ConsultantId = s.ConsultantId,
								  EstimatedAmount = s.EstimatedAmount,
								  ContractAmtEclVat = s.ContractAmtEclVat,
								  PsAmount = s.PsAmount,
								  VAT = s.VAT,
								  ThekkaNumber = s.ThekkaNumber,
								  BolPatraDate = s.BolPatraDate,
								  BolPatraDateEnglish = s.BolPatraDateEnglish,
								  ContractDate = s.ContractDate,
								  ContractDateEnglish = s.ContractDateEnglish,
								  ProjectEndDate = s.ProjectEndDate,
								  ProjectEndDateEng = s.ProjectEndDateEng,
								  ProjectStartDate = s.ProjectStartDate,
								  ProjectStartDateEng = s.ProjectStartDateEng,
								  YojanaName = s.YojanaSetup.YojanaName,
								  IFBNumber = s.IFBNumber,
								  StateId = csd.StateId,
								  DistrictId = csd.DistrictId,
								  PalikaId = csd.PalikaId,
								  Ward = csd.Ward,
								  EmployeerId = csd.EmployeerId,
								  ContractorId = csd.ContractorId,
								  SwikritId = csd.SwikritId,
								  SifarishId = csd.SifarishId,
								  SifarishPadId = csd.SifarishPadId,
								  SwikritPadId = csd.SwikritPadId,
								  SamjhautaPersonPost = csd.SamjhautaPersonPost,
								  ContractTypeName = s.Con_ContractType.Name,
								  ConsultantName = s.Con_Consultant.Name,
								  ConsultantAddress = s.Con_Consultant.MainPersonAddress,
								  SwikritPersonName = context.Employee.Where(x => x.Id == csd.SwikritId).Select(x => x.Name).FirstOrDefault(),
								  SifarishPersonName = context.Employee.Where(x => x.Id == csd.SifarishId).Select(x => x.Name).FirstOrDefault(),

							  }).ToListAsync();
			return data ?? new List<ContractSamjhautaViewModel>();
		}
		public async Task<ContractSamjhautaViewModel> GetContractById(int id = 0)
		{
			var data = await (from s in context.Con_Samjhauta
							  join csd in context.Con_SamjhautaDetails on s.Id equals csd.Con_SamjhautaId into sdetails
							  from csd in sdetails.DefaultIfEmpty()
							  where s.Id == id
							  select new ContractSamjhautaViewModel()
							  {
								  Id = s.Id,
								  ContractSamjhautaDetailsId = csd.Id,
								  ThekkaTypeId = s.ThekkaTypeId,
								  ContractTypeId = s.ContractTypeId,
								  YojanaId = s.YojanaId,

								  ConsultantId = s.ConsultantId,
								  EstimatedAmount = s.EstimatedAmount,
								  ContractAmtEclVat = s.ContractAmtEclVat,
								  PsAmount = s.PsAmount,
								  VAT = s.VAT,
								  ThekkaNumber = s.ThekkaNumber,
								  BolPatraDate = s.BolPatraDate,
								  BolPatraDateEnglish = s.BolPatraDateEnglish,
								  ContractDate = s.ContractDate,
								  ContractDateEnglish = s.ContractDateEnglish,
								  ProjectEndDate = s.ProjectEndDate,
								  ProjectEndDateEng = s.ProjectEndDateEng,
								  ProjectStartDate = s.ProjectStartDate,
								  ProjectStartDateEng = s.ProjectStartDateEng,
								  YojanaName = s.YojanaSetup.YojanaName,
								  IFBNumber = s.IFBNumber,
								  SamjhautaPerson = csd.SamjhautaPerson,
								  SamjhautaPersonPost = csd.SamjhautaPersonPost,
								  Rahobar = csd.Rahobar,
								  StateId = csd.StateId,
								  DistrictId = csd.DistrictId,
								  PalikaId = csd.PalikaId,
								  Ward = csd.Ward,
								  EmployeerId = csd.EmployeerId,
								  ContractorId = csd.ContractorId,
								  SwikritId = csd.SwikritId,
								  SifarishId = csd.SifarishId,
								  SifarishPadId = csd.SifarishPadId,
								  SwikritPadId = csd.SwikritPadId,
								  YojanaAddress = csd.YojanaAddress,
								  ConsultantName = context.Con_Consultant.Where(x => x.Id == s.ConsultantId).Select(x => x.Name).FirstOrDefault(),
								  SwikritPersonName = context.Employee.Where(x => x.Id == csd.SwikritId).Select(x => x.Name).FirstOrDefault(),
								  SifarishPersonName = context.Employee.Where(x => x.Id == csd.SifarishId).Select(x => x.Name).FirstOrDefault(),
								  SifarishPadName = context.Pada.Where(x => x.Id == csd.SifarishPadId).Select(x => x.Name).FirstOrDefault(),
								  SwikritPadName = context.Pada.Where(x => x.Id == csd.SwikritPadId).Select(x => x.Name).FirstOrDefault(),

								  WardIds = context.Con_Yojana_Ward.Where(z => z.YojanaId == s.YojanaId).Select(z => z.Id).ToList(),

							  }).FirstOrDefaultAsync();
			return data ?? new ContractSamjhautaViewModel();
		}

		public async Task<bool> InsertContractSamjhauta(ContractSamjhautaViewModel model)
		{
			using (var transaction = await context.Database.BeginTransactionAsync())
			{
				var fiscalYearId = await context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefaultAsync();
				try
				{
					if (model.Id > 0)
					{
						var contract = await context.Con_Samjhauta.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
						if (contract != null)
						{
							contract.Id = model.Id;
							contract.ThekkaTypeId = model.ThekkaTypeId;
							contract.ContractTypeId = model.ContractTypeId;
							contract.YojanaId = model.YojanaId;
							contract.ConsultantId = model.ConsultantId;
							contract.EstimatedAmount = model.EstimatedAmount;
							contract.ThekkaNumber = model.ThekkaNumber;
							contract.ContractAmtEclVat = model.ContractAmtEclVat;
							contract.PsAmount = model.PsAmount;
							contract.VAT = model.VAT;
							contract.ThekkaNumber = model.ThekkaNumber;
							contract.BolPatraDate = model.BolPatraDate;
							contract.BolPatraDateEnglish = model.BolPatraDateEnglish;
							contract.ContractDate = model.ContractDate;
							contract.ContractDateEnglish = model.ContractDateEnglish;
							contract.ProjectEndDate = model.ProjectEndDate;
							contract.ProjectEndDateEng = model.ProjectEndDateEng;
							contract.ProjectStartDate = model.ProjectStartDate;
							contract.ProjectStartDateEng = model.ProjectStartDateEng;
							contract.IFBNumber = model.IFBNumber;
							context.Entry(contract).State = EntityState.Modified;

						}
						var contractdetails = context.Con_SamjhautaDetails.Where(x => x.Id == model.ContractSamjhautaDetailsId).FirstOrDefault();
						if (contractdetails != null)
						{
							contractdetails.Id = model.ContractSamjhautaDetailsId;
							contractdetails.StateId = model.StateId;
							contractdetails.DistrictId = model.DistrictId;
							contractdetails.PalikaId = model.PalikaId;
							contractdetails.Ward = model.Ward;
							contractdetails.Con_SamjhautaId = model.Id;
							contractdetails.YojanaAddress = model.YojanaAddress;
							contractdetails.Rahobar = model.Rahobar;
							contractdetails.SamjhautaPerson = model.SamjhautaPerson;
							contractdetails.SamjhautaPersonPost = model.SamjhautaPersonPost;
							contractdetails.SifarishId = model.SifarishId;
							contractdetails.SifarishPadId = model.SifarishPadId;
							contractdetails.SwikritId = model.SwikritId;
							contractdetails.SwikritPadId = model.SwikritPadId;
							contractdetails.EmployeerId = model.EmployeerId;
							contractdetails.ContractorId = model.ContractorId;
							context.Entry(contractdetails).State = EntityState.Modified;
							await context.SaveChangesAsync();

						}
					}
					else
					{
						var data = new Con_Samjhauta()
						{
							Id = model.Id,
							FiscalYearId = fiscalYearId,
							ThekkaTypeId = model.ThekkaTypeId,
							ContractTypeId = model.ContractTypeId,
							YojanaId = model.YojanaId,
							ConsultantId = model.ConsultantId,
							EstimatedAmount = model.EstimatedAmount,
							ContractAmtEclVat = model.ContractAmtEclVat,
							PsAmount = model.PsAmount,
							VAT = model.VAT,
							ThekkaNumber = model.ThekkaNumber,
							BolPatraDate = model.BolPatraDate,
							BolPatraDateEnglish = model.BolPatraDateEnglish,
							ContractDate = model.ContractDate,
							ContractDateEnglish = model.ContractDateEnglish,
							ProjectEndDate = model.ProjectEndDate,
							ProjectEndDateEng = model.ProjectEndDateEng,
							ProjectStartDate = model.ProjectStartDate,
							ProjectStartDateEng = model.ProjectStartDateEng,
							IFBNumber = model.IFBNumber,
						};
						await context.Con_Samjhauta.AddAsync(data);
						context.SaveChanges();
						var samjhautadetails = new Con_SamjhautaDetails()
						{
							Id = model.Id,
							Con_SamjhautaId = data.Id,
							StateId = model.StateId,
							DistrictId = model.DistrictId,
							PalikaId = model.PalikaId,
							YojanaAddress = model.YojanaAddress,
							Ward = model.Ward,
							Rahobar = model.Rahobar,
							SamjhautaPerson = model.SamjhautaPerson,
							SamjhautaPersonPost = model.SamjhautaPersonPost,
							SifarishId = model.SifarishId,
							SifarishPadId = model.SifarishPadId,
							SwikritId = model.SwikritId,
							SwikritPadId = model.SwikritPadId,
							EmployeerId = model.EmployeerId,
							ContractorId = model.ContractorId,
						};
						await context.AddAsync(samjhautadetails);
					}
					await context.SaveChangesAsync();
					await transaction.CommitAsync();
					return true;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return false;
				}
			}
		}
		#endregion
		#region Insurance
		public async Task<List<InsuranceViewModel>> GetAllInsurance()
		{
			return await context.Con_Insurance.Where(x => x.Status == true)
				.Select(x => new InsuranceViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					Name = x.Name,
					InsuranceType = x.InsuranceType,
					InsuranceAmount = x.InsuranceAmount,
					PolicyNumber = x.PolicyNumber,
					ValidFrom = x.ValidFrom,
					ValidFromEng = x.ValidFromEng,
					ValidTo = x.ValidTo,
					ValidToEng = x.ValidToEng,
					Amount = x.Amount,
					CompanyName = x.CompanyName,
					IssueDate = x.IssueDate,
					IssueDateEng = x.IssueDateEng,
					Remarks = x.Remarks,
				}).ToListAsync();
		}
		public async Task<List<InsuranceViewModel>> GetAllInsuranceByContractId(int id)
		{
			return await context.Con_Insurance.Where(x => x.ConSamjhautaId == id && x.Status == true)
				.Select(x => new InsuranceViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					Name = x.Name,
					InsuranceType = x.InsuranceType,
					InsuranceAmount = x.InsuranceAmount,
					PolicyNumber = x.PolicyNumber,
					ValidFrom = x.ValidFrom,
					ValidFromEng = x.ValidFromEng,
					ValidTo = x.ValidTo,
					ValidToEng = x.ValidToEng,
					Amount = x.Amount,
					CompanyName = x.CompanyName,
					IssueDate = x.IssueDate,
					IssueDateEng = x.IssueDateEng,
					Remarks = x.Remarks,
				}).ToListAsync();
		}
		public async Task<InsuranceViewModel> GetInsuranceById(int id)
		{
			return await context.Con_Insurance.Where(x => x.Id == id)
				.Select(x => new InsuranceViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					Name = x.Name,
					InsuranceType = x.InsuranceType,
					InsuranceAmount = x.InsuranceAmount,
					PolicyNumber = x.PolicyNumber,
					ValidFrom = x.ValidFrom,
					ValidTo = x.ValidTo,
					Amount = x.Amount,
					CompanyName = x.CompanyName,
					IssueDate = x.IssueDate,
					Remarks = x.Remarks,

				}).FirstOrDefaultAsync() ?? new InsuranceViewModel();
		}
		public async Task<bool> CreateInsurance(InsuranceViewModel model)
		{
			try
			{
				if (model.Id > 0)
				{
					var insurance = await context.Con_Insurance.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
					if (insurance != null)
					{

						insurance.Id = model.Id;
						insurance.ConSamjhautaId = model.ConSamjhautaId;
						insurance.Name = model.Name;
						insurance.InsuranceType = model.InsuranceType;
						insurance.InsuranceAmount = model.InsuranceAmount;
						insurance.PolicyNumber = model.PolicyNumber;
						insurance.ValidFrom = model.ValidFrom;
						insurance.ValidFromEng = model.ValidFromEng;
						insurance.ValidTo = model.ValidTo;
						insurance.ValidToEng = model.ValidToEng;
						insurance.Amount = model.Amount;
						insurance.CompanyName = model.CompanyName;
						insurance.IssueDate = model.IssueDate;
						insurance.IssueDateEng = model.IssueDateEng;
						insurance.Remarks = model.Remarks;
						insurance.Status = model.Status;
						context.Entry(insurance).State = EntityState.Modified;
					}
				}
				else
				{
					var insuranceadd = new Con_Insurance()
					{

						Id = model.Id,
						ConSamjhautaId = model.ConSamjhautaId,
						Name = model.Name,
						InsuranceType = model.InsuranceType,
						InsuranceAmount = model.InsuranceAmount,
						PolicyNumber = model.PolicyNumber,
						ValidFrom = model.ValidFrom,
						ValidFromEng = model.ValidFromEng,
						ValidToEng = model.ValidToEng,
						ValidTo = model.ValidTo,
						Amount = model.Amount,
						CompanyName = model.CompanyName,
						IssueDate = model.IssueDate,
						IssueDateEng = model.IssueDateEng,
						Remarks = model.Remarks,
						Status = true,
					};
					await context.Con_Insurance.AddAsync(insuranceadd);
					await context.SaveChangesAsync();
				}
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				//_logger.LogInformation("FiscalYear Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
				return false;
			}
		}
		#endregion
		#region Variation
		public async Task<List<VariationViewModel>> GetAllVariation()
		{
			return await context.Con_Variation
				.Select(x => new VariationViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					VariationNumber = x.VariationNumber,
					Amount = x.Amount,
					ContractAmt = x.ContractAmt,
					VoDate = x.VoDate,
					Remarks = x.Remarks,
				}).ToListAsync();
		}
		public async Task<List<VariationViewModel>> GetAllVariationByContractId(int id)
		{
			return await context.Con_Variation.Where(x => x.ConSamjhautaId == id & x.Status == true)
				.Select(x => new VariationViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					VariationTypeId = x.VariationTypeId,
					VariationNumber = context.Con_VariationType.Where(z => z.Id == x.VariationTypeId).Select(z => z.Name).FirstOrDefault(),
					Amount = x.Amount,
					ContractAmt = x.ContractAmt,
					VoDate = x.VoDate,
					VoDateEng = x.VoDateEng,
					SamjhautaEndDate = x.SamjhautaEndDate,
					SamjhautaEndDateEng = x.SamjhautaEndDateEng,
					Remarks = x.Remarks,

				}).ToListAsync();
		}
		public async Task<VariationViewModel> GetVariationById(int id)
		{
			return await context.Con_Variation.Where(x => x.Id == id)
				.Select(x => new VariationViewModel()
				{
					Id = x.Id,
					ConSamjhautaId = x.ConSamjhautaId,
					VariationNumber = x.VariationNumber,
					Amount = x.Amount,
					ContractAmt = x.ContractAmt,
					VoDate = x.VoDate,
					Remarks = x.Remarks,
				}).FirstOrDefaultAsync() ?? new VariationViewModel();

		}

		public async Task<bool> CreateVariation(VariationViewModel model)
		{
			try
			{
				if (model.Id > 0)
				{
					var vari = await context.Con_Variation.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
					if (vari != null)
					{

						vari.Id = model.Id;

						vari.ConSamjhautaId = model.ConSamjhautaId;
						vari.VariationNumber = model.VariationNumber;
						vari.Amount = model.Amount;
						vari.ContractAmt = model.ContractAmt;
						vari.VoDate = model.VoDate;
						vari.SamjhautaEndDate = model.SamjhautaEndDate;
						vari.SamjhautaEndDateEng = model.SamjhautaEndDateEng;
						vari.VoDateEng = model.VoDateEng;
						vari.Remarks = model.Remarks;
						vari.VariationTypeId = model.VariationTypeId;
						vari.Status = true;

						context.Entry(vari).State = EntityState.Modified;
					}
				}
				else
				{
					var variation = new Con_Variation()
					{

						Id = model.Id,
						ConSamjhautaId = model.ConSamjhautaId,
						Amount = model.Amount,
						ContractAmt = model.ContractAmt,
						VoDate = model.VoDate,
						SamjhautaEndDate = model.SamjhautaEndDate,
						SamjhautaEndDateEng = model.SamjhautaEndDateEng,
						VoDateEng = model.VoDateEng,
						Remarks = model.Remarks,
						Status = true,
						VariationTypeId = model.VariationTypeId,
					};
					await context.Con_Variation.AddAsync(variation);
					await context.SaveChangesAsync();
				}
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				//_logger.LogInformation("FiscalYear Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
				return false;
			}
		}

		public async Task<bool> DeleteInsurance(int id)
		{
			var data = await context.Con_Insurance.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.Status = false;
				context.Entry(data).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		public async Task<bool> DeleteVariation(int id)
		{
			var data = await context.Con_Variation.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.Status = false;
				context.Entry(data).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		#endregion
		#region Report
		public async Task<ContractSamjhautaViewModel> GetContractReportDataByYojanaId(int id = 0)
		{
			try
			{
				var data = await (from s in context.Con_Samjhauta
								  join csd in context.Con_SamjhautaDetails on s.Id equals csd.Con_SamjhautaId into sdetails
								  from csd in sdetails.DefaultIfEmpty()
								  join b in context.Con_Bhuktani on s.Id equals b.ContractSamjhautaId into bhuktani
								  from b in bhuktani.DefaultIfEmpty()
								  where s.YojanaId == id
								  select new ContractSamjhautaViewModel()
								  {
									  Id = s.Id,
									  ContractSamjhautaDetailsId = csd.Id,
									  ThekkaTypeId = s.ThekkaTypeId,
									  ContractTypeId = s.ContractTypeId,
									  YojanaId = s.YojanaId,
									  ConsultantId = s.ConsultantId,
									  MainPersonAddress = s.Con_Consultant.MainPersonAddress,
									  EstimatedAmount = s.EstimatedAmount,
									  ContractAmtEclVat = s.ContractAmtEclVat,
									  PsAmount = s.PsAmount,
									  VAT = s.VAT,
									  ThekkaNumber = s.ThekkaNumber,
									  BolPatraDate = s.BolPatraDate,
									  BolPatraDateEnglish = s.BolPatraDateEnglish,
									  ContractDate = s.ContractDate,
									  ContractDateEnglish = s.ContractDateEnglish,
									  ProjectEndDate = s.ProjectEndDate,
									  ProjectEndDateEng = s.ProjectEndDateEng,
									  ProjectStartDate = s.ProjectStartDate,
									  ProjectStartDateEng = s.ProjectStartDateEng,
									  YojanaName = s.YojanaSetup.YojanaName,
									  IFBNumber = s.IFBNumber,
									  SamjhautaPerson = csd.SamjhautaPerson,
									  SamjhautaPersonPost = csd.SamjhautaPersonPost,
									  Rahobar = csd.Rahobar,
									  StateId = csd.StateId,
									  DistrictId = csd.DistrictId,
									  PalikaId = csd.PalikaId,
									  Ward = csd.Ward,
									  EmployeerId = csd.EmployeerId,
									  ContractorId = csd.ContractorId,
									  SwikritId = csd.SwikritId,
									  SifarishId = csd.SifarishId,
									  SifarishPadId = csd.SifarishPadId,
									  SwikritPadId = csd.SwikritPadId,
									  YojanaAddress = csd.YojanaAddress,

									  //Bhuktani
									  ContractAmt = b == null ? 0 : b.ContractAmt,
									  BhuktaniAmount = b == null ? 0 : b.BhuktaniAmount,
									  BhuktaniDate = b == null ? null : b.BhuktaniDate,
									  PrabidhikDetailsAmount = b == null ? 0 : b.PrabidhikDetailsAmount,
									  RunningBill = b == null ? 0 : b.RunningBill,
									  PrabidhikDetailsMiti = b == null ? null : b.PrabidhikDetailsMiti,
									  PaskiAmount = b == null ? 0 : b.PaskiAmount,
									  BhuktaniPsAmount = b == null ? 0 : b.PsAmount,
									  BhuktaniVAT = b == null ? 0 : b.VAT,
									  BillNumber = b == null ? null : b.BillNumber,
									  DharutiAmount = b == null ? 0 : b.DharutiAmount,
									  GoshwaraVoucherNo = b == null ? null : b.GoshwaraVoucherNo,
									  KaryDiyekoAmount = b == null ? 0 : b.KaryDiyekoAmount,

									  ConsultantName = s.Con_Consultant.Name,
									  ConsultantAddress = s.Con_Consultant.MainPersonAddress,
									  SwikritPersonName = context.Employee.Where(x => x.Id == csd.SwikritId).Select(x => x.Name).FirstOrDefault(),
									  SifarishPersonName = context.Employee.Where(x => x.Id == csd.SifarishId).Select(x => x.Name).FirstOrDefault(),
									  SifarishPadName = context.Pada.Where(x => x.Id == csd.SifarishPadId).Select(x => x.Name).FirstOrDefault(),
									  SwikritPadName = context.Pada.Where(x => x.Id == csd.SwikritPadId).Select(x => x.Name).FirstOrDefault(),
									  CEOName = context.Employee.Where(x => x.PadaId == 1).Select(x => x.Name).FirstOrDefault(),

									  WardIds = context.Con_Yojana_Ward.Where(z => z.YojanaId == s.YojanaId).Select(z => z.Id).ToList(),
								  }).FirstOrDefaultAsync();
				return data ?? new ContractSamjhautaViewModel();
			}
			catch (Exception ex)
			{
				return new ContractSamjhautaViewModel();
			}
		}
		public async Task<ContractSamjhautaViewModel> CheckAndGetContractReportData(int yojanaId, int reportTypeId)
		{
			var data = await context.Con_PrintReportDetail.Where(x => x.YojanaId == yojanaId && x.ReportTypeId == reportTypeId).Select(x => new ContractSamjhautaViewModel
			{
				PrintContent = x.PrintContent,
			}).FirstOrDefaultAsync() ?? new ContractSamjhautaViewModel();
			return data;
		}
		#endregion
		#region Bhuktani

		public async Task<Con_BhuktaniViewModel> GetBhuktaniListByContractSamjhautaId(int id)
		{
			var model = new Con_BhuktaniViewModel();
			model.ContractBhuktaniList = await context.Con_Bhuktani.Where(model => model.ContractSamjhautaId == id)
				.Select(model => new Con_BhuktaniViewModel()
				{
					ContractSamjhautaId = model.ContractSamjhautaId,
					BhuktaniTypeId = model.BhuktaniTypeId,
					ContractAmt = model.ContractAmt,
					BhuktaniAmount = model.BhuktaniAmount,
					BhuktaniDate = model.BhuktaniDate,
					PrabidhikDetailsAmount = model.PrabidhikDetailsAmount,
					RunningBill = model.RunningBill,
					PsAmount = model.PsAmount,
					VAT = model.VAT,
					PrabidhikDetailsMiti = model.PrabidhikDetailsMiti,
					PaskiAmount = model.PaskiAmount,
					KaryDiyekoAmount = model.KaryDiyekoAmount,
					YojanaMulykanDate = model.YojanaMulykanDate,
					DharutiAmount = model.DharutiAmount,
					GoshwaraVoucherNo = model.GoshwaraVoucherNo,
					BillNumber = model.BillNumber,

				}).ToListAsync() ?? new List<Con_BhuktaniViewModel>();
			return model;
		}
		public Task<List<Con_BhuktaniViewModel>> GetConBhuktaniListFormContractSamjhauta(int? id)
		{
			List<Con_BhuktaniViewModel> data = (from s in context.Con_Samjhauta
												join sd in context.Con_SamjhautaDetails on s.Id equals sd.Con_SamjhautaId into prodetail
												from sd in prodetail.DefaultIfEmpty()
													//join pe in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into pro_contextry
													//from pe in pro_contextry.DefaultIfEmpty()
													//join pd in _context.UpabhoktaSamitiDetail on p.YojanaId equals pd.YojanaId into samiti
													//from pd in samiti.DefaultIfEmpty()
													//join mp in _context.MunicipalitySamitiManjuriPatra on p.PlanningSamjhautaId equals mp.PlanningSamjhautaId into manjuri
													//from mp in manjuri.DefaultIfEmpty()
												select new Con_BhuktaniViewModel
												{
													ContractSamjhautaId = s.Id,

												}).ToList();
			//return data?? new List<PlanningBhuktaniViewModel>();
			return Task.FromResult(data);
		}
		public async Task<Con_BhuktaniViewModel> GetContractBhuktaniByBhuktaniId(int? id)
		{
			var data = await context.Con_Bhuktani.Where(model => model.Id == id).Select(model => new Con_BhuktaniViewModel()
			{
				ContractSamjhautaId = model.ContractSamjhautaId,
				BhuktaniTypeId = model.BhuktaniTypeId,
				ContractAmt = model.ContractAmt,
				BhuktaniAmount = model.BhuktaniAmount,
				BhuktaniDate = model.BhuktaniDate,
				PrabidhikDetailsAmount = model.PrabidhikDetailsAmount,
				RunningBill = model.RunningBill,
				PsAmount = model.PsAmount,
				VAT = model.VAT,
				PrabidhikDetailsMiti = model.PrabidhikDetailsMiti,
				PaskiAmount = model.PaskiAmount,
				KaryDiyekoAmount = model.KaryDiyekoAmount,
				YojanaMulykanDate = model.YojanaMulykanDate,
				DharutiAmount = model.DharutiAmount,
				GoshwaraVoucherNo = model.GoshwaraVoucherNo,
				BillNumber = model.BillNumber,
			}).FirstOrDefaultAsync() ?? new Con_BhuktaniViewModel();
			return data ?? new Con_BhuktaniViewModel();
		}
		public async Task<int> InsertUpdateContractBhuktani(Con_BhuktaniViewModel model)
		{
			using (var transaction = await context.Database.BeginTransactionAsync())
			{
				// var fiscalYearId = _context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefault();
				var ConSamjhautaId = 0;
				try
				{
					if (model.ConBhuktaniId > 0)
					{
						var data = await context.Con_Bhuktani.Where(model => model.Id == model.Id).FirstOrDefaultAsync();
						if (data != null)
						{
							data.ContractSamjhautaId = model.ContractSamjhautaId;
							data.BhuktaniTypeId = model.BhuktaniTypeId;
							data.ContractAmt = model.ContractAmt;
							data.BhuktaniAmount = model.BhuktaniAmount;
							data.BhuktaniDate = model.BhuktaniDate;
							data.PrabidhikDetailsAmount = model.PrabidhikDetailsAmount;
							data.RunningBill = model.RunningBill;
							data.PsAmount = model.PsAmount;
							data.VAT = model.VAT;
							data.PrabidhikDetailsMiti = model.PrabidhikDetailsMiti;
							data.PaskiAmount = model.PaskiAmount;
							data.KaryDiyekoAmount = model.KaryDiyekoAmount;
							data.YojanaMulykanDate = model.YojanaMulykanDate;
							data.DharutiAmount = model.DharutiAmount;
							data.GoshwaraVoucherNo = model.GoshwaraVoucherNo;
							data.BillNumber = model.BillNumber;
							data.ModifiedDate = DateTime.Now;
							data.ModifiedBy = _userId;

							context.Entry(data).State = EntityState.Modified;
						}
						if (model.ConTaxList.Count > 0)
						{

							foreach (var item in await context.Con_TaxDeduction.Where(x => x.ContractBhuktaniId == model.ConBhuktaniId).ToListAsync())
							{
								if (!model.ConTaxList.Any(x => x.TaxDeductionId == item.Id))
								{
									context.Con_TaxDeduction.Remove(item);
									await context.SaveChangesAsync();
								}
							}
							foreach (var item in model.ConTaxList)
							{
								var taxdata = await context.Con_TaxDeduction.Where(x => x.Id == item.TaxDeductionId).FirstOrDefaultAsync();
								if (taxdata != null)
								{
									taxdata.ContractBhuktaniId = data.Id;
									taxdata.ConKarkattiId = item.ConKarkattiId;
									taxdata.Amount = item.Amount;
									context.Entry(taxdata).State = EntityState.Modified;
									await context.SaveChangesAsync();
								}
								else
								{
									var taxdeduct = new Con_TaxDeduction()
									{
										ContractBhuktaniId = data.Id,
										ConKarkattiId = item.ConKarkattiId,
										Amount = item.Amount,
									};
									await context.Con_TaxDeduction.AddAsync(taxdata);
									context.SaveChanges();
								}
							}
						}


						else
						{
							return 0;
						}
					}
					else
					{
						var bhuktani = new Con_Bhuktani()
						{
							ContractSamjhautaId = model.ContractSamjhautaId,
							Id = model.ConBhuktaniId,
							BhuktaniTypeId = model.BhuktaniTypeId,
							ContractAmt = model.ContractAmt,
							BhuktaniAmount = model.BhuktaniAmount,
							BhuktaniDate = model.BhuktaniDate,
							PrabidhikDetailsAmount = model.PrabidhikDetailsAmount,
							RunningBill = model.RunningBill,
							PsAmount = model.PsAmount,
							VAT = model.VAT,
							PrabidhikDetailsMiti = model.PrabidhikDetailsMiti,
							PaskiAmount = model.PaskiAmount,
							KaryDiyekoAmount = model.KaryDiyekoAmount,
							YojanaMulykanDate = model.YojanaMulykanDate,
							DharutiAmount = model.DharutiAmount,
							GoshwaraVoucherNo = model.GoshwaraVoucherNo,
							BillNumber = model.BillNumber,
							CreatedBy = _userId,
						};
						await context.Con_Bhuktani.AddAsync(bhuktani);
						context.SaveChanges();
						if (model.ConTaxList.Count > 0)
						{
							foreach (var item in model.ConTaxList)
							{
								var taxdata = new Con_TaxDeduction()
								{
									ContractBhuktaniId = bhuktani.Id,
									ConKarkattiId = item.ConKarkattiId,
									Amount = item.Amount,
								};
								await context.Con_TaxDeduction.AddAsync(taxdata);
								context.SaveChanges();
							}
						}
						ConSamjhautaId = bhuktani.ContractSamjhautaId;
					}

					await context.SaveChangesAsync();
					await transaction.CommitAsync();
					return ConSamjhautaId;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return 0;
				}
			}
		}
		#endregion

	}
}





