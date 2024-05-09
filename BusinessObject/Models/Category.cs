using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Category
    {
        public Category()
        {
            Budgets = new HashSet<Budget>();
            Transactions = new HashSet<Transaction>();
        }

        public int CategoryId { get; set; }
        
        public string CategoryType { get; set; } = null!;

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
