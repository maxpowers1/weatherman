using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WeathermanServiceLayer.Messages;

namespace WeathermanServiceLayer.Interfaces.Services
{
    public interface IWeatherLookupService
    {
        string ApiUrl { get; }

        WeatherLookupResponse WeatherLookupRequest(WeatherLookupRequest request);


    }
}
