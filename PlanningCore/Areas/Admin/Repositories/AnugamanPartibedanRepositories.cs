using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Repositories
{
   
    
    public class AnugamanPartibedanRepositories : IAnugamanPartibedan
    {
        private readonly PlanningContext _context;
        private readonly IUtility _utility;

        public AnugamanPartibedanRepositories(IUtility utility,PlanningContext context)
        {
            _utility = utility;
            _context = context;
        }       
        public async Task<AnugamanPartibedanViewModel> GetAnugamanPartiBedan(int planningSamjhuataId)
        {
            var data = await _context.AnugamanPartibdeans
                .Where(x => x.PlanningSamjhautaId == planningSamjhuataId)
                .FirstOrDefaultAsync();

            if (data == null)
                return null;

            return new AnugamanPartibedanViewModel
            {
                Id = data.Id,
                PlanningSamjhautaId = data.PlanningSamjhautaId,
                ParikxanDate = data.ParikxanDate ?? string.Empty,
                SampannaType = data.SampannaType ?? string.Empty,
                BillRakam = data.BillRakam ?? string.Empty,
                IsNirnayaBhayako = data.IsNirnayaBhayako??false,
                IsBibad = data.IsBibad??false,
                IsGambirBibad = data.IsGambirBibad ?? false,
                BibadDetails = data.BibadDetails ?? string.Empty,
                BahiyaJokhim = data.BahiyaJokhim ?? string.Empty,
                IsJimmewari = data.IsJimmewari ?? false,
                JimmwariDetails = data.JimmwariDetails ?? string.Empty,
                IsBankAccountOppen = data.IsBankAccountOppen ?? false,
                BankAcocuntDetail = data.BankAcocuntDetail ?? string.Empty,
                BankName = data.BankName ?? string.Empty,
                BankAddress = data.BankAddress ?? string.Empty,
                AccountNumber = data.AccountNumber ?? string.Empty,
                AccountType = data.AccountType ?? string.Empty,
                AccountRemarks = data.AccountRemarks ?? string.Empty,
                PratakshyaFaida = data.PratakshyaFaida,
                GharDhuri = data.GharDhuri ?? string.Empty,
                PratakshyaFaidaDetails = data.PratakshyaFaidaDetails ?? string.Empty,
                IsSuchanaPati = data.IsSuchanaPati ?? false,
                IsAambhela = data.IsAambhela ?? false,
                EngineerName = data.EngineerName ?? string.Empty,
                BhuktaniSifarishRakam = data.BhuktaniSifarishRakam,
                KaryanayanDetails = data.KaryanayanDetails ?? string.Empty,
                SamasyaSamadhanUpaya = data.SamasyaSamadhanUpaya ?? string.Empty,
                AnugamanBibaran = data.AnugamanBibaran ?? string.Empty,
                BeforePhotos = data.BeforePhotos ?? string.Empty,
                AfterPhotos = data.AfterPhotos ?? string.Empty,
                BetweenPhotos = data.BetweenPhotos ?? string.Empty,

                KhataSanchalak1Name = data.KhataSanchalak1Name ?? string.Empty,
                KhataSanchalak1Post = data.KhataSanchalak1Post ?? string.Empty,
                KhataSanchalak2Post = data.KhataSanchalak2Post ?? string.Empty,
                KhataSanchalak3Post = data.KhataSanchalak3Post ?? string.Empty,
                KhataSanchalak1Gender = data.KhataSanchalak1Gender,
                KhataSanchalak1Remarks = data.KhataSanchalak1Remarks ?? string.Empty,
                KhataSanchalak2Name = data.KhataSanchalak2Name ?? string.Empty,
                KhataSanchalak2Gender = data.KhataSanchalak2Gender,
                KhataSanchalak2Remarks = data.KhataSanchalak2Remarks ?? string.Empty,
                KhataSanchalak3Name = data.KhataSanchalak3Name ?? string.Empty,
                KhataSanchalak3Gender = data.KhataSanchalak3Gender,
                KhataSanchalak3Remarks = data.KhataSanchalak3Remarks ?? string.Empty,

                AnugamanKarta1Name = data.AnugamanKarta1Name ?? string.Empty,
                AnugamanKarta1Post = data.AnugamanKarta1Post ?? string.Empty,
                AnugamanKarta1Remarks = data.AnugamanKarta1Remarks ?? string.Empty,

                AnugamanKarta2Name = data.AnugamanKarta2Name ?? string.Empty,
                AnugamanKarta2Post = data.AnugamanKarta2Post ?? string.Empty,
                AnugamanKarta2Remarks = data.AnugamanKarta2Remarks ?? string.Empty,

                AnugamanKarta3Name = data.AnugamanKarta3Name ?? string.Empty,
                AnugamanKarta3Post = data.AnugamanKarta3Post ?? string.Empty,
                AnugamanKarta3Remarks = data.AnugamanKarta3Remarks ?? string.Empty,

                AnugamanKarta4Name = data.AnugamanKarta4Name ?? string.Empty,
                AnugamanKarta4Post = data.AnugamanKarta4Post ?? string.Empty,
                AnugamanKarta4Remarks = data.AnugamanKarta4Remarks ?? string.Empty,

                AnugamanKarta5Name = data.AnugamanKarta5Name ?? string.Empty,
                AnugamanKarta5Post = data.AnugamanKarta5Post ?? string.Empty,
                AnugamanKarta5Remarks = data.AnugamanKarta5Remarks ?? string.Empty,



            } ?? new AnugamanPartibedanViewModel();
        }
        public async Task<bool> InsertAnugamanParitbedan(AnugamanPartibedanViewModel model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var entity = new AnugamanPartibdean
                {
                    PlanningSamjhautaId = model.PlanningSamjhautaId,
                    ParikxanDate = model.ParikxanDate,
                    SampannaType = model.SampannaType,
                    BillRakam = model.BillRakam,
                    IsNirnayaBhayako = model.IsNirnayaBhayako,
                    IsBibad = model.IsBibad,
                    IsGambirBibad = model.IsGambirBibad,
                    BibadDetails = model.BibadDetails,
                    BahiyaJokhim = model.BahiyaJokhim,
                    IsJimmewari = model.IsJimmewari,
                    JimmwariDetails = model.JimmwariDetails,
                    IsBankAccountOppen = model.IsBankAccountOppen,
                    BankAcocuntDetail = model.BankAcocuntDetail,
                    BankName = model.BankName,
                    BankAddress = model.BankAddress,
                    AccountNumber = model.AccountNumber,
                    AccountType = model.AccountType,
                    AccountRemarks = model.AccountRemarks,
                    PratakshyaFaida = model.PratakshyaFaida,
                    GharDhuri = model.GharDhuri,
                    PratakshyaFaidaDetails = model.PratakshyaFaidaDetails,
                    IsSuchanaPati = model.IsSuchanaPati,
                    IsAambhela = model.IsAambhela,
                    EngineerName = model.EngineerName,
                    BhuktaniSifarishRakam = model.BhuktaniSifarishRakam,
                    KaryanayanDetails = model.KaryanayanDetails,
                    SamasyaSamadhanUpaya = model.SamasyaSamadhanUpaya,
                    AnugamanBibaran = model.AnugamanBibaran,
                    BeforePhotos = await _utility.UploadImgAsync("Anugaman", model.BeforePhotosImg),
                    AfterPhotos = await _utility.UploadImgAsync("Anugaman", model.AfterPhotosImg),
                    BetweenPhotos = await _utility.UploadImgAsync("Anugaman", model.BetweenPhotosImg),
                    KhataSanchalak1Name = model.KhataSanchalak1Name,
                    KhataSanchalak1Post = model.KhataSanchalak1Post,
                    KhataSanchalak2Post = model.KhataSanchalak2Post,
                    KhataSanchalak3Post = model.KhataSanchalak3Post,
                    KhataSanchalak1Gender = model.KhataSanchalak1Gender,
                    KhataSanchalak1Remarks = model.KhataSanchalak1Remarks,

                    KhataSanchalak2Name = model.KhataSanchalak2Name,
                    KhataSanchalak2Gender = model.KhataSanchalak2Gender,
                    KhataSanchalak2Remarks = model.KhataSanchalak2Remarks,

                    KhataSanchalak3Name = model.KhataSanchalak3Name,
                    KhataSanchalak3Gender = model.KhataSanchalak3Gender,
                    KhataSanchalak3Remarks = model.KhataSanchalak3Remarks,
                    AnugamanKarta1Name = model.AnugamanKarta1Name,
                    AnugamanKarta1Post = model.AnugamanKarta1Post,
                    AnugamanKarta1Remarks = model.AnugamanKarta1Remarks,

                    AnugamanKarta2Name = model.AnugamanKarta2Name,
                    AnugamanKarta2Post = model.AnugamanKarta2Post,
                    AnugamanKarta2Remarks = model.AnugamanKarta2Remarks,

                    AnugamanKarta3Name = model.AnugamanKarta3Name,
                    AnugamanKarta3Post = model.AnugamanKarta3Post,
                    AnugamanKarta3Remarks = model.AnugamanKarta3Remarks,

                    AnugamanKarta4Name = model.AnugamanKarta4Name,
                    AnugamanKarta4Post = model.AnugamanKarta4Post,
                    AnugamanKarta4Remarks = model.AnugamanKarta4Remarks,

                    AnugamanKarta5Name = model.AnugamanKarta5Name,
                    AnugamanKarta5Post = model.AnugamanKarta5Post,
                    AnugamanKarta5Remarks = model.AnugamanKarta5Remarks,


                };

                _context.AnugamanPartibdeans.Add(entity);
                await _context.SaveChangesAsync();               
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        //public async Task<bool> InsertAnugamanParitbedan(AnugamanPartibedanViewModel model)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        AnugamanPartibdean entity;

        //        // ----------------------- INSERT -----------------------
        //        if (model.Id == 0)
        //        {
        //            entity = new AnugamanPartibdean
        //            {
        //                PlanningSamjhautaId = model.PlanningSamjhautaId,
        //                ParikxanDate = model.ParikxanDate,
        //                SampannaType = model.SampannaType,
        //                BillRakam = model.BillRakam,
        //                IsNirnayaBhayako = model.IsNirnayaBhayako,
        //                IsBibad = model.IsBibad,
        //                IsGambirBibad = model.IsGambirBibad,
        //                BibadDetails = model.BibadDetails,
        //                BahiyaJokhim = model.BahiyaJokhim,
        //                IsJimmewari = model.IsJimmewari,
        //                JimmwariDetails = model.JimmwariDetails,
        //                IsBankAccountOppen = model.IsBankAccountOppen,
        //                BankAcocuntDetail = model.BankAcocuntDetail,
        //                BankName = model.BankName,
        //                BankAddress = model.BankAddress,
        //                AccountNumber = model.AccountNumber,
        //                AccountType = model.AccountType,
        //                AccountRemarks = model.AccountRemarks,
        //                PratakshyaFaida = model.PratakshyaFaida,
        //                GharDhuri = model.GharDhuri,
        //                PratakshyaFaidaDetails = model.PratakshyaFaidaDetails,
        //                IsSuchanaPati = model.IsSuchanaPati,
        //                IsAambhela = model.IsAambhela,
        //                EngineerName = model.EngineerName,
        //                BhuktaniSifarishRakam = model.BhuktaniSifarishRakam,
        //                KaryanayanDetails = model.KaryanayanDetails,
        //                SamasyaSamadhanUpaya = model.SamasyaSamadhanUpaya,
        //                AnugamanBibaran = model.AnugamanBibaran,
        //                BeforePhotos = model.BeforePhotos,
        //                AfterPhotos = model.AfterPhotos,
        //                BetweenPhotos = model.BetweenPhotos
        //            };

        //            _context.AnugamanPartibdeans.Add(entity);
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            // ----------------------- UPDATE -----------------------
        //            entity = await _context.AnugamanPartibdeans
        //                .Include(x => x.KhataSanchalanList)
        //                .FirstOrDefaultAsync(x => x.Id == model.Id);

        //            if (entity == null)
        //                return false;

        //            entity.ParikxanDate = model.ParikxanDate;
        //            entity.SampannaType = model.SampannaType;
        //            entity.BillRakam = model.BillRakam;
        //            entity.IsNirnayaBhayako = model.IsNirnayaBhayako;
        //            entity.IsBibad = model.IsBibad;
        //            entity.IsGambirBibad = model.IsGambirBibad;
        //            entity.BibadDetails = model.BibadDetails;
        //            entity.BahiyaJokhim = model.BahiyaJokhim;
        //            entity.IsJimmewari = model.IsJimmewari;
        //            entity.JimmwariDetails = model.JimmwariDetails;
        //            entity.IsBankAccountOppen = model.IsBankAccountOppen;
        //            entity.BankAcocuntDetail = model.BankAcocuntDetail;
        //            entity.BankName = model.BankName;
        //            entity.BankAddress = model.BankAddress;
        //            entity.AccountNumber = model.AccountNumber;
        //            entity.AccountType = model.AccountType;
        //            entity.AccountRemarks = model.AccountRemarks;
        //            entity.PratakshyaFaida = model.PratakshyaFaida;
        //            entity.GharDhuri = model.GharDhuri;
        //            entity.PratakshyaFaidaDetails = model.PratakshyaFaidaDetails;
        //            entity.IsSuchanaPati = model.IsSuchanaPati;
        //            entity.IsAambhela = model.IsAambhela;
        //            entity.EngineerName = model.EngineerName;
        //            entity.BhuktaniSifarishRakam = model.BhuktaniSifarishRakam;
        //            entity.KaryanayanDetails = model.KaryanayanDetails;
        //            entity.SamasyaSamadhanUpaya = model.SamasyaSamadhanUpaya;
        //            entity.AnugamanBibaran = model.AnugamanBibaran;
        //            entity.BeforePhotos = model.BeforePhotos;
        //            entity.AfterPhotos = model.AfterPhotos;
        //            entity.BetweenPhotos = model.BetweenPhotos;

        //            // ---- Remove old child list ----
        //            _context.KhataSanchalakDetails.RemoveRange(entity.KhataSanchalanList);

        //            await _context.SaveChangesAsync();
        //        }

        //        // ----------------------- CHILD INSERT (Both Insert & Update) -----------------------
        //        if (model.KhataSanchalanList != null)
        //        {
        //            foreach (var item in model.KhataSanchalanList)
        //            {
        //                var child = new KhataSanchalakDetails
        //                {
        //                    AnugamanPartibedanId = entity.Id,
        //                    Name = item.Name,
        //                    Gender = item.Gender,
        //                    Post = item.Post,
        //                    Remarks = item.Remarks
        //                };

        //                _context.KhataSanchalakDetails.Add(child);
        //            }

        //            await _context.SaveChangesAsync();
        //        }

        //        await transaction.CommitAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        await transaction.RollbackAsync();
        //        return false;
        //    }
        //}

    }
}
