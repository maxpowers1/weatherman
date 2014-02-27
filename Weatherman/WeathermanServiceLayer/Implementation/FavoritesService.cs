using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeathermanDataLayer;
using WeathermanServiceLayer.Interfaces.Services;
using WeathermanServiceLayer.Messages;
using WeathermanServiceLayer.Utility;
using WeathermanServiceLayer.ViewModels;

namespace WeathermanServiceLayer.Implementation
{
    public class FavoritesService : IFavoritesService
    {
        public IWeatherLookupService WeatherLookupService { get; set; }

        public UserFavoritesResponse GetUserFavorites(UserFavoritesRequest request)
        {
            var response = new UserFavoritesResponse() { Success = false, Message = "Unable to get user favorites" };

            try
            {
                var dbInstance = request.WeathermanEntities;
                var savedLocations = dbInstance.SavedLocations.Where(s=>s.UserId==request.UserId);
                var userCode = string.Empty;
                var userCodeEntity = dbInstance.UserCodes.SingleOrDefault(s => s.UserId == request.UserId);
    
                if (userCodeEntity != null)
                {
                    userCode = userCodeEntity.Code;
                }
                var favoriteViewModel = new FavoritesViewModel { Username = request.UserName,UserCode=userCode };
                var individualWeatherEntryViewModels = new List<IndividualWeatherEntryViewModel>();
                foreach (var savedLocation in savedLocations)
                {
                    individualWeatherEntryViewModels.Add(GetWeatherData(savedLocation.ZipCode, request.UserId, request.UserName));
                }
                favoriteViewModel.WeatherEntriesViewModels = individualWeatherEntryViewModels;

                response.FavoritesViewModel = favoriteViewModel;

            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Exception = exception;
                return response;
            }
            response.Success = true;
            response.Message = string.Empty;
            return response;
        }

        private IndividualWeatherEntryViewModel GetWeatherData(string zipCode, Guid userId, string userName)
        {
            var individualWeatherEntryViewModel = new IndividualWeatherEntryViewModel();
            var request = new WeatherLookupRequest { ZipCode = zipCode };
            var response = WeatherLookupService.WeatherLookupRequest(request);
            if (!response.Success) return individualWeatherEntryViewModel;
            individualWeatherEntryViewModel.CurrentTemperature = response.Temperature;
            individualWeatherEntryViewModel.CurrentConditions = response.Conditions;
            individualWeatherEntryViewModel.UserId = userId;
            individualWeatherEntryViewModel.Username = userName;
            individualWeatherEntryViewModel.PostalCode = zipCode;
            return individualWeatherEntryViewModel;
        }

        public SaveUserFavoriteResponse SaveUserFavorite(SaveUserFavoriteRequest request)
        {
            var response = new SaveUserFavoriteResponse() { Success = false, Message = "Unable to save user favorite" };
            try
            {
                //Check to see if we have already created the 5 digit unique user code.
                //Not going to check to see if we get a duplicate code for this exercise
                var userCodeExists = request.WeathermanEntities.UserCodes.Any(w => w.UserId == request.UserId);

                //Will add the code if it does not exist for the logged in user.
                if (!userCodeExists)
                {
                    var userCode = new UserCode {UserId = request.UserId};
                    var typingMonkey = new TypingMonkey();
                    userCode.Code = typingMonkey.TypeAway(5);
                    request.WeathermanEntities.UserCodes.Add(userCode);
                }
                
                var savedLocation = new SavedLocation
                {
                    //Did not get a chance to implement saving the city yet.
                    City = string.Empty,
                    ZipCode = request.PostalCode,
                    UserId = request.UserId
                };
                request.WeathermanEntities.SavedLocations.Add(savedLocation);
                request.WeathermanEntities.SaveChanges();
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Exception = exception;
                return response;
            }
            response.Success = true;
            response.Message = string.Empty;
            return response;
        }

    }
}
