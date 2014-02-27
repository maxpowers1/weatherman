using System;
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

            try
            {
                //Using rest sharp to pull weather data from the weather api url.
                var client = new RestClient(_apiUrl);
                var zipCodeRequest = string.Format(_apiRestRequest, request.ZipCode);
                var restRequest = new RestRequest(zipCodeRequest, Method.GET) { RequestFormat = DataFormat.Json };


                //Used http://json2csharp.com/ to build C# classes from the worldWeatherONline API.
                var restResponse = client.Execute<WorldWideWeatherOnline.JSONClasses.RootObject>(restRequest);
                
                //return a failed response if we are unable to pull weather data.
                if (restResponse.ErrorException != null)
                {
                    response.Success = false;
                    response.Exception = restResponse.ErrorException;
                    response.Message = restResponse.ErrorMessage;
                    return response;
                }

                //If any piece of restResponse we need was not returned. Return a failed response.
                if (restResponse.Data == null || restResponse.Data.data == null || restResponse.Data.data.current_condition == null)
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

                //Weather descriptions is a collection, so build a string with all the values
                var weatherDescriptions = currentConditions.weatherDesc;
                var conditions = new StringBuilder();
                foreach (var weatherDescription in weatherDescriptions)
                {
                    conditions.AppendLine(weatherDescription.value);
                }

                
                var weatherIconUrls = currentConditions.weatherIconUrl;

                //Add all weather icon urls gotten to the response.
                foreach (var weatherIconUrl in weatherIconUrls)
                {
                    response.WeatherIconUrls.Add(weatherIconUrl.value);
                }

                response.Temperature = currentConditions.temp_F + " degrees Farenheight";
                response.Conditions = conditions.ToString();

            }
            catch (Exception exception)
            {
                response.Exception = exception;
                response.Success = false;
                return response;
            }
            response.Success = true;
            response.Message = string.Empty;
            return response;
        }



    }
}
