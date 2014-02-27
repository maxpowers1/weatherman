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
            if (!ModelState.IsValid) return View(weatherLookupViewModel);
            var request = new WeatherLookupRequest { ZipCode = weatherLookupViewModel.ZipCodeSearch };
            weatherLookupViewModel.WeatherSearchPerformed = true;
            var response = WeatherLookupService.WeatherLookupRequest(request);

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
            var userId = new Guid(User.Identity.GetUserId());
            var request = new SaveUserFavoriteRequest
            {
                WeathermanEntities = _db,
                PostalCode = zipCodeSearch,
                UserId = userId
            };
            var response = FavoritesService.SaveUserFavorite(request);

            return RedirectToAction("Favorites");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Favorites()
        {



            var userId = User.Identity.GetUserId();
            var userIdGuid = new Guid(userId);
            var userName = User.Identity.Name;
            var request = new UserFavoritesRequest { WeathermanEntities = _db, UserId = userIdGuid, UserName = userName };
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