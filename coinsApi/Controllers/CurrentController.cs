using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace coinsApi.Controllers
{
    //CURRENT endpoint - current price in USD
    //using api/CURRENT/{currency_id}
    //return simple string

    [ApiController]
    [Route("api/[controller]")]
    public class CurrentController : ControllerBase
    {
        //GET empty
        [HttpGet]
        public IActionResult GetCurrentPrice()
        {
            return Content("Current price. Value not found. Use /current/<value>");
        }

        //GET current price by id
        [HttpGet("{id}",Name="GetCurrent")]
        public async Task<IActionResult> GetCurrentPriceById(string id)
        {
            
            string current = await CoinsGeckoApi.GetCurrentMethod(id);
            if (string.IsNullOrEmpty(current))
                return NotFound();
            else
                return Content(current);

        }

    }
}
