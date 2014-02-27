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




        [HttpGet]
        public ActionResult Index()
        {
            return View(new WeatherLookupViewModel());
        }

        [HttpPost]
        public ActionResult Index(WeatherLookupViewModel weatherLookupViewModel)
        {
            if (!ModelState.IsValid) return View(weatherLookupViewModel);
            var request = new WeatherLookupRequest {ZipCode = weatherLookupViewModel.ZipCodeSearch};
            weatherLookupViewModel.WeatherSearchPerformed = true;
            var response = WeatherLookupService.WeatherLookupRequest(request);

            weatherLookupViewModel.WeatherDataFound = response.Success;
            weatherLookupViewModel.CurrentConditions = response.Conditions;
            weatherLookupViewModel.CurrentTemperature = response.Temperature;
            weatherLookupViewModel.WeatherIconUrls = response.WeatherIconUrls;

            return View(weatherLookupViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveFavorite(string zipCodeSearch)
        {
            var userId = User.Identity.GetUserId();
            var savedLocation = new SavedLocation
            {
                City = string.Empty,
                ZipCode = zipCodeSearch,
                UserId = new Guid(userId)
            };
            _db.SavedLocations.Add(savedLocation);
            _db.SaveChanges();
            return View("Favorites");
        }


        [HttpGet]
        [Authorize]
        public ActionResult Favorites()
        {
            return View();
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