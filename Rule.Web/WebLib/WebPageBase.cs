using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Confins.Common;
using System.IO;
//using Confins.Common.Exp;
using Microsoft.Practices.Unity;
using System.Collections;
using System.Web.Security;
using Rule.Web.WebUserControl;
using AdIns.Util.Log;
using Confins.Web.Security;
using System.Xml.Serialization;
using System.Xml;
using Confins.BusinessService.Common.UserManagement;
//using Confins.BusinessService.Common.MessageHandler;
using AdIns.Service;
using AdIns.Util;
using Confins.DataModel.Common.MiscModel;
using Confins.DataModel.Common.NCModel;

namespace Rule.Web
{
    /// <summary>
    /// Provides base for any page created in Confins web. This class is abstract.
    /// </summary>
    public abstract class WebPageBase : Page
    {
        private string _className;

        /// <summary>
        /// Gets the name of this class in string.
        /// </summary>
        private string className
        {
            get
            {
                if (_className == null)
                    _className = this.GetType().ToString();
                return _className;
            }
        }

        static WebPageBase()
        {
        }

        public WebPageBase()
        {

            this.Init += new EventHandler(DefaultCustomPageInit) + new EventHandler(SessionAuth);
            this.Load += new EventHandler(DefaultCustomPageLoad);
        }

        private void DefaultCustomPageInit(object sender, EventArgs e)
        {
            if (CurrentScriptManager != null)
            {
                CurrentScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(AsyncPostBackError);
            }

            loadPageRedirectionState();

            // LoadingPanel
            //UCLoadingPanel ucLoadingPanel = LoadControl(ResolveUrl("~/WebUserControl/UCLoadingPanel.ascx")) as UCLoadingPanel;
            //Form.Controls.Add(ucLoadingPanel);

            #region Piece of code to prevent direct access to form
            //if (base.User.Identity.IsAuthenticated)
            //{
            //    string confMainPage = System.Configuration.ConfigurationManager.AppSettings["MainPage"];
            //    string mainPagePath = confMainPage.Substring(confMainPage.IndexOf("/"));
            //    string resolvedMainPage = base.ResolveUrl(confMainPage);
            //    string scrMainPage = "if(window.location.pathname.toUpperCase()!=('" + mainPagePath + "').toUpperCase() && window.parent.location==window.location && window.parent.parent.location==window.parent.location) ";
            //    scrMainPage += "window.location='" + resolvedMainPage + "?openUrl=" + base.AppRelativeVirtualPath + "';";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "preventDirectAccess", scrMainPage, true);
            //}
            #endregion
        }

        private void DefaultCustomPageLoad(object sender, EventArgs e)
        {
            if (IsAsyncPostback)
            {
                RegisterAsyncJSLoadScript();
            }
            else
            {
                RegisterRequiredJSLib();
                RegisterRequiredCSSLib();
            }
        }

