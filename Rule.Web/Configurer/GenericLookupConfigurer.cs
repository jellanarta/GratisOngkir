using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using AdIns.Service;
using Rule.Web.WebUserControl.GenericLookup;

namespace Rule.Web.Configurer
{
    public class GenericLookupConfigurer : IUnityConfigurer
    {
        #region IUnityConfigurer Members

        public void Configure(IUnityContainer container)
        {
            var genericLookupsData = container.ResolveAll<GenericLookupData>();
            string scriptFilePath = HttpContext.Current.Server.MapPath("~/") + ConfigurationManager.AppSettings[GenericLookupData.SCRIPT_VIRTUAL_PATH].Substring(2).Replace('/', '\\'); ;
            GenericLookupScriptGenerator scriptGenerator = new GenericLookupScriptGenerator(scriptFilePath);
            foreach (var genericLookupData in genericLookupsData)
                scriptGenerator.WriteFunction(genericLookupData.ColumnBinders, genericLookupData.FunctionName);
            scriptGenerator.Close();
        }

        #endregion
    }
}