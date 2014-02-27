using System.Linq;
using System.Text;
using RestSharp;
using WeathermanServiceLayer.Interfaces.Services;
using WeathermanServiceLayer.Messages;

namespace WeathermanServiceLayer.Implementation.WorldWideWeatherOnline
{
    public class WorldWideWeatherOnlineService : IWeatherLookupService
    {
        private const string _apiUrl = "http://api.worldweatheronline.com";
        private const string _apiRestRequest = "free/v1/weather.ashx?q={0}&format=json&num_of_days=1&key=xvh22mvchzpr3dm5j45hf7ar";

        public string ApiUrl
        {
            get { return _apiUrl; }
        }

        public WeatherLookupResponse WeatherLookupRequest(WeatherLookupRequest request)
        {
            var response = new WeatherLookupResponse { Success = false, Message = "Failed to lookup weather." };

            var client = new RestClient(_apiUrl);

            var zipCodeRequest = string.Format(_apiRestRequest, request.ZipCode);
            var restRequest = new RestRequest(zipCodeRequest, Method.GET) { RequestFormat = DataFormat.Json };

            var restResponse = client.Execute<WorldWideWeatherOnline.JSONClasses.RootObject>(restRequest);
            if (restResponse.ErrorException != null)
            {
                response.Success = false;
                response.Exception = restResponse.ErrorException;
                response.Message = restResponse.ErrorMessage;
                return response;
            }

            if (restResponse.Data==null || restResponse.Data.data==null || restResponse.Data.data.current_condition==null)
            {
                response.Success = false;
                response.Message = "Unable to determine the current conditions";
                return response;
            }

            var returnedData = restResponse.Data;
            var currentConditions = returnedData.data.current_condition.FirstOrDefault();
            if (currentConditions == null)
            {
                response.Success = false;
                response.Message = "Unable to determine the current conditions";
                return response;
            }

            var weatherDescriptions = currentConditions.weatherDesc;
            var conditions = new StringBuilder();
            foreach (var weatherDescription in weatherDescriptions)
            {
                conditions.AppendLine(weatherDescription.value);
            }
            var weatherIconUrls = currentConditions.weatherIconUrl;

            foreach (var weatherIconUrl in weatherIconUrls)
            {
                response.WeatherIconUrls.Add(weatherIconUrl.value);
            }
            response.Success = true;
            response.Temperature = currentConditions.temp_F;
            response.Conditions = conditions.ToString();
            return response;
        }



    }
}
