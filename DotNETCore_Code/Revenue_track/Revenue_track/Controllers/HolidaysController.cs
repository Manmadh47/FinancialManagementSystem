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
    public class HolidaysController : Controller
    {
        private readonly HolidaysService holidayService;
    
        public HolidaysController(HolidaysService holidayServiceParam)
        {
            holidayService = holidayServiceParam;
        }
        [HttpGet]
        public IEnumerable<Holidays> Get() =>
               holidayService.Get();

        [HttpGet("{id:length(24)}", Name = "GetHoliday")]
        public ActionResult<Holidays> Get(string id)
        {
            var holidayRow = holidayService.Get(id);

            if (holidayRow == null)
            {
                return NotFound();
            }

            return holidayRow;
        }

        [HttpPost]
        public IActionResult Post(Holidays[] holidayParam)
        {
            foreach (var row in holidayParam)
            {
               holidayService.Create(row);
            }
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Holidays holidayParam)
        {
            var holidayRow = holidayService.Get(id);

            if (holidayRow == null)
            {
                return NotFound();
            }

            holidayService.Update(id, holidayParam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var holidayRow = holidayService.Get(id);

            if (holidayRow == null)
            {
                return NotFound();
            }


            holidayService.Remove(holidayRow.id);

            return NoContent();
        }
    }
}
