using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revenue_track.Model;
using Revenue_track.Service;

namespace Revenue_track.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueTrackerController : Controller
    {
       
        private readonly RevenueService _Service;

        public RevenueTrackerController(RevenueService Service)
        {
            _Service = Service;
        }
        [HttpGet]
        public IEnumerable<Revenue> Get() =>
               _Service.Get();

        //[HttpGet]
        //[Route("getDuplicates")]
        //public IEnumerable<Revenue> GeDuplicatesRows() =>
        //       _Service.GetDuplicates();

        [HttpGet]
        [Route("getLatestRecords")]
        public IEnumerable<Revenue> GetLatestRecords() =>
               _Service.GetByLatestRecords();

        [HttpGet]
        [Route("year")]
        public IEnumerable<string> GetYears()=>
            _Service.GetYear();

        [HttpGet]
        [Route("monthsByYear/{year}")]
        public IEnumerable<string> GetMonthsByYear(string year) =>
            _Service.GetMonth(year);

        [HttpGet]
        [Route("resourceByYearAndMonth/{year}/{month}")]
        public IEnumerable<Revenue> GetByYearAndMonth(string year, string month) =>
            _Service.GetByYearAndMonth(year,month);


        [HttpGet("{id:length(24)}", Name = "GetRevenue")]
        public ActionResult<Revenue> Get(string id)
        {
            var revenue = _Service.Get(id);

            if (revenue == null)
            {
                return NotFound();
            }

            return revenue;
        }

        [HttpPost]
        public List<Revenue> Create([FromBody] Revenue[] revenue)
        {
            List<Revenue> sortedRevenue=revenue.OrderBy(res => res.start_date).ToList();

            List<Revenue> records = new List<Revenue>();
            _Service.returnDuplicates.Clear();

            foreach (var emp in sortedRevenue) {
                if (emp.id == null) {
                    records=_Service.Create(emp);
                }
                else
                {
                    var revenueId = _Service.Get(emp.id);

                    _Service.Update(emp.id, emp);
                }
            }
            return records;
        }


        [HttpPost]
        [Route("forecast")]
        public List<Revenue> CreateForecast([FromBody] Revenue[] revenue)
        {
            List<Revenue> sortedRevenue = revenue.OrderBy(res => res.start_date).ToList();
            List<Revenue> records = new List<Revenue>();

            foreach (var emp in sortedRevenue)
            {
               records = _Service.CreateForecast(emp);
            }
               return records;
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Revenue revenueIn)
        {
            var revenue = _Service.Get(id);

            if (revenue == null)
            {
                return NotFound();
            }

            _Service.Update(id, revenueIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var revenue = _Service.Get(id);

            if (revenue == null)
            {
                return NotFound();
            }


            _Service.Remove(revenue.id);

            return NoContent();
        }
    }
}
