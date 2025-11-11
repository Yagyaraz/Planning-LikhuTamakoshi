using System.ComponentModel;

namespace PlanningCore.Areas.Admin.Models
{

	#region BudgetSource
	public class BudgetSourceViewModel
	{
		public int BudgetSourceId { get; set; }
		[DisplayName("बजेटको स्रोत")]
		public string BudgetSourceName { get; set; }
		[DisplayName("बजेटको स्रोतको रकम")]
		public Nullable<decimal> Amount { get; set; }
		public Nullable<bool> IsDeleted { get; set; }
	}

	public class BudgetTypeViewModel
	{
		public int BudgetTypeId { get; set; }
		[DisplayName("बजेट शिर्षक")]
		public string BudgetTypeName { get; set; }
		public Nullable<bool> IsDeleted { get; set; }
	}
	public class BudgetSubTypeViewModel
	{
		public int Id { get; set; }
		public Nullable<int> BudgetTypeId { get; set; }
		[DisplayName("बजेट उप-शिर्षक")]

		public string BudgetSubTypeName { get; set; }
		public Nullable<decimal> Amount { get; set; }
	}
	#endregion
}

