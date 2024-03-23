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

    public sealed class ServicesConfigurer : IUnityConfigurer
    {
        private string[] assemblies;
        private string[] namespaces;

        public ServicesConfigurer(string[] assemblies, string[] namespaces)
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
            //IMatchingRule matchingRule = new CustomAttributeMatchingRule(typeof(DataSessionAttribute), false);
            IMatchingRule matchingRule = new ServiceMatchingRule();
            VirtualMethodInterceptor virInterceptor = new VirtualMethodInterceptor();

            //TransactionCallHandler tranCallHandler = new TransactionCallHandler() { Order = 1 };
            //LoggingCallHandler logCallHandler = new LoggingCallHandler() { Order = 2 };
            //ExceptionCallHandler expCallHandler = new ExceptionCallHandler() { Order = 3 };

            var callHandlers = container.ResolveAll<IServiceHandler>().ToArray();

            foreach (Type typ in serviceTypes)
            {
                //SessionHandler sessHandler = container.Resolve<SessionHandler>();
                string className = typ.Name;
                container.RegisterType(typeof(Object), typ, className, new ContainerControlledLifetimeManager());
                //container.RegisterType(typ, typ, null, new ContainerControlledLifetimeManager());
                //container.Configure<Interception>()
                //   .SetDefaultInterceptorFor(typ, virInterceptor)
                //   .AddPolicy("InterceptSessionAttribute")
                //   .AddCallHandler(sessHandler)
                //   .AddMatchingRule(matchingRule);

                if(typeof(BaseService).IsAssignableFrom(typ))
                {
                    PolicyDefinition polDef = container.Configure<Interception>()
                       .SetDefaultInterceptorFor(typ, virInterceptor)
                       .AddPolicy("InterceptBusinessService");

                    foreach(ICallHandler callHandler in callHandlers)
                        polDef.AddCallHandler(callHandler);

                    polDef.AddMatchingRule(matchingRule);
                }
            }
        }

        #endregion
    }
}