        private PageStatePersister _pageStatePersister;
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                if (_pageStatePersister == null)
                    _pageStatePersister = new SessionPageStatePersister(this);
                return _pageStatePersister;
            }
        }

        private ScriptManager _currentScriptManager;
        protected ScriptManager CurrentScriptManager
        {
            get
            {
                if (_currentScriptManager == null)
                {
                    if (ScriptManager.GetCurrent(this) == null)
                    {
                        ScriptManager sm = new ScriptManager();
                        Form.Controls.Add(sm);
                    }
                    _currentScriptManager = ScriptManager.GetCurrent(this);
                }

                return _currentScriptManager;
            }
        }

        public bool IsAsyncPostback
        {
            get
            {
                if (CurrentScriptManager != null)
                    return CurrentScriptManager.IsInAsyncPostBack;
                else
                    return false;
            }
        }

        #region Global exception handler
        private string formErrorMessage()
        {
            //string errorMessage = "";
            //if (Context.Error is ConfinsException)
            //    errorMessage = ExceptionMessageHandler((ConfinsException)Context.Error);
            //else if (Context.Error is CompositeException)
            //    errorMessage = ExceptionMessageHandler((CompositeException)Context.Error);
            //else
            //    errorMessage = Context.Error.Message;

            string errorMessage = Context.Error.Message;

            return errorMessage;
        }

        protected override void OnError(EventArgs e)
        {
            if (!IsPostBack) //Response.Redirect("");
            {

            }
            else
            {
                if (!IsAsyncPostback)
                {
                    gotoBackPage(formErrorMessage());
                    Context.ClearError();
                }
                else base.OnError(e);
            }
        }

        //private string ExceptionMessageHandler(ConfinsException exp)
        //{
        //    if (exp.ErrorMessage != null)
        //        return string.Format("new Array('{0}')", exp.ErrorMessage.Replace("'", "\\'"));
        //    else
        //        return string.Format("new Array('{0}')", ConfinsMsgHandler.FormatExpMessage(exp, this.CurrentUserContext == null ? "EN" : this.LanguageCode).Replace("'", "\\'"));
        //}

        //private string ExceptionMessageHandler(CompositeException exp)
        //{
        //    String[] messages;
        //    if (exp.ErrorMessages != null)
        //        messages = exp.ErrorMessages;
        //    else
        //        messages = ConfinsMsgHandler.FormatExpMessage(exp.GetExceptions(), this.LanguageCode);

        //    StringBuilder sb = new StringBuilder(1024);
        //    sb.Append("new Array(");
        //    foreach (string msg in messages)
        //    {
        //        sb.Append("'");
        //        sb.Append(msg.Replace("'", "\\'"));
        //        sb.Append("',");
        //    }
        //    if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
        //    sb.Append(")");
        //    return sb.ToString();
        //}

        private void gotoBackPage(string errorMessage)
        {
            HtmlTextWriter htw = new HtmlTextWriter(Response.Output);
            htw.RenderBeginTag(HtmlTextWriterTag.Html);
            {
                htw.Write(string.Format("<script type='text/javascript'>{0}history.back();</script>", GenerateErrorScript(errorMessage)));
            }
            htw.RenderEndTag();
        }

        protected virtual string GenerateErrorScript(string errorMessage)
        {
            return "";
        }

        private void AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            CurrentScriptManager.AsyncPostBackErrorMessage = formErrorMessage();
        }
        #endregion

        #region Register required libraries
        protected virtual void RegisterRequiredJSLib()
        {
            string[] libs = new string[] 
            {
                this.ResolveUrl("~/Scripts/jquery-1.4.2.min.js"),
                this.ResolveUrl("~/Scripts/jquery-ui-1.8.2.custom.min.js"),
                this.ResolveUrl("~/Scripts/NCScript.js")
            };

            foreach (string lib in libs)
            {
                ClientScript.RegisterClientScriptInclude(lib, lib);
            }
        }

        public void AddCSSLib(string path)
        {
            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = path;
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(cssLink);
        }

        protected virtual void RegisterRequiredCSSLib()
        {
            string[] libs = new string[] 
                {
                    this.ResolveUrl("~/Styles/NC_Grid.css"),
                    this.ResolveUrl("~/Styles/NC_ModalWindows.css"),
                    this.ResolveUrl("~/Styles/NC_Toolbar.css"),
                    this.ResolveUrl("~/Styles/NC_Tooltips.css"),
                    this.ResolveUrl("~/Styles/NC_Button.css")
                };

            foreach (string lib in libs)
            {
                AddCSSLib(lib);
            }
        }
        #endregion
        #region Register JS Script
        protected virtual void RegisterAsyncJSLoadScript()
        {
        }
        #endregion
        #region Page redirection handler
        public class PageRedirectionState : IEnumerable<KeyValuePair<string, object>>
        {
            private PageRedirectionState()
            {
                items = new Dictionary<string, object>();
            }

            internal Dictionary<string, object> items { get; set; }

            public static PageRedirectionState InitAdd(string keyName, object value)
            {
                PageRedirectionState obj = new PageRedirectionState();
                obj.items.Add(keyName, value);
                return obj;
            }

            public PageRedirectionState Add(string keyName, object value)
            {
                this.items.Add(keyName, value);
                return this;
            }

            public object this[string keyName]
            {
                get { return items[keyName]; }
                set
                {
                    object currVal;
                    if (items.TryGetValue(keyName, out currVal))
                        items[keyName] = value;
                    else
                        this.Add(keyName, value);
                }
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                foreach (var i in items)
                    yield return i;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        protected void RedirectUrl(string requestedUrl, PageRedirectionState state = null)
        {
            if (state != null)
            {
                string stateId = Guid.NewGuid().ToString("N");
                if (requestedUrl.Contains('?'))
                    requestedUrl += "&";
                else
                    requestedUrl += "?";

                requestedUrl += string.Format("state={0}", stateId);
                Session[stateId] = state;
            }
            requestedUrl = ResolveUrl(requestedUrl);

            Response.Redirect(requestedUrl);
        }

        protected string PrepareRedirectUrl(string requestedUrl, PageRedirectionState state = null)
        {
            if (state != null)
            {
                string stateId = Guid.NewGuid().ToString("N");
                if (requestedUrl.Contains('?'))
                    requestedUrl += "&";
                else
                    requestedUrl += "?";

                requestedUrl += string.Format("state={0}", stateId);
                Session[stateId] = state;
            }
            requestedUrl = ResolveUrl(requestedUrl);

            return requestedUrl;
        }

        public PageRedirectionState RedirectState
        {
            get
            {
                return Context.Items["RedirectState"] as PageRedirectionState;
            }
            private set
            {
                Context.Items["RedirectState"] = value;
            }
        }

        private void loadPageRedirectionState()
        {
            if (!IsPostBack && Request.QueryString["state"] != null)
            {
                string stateId = Request.QueryString["state"];
                RedirectState = Session[stateId] as PageRedirectionState;
                Session.Remove(stateId);
            }
        }
        #endregion

        #region Extensions
        public void RegisterClientScriptStartupInclude(Control control, Type type, string key, string url)
        {
            ScriptManager.RegisterStartupScript(control, type, key, string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", url), false);
        }
        #endregion

        #region Login Information
        private ConfinsUserContext currentUserContext;
        public ConfinsUserContext CurrentUserContext
        {
            get { return currentUserContext; }
        }
        #endregion

        #region Session Checker
        protected virtual void SessionAuth(object sender, EventArgs e)
        {
            loginProcess();
            string LoginUrl = System.Configuration.ConfigurationManager.AppSettings["LoginPage"];
            string MainUrl = System.Configuration.ConfigurationManager.AppSettings["MainPage"];
            //string LicenseManagerUrl = System.Configuration.ConfigurationManager.AppSettings["LicenseManagerPage"];
            //string LicenseUploadUrl = System.Configuration.ConfigurationManager.AppSettings["LicenseUploadPage"];
            string RelativePath = Request.AppRelativeCurrentExecutionFilePath;

            //UserActivityLogService service = this.Container.Resolve<UserActivityLogService>();
            //UserActivityLogService service = UnityFactory.Resolve<UserActivityLogService>();
            //this.BusinessDate = service.GetBusinessDate();

            if (!User.Identity.IsAuthenticated)
            {
                if (RelativePath.ToUpper() != LoginUrl.ToUpper())
                {
                    string script = "";
                    string ReturnUrl = "";
                    if (RelativePath != MainUrl) ReturnUrl = "?ReturnUrl=" + Request.AppRelativeCurrentExecutionFilePath;
                    script = @"window.top.location='" + this.ResolveUrl(LoginUrl) + ReturnUrl + "'";
                    if (IsAsyncPostback == true && IsPostBack == true)
                    {
                        script = "eval=" + script;
                    }
                    else
                    {
                        script = "<script type='text/javascript'>" + script + ";</script>";
                    }
                    Response.Write(script);
                    Response.End();
                }
            }
            else
            {


                bool fRenew = false;

                FormsAuthenticationTicket ft = ((FormsIdentity)User.Identity).Ticket;
                if (!(RelativePath == MainUrl && Request.Form["__EVENTTARGET"] == "tmrNotification"))
                {
                    if (FormsAuthentication.SlidingExpiration)
                    {
                        FormsAuthenticationTicket newFt = FormsAuthentication.RenewTicketIfOld(ft);
                        if (newFt.Expiration > ft.Expiration)
                        {
                            HttpCookie objCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newFt));
                            Response.Cookies.Add(objCookie);
                            ft = newFt;
                            fRenew = true;
                        }
                    }
                }
                XmlReader reader = XmlTextReader.Create(new StringReader(ft.UserData));
                XmlSerializer serializer = new XmlSerializer(typeof(ConfinsUserContext));
                UserContextThreadLocal.Value = this.currentUserContext = serializer.Deserialize(reader) as ConfinsUserContext;

                //Toby, 2011/04/28
                if (!IsPostBack)
                {
                    DateTime dtNow = DateTime.Now;
                    if (HttpContext.Current.Session["OpenPageDt"] == null)
                        HttpContext.Current.Session.Add("OpenPageDt", dtNow);
                    else
                        HttpContext.Current.Session["OpenPageDt"] = dtNow;
                    currentUserContext.OpenPageDt = dtNow;
                    UserContextThreadLocal.Value.OpenPageDt = dtNow;
                }
                else
                {
                    if (HttpContext.Current.Session["OpenPageDt"] != null)
                    {
                        DateTime dtNow = (DateTime)HttpContext.Current.Session["OpenPageDt"];
                        currentUserContext.OpenPageDt = dtNow;
                        UserContextThreadLocal.Value.OpenPageDt = dtNow;
                    }
                }

                if (fRenew == true)
                {
                    //service.UserSessionExpiration(currentUserContext.UserSessionLogId, ft.Expiration);
                }

            }
        }
        #endregion

        protected void loginProcess()
        {
            // Purpose: used by Activity Logging
            RefUser refUser = new RefUser() { Username = "Trainee", RefUserId = 999, Password = "Trainee" };
            ConfinsUserContext userContext = new ConfinsUserContext()
            {
                UserId = refUser.Username,
                RoleCode = "Trainee",
                RefOfficeId = 1,
                RefUserId = refUser.RefUserId
            };

            UserSessionLog usrSessLog = new UserSessionLog();
            usrSessLog.Username = userContext.UserId;
            usrSessLog.LoginDatetime = DateTime.Now;
            usrSessLog.LogoutDatetime = null;
            userContext.UserSessionLogId = usrSessLog.UserSessionLogId;

            HttpCookie cookie = FormsAuthentication.GetAuthCookie(userContext.UserId, false);
            FormsAuthenticationTicket ft = FormsAuthentication.Decrypt(cookie.Value);
            XmlSerializer serializer = new XmlSerializer(typeof(ConfinsUserContext));
            StringBuilder builder = new StringBuilder();
            XmlWriter xmlWriter = XmlTextWriter.Create(builder);
            serializer.Serialize(xmlWriter, userContext);
            xmlWriter.Flush();
            string userData = builder.ToString();
            FormsAuthenticationTicket newFt = new FormsAuthenticationTicket(ft.Version, ft.Name, ft.IssueDate, ft.Expiration, ft.IsPersistent, userData, ft.CookiePath);
            string encryptedValue = FormsAuthentication.Encrypt(newFt);
            cookie.Value = encryptedValue;
            Response.Cookies.Add(cookie);

            usrSessLog.LoginDatetime = newFt.IssueDate;
            usrSessLog.ExpirationDatetime = newFt.Expiration;
        }
    }
}
