using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.ComponentModel;
using System.Security.Claims;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PlanningCore.Areas.Admin.Repositories
{
	public class CommonRepository : ICommon
	{
		private readonly PlanningContext _context;
		private readonly ILogger<CommonRepository> _logger;
		private readonly IUtility _utility;
		private readonly string _userId = null;

		public CommonRepository(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
		{
			_context = context;
			_logger = logger;
			_utility = utility;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		#region FiscalYear
		public async Task<List<FiscalYearViewModel>> GetAllFiscalYear()
		{
			return await _context.FiscalYear
				.Select(x => new FiscalYearViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Name_En = x.Name_En,
					Code = x.Code,
					StartYear = x.StartYear,
					EndYear = x.EndYear,
					IsActive = x.IsActive,
					DateFrom = x.DateFrom,
					DateFromEng = x.DateFromEng,
					DateTo = x.DateTo,
					DateToEng = x.DateToEng,
				}).ToListAsync();
		}
		public async Task<FiscalYearViewModel> GetFiscalYear(int id)
		{
			return await _context.FiscalYear.Where(x => x.Id == id)
				.Select(x => new FiscalYearViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Name_En = x.Name_En,
					Code = x.Code,
					StartYear = x.StartYear,
					EndYear = x.EndYear,
					IsActive = x.IsActive,
					DateFrom = x.DateFrom,
					DateFromEng = x.DateFromEng,
					DateTo = x.DateTo,
					DateToEng = x.DateToEng,
				}).FirstOrDefaultAsync() ?? new FiscalYearViewModel();
		}
		public async Task<bool> InsertUpdateFiscalYear(FiscalYearViewModel model)
		{
			try
			{
				if (model.IsActive == true)
				{
					foreach (var item in await _context.FiscalYear.Where(x => x.IsActive == true).ToListAsync())
					{
						item.IsActive = false;
						_context.Entry(item).State = EntityState.Modified;
						await _context.SaveChangesAsync();
					}
				}

				var fiscalYear = await _context.FiscalYear.FirstOrDefaultAsync(x => x.Id == model.Id);
				if (fiscalYear != null)
				{
					fiscalYear.Name = model.Name;
					fiscalYear.Name_En = model.Name_En;
					fiscalYear.Code = model.Code;
					fiscalYear.StartYear = model.StartYear;
					fiscalYear.EndYear = model.EndYear;
					fiscalYear.IsActive = model.IsActive;
					fiscalYear.DateFrom = model.DateFrom;
					fiscalYear.DateFromEng = model.DateFromEng;
					fiscalYear.DateTo = model.DateTo;
					fiscalYear.DateToEng = model.DateToEng;
					fiscalYear.UpdatedBy = _userId;
					fiscalYear.UpdatedDate = DateTime.Now;

					_context.Entry(fiscalYear).State = EntityState.Modified;
					await _context.SaveChangesAsync();
				}
				else
				{
					var fiscal = new FiscalYear()
					{
						Name = model.Name,
						Name_En = model.Name_En,
						Code = model.Code,
						StartYear = model.StartYear,
						EndYear = model.EndYear,
						IsActive = model.IsActive,
						DateFrom = model.DateFrom,
						DateFromEng = model.DateFromEng,
						DateTo = model.DateTo,
						DateToEng = model.DateToEng,
						CreatedBy = _userId,
						CreatedDate = DateTime.Now,
					};
					await _context.FiscalYear.AddAsync(fiscal);
					await _context.SaveChangesAsync();
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

		#region Pada
		public async Task<List<PadaViewModel>> GetAllPada()
		{
			return await _context.Pada.Where(x => x.IsDeleted == false)
				.Select(x => new PadaViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					IsDeleted = x.IsDeleted,
				}).ToListAsync();
		}
		public async Task<PadaViewModel> GetPadaById(int id)
		{
			return await _context.Pada.Where(x => x.Id == id)
				.Select(x => new PadaViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					IsDeleted = x.IsDeleted,
				}).FirstOrDefaultAsync() ?? new PadaViewModel();
		}
		public async Task<bool> InsertUpdatePada(PadaViewModel model)
		{
			try
            {
                var pada = await _context.Pada.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (pada != null)
                {
                    pada.Name = model.Name;
                    pada.IsDeleted = false;
                    pada.UpdatedBy = _userId;
                    pada.UpdatedDate = DateTime.Now;
                    _context.Entry(pada).State = EntityState.Modified;
                }
                else
                {
                    pada = await _context.Pada.FirstOrDefaultAsync(x => x.Name.Trim().Equals(model.Name));
                    if (pada != null)
                    {
                        pada.IsDeleted = false;
                        _context.Entry(pada).State = EntityState.Modified;
                    }
                    else
                    {
                        pada = new Pada()
                        {
                            Name = model.Name,
                            IsDeleted = false,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.Pada.AddAsync(pada);
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
        public async Task<bool> DeletePada(int id)
        {
            var data = await _context.Pada.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.IsDeleted = true;
                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        #endregion

        #region Employee
        public async Task<List<EmployeeViewModel>> GetAllEmployee()
		{
			return await _context.Employee.Where(x => x.IsDeleted == false)
				.Select(x => new EmployeeViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					PadaId = x.PadaId,
					WardId = x.WardId,
					WardName = _context.Ward.Where(z => z.Id == x.WardId).Select(z => z.Name).FirstOrDefault(),
					PadaName = _context.Pada.Where(y => y.Id == x.PadaId).Select(y => y.Name).FirstOrDefault(),
				}).ToListAsync();
		}
		public async Task<EmployeeViewModel> GetEmployeeById(int id)
		{
			return await _context.Employee.Where(x => x.Id == id)
				.Select(x => new EmployeeViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					PadaId = x.PadaId,
					WardId = x.WardId,
					WardName = _context.Ward.Where(z => z.Id == x.WardId).Select(z => z.Name).FirstOrDefault(),
					PadaName = _context.Pada.Where(y => y.Id == x.Id).Select(y => y.Name).FirstOrDefault(),
				}).FirstOrDefaultAsync() ?? new EmployeeViewModel();
		}
		public async Task<bool> InsertUpdateEmployee(EmployeeViewModel model)
		{
			try
			{
				if (model.Id > 0)
				{
					var employee = await _context.Employee.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
					if (employee != null)
					{
						employee.Name = model.Name;
						employee.WardId = model.WardId;
						employee.PadaId = model.PadaId;
						employee.IsDeleted = false;
						employee.UpdatedDate = DateTime.Now;
						employee.UpdatedBy = _userId;
						_context.Entry(employee).State = EntityState.Modified;
					}
				}
				else
				{
					var employee = new Employee()
					{
						Name = model.Name,
						WardId = model.WardId,
						PadaId = model.PadaId,
						IsDeleted = false,
						CreatedBy = _userId,
						CreatedDate = DateTime.Now,
					};
					await _context.Employee.AddAsync(employee);
					await _context.SaveChangesAsync();
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				//_logger.LogInformation("Employee Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
				return false;
			}
		}
		public async Task<bool> DeleteEmployee(int id)
		{
			var sarta = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (sarta != null)
			{
				sarta.IsDeleted = true;

				_context.Entry(sarta).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		#endregion
		#region AnugumanSamiti
		public async Task<List<AnugamanSamitiSetupViewModel>> GetAnugumanSamiti()
		{
			var data = await _context.AnugamanSamitiSetup.Where(x=>x.IsDeleted==false).Select(x => new AnugamanSamitiSetupViewModel()
			{
				Id = x.Id,
				WardId = x.WardId,
				FiscalYearId = x.FiscalYearId,
				AnuhumanSamitiType=x.AnugumanType,
				AnuhumanSamitiTypeName=(x.AnugumanType==1?"पालिका अनुगमन समिति ":(x.WardId+" न. वडा अनुगमन समिति")),
			}).ToListAsync()??new List<AnugamanSamitiSetupViewModel>();
			return data;

		}	
		public async Task<AnugamanSamitiSetupViewModel> GetAnugumanSamitiById(int? id)
		{
			var data = await _context.AnugamanSamitiSetup.Where(x => x.Id == id).Select(x => new AnugamanSamitiSetupViewModel()
			{
				Id = x.Id,
				WardId = x.WardId,
				YojanaId = x.YojanaId,
				FiscalYearId = x.FiscalYearId,
				AnuhumanSamitiType = x.AnugumanType,

				AnugamanSamitiList =  _context.AnugamanSamitiMember.Where(y => y.AnugamanSamitiId ==x.Id).Select(z=>new AnugamanSamitiMemberViewModel()
				{
					//AnugamanMemberId=z.AnugamanSamitiId,
					PostId=z.PostId,
					Name=z.Name,
					PostName= _context.Pada.Where(y => y.Id == z.PostId).Select(z => z.Name).FirstOrDefault(),
				}).ToList()?? new List<AnugamanSamitiMemberViewModel>(),
			}).FirstOrDefaultAsync()??new AnugamanSamitiSetupViewModel();
			return data;
			
		}
        //public async Task<bool>InsertUpdateAnugamanSamiti(AnugamanSamitiSetupViewModel model)
        //{
        //	using (var transaction = _context.Database.BeginTransaction())
        //	{
        //              try
        //              {
        //                  if (model.Id > 0)
        //                  {
        //                      using var transection = await _context.Database.BeginTransactionAsync();
        //                      try
        //                      {
        //                          // Update main entity
        //                          var anugamansamiti = await _context.AnugamanSamitiSetup
        //                              .FirstOrDefaultAsync(x => x.Id == model.Id);
        //                          if (anugamansamiti == null)
        //                              return false;

        //                          anugamansamiti.WardId = model.WardId;
        //                          anugamansamiti.AnugumanType = model.AnuhumanSamitiType;
        //                          anugamansamiti.YojanaId = model.YojanaId;
        //                          anugamansamiti.IsDeleted = false;

        //                          // Handle members if any exist
        //                          if (model.AnugamanSamitiList?.Count > 0)
        //                          {
        //                              // Get existing members in one query
        //                              var existingMembers = await _context.AnugamanSamitiMember
        //                                  .Where(x => x.AnugamanSamitiId == model.Id)
        //                                  .ToListAsync();

        //                              // Remove members not in the new list
        //                              var membersToRemove = existingMembers
        //                                  .Where(x => !model.AnugamanSamitiList
        //                                      .Any(m => m.AnugamanMemberId == x.AnugamanMemberId))
        //                                  .ToList();
        //                              _context.AnugamanSamitiMember.RemoveRange(membersToRemove);

        //                              // Update or add members
        //                              foreach (var item in model.AnugamanSamitiList)
        //                              {
        //                                  var existingMember = existingMembers
        //                                      .FirstOrDefault(x => x.AnugamanMemberId == item.AnugamanMemberId);

        //                                  if (existingMember != null)
        //                                  {
        //                                      existingMember.Name = item.Name;
        //                                      existingMember.PostId = item.PostId;
        //                                      existingMember.IsDeleted = false;
        //                                  }
        //                                  else
        //                                  {
        //                                      await _context.AnugamanSamitiMember.AddAsync(new AnugamanSamitiMember
        //                                      {
        //                                          AnugamanMemberId = item.AnugamanMemberId,
        //                                          AnugamanSamitiId = model.Id,
        //                                          Name = item.Name,
        //                                          PostId = item.PostId,
        //                                          IsDeleted = false,
        //                                      });
        //                                  }
        //                              }
        //                          }

        //                          // Save all changes in one go
        //                          await _context.SaveChangesAsync();
        //                          await transection.CommitAsync();
        //                          return true;
        //                      }
        //                      catch (Exception)
        //                      {
        //                          await transection.RollbackAsync();
        //                          throw;
        //                      }
        //                  }

        //                  else
        //                  {
        //                      int fiscalYearId = await _utility.GetCurrentFiscalYear();
        //				var anugamansamiti = new AnugamanSamitiSetup()
        //				{
        //					Id = model.Id,
        //					WardId = model.WardId,
        //					FiscalYearId = fiscalYearId,
        //					AnugumanType = model.AnuhumanSamitiType,
        //					IsDeleted= false,
        //				};
        //				await _context.AnugamanSamitiSetup.AddAsync(anugamansamiti);
        //				await _context.SaveChangesAsync();
        //				if (model.AnugamanSamitiList.Count > 0)
        //				{
        //					foreach (var item in model.AnugamanSamitiList)
        //					{
        //						var anugaman = new AnugamanSamitiMember()
        //						{
        //							AnugamanMemberId = item.AnugamanMemberId,
        //							AnugamanSamitiId= anugamansamiti.Id,
        //                                  Name = item.Name,
        //							PostId = item.PostId,
        //							IsDeleted=false,
        //						};
        //						await _context.AnugamanSamitiMember.AddAsync(anugaman);
        //						await _context.SaveChangesAsync();
        //					}
        //				}
        //				await _context.SaveChangesAsync();
        //				await transaction.CommitAsync();
        //				return true;
        //			}
        //              }
        //              catch (Exception ex)
        //              {
        //			await transaction.RollbackAsync();
        //                  return false;
        //              }
        //          }					
        //}
        public async Task<bool> InsertUpdateAnugamanSamiti(AnugamanSamitiSetupViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Id > 0)
                    {
                        var anugamansamiti = await _context.AnugamanSamitiSetup
                            .FirstOrDefaultAsync(x => x.Id == model.Id);

                        if (anugamansamiti == null)
                            return false;

                        anugamansamiti.WardId = model.WardId;
                        anugamansamiti.AnugumanType = model.AnuhumanSamitiType;
                        anugamansamiti.YojanaId = model.YojanaId;
                        anugamansamiti.IsDeleted = false;

                        _context.AnugamanSamitiSetup.Update(anugamansamiti);

                        if (model.AnugamanSamitiList?.Any() == true)
                        {
                            var existingMembers = await _context.AnugamanSamitiMember
                                .Where(x => x.AnugamanSamitiId == model.Id)
                                .ToListAsync();

                            var membersToRemove = existingMembers
                                .Where(x => !model.AnugamanSamitiList
                                    .Any(m => m.AnugamanMemberId == x.AnugamanMemberId))
                                .ToList();

                            if (membersToRemove.Any())
                            {
                                _context.AnugamanSamitiMember.RemoveRange(membersToRemove);
                            }

                            foreach (var item in model.AnugamanSamitiList)
                            {
                                var existingMember = existingMembers
                                    .FirstOrDefault(x => x.AnugamanMemberId == item.AnugamanMemberId);

                                if (existingMember != null)
                                {
                                    existingMember.Name = item.Name;
                                    existingMember.PostId = item.PostId;
                                    existingMember.IsDeleted = false;
                                    _context.AnugamanSamitiMember.Update(existingMember);
                                }
                                else
                                {
                                    var newMember = new AnugamanSamitiMember
                                    {
                                        AnugamanMemberId = item.AnugamanMemberId,
                                        AnugamanSamitiId = model.Id,
                                        Name = item.Name,
                                        PostId = item.PostId,
                                        IsDeleted = false
                                    };
                                    await _context.AnugamanSamitiMember.AddAsync(newMember);
                                }
                            }
                        }
                    }
                    else
                    {
                        int fiscalYearId = await _utility.GetCurrentFiscalYear();
                        var anugamansamiti = new AnugamanSamitiSetup
                        {
                            WardId = model.WardId,
                            FiscalYearId = fiscalYearId,
                            AnugumanType = model.AnuhumanSamitiType,
                            YojanaId = model.YojanaId,
                            IsDeleted = false
                        };

                        await _context.AnugamanSamitiSetup.AddAsync(anugamansamiti);
                        await _context.SaveChangesAsync(); 

                        if (model.AnugamanSamitiList?.Any() == true)
                        {
                            var members = model.AnugamanSamitiList.Select(item => new AnugamanSamitiMember
                            {
                                AnugamanMemberId = item.AnugamanMemberId,
                                AnugamanSamitiId = anugamansamiti.Id,
                                Name = item.Name,
                                PostId = item.PostId,
                                IsDeleted = false
                            }).ToList();

                            await _context.AnugamanSamitiMember.AddRangeAsync(members);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<bool> DeleteAnugamanSamiti(int id)
        {
            var anugumansamiti = await _context.AnugamanSamitiSetup.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (anugumansamiti != null)
            {
                anugumansamiti.IsDeleted = true;

                _context.Entry(anugumansamiti).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
		public async Task<AnugamanSamitiSetupViewModel> GetAllSamitiMember(int? id)
		{
			var data= await _context.AnugamanSamitiSetup.Where(x=>x.Id == id).Select(x=>new AnugamanSamitiSetupViewModel()
			{
				Id = x.Id,
                AnugamanSamitiList = _context.AnugamanSamitiMember.Where(y => y.AnugamanSamitiId == x.Id).Select(z => new AnugamanSamitiMemberViewModel()
                {
                    //AnugamanMemberId=z.AnugamanSamitiId,
                    PostId = z.PostId,
                    Name = z.Name,
                    PostName = _context.Pada.Where(y => y.Id == z.PostId).Select(z => z.Name).FirstOrDefault(),
                }).ToList() ?? new List<AnugamanSamitiMemberViewModel>(),
            }).FirstOrDefaultAsync()??new AnugamanSamitiSetupViewModel();
			return data;
		}
        #endregion
        #region Chhetra

        //Chettra
        public async Task<List<ChettraViewModel>> GetAllChettra()
		{
			return await _context.Chettra.Where(x => x.IsDeleted == false)
             .Select(x => new ChettraViewModel()
			 {
				 ChettraId = x.ChettraId,
				 ChettraName = x.ChettraName,
				 IsDeleted = x.IsDeleted,
			 }).ToListAsync() ?? new List<ChettraViewModel>();
		}
		public async Task<ChettraViewModel> GetChettraById(int id)
		{
			return await _context.Chettra.Where(x => x.ChettraId == id)
				 .Select(x => new ChettraViewModel()
				 {
					 ChettraId = x.ChettraId,
					 ChettraName = x.ChettraName,
					 IsDeleted = x.IsDeleted,
				 }).Where(x => x.IsDeleted == false).FirstOrDefaultAsync() ?? new ChettraViewModel();
		}
		public async Task<bool> InsertUpdateChettra(ChettraViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var Chettra = await _context.Chettra.FirstOrDefaultAsync(x => x.ChettraId == model.ChettraId);
					if (Chettra != null)
					{
						Chettra.ChettraName = model.ChettraName;
						Chettra.IsDeleted = false;
						_context.Entry(Chettra).State = EntityState.Modified;
					}
					else
					{
						Chettra = await _context.Chettra.FirstOrDefaultAsync(x => x.ChettraName.Trim().Equals(model.ChettraName));
						if (Chettra != null)
						{
							Chettra.IsDeleted = false;
							_context.Entry(Chettra).State = EntityState.Modified;
						}
						else
						{
							Chettra = new Chettra()
							{
								ChettraName = model.ChettraName,
								IsDeleted = false,
							};
							await _context.Chettra.AddAsync(Chettra);
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
		public async Task<bool> DeleteChettraById(int id)
		{
			var data = await _context.Chettra.Where(x => x.ChettraId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}

		//UpaChetra
		public async Task<List<UpaChetraViewModel>> GetAllUpaChetra()
		{
			return await _context.UpaChetra.Where(x=> x.IsDeleted == false)
			 .Select(x => new UpaChetraViewModel()
			 {
				 UpaChettraId = x.UpaChettraId,
				 UpaChettra = x.UpaChettra,
				 ChettraId = x.ChettraId??0,
				 KharchaSirshark = x.KharchaSirshark,
				 ChettraName = x.Chettra.ChettraName,
				 IsDeleted = x.IsDeleted,
			 }).ToListAsync() ?? new List<UpaChetraViewModel>();
		}
		public async Task<UpaChetraViewModel> GetUpaChetraById(int id)
		{
			return await _context.UpaChetra.Where(x => x.UpaChettraId == id)
				 .Select(x => new UpaChetraViewModel()
				 {
					 UpaChettraId = x.UpaChettraId,
					 UpaChettra = x.UpaChettra,
					 ChettraId = x.ChettraId??0,
					 KharchaSirshark = x.KharchaSirshark,
                     ChettraName = x.Chettra.ChettraName,
                     IsDeleted = x.IsDeleted,
				 }).Where(x => x.IsDeleted == false).FirstOrDefaultAsync() ?? new UpaChetraViewModel();
		}
		public async Task<bool> InsertUpdateUpaChetra(UpaChetraViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var upaChetra = await _context.UpaChetra.FirstOrDefaultAsync(x => x.UpaChettraId == model.UpaChettraId);
					if (upaChetra != null)
					{
						upaChetra.UpaChettra = model.UpaChettra;
						upaChetra.KharchaSirshark = model.KharchaSirshark;
						upaChetra.ChettraId = model.ChettraId;
						upaChetra.IsDeleted = false;
						_context.Entry(upaChetra).State = EntityState.Modified;
					}
					else
					{
						upaChetra = await _context.UpaChetra.FirstOrDefaultAsync(x => x.ChettraId == model.ChettraId && x.UpaChettra.Trim().Equals(model.UpaChettra));
						if (upaChetra != null)
						{
							upaChetra.IsDeleted = false;
							_context.Entry(upaChetra).State = EntityState.Modified;
						}
						else
						{
							upaChetra = new UpaChetra()
							{
								UpaChettra = model.UpaChettra,
								KharchaSirshark = model.KharchaSirshark,
								ChettraId = model.ChettraId,
								IsDeleted = false,
							};
							await _context.UpaChetra.AddAsync(upaChetra);
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
		public async Task<bool> DeleteUpaChetraById(int id)
		{
			var data = await _context.UpaChetra.Where(x => x.UpaChettraId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}


		//UpaChetraDetail
		public async Task<List<UpaChetraDetailViewModel>> GetAllUpaChetraDetail()
		{
			return await _context.UpaChetraDetail.Where(x=> x.IsDeleted == false)
			 .Select(x => new UpaChetraDetailViewModel()
			 {
				 UpaChetraDetailId = x.UpaChetraDetailId,
				 UpaChetraName = x.UpaChetra.UpaChettra,
				 UpaChetraId = x.UpaChetraId,
				 Name = x.Name,
				 IsDeleted = x.IsDeleted,
				 WardNumber=x.WardNumber
			 }).Where(x => x.IsDeleted == false).ToListAsync();
		}
		public async Task<UpaChetraDetailViewModel> GetUpaChetraDetailById(int id)
		{
			return await _context.UpaChetraDetail.Where(x => x.UpaChetraDetailId == id)
					 .Select(x => new UpaChetraDetailViewModel()
					 {
						 UpaChetraDetailId = x.UpaChetraDetailId,
						 UpaChetraId = x.UpaChetraId,
						 Name = x.Name,
						 IsDeleted = x.IsDeleted,
						 WardNumber=x.WardNumber
					 }).FirstOrDefaultAsync() ?? new UpaChetraDetailViewModel();
		}
		public async Task<bool> InsertUpdateUpaChetraDetail(UpaChetraDetailViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var upaChetraDetail = await _context.UpaChetraDetail.FirstOrDefaultAsync(x => x.UpaChetraDetailId == model.UpaChetraDetailId);
                    if (upaChetraDetail != null)
                    {
                        upaChetraDetail.UpaChetraId = model.UpaChetraId;
                        upaChetraDetail.Name = model.Name;
						upaChetraDetail.WardNumber = model.WardNumber;

                        _context.Entry(upaChetraDetail).State = EntityState.Modified;
                    }
                    else
                    {
                        upaChetraDetail = await _context.UpaChetraDetail.FirstOrDefaultAsync(x => x.UpaChetraId == model.UpaChetraId && x.Name.Trim().Equals(model.Name));
                        if (upaChetraDetail != null)
                        {
                            upaChetraDetail.IsDeleted = false;
                            _context.Entry(upaChetraDetail).State = EntityState.Modified;
                        }
                        else
                        {
                            upaChetraDetail = new UpaChetraDetail()
                            {
                                UpaChetraId = model.UpaChetraId,
                                Name = model.Name,
                                IsDeleted = false,
								WardNumber=model.WardNumber
                            };
                            await _context.UpaChetraDetail.AddAsync(upaChetraDetail);
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
        public async Task<bool> DeleteUpaChetraDetailById(int id)
		{
			var data = await _context.UpaChetraDetail.Where(x => x.UpaChetraDetailId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;

				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}
		#endregion

		#region YojanaSetup
		public async Task<List<YojanaSetupViewModel>> GetAllYojanaSetup()
		{
			int wardId = await _utility.GetWardNoForLogin_Role_User() ?? 0;
			var data = await _context.YojanaSetup.Where(x => x.IsDeleted == false && (wardId == 0 || x.WardId == wardId))
				.Select(x => new YojanaSetupViewModel()
			{
				YojanaSetupId = x.YojanaSetupId,
				YojanaName = x.YojanaName,
				EstimatedAmount = x.EstimatedAmount,
				IsDeleted = x.IsDeleted,
				WardId = x.WardId,
				Status=x.Status,
				FiscalYearId = x.FiscalYearId,
				SarkarBudget = x.SarkarBudget,
				UpabhoktaBudget = x.UpabhoktaBudget,
				OtherBudget = x.OtherBudget,
				WardName = _context.Ward.Where(w => w.Id == x.WardId).Select(w => w.Name).FirstOrDefault(),
				//BudgetTypeId = x.BudgetTypeId,
				//BudgetSubTypeId = x.BudgetSubTypeId,
				//Amount = x.Amount,
				//RemainingBudget = x.RemainingBudget,
				//BudgetTypeName = _context.BudgetType.Where(y => y.BudgetTypeId == x.BudgetTypeId).Select(y => y.BudgetTypeName).FirstOrDefault(),
				//BudgetSubTypeName = _context.BudgetSubType.Where(y => y.Id == x.BudgetSubTypeId).Select(y => y.BudgetSubTypeName).FirstOrDefault(),
			}).ToListAsync() ?? new List<YojanaSetupViewModel>();
			return data;
		}

		public async Task<YojanaSetupViewModel> GetYojanaSetupById(int? id)
		{
			var data = await _context.YojanaSetup.Where(x => x.YojanaSetupId == id).Select(x => new YojanaSetupViewModel()
			{
				YojanaSetupId = x.YojanaSetupId,
				YojanaName = x.YojanaName,
				EstimatedAmount = x.EstimatedAmount,
				IsDeleted = x.IsDeleted,
				WardId = x.WardId,

				FiscalYearId = x.FiscalYearId,
				SarkarBudget = x.SarkarBudget,
				UpabhoktaBudget = x.UpabhoktaBudget,
				OtherBudget = x.OtherBudget,
				//RemainingBudget = x.RemainingBudget,
				//BudgetTypeId = x.BudgetTypeId,
				//BudgetSubTypeId = x.BudgetSubTypeId,
				//Amount = x.Amount,
				//BudgetTypeName = _context.BudgetType.Where(y => y.BudgetTypeId == x.BudgetTypeId).Select(y => y.BudgetTypeName).FirstOrDefault(),
				//BudgetSubTypeName = _context.BudgetSubType.Where(y => y.Id == x.BudgetSubTypeId).Select(y => y.BudgetSubTypeName).FirstOrDefault(),
				////samiti = _context.UpabhoktaSamitiDetail.Where(y=>y.UpabhoktaSamitiDetailId==x.UpabhoktaSamitiDetailId).Select(y=> new UpavoktaSamitiDetailViewModel()
				//{

				//}).FirstOrDefault(),

			}).FirstOrDefaultAsync() ?? new YojanaSetupViewModel();
			return data;
		}

		public async Task<bool> InsertUpdateYojanaSetup(YojanaSetupViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				//int fiscalYearId = await _utility.GetCurrentFiscalYear();
				try
				{
					if (model.YojanaSetupId > 0)
					{
						var update = _context.YojanaSetup.Find(model.YojanaSetupId);
						if (update != null)
						{
							update.YojanaSetupId = model.YojanaSetupId;
							update.YojanaName = model.YojanaName;
							update.EstimatedAmount = model.EstimatedAmount;
							update.WardId = model.WardId;
							update.SarkarBudget = model.SarkarBudget;
							update.UpabhoktaBudget = model.UpabhoktaBudget;
							update.OtherBudget = model.OtherBudget;
							//update.BudgetTypeId = model.BudgetTypeId;
							//update.BudgetSubTypeId = model.BudgetSubTypeId;
							//update.Amount = model.Amount;
							//update.RemainingBudget = model.RemainingBudget;
							//update.FiscalYearId = model.FiscalYearId();
							update.Status = "Created";
							_context.Entry(update).State = EntityState.Modified;
						}
						else { return false; }
					}
					else
					{

						var data = new YojanaSetup()
						{
							//FiscalYearId = fiscalYearId,
							YojanaSetupId = model.YojanaSetupId,
							YojanaName = model.YojanaName,
							EstimatedAmount = model.EstimatedAmount,
							IsDeleted = false,
							WardId = model.WardId,
							SarkarBudget = model.SarkarBudget,
							UpabhoktaBudget = model.UpabhoktaBudget,
							OtherBudget = model.OtherBudget,
							Status="Created"
							//BudgetTypeId = model.BudgetTypeId,
							//BudgetSubTypeId = model.BudgetSubTypeId,
							//Amount = model.Amount,
							//RemainingBudget = model.RemainingBudget,

						};
						await _context.YojanaSetup.AddAsync(data);
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

		public async Task<bool> DeleteYojanaSetup(int id)
		{
			var data = await _context.YojanaSetup.Where(x => x.YojanaSetupId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}

		public async Task<int> InsertUpdateExcelYojana(IFormFile excelFile)
		{

			var result = false;
			int dataCount = 0;
			var data = new List<YojanaSetupViewModel>();
			var model = new YojanaSetupViewModel();
			List<YojanaSetupViewModel> list = new List<YojanaSetupViewModel>();

			if (excelFile != null && excelFile.Length > 0)
			{
				//var resultSet = _utility.UploadImgReturnPathAndName("Planning", excelFile, "Planning-Yojana").Result.FilePath;
				using (var stream = new MemoryStream())
				{
					await excelFile.CopyToAsync(stream);
					ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
					using (var package = new ExcelPackage(stream))
					{
						ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
						ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
						var cellValue = worksheet.Cells["A1"].Value.ToString();
						var rowCount = worksheet.Dimension.End.Row;

						for (int row = 2; row <= rowCount; row++)
						{
							var amount = "";
							var ward = "";
							var kharchaSirshak = "";
							var Ndata = (worksheet.Cells[row, 6].Text.Trim());
							var checkNumber = _utility.CheckNumber(Ndata.ToString());
							if (checkNumber == true)
							{
								amount = _utility.ConvertNepaliToEnglish(worksheet.Cells[row, 6].Text.Trim());

							}
							else
							{
								amount = worksheet.Cells[row, 6].Text.Trim();
							}
							if (_utility.CheckNumber(worksheet.Cells[row, 2].Text.Trim()) == true)
							{
								ward = _utility.ConvertNepaliToEnglish(worksheet.Cells[row, 2].Text.Trim());

							}
							else
							{
								ward = worksheet.Cells[row, 2].Text.Trim();
							}
							if (_utility.CheckNumber(worksheet.Cells[row, 4].Text.Trim()) == true)
							{

								kharchaSirshak = _utility.ConvertNepaliToEnglish(worksheet.Cells[row, 4].Text.Trim());
							}
							else
							{
								kharchaSirshak = worksheet.Cells[row, 2].Text.Trim();
							}
							YojanaSetupViewModel emp = new YojanaSetupViewModel();
							emp.YojanaName = worksheet.Cells[row, 1].Text.Trim();
							emp.WardId = Convert.ToInt32(ward);
							emp.UpaChhetra = worksheet.Cells[row, 3].Text.Trim();
							emp.Kharcha_Sirshak = kharchaSirshak;
							emp.Shrot = worksheet.Cells[row, 5].Text.Trim();
							emp.EstimatedAmount = Convert.ToDecimal(amount);
							list.Add(emp);
						}
						data = list ?? new List<YojanaSetupViewModel>();
					}
				}
				//Insert Data
				if (data.Count > 0)
				{
					foreach (var item in data)
					{
						result = await InsertExcelData(item);
						dataCount= result == true ? dataCount+1: dataCount;
					}
				}
			}

			return dataCount;

		}

		public async Task<bool> InsertExcelData(YojanaSetupViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					Nullable<int> shrotId = null;
					Nullable<int> upaChhetraId = null;
					if (model.UpaChhetra != null && model.UpaChhetra != "")
					{
						upaChhetraId = CheckAndInsertUpaChetra(model.UpaChhetra);
					}
					if (model.Shrot != null && model.Shrot != "")
					{
						shrotId =  CheckAndInsertShrot(model.Shrot);
					}
					var checkYojanaName = _context.YojanaSetup.Where(z=>z.YojanaName == model.YojanaName.Trim()).FirstOrDefault();
					if (checkYojanaName == null)
					{
						var data = new YojanaSetup()
						{
							BudgetTypeId = model.BudgetTypeId,
							BudgetSubTypeId = model.BudgetSubTypeId,
							YojanaName = model.YojanaName.Trim(),
							Amount = model.EstimatedAmount,
							EstimatedAmount = model.EstimatedAmount,
							IsDeleted = false,
							WardId = model.WardId,
							KharchaShirsak = model.Kharcha_Sirshak,
							//UpabhoktaSamitiDetailId = model.UpabhoktaSamitiDetailId,
							//	RemainingBudget = model.RemainingBudget,
							SarkarBudget = model.SarkarBudget,
							UpabhoktaBudget = model.UpabhoktaBudget,
							OtherBudget = model.OtherBudget,
							FiscalYearId = await _utility.GetCurrentFiscalYear(),
							ShrotId = shrotId,
							UpaChhetraId = upaChhetraId,
							CreatedBy = _userId,
							CreatedDate = DateTime.Now,
							Status="Created"
						};
						await _context.YojanaSetup.AddAsync(data);
						await _context.SaveChangesAsync();
						await transaction.CommitAsync();
						return true;
					}
					else 
					{
						return false;
					}
					
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return false;
				}

			}
		}

		public int CheckAndInsertUpaChetra(string chettraName)
		{
			//using (var transaction = _context.Database.BeginTransaction())
			//{
			var id = 0;
			try
			{
				var workArea = _context.UpaChetra.FirstOrDefault(x => x.UpaChettra.Trim() == chettraName.Trim());
				if (workArea != null)
				{
					return workArea.UpaChettraId;
				}
				else
				{
					var data = new UpaChetra()
					{
						UpaChettra = chettraName.Trim(),
						ChettraId = null,
						CreatedBy = _userId,
						CreatedDate = DateTime.Now,
						IsDeleted = false,
					};
					_context.UpaChetra.Add(data);
					_context.SaveChanges();
					id = data.UpaChettraId;
				}
				
				//   transaction.CommitAsync();
				return id;
			}
			catch (Exception ex)
			{
				//   transaction.RollbackAsync();
				return id;
			}
			//}
		}

		public int CheckAndInsertShrot(string shrotName)
		{
			//using (var transaction = _context.Database.BeginTransaction())
			//{
				var id = 0;
				try
				{
					var shrot = _context.BudgetSource.FirstOrDefault(x => x.BudgetSourceName.Trim() == shrotName.Trim());
					if (shrot != null)
					{
						return shrot.BudgetSourceId;
					}
					else
					{
						var data = new BudgetSource()
						{
							BudgetSourceName = shrotName.Trim(),
							CreatedBy = _userId,
							CreatedDate = DateTime.Now,
							IsDeleted = false,
						};
					    _context.BudgetSource.Add(data);
					   _context.SaveChanges();
					   id = data.BudgetSourceId;
					
				}
					 
					// transaction.Commit();
					return id;
				}
				catch (Exception ex)
				{
				 //   transaction.Rollback();
					return id;
				}
			//}
		}
		#endregion


		#region Sarta
		public async Task<List<SartaSetupViewModel>> GetAllSarta()
		{
			return await _context.SartaSetup.Where(x => x.Status == true)
				.Select(x => new SartaSetupViewModel()
				{
					SartaSetupId = x.SartaSetupId,
					Name = x.Name,
					Description = x.Description,
					Status = x.Status,
				}).ToListAsync();
		}
		public async Task<SartaSetupViewModel> GetSartaById(int id)
		{
			return await _context.SartaSetup.Where(x => x.SartaSetupId == id)
				.Select(x => new SartaSetupViewModel()
				{
					SartaSetupId = x.SartaSetupId,
					Name = x.Name,
					Description = x.Description,
					Status = x.Status,
				}).FirstOrDefaultAsync() ?? new SartaSetupViewModel();
		}
		public async Task<bool> InsertUpdateSarta(SartaSetupViewModel model)
		{
			try
			{
				if (model.SartaSetupId > 0)
				{
					var sarta = await _context.SartaSetup.Where(x => x.SartaSetupId == model.SartaSetupId).FirstOrDefaultAsync();
					if (sarta != null)
					{
						sarta.Name = model.Name;
						sarta.Description = model.Description;
						sarta.UpdatedBy = _userId;
						sarta.UpdatedDate = DateTime.Now;
						_context.Entry(sarta).State = EntityState.Modified;
						await _context.SaveChangesAsync();
					}
				}
				else
				{
					var sartanew = new SartaSetup()
					{
						Name = model.Name,
						Status = true,
						Description = model.Description,
						CreatedBy = _userId,
						CreatedDate = DateTime.Now,
					};
					await _context.SartaSetup.AddAsync(sartanew);
					await _context.SaveChangesAsync();
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> DeleteSartaById(int id)
		{
			var sarta = await _context.SartaSetup.Where(x => x.SartaSetupId == id).FirstOrDefaultAsync();
			if (sarta != null)
			{
				sarta.Status = false;

				_context.Entry(sarta).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		#endregion

		#region Karkkatti
		public async Task<List<KarKattiViewModel>> GetAllKarkatti()
        {
            var fiscalId = await _utility.GetCurrentFiscalYear();
            return await _context.KarKatti.Where(x=> x.FiscalYearId == fiscalId)
			 .Select(x => new KarKattiViewModel()
			 {
				 KarKattiId = x.KarKattiId,
				 AgrimShulka = x.AgrimShulka,
				 BahalKar = x.BahalKar,
				 Dhuwani = x.Dhuwani,
				 MarmatSambhar = x.MarmatSambhar,
				 Parishramik = x.Parishramik,
				 SamajikSurekchya = x.SamajikSurekchya,
				 Royality = x.Royality,
				 Contigency = x.Contigency,
				 Status = x.Status,
			 }).Where(x => x.Status == true).ToListAsync() ?? new List<KarKattiViewModel>();
		}
		public async Task<KarKattiViewModel> GetKarkatttiById(int id)
        {
            var fiscalId = await _utility.GetCurrentFiscalYear();
            var data = await _context.KarKatti.Where(x => x.FiscalYearId == fiscalId)
             .Select(x => new KarKattiViewModel()
			 {
				 KarKattiId = x.KarKattiId,
				 AgrimShulka = x.AgrimShulka,
				 BahalKar = x.BahalKar,
				 Dhuwani = x.Dhuwani,
				 MarmatSambhar = x.MarmatSambhar,
				 Parishramik = x.Parishramik,
				 SamajikSurekchya = x.SamajikSurekchya,
				 Royality = x.Royality,
				 Contigency = x.Contigency,
				 Status = x.Status,
			 }).Where(x => x.Status == true).FirstOrDefaultAsync() ?? new KarKattiViewModel();
			return data;
		}
		public async Task<bool> InsertUpdateKarkatti(KarKattiViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var detail = await _context.KarKatti.FirstOrDefaultAsync(x => x.KarKattiId == model.KarKattiId);
                    if (detail != null)
                    {
                        detail.KarKattiId = model.KarKattiId;
                        detail.AgrimShulka = model.AgrimShulka;
                        detail.BahalKar = model.BahalKar;
                        detail.Dhuwani = model.Dhuwani;
                        detail.MarmatSambhar = model.MarmatSambhar;
                        detail.Parishramik = model.Parishramik;
                        detail.SamajikSurekchya = model.SamajikSurekchya;
                        detail.Royality = model.Royality;
                        detail.Contigency = model.Contigency;
                        detail.Status = true;
                        _context.Entry(detail).State = EntityState.Modified;
                    }
                    else
                    {
						var fiscalId = await _utility.GetCurrentFiscalYear();
                        var kar = new KarKatti()
                        {
                            AgrimShulka = model.AgrimShulka,
                            BahalKar = model.BahalKar,
                            Dhuwani = model.Dhuwani,
                            MarmatSambhar = model.MarmatSambhar,
                            Parishramik = model.Parishramik,
                            SamajikSurekchya = model.SamajikSurekchya,
                            Royality = model.Royality,
                            Contigency = model.Contigency,
                            Status = true,
							FiscalYearId = fiscalId,
                        };
                        await _context.KarKatti.AddAsync(kar);
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
        #endregion Karkatti

        #region DocumentType
        public async Task<bool> DeleteDocumentTypeById(int id)
		{
			var data = await _context.DocumentType.Where(x => x.DocumentTypeId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.IsDeleted = true;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}
		public async Task<List<DocumentTypeViewModel>> GetAllDocumentType()
		{
			return await _context.DocumentType
			 .Select(x => new DocumentTypeViewModel()
			 {
				 DocumentTypeId = x.DocumentTypeId,
				 DocumentTypeName = x.DocumentTypeName,
				 IsDeleted = x.IsDeleted,
			 }).Where(x => x.IsDeleted == false).ToListAsync() ?? new List<DocumentTypeViewModel>();
		}
		public async Task<DocumentTypeViewModel> GetDocumentTypeById(int id)
		{
			return await _context.DocumentType.Where(x => x.DocumentTypeId == id)
			 .Select(x => new DocumentTypeViewModel()
			 {
				 DocumentTypeId = x.DocumentTypeId,
				 DocumentTypeName = x.DocumentTypeName,
				 IsDeleted = x.IsDeleted,

			 }).Where(x => x.IsDeleted == false).FirstOrDefaultAsync() ?? new DocumentTypeViewModel();
		}
		public async Task<bool> InsertUpdateDocumentType(DocumentTypeViewModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (model.DocumentTypeId > 0)
					{
						var detail = await _context.DocumentType.FirstOrDefaultAsync(x => x.DocumentTypeId == model.DocumentTypeId);
						if (detail != null)
						{
							detail.DocumentTypeName = model.DocumentTypeName;
							detail.IsDeleted = false;
							_context.Entry(detail).State = EntityState.Modified;
						}
						else
						{
							return false;
						}
					}
					else
					{
						var detail = new DocumentType()
						{
							DocumentTypeId = model.DocumentTypeId,
							DocumentTypeName = model.DocumentTypeName,
							IsDeleted = false

						};
						await _context.DocumentType.AddAsync(detail);
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

		//BhuktaniType
		public async Task<bool> DeleteBhuktaniTypeById(int id)
		{
			var data = await _context.BhuktaniType.Where(x => x.BhuktaniTypeId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.Status = false;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}
		#endregion

		#region Unit
		public async Task<List<UnitViewModel>> GetAllUnit()
		{
			return await _context.Unit.Where(x=> x.IsDeleted == false)
				.Select(x => new UnitViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					NameEng = x.NameEng,
				}).ToListAsync();
		}
		public async Task<UnitViewModel> GetUnitById(int id)
		{
			return await _context.Unit.Where(x => x.Id == id)
				.Select(x => new UnitViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					NameEng = x.NameEng,
				}).FirstOrDefaultAsync() ?? new UnitViewModel();
		}
		public async Task<bool> InsertUpdateUnit(UnitViewModel model)
		{
			try
			{
				var unit = await _context.Unit.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				if (unit != null)
				{
					unit.Name = model.Name;
					unit.NameEng = model.NameEng;
					_context.Entry(unit).State = EntityState.Modified;
				}
				else
                {
                    unit = await _context.Unit.FirstOrDefaultAsync(x => x.Name.Trim().Equals(model.Name));
                    if (unit != null)
                    {
                        unit.IsDeleted = false;
                        _context.Entry(unit).State = EntityState.Modified;
                    }
                    else
                    {
                        unit = new Unit()
                        {
                            Name = model.Name,
                            NameEng = model.NameEng,
                            IsDeleted = false,
                        };
                        await _context.Unit.AddAsync(unit);
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
        public async Task<bool> DeleteUnit(int id)
        {
            var data = await _context.Unit.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.IsDeleted = true;
                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        #endregion

        #region Bank
        public async Task<List<BankViewModel>> GetAllBank()
        {
            return await _context.Class_A_Bank_List.Where(x=> x.Status == true)
                .Select(x => new BankViewModel()
                {
                    Id = x.Class_A_Bank_List_Id,
                    BankName_Nep = x.BankName_Nep,
                    BankName_Eng = x.BankName_Eng,
                    Address = x.Address,
                }).ToListAsync();
        }
        public async Task<BankViewModel> GetBankById(int id)
        {
            return await _context.Class_A_Bank_List.Where(x => x.Class_A_Bank_List_Id == id)
                .Select(x => new BankViewModel()
                {
                    Id = x.Class_A_Bank_List_Id,
                    BankName_Nep = x.BankName_Nep,
                    BankName_Eng = x.BankName_Eng,
                    Address = x.Address,
                }).FirstOrDefaultAsync() ?? new BankViewModel();
        }
        public async Task<bool> InsertUpdateBank(BankViewModel model)
        {
            try
            {
                var Bank = await _context.Class_A_Bank_List.Where(x => x.Class_A_Bank_List_Id == model.Id).FirstOrDefaultAsync();
                if (Bank != null)
                {
                    Bank.BankName_Nep = model.BankName_Nep;
                    Bank.BankName_Eng = model.BankName_Eng;
                    Bank.Address = model.Address;
                    _context.Entry(Bank).State = EntityState.Modified;
                }
                else
                {
                    Bank = await _context.Class_A_Bank_List.FirstOrDefaultAsync(x => x.BankName_Nep.Trim().Equals(model.BankName_Nep));
                    if (Bank != null)
                    {
                        Bank.Status = true;
                        _context.Entry(Bank).State = EntityState.Modified;
                    }
                    else
                    {
                        Bank = new Class_A_Bank_List()
                        {
                            BankName_Nep = model.BankName_Nep,
                            BankName_Eng = model.BankName_Eng,
                            Address = model.Address,
                            Status = true,
                        };
                        await _context.Class_A_Bank_List.AddAsync(Bank);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Class_A_Bank_List Repo create/update Error User Id = " + _userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<bool> DeleteBank(int id)
        {
            var data = await _context.Class_A_Bank_List.Where(x => x.Class_A_Bank_List_Id == id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Status = false;

                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        #endregion

    }
}