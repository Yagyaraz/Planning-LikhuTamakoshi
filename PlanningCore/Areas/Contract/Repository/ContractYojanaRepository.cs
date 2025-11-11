using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Admin.Repositories;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanningCore.Areas.Contract.Repository
{
	public class ContractYojanaRepository : IContractYojana
	{
		private readonly PlanningContext _context;
		private readonly ILogger<CommonRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility = null;
		public ContractYojanaRepository(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
		{
			_context = context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			_utility = utility;
		}
		public async Task<bool> DeleteContractYojana(int id)
		{
			var data = await _context.Con_Yojana.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<Con_YojanaViewModel>> GetAllContractYojana()
		{
			var data = await _context.Con_Yojana.Where(x => x.IsDeleted == false).Select(x => new Con_YojanaViewModel()
			{
				Id = x.Id,
				YojanaName = x.YojanaName,
				EstimatedAmount = x.EstimatedAmount,
				IsDeleted = x.IsDeleted,
				WardId = x.WardId,
				Amount = x.Amount,
				FiscalYearId = x.FiscalYearId,
				SarkarBudget = x.SarkarBudget,
				UpabhoktaBudget = x.UpabhoktaBudget,
				OtherBudget = x.OtherBudget,
				YojanaAddress = x.YojanaAddress,
				WardIds = _context.Con_Yojana_Ward.Where(z => z.YojanaId == x.Id).Select(z => z.Id).ToList(),
				//  WardName = _context.Ward.Where(w => w.Id == x.WardId).Select(w => w.Name).FirstOrDefault(),
				IsAllowToDelete = !(_context.Con_BidSecurity.Any(b=> b.YojanaId == x.Id) || _context.Con_BankGuarantee.Any(b=> b.YojanaId == x.Id) || _context.Con_Samjhauta.Any(b=> b.YojanaId == x.Id)),
			}).ToListAsync() ?? new List<Con_YojanaViewModel>();
			return data;
		}

		public async Task<Con_YojanaViewModel> GetContractYojanaById(int? id)
		{
			var data = await _context.Con_Yojana.Where(x => x.Id == id).Select(x => new Con_YojanaViewModel()
			{
				Id = x.Id,
				YojanaName = x.YojanaName,
				EstimatedAmount = x.EstimatedAmount,
				WardId = x.WardId,
				FiscalYearId = x.FiscalYearId,
				SarkarBudget = x.SarkarBudget,
				UpabhoktaBudget = x.UpabhoktaBudget,
				OtherBudget = x.OtherBudget,
				YojanaAddress = x.YojanaAddress,
				Latitude = x.Latitude,
				Longitude = x.Longitude,
				WardIds = _context.Con_Yojana_Ward.Where(z => z.YojanaId == x.Id).Select(z => z.WardId).ToList(),
			}).FirstOrDefaultAsync() ?? new Con_YojanaViewModel();
			return data;
		}

		public async Task<bool> InsertUpdateContractYojana(Con_YojanaViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				//int fiscalYearId = await _utility.GetCurrentFiscalYear();
				try
				{
					var update = _context.Con_Yojana.Find(model.Id);
					if (update != null)
					{
						update.YojanaName = model.YojanaName;
						update.EstimatedAmount = model.EstimatedAmount;
						update.WardId = model.WardId;
						update.SarkarBudget = model.SarkarBudget;
						update.UpabhoktaBudget = model.UpabhoktaBudget;
						update.OtherBudget = model.OtherBudget;
						update.YojanaAddress = model.YojanaAddress;
						update.Latitude = model.Latitude;
						update.Longitude = model.Longitude;
						update.ModifiedBy = _userId;
						update.ModifiedDate = DateTime.Now;

						_context.Entry(update).State = EntityState.Modified;
						await _context.SaveChangesAsync();

						if (model.WardIds.Count > 0)
						{
							var data_wardIds = _context.Con_Yojana_Ward.Where(x => x.YojanaId == model.Id).ToList();

							if (data_wardIds.Count > 0)
							{
								foreach (var item in data_wardIds)
								{
									_context.Con_Yojana_Ward.Remove(item);
									_context.SaveChanges();
								}
							}
							foreach (var item1 in model.WardIds)
							{
								var wardData = new Con_Yojana_Ward()
								{
									WardId = item1,
									YojanaId = model.Id
								};
								await _context.Con_Yojana_Ward.AddAsync(wardData);
								await _context.SaveChangesAsync();
							}
						}
					}
					else
					{
						var data = new Con_Yojana()
						{
							YojanaName = model.YojanaName,
							EstimatedAmount = model.EstimatedAmount,
							SarkarBudget = model.SarkarBudget,
							UpabhoktaBudget = model.UpabhoktaBudget,
							OtherBudget = model.OtherBudget,
							YojanaAddress = model.YojanaAddress,
							Longitude = model.Longitude,
							Latitude = model.Latitude,
							IsDeleted = false,
							CreatedBy = _userId,
							CreatedDate = DateTime.Now
						};
						await _context.Con_Yojana.AddAsync(data);
						await _context.SaveChangesAsync();
						if (model.WardIds.Count > 0)
						{
							foreach (var item1 in model.WardIds)
							{
								var wardData = new Con_Yojana_Ward()
								{
									WardId = item1,
									YojanaId = data.Id
								};
								await _context.Con_Yojana_Ward.AddAsync(wardData);
								await _context.SaveChangesAsync();
							}
						}
					}

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




	}
}
