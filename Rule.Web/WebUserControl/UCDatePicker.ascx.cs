using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Confins.WebLib;

namespace Rule.Web.WebUserControl
{
    [ValidationProperty("Text")]
    public partial class UCDatePicker : UserControlBase
    {
        #region "PROPERTIES"
        public DateTime DateValue
        {
            get
            {
                DateTime dTime;
                if (txtDatePicker.Text == "")
                    dTime = DateTime.MinValue;
                else
                    dTime = GetDateOnENFormat();
                return dTime;
            }
            set { txtDatePicker.Text = value.ToString(); }
        }

        public string Text
        {
            get { return txtDatePicker.Text; }
            set
            {
                //if (value.Contains('/'))
                //{
                //    string[] Arr = value.Split('/');
                //    value = Arr[1] + "/" + Arr[0] + "/" + Arr[2].Split(' ').FirstOrDefault();
                //}
                txtDatePicker.Text = value;
            }
        }

        public bool Required
        {
            get { return rfvDatePicker.Enabled; }
            set { rfvDatePicker.Enabled = value; }
        }

        public string ValidationGroup
        {
            set
            {
                rfvDatePicker.ValidationGroup = value;
                rvDate.ValidationGroup = value;
            }
        }

        public string SetMinValue
        {
            get { return rvDate.MinimumValue; }
            set
            {
                if (value.Length > 0) rvDate.MinimumValue = value;
                else rvDate.MinimumValue = DateTime.MinValue.ToShortDateString();
            }
        }

        public string SetMaxValue
        {
            get { return rvDate.MaximumValue; }
            set
            {
                if (value.Length > 0) rvDate.MaximumValue = value;
                else rvDate.MaximumValue = DateTime.MaxValue.ToShortDateString();
            }
        }

        public bool Enabled
        {
            get { return txtDatePicker.Enabled; }
            set { txtDatePicker.Enabled = value; }
        }

        public string RangeMessage
        {
            set { rvDate.ErrorMessage = value; }
        }
        #endregion

        #region "PAGE LOAD"
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(UserControl), "ucDatePicker" + this.ClientID, "$('#" + txtDatePicker.ClientID + @"').datepicker({
                                                                                                                    changeMonth: true,
                                                                                                                    changeYear: true,
                                                                                                                    dateFormat: 'dd/mm/yy',
                                                                                                                    yearRange: '-70:+70'
                                                                                                                });", true);

            if (!IsPostBack)
            {
                checkPageLoad = "1";
                if (rvDate.MinimumValue == "") rvDate.MinimumValue = DateTime.MinValue.ToShortDateString();
                if (rvDate.MaximumValue == "") rvDate.MaximumValue = DateTime.MaxValue.ToShortDateString();
            }
            if (checkPageLoad == null)
            {
                checkPageLoad = "1";
                if (rvDate.MinimumValue == "") rvDate.MinimumValue = DateTime.MinValue.ToShortDateString();
                if (rvDate.MaximumValue == "") rvDate.MaximumValue = DateTime.MaxValue.ToShortDateString();
            }

            txtDatePicker.Attributes.Add("onchange", "copy('" + txtDatePicker.ClientID + "','" + txtHidden.ClientID + "', '" + rvDate.ClientID + "')");
        }
        #endregion

        #region "METHODS"
        protected DateTime GetDateOnENFormat()
        {
            DateTime DtForm;
            string[] Arr = txtDatePicker.Text.Split('/');
            DtForm = DateTime.Parse(Arr[2] + "/" + Arr[1] + "/" + Arr[0]);
            return DtForm;
        }

        private string checkPageLoad
        {
            get { return (string)ViewState["CheckPageLoad"]; }
            set { ViewState["CheckPageLoad"] = value; }
        }

        protected override void RegisterAsyncJSLoadScript()
        {
            base.RegisterAsyncJSLoadScript();
        }

        protected override void RegisterRequiredJSLib()
        {
            base.RegisterRequiredJSLib();
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "ucDatePickerJS", this.ResolveUrl("~/Scripts/jquery-ui-1.8.2.custom.min.js"));
        }

        public void Clear()
        {
            txtDatePicker.Text = "";
        }
        #endregion
    }
}