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
using Rule.Web.WebUserControl;
using Confins.DataModel;
using AdIns.Service.QueryService;
using AdIns.Util;
using AdIns.Service;
using AdIns.Util.Query;
//using Confins.BusinessService.Common.Security;
using System.Data;
//using Confins.BusinessService.Common;

namespace Rule.Web
{
    public abstract class WebFormBase : WebPageBase
    {
        protected HtmlTextWriter OldWriter = null;
        protected StringWriter StringWriter = new StringWriter();

        #region Language Code
        public string LanguageCode
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["LanguageCode"]; }
        }
        #endregion

        public WebFormBase()
        {
            this.Init += new EventHandler(FormInit);
            //this.LoadComplete += new EventHandler(InitFormFeatureParser);
        }

        protected void FormInit(object sender, EventArgs e)
        {
            UCLoadingPanel ucLoadingPanel = LoadControl(ResolveUrl("~/WebUserControl/UCLoadingPanel.ascx")) as UCLoadingPanel;
            Form.Controls.Add(ucLoadingPanel);
        }

        protected override void RegisterAsyncJSLoadScript()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "closeErrorList", "if(parent.CloseErrorList) parent.CloseErrorList();", true);
        }

        protected override void RegisterRequiredJSLib()
        {
            base.RegisterRequiredJSLib();
            string[] libs = new string[] 
            {
                this.ResolveUrl("~/Scripts/NCScript_Form.js"),
                this.ResolveUrl("~/Scripts/NCScript_LookUp.js")
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
                    this.ResolveUrl("~/Styles/NC_Form.css"),
                    this.ResolveUrl("~/Styles/jquery-ui-1.8.2.custom.css")
                };

            foreach (string lib in libs)
            {
                AddCSSLib(lib);
            }
        }

        protected override string GenerateErrorScript(string errorMessage)
        {
            return string.Format("parent.ShowErrorList({0});", errorMessage);
        }

        protected enum saveMessageType { save, edit, delete }
        protected void showSaveNotification(saveMessageType messageType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveNotification", "parent.ShowSaveNotification('" + messageType.ToString() + "');", true);
        }

        protected void showSaveNotification(string customMessage)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveNotification", "parent.ShowSaveNotification('" + customMessage + "');", true);
        }

        protected void showMessageBox(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showFailedNotification", "alert('" + message + "');", true);
        }

        #region Grid View
        protected void ItalicizeHeaderRow(GridView grid)
        {
            if (grid.HeaderRow != null)
            {
                foreach (Control control in grid.HeaderRow.Controls)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is Literal)
                        {
                            Literal ltl = (Literal)control2;
                            ltl.Text = ltl.Text.ToUpper();
                        }
                        else if (control2 is Label || control2 is LinkButton)
                        {
                            LinkButton lb = (LinkButton)control2;
                            lb.Text = lb.Text.ToUpper();
                        }
                    }
                }
            }
        }

        protected void ConvertYesNo(DataTable dtTable, params String[] colNames)
        {
            if (colNames != null)
            {
                foreach (String colName in colNames)
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        string check = dtTable.Rows[i][colName].ToString();
                        if (check == "0")
                            dtTable.Rows[i][colName] = "No";
                        else if (check == "1")
                            dtTable.Rows[i][colName] = "Yes";
                    }
                }
            }
        }
        #endregion

        #region Render
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    base.Render(writer);
        //}

        //protected override HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
        //{
        //    OldWriter = base.CreateHtmlTextWriter(tw);
        //    return base.CreateHtmlTextWriter(StringWriter);
        //}
        #endregion
    }
}
