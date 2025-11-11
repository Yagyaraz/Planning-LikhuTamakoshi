using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Repositories;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Security.Claims;

namespace PlanningCore.Areas.Contract.Repository
{
	public class ConsultantRepositories : IConsultant
	{
		private readonly PlanningContext context;
		private readonly ILogger<CommonRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public ConsultantRepositories(PlanningContext _context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
		{
			context = _context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			utility = _utility;
		}
		public async Task<List<ConsultantViewModel>> GetAllConsultant()
		{
			var data = await context.Con_Consultant.Where(x => x.Status == true)
				.Select(x => new ConsultantViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					NameEng = x.NameEng,
					MobileNumber = x.MobileNumber,
					EMail = x.EMail,
					MainPersonName = x.MainPersonName,
					MainPersonPhoneNumber = x.MainPersonPhoneNumber,
					MainPersonAddress = x.MainPersonAddress,
					PanNumber = x.PanNumber,
					Registration_Number = x.Registration_Number,
					Registration_Date = x.Registration_Date,
					VatEndDate = x.VatEndDate,
					EjajatNumber = x.EjajatNumber,
					EjajatType = x.EjajatType,
					StateId = x.StateId,
					DistrictId = x.DistrictId,
					PalikaId = x.PalikaId,
					IsAllowToDelete = (context.Con_BidSecurity.Any(b => b.ConsultantId == x.Id) || context.Con_BankGuarantee.Any(b => b.ConsultantId == x.Id) || context.Con_Samjhauta.Any(b => b.ConsultantId == x.Id)),
					JointVentureList = context.Con_JV.Where(y => y.ConsultantId == x.Id).
					Select(y => new JointVentureViewModel()
					{
						Id = y.Id,
						Name = y.Name,
						NameEng = y.NameEng,
						MobileNumber = y.MobileNumber,
						EMail = y.EMail,
						MainPersonName = y.MainPersonName,
						MainPersonPhoneNumber = y.MainPersonPhoneNumber,
						MainPersonAddress = y.MainPersonAddress,
						PanNumber = y.PanNumber,
						Registration_Number = y.Registration_Number,
						Registration_Date = y.Registration_Date,
						VatEndDate = y.VatEndDate,
						EjajatNumber = y.EjajatNumber,
						EjajatType = y.EjajatType,
						StateId = y.StateId,
						DistrictId = y.DistrictId,
						PalikaId = y.PalikaId,
					}).ToList(),
				}).ToListAsync();
			return data;
		}
		public async Task<ConsultantViewModel> GetConsultantById(int id)
		{
			var data = context.Con_Consultant.Where(x => x.Id == id)
				.Select(x => new ConsultantViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					NameEng = x.NameEng,
					MobileNumber = x.MobileNumber,
					EMail = x.EMail,
					MainPersonName = x.MainPersonName,
					MainPersonPhoneNumber = x.MainPersonPhoneNumber,
					MainPersonAddress = x.MainPersonAddress,
					PanNumber = x.PanNumber,
					Registration_Number = x.Registration_Number,
					Registration_Date = x.Registration_Date,
					VatEndDate = x.VatEndDate,
					EjajatNumber = x.EjajatNumber,
					EjajatType = x.EjajatType,
					StateId = x.StateId,
					DistrictId = x.DistrictId,
					PalikaId = x.PalikaId,
					IsJV = x.IsJV,
					JointVentureList = context.Con_JV.Where(y => y.ConsultantId == x.Id)
					.Select(y => new JointVentureViewModel()
					{
						Id = y.Id,
						Name = y.Name,
						NameEng = y.NameEng,
						MobileNumber = y.MobileNumber,
						EMail = y.EMail,
						MainPersonName = y.MainPersonName,
						MainPersonPhoneNumber = y.MainPersonPhoneNumber,
						MainPersonAddress = y.MainPersonAddress,
						PanNumber = y.PanNumber,
						Registration_Number = y.Registration_Number,
						Registration_Date = y.Registration_Date,
						VatEndDate = y.VatEndDate,
						EjajatNumber = y.EjajatNumber,
						EjajatType = y.EjajatType,
						StateId = y.StateId,
						DistrictId = y.DistrictId,
						PalikaId = y.PalikaId,
					}).ToList(),
				}).FirstOrDefaultAsync();
			return await data ?? new ConsultantViewModel();
		}
		public async Task<bool> InsertConsultant(ConsultantViewModel model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var data = context.Con_Consultant.Where(x => x.Id == model.Id).FirstOrDefault();
					if (data != null)
					{
						data.Name = model.Name;
						data.NameEng = model.NameEng;
						data.MobileNumber = model.MobileNumber;
						data.EMail = model.EMail;
						data.MainPersonName = model.MainPersonName;
						data.MainPersonPhoneNumber = model.MainPersonPhoneNumber;
						data.MainPersonAddress = model.MainPersonAddress;
						data.PanNumber = model.PanNumber;
						data.Registration_Number = model.Registration_Number;
						data.Registration_Date = model.Registration_Date;
						data.VatEndDate = model.VatEndDate;
						data.EjajatNumber = model.EjajatNumber;
						data.EjajatType = model.EjajatType;
						data.StateId = model.StateId;
						data.DistrictId = model.DistrictId;
						data.PalikaId = model.PalikaId;
						data.IsJV = model.IsJV;
						context.Entry(data).State = EntityState.Modified;
						await context.SaveChangesAsync();

						if (model.JointVentureList.Count > 0)
						{
							foreach (var item in await context.Con_JV.Where(x => x.ConsultantId == model.Id).ToListAsync())
							{
								if (!model.JointVentureList.Any(x => x.Id == item.Id))
								{
									context.Con_JV.Remove(item);
									await context.SaveChangesAsync();
								}
							}

							foreach (var item in model.JointVentureList)
							{
								var JVdata = await context.Con_JV.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
								if (JVdata != null)
								{
									JVdata.ConsultantId = model.Id;
									JVdata.Name = item.Name;
									JVdata.NameEng = item.NameEng;
									JVdata.MobileNumber = item.MobileNumber;
									JVdata.EMail = item.EMail;
									JVdata.MainPersonName = item.MainPersonName;
									JVdata.MainPersonPhoneNumber = item.MainPersonPhoneNumber;
									JVdata.MainPersonAddress = item.MainPersonAddress;
									JVdata.PanNumber = item.PanNumber;
									JVdata.Registration_Number = item.Registration_Number;
									JVdata.Registration_Date = item.Registration_Date;
									JVdata.VatEndDate = item.VatEndDate;
									JVdata.EjajatNumber = item.EjajatNumber;
									JVdata.EjajatType = item.EjajatType;
									JVdata.StateId = item.StateId;
									JVdata.DistrictId = item.DistrictId;
									JVdata.PalikaId = item.PalikaId;
									context.Entry(JVdata).State = EntityState.Modified;
									await context.SaveChangesAsync();
								}
								else
								{
									var joint = new Con_JV()
									{
										ConsultantId = model.Id,
										Name = item.Name,
										NameEng = item.NameEng,
										MobileNumber = item.MobileNumber,
										EMail = item.EMail,
										MainPersonName = item.MainPersonName,
										MainPersonPhoneNumber = item.MainPersonPhoneNumber,
										MainPersonAddress = item.MainPersonAddress,
										PanNumber = item.PanNumber,
										Registration_Number = item.Registration_Number,
										Registration_Date = item.Registration_Date,
										VatEndDate = item.VatEndDate,
										EjajatNumber = item.EjajatNumber,
										EjajatType = item.EjajatType,
										StateId = item.StateId,
										DistrictId = item.DistrictId,
										PalikaId = item.PalikaId,

									};
									await context.Con_JV.AddAsync(joint);
									context.SaveChanges();
								}
							}

						}
					}
					else
					{
						data = new Con_Consultant()
						{
							Name = model.Name,
							NameEng = model.NameEng,
							MobileNumber = model.MobileNumber,
							EMail = model.EMail,
							MainPersonName = model.MainPersonName,
							MainPersonPhoneNumber = model.MainPersonPhoneNumber,
							MainPersonAddress = model.MainPersonAddress,
							PanNumber = model.PanNumber,
							Registration_Number = model.Registration_Number,
							Registration_Date = model.Registration_Date,
							VatEndDate = model.VatEndDate,
							EjajatNumber = model.EjajatNumber,
							EjajatType = model.EjajatType,
							StateId = model.StateId,
							DistrictId = model.DistrictId,
							PalikaId = model.PalikaId,
							Status = true,
							IsJV = model.IsJV,
						};
						await context.Con_Consultant.AddAsync(data);
						context.SaveChanges();

						if (model.JointVentureList.Count > 0)
						{
							foreach (var item in model.JointVentureList)
							{
								var joint = new Con_JV()
								{
									ConsultantId = data.Id,
									Name = item.Name,
									NameEng = item.NameEng,
									EMail = item.EMail,
									MainPersonName = item.MainPersonName,
									MainPersonPhoneNumber = item.MainPersonPhoneNumber,
									MainPersonAddress = item.MainPersonAddress,
									PanNumber = item.PanNumber,
									Registration_Number = item.Registration_Number,
									Registration_Date = item.Registration_Date,
									VatEndDate = item.VatEndDate,
									EjajatNumber = item.EjajatNumber,
									EjajatType = item.EjajatType,
									StateId = item.StateId,
									DistrictId = item.DistrictId,
									PalikaId = item.PalikaId,

								};
								await context.Con_JV.AddAsync(joint);
								context.SaveChanges();

							}
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
		public async Task<bool> DeleteConsultant(int id)
		{
			var data = await context.Con_Consultant.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.Status = false;
				data.DeletedBy = _userId;
				data.DeletedDate = DateTime.Now;
				context.Entry(data).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
