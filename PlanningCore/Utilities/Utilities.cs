using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Areas.Identity.Pages.Account;
using PlanningCore.Data;
using PlanningCore.Models;
using PlanningCore.Security;
using System.Security.Claims;
using static PlanningCore.Models.DashboardViewModel;

namespace PlanningCore.Utilities
{
    public class Utilities : IUtility
    {
        private readonly PlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly string userId = null;

        public Utilities(PlanningContext context, IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ILogger<RegisterModel> logger)
        {
            _context = context;
            _webHostEnvironment = webHost;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;

            userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<SelectList> GetStateSelectListItems()
        {
            return new SelectList(await _context.State.ToListAsync(), "StateId", "StateNameNep");
        }
        public async Task<SelectList> GetDistrictSelectListItems()
        {
            return new SelectList(await _context.District.ToListAsync(), "DistrictId", "DistrictNameNep");
        }

        public async Task<SelectList> GetDistrictByStateId(int? stateId)
        {
            var data = await _context.District.Where(x => (x.StateId == stateId || stateId == null)).ToListAsync();
            return new SelectList(await _context.District.Where(x => (x.StateId == stateId || stateId == null)).ToListAsync(), "DistrictId", "DistrictNameNep");
        }
        public async Task<SelectList> GetSubDepartmentListItems(int? depId)
        {
            return new SelectList(await _context.SubDepartment.Where(x => (x.DepartmentId == depId || depId == null)).ToListAsync(), "Id", "Name"); ;
        }
        public async Task<SelectList> GetPalikaSelectListItems()
        {
            return new SelectList(await _context.Palika.ToListAsync(), "PalikaId", "PalikaNameNep");
        }

        public async Task<SelectList> GetPalikaByDistrictId(int? distId)
        {
            return new SelectList(await _context.Palika.Where(x => (x.DistrictId == distId || distId == null)).ToListAsync(), "PalikaId", "PalikaNameNep");
        }

        public async Task<SelectList> GetDepartmentListItems(int? wardId)
        {
            return new SelectList(await _context.Department.Where(x => (x.WardId == wardId || wardId == null)).ToListAsync(), "Id", "Name"); ;
        }
        public async Task<SelectList> GetDepartmentList()
        {
            return new SelectList(await _context.Department.ToListAsync(), "Id", "Name"); ;
        }
        public async Task<SelectList> GetSubDepartmentList()
        {
            return new SelectList(await _context.SubDepartment.ToListAsync(), "Id", "Name"); ;
        }


        public async Task<List<SelectListItem>> GetSelectListWard()
        {
            var wards = await _context.Ward.Where(x => x.Status == true).ToListAsync();
            return wards.Select(ward => new SelectListItem
            {
                Value = ward.Id.ToString(),
                Text = ward.Name
            }).ToList();
        }

        public async Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile file)
        {
            try
            {
                FileUploadModel model = new FileUploadModel();
                string returnPath = null;
                if (file != null)
                {
                    var fileExt = Path.GetExtension(file.FileName).Substring(1);
                    folderName = string.IsNullOrEmpty(folderName) ? "images" : folderName;
                    folderName = (folderName == "images") ? "images/AppImage/" : "images/" + folderName + "/";
                    model.FileName = Guid.NewGuid().ToString() + "." + fileExt;
                    returnPath = folderName + model.FileName;

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);// if Path not present than create

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, returnPath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    model.FilePath = "/" + returnPath;
                    return model;
                }
                else
                    return model;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<string> UploadImgAsync(string folderName, IFormFile file)
        {
            try
            {
                string returnPath = null;
                if (file != null)
                {
                    var fileExt = Path.GetExtension(file.FileName).Substring(1);
                    folderName = string.IsNullOrEmpty(folderName) ? "images" : folderName;
                    folderName = (folderName == "images") ? "images/AppImage/" : "images/" + folderName + "/";
                    returnPath = folderName + Guid.NewGuid().ToString() + "." + fileExt;

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);// if Path not present than create

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, returnPath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return "/" + returnPath;
                }
                else
                    return returnPath;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<int> GetCurrentFiscalYear()
        {
            return await _context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Id).FirstOrDefaultAsync();
        }
        public async Task<string> GetCurrentFiscalYearName()
        {
            return await _context.FiscalYear.Where(x => x.IsActive == true).Select(x => x.Name).FirstOrDefaultAsync();
        }

        public async Task<SelectList> GetSelectListRoles()
        {
            return new SelectList(
                await _context.Roles
                    .Where(x =>
                        x.Name != UserRoles.Administrator &&
                        x.Name != UserRoles.SuperAdmin &&
                        x.Name != UserRoles.Contract_Admin   
                    )
                    .ToListAsync(),
                "Name",
                "Name"
            );
        }

        public async Task<List<UserViewModel>> GetUserList()
        {
            var data = await _context.Users.Where(x => !x.UserName.Equals("softech@gmail.com") && !x.UserName.Equals("superadmin@gmail.com"))
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.FullName,
                    UserName = x.UserName,
                    Role = (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == x.Id select r.Name).FirstOrDefault(),
                    WardId = x.WardId,
                    WardName = _context.Ward.Where(w => w.Id == x.WardId).Select(w => w.Name).FirstOrDefault(),
                    Active = (x.LockoutEnd ?? DateTime.UtcNow) <= DateTime.UtcNow
                }).ToListAsync();
            return data;
        }
        public string ConvertEnglishToNepali(object EnglishNumericValue)
        {
            if (EnglishNumericValue == null)
            {
                EnglishNumericValue = " ";
            }
            string Eng_Value = EnglishNumericValue.ToString(); // unicode  numeric chars
            string Nep_value = "";
            string[] Text_Nepali = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", ".", "/", "-" };
            string[] Text_English = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            char[] Inputtext = Eng_Value.ToString().ToCharArray();
            for (int j = 0; j < Eng_Value.Length; j++)
            {
                string v = Inputtext[j].ToString();
                var index = Array.IndexOf(Text_English, v);
                if (index >= 0)
                    Nep_value += Text_Nepali[index].ToString();
                else
                    Nep_value += v;
            }
            return Nep_value;
        }
        public async Task<bool> ResetUserPassword(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, "NewPass@123");
                if (result.Succeeded)
                {
                    _logger.LogInformation("Reset password - UserId = " + id + " Updated by - " + userId + " Date : " + DateTime.Now);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Reset password - Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<SelectList> GetYojanaAccToSamjhautaList()
        {
            var data = await (from p in _context.PlanningSamjhauta
                              where p.IsDeleted == false
                              select new PlanningSamjhautaViewModel()
                              {
                                  PlanningSamjhautaId = p.PlanningSamjhautaId,
                                  YojanaNames = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                              }).ToListAsync();
            return new SelectList(data, "PlanningSamjhautaId", "YojanaNames");
        }
        public async Task<bool> AssignOrChangeRole(string id, string role)
        {
            try
            {
                var roles = await _context.UserRoles.Where(x => x.UserId == id).ToListAsync();
                foreach (var item in roles)
                {
                    _context.UserRoles.Remove(item);
                    await _context.SaveChangesAsync();
                }
                var user = await _userManager.FindByIdAsync(id);
                var newUserRole = _userManager.AddToRoleAsync(user, role);
                newUserRole.Wait();

                if (newUserRole.IsCompletedSuccessfully)
                {
                    _logger.LogInformation("Assign Or Change Role - UserId = " + id + " Updated by - " + userId + " Date : " + DateTime.Now);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Assign Or Change Role - Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return false;
            }
        }
        public async Task<int> UserActivateOrDeactivate(string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    if (user.LockoutEnd == null)
                    {
                        user.LockoutEnd = DateTimeOffset.MaxValue;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("User Deactive - UserId = " + id + " Updated by - " + userId + " Date : " + DateTime.Now);
                        return 2;
                    }
                    else
                    {
                        user.LockoutEnd = null;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("User Active - UserId = " + id + " Updated by - " + userId + " Date : " + DateTime.Now);
                        return 1;
                    }
                }
                _logger.LogInformation("User not found - UserId = " + id + " Updated by - " + userId + " Date : " + DateTime.Now);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("User Active/DeActive - Error User Id = " + userId + " Date : " + DateTime.Now + " Error log : " + ex);
                return 0;
            }
        }

