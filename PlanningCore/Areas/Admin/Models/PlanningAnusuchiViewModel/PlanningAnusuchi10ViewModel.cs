namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi10ViewModel
    {
        public int Anusuchi10Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string AdakshyaName { get; set; }
        public string AdakshyaGender { get; set; }
        public string AdakshyaMobileNo { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public List<PlanningAnusuchi10DiscussionConclusionViewModel> PlanningAnusuchi10DiscussionConclusionList { get; set; } = new List<PlanningAnusuchi10DiscussionConclusionViewModel>();
        public List<PlanningAnusuchi10DiscussionSubjectViewModel> PlanningAnusuchi10DiscussionSubjectList { get; set; } = new List<PlanningAnusuchi10DiscussionSubjectViewModel>();
        public List<PlanningAnusuchi10MeetingViewModel> PlanningAnusuchi10MeetingList { get; set; } = new List<PlanningAnusuchi10MeetingViewModel>();
        public List<PlanningAnusuchi10MembersViewModel> PlanningAnusuchi10MembersList { get; set; } = new List<PlanningAnusuchi10MembersViewModel>();
    }
    public class PlanningAnusuchi10DiscussionConclusionViewModel
    {
        public int Anusuchi10DiscussionConclusionId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string Conclusion { get; set; }
    }
    public class PlanningAnusuchi10DiscussionSubjectViewModel
    {
        public int Anusuchi10DiscussionSubjectId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string Subject { get; set; }
    }
    public class PlanningAnusuchi10MeetingViewModel
    {
        public int Anusuchi10MeetingId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string BaithakNo { get; set; }
        public string BaithakDate { get; set; }
    }
    public class PlanningAnusuchi10MembersViewModel
    {
        public int Anusuchi10MemberId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string MemberName { get; set; }
        public int MemberGender { get; set; }
        public int MemberPad { get; set; }
        public string MemberPhone { get; set; }
        public string PadaName { get; set; }
        public string GenderName { get; set; }
    }
}
