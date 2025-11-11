using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Repositories
{
    public class UpabhoktaSamitiRepositories : IUpabhoktaSamiti
    {
        private readonly PlanningContext _context;
        private readonly ILogger<CommonRepository> _logger;
        private readonly string _userId = null;
        private readonly IUtility _utility;

        public UpabhoktaSamitiRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, ILogger<CommonRepository> logger, IUtility utility)
        {
            _context = context;
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _utility = utility;
        }
        #region Upabhoktasamiti
        public async Task<bool> CreateUpabhoktaSamiti(UpavoktaSamitiDetailViewModel model)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    if (model.UpabhoktaSamitiDetailId > 0)
                    {
                        var upavoktaSamiti = await _context.UpabhoktaSamitiDetail.FirstOrDefaultAsync(x => x.UpabhoktaSamitiDetailId == model.UpabhoktaSamitiDetailId);
                        if (upavoktaSamiti != null)
                        {
                            upavoktaSamiti.UpabhoktaSamitiDetailId = model.UpabhoktaSamitiDetailId;
                            upavoktaSamiti.Samiti_Estd_Date = model.Samiti_Estd_Date;
                            upavoktaSamiti.NepaliSamitiEstdDate = model.NepaliSamitiEstdDate;
                            upavoktaSamiti.Name = model.Name;
                            upavoktaSamiti.DartaNo = model.DartaNo;
                            upavoktaSamiti.Beneficiaries_Absent = model.Beneficiaries_Absent;
                            upavoktaSamiti.Beneficiaries_Attendance = model.Beneficiaries_Attendance;
                            upavoktaSamiti.NibedanMiti = model.NibedanMiti;
                            upavoktaSamiti.Female_Present = model.Female_Present;
                            upavoktaSamiti.Male_Present = model.Male_Present;
                            upavoktaSamiti.Status = true;
                            upavoktaSamiti.WardNo = model.WardNo;
                            upavoktaSamiti.FiscalYearId = model.FiscalYearId;
                            upavoktaSamiti.BankId = model.BankId;
                            upavoktaSamiti.AccountNumber = model.AccountNumber;
                            upavoktaSamiti.Address = model.Address;
                            _context.Entry(upavoktaSamiti).State = EntityState.Modified;

                            if (model.YojanaIds != null && model.YojanaIds.Count > 0)
                            {
                                foreach (var item in await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == upavoktaSamiti.UpabhoktaSamitiDetailId).ToListAsync())
                                {
                                    if (!model.YojanaIds.Any(x => x == item.YojanaId))
                                    {
                                        _context.UpabhoktaSamitiDetailYojanas.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                foreach (var item in model.YojanaIds)
                                {
                                    if (!await _context.UpabhoktaSamitiDetailYojanas.AnyAsync(x => x.UpabhoktaSamitiDetailId == upavoktaSamiti.UpabhoktaSamitiDetailId && x.YojanaId == item))
                                    {
                                        var yoj = new UpabhoktaSamitiDetailYojanas()
                                        {
                                            UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                            YojanaId = item,
                                        };
                                        await _context.UpabhoktaSamitiDetailYojanas.AddAsync(yoj);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }


                            if (model.samitiMemberDetaillist.Count > 0)
                            {
                                foreach (var item in await _context.UpabhoktaSamitiMemberDetail.Where(x => x.UpabhoktaSamitiDetailId == model.UpabhoktaSamitiDetailId).ToListAsync())
                                {
                                    if (!model.samitiMemberDetaillist.Any(x => x.UpabhoktaSamitiMemberDetailId == item.UpabhoktaSamitiMemberDetailId))
                                    {
                                        _context.UpabhoktaSamitiMemberDetail.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                foreach (var item in model.samitiMemberDetaillist)
                                {
                                    var other = new PlanningCore.Models.FileUploadModel();
                                    if (item.ImagePath != null)
                                    {
                                        other = await _utility.UploadImgReturnPathAndName("PlanningDocuments", item.ImagePath, "Upabhoktasamiti-UploadedDocs");
                                    }
                                    else
                                    {
                                        other.FilePath = item.PhotoPath;
                                    }
                                    var data = await _context.UpabhoktaSamitiMemberDetail.Where(x => x.UpabhoktaSamitiMemberDetailId == item.UpabhoktaSamitiMemberDetailId).FirstOrDefaultAsync();
                                    if (data != null)
                                    {


                                        data.UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId;
                                        //data.PadaId = item.PadaId;
                                        data.SamitiPostId = item.SamitiPostId;
                                        data.MemberName = item.MemberName;
                                        data.Address = item.Address;
                                        data.GrandFatherName = item.GrandFatherName;
                                        data.Age = item.Age;
                                        data.DOB = item.DOB;
                                        data.PhoneNo = item.PhoneNo;
                                        data.CitizenshipNumber = item.CitizenshipNumber;
                                        data.ImagePath = _utility.UploadImgReturnPathAndName("SamitiDetails", item.ImagePath).Result.FilePath;
                                        data.Status = true;
                                        data.FatherName = item.FatherName;
                                        //data.ImagePath = item.FilePath;
                                        data.UpdatedBy = _userId;
                                        data.UpdatedDate = DateTime.Now;
                                        _context.Entry(data).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {

                                        var samitimemb = new UpabhoktaSamitiMemberDetail()
                                        {
                                            UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                            UpabhoktaSamitiMemberDetailId = item.UpabhoktaSamitiMemberDetailId,
                                            //PadaId = item.PadaId,
                                            SamitiPostId = item.SamitiPostId,
                                            MemberName = item.MemberName,
                                            Address = item.Address,
                                            FatherName = item.FatherName,
                                            GrandFatherName = item.GrandFatherName,
                                            Age = item.Age,
                                            PhoneNo = item.PhoneNo,
                                            DOB = item.DOB,
                                            ImagePath = other.FilePath,
                                            CitizenshipNumber = item.CitizenshipNumber,
                                            //ImagePath = _utility.UploadImgReturnPathAndName("SamitiMember", item.ImagePath).Result.FilePath,
                                            CreatedBy = _userId,
                                            CreatedDate = DateTime.Now,
                                            Status = true,
                                        };
                                        await _context.UpabhoktaSamitiMemberDetail.AddAsync(samitimemb);
                                        _context.SaveChanges();
                                    }

                                }
                            }

                            if (model.AnugamanMemberList.Count > 0)
                            {

                                foreach (var item in await _context.AnugamanMember.Where(x => x.UpabhoktaSamitiDetailId == model.UpabhoktaSamitiDetailId).ToListAsync())
                                {
                                    if (!model.AnugamanMemberList.Any(x => x.AnugamanMemberId == item.AnugamanMemberId))
                                    {
                                        _context.AnugamanMember.Remove(item);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                foreach (var item in model.AnugamanMemberList)
                                {

                                    var data = await _context.AnugamanMember.Where(x => x.AnugamanMemberId == item.AnugamanMemberId).FirstOrDefaultAsync();
                                    if (data != null)
                                    {

                                        data.UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId;
                                        //data.PadaId = item.PadaId;
                                        data.PostId = item.PostId;
                                        data.Name = item.Name;
                                        data.Address = item.Address;
                                        data.GrandFatherName = item.GrandFatherName;
                                        data.DOB = item.DOB;
                                        data.Contact = item.Contact;
                                        data.Status = true;
                                        data.FatherName = item.FatherName;
                                        //data.ImagePath = item.FilePath;
                                        data.UpdatedBy = _userId;
                                        data.UpdatedDate = DateTime.Now;
                                        _context.Entry(data).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        var anugamanmemb = new AnugamanMember()
                                        {
                                            UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                            AnugamanMemberId = item.AnugamanMemberId,
                                            //PadaId = item.PadaId,
                                            PostId = item.PostId,
                                            Name = item.Name,
                                            Address = item.Address,
                                            FatherName = item.FatherName,
                                            GrandFatherName = item.GrandFatherName,
                                            Contact = item.Contact,
                                            DOB = item.DOB,
                                            Status = true,
                                        };
                                        await _context.AnugamanMember.AddAsync(anugamanmemb);
                                        _context.SaveChanges();
                                    }

                                }
                            }
                        }


                    }
                    else
                    {
                        int fiscalYearId = await _utility.GetCurrentFiscalYear();
                        var upavoktaSamiti = new UpabhoktaSamitiDetail()
                        {
                            UpabhoktaSamitiDetailId = model.UpabhoktaSamitiDetailId,
                            Samiti_Estd_Date = model.Samiti_Estd_Date,
                            NepaliSamitiEstdDate = model.NepaliSamitiEstdDate,
                            Name = model.Name,
                            DartaNo = model.DartaNo,
                            Beneficiaries_Absent = model.Beneficiaries_Absent,
                            Beneficiaries_Attendance = model.Beneficiaries_Attendance,
                            NibedanMiti = model.NibedanMiti,
                            Female_Present = model.Female_Present,
                            Male_Present = model.Male_Present,
                            Status = true,
                            WardNo = model.WardNo,
                            FiscalYearId = fiscalYearId,
                            BankId = model.BankId,
                            Address = model.Address,
                            AccountNumber = model.AccountNumber,
                        };
                        await _context.UpabhoktaSamitiDetail.AddAsync(upavoktaSamiti);
                        _context.SaveChanges();

                        foreach (var item in model.YojanaIds)
                        {
                            var yoj = new UpabhoktaSamitiDetailYojanas()
                            {
                                UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                YojanaId = item,
                            };
                            await _context.UpabhoktaSamitiDetailYojanas.AddAsync(yoj);
                            await _context.SaveChangesAsync();
                        }

                        if (model.samitiMemberDetaillist.Count > 0)
                        {
                            foreach (var item in model.samitiMemberDetaillist)
                            {
                                var img = await _utility.UploadImgReturnPathAndName("SamitiMembers", item.ImagePath, "SamitiMembers-Cit");

                                var data = new UpabhoktaSamitiMemberDetail()
                                {
                                    UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                    UpabhoktaSamitiMemberDetailId = item.UpabhoktaSamitiMemberDetailId,
                                    //PadaId = item.PadaId,
                                    SamitiPostId = item.SamitiPostId,
                                    MemberName = item.MemberName,
                                    Address = item.Address,
                                    FatherName = item.FatherName,
                                    GrandFatherName = item.GrandFatherName,
                                    Age = item.Age,
                                    DOB = item.DOB,
                                    PhoneNo = item.PhoneNo,
                                    //ImagePath = _utility.UploadImgReturnPathAndName("SamitiMember", item.ImagePath).Result.FilePath,
                                    ImagePath = img.FilePath,
                                    Status = true,
                                    CreatedBy = _userId,
                                    CitizenshipNumber=item.CitizenshipNumber,
                                    CreatedDate = DateTime.Now,
                                };
                                await _context.UpabhoktaSamitiMemberDetail.AddAsync(data);
                                _context.SaveChanges();

                            }
                        }
                        if (model.AnugamanMemberList.Count > 0)
                        {
                            foreach (var item in model.AnugamanMemberList)
                            {

                                var Anugamandata = new AnugamanMember()
                                {
                                    UpabhoktaSamitiDetailId = upavoktaSamiti.UpabhoktaSamitiDetailId,
                                    AnugamanMemberId = item.AnugamanMemberId,
                                    //PadaId = item.PadaId,
                                    PostId = item.PostId,
                                    Name = item.Name,
                                    Address = item.Address,
                                    FatherName = item.FatherName,
                                    GrandFatherName = item.GrandFatherName,
                                    DOB = item.DOB,
                                    Contact = item.Contact,
                                    Status = true,
                                    CreatedBy = _userId,
                                    CreatedDate = DateTime.Now,
                                };
                                await _context.AnugamanMember.AddAsync(Anugamandata);
                                _context.SaveChanges();

                            }
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
        public async Task<UpavoktaSamitiDetailViewModel> GetUpavoktaSamitiDetailById(int? id)
        {
            return await _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == id).Select(x => new UpavoktaSamitiDetailViewModel()
            {
                UpabhoktaSamitiDetailId = x.UpabhoktaSamitiDetailId,
                DartaNo = x.DartaNo,
                Samiti_Estd_Date = x.Samiti_Estd_Date,
                NepaliSamitiEstdDate = x.NepaliSamitiEstdDate,
                Name = x.Name,
                Beneficiaries_Attendance = x.Beneficiaries_Attendance,
                Beneficiaries_Absent = x.Beneficiaries_Absent,
                NibedanMiti = x.NibedanMiti,
                Female_Present = x.Female_Present,
                Male_Present = x.Male_Present,
                Status = x.Status,
                WardNo = x.WardNo,
                FiscalYearId = x.FiscalYearId,
                BankName = x.class_A_Bank_List.BankName_Nep,
                AccountNumber = x.AccountNumber,
                Address = x.Address,
                BankId = x.BankId,
                YojanaIds = _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(u => u.YojanaId).ToList(),
                YojanaNames = (from y in _context.YojanaSetup
                               join uy in _context.UpabhoktaSamitiDetailYojanas on y.YojanaSetupId equals uy.YojanaId
                               where uy.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId
                               select y.YojanaName).ToList(),
                FiscalYearName = _context.FiscalYear.Where(f => f.Id == x.FiscalYearId).Select(f => f.Name).FirstOrDefault(),
                HakimName = _context.Employee.Where(a => a.PadaId == 1).Select(a => a.Name).FirstOrDefault(),
                samitiMemberDetaillist = _context.UpabhoktaSamitiMemberDetail.Where(y => y.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(z => new UpavoktaSamitiMemberDetailViewModel()
                {

                    UpabhoktaSamitiMemberDetailId = z.UpabhoktaSamitiMemberDetailId,
                    UpabhoktaSamitiDetailId = z.UpabhoktaSamitiDetailId ?? 0,
                    SamitiPostId = z.SamitiPostId,
                    PadaName = _context.SamitiPost.Where(y => y.Id == z.SamitiPostId).Select(z => z.Name).FirstOrDefault(),
                    MemberName = z.MemberName,
                    Address = z.Address,
                    FatherName = z.FatherName,
                    GrandFatherName = z.GrandFatherName,
                    Age = z.Age,
                    DOB = z.DOB,
                    PhoneNo = z.PhoneNo,
                    PhotoPath = z.ImagePath,
                    Status = z.Status,
                    CitizenshipNumber = z.CitizenshipNumber,
                }).ToList(),
                AnugamanMemberList = _context.AnugamanMember.Where(y => y.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId).Select(z => new AnugamanViewModel()
                {

                    AnugamanMemberId = z.AnugamanMemberId,
                    PostId = z.PostId,
                    PadaName = _context.AnugamanSamitiPost.Where(y => y.Id == z.PostId).Select(z => z.Name).FirstOrDefault(),
                    Name = z.Name,
                    Address = z.Address,
                    FatherName = z.FatherName,
                    GrandFatherName = z.GrandFatherName,
                    Contact = z.Contact,
                    DOB = z.DOB,
                    Status = z.Status,

                }).ToList(),
            }).FirstOrDefaultAsync() ?? new UpavoktaSamitiDetailViewModel();
        }
        public async Task<List<UpavoktaSamitiDetailViewModel>> GetAllUpavoktaSamitiDetail()
        {
            int wardId = await _utility.GetWardNoForLogin_Role_User() ?? 0;
            return await (from u in _context.UpabhoktaSamitiDetail
                          join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                          where u.Status == true && (wardId == 0 || uy.YojanaSetup.WardId == wardId)
                          select new UpavoktaSamitiDetailViewModel()
                          {
                              UpabhoktaSamitiDetailId = u.UpabhoktaSamitiDetailId,
                              Name = u.Name,
                              NepaliSamitiEstdDate = u.NepaliSamitiEstdDate,
                          }).ToListAsync();
        }

        public async Task<bool> DeleteUpavoktaSamitiDetailById(int id)
        {
            var data = await _context.UpabhoktaSamitiDetail.Where(x => x.UpabhoktaSamitiDetailId == id).FirstOrDefaultAsync();
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
        #region Tolbikash
        public async Task<bool> CreateTolbikashSamiti(TolBikashSansthaViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (model.TolBikashSansthaId > 0)
                    {
                        var tolBikashSanstha = await _context.TolBikashSanstha.FirstOrDefaultAsync(x => x.TolBikashSansthaId == model.TolBikashSansthaId);
                        if (tolBikashSanstha != null)
                        {
                            tolBikashSanstha.TolBikashSansthaId = model.TolBikashSansthaId;
                            tolBikashSanstha.TolBikashSansthaName = model.TolBikashSansthaName;
                            tolBikashSanstha.DartaNo = model.DartaNo;
                            tolBikashSanstha.Address = model.Address;
                            tolBikashSanstha.TolSamitiEstdDate = model.TolSamitiEstdDate;
                            tolBikashSanstha.Beneficiaries_Attendance = model.Beneficiaries_Attendance;
                            tolBikashSanstha.Beneficiaries_Absent = model.Beneficiaries_Absent;
                            tolBikashSanstha.UpastithiDate = model.UpastithiDate;
                            tolBikashSanstha.Female_Present = model.Female_Present;
                            tolBikashSanstha.AnugamanMember = model.AnugamanMember;
                            tolBikashSanstha.NibedanMiti = model.NibedanMiti;
                            tolBikashSanstha.Status = true;
                            tolBikashSanstha.WardNo = model.WardNo;
                            tolBikashSanstha.FiscalYearId = model.FiscalYearId;
                            tolBikashSanstha.YojanaId = model.YojanaId;
                            tolBikashSanstha.AccountNumber = model.AccountNumber;
                            tolBikashSanstha.BankName = model.AccountNumber;
                            _context.Entry(tolBikashSanstha).State = EntityState.Modified;
                        }
                        if (model.tolBikashSansthaMemberList.Count > 0)
                        {
                            foreach (var item in await _context.TolBikashSansthaMember.Where(x => x.TolBikashSansthaId == model.TolBikashSansthaId).ToListAsync())
                            {
                                if (!model.tolBikashSansthaMemberList.Any(x => x.TolBikashSansthaMemberId == item.TolBikashSansthaMemberId))
                                {
                                    _context.TolBikashSansthaMember.Remove(item);
                                    await _context.SaveChangesAsync();
                                }
                            }

                            foreach (var item in model.tolBikashSansthaMemberList)
                            {
                                var data = await _context.TolBikashSansthaMember.Where(x => x.TolBikashSansthaMemberId == item.TolBikashSansthaMemberId).FirstOrDefaultAsync();
                                if (data != null)
                                {

                                    data.TolBikashSansthaId = model.TolBikashSansthaId;
                                    //data.PadaId = item.PadaId;
                                    data.SchoolPostId = item.SchoolPostId;
                                    data.Name = item.Name;
                                    data.Address = item.Address;
                                    data.PhoneNumber = item.PhoneNumber;
                                    data.NagariktaNumber = item.NagariktaNumber;
                                    data.FatherName = item.FatherName;
                                    data.ImagePath = item.ImagePath;

                                    _context.Entry(data).State = EntityState.Modified;
                                    _context.SaveChanges();
                                }
                                else
                                {
                                    var addData = new TolBikashSansthaMember()
                                    {
                                        TolBikashSansthaId = tolBikashSanstha.TolBikashSansthaId,
                                        //PadaId = item.PadaId,
                                        SchoolPostId = item.SchoolPostId,
                                        Name = item.Name,
                                        Address = item.Address,
                                        PhoneNumber = item.PhoneNumber,
                                        NagariktaNumber = item.NagariktaNumber,
                                        FatherName = item.FatherName,
                                        GrandFatherName = item.GrandFatherName,
                                        ImagePath = item.ImagePath,

                                    };
                                    await _context.TolBikashSansthaMember.AddAsync(addData);
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
                        int fiscalYearId = await _utility.GetCurrentFiscalYear();
                        var tolBikashSanstha = new TolBikashSanstha()
                        {

                            TolBikashSansthaId = model.TolBikashSansthaId,
                            TolBikashSansthaName = model.TolBikashSansthaName,
                            DartaNo = model.DartaNo,
                            Address = model.Address,
                            TolSamitiEstdDate = model.TolSamitiEstdDate,
                            Beneficiaries_Attendance = model.Beneficiaries_Attendance,
                            Beneficiaries_Absent = model.Beneficiaries_Absent,
                            UpastithiDate = model.UpastithiDate,
                            Female_Present = model.Female_Present,
                            AnugamanMember = model.AnugamanMember,
                            NibedanMiti = model.NibedanMiti,
                            Status = true,
                            WardNo = model.WardNo,
                            FiscalYearId = fiscalYearId,
                            YojanaId = model.YojanaId,
                            BankName = model.BankName,
                            AccountNumber = model.AccountNumber,
                        };
                        await _context.TolBikashSanstha.AddAsync(tolBikashSanstha);
                        _context.SaveChanges();
                        if (model.tolBikashSansthaMemberList.Count > 0)
                        {
                            foreach (var item in model.tolBikashSansthaMemberList)
                            {

                                var data = new TolBikashSansthaMember()
                                {
                                    TolBikashSansthaId = tolBikashSanstha.TolBikashSansthaId,
                                    //PadaId = item.PadaId,
                                    SchoolPostId = item.SchoolPostId,
                                    Name = item.Name,
                                    Address = item.Address,
                                    PhoneNumber = item.PhoneNumber,
                                    NagariktaNumber = item.NagariktaNumber,
                                    FatherName = item.FatherName,
                                    GrandFatherName = item.GrandFatherName,
                                    ImagePath = item.ImagePath,

                                };
                                await _context.TolBikashSansthaMember.AddAsync(data);
                                _context.SaveChanges();

                            }
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

        public async Task<TolBikashSansthaViewModel> GetTolBikashSansthaById(int? id)
        {
            return await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == id).Select(x => new TolBikashSansthaViewModel()
            {
                TolBikashSansthaId = x.TolBikashSansthaId,
                TolBikashSansthaName = x.TolBikashSansthaName,
                DartaNo = x.DartaNo,
                Address = x.Address,
                TolSamitiEstdDate = x.TolSamitiEstdDate,
                Beneficiaries_Attendance = x.Beneficiaries_Attendance,
                Beneficiaries_Absent = x.Beneficiaries_Absent,
                UpastithiDate = x.UpastithiDate,
                Female_Present = x.Female_Present,
                AnugamanMember = x.AnugamanMember,
                NibedanMiti = x.NibedanMiti,
                WardNo = x.WardNo,
                FiscalYearId = x.FiscalYearId,
                YojanaId = x.YojanaId,
                YojanaName = _context.YojanaSetup.Where(a => a.YojanaSetupId == x.YojanaId).Select(a => a.YojanaName).FirstOrDefault(),
                AccountNumber = x.AccountNumber,
                BankName = x.BankName,
                BiupurjiAmount = x.BiupurjiAmount,
                tolBikashSansthaMemberList = _context.TolBikashSansthaMember.Where(y => y.TolBikashSansthaId == id).Select(z => new TolBikashSansthaMemberViewModel()
                {
                    TolBikashSansthaMemberId = z.TolBikashSansthaMemberId,
                    Address = z.Address,
                    FatherName = z.FatherName,
                    GrandFatherName = z.GrandFatherName,
                    ImagePath = z.ImagePath,
                    NagariktaNumber = z.NagariktaNumber,
                    Name = z.Name,
                    //PadaId = z.PadaId,
                    SchoolPostId = z.SchoolPostId,
                    PhoneNumber = z.PhoneNumber,
                    PostName = z.SamitiPost.Name,
                }).ToList(),
            }).FirstOrDefaultAsync() ?? new TolBikashSansthaViewModel();
        }

        public async Task<List<TolBikashSansthaViewModel>> GetTolBikashSansthaList()
        {
            var data = await _context.TolBikashSanstha.Where(x => x.Status == true).Select(x => new TolBikashSansthaViewModel()
            {
                TolBikashSansthaId = x.TolBikashSansthaId,
                TolBikashSansthaName = x.TolBikashSansthaName,
                DartaNo = x.DartaNo,
                TolSamitiEstdDate = x.TolSamitiEstdDate,
                Beneficiaries_Attendance = x.Beneficiaries_Attendance,
                Beneficiaries_Absent = x.Beneficiaries_Absent,
                UpastithiDate = x.UpastithiDate,
                Female_Present = x.Female_Present,
                AnugamanMember = x.AnugamanMember,
                NibedanMiti = x.NibedanMiti,
                WardNo = x.WardNo,
                FiscalYearId = x.FiscalYearId,
                YojanaId = x.YojanaId,
                YojanaName = _context.YojanaSetup.Where(a => a.YojanaSetupId == x.YojanaId).Select(a => a.YojanaName).FirstOrDefault(),
                BankName = x.BankName,
                AccountNumber = x.AccountNumber,
                BiupurjiAmount = x.BiupurjiAmount,
                tolBikashSansthaMemberList = _context.TolBikashSansthaMember.Where(y => y.TolBikashSansthaId == x.TolBikashSansthaId).Select(z => new TolBikashSansthaMemberViewModel()
                {
                    TolBikashSansthaMemberId = z.TolBikashSansthaMemberId,
                    Address = z.Address,
                    FatherName = z.FatherName,
                    GrandFatherName = z.GrandFatherName,
                    ImagePath = z.ImagePath,
                    NagariktaNumber = z.NagariktaNumber,
                    Name = z.Name,
                    //PadaId = z.PadaId,
                    SchoolPostId = z.SchoolPostId,
                    PhoneNumber = z.PhoneNumber
                }).ToList(),
            }).ToListAsync();
            return data;

        }

        public async Task<bool> DeleteTolBikashSansthaById(int id)
        {
            var data = await _context.TolBikashSanstha.Where(x => x.TolBikashSansthaId == id).FirstOrDefaultAsync();
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
        #region Biupurji
        public async Task<bool> Updatebiupurji(int? id, decimal Amount)
        {
            try
            {

                var data = await _context.TolBikashSanstha.FirstOrDefaultAsync(x => x.TolBikashSansthaId == id);
                if (data != null)
                {
                    data.BiupurjiAmount = Amount;
                    _context.Entry(data).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        public async Task<List<UpbhoktaSamitiMembersModel>> CheckSamitMember(string ctzno, int samitiId)
        {
            if (string.IsNullOrWhiteSpace(ctzno)) return null;
            var data = await _context.UpabhoktaSamitiMemberDetail.Where(x => x.CitizenshipNumber.Equals(ctzno) && x.UpabhoktaSamitiDetailId != samitiId)
                .Select(x => new UpbhoktaSamitiMembersModel()
                {
                    Name = x.MemberName,
                    Post = x.SamitiPost.Name,
                    ContactNo = x.PhoneNo,
                    SamitiName = x.UpabhoktaSamitiDetail.Name
                }).ToListAsync();
            return data;
        }

        public async Task<UpabhoktaSamitiDetailDocsViewModel> GetDocsListByUpbhokataId(int id)
        {
            var data = new UpabhoktaSamitiDetailDocsViewModel()
            {
                UpabhoktaSamitiDetailId = id,
                DocsList = await _context.UpabhoktaSamitiDetailDocType
                .Select(x => new UpabhoktaSamitiDetailDocsDetailsViewModel()
                {
                    UpabhoktaSamitiDetailDocTypeId = x.Id,
                    DocPath = _context.UpabhoktaSamitiDetailDocs.Where(d => d.UpabhoktaSamitiDetailDocTypeId == x.Id && d.UpabhoktaSamitiDetailId == id).Select(d => d.DocPath).FirstOrDefault(),
                }).ToListAsync(),
            };
            return data;
        }

        public async Task<bool> InsertUpdateDocsUpload(UpabhoktaSamitiDetailDocsViewModel model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in model.DocsList)
                    {
                        if (item.DocsFile != null && item.DocsFile.Length > 0)
                        {
                            var data = await _context.UpabhoktaSamitiDetailDocs.FirstOrDefaultAsync(x => x.UpabhoktaSamitiDetailId == model.UpabhoktaSamitiDetailId && x.UpabhoktaSamitiDetailDocTypeId == item.UpabhoktaSamitiDetailDocTypeId);
                            if (data != null)
                            {
                                data.DocPath = await _utility.UploadImgAsync("UpabhoktaSamitiDetailDocs", item.DocsFile);

                                _context.Entry(data).State = EntityState.Modified;
                            }
                            else
                            {
                                data = new UpabhoktaSamitiDetailDocs()
                                {
                                    UpabhoktaSamitiDetailId = model.UpabhoktaSamitiDetailId,
                                    UpabhoktaSamitiDetailDocTypeId = item.UpabhoktaSamitiDetailDocTypeId,
                                    DocPath = await _utility.UploadImgAsync("UpabhoktaSamitiDetailDocs", item.DocsFile),
                                };
                                await _context.UpabhoktaSamitiDetailDocs.AddAsync(data);
                            }
                            await _context.SaveChangesAsync();
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

        public async Task<List<NewToleBikashViewModel>> GetAllNewToleBikash()
        {
            int wardId = await _utility.GetWardNoForLogin_Role_User() ?? 0;
          var data=await (from u in _context.NewToleBikash
                          join uy in _context.NewToleBikashYojanas on u.Id equals uy.NewToleBikashId
                          where u.Status == true && (wardId == 0 || uy.YojanaSetup.WardId == wardId)
                          select new NewToleBikashViewModel()
                          {
                              Id = u.Id,
                              Name = u.Name,
                              NepaliSamitiEstdDate = u.NepaliSamitiEstdDate,
                          }).ToListAsync();
            return data;
        }

        public async Task<NewToleBikashViewModel> GetNewToleBikashById(int id = 0)
        {
            var data = await _context.NewToleBikash.Where(x => x.Id == id)
                .Select(x => new NewToleBikashViewModel()
                {
                    Id = x.Id,
                    DartaNo = x.DartaNo,
                    Name = x.Name,
                    NepaliSamitiEstdDate = x.NepaliSamitiEstdDate,
                    Samiti_Estd_Date = x.Samiti_Estd_Date,
                    Address = x.Address,
                    Beneficiaries_Attendance = x.Beneficiaries_Attendance,
                    Beneficiaries_Absent = x.Beneficiaries_Absent,
                    Female_Present = x.Female_Present,
                    Male_Present = x.Male_Present,
                    NibedanMiti = x.NibedanMiti,
                    BankId = x.BankId,
                    AccountNumber = x.AccountNumber,
                    BankName = _context.Class_A_Bank_List.Where(b => b.Class_A_Bank_List_Id == x.BankId).Select(b => b.BankName_Nep).FirstOrDefault(),
                    YojanaIds = _context.NewToleBikashYojanas.Where(b => b.NewToleBikashId == x.Id).Select(b => b.YojanaId).ToList(),
                    YojanaNames = (from y in _context.YojanaSetup
                                   join uy in _context.NewToleBikashYojanas on y.YojanaSetupId equals uy.YojanaId
                                   where uy.NewToleBikashId == x.Id
                                   select y.YojanaName).ToList(),
                    NewToleBikashMemberList = _context.NewToleBikashMemberDetail.Where(m => m.NewToleBikashlId == x.Id)
                    .Select(m => new NewToleBikashMemberDetailViewModel()
                    {
                        Id = m.Id,
                        NewToleBikashlId = m.NewToleBikashlId,
                        SamitiPostId = m.SamitiPostId,
                        MemberName = m.MemberName,
                        Address = m.Address,
                        CitizenshipNumber = m.CitizenshipNumber,
                        PhoneNo = m.PhoneNo,
                        DOB = m.DOB,
                        DOBEng = m.DOBEng,
                        ImagePath = m.ImagePath,
                        SamitiPostName = _context.SamitiPost.Where(s => s.Id == m.SamitiPostId).Select(s => s.Name).FirstOrDefault(),
                    }).ToList(),
                    AnugamanMemberList = _context.NewToleBikashAnugamanMember.Where(a => a.NewToleBikashId == x.Id)
                    .Select(a => new NewToleBikashAnugamanMemberViewModel()
                    {
                        Id = a.Id,
                        NewToleBikashId = a.NewToleBikashId,
                        PostId = a.PostId,
                        Name = a.Name,
                        Address = a.Address,
                        Contact = a.Contact,
                        PostName = _context.AnugamanSamitiPost.Where(s => s.Id == a.PostId).Select(s => s.Name).FirstOrDefault(),
                    }).ToList(),
                }).FirstOrDefaultAsync() ?? new NewToleBikashViewModel();
            return data;
        }

        public async Task<bool> CreateUpabhoktaSamiti(NewToleBikashViewModel model)
        {
            int fiscalId = await _utility.GetCurrentFiscalYear();
            int wardId = await _utility.GetWardNoForLogin_Role_User() ?? 0;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var data = await _context.NewToleBikash.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (model.Id > 0)
                    {
                        if (data != null)
                        {
                            data.Name = model.Name;
                            data.Address = model.Address;
                            data.NepaliSamitiEstdDate = model.NepaliSamitiEstdDate;
                            data.Samiti_Estd_Date = model.Samiti_Estd_Date;
                            data.Beneficiaries_Attendance = model.Beneficiaries_Attendance;
                            data.Beneficiaries_Absent = model.Beneficiaries_Absent;
                            data.Female_Present = model.Female_Present;
                            data.Male_Present = model.Male_Present;
                            data.NibedanMiti = model.NibedanMiti;
                            data.BankId = model.BankId;
                            data.AccountNumber = model.AccountNumber;
                            data.UpdatedBy = _userId;
                            data.UpdatedDate = DateTime.Now;

                            _context.Entry(data).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            foreach (var item in model.NewToleBikashMemberList)
                            {
                                var member = await _context.NewToleBikashMemberDetail.FirstOrDefaultAsync(x => x.Id == item.Id);
                                if (member != null)
                                {
                                    member.SamitiPostId = item.SamitiPostId;
                                    member.MemberName = item.MemberName;
                                    member.Address = item.Address;
                                    member.CitizenshipNumber = item.CitizenshipNumber;
                                    member.PhoneNo = item.PhoneNo;
                                    member.DOB = item.DOB;
                                    member.DOBEng = item.DOBEng;
                                    member.ImagePath = item.ImageFile == null ? member.ImagePath : _utility.UploadImgReturnPathAndName("NewToleBikashMemberDetail", item.ImageFile).Result.FilePath;
                                    member.UpdatedBy = _userId;
                                    member.UpdatedDate = DateTime.Now;

                                    _context.Entry(member).State = EntityState.Modified;
                                }
                                else
                                {
                                    member = new NewToleBikashMemberDetail()
                                    {
                                        NewToleBikashlId = data.Id,
                                        SamitiPostId = item.SamitiPostId,
                                        MemberName = item.MemberName,
                                        Address = item.Address,
                                        CitizenshipNumber = item.CitizenshipNumber,
                                        PhoneNo = item.PhoneNo,
                                        DOB = item.DOB,
                                        DOBEng = item.DOBEng,
                                        ImagePath = _utility.UploadImgReturnPathAndName("NewToleBikashMemberDetail", item.ImageFile).Result.FilePath,
                                        CreatedBy = _userId,
                                        CreatedDate = DateTime.Now,
                                    };
                                    await _context.NewToleBikashMemberDetail.AddAsync(member);
                                }
                                await _context.SaveChangesAsync();
                            }
                            foreach (var item in model.YojanaIds)
                            {
                                if (!await _context.NewToleBikashYojanas.AnyAsync(x => x.Id == data.Id && x.YojanaId == item))
                                {
                                    var yoj = new NewToleBikashYojanas()
                                    {
                                        NewToleBikashId = data.Id,
                                        YojanaId = item,
                                    };
                                    await _context.NewToleBikashYojanas.AddAsync(yoj);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            foreach (var item in model.AnugamanMemberList)
                            {
                                var amember = await _context.NewToleBikashAnugamanMember.FirstOrDefaultAsync(x => x.Id == item.Id);
                                if (amember != null)
                                {
                                    amember.PostId = item.PostId;
                                    amember.Name = item.Name;
                                    amember.Address = item.Address;
                                    amember.Contact = item.Contact;
                                    amember.UpdatedBy = _userId;
                                    amember.UpdatedDate = DateTime.Now;

                                    _context.Entry(amember).State = EntityState.Modified;
                                }
                                else
                                {
                                    amember = new NewToleBikashAnugamanMember()
                                    {
                                        NewToleBikashId = data.Id,
                                        PostId = item.PostId,
                                        Name = item.Name,
                                        Address = item.Address,
                                        Contact = item.Contact,
                                        CreatedBy = _userId,
                                        CreatedDate = DateTime.Now,
                                    };
                                    await _context.NewToleBikashAnugamanMember.AddAsync(amember);
                                }
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {

                        data = new NewToleBikash()
                        {
                            DartaNo = model.DartaNo,
                            Name = model.Name,
                            Address = model.Address,
                            NepaliSamitiEstdDate = model.NepaliSamitiEstdDate,
                            Samiti_Estd_Date = model.Samiti_Estd_Date,
                            Beneficiaries_Attendance = model.Beneficiaries_Attendance,
                            Beneficiaries_Absent = model.Beneficiaries_Absent,
                            Female_Present = model.Female_Present,
                            Male_Present = model.Male_Present,
                            NibedanMiti = model.NibedanMiti,
                            BankId = model.BankId,
                            AccountNumber = model.AccountNumber,
                            FiscalYearId = fiscalId,
                            WardNo = wardId,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now,
                        };
                        await _context.NewToleBikash.AddAsync(data);
                        await _context.SaveChangesAsync();

                        foreach (var item in model.NewToleBikashMemberList)
                        {
                            var member = new NewToleBikashMemberDetail()
                            {
                                NewToleBikashlId = data.Id,
                                SamitiPostId = item.SamitiPostId,
                                MemberName = item.MemberName,
                                Address = item.Address,
                                CitizenshipNumber = item.CitizenshipNumber,
                                PhoneNo = item.PhoneNo,
                                DOB = item.DOB,
                                DOBEng = item.DOBEng,
                                ImagePath = _utility.UploadImgReturnPathAndName("NewToleBikashMemberDetail", item.ImageFile).Result.FilePath,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.NewToleBikashMemberDetail.AddAsync(member);
                            await _context.SaveChangesAsync();
                        }

                        foreach (var item in model.AnugamanMemberList)
                        {
                            var amember = new NewToleBikashAnugamanMember()
                            {
                                NewToleBikashId = data.Id,
                                PostId = item.PostId,
                                Name = item.Name,
                                Address = item.Address,
                                Contact = item.Contact,
                                CreatedBy = _userId,
                                CreatedDate = DateTime.Now,
                            };
                            await _context.NewToleBikashAnugamanMember.AddAsync(amember);
                            await _context.SaveChangesAsync();
                        }

                        foreach (var item in model.YojanaIds)
                        {
                            var yoj = new NewToleBikashYojanas()
                            {
                                NewToleBikashId = data.Id,
                                YojanaId = item,
                            };
                            await _context.NewToleBikashYojanas.AddAsync(yoj);
                            await _context.SaveChangesAsync();
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

        public async Task<bool> DeleteNewToleBikashById(int id)
        {
            var data = await _context.NewToleBikash.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Status = false;
                data.UpdatedBy = _userId;
                data.UpdatedDate = DateTime.Now;

                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}