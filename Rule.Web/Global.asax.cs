using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AdIns.Service;
using Confins.Common;
using Confins.DataModel.Common.MiscModel;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace Rule.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            UnityFactory.CreateContainer(
                new String[] { "unity", "unity-formatter", "unity-genericlookup" },
                new string[] { "ConfinsServiceConfigurer", "ConfinsQueryConfigurer", "ConfinsGenericLookupConfigurer","TestServiceConfigurer" });
            Application["container"] = UnityFactory.GetContainer();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }        
    }
}
