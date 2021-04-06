using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace coinsApi.Controllers
{
    //ATHDATE endpoint - All time High Date
    // using api/ATHDATE/{currency_id}
    //return JSON {"dt":string,"dtstr":string}

    [ApiController]
    [Route("api/[controller]")]
    public class AthDateController : ControllerBase
    {
        //GET empty
        [HttpGet]
        public IActionResult GetAthDate()
        {
            return Content("All time high date. Value not found. Use /athdate/<value>");
        }

        //GET athdate by id
        [HttpGet("{id}",Name="GetAthDate")]
        public async Task<IActionResult> GetAthDateById(string id)
        {
            
            string athdate = await CoinsGeckoApi.GetAthDateMethod(id);
            if (string.IsNullOrEmpty(athdate))
                return NotFound();
            else

                return Content(athdate,"application/json");

        }

    }
}
