using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeathermanDataLayer;
using WeathermanServiceLayer.ViewModels;

namespace WeathermanServiceLayer.Messages
{
    public class UserFavoritesResponse:GenericResponse
    {
        public FavoritesViewModel FavoritesViewModel { get; set; }
    }

    public class UserFavoritesRequest
    {
        //Did not have time to implement a better solution.  Service 
        //layer should not know the specifics of how the data layer is saving data.
        public weathermanEntities WeathermanEntities { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }


    public class SaveUserFavoriteRequest
    {
        //Did not have time to implement a better solution.  Service 
        //layer should not know the specifics of how the data layer is saving data.
        public weathermanEntities WeathermanEntities { get; set; }
        public string PostalCode { get; set; }
        public Guid UserId { get; set; }
    }

    public class SaveUserFavoriteResponse:GenericResponse
    {
        
    }


}
