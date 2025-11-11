using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanningCore.Areas.Admin.Repositories
{
	public class BhuktaniRepositories : IBhuktani
	{
		private readonly PlanningContext _context;
		private readonly ILogger<CommonRepository> _logger;
		private readonly string _userId = null;
		private readonly IUtility _utility;
		public BhuktaniRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
		{
			_context = context;
			_logger = logger;
			_userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			utility = _utility;
		}

		public async Task<bool> DeletePlanningBhuktani(int id)
		{
			var data = await _context.PlanningBhuktani.Where(model => model.PlanningBhuktaniId == id).FirstOrDefaultAsync();
			if (data != null)
			{
				data.Status = false;
				_context.Entry(data).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			else { return false; }
		}

		public async Task<PlanningBhuktaniViewModel> GetPlanningBhuktaniByBhuktaniId(int? pid,int?id)
		{			
			var PlanningSamjautaId = _context.PlanningBhuktani.Where(x => x.PlanningBhuktaniId == id).Select(x => x.PlanningSamjhautaId).FirstOrDefault();
            var samitiid = _context.PlanningSamjhauta.Where(x => x.PlanningSamjhautaId == PlanningSamjautaId).Select(x => x.SamitiDetailId).FirstOrDefault();
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
				UpaBhoktaSamitiAccNumber=model.UpaBhoktaSamitiAccNumber,
				Technical_Amount = model.Technical_Amount,
				Kantigenci = model.Kantigenci,
				Remaining_Bhuktani_Amount = model.Remaining_Bhuktani_Amount,
				Agrim_Shulka = model.Agrim_Shulka,
				Bahal_Kar = model.Bahal_Kar,
				MarmatShmar = model.MarmatShmar,
				Katti_Rakam = model.Katti_Rakam,
				Aanya_Raaya = model.Aanya_Raaya,
				AdakshyaName = model.AdakshyaName,
                KaryasampannaAnushar = model.KaryasampannaAnushar,
				SamjhautaDate = model.SamjhautaDate,
				FarfarakDate = model.FarfarakDate,
				Farchot_Amount = model.Farchot_Amount,
				Running_Bhuktani = model.Running_Bhuktani,
				Samajik_Surekchya = model.Samajik_Surekchya,
				Parishramik = model.Parishramik,
				Dhuwani = model.Dhuwani,
				Royality = model.Royality,
				PlanningSamjhautaId = model.PlanningSamjhautaId,
				BankName=_context.UpabhoktaSamitiDetail.Where(x=>x.UpabhoktaSamitiDetailId==samitiid).Select(x=>x.BankName).FirstOrDefault(),
                //AccountNumber = _context.UpabhoktaSamitiDetail.Where(x=>x.UpabhoktaSamitiDetailId==samitiid).Select(x=>x.AccountNumber).FirstOrDefault(),
                AccountNumber = model.UpaBhoktaSamitiAccNumber,
				BhuktaniTypeId = model.BhuktaniTypeId,
				IsBhuktaniApproval = model.IsBhuktaniApproval,		
				Status = model.Status,
				Address =string.Join(",<br/>", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == samitiid).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Code).FirstOrDefault()).ToList()),
				KaryasampannaDate = _context.ProjectEntryDetail.Where(x => x.PlanningSamjhautaId == PlanningSamjautaId).Select(x => x.Project_End_Date).FirstOrDefault(),
				FiscalYearName = _context.FiscalYear.Where(x => x.Id == model.FiscalYearId).Select(x => x.Name).FirstOrDefault()
			}).FirstOrDefaultAsync() ?? new PlanningBhuktaniViewModel();
			return data ?? new PlanningBhuktaniViewModel();
		}

		public async Task<PlanningBhuktaniViewModel> GetPlanningBhuktaniListByPlanningSamjhautaId(int id)
		{
			var model = new PlanningBhuktaniViewModel();
			model.PlanningBhuktaniList = await _context.PlanningBhuktani.Where(model => model.PlanningSamjhautaId == id && model.Status == true)
				.Select(model => new PlanningBhuktaniViewModel()
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
					FarfarakDate = model.FarfarakDate,
					Farchot_Amount = model.Farchot_Amount,
					Running_Bhuktani = model.Running_Bhuktani,
					Samajik_Surekchya = model.Samajik_Surekchya,
					Parishramik = model.Parishramik,
					Dhuwani = model.Dhuwani,
					Royality = model.Royality,
                    KaryasampannaAnushar = model.KaryasampannaAnushar,
					PlanningSamjhautaId = model.PlanningSamjhautaId,
					BhuktaniTypeId = model.BhuktaniTypeId,
					IsBhuktaniApproval = model.IsBhuktaniApproval,
					Status = model.Status,
					
				}).ToListAsync() ?? new List<PlanningBhuktaniViewModel>();
			return model;
		}


		public Task<List<PlanningBhuktaniViewModel>> GetBhuktaniListFormPlanningSamjhauta(int? id)
		{
			List<PlanningBhuktaniViewModel> data = (from p in _context.PlanningSamjhauta
													join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into prodetail
													from ps in prodetail.DefaultIfEmpty()
													join pe in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into pro_contextry
													from pe in pro_contextry.DefaultIfEmpty()
													//join pd in _context.UpabhoktaSamitiDetail on p.YojanaId equals pd.YojanaId into samiti
													//from pd in samiti.DefaultIfEmpty()
													join mp in _context.MunicipalitySamitiManjuriPatra on p.PlanningSamjhautaId equals mp.PlanningSamjhautaId into manjuri
													from mp in manjuri.DefaultIfEmpty()
													select new PlanningBhuktaniViewModel
													{
														PlanningSamjhautaId = p.PlanningSamjhautaId,
														FiscalYearId = p.FiscalYearId,
														//Aayojana_Karyakram = _context.YojanaSetup.Where(x => x.YojanaSetupId == p.YojanaId).Select(x => x.YojanaName).FirstOrDefault(),
														//Nirman_Upabhokta = _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == pd.UpabhoktaSamitiDetailId).Select(x => x.Name).FirstOrDefault(),

														SamjhautaDate = mp.Municipality_Manjuri_Date,

														//NaPa_Binayajit = ps.Municipality,
														NaPa_Binayajit = ps.Project_estimated_Amount,
														Jana_Sahabagita = ps.Loan_Grant,
														Kantigenci = p.Contegency_Amount,
														Status = p.IsDeleted,
														MarmatShmar = p.MarmatSambhar_Amount,
													}).ToList();
			//return data?? new List<PlanningBhuktaniViewModel>();
			return Task.FromResult(data);
		}
		public List<PlanningBhuktaniViewModel> GetPlanningKarKatti()
		{
			List<PlanningBhuktaniViewModel> list = _context.KarKatti.Select(x => new PlanningBhuktaniViewModel()
			{
				KarkattiId = x.KarKattiId,
				Kantigenci = x.Contigency,
				Samajik_Surekchya = x.SamajikSurekchya,
				Agrim_Shulka = x.AgrimShulka,
				Parishramik = x.Parishramik,
				Bahal_Kar = x.BahalKar,
				MarmatShmar = x.MarmatSambhar,
				Royality = x.Royality,
				Dhuwani = x.Dhuwani,
				Status = x.Status ?? false
			}).ToList();
			return list ?? new List<PlanningBhuktaniViewModel>();
		}


		public async Task<int> InsertUpdatePlanningBhuktani(PlanningBhuktaniViewModel model)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				var fiscalYearId = _context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefault();
				var SamjhautaId = 0;
				try
				{
					if (model.PlanningBhuktaniId > 0)
					{
						var data = await _context.PlanningBhuktani.Where(x => x.PlanningBhuktaniId == model.PlanningBhuktaniId).FirstOrDefaultAsync();
						if (data != null)
						{
							data.PlanningBhuktaniId = model.PlanningBhuktaniId;
							data.FiscalYearId = model.FiscalYearId;
							data.Nirman_Upabhokta = model.Nirman_Upabhokta;
							data.Aayojana_Karyakram = model.Aayojana_Karyakram;
							data.Kul_La_Ie = model.Kul_La_Ie;
							data.UpaBhoktaSamitiAccNumber = model.UpaBhoktaSamitiAccNumber;
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
							data.FarfarakDate = model.FarfarakDate;
							data.Farchot_Amount = model.Farchot_Amount;
							data.Running_Bhuktani = model.Running_Bhuktani;
							data.Samajik_Surekchya = model.Samajik_Surekchya;
							data.Parishramik = model.Parishramik;
							data.Dhuwani = model.Dhuwani;
							data.KaryasampannaAnushar = model.KaryasampannaAnushar;
							data.Royality = model.Royality;
							data.PlanningSamjhautaId = model.PlanningSamjhautaId;
							data.BhuktaniTypeId = model.BhuktaniTypeId;
							data.IsBhuktaniApproval = model.IsBhuktaniApproval;
							data.Status = true;
						}
						_context.Entry(data).State = EntityState.Modified;
					     SamjhautaId = model.PlanningSamjhautaId ?? 0;
                    }

					else
					{
						var bhuktani = new PlanningBhuktani()
						{
							PlanningSamjhautaId = model.PlanningSamjhautaId,
							PlanningBhuktaniId = model.PlanningBhuktaniId,
							FiscalYearId = fiscalYearId,
							Nirman_Upabhokta = model.Nirman_Upabhokta,
							Aayojana_Karyakram = model.Aayojana_Karyakram,
							Kul_La_Ie = model.Kul_La_Ie,
							NaPa_Binayajit = model.NaPa_Binayajit,
							UpaBhoktaSamitiAccNumber=model.UpaBhoktaSamitiAccNumber,
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
							FarfarakDate = model.FarfarakDate,
							Farchot_Amount = model.Farchot_Amount,
							Running_Bhuktani = model.Running_Bhuktani,
							Samajik_Surekchya = model.Samajik_Surekchya,
							Parishramik = model.Parishramik,
							KaryasampannaAnushar = model.KaryasampannaAnushar,
							Dhuwani = model.Dhuwani,
							Royality = model.Royality,
							BhuktaniTypeId = model.BhuktaniTypeId,
							IsBhuktaniApproval = model.IsBhuktaniApproval,
							Status = true,
							CreatedBy = _userId,
							CreatedDate = DateTime.Now,

						};
						await _context.PlanningBhuktani.AddAsync(bhuktani);
						SamjhautaId = bhuktani.PlanningSamjhautaId ?? 0;
						
					}

					await _context.SaveChangesAsync();					
					await transaction.CommitAsync();
					return SamjhautaId;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return 0;
				}
			}
		}

		public async Task<PlanningBhuktaniViewModel> GetBhuktaniFromPlanningSamjhauta(int id)
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
							  join upa in _context.UpabhoktaSamitiDetail on p.YojanaSetup.YojanaSetupId equals upa.UpabhoktaSamitiDetailId into upabhokta
							  from upa in upabhokta.DefaultIfEmpty()
							  join yojana in _context.YojanaSetup on p.YojanaSetup.YojanaSetupId equals yojana.YojanaSetupId into yojanas
							  from yojana in yojanas.DefaultIfEmpty()
							  where p.PlanningSamjhautaId == id
							  select new PlanningBhuktaniViewModel()
							  {
								  FiscalYearId = p.FiscalYearId,
								  SamjhautaDate = ms.Municipality_Manjuri_Date,
								  NaPa_Binayajit = ps.Project_estimated_Amount,
								  Kul_La_Ie = ps.Total_Amount_Source,
								  Jana_Sahabagita = ps.Loan_Grant,
								  Kantigenci = p.Contegency_Amount,
								  Status = p.IsDeleted,
								  MarmatShmar = p.MarmatSambhar_Amount,
                                  AdakshyaName = _context.UpabhoktaSamitiMemberDetail.Where(sn => sn.UpabhoktaSamitiMemberDetailId == o.RepresentativeNameId).Select(sn => sn.MemberName).FirstOrDefault(),
                                  Aayojana_Karyakram = _context.UpabhoktaSamitiDetailYojanas.Where(n=>n.UpabhoktaSamitiDetailId==p.SamitiDetailId).Select(n=>n.YojanaSetup.YojanaName).FirstOrDefault(),
                                  Nirman_Upabhokta = _context.UpabhoktaSamitiDetailYojanas.Where(n=>n.UpabhoktaSamitiDetailId==p.SamitiDetailId).Select(n=>n.YojanaSetup.YojanaName).FirstOrDefault() + "उपभोक्ता समिति ",
                              }).FirstOrDefaultAsync() ?? new PlanningBhuktaniViewModel();							
            return data ?? new PlanningBhuktaniViewModel();
		}
        public async Task<bool> DeleteBhuktaniById(int id)
        {
            var data = await _context.PlanningBhuktani.Where(x => x.PlanningBhuktaniId == id).FirstOrDefaultAsync();
            if (data != null)
            {
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

    }
}
