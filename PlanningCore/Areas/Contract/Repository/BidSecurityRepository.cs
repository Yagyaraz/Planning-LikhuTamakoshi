using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using PlanningCore.Areas.Admin.Repositories;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Net;
using System.Security.Claims;

namespace PlanningCore.Areas.Contract.Repository
{
	public class BidSecurityRepository : IBidSecurity
	{
		private readonly PlanningContext context;
		private readonly ILogger<BidSecurityRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public BidSecurityRepository(PlanningContext _context, IHttpContextAccessor httpContextAccessor, ILogger<BidSecurityRepository> logger, IUtility utility)
		{
			context = _context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			_utility = utility;
		}
		public async Task<List<BidSecurityViewModel>> GetBidSecurity()
		{
			var data = await context.Con_BidSecurity.Where(x => x.Status == true)
				.Select(x => new BidSecurityViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					YojanaId = x.YojanaId,
					ConsultantId = x.ConsultantId,
					Address = x.Address,
					BGAmount = x.BGAmount,
					BGNumber = x.BGNumber,
					BSAmount = x.BSAmount,
					BankId = x.BankId,
					BankAccNumber = x.BankAccNumber,
					FromDate = x.FromDate,
					FromDateEng = x.FromDateEng,
					TODate = x.TODate,
					ToDateEng = x.ToDateEng,
					JariDate = x.JariDate,
					JariDateEng = x.JariDateEng,
					PaymentTypeId = x.PaymentTypeId,
					PaymentTypeName = x.PaymentType.Name,
					BankName = context.Class_A_Bank_List.Where(z => z.Class_A_Bank_List_Id == x.BankId).Select(z => z.BankName_Eng).FirstOrDefault(),
				}).ToListAsync();
			return data;
		}
		public async Task<BidSecurityViewModel> GetBidSecurityById(int id)
		{
			var data = context.Con_BidSecurity.Where(x => x.Id == id && x.Status == true).Select(x => new BidSecurityViewModel()

			{
				Id = x.Id,
				Name = x.Name,
				YojanaId = x.YojanaId,
				ConsultantId = x.ConsultantId,
				Address = x.Address,
				BGAmount = x.BGAmount,
				BGNumber = x.BGNumber,
				BSAmount = x.BSAmount,
				BankId = x.BankId,
				BankAccNumber = x.BankAccNumber,
				FromDate = x.FromDate,
				FromDateEng = x.FromDateEng,
				TODate = x.TODate,
				ToDateEng = x.ToDateEng,
				JariDate = x.JariDate,
				JariDateEng = x.JariDateEng,

				DecisionDate = x.DecisionDate,
				DecisionDateEng = x.DecisionDateEng,
				DecisionOfficer = x.DecisionOfficer,
				FukuwaAmount = x.FukuwaAmount,
				FukuwaDate = x.FukuwaDate,
				FukuwaDateEng = x.FukuwaDateEng,
				FukuwaStatus = x.FukuwaStatus,
				FukuwaTypeId = x.FukuwaTypeId,
				PaymentTypeId = x.PaymentTypeId,
				PaymentTypeName = x.PaymentType.Name,
			}).FirstOrDefaultAsync();
			return await data ?? new BidSecurityViewModel();
		}

		public async Task<bool> InsertUpdateBidSecurity(BidSecurityViewModel model)
		{
			try
			{
				var biddata = context.Con_BidSecurity.Where(x => x.Id == model.Id).FirstOrDefault();
				if (biddata != null)
				{
					biddata.Id = model.Id;
					biddata.Name = model.Name;
					biddata.YojanaId = model.YojanaId;
					biddata.ConsultantId = model.ConsultantId;
					biddata.Address = model.Address;
					biddata.BGAmount = model.BGAmount;
					biddata.BGNumber = model.BGNumber;
					biddata.BSAmount = model.BSAmount;
					biddata.BankId = model.BankId;
					biddata.BankAccNumber = model.BankAccNumber;
					biddata.FromDate = model.FromDate;
					biddata.FromDateEng = model.FromDateEng;
					biddata.TODate = model.TODate;
					biddata.ToDateEng = model.ToDateEng;
					biddata.JariDate = model.JariDate;
					biddata.JariDateEng = model.JariDateEng;

					biddata.DecisionDate = model.DecisionDate;
					biddata.DecisionDateEng = model.DecisionDateEng;
					biddata.DecisionOfficer = model.DecisionOfficer;
					biddata.FukuwaAmount = model.FukuwaAmount;
					biddata.FukuwaDate = model.FukuwaDate;
					biddata.FukuwaDateEng = model.FukuwaDateEng;
					biddata.FukuwaStatus = model.FukuwaStatus;
					biddata.FukuwaTypeId = model.FukuwaTypeId;
					biddata.PaymentTypeId = model.PaymentTypeId;
					biddata.Docs = model.File != null ? await _utility.UploadImgAsync("Con_BidSecurity", model.File) : biddata.Docs;

					context.Entry(biddata).State = EntityState.Modified;
					await context.SaveChangesAsync();
				}
				else
				{
					var data = new Con_BidSecurity()
					{
						Id = model.Id,
						Name = model.Name,
						YojanaId = model.YojanaId,
						ConsultantId = model.ConsultantId,
						Address = model.Address,
						BGAmount = model.BGAmount,
						BGNumber = model.BGNumber,
						BSAmount = model.BSAmount,
						BankId = model.BankId,
						BankAccNumber = model.BankAccNumber,
						FromDate = model.FromDate,
						FromDateEng = model.FromDateEng,
						TODate = model.TODate,
						ToDateEng = model.ToDateEng,
						JariDate = model.JariDate,
						JariDateEng = model.JariDateEng,

						DecisionDate = model.DecisionDate,
						DecisionDateEng = model.DecisionDateEng,
						DecisionOfficer = model.DecisionOfficer,
						FukuwaAmount = model.FukuwaAmount,
						FukuwaDate = model.FukuwaDate,
						FukuwaDateEng = model.FukuwaDateEng,
						FukuwaStatus = model.FukuwaStatus,
						FukuwaTypeId = model.FukuwaTypeId,
						PaymentTypeId = model.PaymentTypeId,
						Docs = await _utility.UploadImgAsync("Con_BidSecurity", model.File),
						Status = true,
					};
					await context.Con_BidSecurity.AddAsync(data);
					context.SaveChanges();
				}
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<object> GetYojanaInfo(int id)
		{
			return await context.Con_Yojana.Where(x => x.Id == id)
				.Select(x => new
				{
					name = x.YojanaName,
					address = x.YojanaAddress,
					amount = x.EstimatedAmount,
				}).FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteBidSecurity(int id)
		{
			var data = await context.Con_BidSecurity.FirstOrDefaultAsync(x => x.Id == id);
			if (data != null)
			{
				data.Status = false;
				context.Entry(data).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}


	}
}
