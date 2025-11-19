using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace PlanningCore.Data
{
    public class PlanningContext : IdentityDbContext<ApplicationUser>
    {
        public PlanningContext(DbContextOptions<PlanningContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<FiscalYear> FiscalYear { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Palika> Palika { get; set; }
        public DbSet<Ward> Ward { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<SubDepartment> SubDepartment { get; set; }
        public DbSet<MainSetting> MainSetting { get; set; }
        public DbSet<Pada> Pada { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<PrintReport> PrintReport { get; set; }
        public DbSet<PrintReportUpabhokta> PrintReportUpabhokta { get; set; }
        public DbSet <Class_A_Bank_List> Class_A_Bank_List { get; set; }
		public DbSet<AnugamanMember> AnugamanMember { get; set; }
		public DbSet<TeavelExpense> TravelExpense { get; set; }
		public DbSet<TravelRequiredItems> TravelRequiredItems { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<AnugamanSamitiSetup> AnugamanSamitiSetup { get; set; }
        public DbSet<AnugamanSamitiMember> AnugamanSamitiMember { get; set; }


        #region Planning
        public DbSet<BudgetType>BudgetType { get; set; }
		public DbSet<PlanningType> PlanningType { get; set; }
        public DbSet<BudgetSubType> BudgetSubType { get; set; }
        public DbSet<BudgetSource> BudgetSource { get; set; }
        public DbSet<WorkArea> WorkArea { get; set; }
        public DbSet<WorkType> WorkType { get; set; }
        public DbSet<Chettra>Chettra { get; set; }
        public DbSet<UpaChetra>UpaChetra { get; set; }
        public DbSet<UpaChetraDetail> UpaChetraDetail { get; set; }
        public DbSet<KarKatti> KarKatti { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<BhuktaniType> BhuktaniType { get; set; }
        public DbSet<ThekkaShrotType> ThekkaShrotType { get; set; }
        public DbSet<Con_KarKatti> Con_KarKatti { get; set; }
        public DbSet<ThekkaBhuktaniType> ThekkaBhuktaniType { get; set; }
        public DbSet<SartaSetup> SartaSetup { get; set; }
        public DbSet<TolBikashSanstha> TolBikashSanstha { get; set; }
        public DbSet<TolBikashSansthaMember> TolBikashSansthaMember { get; set; }
        public DbSet<UpabhoktaSamitiDetail> UpabhoktaSamitiDetail { get; set; }
        public DbSet<UpabhoktaSamitiMemberDetail> UpabhoktaSamitiMemberDetail { get; set; }
        public DbSet<UpabhoktaSamitiDetailYojanas> UpabhoktaSamitiDetailYojanas { get; set; }
        public DbSet<UpabhoktaSamitiDetailDocType> UpabhoktaSamitiDetailDocType { get; set; }
        public DbSet<UpabhoktaSamitiDetailDocs> UpabhoktaSamitiDetailDocs { get; set; }
        public DbSet<DarkhasthaForm>DarkhasthaForm { get; set; }
        public DbSet<YojanaSetup> YojanaSetup { get; set; }
        public DbSet<PlanningSamjhauta>PlanningSamjhauta { get; set; }
        public DbSet<OrganizationRepresentative> OrganizationRepresentative { get; set; }
        public DbSet<ProjectEntryDetail> ProjectEntryDetail { get; set; }
        public DbSet<ProjectSourceDetail> ProjectSourceDetail { get; set; }
        public DbSet<BeneficiariesGroup> BeneficiariesGroup { get; set; }
        public DbSet<PlanningPravidikDetails> PlanningPravidikDetails { get; set; }
        public DbSet<PlanningEntry> PlanningEntry { get; set; }
        public DbSet<MunicipalitySamitiManjuriPatra> MunicipalitySamitiManjuriPatra { get; set; }
        public DbSet<AayojanaMaintainance> AayojanaMaintainance { get; set; }
        public DbSet<AmanatDetail> AmanatDetail { get; set; }
        public DbSet<PaymentRecord> PaymentRecord { get; set; }
        public DbSet<PlanningBhuktani> PlanningBhuktani { get; set; }

        //Anusuchi
        public DbSet<Anusuchi1> Anusuchi1 { get; set; }
        public DbSet<Anusuchi3> Anusuchi3 { get; set; }
        public DbSet<Anusuchi3Bhuktani> Anusuchi3Bhuktani { get; set; }
        public DbSet<Anusuchi3Income> Anusuchi3Income { get; set; }
        public DbSet<Anusuchi3Maujat> Anusuchi3Maujat { get; set; }
        public DbSet<Anusuchi3ExpensesType> Anusuchi3ExpensesType { get; set; }
        public DbSet<Anusuchi3Expenses> Anusuchi3Expenses { get; set; }
        public DbSet<Anusuchi3ProjectWorkDetail>Anusuchi3ProjectWorkDetail { get; set; }
        public DbSet<Anusuchi3WorkDivision> Anusuchi3WorkDivision { get; set; }
        public DbSet<Anusuchi4> Anusuchi4 { get; set; }
        public DbSet<Anusuchi4Expense> Anusuchi4Expense { get; set; }
        public DbSet<Anusuchi4ExpenseType> Anusuchi4ExpenseType { get; set; }
        public DbSet<Anusuchi4Income> Anusuchi4Income { get; set; }
        public DbSet<Anusuchi5> Anusuchi5 { get; set; }
        public DbSet<Anusuchi6> Anusuchi6 { get; set; }
        public DbSet<Anusuchi6Janasahabhagita> Anusuchi6Janasahabhagita { get; set; }
        public DbSet<Anusuchi6Karyalaya> Anusuchi6Karyalaya { get; set; }
        public DbSet<Anusuchi6Solution> Anusuchi6Solution { get; set; }
        public DbSet<Anusuchi7>Anusuchi7 { get; set; }
        public DbSet<Anusuchi7AnugamanSamiti> Anusuchi7AnugamanSamiti { get; set; }
        public DbSet<Anusuchi7UpabhoktaSamiti> Anusuchi7UpabhoktaSamiti { get; set; }
        public DbSet<DocumentUploaded> DocumentUploaded { get; set; }
        public DbSet<SamitiPost> SamitiPost { get; set; }
        public DbSet<SchoolPost> SchoolPost { get; set; }
        public DbSet<AnugamanSamitiPost> AnugamanSamitiPost { get; set; }
        public DbSet<PragatiBibaran> PragatiBibaran { get; set; }

        //NewToleBikash
        public DbSet<NewToleBikash> NewToleBikash { get; set; }
        public DbSet<NewToleBikashYojanas> NewToleBikashYojanas { get; set; }
        public DbSet<NewToleBikashMemberDetail> NewToleBikashMemberDetail { get; set; }
        public DbSet<NewToleBikashAnugamanMember> NewToleBikashAnugamanMember { get; set; }
        public DbSet<YojanaKaryakramChecklist> YojanaKaryakramChecklist { get; set; }
        //new 
        public DbSet<AnugamanPartibdean>AnugamanPartibdeans { get; set; }
        public DbSet<KhataSanchalakDetails> KhataSanchalakDetails { get; set; }

        #endregion

        #region Contract

        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Con_Yojana> Con_Yojana { get; set; }
        public DbSet<Con_Yojana_Ward> Con_Yojana_Ward { get; set; }
        public DbSet<Con_ContractType> Con_ContractType { get; set; }
        public DbSet<Con_Consultant> Con_Consultant { get; set; }
        public DbSet<Con_JV> Con_JV { get; set; }
        public DbSet<Con_BidSecurity> Con_BidSecurity { get; set; }
        public DbSet<Con_BankGuarantee> Con_BankGuarantee { get; set; }
        public DbSet<Con_Samjhauta> Con_Samjhauta { get; set; }
        public DbSet<Con_SamjhautaDetails> Con_SamjhautaDetails { get; set; }
        public DbSet<Con_Insurance> Con_Insurance { get; set; }
        public DbSet<Con_Variation> Con_Variation { get; set; }
        public DbSet<Con_BankGuaranteeType> Con_BankGuaranteeType { get; set; }
        public DbSet<Con_ThekkaType> Con_ThekkaType { get; set; }
        public DbSet<Con_FukuwaType> Con_FukuwaType { get; set; }
        public DbSet<Con_MyadThapType> Con_MyadThapType { get; set; }
        public DbSet<Con_VariationType> Con_VariationType { get; set; }
        public DbSet<Con_ReportType> Con_ReportType { get; set; }
        public DbSet<Con_PrintReportDetail> Con_PrintReportDetail { get; set; }
        public DbSet<Con_Bhuktani> Con_Bhuktani { get; set; }
        public DbSet<Con_TaxDeduction> Con_TaxDeduction { get; set; }
        #endregion
    }
}
