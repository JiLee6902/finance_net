using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Budget
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
