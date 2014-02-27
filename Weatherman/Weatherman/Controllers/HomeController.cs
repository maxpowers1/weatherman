using System;
using System.Web.Mvc;
using Castle.MicroKernel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WeathermanDataLayer;
using WeathermanServiceLayer.Implementation;
using WeathermanServiceLayer.Implementation.WorldWideWeatherOnline;
using WeathermanServiceLayer.Interfaces.Services;
using WeathermanServiceLayer.Messages;
using WeathermanServiceLayer.ViewModels.WeatherLookup;
using WeathermanWeb.CastleWindsor;

namespace WeathermanWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly weathermanEntities _db = new weathermanEntities();

        public HomeController()
        { }

        public virtual IWeatherLookupService WeatherLookupService { get; set; }
        public virtual IFavoritesService FavoritesService { get; set; }




        [HttpGet]
        public ActionResult Index()
        {
            return View(new WeatherLookupViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(WeatherLookupViewModel weatherLookupViewModel)
        {
            //Check if model state (Data annotations on viewmodel class) is valid if not return the model to the 
            //view.
            if (!ModelState.IsValid) return View(weatherLookupViewModel);

            //Create request to be sent to the WeatherLookupService
            var request = new WeatherLookupRequest { ZipCode = weatherLookupViewModel.ZipCodeSearch };


            //Execute the request against the weatherlookupservice
            var response = WeatherLookupService.WeatherLookupRequest(request);

            //If the service was unable to grab the current weather, then tell the view not to show the 
            //Weather search results box.
            weatherLookupViewModel.WeatherSearchPerformed = response.Success;

            //Depending on the state of the 
            weatherLookupViewModel.WeatherDataFound = response.Success;
            weatherLookupViewModel.CurrentConditions = response.Success ? response.Conditions : string.Empty;
            weatherLookupViewModel.CurrentTemperature = response.Success ? response.Temperature : string.Empty;
            if (response.Success)
            {
                weatherLookupViewModel.WeatherIconUrls = response.WeatherIconUrls;
            }

            return View(weatherLookupViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFavorite(string zipCodeSearch)
        {
            //Get the currently logged in user.  Better error trapping could be used here. 
            //Even though "Authorize" is set, still should be checking to see if the User.Identity call 
            //is available.
            var userId = new Guid(User.Identity.GetUserId());
            var request = new SaveUserFavoriteRequest
            {
                WeathermanEntities = _db,
                PostalCode = zipCodeSearch,
                UserId = userId
            };
            var response = FavoritesService.SaveUserFavorite(request);
            
            //if our service call succeeds, send the user to the Favorites view, otherwise just put them back
            //at the Index page. 
            //A more detailed explanation should be added to the UI as to why their "Favorite"
            //was not saved.
            return RedirectToAction(response.Success ? "Favorites" : "Index");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Favorites()
        {



            var userId = User.Identity.GetUserId();
            var userIdGuid = new Guid(userId);
            var userName = User.Identity.Name;
            var request = new UserFavoritesRequest { WeathermanEntities = _db, UserId = userIdGuid, UserName = userName };
            
            //If we are able to get the user's favorites, then display them
            //otherwise, return the user back to the index page.
            var response = FavoritesService.GetUserFavorites(request);
            if (response.Success)
            {
                return View(response.FavoritesViewModel);
            }
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}