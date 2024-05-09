using API_SYSTEM.Commons;
using Application.IRepository;
using BusinessObject.DTOs;
using BusinessObject.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_SYSTEM.Controllers
{
    [Route("api/statistic")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public StatisticController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("get-statistic-result")]
        public async Task<ActionResult> GetStatisticResult(int userID, int cateID, DateTime startDate, DateTime endDate)
        {
            var statisticList = await _transactionRepository.GetTransactionByStatistic(userID, cateID, startDate, endDate);
            if (!statisticList.Any())
            {
                return NotFound(new APIReponse<IEnumerable<StatisticTransactionDTO>>
                {
                    StatusCode = 200,
                    Result = false,
                    Message = $"Currently no transaction!",
                    Data = statisticList
                });
            }
            return Ok(new APIReponse<IEnumerable<StatisticTransactionDTO>>
            {
                StatusCode = 200,
                Result = true,
                Message = $"Get transaction list successful!",
                Data = statisticList
            });
        }

        [HttpGet("export-statistic-file")]
        public async Task<ActionResult> ExportStatisticFile(int userID, int cateID, DateTime startDate, DateTime endDate)
        {
            var statisticList = await _transactionRepository.GetTransactionByStatistic(userID, cateID, startDate, endDate);
            if (!statisticList.Any())
            {
                return NotFound(new APIReponse<IEnumerable<StatisticTransactionDTO>>
                {
                    StatusCode = 200,
                    Result = false,
                    Message = $"Currently no transaction!",
                    Data = statisticList
                });
            }

            var empData = GetEmpData(statisticList);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(empData, "Statistic Result");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Statistic.xlsx");
                }
            }
        }

        [NonAction]
        private DataTable GetEmpData(IEnumerable<StatisticTransactionDTO> list)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Emp Data";
            dt.Columns.Add("Transaction ID", typeof(int));
            dt.Columns.Add("Category Type", typeof(string));
            dt.Columns.Add("Merchant", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Notes", typeof(string));
            dt.Columns.Add("Transaction Date", typeof(DateTime));

            if (list.Count() > 0)
            {
                foreach (StatisticTransactionDTO item in list)
                {
                    dt.Rows.Add(item.TransId, item.CategoryType, item.Merchant, item.Amount, item.Notes, item.TransactionsDate);
                }
            }
            return dt;
        }
    }
}
