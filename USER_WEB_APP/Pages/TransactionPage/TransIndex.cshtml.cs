using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Library.DataAccess;

namespace USER_WEB_APP.Pages.TransactionPage
{
    public class TransIndexModel : PageModel
    {
        private readonly Library.DataAccess.PRN221Context _context;

        public TransIndexModel(Library.DataAccess.PRN221Context context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transactions != null)
            {
                Transaction = await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.User).ToListAsync();
            }
        }
    }
}
