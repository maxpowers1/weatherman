using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeathermanServiceLayer.ViewModels
{
   public class FavoritesViewModel
   {
       public IEnumerable<IndividualWeatherEntryViewModel> WeatherEntriesViewModels { get; set; }
       public string Username { get; set; }
       public string UserCode { get; set; }
   }

    public class IndividualWeatherEntryViewModel
    {
        public string PostalCode { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentConditions { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserCode { get; set; }
    }
}
