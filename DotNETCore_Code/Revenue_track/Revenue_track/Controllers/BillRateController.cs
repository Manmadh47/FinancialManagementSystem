using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revenue_track.Service;
using Revenue_track.Model;

namespace Revenue_track.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillRateController : Controller
    {
        private readonly BillRatesService billRatesService;

        public BillRateController(BillRatesService billServiceParam)
        {
            billRatesService = billServiceParam;
        }

        [HttpGet]
        public IEnumerable<BillRates> Get() =>
               billRatesService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBillRate")]
        public ActionResult<BillRates> Get(string id)
        {
            var billRow = billRatesService.Get(id);

            if (billRow == null)
            {
                return NotFound();
            }

            return billRow;
        }

        [HttpPost]
        public IActionResult Post(BillRates billRate)
        {
            
                billRatesService.Create(billRate);
            
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, BillRates billRateParam)
        {
            var billRow = billRatesService.Get(id);

            if (billRow == null)
            {
                return NotFound();
            }

            billRatesService.Update(id, billRateParam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var billRateRow = billRatesService.Get(id);

            if (billRateRow == null)
            {
                return NotFound();
            }


            billRatesService.Remove(billRateRow.id);

            return NoContent();
        }
    }
}
