using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class TransactionDTO
    {
        public int TransId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Merchant { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Notes { get; set; } = null!;
        public DateTime? TransactionsDate { get; set; }
    }
}
