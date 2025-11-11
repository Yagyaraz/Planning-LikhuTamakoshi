using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Security.Claims;

namespace PlanningCore.Areas.Admin.Repositories
{
	public class BudgetRepositories : IBudget
	{
		private readonly PlanningContext _context;
		private readonly ILogger<CommonRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public BudgetRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
		{
			_context = context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			_utility = utility;
		}
		public async Task<List<BudgetSourceViewModel>> GetAllBudgetSource()
		{
			var fiscalId = await _utility.GetCurrentFiscalYear();
			return await _context.BudgetSource.Where(x => x.FiscalYearId == fiscalId && x.IsDeleted == false)
				.Select(x => new BudgetSourceViewModel()
				{
					BudgetSourceId = x.BudgetSourceId,
					BudgetSourceName = x.BudgetSourceName,
					Amount = x.Amount,
					IsDeleted = x.IsDeleted,
				}).ToListAsync();
		}
		public async Task<BudgetSourceViewModel> GetBudgetSourceById(int id)
		{
			var fiscalId = await _utility.GetCurrentFiscalYear();
			return await _context.BudgetSource.Where(x => x.BudgetSourceId == id && x.FiscalYearId == fiscalId)
			   .Select(x => new BudgetSourceViewModel()
			   {
				   BudgetSourceId = x.BudgetSourceId,
				   BudgetSourceName = x.BudgetSourceName,
				   Amount = x.Amount,
				   IsDeleted = x.IsDeleted,
			   }).FirstOrDefaultAsync() ?? new BudgetSourceViewModel();
		}
		public async Task<bool> InsertUpdateBudgetSource(BudgetSourceViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var budgetSource = await _context.BudgetSource.FirstOrDefaultAsync(x => x.BudgetSourceId == model.BudgetSourceId);
					if (budgetSource != null)
					{
						budgetSource.BudgetSourceName = model.BudgetSourceName;
						budgetSource.Amount = model.Amount;
						budgetSource.IsDeleted = false;
						_context.Entry(budgetSource).State = EntityState.Modified;
					}
					else
					{
						var fiscalId = await _utility.GetCurrentFiscalYear();
						budgetSource = await _context.BudgetSource.FirstOrDefaultAsync(x => x.FiscalYearId == fiscalId && x.BudgetSourceName.Trim().Equals(model.BudgetSourceName));
						if (budgetSource != null)
						{
							budgetSource.IsDeleted = false;
							_context.Entry(budgetSource).State = EntityState.Modified;
						}
						else
						{
							budgetSource = new BudgetSource()
							{
								BudgetSourceName = model.BudgetSourceName,
								Amount = model.Amount,
								IsDeleted = false,
								FiscalYearId = fiscalId,
							};
							await _context.BudgetSource.AddAsync(budgetSource);
						}
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
		public async Task<bool> DeleteBudgetSourceById(int id)
		{
			var data = await _context.BudgetSource.Where(x => x.BudgetSourceId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				data.DeletedBy = _userId;
				data.DeletedDate = DateTime.Now;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}

		//BudgetType
		public async Task<List<BudgetTypeViewModel>> GetAllBudgetType()
		{
			return await _context.BudgetType
			   .Select(x => new BudgetTypeViewModel()
			   {
				   BudgetTypeId = x.BudgetTypeId,
				   BudgetTypeName = x.BudgetTypeName,
				   IsDeleted = x.IsDeleted,
			   }).ToListAsync() ?? new List<BudgetTypeViewModel>();
		}

		public async Task<BudgetTypeViewModel> GetBudgetTypeById(int id)
		{
			return await _context.BudgetType.Where(x => x.BudgetTypeId == id)
			  .Select(x => new BudgetTypeViewModel()
			  {
				  BudgetTypeId = x.BudgetTypeId,
				  BudgetTypeName = x.BudgetTypeName,
				  IsDeleted = x.IsDeleted,
			  }).FirstOrDefaultAsync() ?? new BudgetTypeViewModel();
		}

		public async Task<bool> InsertUpdateBudgetType(BudgetTypeViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.BudgetTypeId > 0)
					{
						var budgetSource = await _context.BudgetType.FirstOrDefaultAsync(x => x.BudgetTypeId == model.BudgetTypeId);
						if (budgetSource != null)
						{
							budgetSource.BudgetTypeName = model.BudgetTypeName;

							_context.Entry(budgetSource).State = EntityState.Modified;
						}
						else
						{
							return false;
						}
					}
					else
					{
						var budgetType = new BudgetType()
						{
							BudgetTypeName = model.BudgetTypeName,

							IsDeleted = false,

						};
						await _context.BudgetType.AddAsync(budgetType);
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

		public async Task<bool> DeleteBudgetTypeById(int id)
		{
			var data = await _context.BudgetType.Where(x => x.BudgetTypeId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;

				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}

		//BudgetSubType
		public async Task<List<BudgetSubTypeViewModel>> GetAllBudgetSubType()
		{
			return await _context.BudgetSubType
			   .Select(x => new BudgetSubTypeViewModel()
			   {
				   Id = x.Id,
				   Amount = x.Amount,
				   BudgetTypeId = x.BudgetTypeId,
				   BudgetSubTypeName = x.BudgetSubTypeName,
			   }).ToListAsync();
		}

		public async Task<BudgetSubTypeViewModel> GetBudgetSubTypeById(int id)
		{
			return await _context.BudgetSubType.Where(x => x.Id == id)
			  .Select(x => new BudgetSubTypeViewModel()
			  {
				  Id = x.Id,
				  BudgetTypeId = x.BudgetTypeId,
				  BudgetSubTypeName = x.BudgetSubTypeName,
			  }).FirstOrDefaultAsync();
		}

		public async Task<bool> InsertUpdateBudgetSubType(BudgetSubTypeViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.Id > 0)
					{
						var budgetSource = await _context.BudgetSubType.FirstOrDefaultAsync(x => x.Id == model.Id);
						if (budgetSource != null)
						{
							budgetSource.BudgetSubTypeName = model.BudgetSubTypeName;
							budgetSource.BudgetTypeId = model.BudgetTypeId;
							budgetSource.Amount = model.Amount;

							_context.Entry(budgetSource).State = EntityState.Modified;
						}
						else
						{
							return false;
						}
					}
					else
					{
						int fiscalid = await _utility.GetCurrentFiscalYear();
						var budgetSubType = new BudgetSubType()
						{
							BudgetSubTypeName = model.BudgetSubTypeName,
							BudgetTypeId = model.BudgetTypeId,
							FiscalYearId = fiscalid,
							Amount = model.Amount,
							IsDeleted = false,
						};
						await _context.BudgetSubType.AddAsync(budgetSubType);
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

		public async Task<bool> DeleteBudgetSubTypeById(int id)
		{
			var data = await _context.BudgetSubType.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				data.DeletedDate = DateTime.Now;
				data.DeletedBy = _userId;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}
	}
}

