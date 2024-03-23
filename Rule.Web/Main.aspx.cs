using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using Confins.WebLib;
using Confins.Common;
using System.Web.Security;
using Confins.BusinessService.Common.UserManagement;
using System.Collections;
using Microsoft.Practices.Unity;
using AdIns.Service;
using System.Text;
using AdIns.Service.QueryService;
using AdIns.Util;
using AdIns.Util.Query;
using System.Reflection;
using Rule.Web.WebUserControl;
using System.Xml.Serialization;
using System.Xml;
using Confins.DataModel.Common.NCModel;

namespace Rule.Web
{
    public partial class Main : WebPageBase
    {
        #region "OVERRIDING PAGEPERSISTER"
        private PageStatePersister _pageStatePersister;
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                if (_pageStatePersister == null)
                    _pageStatePersister = new HiddenFieldPageStatePersister(this);
                return _pageStatePersister;
            }
        }
        #endregion

        #region "PROPERTIES"
       
        #region "NOTIFICATION PROPERTIES"
        //private Int32 notifSize
        //{
        //    get { return UCNotification.TotalNotifRecord; }
        //}
        //private Int32 currentNotifPage
        //{
        //    get { return UCNotification.CurrentNotifPage; }
        //}
        //private IList<NcMessage> listOfLoadedNotification
        //{
        //    get { return (IList<NcMessage>)ViewState["listOfLoadedNotification"]; }
        //    set { ViewState["listOfLoadedNotification"] = value; }
        //}
        #endregion

        #region "CURRENT USER CONTEXT INITIALISATION"
        private bool isOldMessage
        {
            get { return (bool)ViewState["isOldMessage"]; }
            set { ViewState["isOldMessage"] = value; }
        }
        #endregion

        #endregion

        #region "LICENSE MANAGER"
        #endregion

        #region "REGISTER JS & CSS"
        protected override void RegisterRequiredJSLib()
        {
            base.RegisterRequiredJSLib();
            string[] libs = new string[] 
            {
                this.ResolveUrl("~/Scripts/NCScript_Main.js")
            };

            foreach (string lib in libs)
            {
                ClientScript.RegisterClientScriptInclude(lib, lib);
            }
        }

        protected override void RegisterRequiredCSSLib()
        {
            base.RegisterRequiredCSSLib();
            string[] libs = new string[] 
                {
                    this.ResolveUrl("~/Styles/NC_Main.css")
                };

            foreach (string lib in libs)
            {
                AddCSSLib(lib);
            }
        }
        #endregion

        #region "PAGE LOAD"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Url = Request["openUrl"];
                if (Url != null)
                    if (Url != "")
                        mainPage.Attributes.Add("src", Page.ResolveUrl(Url));

            }
            ltlWelcomeMsg.Text = "Welcome, " + this.CurrentUserContext.UserId;
            
            ltlBusinessDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

        }
        #endregion

        #region "LOGOUT"
        protected void navLogout_Click(object sender, EventArgs e)
        {
            //ConfinsUserContext context = (ConfinsUserContext)Session[ConfinsConstant.SESSION_USER_CONTEXT];
            //if (context != null)
            //{
            //    UserActivityLogService service = UnityFactory.Container.Resolve<UserActivityLogService>("UserActivityLogService");
            //    service.UserSessionLogout(context.UserSessionLogId, DateTime.Now);
            //}
            //Session.Remove(ConfinsConstant.SESSION_USER_CONTEXT);
            Session.Remove("treeLastState"); // untuk meremove session yang digunakan pada tree menu (erwin)
            if (base.CurrentUserContext != null)
            {
                UserActivityLogService service = (UserActivityLogService)UnityFactory.Resolve<UserActivityLogService>("UserActivityLogService");
                service.UserSessionLogout(base.CurrentUserContext.UserSessionLogId, DateTime.Now);
            }
            FormsAuthentication.SignOut();
            Response.Clear();
            Response.Redirect("~/Login.aspx");
        }
        #endregion
    }
}