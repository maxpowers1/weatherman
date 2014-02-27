using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeathermanServiceLayer.Messages;

namespace WeathermanServiceLayer.Interfaces.Services
{
   public interface IFavoritesService
   {
       UserFavoritesResponse GetUserFavorites(UserFavoritesRequest request);
       //UserFavoritesApiResponse GetUserFavoritesApiRequest(UserFavoritesApiRequest request);
       SaveUserFavoriteResponse SaveUserFavorite(SaveUserFavoriteRequest request);


   }
}
