using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace coinsApi.Controllers
{
    //ATH endpoint - All time High in USD
    //using api/ATH/{currency_id}
    //return simple string

    [ApiController]
    [Route("api/[controller]")]
    public class AthController : ControllerBase
    {
        //GET empty
        [HttpGet]

        public IActionResult GetAth()
        {
            //test
            return Content("All time high. Value not found. Use /ath/<value>");

        }

        //GET ath by id
        [HttpGet("{id}",Name="GetAth")]
        public async Task<IActionResult> GetAthById(string id)
        {
            
            string ath = await CoinsGeckoApi.GetAthMethod(id);
            if (string.IsNullOrEmpty(ath))
                return NotFound();
            else
                return Content(ath);

        }

    }
}
