using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanningCore.Areas.Admin.Repositories
{
    public class TravelExpenseRepository : ITravelExpense
    {
        private readonly PlanningContext _context;
        private readonly ILogger<CommonRepository> _logger;
        private readonly string _userId = null;
        private readonly IUtility _utility = null;

        public TravelExpenseRepository(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
        {
            _context = context;
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _utility = utility;
        }
        #region FiscalYear
        public async Task<List<TravelExpenseViewModel>> GetAllTravelExpenses()
        {
            return await _context.TravelExpense
                .Select(x => new TravelExpenseViewModel()
                {
                    Id = x.Id,
                    EmployeeName = x.EmployeeName,
                    PostId = x.PostId,
                    Advance = x.Advance,
                    Amount = x.Amount,
                    BudgetNumber = x.BudgetNumber,
                    ChequeNumber = x.ChequeNumber,
                    Location = x.Location,
                    Reason = x.Reason,
                    TravelFrom = x.TravelFrom,
                    TravelTo = x.TravelTo,
                    vehicle = x.vehicle,
                    PostName = x.Pada.Name,
                }).ToListAsync() ?? new List<TravelExpenseViewModel>();
        }
        public async Task<TravelExpenseViewModel> GetTravelExpenseById(int? id)
        {
            return await _context.TravelExpense.Where(x => x.Id == id)
                .Select(x => new TravelExpenseViewModel()
                {
                    Id = x.Id,
                    EmployeeName = x.EmployeeName,
                    PostId = x.PostId,
                    Advance = x.Advance,
                    Amount = x.Amount,
                    BudgetNumber = x.BudgetNumber,
                    ChequeNumber = x.ChequeNumber,
                    Location = x.Location,
                    Reason = x.Reason,
                    TravelFrom = x.TravelFrom,
                    TravelTo = x.TravelTo,
                    vehicle = x.vehicle,
                    PostName = x.Pada.Name,
                }).FirstOrDefaultAsync() ?? new TravelExpenseViewModel();
        }
        public async Task<bool> CreateTravelExpense(TravelExpenseViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    var travel = await _context.TravelExpense.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (travel != null)
                    {
                        travel.EmployeeName = model.EmployeeName;
                        travel.PostId = model.PostId;
                        travel.Advance = model.Advance;
                        travel.Amount = model.Amount;
                        travel.BudgetNumber = model.BudgetNumber;
                        travel.ChequeNumber = model.ChequeNumber;
                        travel.Location = model.Location;
                        travel.Reason = model.Reason;
                        travel.TravelFrom = model.TravelFrom;
                        travel.TravelTo = model.TravelTo;
                        travel.vehicle = model.vehicle;
                        travel.UpdatedBy = _userId;
                        travel.UpdatedDate = DateTime.Now;
                        travel.UpdatedBy = _userId;
                        travel.UpdatedDate = DateTime.Now;
                        _context.Entry(travel).State = EntityState.Modified;
                    }
                    if (model.TravelExpenseList.Count > 0)
                    {
                        foreach (var item in await _context.TravelRequiredItems.Where(x => x.TeavelExpenseId == model.Id).ToListAsync())
                        {
                            if (!model.TravelExpenseList.Any(x => x.TravelRequiredItemId == item.Id))
                            {
                                _context.TravelRequiredItems.Remove(item);
                                await _context.SaveChangesAsync();
                            }
                        }
                        foreach (var item in model.TravelExpenseList)
                        {
                            var traveldata = await _context.TravelRequiredItems.Where(x => x.Id == item.TravelRequiredItemId).FirstOrDefaultAsync();
                            if (traveldata != null)
                            {
                                traveldata.TeavelExpenseId = travel.Id;
                                traveldata.ItemNames = item.ItemNames;
                                _context.Entry(traveldata).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                var travelexpense = new TravelRequiredItems()
                                {
                                    TeavelExpenseId = travel.Id,
                                    ItemNames = item.ItemNames,

                                };
                                await _context.TravelRequiredItems.AddAsync(travelexpense);
                                _context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    var travelex = new TeavelExpense()
                    {
                        EmployeeName = model.EmployeeName,
                        PostId = model.PostId,
                        Advance = model.Advance,
                        Amount = model.Amount,
                        BudgetNumber = model.BudgetNumber,
                        ChequeNumber = model.ChequeNumber,
                        Location = model.Location,
                        Reason = model.Reason,
                        TravelFrom = model.TravelFrom,
                        TravelTo = model.TravelTo,
                        vehicle = model.vehicle,
                        CreatedBy = _userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.TravelExpense.AddAsync(travelex);
                    _context.SaveChanges();
                    if (model.TravelExpenseList.Count > 0)
                    {
                        foreach (var item in model.TravelExpenseList)
                        {
                            var data = new TravelRequiredItems()
                            {
                                TeavelExpenseId = travelex.Id,
                                ItemNames = item.ItemNames,
                            };
                            await _context.TravelRequiredItems.AddAsync(data);
                            _context.SaveChanges();
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("FiscalYear Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }

        }
        #endregion

    }
}
