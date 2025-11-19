using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanningCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Repositories;
using PlanningCore.Data;
using PlanningCore.Security;
using PlanningCore.Utilities;
using System.Text.Json;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Repository;
using PlanningCore.Dashboard;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PlanningContextConnection") ?? throw new InvalidOperationException("Connection string 'PlanningContextConnection' not found.");

//builder.Services.AddDbContext<PlanningContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<PlanningContext>(options =>options.UseSqlServer((connectionString)), ServiceLifetime.Transient);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<PlanningContext>();


builder.Services.AddDbContext<PlanningContext>(options => options.UseSqlServer(connectionString,sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddAuthentication();

builder.Services.AddMvc(options =>
{
	options.EnableEndpointRouting = false;
	options.Filters.Add(typeof(CustomAuthorizeAttribute));
});
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null;
	});
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//User Claims like (Name, post)
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsDetails>();

#region Register
builder.Services.AddScoped<ICommon, CommonRepository>();
builder.Services.AddScoped<IUtility, Utilities>();
builder.Services.AddScoped<IUpabhoktaSamiti, UpabhoktaSamitiRepositories>();
builder.Services.AddScoped<IBudget, BudgetRepositories>();
builder.Services.AddScoped<IPlanningSamjhauta, PlanningSamjhautaRepositories>();
builder.Services.AddScoped<IBhuktani, BhuktaniRepositories>();
builder.Services.AddScoped<IConsultant, ConsultantRepositories>();
builder.Services.AddScoped<IBidSecurity, BidSecurityRepository>();
builder.Services.AddScoped<IBankGuarantee, BankGuaranteeRepository>();
builder.Services.AddScoped<IContractSamjhauta, ContractSamjhautaRepository>();
builder.Services.AddScoped<IContractYojana, ContractYojanaRepository>();
builder.Services.AddScoped<IDashboard, DashboardRepository>();
builder.Services.AddScoped<IContractReport, ContractReportRepository>();
builder.Services.AddScoped<IReport, ReportRepositories>();
builder.Services.AddScoped<IAnusuchi, AnusuchiRepositories>();
builder.Services.AddScoped<ITravelExpense, TravelExpenseRepository>();
builder.Services.AddScoped<IFormat, FormatRepository>();
builder.Services.AddScoped<IAnugamanPartibedan,AnugamanPartibedanRepositories>();
#endregion
builder.Services.AddAuthorization();
// Add services to the container.
//builder.Services.AddControllersWithViews();

// added for error 
//builder.Services.Configure<IISServerOptions>(options =>
//{
//    options.MaxRequestBodySize = int.MaxValue;
//});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	CreateRolesAndAdminUser(scope.ServiceProvider);
	AddRequiredData(scope.ServiceProvider);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "Contract",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

#region Create Role and initial User Rergistered
void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
{
	var roleNames = new List<string>
			{
				UserRoles.Administrator,
				UserRoles.SuperAdmin,
				UserRoles.Admin,
				UserRoles.User,
				UserRoles.Contract_Admin,
			
			};
	// Creating roles
	foreach (var role in roleNames) { CreateRole(serviceProvider, role); }

	// Administrator user Setup
	const string administratorUserEmail = "softech@gmail.com";
	const string administratorPwd = "Softech@123!";
	AddUserToRole(serviceProvider, new ApplicationUser()
	{
		FullName = UserRoles.Administrator,
		Email = administratorUserEmail,
		UserName = administratorUserEmail,
		EmailConfirmed = true,
		LockoutEnabled = false,
	}, administratorPwd, UserRoles.Administrator);

	// For Super admin 
	const string superAdminUserEmail = "superadmin@gmail.com";
	const string superAdminPwd = "Softech@123";
	AddUserToRole(serviceProvider, new ApplicationUser()
	{
		FullName = UserRoles.SuperAdmin,
		Email = superAdminUserEmail,
		UserName = superAdminUserEmail,
		EmailConfirmed = true,
	}, superAdminPwd, UserRoles.SuperAdmin);

}
void CreateRole(IServiceProvider serviceProvider, string roleName)
{
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roleExists = roleManager.RoleExistsAsync(roleName);
	roleExists.Wait();

	if (roleExists.Result) return;
	var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
	roleResult.Wait();
}

