using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class StatisticTransactionDTO
    {
        public int TransId { get; set; }
        public string CategoryType { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public DateTime? TransactionsDate { get; set; }

    }
}
