using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeathermanServiceLayer.ViewModels.WeatherLookup
{
   public class WeatherLookupViewModel
   {
       public WeatherLookupViewModel()
       {
           this.ZipCodeSearch = string.Empty;
           this.City = string.Empty;
           this.WeatherDataFound = false;
           this.WeatherSearchPerformed = false;
           this.WeatherIconUrls = new List<String>();
       }


       [Required(ErrorMessage = "Please enter a zip code",AllowEmptyStrings = false)]
       [RegularExpression("^\\d{5}$",ErrorMessage = "Zip code must be five numbers.")]
       [Display(Name = "Enter a zip code:")]
       public string ZipCodeSearch { get; set; }
       public string City { get; set; }
       public bool WeatherSearchPerformed { get; set; }
       public bool WeatherDataFound { get; set; }
       public string CurrentConditions { get; set; }
       public string CurrentTemperature { get; set; }
       public string LatLong { get; set; }
       public IEnumerable<string> WeatherIconUrls { get; set; } 

   }
}
