using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeathermanServiceLayer.Messages
{

    public class WeatherLookupRequest
    {
        public string ZipCode { get; set; }
    }

    public class WeatherLookupResponse : GenericResponse
    {
        public WeatherLookupResponse()
        {
            WeatherIconUrls = new List<string>();
        }


        public string Conditions { get; set; }
        public string Temperature { get; set; }
        public List<string> WeatherIconUrls { get; set; } 
    }

}
