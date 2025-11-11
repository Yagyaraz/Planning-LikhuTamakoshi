using System.Security.Claims;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Contract.Repository
{
	public class BankGuaranteeRepository:IBankGuarantee
	{
		private readonly PlanningContext context;
		private readonly ILogger<BankGuaranteeRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public BankGuaranteeRepository(PlanningContext _context, IHttpContextAccessor httpContextAccessor, ILogger<BankGuaranteeRepository> logger, IUtility utility)
		{
			context = _context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			utility = _utility;
		}
		public async Task<List<BankGuaranteeViewModel>> GetBankGuarantee()
		{
			var data = await context.Con_BankGuarantee
			    .Where(b => b.MyadThapTypeId == null)
				.Select(x => new BankGuaranteeViewModel()
			{
				Id = x.Id,
				BankGuaranteeName = x.BankGuaranteeName,
				YojanaId = x.YojanaId,
				ConsultantId = x.ConsultantId,
				BankId=x.BankId,
				BGType = x.BGType,
				BGNumber = x.BGNumber,
				Currency = x.Currency,
				Amount = x.Amount,
				JariMiti= x.JariMiti,
				JariMitiEng = x.JariMitiEng,
				FromDate= x.FromDate,
				FromDateEng= x.FromDateEng,
				ToDate= x.ToDate,
				ToDateEng= x.ToDateEng,
				YojanaName=x.YojanaSetup.YojanaName,

			}).ToListAsync();
			return data;
		}
		public async Task<BankGuaranteeViewModel> GetBankGuaranteeById(int id)
		{
			var data = await context.Con_BankGuarantee.Where(x=>x.Id==id).Select(x => new BankGuaranteeViewModel()
			{
				Id = x.Id,
				BankGuaranteeName = x.BankGuaranteeName,
				YojanaId = x.YojanaId,
				BankId = x.BankId,
				ConsultantId = x.ConsultantId,
				BGType = x.BGType,
				BGNumber = x.BGNumber,
				Currency = x.Currency,
				Amount = x.Amount,
				JariMiti = x.JariMiti,
				JariMitiEng = x.JariMitiEng,
				FromDate = x.FromDate,
				FromDateEng = x.FromDateEng,
				ToDate = x.ToDate,
				ToDateEng = x.ToDateEng,
				YojanaName=x.YojanaSetup.YojanaName,
				FukuwaStatus = x.FukuwaStatus,
				FukuwaTypeId = x.FukuwaTypeId,
				FukwaDate = x.FukwaDate,
				FukwaDateEng = x.FukwaDateEng,
				FukwaAmount = x.FukwaAmount,
				Remarks = x.Remarks,
				EmailDays = x.EmailDays,
				PaymentType = x.PaymentType,
				JammaDate = x.JammaDate,
				JammaDateEng = x.JammaDateEng,
				BankAccount = x.BankAccount,
				PaskiDate = x.PaskiDate,
				PaskiDateEng = x.PaskiDateEng,
				PaskiAmount = x.PaskiAmount,
				PaskiRemaining = x.PaskiRemaining,
				NirnayDate = x.NirnayDate,
				NirnayDateEng = x.NirnayDateEng,
				NirnayAdikari = x.NirnayAdikari,
				MyadThapTypeId = x.MyadThapTypeId,
				MyadThapStartDate = x.MyadThapStartDate,
				MyadThapStartDateEng = x.MyadThapStartDateEng,
				MyadThapEndDate = x.MyadThapEndDate,
				MyadThapEndDateEng = x.MyadThapEndDateEng,
			}).FirstOrDefaultAsync();
			if (data != null)
			{
				data.BankGuaranteeList = context.Con_BankGuarantee
					.Where(b => b.YojanaId == data.YojanaId && b.ConsultantId == data.ConsultantId && b.MyadThapTypeId != null)
					.Select(b => new BankGuaranteeViewModel()
					{
						Id = b.Id,
						BankGuaranteeName = b.BankGuaranteeName,
						YojanaId = b.YojanaId,
						BankId = b.BankId,
						ConsultantId = b.ConsultantId,
						BGType = b.BGType,
						BGNumber = b.BGNumber,
						Currency = b.Currency,
						Amount = b.Amount,
						JariMiti = b.JariMiti,
						JariMitiEng = b.JariMitiEng,
						FromDate = b.FromDate,
						FromDateEng = b.FromDateEng,
						ToDate = b.ToDate,
						ToDateEng = b.ToDateEng,
						YojanaName = b.YojanaSetup.YojanaName,
						FukuwaStatus = b.FukuwaStatus,
						FukuwaTypeId = b.FukuwaTypeId,
						FukwaDate = b.FukwaDate,
						FukwaDateEng = b.FukwaDateEng,
						FukwaAmount = b.FukwaAmount,
						Remarks = b.Remarks,
						EmailDays = b.EmailDays,
						PaymentType = b.PaymentType,
						JammaDate = b.JammaDate,
						JammaDateEng = b.JammaDateEng,
						BankAccount = b.BankAccount,
						PaskiDate = b.PaskiDate,
						PaskiDateEng = b.PaskiDateEng,
						PaskiAmount = b.PaskiAmount,
						PaskiRemaining = b.PaskiRemaining,
						NirnayDate = b.NirnayDate,
						NirnayDateEng = b.NirnayDateEng,
						NirnayAdikari = b.NirnayAdikari,
						MyadThapTypeId = b.MyadThapTypeId,
						MyadThapStartDate = b.MyadThapStartDate,
						MyadThapStartDateEng = b.MyadThapStartDateEng,
						MyadThapEndDate = b.MyadThapEndDate,
						MyadThapEndDateEng = b.MyadThapEndDateEng,
					}).ToList() ?? new List<BankGuaranteeViewModel>();
			}
			
			return  data ?? new BankGuaranteeViewModel();
		}

		public async Task<bool> InsertUpdateBankGuarantee(BankGuaranteeViewModel model)
		{
			try
			{
				if (model.Id > 0)
				{
					var bgdata = context.Con_BankGuarantee.Where(x => x.Id == model.Id).FirstOrDefault();
					if (bgdata != null)
					{
							bgdata.Id = model.Id;
							bgdata.BankGuaranteeName = model.BankGuaranteeName;
							bgdata.YojanaId = model.YojanaId;
							bgdata.ConsultantId = model.ConsultantId;
							bgdata.BankId = model.BankId;
							bgdata.BGType = model.BGType;
							bgdata.BGNumber = model.BGNumber;
							bgdata.Currency = model.Currency;
							bgdata.Amount = model.Amount;
							bgdata.JariMiti = model.JariMiti;
							bgdata.JariMitiEng = model.JariMitiEng;
							bgdata.FromDate = model.FromDate;
							bgdata.FromDateEng = model.FromDateEng;
							bgdata.ToDate = model.ToDate;
							bgdata.ToDateEng = model.ToDateEng;
							bgdata.FukuwaStatus = model.FukuwaStatus;
							bgdata.FukuwaTypeId = model.FukuwaTypeId;
							bgdata.FukwaDate = model.FukwaDate;
							bgdata.FukwaDateEng = model.FukwaDateEng;
							bgdata.FukwaAmount = model.FukwaAmount;
							bgdata.Remarks = model.Remarks;
							bgdata.EmailDays = model.EmailDays;
							bgdata.PaymentType = model.PaymentType;
							bgdata.JammaDate = model.JammaDate;
							bgdata.JammaDateEng = model.JammaDateEng;
							bgdata.BankAccount = model.BankAccount;
							bgdata.PaskiDate = model.PaskiDate;
							bgdata.PaskiDateEng = model.PaskiDateEng;
							bgdata.PaskiAmount = model.PaskiAmount;
							bgdata.PaskiRemaining = model.PaskiRemaining;
							bgdata.NirnayDate = model.NirnayDate;
							bgdata.NirnayDateEng = model.NirnayDateEng;
							bgdata.NirnayAdikari = model.NirnayAdikari;
							bgdata.MyadThapTypeId = model.MyadThapTypeId;
							bgdata.MyadThapStartDate = model.MyadThapStartDate;
							bgdata.MyadThapStartDateEng = model.MyadThapStartDateEng;
							bgdata.MyadThapEndDate = model.MyadThapEndDate;
							bgdata.MyadThapEndDateEng = model.MyadThapEndDateEng;
						    context.Entry(bgdata).State = EntityState.Modified;
						   await context.SaveChangesAsync();
					}

					if (model.BankGuaranteeList.Count > 0)
					{
						
						foreach (var item in model.BankGuaranteeList)
						{
							var partial = context.Con_BankGuarantee.Where(x => x.Id == item.Id).FirstOrDefault();
							if(partial != null)
							{
								partial.Id = item.Id;
								partial.BankGuaranteeName = item.BankGuaranteeName;
								partial.YojanaId = model.YojanaId;
								partial.ConsultantId = model.ConsultantId;
								partial.BankId = item.BankId;
								partial.BGType = item.BGType;
								partial.BGNumber = item.BGNumber;
								partial.Currency = item.Currency;
								partial.Amount = item.Amount;
								partial.JariMiti = item.JariMiti;
								partial.JariMitiEng = item.JariMitiEng;
								partial.FromDate = item.FromDate;
								partial.FromDateEng = item.FromDateEng;
								partial.ToDate = item.ToDate;
								partial.ToDateEng = item.ToDateEng;
								partial.FukuwaStatus = item.FukuwaStatus;
								partial.FukuwaTypeId = item.FukuwaTypeId;
								partial.FukwaDate = item.FukwaDate;
								partial.FukwaDateEng = item.FukwaDateEng;
								partial.FukwaAmount = item.FukwaAmount;
								partial.Remarks = item.Remarks;
								partial.EmailDays = item.EmailDays;
								partial.PaymentType = item.PaymentType;
								partial.JammaDate = item.JammaDate;
								partial.JammaDateEng = item.JammaDateEng;
								partial.BankAccount = item.BankAccount;
								partial.PaskiDate = item.PaskiDate;
								partial.PaskiDateEng = item.PaskiDateEng;
								partial.PaskiAmount = item.PaskiAmount;
								partial.PaskiRemaining = item.PaskiRemaining;
								partial.NirnayDate = item.NirnayDate;
								partial.NirnayDateEng = item.NirnayDateEng;
								partial.NirnayAdikari = item.NirnayAdikari;
								partial.MyadThapTypeId = item.MyadThapTypeId;
								partial.MyadThapStartDate = item.MyadThapStartDate;
								partial.MyadThapStartDateEng = item.MyadThapStartDateEng;
								partial.MyadThapEndDate = item.MyadThapEndDate;
								partial.MyadThapEndDateEng = item.MyadThapEndDateEng;
							
							}

							context.Entry(partial).State = EntityState.Modified;
						    context.SaveChanges();

						}
					}
				}
				else
				{
				   	var data = new Con_BankGuarantee()
					{
						Id = model.Id,
						BankGuaranteeName = model.BankGuaranteeName,
						YojanaId = model.YojanaId,
						ConsultantId = model.ConsultantId,
						BankId = model.BankId,
						BGType = model.BGType,
						BGNumber = model.BGNumber,
						Currency = model.Currency,
						Amount = model.Amount,
						JariMiti = model.JariMiti,
						JariMitiEng = model.JariMitiEng ,
						FromDate = model.FromDate,
						FromDateEng = model.FromDateEng,
						ToDate = model.ToDate,
						ToDateEng = model.ToDateEng,
					    FukuwaStatus = model.FukuwaStatus,
					    FukuwaTypeId = model.FukuwaTypeId,
					    FukwaDate = model.FukwaDate,
					    FukwaDateEng = model.FukwaDateEng?? DateTime.Now,
					    FukwaAmount = model.FukwaAmount,
					    Remarks = model.Remarks,
					    EmailDays = model.EmailDays,
					    PaymentType = model.PaymentType,
					    JammaDate = model.JammaDate,
					    JammaDateEng = model.JammaDateEng ?? DateTime.Now,
					    BankAccount = model.BankAccount,
					    PaskiDate = model.PaskiDate,
					    PaskiDateEng = model.PaskiDateEng?? DateTime.Now,
					    PaskiAmount = model.PaskiAmount,
					    PaskiRemaining = model.PaskiRemaining,
					    NirnayDate = model.NirnayDate,
					    NirnayDateEng = model.NirnayDateEng ?? DateTime.Now,
					    NirnayAdikari = model.NirnayAdikari,
					    MyadThapTypeId = model.MyadThapTypeId,
					    MyadThapStartDate = model.MyadThapStartDate,
					    MyadThapStartDateEng = model.MyadThapStartDateEng ?? DateTime.Now,
					    MyadThapEndDate = model.MyadThapEndDate,
					    MyadThapEndDateEng = model.MyadThapEndDateEng ??DateTime.Now,
				};
			    	 await	context.Con_BankGuarantee.AddAsync(data);
				     await 	context.SaveChangesAsync();

					if(model.BankGuaranteeList.Count > 0)
					{
						foreach(var item in model.BankGuaranteeList)
						{
							var partial = new Con_BankGuarantee()
							{
								Id = model.Id,
								BankGuaranteeName = item.BankGuaranteeName,
								YojanaId = model.YojanaId,
								ConsultantId = model.ConsultantId,
								BankId = item.BankId,
								BGType = item.BGType,
								BGNumber = item.BGNumber,
								Currency = item.Currency,
								Amount = item.Amount,
								JariMiti = item.JariMiti,
								JariMitiEng = item.JariMitiEng,
								FromDate = item.FromDate,
								FromDateEng = item.FromDateEng ,
								ToDate = item.ToDate,
								ToDateEng = item.ToDateEng,
								FukuwaStatus = item.FukuwaStatus,
								FukuwaTypeId = item.FukuwaTypeId,
								FukwaDate = item.FukwaDate,
								FukwaDateEng = item.FukwaDateEng ?? DateTime.Now,
								FukwaAmount = item.FukwaAmount,
								Remarks = item.Remarks,
								EmailDays = item.EmailDays,
								PaymentType = item.PaymentType,
								JammaDate = item.JammaDate,
								JammaDateEng = item.JammaDateEng?? DateTime.Now,
								BankAccount = item.BankAccount,
								PaskiDate = item.PaskiDate,
								PaskiDateEng = item.PaskiDateEng ?? DateTime.Now,
								PaskiAmount = item.PaskiAmount,
								PaskiRemaining = item.PaskiRemaining,
								NirnayDate = item.NirnayDate,
								NirnayDateEng = item.NirnayDateEng ?? DateTime.Now,
								NirnayAdikari = item.NirnayAdikari,
								MyadThapTypeId = item.MyadThapTypeId,
								MyadThapStartDate = item.MyadThapStartDate,
								MyadThapStartDateEng = item.MyadThapStartDateEng ?? DateTime.Now,
								MyadThapEndDate = item.MyadThapEndDate,
								MyadThapEndDateEng = item.MyadThapEndDateEng ?? DateTime.Now,
							};
							await context.Con_BankGuarantee.AddAsync(partial);
							await context.SaveChangesAsync();

						}
					}
				}
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
