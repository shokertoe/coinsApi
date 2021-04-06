using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Net;
using System.Globalization;

namespace coinsApi
{
    public class CoinsGeckoApi
    {
        //Default route to COINGECKO API
        private static string main_route = "https://api.coingecko.com/api/v3/";
        //Common method Request -> json to model 
        private static async Task<model> Request(string url)
        {

            model m = null;

            //Create http Request
            HttpWebRequest httprequest = WebRequest.Create(url) as HttpWebRequest;
            httprequest.Method = "GET";

            try
            {
                using (HttpWebResponse response = await httprequest.GetResponseAsync() as HttpWebResponse)
                {
                    //convert JSON to model
                    using (StreamReader reponse = new StreamReader(response.GetResponseStream()))
                    {
                        string Output = string.Empty;
                        Output = reponse.ReadToEnd();
                        m = JsonSerializer.Deserialize<model>(Output);
                    }
                }
            }
            catch
            {
                //Error of getting data
            }

            return m;


        }

        //simple model to Coingecko/api/Coins endpoint
        private class model
        {
            public string id { get; set; }
            public Dictionary<string, object> market_data { get; set; }
        }



        //-----------ATH------------------------------

        //GET from api ATH, (ATH by ID into usd (xxxxx.xxx format))   
        public async static Task<string> GetAthMethod(string id)
        {

            model res = await Request(string.Format("{0}coins/{1}?market_data=true&tickers=false&developer_data=false&community_data=false", main_route, id));
            if (res == null)
                return string.Empty;
            else
            {
                if (res.market_data.ContainsKey("ath"))
                {
                    float val = JsonSerializer.Deserialize<Dictionary<string, float>>(res.market_data["ath"].ToString())["usd"];
                    return String.Format("{0:0.###}", val);
                }
                else
                    return string.Empty;
            }
        }



        //-----------CURRENT PRICE------------------------------

        //GET from api CURRENT PRICE, (CURRENT PRICE by ID into usd (xxxxx.xxx format))   
        public async static Task<string> GetCurrentMethod(string id)
        {

            model res = await Request(string.Format("{0}coins/{1}?market_data=true&tickers=false&developer_data=false&community_data=false", main_route, id));
            if (res == null)
                return string.Empty;
            else
            {
                if (res.market_data.ContainsKey("current_price"))
                {
                    float val = JsonSerializer.Deserialize<Dictionary<string, float>>(res.market_data["current_price"].ToString())["usd"];
                    return String.Format("{0:0.###}", val);
                }
                else
                    return string.Empty;
            }
        }



        //-------------ATH_DATE-----------------

        //Convert DataString to task XX январ(я - в родит. падеже) 1983 
        private static string ConvertDate(DateTime dt)
        {
            string res = string.Empty;

            return res;
        }

        //output JSON class
        private class dateClass
        {
            public string dt { get; set; }
            public string dtstr { get; set; }

            public dateClass(DateTime date)
            {
                dt = date.ToString("dd.MM.yyyy");
                dtstr = date.ToString("dd MMMM yyyy", new CultureInfo("ru-RU"));

            }
        }
        
        //GET from api- ATH_DATE
        public async static Task<string> GetAthDateMethod(string id)
        {
            model res = await Request(string.Format("{0}coins/{1}?market_data=true&tickers=false&developer_data=false&community_data=false", main_route, id));
            if (res == null)
                return string.Empty;
            else
            {
                if (res.market_data.ContainsKey("ath_date"))
                {
                    DateTime val = JsonSerializer.Deserialize<Dictionary<string, DateTime>>(res.market_data["ath_date"].ToString())["usd"];
                    dateClass dt_res = new dateClass(val);
                    return JsonSerializer.Serialize<dateClass>(dt_res);
                    
                }
                else
                    return string.Empty;
            }
        }
    }
}
