using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using WeatherApp.Core.Domain;
using Newtonsoft.Json;

namespace WeatherApp.Core.Services
{
    public class WeatherService
    {
        private static string apiKey = "9f515ab125ef48a2";

        public static ConditionsResult GetWeatherFor(string zipCode)
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"http://api.wunderground.com/api/{apiKey}/conditions/q/CA/{zipCode}.json");

                /// var result = new ConditionsResult();  I'll write the rest of this as practice!

                var o = JObject.Parse(json);

                string currentLocationJson = o["current_observation"].ToString();

                var result = JsonConvert.DeserializeObject<ConditionsResult>(currentLocationJson);

                return result;
            }
        }
    }
}