        public async Task<int?> GetCurrentPalikaId()
        {
            return await _context.MainSetting.Select(x => x.PalikaId).FirstOrDefaultAsync();
        }
        public async Task<int> GetCurrentDepartmentId()
        {
            return await _context.FiscalYear.Where(x => x.IsActive).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<SelectList> GetWardList()
        {
            int wardId = await GetWardNoForLogin_Role_User() ?? 0;
            return new SelectList(await _context.Ward.Where(x => x.Status != false && (wardId == 0 || x.Id == wardId)).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetFukuwaTypeList()
        {
            return new SelectList(await _context.Con_FukuwaType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetPaymentTypeList()
        {
            return new SelectList(await _context.PaymentType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetMyadThapTypeList()
        {
            return new SelectList(await _context.Con_MyadThapType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetPadaList()
        {
            return new SelectList(await _context.Pada.Where(x => x.IsDeleted == false).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetSchoolPostList()
        {
            return new SelectList(await _context.SchoolPost.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetSamitiPadaList()
        {
            return new SelectList(await _context.SamitiPost.ToListAsync(), "Id", "Name");
        }
        //public async Task<SelectList> GetNewTolBikashPostList()
        //{
        //    return new SelectList(await _context.SamitiPost.ToListAsync(), "Id", "Name");
        //}
        public async Task<SelectList> GetAnugamanSamitiPostList()
        {
            return new SelectList(await _context.AnugamanSamitiPost.ToListAsync(), "Id", "Name");
        }

        public async Task<SelectList> GetRolesSelectListItems()
        {
            var data = await _context.Roles.Where(x => x.Name != "SuperAdmin").ToListAsync();
            return new SelectList(data, "Name", "Name");
        }

        public async Task<SelectList> GetEmployeeList()
        {
            var data = await _context.Users.Where(x => !x.UserName.Equals("superadmin@gmail.com") && !x.UserName.Equals("superadmin@gmail.com"))
               .Select(x => new UserViewModel()
               {
                   Id = x.Id,
                   Name = x.FullName,
                   UserName = x.UserName,
                   Role = (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == x.Id select r.Name).FirstOrDefault(),
                   Active = (x.LockoutEnd ?? DateTime.UtcNow) <= DateTime.UtcNow

               }).ToListAsync();
            return new SelectList(data, "Id", "Name");
        }
        public async Task<SelectList> EmployeeFilter(int? wardId, int? depId, int? subId)
        {
            var data = await _context.Users.Where(x => !x.UserName.Equals("softech@gmail.com") && !x.UserName.Equals("superadmin@gmail.com"))
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.FullName,
                    UserName = x.UserName,
                    Role = (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id where ur.UserId == x.Id select r.Name).FirstOrDefault(),
                    //WardId = x.WardId,
                    Active = (x.LockoutEnd ?? DateTime.UtcNow) <= DateTime.UtcNow
                }).ToListAsync();
            if (wardId > 0)
            {
                data = data.Where(x => x.WardId == wardId).ToList();
            }

            return new SelectList(data, "Id", "Name");
        }

        public async Task<string> GetUsername(string id)
        {
            var data = await _context.Users.Where(x => x.Id == id).Select(x => x.FullName).FirstOrDefaultAsync();
            return data;
        }
        //public async Task<int> GetUserWard(string id)
        //{
        //    var data = await _context.Users.Where(x => x.Id == id).Select(x => x.WardId).FirstOrDefaultAsync();
        //    return data ?? 0;
        //}
        #region MainSetting
        public async Task<List<MainSettingViewModel>> GetAllMainSettings()
        {
            return await _context.MainSetting
                .Select(x => new MainSettingViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    Address2 = x.Address2,
                    ContactName = x.ContactName,
                    ContactNumber = x.ContactNumber,
                    MobileNumber = x.MobileNumber,
                    FaxNo = x.FaxNo,
                    Website = x.Website,
                    LogoPath = x.LogoPath,
                    PalikaType = x.PalikaType,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    PalikaId = x.PalikaId,
                    StateName = _context.State.Where(a => a.StateId == x.StateId).Select(a => a.StateNameNep).FirstOrDefault(),
                    DistrictName = _context.District.Where(a => a.DistrictId == x.DistrictId).Select(a => a.DistrictNameNep).FirstOrDefault(),
                    PalikaName = _context.Palika.Where(a => a.PalikaId == x.PalikaId).Select(a => a.PalikaNameNep).FirstOrDefault(),

                }).ToListAsync();
        }
        public async Task<SelectList> GetPalikaType(int id)
        {
            var palikaTypes = new[]
            {
            new { Id = "1", Value = "नगर" },
            new { Id = "2", Value = "गाँउ" },
             };

            return await Task.FromResult(new SelectList(palikaTypes, "Id", "Value"));
        }
        public string GetPalikaTypeName(int id)
        {
            if (id == 1)
            {
                return "नगर";
            }
            else
            {
                return "गाँउ";
            }
        }
        public async Task<MainSettingViewModel> GetMainSettingById(int id)
        {
            var data = await _context.MainSetting
                .Select(x => new MainSettingViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    Address2 = x.Address2,
                    ContactName = x.ContactName,
                    ContactNumber = x.ContactNumber,
                    MobileNumber = x.MobileNumber,
                    FaxNo = x.FaxNo,
                    Website = x.Website,
                    LogoPath = x.LogoPath,
                    PalikaType = x.PalikaType,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    PalikaId = x.PalikaId,
                    StateName = x.State.StateNameNep,
                    DistrictName = x.District.DistrictNameNep,
                    PalikaName = x.Palika.PalikaNameNep,
                    PalikaNameEng = x.Palika.PalikaName,
                    StateNameEng = x.State.StateName,
                    DistrictNameEng = x.District.DistrictName,
                    EnglishLetterHead = x.EnglishLetterHead,
                }).FirstOrDefaultAsync() ?? new MainSettingViewModel();
            data.PalikaTypeName = GetPalikaTypeName(data.PalikaType);
            return data;
        }
        public async Task<bool> InsertUpdateMainSetting(MainSettingViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    var photo = await UploadImgAsync(null, model.ProfileImage);
                    var data = _context.MainSetting.FirstOrDefault(x => x.Id == model.Id);
                    if (data != null)
                    {
                        data.Name = model.Name;
                        data.Email = model.Email;
                        data.ContactName = model.ContactName;
                        data.ContactNumber = model.ContactNumber;
                        data.MobileNumber = model.MobileNumber;
                        data.Address = model.Address;
                        data.Address2 = model.Address2;
                        data.FaxNo = model.FaxNo;
                        data.Website = model.Website;
                        data.PalikaType = model.PalikaType;
                        data.LogoPath = photo;
                        data.StateId = model.StateId;
                        data.DistrictId = model.DistrictId;
                        data.PalikaId = model.PalikaId;
                        data.EnglishLetterHead = model.EnglishLetterHead;

                        _context.Entry(data).State = EntityState.Modified;
                    }

                    else
                        return false;
                }
                else
                {
                    var siteSetting = new MainSetting()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        ContactName = model.ContactName,
                        ContactNumber = model.ContactNumber,
                        MobileNumber = model.MobileNumber,
                        Address = model.Address,
                        Address2 = model.Address2,
                        FaxNo = model.FaxNo,
                        Website = model.Website,
                        PalikaType = model.PalikaType,
                        StateId = model.StateId,
                        DistrictId = model.DistrictId,
                        PalikaId = model.PalikaId,
                        EnglishLetterHead = model.EnglishLetterHead,
                    };
                    siteSetting.LogoPath = await UploadImgAsync(null, model.ProfileImage);
                    await _context.MainSetting.AddAsync(siteSetting);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region WardSetting
        public async Task<List<WardViewModel>> GetAllWards()
        {
            return await _context.Ward.Where(x => x.Status == true)
                .Select(x => new WardViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Name_En = x.Name_En,
                    Code = x.Code,
                    Address = x.Address
                }).ToListAsync();
        }
        public async Task<WardViewModel> GetWardById(int id)
        {
            return await _context.Ward.Where(x => x.Id == id && x.Status == true)
                       .Select(x => new WardViewModel()
                       {
                           Id = x.Id,
                           Name = x.Name,
                           Name_En = x.Name_En,
                           Code = x.Code,
                           Address = x.Address,
                           AddressEng = x.AddressEng,
                           Adakshya = x.Adakshya,
                           Sachib = x.Sachib,
                           Koshadhaskhya = x.Koshadhaskhya,
                           AdakshyaContact = x.AdakshyaContact,
                           SachibContact = x.SachibContact,
                           KoshadhaskhyaContact = x.KoshadhaskhyaContact,
                       }).FirstOrDefaultAsync() ?? new WardViewModel();
        }
        public async Task<bool> InsertUpdateWard(WardViewModel model)
        {
            try
            {
                var data = await _context.Ward.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data != null)
                {
                    data.Name = model.Name;
                    data.Name_En = model.Name_En;
                    data.Code = model.Code;
                    data.Address = model.Address;
                    data.AddressEng = model.AddressEng;
                    data.Adakshya = model.Adakshya;
                    data.SachibContact = model.SachibContact;
                    data.Sachib = model.Sachib;
                    data.KoshadhaskhyaContact = model.KoshadhaskhyaContact;
                    data.AdakshyaContact = model.AdakshyaContact;
                    data.Koshadhaskhya = model.Koshadhaskhya;
                    _context.Entry(data).State = EntityState.Modified;
                }
                else
                {
                    data = await _context.Ward.FirstOrDefaultAsync(x => x.Name.Trim().Equals(model.Name));
                    if (data != null)
                    {
                        data.Status = true;
                        _context.Entry(data).State = EntityState.Modified;
                    }
                    else
                    {
                        var WardSetting = new Ward()
                        {
                            Name = model.Name,
                            Name_En = model.Name_En,
                            Code = model.Code,
                            Address = model.Address,
                            AddressEng = model.AddressEng,
                            Adakshya = model.Adakshya,
                            SachibContact = model.SachibContact,
                            Sachib = model.Sachib,
                            KoshadhaskhyaContact = model.KoshadhaskhyaContact,
                            AdakshyaContact = model.AdakshyaContact,
                            Koshadhaskhya = model.Koshadhaskhya,
                            Status = true,
                        };
                        await _context.Ward.AddAsync(WardSetting);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteWard(int id)
        {
            var data = await _context.Ward.FirstOrDefaultAsync(x => x.Id == id);
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

        #region Numeric To Text
        public string ConvertNumberToNepaliText(string paramNumber)
        {
            if (!string.IsNullOrWhiteSpace(paramNumber))
            {
                string[] value = paramNumber.Split('.');
                string rupeesFormat = NumericToText.ConvertToNepaliWord_Rupees(value[0]);
                string paisaFormat;
                if (value.Length == 1)
                {
                    paisaFormat = "मात्र";
                    return rupeesFormat + paisaFormat;
                }
                else
                {
                    paisaFormat = NumericToText.ConvertToNepali_Paisa(Convert.ToInt32(value[1]));
                    return rupeesFormat + paisaFormat + " पैसा मात्र";
                }
            }
            else
            {
                return "N/A";
            }
        }


        public class NumericToText
        {
            static readonly string[] NumToWord =
                new string[] {
                    "शुन्य","एक","दुई","तिन","चार","पांच","छ","सात", "आठ","नैा","दस",
                    "एघार","बार्ह","तेर्ह","चैाध","पन्ध्र","सोर्ह","सत्र","आठार","उन्नाइस","बिस",
                    "एक्काइस","बाइस","तेइस","चैाबिस","पच्चिस","छब्बिस","सत्ताइस","अठ्ठाईस","उनान्तिस","तिस",
                    "एकतिस","बत्तिस","तेत्तिस","चौतिस","पैतिस","छत्तिस","सइतिस","अड्तिस","उन्चालिस","चालिस",
                    "एकचालिस","बयालिस","त्रिचालिस","चौवालिस","पैतालिस","छयालिस","सच्चालिस","अड्चालिस","उन्पचास","पचास",
                    "एकाउन्न","बाउन्न","त्रिपन्न","चौवन्न","पचपन्न","छपन्न","सन्ताउन्न","अनठाउन्न","उन्ननसाठी","साठी",
                    "एकसठ्ठी","वैसठ्ठी","त्रीसठ्ठी","चौसठ्ठी","पैसठ्ठी","छैसठ्ठी","सड्सठ्ठी","अड्सठ्ठी","उनन्सत्तरी","सत्तरी",
                    "एकत्तर","बहत्तर","त्रिहत्तर","चौहत्तर","पचत्तर","छयत्तर","सतत्तर","अठ्त्तर","उनासी","असि",
                    "एकासी","बयासी","त्रीयासी","चौरासी","पचासी","छयासी","सतासी","अठासी","उन्नब्बे","नब्बे",
                    "एकानब्बे","बयानब्बे","त्रियानब्बे","चौरानब्बे","पन्चानब्बे","छयानब्बे","सन्तानब्बे","अन्ठानब्बे","उनान्सय"
                };
            // add more
            static readonly string[] Num_Unit = new string[] { "सय", "हजार", "लाख", "करोड", "अर्ब", "खर्ब", "शंख" };

            public static string ConvertToNepali_Paisa(int paramNumber)
            {
                return NumToWord[paramNumber];
            }

            public static string Common(string ten_, string hun_, string thou_, string lakh_, string karod_, string araba_)
            {
                int ten = 0, hun = 0, thou = 0, lakh = 0, karod = 0;
                if (ten_ != "")
                {
                    ten = Convert.ToInt32(ten_);
                }

                if (hun_ != "")
                {
                    hun = Convert.ToInt32(hun_);
                }
                if (thou_ != "")
                {
                    thou = Convert.ToInt32(thou_);
                }
                if (lakh_ != "")
                {
                    lakh = Convert.ToInt32(lakh_);
                }
                if (karod_ != "")
                {
                    karod = Convert.ToInt32(karod_);
                }

                string str_word = "";
                if (karod_ != "" && karod != 0)
                {
                    if (lakh_ != "" && lakh != 0)
                    {
                        if (thou_ != "" && thou != 0)
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " +
                           NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                            + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                            + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                              NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                              + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {

                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                                        NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                                        + NumToWord[ten];
                                }
                                else
                                {

                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1];
                                }

                            }
                        }
                        else
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {

                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                             + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                             + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                               + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2];
                                }

                            }

                        }
                    }
                    else
                    {
                        if (thou_ != "" && thou != 0)
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "")
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                              + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                             + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1];
                                }
                            }
                        }
                        else
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3];
                                }

                            }

                        }

                    }

                }
                else
                {
                    if (lakh_ != "" && lakh != 0)
                    {
                        if (thou_ != "" && thou != 0)
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                            + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                            + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                            + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                            + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " " +
                            NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1];
                                }

                            }
                        }
                        else
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                              + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                               + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }

                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2] + " "
                              + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[lakh] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[2];
                                }
                            }

                        }
                    }
                    else
                    {
                        if (thou_ != "" && thou != 0)
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                             + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                             + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                               + NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }
                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1] + " "
                             + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[thou] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[1];
                                }
                            }
                        }
                        else
                        {
                            if (hun_ != "" && hun != 0)
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0] + " "
                             + NumToWord[ten];
                                }
                                else
                                {
                                    str_word = NumToWord[hun] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[0];
                                }
                            }
                            else
                            {
                                if (ten_ != "" && ten != 0)
                                {
                                    str_word = NumToWord[ten];
                                }
                                else
                                {
                                    str_word = "";
                                }
                            }
                        }
                    }
                }

                return str_word;
            }

            public static string ConvertToNepaliWord_Rupees(string paramNumber)
            {
                string dec_word = paramNumber.ToString(), str_word = "";
                string str_word_pre = "", str_word_post = " रुपैंया ";
                string sOne, sTen, sHun, sThou, sLakh, sKarod;//, sAraba;
                if (dec_word.Length == 1)
                {
                    str_word = NumToWord[Convert.ToInt32(paramNumber)];
                }
                else if (dec_word.Length == 2)
                {
                    str_word = NumToWord[Convert.ToInt32(paramNumber)];
                }

                else if (dec_word.Length == 3)
                {
                    sTen = dec_word.Substring(0, 1);
                    sOne = dec_word.Substring(1, 2);
                    int ten = Convert.ToInt32(sTen);
                    int one = Convert.ToInt32(sOne); // र 
                    if (one != 0)
                    {
                        str_word = str_word = Common(sTen, "", "", "", "", "") + " " + Num_Unit[0] + " " + NumToWord[one];
                    }
                    else
                    {
                        str_word = str_word = Common(sTen, "", "", "", "", "") + " " + Num_Unit[0];
                    }
                }
                else if (dec_word.Length == 4)
                {
                    sThou = dec_word.Substring(0, 1);
                    sHun = dec_word.Substring(1, 1);
                    sTen = dec_word.Substring(2, 2);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    str_word = Common(sTen, sHun, sThou, "", "", "");


                }
                else if (dec_word.Length == 5)
                {

                    sThou = dec_word.Substring(0, 2);
                    sHun = dec_word.Substring(2, 1);
                    sTen = dec_word.Substring(3, 2);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    str_word = Common(sTen, sHun, sThou, "", "", "");
                }
                else if (dec_word.Length == 6)
                {
                    sLakh = dec_word.Substring(0, 1);
                    sThou = dec_word.Substring(1, 2);
                    sHun = dec_word.Substring(3, 1);
                    sTen = dec_word.Substring(4, 2);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    str_word = Common(sTen, sHun, sThou, sLakh, "", "");
                }
                else if (dec_word.Length == 7)
                {
                    sLakh = dec_word.Substring(0, 2);
                    sThou = dec_word.Substring(2, 2);
                    sHun = dec_word.Substring(4, 1);
                    sTen = dec_word.Substring(5, 2);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    str_word = Common(sTen, sHun, sThou, sLakh, "", "");
                }
                else if (dec_word.Length == 8)
                {
                    sKarod = dec_word.Substring(0, 1);
                    sLakh = dec_word.Substring(1, 2);
                    sThou = dec_word.Substring(3, 2);
                    sHun = dec_word.Substring(5, 1);
                    sTen = dec_word.Substring(6, 2);
                    int karod = Convert.ToInt32(sKarod);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    str_word = NumToWord[karod] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[3] + " " +
                        Common(sTen, sHun, sThou, sLakh, "", "");
                }

                else if (dec_word.Length == 9)
                {

                    sKarod = dec_word.Substring(0, 2);
                    sLakh = dec_word.Substring(2, 2);
                    sThou = dec_word.Substring(4, 2);
                    sHun = dec_word.Substring(6, 1);
                    sTen = dec_word.Substring(7, 2);
                    int karod = Convert.ToInt32(sKarod);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);
                    // र 
                    str_word = Common(sTen, sHun, sThou, sLakh, sKarod, "");
                }
                else if (dec_word.Length == 10)
                {
                    sKarod = dec_word.Substring(1, 2);
                    sLakh = dec_word.Substring(3, 2);
                    sThou = dec_word.Substring(5, 2);
                    sHun = dec_word.Substring(7, 1);
                    sTen = dec_word.Substring(8, 2);
                    int arba = Convert.ToInt32(dec_word.Substring(0, 1));
                    int karod = Convert.ToInt32(sKarod);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);


                    str_word = NumToWord[arba] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[4] + " " +
                         Common(sTen, sHun, sThou, sLakh, sKarod, "");
                }
                else if (dec_word.Length == 11)
                {
                    sKarod = dec_word.Substring(2, 2);
                    sLakh = dec_word.Substring(4, 2);
                    sThou = dec_word.Substring(6, 2);
                    sHun = dec_word.Substring(8, 1);
                    sTen = dec_word.Substring(9, 2);
                    int arba = Convert.ToInt32(dec_word.Substring(0, 2));
                    int karod = Convert.ToInt32(sKarod);
                    int lakh = Convert.ToInt32(sLakh);
                    int thou = Convert.ToInt32(sThou);
                    int hun = Convert.ToInt32(sHun);
                    int ten = Convert.ToInt32(sTen);


                    str_word = NumToWord[arba] + " ‌‌‌‌‌‌‌‌‌‌" + Num_Unit[4] + " " + Common(sTen, sHun, sThou, sLakh, sKarod, "");
                }
                else
                {
                    int size_of_arr = (int)(paramNumber.ToString().Length / 2 - 3);
                    int[] nums = new int[size_of_arr];
                }
                return str_word_pre + str_word + str_word_post;
            }

        }

        public string ConvertEnglishToNepali1(object text)
        {
            var result = "";
            var inputText = text?.ToString();
            if (string.IsNullOrWhiteSpace(inputText)) return result;
            string[] nepaliText = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", ".", "/", "-" };
            string[] englishText = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            char[] inputArray = inputText.ToCharArray();
            for (int i = 0; i < inputText.Length; i++)
            {
                for (int j = 0; j < englishText.Length; j++)
                {
                    if (inputArray[i].ToString() == englishText[j])
                    {
                        result += nepaliText[j];
                    }
                }
            }
            var afterRemove = result.Replace("/", "");
            afterRemove = afterRemove.Replace(".", "");
            afterRemove = afterRemove.Replace("-", "");
            if (!string.IsNullOrWhiteSpace(inputText) && (string.IsNullOrWhiteSpace(result) || string.IsNullOrWhiteSpace(afterRemove)))
            {
                return inputText;
            }
            return result;
        }
        #endregion
        public async Task<SelectList> GetChhetraList()
        {
            return new SelectList(await _context.Chettra.Where(x => x.IsDeleted == false).ToListAsync(), "ChettraId", "ChettraName");
        }
        public async Task<SelectList> GetUpaChhetraList()
        {
            return new SelectList(await _context.UpaChetra.Where(x => x.IsDeleted == false).ToListAsync(), "UpaChettraId", "UpaChettra");
        }
        public async Task<SelectList> GetUpaChhetraDetailsList(int? id)
        {
            id = id ?? 0;
            return new SelectList(await _context.UpaChetraDetail.Where(x => x.IsDeleted == false && (id == 0 || x.UpaChetraId == id)).ToListAsync(), "UpaChetraDetailId", "Name");
        }

        public async Task<SelectList> GetUpaBhoktaSamitiList()
        {
            return new SelectList(await _context.UpabhoktaSamitiDetail.ToListAsync(), "UpabhoktaSamitiDetailId", "Name");
        }
        public async Task<SelectList> GetNewTolBikashList()
        {
            return new SelectList(await _context.NewToleBikash.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetTolBikashList()
        {
            return new SelectList(await _context.TolBikashSanstha.ToListAsync(), "TolBikashSansthaId", "TolBikashSansthaName");
        }
        public async Task<SelectList> GetyojanaList()
        {
            return new SelectList(await _context.YojanaSetup.Where(x => x.IsDeleted == false).ToListAsync(), "YojanaSetupId", "YojanaName");
        }
        public async Task<SelectList> GeUnitList()
        {
            return new SelectList(await _context.Unit.Where(x => x.IsDeleted == false).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetThekkaYojanaList()
        {
            return new SelectList(await _context.Con_Yojana.Where(x => x.IsDeleted == false).ToListAsync(), "Id", "YojanaName");
        }
        public async Task<SelectList> GetVariationTypeList()
        {
            return new SelectList(await _context.Con_VariationType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetThekkaTypeList()
        {
            return new SelectList(await _context.Con_ThekkaType.ToListAsync(), "Id", "Name");
        }

        public async Task<SelectList> GetBankGuranteeTypeList()
        {
            return new SelectList(await _context.Con_BankGuaranteeType.ToListAsync(), "Id", "Name");
        }

        public async Task<SelectList> GetFiscalYearList()
        {
            return new SelectList(await _context.FiscalYear.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetBudgetSourceList()
        {
            var fiscalId = await GetCurrentFiscalYear();
            return new SelectList(await _context.BudgetSource.Where(x => x.FiscalYearId == fiscalId && x.IsDeleted == false).ToListAsync(), "BudgetSourceId", "BudgetSourceName");
        }
        public async Task<SelectList> GetPlanningTypeList()
        {
            return new SelectList(await _context.PlanningType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetWorkType()
        {
            return new SelectList(await _context.WorkType.ToListAsync(), "WorkTypeId", "WorkTypeName");
        }
        public async Task<SelectList> GetContractType()
        {
            return new SelectList(await _context.Con_ContractType.ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetContractReportType()
        {
            return new SelectList(await _context.Con_ReportType.ToListAsync(), "Id", "ReportName_Nep");
        }


        public async Task<SelectList> GetSartaList()
        {
            return new SelectList(await _context.SartaSetup.Where(x=>x.Status==false).ToListAsync(), "SartaSetupId", "Name");
        }
        //public async Task<SelectList> GetUpabhoktaMemberPost()
        //{
        //	return new SelectList(await _context.UpabhoktaSamitiMemberDetail.ToListAsync(), "UpabhoktaSamitiMemberDetailId", "YojanaName");
        //}
        public async Task<SelectList> GetUpabhoktaMemberName()
        {
            return new SelectList(await _context.UpabhoktaSamitiMemberDetail.ToListAsync(), "UpabhoktaSamitiMemberDetailId", "MemberName");
        }
        public async Task<SelectList> GetUpabhoktaMemberPost()
        {
            return new SelectList(await _context.SamitiPost.ToListAsync(), "Id", "Name");
        }

        #region Header
        public async Task<MainSettingViewModel> GetHeaderData()
        {
            var data = await _context.MainSetting
                .Select(x => new MainSettingViewModel()
                {
                    Name = x.Name,
                    Address = x.Address,
                    Address2 = x.Address2,
                    PalikaType = x.PalikaType,
                    PalikaId = x.PalikaId,
                    StateId = x.StateId,
                    DistrictId = x.DistrictId,
                    StateName = x.State.StateNameNep,
                    EnglishLetterHead = x.EnglishLetterHead,
                    LogoPath = x.LogoPath,
                    DistrictName = x.District.DistrictNameNep,
                    PalikaName = x.Palika.PalikaNameNep,
                    StateNameEng = x.State.StateName,
                    DistrictNameEng = x.District.DistrictName,
                    PalikaNameEng = x.Palika.PalikaName,
                    PalikaTypeName = x.PalikaType == 1 ? "नगर" : "गाँउ",
                    PalikaTypeNameEng = x.PalikaType == 1 ? "Office Of The  Municipal Executive" : "Office Of The Rural Municipal Executive",
                }).FirstOrDefaultAsync();

            if (_httpContextAccessor.HttpContext.User.IsInRole("User") || _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                var wardId = await GetWardNoForLogin_Role_User();
                var ward = await _context.Ward.Where(x => x.Id == wardId).FirstOrDefaultAsync();
                if (ward != null)
                {
                    data.Address = ward?.Address;
                    data.WardName = ward?.Name;
                }
            }
            return data;
        }
        #endregion
        public async Task<SelectList> GetEmpList()
        {
            return new SelectList(await _context.Employee.Where(x => x.IsDeleted == false).ToListAsync(), "Id", "Name");
        }

        public async Task<SelectList> GetEmpNameByPostId(int? PostId)
        {
            return new SelectList(await _context.Employee.Where(x => x.IsDeleted == false && (x.PadaId == PostId || PostId == null)).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetEmpNameBySmitiPostId(int? PostId)
        {
            return new SelectList(await _context.UpabhoktaSamitiMemberDetail.Where(x => (x.SamitiPostId == PostId || PostId == null)).ToListAsync(), "Id", "Name");
        }
        public async Task<SelectList> GetDocumentType()
        {
            return new SelectList(await _context.DocumentType.ToListAsync(), "DocumentTypeId", "DocumentTypeName");
        }
        public async Task<SelectList> GetBhuktaniType()
        {
            return new SelectList(await _context.BhuktaniType.ToListAsync(), "BhuktaniTypeId", "BhuktaniTypeName");
        }
        public async Task<SelectList> GetSamjhautaList()
        {
            return new SelectList(await _context.PlanningSamjhauta.ToListAsync(), "YojanaId", "SamitiDetailId");
        }

        public string ConvertNepaliToEnglish(string NepaliNumericValue)
        {
            if (NepaliNumericValue == null)
            {
                return 0.ToString();
            }
            int k = 0;
            string Nepali_Value = NepaliNumericValue;
            string Eng_Value = "";
            string[] Text_English = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            string[] Text_Nepali = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", ".", "/", "-" };
            char[] InputText = NepaliNumericValue.ToString().ToCharArray();
            for (int j = 0; j < Nepali_Value.Length; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    string value = Text_Nepali[i].ToString();
                    string value1 = InputText[j].ToString();
                    if (value == value1)
                    {
                        Eng_Value += Text_English[i].ToString();
                        k++;
                    }
                }
                if (k == 0)
                {
                    return Eng_Value = Nepali_Value;
                }
            }
            return Eng_Value;
        }

        public bool CheckNumber(string Number)
        {
            var data = false;
            if (Number == null || Number == "")
            {
                Number = 0.ToString();
            }
            string value = Number;
            string[] Text_Nepali = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९" };
            //  string[] Text_English = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            for (int j = 0; j < value.Length; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    string val = Text_Nepali[i].ToString();
                    string value1 = value[j].ToString();
                    if (value1 == val)
                    {
                        return data = true;
                    }
                }
            }
            return data;
        }
        public async Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile file, string name)
        {
            try
            {
                var model = new FileUploadModel();
                string defaultFolder = "UploadAllFiles";
                if (file == null) return model;
                name = string.IsNullOrEmpty(name) ? "Planning" : name;
                var fileExt = Path.GetExtension(file.FileName).Substring(1);
                folderName = string.IsNullOrEmpty(folderName) ? defaultFolder : folderName;
                folderName = folderName.Equals(defaultFolder) ? $"{defaultFolder}/AppImage/" : $"{defaultFolder}/{folderName}/";
                model.FileName = name + "_" + Guid.NewGuid().ToString() + "." + fileExt;
                var returnPath = folderName + model.FileName;

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);// if Path not present than create

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, returnPath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.FilePath = "/" + returnPath;
                return model;
            }
            catch (Exception ex)
            {
                return new FileUploadModel();
            }
        }

        public async Task<string> GetEmpNameById(int? id)
        {
            return await _context.Employee.Where(x => x.Id == id && x.IsDeleted == false).Select(x => x.Name).FirstOrDefaultAsync();
        }
        public async Task<string> GetPostById(int? id)
        {
            return await _context.Pada.Where(x => x.Id == id && x.IsDeleted == false).Select(x => x.Name).FirstOrDefaultAsync();
        }

        //public async Task<EmployeeViewModel> GetEmployeeNamePost(int? id)
        //{
        //	var data = await (from p in _context.Pada
        //					  join e in _context.Employee on p.Id equals e.PadaId into padas
        //					  from e in padas.DefaultIfEmpty()
        //					  where p.Id == id
        //					  select new EmployeeViewModel
        //					  {
        //						  Id = p.Id,
        //						  Name = e.Name,
        //						  PadaId=p.Id,
        //						  PadaName=_context.Pada.Where(x=>x.Id==p.Id).Select(x=>x.Name).FirstOrDefault(),
        //					  }).FirstOrDefaultAsync() ?? new EmployeeViewModel();
        //	return data ?? new EmployeeViewModel();
        //}

        public Task RemoveFileFormServer(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return Task.CompletedTask;
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot" + path);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("File Remove Error : " + ex.Message);
            }
            return Task.CompletedTask;
        }
        public async Task<bool> PrintReport(int PlanningSamjhautaId, string ReportName, string PrintContent)
        {
            try
            {
                if (PrintContent != null)
                {
                    var data = new PrintReport()
                    {
                        PlanningSamjhautaId = PlanningSamjhautaId,
                        PrintContent = PrintContent,
                        ReportName = ReportName,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.PrintReport.AddAsync(data);
                    await _context.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> PrintReportUpabhota(int UpabhoktaSamitiDetailId, string ReportName, string PrintContent)
        {
            try
            {
                if (PrintContent != null)
                {
                    var data = new PrintReportUpabhokta()
                    {
                        UpabhoktaSamitiDetailId = UpabhoktaSamitiDetailId,
                        PrintContent = PrintContent,
                        ReportName = ReportName,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.PrintReportUpabhokta.AddAsync(data);
                    await _context.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<string> GetPrintReportContent(int PlanningSamjhautaId, string ReportName)
        {
            return await _context.PrintReport.Where(x => x.PlanningSamjhautaId == PlanningSamjhautaId && x.ReportName.Equals(ReportName)).Select(x => x.PrintContent).FirstOrDefaultAsync();
        }
        public async Task<string> GetPrintReportContentUpabhokta(int UpabhoktaSamitiDetailId, string ReportName)
        {
            return await _context.PrintReportUpabhokta.Where(x => x.UpabhoktaSamitiDetailId == UpabhoktaSamitiDetailId && x.ReportName.Equals(ReportName)).Select(x => x.PrintContent).FirstOrDefaultAsync();
        }
        //public async Task<string> GetPrintReportContentNewTolBikash(int Id, string ReportName)
        //{
        //    return await _context.PrintReportUpabhokta.Where(x => x.UpabhoktaSamitiDetailId == UpabhoktaSamitiDetailId && x.ReportName.Equals(ReportName)).Select(x=> x.PrintContent).FirstOrDefaultAsync();
        //}
        public async Task<bool> ContractPrintReportDetail(Con_PrintReportDetailViewModel model)
        {
            try
            {
                if (model != null)
                {
                    int? getConSamjhautaId = _context.Con_Samjhauta.Where(x => x.YojanaId == model.YojanaId).Select(x => x.Id).FirstOrDefault();
                    var data = new Con_PrintReportDetail()
                    {
                        ContractSamjhautaId = getConSamjhautaId,
                        ReportTypeId = model.ReportTypeId,
                        YojanaId = model.YojanaId,
                        ReportCode = model.ReportCode,
                        PrintContent = model.PrintContent,
                        PrintDate = model.PrintDate,
                        PrintDateEng = model.PrintDateEng,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    await _context.Con_PrintReportDetail.AddAsync(data);
                    await _context.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<SelectList> GetEmpNameBySamitiPostId(int? PostId, int? samitiId)
        {
            return new SelectList(await _context.UpabhoktaSamitiMemberDetail.Where(x => (x.SamitiPostId == PostId || PostId == null) && (x.UpabhoktaSamitiDetailId == samitiId || samitiId == null)).ToListAsync(), "UpabhoktaSamitiMemberDetailId", "MemberName");
        }

        public async Task<SelectList> GetEmpNameBySchoolPostId(int? PostId, int? samitiId)
        {
            return new SelectList(await _context.TolBikashSansthaMember.Where(x => (x.SchoolPostId == PostId || PostId == null) && (x.TolBikashSansthaId == samitiId || samitiId == null)).ToListAsync(), "TolBikashSansthaId", "Name");
        }

        public async Task<SelectList> GetEmpNameByTolBikashPostId(int? PostId, int? samitiId)
        {
            return new SelectList(await _context.NewToleBikashMemberDetail.Where(x => (x.SamitiPostId == PostId || PostId == null) && (x.NewToleBikashlId == samitiId || samitiId == null)).ToListAsync(), "Id", "MemberName");
        }

        public async Task<SelectList> GetBankList()
        {
            return new SelectList(await _context.Class_A_Bank_List.Where(x => x.Status == true).ToListAsync(), "Class_A_Bank_List_Id", "BankName_Nep");
        }
        public async Task<SelectList> GetConsultantList()
        {
            return new SelectList(await _context.Con_Consultant.Where(x => x.Status == true).ToListAsync(), "Id", "Name");
        }


        public async Task<SelectList> GetEmployeer()
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data.Add(new SelectListItem() { Text = "Employeer", Value = "Employeer" });
            data.Add(new SelectListItem() { Text = "Purchaser", Value = "Purchaser" });
            return new SelectList(data, "Value", "Text");
        }
        public async Task<SelectList> GetSupplier()
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data.Add(new SelectListItem() { Text = "Contractor", Value = "Contractor" });
            data.Add(new SelectListItem() { Text = "Supplier", Value = "Supplier" });
            return new SelectList(data, "Value", "Text");
        }
        public async Task<List<PieChartViewModel>> GetSamjhautaPieChart()
        {
            var data = new List<PieChartViewModel>();
            var yojana = await _context.YojanaSetup.Where(x => x.IsDeleted == false).CountAsync();
            var samjhauta = await _context.PlanningSamjhauta.Where(x => x.Status == true).CountAsync();
            var bhuktani = await _context.PlanningBhuktani.Select(x => x.PlanningSamjhautaId).CountAsync();
            var nonsamjhauta = yojana - samjhauta;
            data.Add(new PieChartViewModel() { Name = "योजना", Amount = yojana });
            data.Add(new PieChartViewModel() { Name = "सम्झौता", Amount = samjhauta });
            data.Add(new PieChartViewModel() { Name = "सम्झौता नभएका", Amount = nonsamjhauta });
            data.Add(new PieChartViewModel() { Name = "भुक्तानि भएका योजना", Amount = bhuktani });
            return data;
        }
        public async Task<List<PieChartViewModel>> GetThekkaPieChart()
        {
            var data = new List<PieChartViewModel>();
            var yojana = await _context.YojanaSetup.Where(x => x.IsDeleted == false).CountAsync();
            var samjhauta = await _context.Con_Samjhauta.CountAsync();
            var contractor = await _context.Con_Consultant.Where(x => x.Status == true).CountAsync();
            data.Add(new PieChartViewModel() { Name = "योजना", Amount = yojana });
            data.Add(new PieChartViewModel() { Name = "सम्झौता भएका ठेक्काहरु", Amount = samjhauta });
            data.Add(new PieChartViewModel() { Name = "जम्मा निर्माण व्यवसायी", Amount = contractor });

            return data;
        }
        public async Task<string> GetThekkaReportCodeById(int reportId)
        {
            var data = await _context.Con_ReportType.Where(x => x.Id == reportId).Select(x => x.ReportCode).FirstOrDefaultAsync();
            return data;
        }
        //Merge Later
        public async Task<SelectList> GetYojanaNotContractList(int id)
        {
            int? wardId = await GetWardNoForLogin_Role_User();
            var data = _context.YojanaSetup.Where(x => x.IsDeleted == false && (wardId == 0 || x.WardId == wardId)).Select(x => new YojanaSetup() { YojanaSetupId = x.YojanaSetupId, YojanaName = (x.YojanaName + " (" + x.EstimatedAmount + ")") }).ToListAsync();

            return new SelectList(await data, "YojanaSetupId", "YojanaName");
        }
        public async Task<SelectList> GetUpabhhoktaSamitiList(int id)
        {
            int? wardId = await GetWardNoForLogin_Role_User();

            // Retrieve and format the YojanaName list
            var data = await _context.YojanaSetup
                .Where(x => !x.IsDeleted && (wardId == 0 || x.WardId == wardId))
                .Select(x => new
                {
                    YojanaName = x.YojanaName + " " + "उपभोक्ता समिति"
                })
                .ToListAsync();

            return new SelectList(data, "YojanaName", "YojanaName");
        }

        public async Task<SelectList> GetNonContractSamiti(int id)
        {

            var data = _context.UpabhoktaSamitiDetail.Where(x =>
            (id == 0 && !_context.PlanningSamjhauta.Any(z => z.UpabhoktaSamitiDetail.UpabhoktaSamitiDetailId == x.UpabhoktaSamitiDetailId))
            || (id > 0 && x.UpabhoktaSamitiDetailId == id)
            ).ToListAsync();

            return new SelectList(await data, "UpabhoktaSamitiDetailId", "Name");
        }
        public async Task<GetInsertedFieldInSamjhautaViewModel> GetYojanaNameBySamitiId(int id)
        {
            var data = await (from us in _context.UpabhoktaSamitiDetail
                                  //join yj in _context.YojanaSetup on us.YojanaId equals yj.YojanaSetupId into sa
                                  //from yj in sa.DefaultIfEmpty()
                              join mem in _context.UpabhoktaSamitiMemberDetail on us.UpabhoktaSamitiDetailId equals mem.UpabhoktaSamitiDetailId into usd
                              from mem in usd.DefaultIfEmpty()
                              where us.UpabhoktaSamitiDetailId == id
                              select new GetInsertedFieldInSamjhautaViewModel
                              {
                                  UpabhoktaSamitiDetailId = us.UpabhoktaSamitiDetailId,
                                  //YojanaId = us.YojanaId,
                                  //UpaChhetraId = yj.UpaChhetraId,
                                  //EstimatedAmount = yj.EstimatedAmount,
                                  //WardId = yj.WardId,
                                  //YojanaAddress = yj.YojanaAddress
                              }).FirstOrDefaultAsync();

            return data ?? new GetInsertedFieldInSamjhautaViewModel();
        }
        public async Task<KarKattiViewModel> GetKarkatti(int? id)
        {
            return (await _context.KarKatti.Where(x => x.KarKattiId == id).Select(x => new KarKattiViewModel()
            {
                KarKattiId = x.KarKattiId,
                BahalKar = x.BahalKar,
                Contigency = x.Contigency,
                MarmatSambhar = x.MarmatSambhar,
                SamajikSurekchya = x.SamajikSurekchya,

            }).FirstOrDefaultAsync());

        }
        public async Task<PadaViewModel> GetPadaDetail(int? id)
        {
            return (await _context.Pada.Where(x => x.Id == id && x.IsDeleted == false).Select(x => new PadaViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync());

        }
        public async Task<UpavoktaSamitiMemberDetailViewModel> Getdata(int? samitiId)
        {
            return (await _context.UpabhoktaSamitiMemberDetail.Where(x => x.UpabhoktaSamitiDetailId == samitiId).Select(x => new UpavoktaSamitiMemberDetailViewModel()
            {
                SamitiPostId = x.SamitiPostId,
                MemberName = x.MemberName,
                Address = x.Address,

            }).FirstOrDefaultAsync());

        }
        public async Task<int?> GetWardNoForLogin_Role_User()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isUserInRole = _httpContextAccessor.HttpContext.User.IsInRole("User") || _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            int? wardId = 0;
            if (isUserInRole)
            {
                wardId = await _context.Users.Where(x => x.Id == userId).Select(x => x.WardId).FirstOrDefaultAsync() ?? 0;
            }

            return wardId;
        }


        public async Task<SelectList> GetContractKarkatti()
        {
            return new SelectList(await _context.Con_KarKatti.Where(x => x.Status == true).ToListAsync(), "Con_katkattiId", "Name");
        }
        public async Task<SelectList> GetUpachhetraByCheetraId(int? Id)
        {
            return new SelectList(await _context.UpaChetra.Where(x => x.IsDeleted == false && (x.ChettraId == Id || Id == null)).ToListAsync(), "UpaChettraId", "UpaChettra");
        }
        public async Task<bool> CheckCitizenship(string CitizenshipNumber)
        {
            var citizen = await _context.UpabhoktaSamitiMemberDetail
                               .AnyAsync(x => x.CitizenshipNumber == CitizenshipNumber);
            return citizen;
        }

        public async Task<SelectList> GetUpabhoktaSamitiDocsType()
        {
            return new SelectList(await _context.UpabhoktaSamitiDetailDocType.ToListAsync(), "Id", "Name");
        }

        public async Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByUpbhokataId(int id)
        {
            var data = await _context.UpabhoktaSamitiDetailYojanas.Where(x => x.UpabhoktaSamitiDetailId == id)
                .Select(x => new DisplayUpbhokataDetailinPlaningSamjhuta()
                {
                    YojanaDetails = (x.YojanaSetup.YojanaName + " / " + _context.Ward.Where(w => w.Id == x.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()),
                    EstimatedAmount = x.YojanaSetup.EstimatedAmount,
                }).ToListAsync();
            return data;

            throw new NotImplementedException();
        }

        public async Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByTolBikasSansthaId(int id)
        {

            var data = await (from tbs in _context.TolBikashSanstha.AsNoTracking()
                              where tbs.TolBikashSansthaId == id
                              join yojan in _context.YojanaSetup.AsNoTracking() on tbs.YojanaId equals yojan.YojanaSetupId
                              join ward in _context.Ward.AsNoTracking() on yojan.WardId equals ward.Id
                              select new DisplayUpbhokataDetailinPlaningSamjhuta
                              {
                                  YojanaDetails = yojan.YojanaName + " / " + ward.Name,
                                  EstimatedAmount = yojan.EstimatedAmount
                              }).ToListAsync();

            return data;

            throw new NotImplementedException();
        }

        public async Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByNewTolBikasSansthaId(int id)
        {

            var data = await (from ntb in _context.NewToleBikash
                              where ntb.Id == id
                              join ntbYojana in _context.NewToleBikashYojanas.AsNoTracking() on ntb.Id equals ntbYojana.NewToleBikashId
                              join yojan in _context.YojanaSetup.AsNoTracking() on ntbYojana.YojanaId equals yojan.YojanaSetupId
                              join ward in _context.Ward.AsNoTracking() on yojan.WardId equals ward.Id
                              select new DisplayUpbhokataDetailinPlaningSamjhuta
                              {
                                  YojanaDetails = yojan.YojanaName + " / " + ward.Name,
                                  EstimatedAmount = yojan.EstimatedAmount

                              }).ToListAsync();

            return data;

            throw new NotImplementedException();
        }

        public async Task<bool> CheckPrintContentAvilable(int PlanningSamjhautaId, string ReportName)
        {
            return await _context.PrintReport.Where(x => x.PlanningSamjhautaId == PlanningSamjhautaId && x.ReportName.Equals(ReportName)).AnyAsync();
            throw new NotImplementedException();
        }
        public async Task<bool> CheckPrintContentAvilableUpaBhokta(int UpabhoktaSamitiDetailId, string ReportName)
        {
            return await _context.PrintReportUpabhokta.Where(x => x.UpabhoktaSamitiDetailId == UpabhoktaSamitiDetailId && x.ReportName.Equals(ReportName)).AnyAsync();

        }



	}
}






