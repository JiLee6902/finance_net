using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Transaction
    {
        public int TransId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Merchant { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Notes { get; set; } = null!;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
        public DateTime? TransactionsDate { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
