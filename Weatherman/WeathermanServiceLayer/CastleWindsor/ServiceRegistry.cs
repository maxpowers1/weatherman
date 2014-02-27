using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WeathermanServiceLayer.Implementation;
using WeathermanServiceLayer.Implementation.WorldWideWeatherOnline;
using WeathermanServiceLayer.Interfaces.Services;

namespace WeathermanServiceLayer.CastleWindsor
{




    public class ServiceRegistry : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
                

            container.Register(Component.For<IWeatherLookupService>()
                     .ImplementedBy<WorldWideWeatherOnlineService>());

            container.Register(Component.For<IFavoritesService>()
         .ImplementedBy<FavoritesService>());

        }
    }


}
