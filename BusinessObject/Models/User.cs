using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            Budgets = new HashSet<Budget>();
            Transactions = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public decimal Balance { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