/// <summary>
/// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
/// </summary>
/// <param name="serviceProvider">Service Provider</param>
/// <param name="user"></param>
/// <param name="userPwd">User Password. Used to create the user if not exists.</param>
/// <param name="roleName">Role Name</param>
void AddUserToRole(IServiceProvider serviceProvider, ApplicationUser user, string userPwd, string roleName)
{
	var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

	Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(user.Email);
	checkAppUser.Wait();

	ApplicationUser appUser = checkAppUser.Result;

	if (checkAppUser.Result == null)
	{
		Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(user, userPwd);
		taskCreateAppUser.Wait();

		if (taskCreateAppUser.Result.Succeeded)
		{
			appUser = user;
		}
	}

	Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
	newUserRole.Wait();
}
#endregion

#region Add Required Data Before Run Project (Single time Run)
void AddRequiredData(IServiceProvider serviceProvider)
{
	using var context = serviceProvider.GetService<PlanningContext>();

	#region State
	var states = JsonSerializer.Deserialize<List<State>>(RequiredInitialData.StateJson);
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[State] On");
			foreach (var item in states)
			{
				if (context.State.Any(x => x.StateId == item.StateId)) continue;
				context.State.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[State] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			Console.Write(ex);
			transaction.Rollback();
		}
	}
	#endregion
	#region District
	var Districts = JsonSerializer.Deserialize<List<District>>(RequiredInitialData.DistrictJson);
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[District] On");
			foreach (var item in Districts)
			{
				if (context.District.Any(x => x.DistrictId == item.DistrictId)) continue;
				context.District.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[District] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			Console.Write(ex);
			transaction.Rollback();
		}
	}
	#endregion
	#region Palika
	var Palikas = JsonSerializer.Deserialize<List<Palika>>(RequiredInitialData.PalikaJson);
	int rows = 100;
	int length = (Palikas.Count / rows) + 1;
	for (int i = 0; i < length; i++)
	{
		var data = Palikas.Skip(i * rows).Take(rows).ToList();
		using (var transaction = context.Database.BeginTransaction())
		{
			try
			{
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Palika] On");
				foreach (var item in data)
				{
					if (context.Palika.Any(x => x.PalikaId == item.PalikaId)) continue;
					context.Palika.Add(item);
					context.SaveChanges();
				}
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Palika] Off");
				transaction.Commit();
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				transaction.Rollback();
			}
		}
	}
	#endregion
	#region Office
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			var office = new Office() { Id = 1, Name = "Softech Foundation", StateId = 3, DistrictId = 28, PalikaId = 312, CreatedDate = DateTime.Now };
			if (!context.Office.Any(x => x.Id == office.Id))
			{
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Office] On");
				context.Office.Add(office);
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Office] Off");
				transaction.Commit();
			}
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Office Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
    #endregion
    #region Ward
    var Ward = new List<Ward>
    {
           new () { Id = 1, Name = "१ नं वडा" },
           new () { Id = 2, Name = "२ नं वडा" },
           new () { Id = 3, Name = "३ नं वडा" },
           new () { Id = 4, Name = "४ नं वडा" },
           new () { Id = 5, Name = "५ नं वडा" },
       };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Ward] On");
            foreach (var items in Ward)
            {
				items.Status = true;
                if (context.Ward.Any(x => x.Id == items.Id)) continue;
                context.Ward.Add(items);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Ward] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"Ward Not Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region pada
    var listOfPost = new List<Pada>
	{
		   new Pada() { Id = 1, Name = "प्रमुख प्रशासकीय अधिकृत" },
		   new Pada() { Id = 2, Name = "सूचना तथा प्रविधि अधिकृत" },
		   new Pada() { Id = 3, Name = "योजना शाखा प्रमुख" },
		   new Pada() { Id = 4, Name = "ईन्जिनियर" },
		   new Pada() { Id = 5, Name = "सहायक पाँचौ"},
		   new Pada() { Id = 6, Name = "कम्प्युटर अपरेटर" },
		   new Pada() { Id = 7, Name = "सहायकस्तर पाँचौ" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Pada] On");
			foreach (var item in listOfPost)
			{
				if (context.Pada.Any(x => x.Id == item.Id)) continue;
				context.Pada.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Pada] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region SamitiPost
	var SamitiPost = new List<SamitiPost>
	{
		   new SamitiPost() { Id = 1, Name = "अध्यक्ष" },
		   new SamitiPost() { Id = 2, Name = "कोषाध्यक्ष" },
		   new SamitiPost() { Id = 3, Name = "सचिव" },
		   new SamitiPost() { Id = 4, Name = "सदस्य" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SamitiPost] On");
			foreach (var items in SamitiPost)
			{
				if (context.SamitiPost.Any(x => x.Id == items.Id)) continue;
				context.SamitiPost.Add(items);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SamitiPost] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
    #endregion
	#region SchoolPost
	var SchoolPost = new List<SchoolPost>
	{
		   new SchoolPost() { Id = 1, Name = "अध्यक्ष" },
		   new SchoolPost() { Id = 2, Name = "कोषाध्यक्ष" },
		   new SchoolPost() { Id = 3, Name = "सचिव" },
		   new SchoolPost() { Id = 4, Name = "सदस्य" },
		   new SchoolPost() { Id = 5, Name = "प्रधानाध्यापक" },
		   new SchoolPost() { Id = 6, Name = "संयोजक" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SchoolPost] On");
			foreach (var items in SchoolPost)
			{
				if (context.SchoolPost.Any(x => x.Id == items.Id)) continue;
				context.SchoolPost.Add(items);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SchoolPost] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
    #endregion
    #region AnugamanSamitiPost
    var AnugamanSamitiPost = new List<AnugamanSamitiPost>
    {
           new AnugamanSamitiPost() { Id = 1, Name = "संयोजक" },
           new AnugamanSamitiPost() { Id = 2, Name = "सदस्य" },
       };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AnugamanSamitiPost] On");
            foreach (var items in AnugamanSamitiPost)
            {
                if (context.AnugamanSamitiPost.Any(x => x.Id == items.Id)) continue;
                context.AnugamanSamitiPost.Add(items);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AnugamanSamitiPost] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"Post Not Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region MainSetting
    using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			var Mainsettings = new MainSetting() { Id = 1, Name = "Softech Foundation", StateId = 3, DistrictId = 28, PalikaId = 312 };
			if (!context.MainSetting.Any(x => x.Id == Mainsettings.Id))
			{
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[MainSetting] On");
				context.MainSetting.Add(Mainsettings);
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[MainSetting] Off");
				transaction.Commit();
			}
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"MainSetting Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region contracttype
	var ListContractType = new List<Con_ContractType>
	{
		   new Con_ContractType() { Id = 1, Name = "Goods",Status=true },
		   new Con_ContractType() { Id = 2, Name = "Works" ,Status=true},
		   new Con_ContractType() { Id = 3, Name = "Consulting Services",Status=true },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ContractType] On");
			foreach (var item in ListContractType)
			{
				if (context.Con_ContractType.Any(x => x.Id == item.Id)) continue;
				context.Con_ContractType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ContractType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region BankGuaranteeType
	var listOfBankGuaranteeType = new List<Con_BankGuaranteeType>
	{
		   new Con_BankGuaranteeType() { Id = 1, Name = "PBG" },
		   new Con_BankGuaranteeType() { Id = 2, Name = "APG" },
		   new Con_BankGuaranteeType() { Id = 3, Name = "APG-I" },
		   new Con_BankGuaranteeType() { Id = 4, Name = "APG-II" },


	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_BankGuaranteeType] On");
			foreach (var item in listOfBankGuaranteeType)
			{
				if (context.Con_BankGuaranteeType.Any(x => x.Id == item.Id)) continue;
				context.Con_BankGuaranteeType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_BankGuaranteeType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region ThekkaType
	var listOfThekkaType = new List<Con_ThekkaType>
	{
		   new Con_ThekkaType() { Id = 1, Name = "Seal Quotation" },
		   new Con_ThekkaType() { Id = 2, Name = "Quotation" },
		   new Con_ThekkaType() { Id = 3, Name = "NCV" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ThekkaType] On");
			foreach (var item in listOfThekkaType)
			{
				if (context.Con_ThekkaType.Any(x => x.Id == item.Id)) continue;
				context.Con_ThekkaType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ThekkaType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region MyadThapType
	var listOfMyadThapType = new List<Con_MyadThapType>
	{
		   new Con_MyadThapType() { Id = 1, Name = "प्रथम" },
		   new Con_MyadThapType() { Id = 2, Name = "दोस्रो" },
		   new Con_MyadThapType() { Id = 3, Name = "तेस्रो" },
		   new Con_MyadThapType() { Id = 4, Name = "चौथो" },
		   new Con_MyadThapType() { Id = 5, Name = "पाँचौं" },
		   new Con_MyadThapType() { Id = 6, Name = "छैठौं" },
		   new Con_MyadThapType() { Id = 7, Name = "सातौ" },
		   new Con_MyadThapType() { Id = 8, Name = "आठौं" },
		   new Con_MyadThapType() { Id = 9, Name = "नवौं" },
		   new Con_MyadThapType() { Id = 10, Name = "दशौं" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_MyadThapType] On");
			foreach (var item in listOfMyadThapType)
			{
				if (context.Con_MyadThapType.Any(x => x.Id == item.Id)) continue;
				context.Con_MyadThapType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_MyadThapType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region FukuwaType
	var listOfFukuwaType = new List<Con_FukuwaType>
	{
		   new Con_FukuwaType() { Id = 1, Name = "EFT" },
		   new Con_FukuwaType() { Id = 2, Name = "Cheque" },
		   new Con_FukuwaType() { Id = 3, Name = "Cash" },
		   new Con_FukuwaType() { Id = 4, Name = "Letter" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_FukuwaType] On");
			foreach (var item in listOfFukuwaType)
			{
				if (context.Con_FukuwaType.Any(x => x.Id == item.Id)) continue;
				context.Con_FukuwaType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_FukuwaType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region Bank
	var listOfBank = new List<Class_A_Bank_List>
	{
		   new Class_A_Bank_List() { Class_A_Bank_List_Id = 1, BankName_Nep = "सिद्धार्थ बैंक", BankName_Eng="Siddhartha Bank",Status=true },
		   new Class_A_Bank_List() { Class_A_Bank_List_Id = 2, BankName_Nep = "नेपाल इन्भेस्टमेन्ट बैंक", BankName_Eng="NMB",Status=true },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Class_A_Bank_List] On");
			foreach (var item in listOfBank)
			{
				item.Status = true;
				if (context.Class_A_Bank_List.Any(x => x.Class_A_Bank_List_Id == item.Class_A_Bank_List_Id)) continue;
				context.Class_A_Bank_List.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Class_A_Bank_List] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"PaymentType Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
    #endregion
    #region PaymentType
    var listOfPaymentType = new List<PaymentType>
    {
           new () { Id = 1, Name = "नगद" },
           new () {Id = 2, Name = "बैंक"},
       };
    using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[PaymentType] On");
			foreach (var item in listOfPaymentType)
			{
				if (context.PaymentType.Any(x => x.Id == item.Id)) continue;
				context.PaymentType.Add(item);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[PaymentType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"PaymentType Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region Variation Type
	var listOfVariationType = new List<Con_VariationType>
	{
		   new Con_VariationType() { Id = 1,  Name="VO-1" },
		   new Con_VariationType() { Id = 2,  Name="VO-2"},
		   new Con_VariationType() { Id = 3,  Name="VO-3"},
		   new Con_VariationType() { Id = 4,  Name="VO-4"},
		   new Con_VariationType() { Id = 5,  Name="VO-5"},
		   new Con_VariationType() { Id = 6,  Name="VO-6"},
		   new Con_VariationType() { Id = 7,  Name="VO-7"},
		   new Con_VariationType() { Id = 8,  Name="VO-8"},
		   new Con_VariationType() { Id = 9,  Name="VO-9"},
		   new Con_VariationType() { Id = 10, Name="VO-10"},


	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_VariationType] On");
			foreach (var item1 in listOfVariationType)
			{
				if (context.Con_VariationType.Any(x => x.Id == item1.Id)) continue;
				context.Con_VariationType.Add(item1);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_VariationType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region Contract Report Type
	var listOfConReportType = new List<Con_ReportType>
	{
		   new Con_ReportType() { Id = 1,  ReportName_Nep="कार्यदेश" ,ReportName_Eng="Karyadesh", ReportCode="K" },
		   new Con_ReportType() { Id = 2,  ReportName_Nep="प्रथम रनिङ्ग बिल" ,ReportName_Eng="FirstRunningBill", ReportCode="FirstRunningBill" },
		   new Con_ReportType() { Id = 3,  ReportName_Nep="अन्तिम बिल भुक्तानी" ,ReportName_Eng="AntimRunningBill", ReportCode="AntimRunningBill" },
		   new Con_ReportType() { Id = 4,  ReportName_Nep="CONTRACT AGGREEMENT" ,ReportName_Eng="CONTRACT AGGREEMENT", ReportCode="ContractAgreement" },
		   new Con_ReportType() { Id = 5,  ReportName_Nep="मोबिलाइजेशन पेश्की" ,ReportName_Eng="Mobilization", ReportCode="Mobilization" },
		   new Con_ReportType() { Id = 6,  ReportName_Nep="Contract Agreement English" ,ReportName_Eng="Contract Agreement English", ReportCode="AgreementEnglish" },

	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ReportType] On");
			foreach (var item1 in listOfConReportType)
			{
				if (context.Con_ReportType.Any(x => x.Id == item1.Id)) continue;
				context.Con_ReportType.Add(item1);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Con_ReportType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
    #endregion
    #region WorkType
    var worktype = new List<WorkType>
    {
           new WorkType() { WorkTypeId = 1, WorkTypeName = "उपभोक्ता समिति" },
           new WorkType() { WorkTypeId = 2, WorkTypeName = "बिद्यालय व्यवस्थापन समिति" },
       };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkType] On");
            foreach (var items in worktype)
            {
                if (context.WorkType.Any(x => x.WorkTypeId == items.WorkTypeId)) continue;
                context.WorkType.Add(items);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"Post Not Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region BudgetSource
    var budgetsrc = new List<BudgetSource>
	{
		   new BudgetSource() { BudgetSourceId = 1, BudgetSourceName = "नेपाल सरकार" },
		   new BudgetSource() { BudgetSourceId = 2, BudgetSourceName = "बित्तिय समानिकरण" },
		   new BudgetSource() { BudgetSourceId = 3, BudgetSourceName = "प्रदेश सरकार" },


	   };
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[BudgetSource] On");
			foreach (var items in budgetsrc)
			{
				items.IsDeleted = false;
				if (context.BudgetSource.Any(x => x.BudgetSourceId == items.BudgetSourceId)) continue;
				context.BudgetSource.Add(items);
				context.SaveChanges();
			}
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[BudgetSource] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			app.Logger.LogInformation($"Post Not Created, Error : {ex}");
			transaction.Rollback();
		}
	}
	#endregion
	#region planningtype
	var PLType = JsonSerializer.Deserialize<List<PlanningType>>(RequiredInitialData.PlanningtypeJson);
	using (var transaction = context.Database.BeginTransaction())
	{
		try
		{
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[PlanningType] On");
			foreach (var item in PLType)
			{
                if (context.PlanningType.Any(x => x.Id == item.Id)) continue;
                context.PlanningType.Add(item);
                context.SaveChanges();
            }
			context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[PlanningType] Off");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			Console.Write(ex);
			transaction.Rollback();
		}
	}
    #endregion
    #region UpabhoktaSamitiDetailDocType
    var UpabhoktaSamitiDetailDocType = new List<UpabhoktaSamitiDetailDocType>
    {
           new () { Id = 1, Name = "उ.भेला र समिति गठनको निर्णय पुस्तिका" },
           new () { Id = 2, Name = "उ.स.को बैठकको निर्णय पुस्तिका" },
           new () { Id = 3, Name = "वार्डको सिफारिस" },
           new () { Id = 4, Name = "उ.स.को नगद जम्मा गरेको भौचर" },
           new () { Id = 5, Name = "उ.स.को निवेदन" },
       };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[UpabhoktaSamitiDetailDocType] On");
            foreach (var items in UpabhoktaSamitiDetailDocType)
            {
                if (context.UpabhoktaSamitiDetailDocType.Any(x => x.Id == items.Id)) continue;
                context.UpabhoktaSamitiDetailDocType.Add(items);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[UpabhoktaSamitiDetailDocType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"UpabhoktaSamitiDetailDocType Not Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region BhuktaniType
    var BhuktaniType = new List<BhuktaniType>
    {
           new () { BhuktaniTypeId = 1, BhuktaniTypeName = "पेश्की रकम", Status = true },
           new () { BhuktaniTypeId = 2, BhuktaniTypeName = "प्रथम किस्ता", Status = true },
           new () { BhuktaniTypeId = 3, BhuktaniTypeName = "दोस्रो किस्ता", Status = true },
           new () { BhuktaniTypeId = 4, BhuktaniTypeName = "तेस्रो किस्ता", Status = true },
     };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[BhuktaniType] On");
            foreach (var items in BhuktaniType)
            {
                if (context.BhuktaniType.Any(x => x.BhuktaniTypeId == items.BhuktaniTypeId)) continue;
                context.BhuktaniType.Add(items);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[BhuktaniType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"BhuktaniType Not Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
}
#endregion
