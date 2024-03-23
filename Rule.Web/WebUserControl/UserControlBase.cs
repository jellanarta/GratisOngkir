using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Confins.WebLib;

namespace Rule.Web.WebUserControl
{
    public class UserControlBase : System.Web.UI.UserControl
    {
        public UserControlBase()
        {
            this.Load += new EventHandler(DefaultCustomUCLoad);
        }

        public new WebPageBase Page
        {
            get
            {
                return base.Page as WebPageBase;
            }
        }

        private void DefaultCustomUCLoad(object sender, EventArgs e)
        {
            if (Page.IsAsyncPostback)
            {
                RegisterAsyncJSLoadScript();
            }
            else
            {
                RegisterRequiredCSSLib();
                RegisterRequiredJSLib();
                
            }
        }

        protected virtual void RegisterRequiredJSLib()
        {
            string[] libs = new string[]
            {
                //this.ResolveUrl("~/Scripts/NCScript_DatePicker.js")
            };

            foreach (string lib in libs)
            {
                Page.ClientScript.RegisterClientScriptInclude(lib, lib);
            }
        }

        protected virtual void RegisterRequiredCSSLib()
        {
            string[] libs = new string[] 
                {
                    //this.ResolveUrl("~/Styles/NC_Form.css")
                };

            foreach (string lib in libs)
            {
                Page.ClientScript.RegisterClientScriptInclude(lib, lib);
            }
        }

        protected virtual void RegisterAsyncJSLoadScript()
        {
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
        #endregion
    }
}