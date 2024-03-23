using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using AdIns.Util;
using AdIns.Service;
//using AdIns.DataAccess.Session;
using Confins.BusinessService.Common.ServiceCallHandler;
using Confins.BusinessService.Common;

namespace Rule.Web.Configurer
{

    public sealed class InterfaceConfigurer : IUnityConfigurer
    {
        private string[] assemblies;
        private string[] namespaces;

        public InterfaceConfigurer(string[] assemblies, string[] namespaces)
        {
            this.assemblies = assemblies;
            this.namespaces = namespaces;
        }


        #region IUnityConfigurer Members

        public void Configure(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();

            ClassDiscovererHelper classDisc = new ClassDiscovererHelper(this.assemblies, this.namespaces);
            Type[] serviceTypes = classDisc.GetTypes();
            IMatchingRule matchingRule = new ServiceMatchingRule();
            InterfaceInterceptor ifcInterceptor = new InterfaceInterceptor();
            var callHandlers = container.ResolveAll<IServiceHandler>().ToArray();

            foreach (Type typ in serviceTypes)
            {
                Type[] interfaces = typ.GetInterfaces();
                string className = typ.Name;
                if (interfaces.Count() > 0 && typeof(BaseService).IsAssignableFrom(typ))
                {
                    container.RegisterType(interfaces[0], typ, new ContainerControlledLifetimeManager());
                    PolicyDefinition polDef = container.Configure<Interception>()
                       .SetDefaultInterceptorFor(interfaces[0], ifcInterceptor)
                       .AddPolicy("InterceptBusinessService");

                    foreach (ICallHandler callHandler in callHandlers)
                        polDef.AddCallHandler(callHandler);

                    polDef.AddMatchingRule(matchingRule);
                }
            }
        }

        #endregion
    }
}