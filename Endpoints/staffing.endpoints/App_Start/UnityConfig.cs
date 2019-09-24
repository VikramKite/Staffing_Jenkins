using staffing.data.ef.Lookups;
using staffing.data.ef.Staffing;
using staffing.data.repository.Lookups;
using staffing.data.repository.Staffing;
using staffing.endpoints.Controllers;
using staffing.interfaces.data.Lookups;
using staffing.interfaces.data.Staffing;
using staffing.interfaces.processor.Lookups;
using staffing.interfaces.processor.Staffing;
using staffing.interfaces.repository.Lookups;
using staffing.interfaces.repository.Staffing;
using staffing.processor.webadmin.Lookups;
using staffing.processor.webadmin.Staffing;
using System;

using Unity;
using Unity.Injection;

namespace staffing.endpoints
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

            //https://stackoverflow.com/questions/45429116/iuserstoremodels-applicationuser-is-not-resolved-by-unity-interface-to-class
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IStaffingData, StaffingData>();
            container.RegisterType<IStaffingRepository, StaffingRepository>();
            container.RegisterType<IStaffingProcessor, StaffingProcessor>();
            container.RegisterType<IStaffingProcessor, StaffingProcessor>();
            container.RegisterType<IStaffingRepository, StaffingRepository>();

            container.RegisterType<ILookupsData, LookupsData>();
            container.RegisterType<ILookupsRepository, LookupsRepository>();
            container.RegisterType<ILookupsProcessor, LookupsProcessor>();
        }
    }
}