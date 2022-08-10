using BusinessLogicLayer.ControllerHelper.ProfileController;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Utilities;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Repositories;
using DataAccessLayer.Data.UnitOfWork;
using System;

using Unity;
using Unity.Lifetime;

namespace bucketSubs.service
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IProfileControllerHelper, ProfileControllerHelper>();
            container.RegisterType<IXMLSerializer, XMLSerializer>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IMApper, Mapper>();
            container.RegisterType<IProfileStatusRepo, ProfileStatusRepo>();
            container.RegisterType<IClassesMigrationRepo, ClassesMigrationRepo>();

            container.RegisterType<ApplicationUserManager>(new PerResolveLifetimeManager());
            container.RegisterType<ApplicationDbContext>(new PerResolveLifetimeManager());


            //Inject EF6 DbContext with each request (Singleton)
            //container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
            //If you forget to specify it, a new DbContext will be injected in each of your repositories/command/query objects,
            //you'll not be able to share the context and everything will fall apart.
        }
    }
}