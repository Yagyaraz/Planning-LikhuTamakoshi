using AspNetCoreGeneratedDocument;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class PlanningSamjhautaViewModel
    {
        public int PlanningSamjhautaId { get; set; }
        [Display(Name = "आर्थिक बर्ष")]
        public int FiscalYearId { get; set; }
        [Display(Name = "आर्थिक बर्ष")]
        public string FiscalYearName { get; set; }
        [Display(Name = "सम्झौता गर्ने संस्थाको नामः")]
        public string Samjhauta_Org_Name { get; set; }
        [Display(Name = "कन्टेन्जेंसी ")]
        public decimal Contegency_Amount { get; set; }
        [Display(Name = "मर्मत सम्भार")]
        public decimal MarmatSambhar_Amount { get; set; }
        [Display(Name = "जम्मा रु")]
        public decimal Total_Amount { get; set; }
        [Display(Name = "आयोजना स्वीकृत गर्ने निकाय")]
        public Nullable<bool> Samjhauta_Acceptance { get; set; }
        [Display(Name = "आर्थिक बर्ष")]
        public decimal Contegency_Percentage { get; set; }
        [Display(Name = "उपभोक्ता समितिको नाम")]
        public Nullable<int> SamitiDetailId { get; set; }
        [Display(Name = "सम्झौताका सर्त")]
        public Nullable<int> SartaSetupId { get; set; }
        [Display(Name = "पेश्कि कट्टि")]
        public decimal Peski_Katti { get; set; }
        [Display(Name = "बिद्यालय व्यवस्थापन समितिको नाम")]
        public Nullable<int> TolBikashSansthaId { get; set; }
        [Display(Name = "टोल बिकास समितिको नाम")]
        public Nullable<int> NewTolBikashSansthaId { get; set; }
        [Display(Name = "सम्झौता स्विकृत गर्नुहोस")]
		public bool Status { get; set; }
		public bool Isdeleted { get; set; }
        
        [Display(Name = "वडा नम्बर")]
        public string WardName {  get; set; } 
        public string WardAdakhsya {  get; set; } 
        public string WardSachib {  get; set; }   
        public string WardKoshadaskhya {  get; set; }
        public string WardAdakhsyaContact { get; set; }
        public string WardSachibContact { get; set; }
        public string WardKoshadaskhyaContact { get; set; }

        public string WardAddress {  get; set; }   
     


        //OrganisationRepresentative
        public int Organization_Representative_Id { get; set; }
        [Display(Name = " नाम")]
		public int RepresentativeNameId { get; set; }
        [Display(Name = "पद")]
		public int RepresentativePostId { get; set; }
		[Display(Name = "सम्झौता गर्ने संस्थाको प्रतिनिधीको नाम")]
		public string RepresentativeName { get; set; }
		[Display(Name = "सम्झौता गर्ने संस्थाको प्रतिनिधीको पद")]
		public string RepresentativeDesignition { get; set; }
        [Display(Name = "प्रतिनिधीको ठेगाना")]
        public string RepresentativeAddress { get; set; }
     

		//ProjectEntryDetail
		public int ProjectEntryDetailId { get; set; }
        [Display(Name = "योजनाको नाम")]
        public string Project_Name { get; set; }
        [Display(Name = "ठेगाना")]
        public string Project_Place { get; set; }
        [Display(Name = "उदेश्य")]
        public string Project_Objective { get; set; }
        [Display(Name = "आयोजना स्वीकृत गर्ने निकाय")]
        public string Project_Acceptance_By { get; set; }
        [Display(Name = "आयोजना शुरु हुने मिति")]
        public string Project_Start_Date { get; set; }
        [Display(Name = "आयोजना सम्पन्न हुने मिति")]
        public string Project_End_Date { get; set; }
        [Display(Name = "आयोजना समाप्त  हुने मिति")]
        public string Project_Complete_Date { get; set; }
        [Display(Name = "विनियोजित रकम रु:")]
        public decimal Project_estimated_Amount { get; set; }
        [Display(Name = "लागत अनुमान रकम")]
        public decimal Total_Amount_Source { get; set; }
      
        public decimal Total_Use_Amount { get; set; }
       
        public decimal ProjectAllocatedAmount { get; set; }
        [Display(Name = "म्याद थप मिति")]
        public string Project_Extend_End_Date { get; set; }
    
        public Nullable<bool> Project_Working_Status { get; set; }

        //ProjectSourceDetail
        public int ProjectSourceDetailId { get; set; }
        [Display(Name = "नेपाल सरकार")]
        public decimal Nepal_Government { get; set; }
        [Display(Name = "पालिका वाट")]
        public decimal Municipality { get; set; }
        [Display(Name = "प्रदेश")]
        public decimal State { get; set; }
        [Display(Name = " गैर सरकारी संघ संस्थाबाट")]
        public decimal NGO_INGO { get; set; }
        [Display(Name = "समुदायमा आधारित संस्था")]
        public decimal Community_Org { get; set; }
        [Display(Name = "विदेशी दातृ संघ संस्थाबाट")]
        public decimal Foreign_Org { get; set; }
        [Display(Name = " उपभोक्ता समितिवाट नगद")]
        public decimal Public_Community { get; set; }
        [Display(Name = "श्रमदान")]
        public decimal Loan_Grant { get; set; }
        [Display(Name = "अन्यवाट")]
        public decimal Other_Source { get; set; }

        // BeneficiariesGroup
        public int BeneficiariesGroupId { get; set; }
        [Display(Name = "घर संख्या")]
        public decimal Total_House { get; set; }
        [Display(Name = "पुरुष")]
        public decimal Total_Male { get; set; }
        [Display(Name = "महिला")]
        public decimal Total_Female { get; set; }
        [Display(Name = "सामुदाय")]
        public decimal Community { get; set; }
        [Display(Name = "अन्य")]
        public decimal Other { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }

        //PlanningPravidikDetails
        public int PlanningPravidikDetailId { get; set; }
        [Display(Name = "क्षेत्र")]
        public Nullable<int> ChettraId { get; set; }
        [Display(Name = "उप-क्षेत्र")]
        public Nullable<int> UpaChetraId { get; set; }
        [Display(Name = "उप-क्षेत्रको प्रकार")]
        public Nullable<int> UpaChetraDetailId { get; set; }
        [Display(Name = "Es विवरण")]
        public string EsDetail { get; set; }
        [Display(Name = "विवरण")]
        public string Detail { get; set; }
        [Display(Name = "कैफियत")]
        public string Kaifiyat { get; set; }
        [Display(Name = "इकाई")]
        public int? UnitId { get; set; }
        [Display(Name = "परिमाण")]
        public string Pariman { get; set; }

        //PlanningEntry
        public int PlanningEntryId { get; set; }
        [Display(Name = "योजनाको नाम")]
        public string PlanningName { get; set; }
        [Display(Name = "आयोजना शुरु हुने मिति")]
        //public string PlanningStartDate { get; set; }
        //[Display(Name = "आयोजना सम्पन्न हुने मिति")]
        //public string PlanningEndDate { get; set; }
        //[Display(Name = "Work Details")]
        public string Work_Details { get; set; }
        [Display(Name = "विनियोजित रकम")]
        public decimal Amount_Estimate { get; set; }
        [Display(Name = "कामको किसिम")]
        public Nullable<int> WorkTypeId { get; set; }
        [Display(Name = "वडा नम्बर")]

        public Nullable<int> WardNo { get; set; }
        [Display(Name = "WOrk Area")]
        public Nullable<int> WorkAreaId { get; set; }
        [Display(Name = "सि.न")]
        public string SerialNo { get; set; }
        [Display(Name = "बजेट स्रोत")]
        public Nullable<int> BudgetSourceId { get; set; }
        [Display(Name = "स्तर अनुशार विवरण")]
        public Nullable<int> PlanningTypeId { get; set; }
        [Display(Name = "योजनाको किसिम")]
        public string Planning_Type { get; set; }
      
        public string UpaBhoktaSamiti_HeadName { get; set; }
      
        public string Contractor_Name { get; set; }
        [Display(Name = "बजेट शिर्षक नं")]
        public string BudgetSirshakNo { get; set; }
        [Display(Name = "बजेट शिर्षक")]
        public string BudgetSirshak { get; set; }
        [Display(Name = "खर्च शिर्षक नं")]
        public string KharchaSirshakNo { get; set; }
        [Display(Name = "खर्च शिर्षक")]
        public string KharchaSirshak { get; set; }
        [Display(Name = "तयार गर्ने")]
        public int? TayarGarneId { get; set; }
        [Display(Name = "सिफारीस गर्ने")]
        public int? SifarisGarneId { get; set; }
        [Display(Name = "रुजु गर्ने")]
        public int? RujuGarneId { get; set; }
        [Display(Name = "स्वकृत गर्ने")]
        public int? SwikritGarneId { get; set; }
        [Display(Name = "प्राविधिक")]
        public int? PravidhikEmployeeId { get; set; }
        [Display(Name = "योजना सँकेत नं")]
        public string PlanningSanketNo { get; set; }
        [Display(Name = "प्राविधिक पद")]
        public int? PravidhikEmployeePadId { get; set; }
        [Display(Name = "स्वकृत गर्ने पद")]
        public int? SwikritGarnePadId { get; set; }
        [Display(Name = "रुजु गर्ने पद")]
        public int? RujuGarnePadId { get; set; }
        [Display(Name = "सिफारीस गर्ने पद")]
        public int? SifarisGarnePadId { get; set; }
        [Display(Name = "तयार गर्ने पद")]
        public int? TayarGarnePadId { get; set; }
        public string SifarishGarneName { get; set; }
        public string SifarishGarnePost { get; set; }
        public string SwikritGarneName { get; set; }
        public string SwikritGarnePost { get; set; }
        public string PrintDate { get; set; }
		//Added Later For Ward
		public int? WardSifarishPadId { get; set; }
		public int? WardSifarishId { get; set; }
		public int? WardSwikritPadId { get; set; }
		public int? WardSwikritId { get; set; }
		public string WardSifarishPost { get; set; }
		public string WardSifarishName { get; set; }
		public string WardSwikitPost { get; set; }
		public string WardSwikritName { get; set; }
		public string PlanningTypeName { get; set; }
		public string UnitName { get; set; }


		//MunicipalitySamitiManjuriPatra
		public int MunicipalitySamitiManjuriPatraId { get; set; }
  
        public string Municipality_Rep_Name { get; set; }
       
        public string Municipality_Rep_Post { get; set; }
        
        public string Municipality_Rep_Sign { get; set; }
        [Display(Name ="सम्झौता मिति")]
        public string Municipality_Manjuri_Date { get; set; }
        
        public string Samiti_Adakshya_Name { get; set; }
       
        public string Samiti_Adakshya_Sign { get; set; }
    
        public string Samiti_Sachib_Name { get; set; }
       
        public string Samiti_Sachib_Sign { get; set; }
       
        public string Samiti_Rohabar_Name { get; set; }
       
        public string Samiti_Rohabar_Sign { get; set; }
        
        public string YojanaSakhaRepresentativePostSign { get; set; }
      
        public string YojanaSakhaRepresentativePost { get; set; }
      
        public string YojanaSakhaRepresentaive { get; set; }

        //AayojanaMaintainance
        public int AayojanaMaintainanceId { get; set; }
      
        public string ResponsibleOrg { get; set; }
         
        public string Janashram { get; set; }
         
        public string SewaSulka { get; set; }
         
        public string DasturChanda { get; set; }
         
        public string LagatAnudhan { get; set; }
         
        public string InterestSaving { get; set; }

        //AmanatDetail
        public int AmanatDetailId { get; set; }
        public string AmanatName { get; set; }
         
        public string Darja { get; set; }
         
        public string AmantSahiDate { get; set; }
		[Display(Name = "डाटा प्रविष्टि गर्नेको नाम")]
		public string CreatedBy { get; set; }


        //PaymentRecord[]
        [Display(Name ="बजेट स्रोत")]
        public string BudgetSourceName { get; set; }
        //Kista Payment
        public PlanningSamjhautaPaymentRecordViewModel PlanningSamjhautaKistaFirstDetailsList { get; set; } = new PlanningSamjhautaPaymentRecordViewModel();
        public PlanningSamjhautaPaymentRecordViewModel PlanningSamjhautaKistaSecondDetailsList { get; set; } = new PlanningSamjhautaPaymentRecordViewModel();
        public PlanningSamjhautaPaymentRecordViewModel PlanningSamjhautaKistaThirdDetailsList { get; set; } = new PlanningSamjhautaPaymentRecordViewModel();
		public List<PlanningTaxViewModel> TaxList { get; set; } = new List<PlanningTaxViewModel>();
		public List<PlanningSamjhautaViewModel> SamjhautaList { get; set; } = new List<PlanningSamjhautaViewModel>();
		public List<YojanaSetupViewModel> yojanalist { get; set; } = new List<YojanaSetupViewModel>();
        public List<AnugamanViewModel> AnugumanmemberList { get; set; }=new List<AnugamanViewModel>();
        public AnugamanSamitiSetupViewModel AnugamanSamitiSetup { get; set; } =new AnugamanSamitiSetupViewModel();
        public List<AnugamanSamitiMemberViewModel> AnugamanSamitiMember { get; set; } = new List<AnugamanSamitiMemberViewModel>();
        public List<AnugamanSamitiSetupViewModel> AnugamanSamitiSetupList { get; set; } = new List<AnugamanSamitiSetupViewModel>();

        public string SartaName { get; set; }
        public string SartaDiscription { get; set; }
         
        public string ChetraName { get; set; }
         
        public string UpaChetraName { get; set; }
         
        public string UpaChetraDetailName { get; set; }
        public List<UpavoktaSamitiDetailViewModel> samitiList { get; set; } = new List<UpavoktaSamitiDetailViewModel>();
        public UpavoktaSamitiDetailViewModel samitiView { get; set; } = new UpavoktaSamitiDetailViewModel();
		public List<PlanningDocumentUploadedViewModel> DocumentsList { get; set; } = new List<PlanningDocumentUploadedViewModel>();

		//EMP
		public string EmployeeName { get; set; }
		public int EmployeeId { get; set; }

        // आयोजनाको विवरणः
        public string YojanaNames { get; set; }
        public string YojanaWards { get; set; }
        public string YojanaAddress { get; set; } 
        public string YojanaWardsEng { get; set; }
        public string YojanaAddressEng { get; set; }

        //new added
        public bool IsAnugaman { get; set; }

    }
    public class PlanningSamjhautaPaymentRecordViewModel
    {
        public int Payment_Records_Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
         
        public string Kista_Kram { get; set; }
         
        public Nullable<System.DateTime> Payment_Date { get; set; }
         
        public string Kista_Rakam { get; set; }
         
        public string Nirmarn_Samagri { get; set; }
         
        public string Remarks { get; set; }
    }
	public class PlanningDocumentUploadedViewModel
	{
		public int Id { get; set; }
		public int PlanningSamjhuataId { get; set; }
        [Display(Name ="कागजातको नाम")]
		public int DocumentTypeId { get; set; }
		public IFormFile Image { get; set; }
		public string DocumentTypeName { get; set; }
		public string ImagePath { get; set; }
		public List<PlanningDocumentUploadedViewModel> DocumentsList { get; set; } = new List<PlanningDocumentUploadedViewModel>();
	}

    public class PlanningTaxViewModel
    {
        public int Id { get; set; }
        public decimal Kantigenci { get; set;}
        public decimal MarmatSambar { get; set;}
        public List<PlanningTaxViewModel> TaxList { get; set; } = new List<PlanningTaxViewModel>();
    }
    
    public class PragatiBibaranViewModel
	{
        public int Id { get; set; }
        public decimal BittiyaPragati { get; set;}
        public decimal BhautikPragati { get; set;}
        public string Remarks { get; set;}
    }


}