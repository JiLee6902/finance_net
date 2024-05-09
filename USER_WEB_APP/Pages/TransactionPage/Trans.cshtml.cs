
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlX.XDevAPI;

namespace USER_WEB_APP.Pages.TransactionPage
{

    public class Trans : PageModel
    {

        Uri baseAddress = new Uri("https://localhost:5267/api");
        private readonly HttpClient client = null;

        public Trans()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            Transaction = new Transaction();

        }
 

  

        public User user { get; set; }
      

        public bool IncomeCheck { get; set; } = false;
        public Category cate { get; set; }

        [BindProperty]
        public string id { get; set; }
       public Transaction Transaction { get; set; }
        public IList<Transaction> listTransaction { get; set; }


        DateTime n = DateTime.Now;
        public string LocalDate;
        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            user = new User();
            user.UserId = 14; 
            cate = new Category();
            cate.CategoryId = 1;
            Transaction = new Transaction();
            listTransaction = new List<Transaction>();
            Transaction.UserId = 14;

            LocalDate = $"{n.Year}-{n.Month}-{n.Day}T{n.Hour}:{n.Minute}";
            bool.TryParse(Request.Query["IncomeCheck"].FirstOrDefault("false"), out bool s);
            IncomeCheck = s;
            if (s)
            {
                Transaction.CategoryId = 2;
            }
            else
            {
                Transaction.CategoryId = 1;
            }




            HttpResponseMessage response1 = await client.GetAsync(baseAddress + "/transaction/get-all-transaction-by-categor-id/?cid=" + Transaction.CategoryId + "&userID=" + Transaction.UserId);
            HttpResponseMessage response2 = await client.GetAsync(baseAddress + "/ProductAPI/GetCategories");
            if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listTransaction = JsonConvert.DeserializeObject<List<Transaction>>(data1);
                

            }
            return Page();

        }
    }
}
