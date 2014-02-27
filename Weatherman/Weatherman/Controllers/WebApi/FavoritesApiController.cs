using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeathermanDataLayer;
using WeathermanServiceLayer.Implementation;
using WeathermanServiceLayer.Interfaces.Services;
using WeathermanServiceLayer.Messages;
using WeathermanServiceLayer.ViewModels;

namespace WeathermanWeb.Controllers.WebApi
{
    public class FavoritesApiController : ApiController
    {

        private readonly weathermanEntities _db = new weathermanEntities();



        public IEnumerable<IndividualWeatherEntryViewModel> GetAllFavorites(string userCode)
        {
            //cheating new to Castle Windsor.
            var fav = new FavoritesService();

            var request = new UserFavoritesApiRequest {UserCode = userCode,WeathermanEntities = _db};


            var response = fav.GetUserFavoritesApiRequest(request);

            if (response.Success)
            {
                return response.IndividualWeatherEntryViewModels;
            }

            return new List<IndividualWeatherEntryViewModel>();
        }

    }
}
