using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rule.Web.WebUserControl
{
    public partial class UCInputNumber : UserControlBase
    {
        #region "PROPERTIES"

        public string CssClass
        {
            set
            {
                txtInput.CssClass = value;
            }
        }

        public string Text
        {
            get
            {
                if (hdnInput.Value != "")
                    return hdnInput.Value;
                else
                    return txtInput.Text.Replace(",", "");
            }
            set
            {
                txtInput.Text = value;
                hdnInput.Value = value;
            }
        }

        public String ValidationGroup
        {
            get
            {
                return rfvInput.ValidationGroup;
            }
            set
            {
                rfvInput.ValidationGroup = value;
                rgvInput.ValidationGroup = value;
            }
        }

        public string TextHidden
        {
            get
            {
                return hdnInput.Value;
            }
            set
            {
                hdnInput.Value = value;
            }
        }

        public string TextDummy
        {
            get
            {
                return txtInputDummy.Text;
            }
            set
            {
                txtInputDummy.Text = value;
            }
        }

        public bool IsNull
        {
            set
            {
                rfvInput.Enabled = value;
                rgvInput.Enabled = value;
                cpvInput.Enabled = value;
            }
        }

        public bool IsRequiredField
        {
            set
            {
                rfvInput.Enabled = value;
                rfvInput.InitialValue = "0.00";
                if (value == true)
                    txtInput.CssClass = "txtMandatory";
                else
                    txtInput.CssClass = "txtInput";
            }
        }

        public bool IsRange
        {
            set
            {
                rgvInput.Enabled = value;
            }
        }

        public bool IsCompare
        {
            set
            {
                cpvInput.Enabled = value;
            }
        }

        public bool IsCustom
        {
            set
            {
                cvInput.Enabled = value;
            }
        }

        public string MaxValue
        {
            get
            {
                return rgvInput.MaximumValue;
            }
            set
            {
                rgvInput.MaximumValue = value;
            }
        }

        public string MinValue
        {
            get
            {
                return rgvInput.MinimumValue;
            }
            set
            {
                rgvInput.MinimumValue = value;
            }
        }

        public int MaxLength
        {
            set
            {
                txtInput.MaxLength = value;
            }
        }

        public int InitialValue
        {
            set
            {
                rfvInput.InitialValue = value.ToString();
            }
        }

        public int Columns
        {
            set
            {
                txtInput.Columns = value;
            }
        }

        public bool NotEditable
        {
            set
            {
                txtInput.ReadOnly = value;
            }
        }

        public bool IsEnabled
        {
            set
            {
                txtInput.Enabled = value;
            }
        }

        public bool ReadOnly
        {
            set
            {
                txtInput.ReadOnly = value;
            }
        }

        //public bool IsViewPercent
        //{
        //    get
        //    {
        //        return ltlPercent.Visible;
        //    }
        //    set
        //    {
        //        ltlPercent.Visible = value;
        //    }
        //}

        public string RequiredMessage
        {
            set
            {
                rfvInput.ErrorMessage = value;
            }
        }

        public string RangeMessage
        {
            set
            {
                rgvInput.ErrorMessage = value;
            }
        }

        public string CompareMessage
        {
            set
            {
                cpvInput.ErrorMessage = value;
            }
        }

        public string CustomMessage
        {
            set
            {
                cvInput.ErrorMessage = value;
            }
        }

        public bool CalculateValue
        {
            set
            {
                if (value == true)
                {
                    txtInput.Attributes.Add("onKeyUp", "calculateValue()");
                }
            }
        }

        public bool CopyValue
        {
            set
            {
                if (value == true)
                {
                    txtInput.Attributes.Add("onKeyUp", "copyValue()");
                }
            }
        }

        public bool IsVisible
        {
            set
            {
                txtInput.Visible = value;
            }
        }

        public bool IsVisibleHiddenInput
        {
            set
            {
                txtInputDummy.Visible = value;
            }
        }

        public ValidationDataType RangeType
        {
            set
            {
                rgvInput.Type = value;
            }
        }

        public string HiddenID
        {
            get
            {
                return hdnInput.ClientID;
            }
        }

        public string RangeValidatorID
        {
            get
            {
                return rgvInput.ClientID;
            }
        }

        public string PercentID
        {
            get
            {
                return ltlPercent.ClientID;
            }
        }

        public string TextID
        {
            get
            {
                return txtInput.ClientID;
            }
        }

        public string TextDummyID
        {
            get
            {
                return txtInputDummy.ClientID;
            }
        }

        public TextBox txtInputObj
        {
            get
            {
                return txtInput;
            }
            set
            {
                txtInput = value;
            }
        }

        public bool AutoPostBack
        {
            set
            {
                txtInput.AutoPostBack = value;
            }
        }

        public string ClientValidationFunction
        {
            set { cvInput.ClientValidationFunction = value; }
        }

        public delegate void TextChangedDelegate(string compId, int index);
        public TextChangedDelegate TextChangedEvent { get; set; }

        private String controlId
        {
            get { return (String)ViewState["controlId"]; }
            set { ViewState["controlId"] = value; }
        }
        private int index
        {
            get { return (int)ViewState["index"]; }
            set { ViewState["index"] = value; }
        }

        public bool IsViewInteger
        {
            get
            {
                return ltlInteger.Visible;
            }
            set
            {
                ltlInteger.Visible = value;
            }
        }

        public bool IsViewShortPercent
        {
            get { return ltlShortPercent.Visible; }
            set
            {
                ltlShortPercent.Visible = value;
                ltlPercent.Visible = value;
            }
        }

        public bool IsViewMedPercent
        {
            get { return ltlMedPercent.Visible; }
            set
            {
                ltlMedPercent.Visible = value;
                ltlPercent.Visible = value;
            }
        }

        #endregion

        #region "PAGE LOAD"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsViewShortPercent)
            {
                IsViewMedPercent = false;
                IsViewInteger = false;
                //txtInput.Width = Unit.Point(70);
                txtInput.CssClass = "txtShortPercentageStyle";
                if (txtInput.Attributes["onblur"] == null)
                {
                    txtInput.Attributes.Add("onblur", "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
                }
                else
                {
                    txtInput.Attributes["onblur"] += "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
                }
                txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                    "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
            }
            else if (IsViewMedPercent)
            {
                IsViewShortPercent = false;
                IsViewInteger = false;
                //txtInput.Width = Unit.Point(70);
                txtInput.CssClass = "txtMedPercentageStyle";
                if (txtInput.Attributes["onblur"] == null)
                {
                    txtInput.Attributes.Add("onblur", "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
                }
                else
                {
                    txtInput.Attributes["onblur"] += "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
                }
                txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                    "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
            }
            else if (IsViewInteger)
            {
                IsViewShortPercent = false;
                IsViewMedPercent = false;
                //txtInput.Width = Unit.Point(70);
                txtInput.CssClass = "txtMedPercentageStyle";
                if (txtInput.Attributes["onblur"] == null)
                {
                    txtInput.Attributes.Add("onblur", "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
                }
                else
                {
                    txtInput.Attributes["onblur"] += "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                    "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
            }
            else
            {
                IsViewShortPercent = false;
                IsViewMedPercent = false;
                IsViewInteger = false;
                //txtInput.Width = Unit.Point(100);
                txtInput.CssClass = "txtAmountStyle";
                if (txtInput.Attributes["onblur"] == null)
                {
                    txtInput.Attributes.Add("onblur", "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
                }
                else
                {
                    txtInput.Attributes["onblur"] += "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
                }
                txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
                txtInput.Attributes.Add("onkeypress", "return validateKey(event);");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                    "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
            }
        }
        #endregion

        #region "EVENTS"
        public void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txtInput.AutoPostBack == true && TextChangedEvent != null)
            {
                TextChangedEvent(controlId, index);
            }
        }

        #endregion

        #region "OTHER METHODS"

        public void SetAmount()
        {
            IsViewShortPercent = false;
            IsViewMedPercent = false;
            IsViewInteger = false;
            //txtInput.Width = Unit.Point(100);
            txtInput.CssClass = "txtAmountStyle";
            if (txtInput.Attributes["onblur"] == null)
            {
                txtInput.Attributes.Add("onblur", "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
            }
            else
            {
                txtInput.Attributes["onblur"] += "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
            }
            txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                "formatValue('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
        }

        public void SetShortPercent()
        {
            IsViewShortPercent = true;
            IsViewMedPercent = false;
            IsViewInteger = false;
            //txtInput.Width = Unit.Point(70);
            txtInput.CssClass = "txtShortPercentageStyle";
            if (txtInput.Attributes["onblur"] == null)
            {
                txtInput.Attributes.Add("onblur", "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
            }
            else
            {
                txtInput.Attributes["onblur"] += "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
            }
            txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
        }

        public void SetMedPercent()
        {
            IsViewShortPercent = false;
            IsViewMedPercent = true;
            IsViewInteger = false;
            //txtInput.Width = Unit.Point(70);
            txtInput.CssClass = "txtMedPercentageStyle";
            if (txtInput.Attributes["onblur"] == null)
            {
                txtInput.Attributes.Add("onblur", "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
            }
            else
            {
                txtInput.Attributes["onblur"] += "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
            }
            txtInput.Attributes.Add("onfocus", "clearFormatValue('" + txtInput.ClientID + "');");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                "formatValuePercent('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
        }

        public void SetInteger()
        {
            IsViewShortPercent = false;
            IsViewMedPercent = false;
            IsViewInteger = true;
            //txtInput.Width = Unit.Point(70);
            txtInput.CssClass = "txtMedPercentageStyle";
            if (txtInput.Attributes["onblur"] == null)
            {
                txtInput.Attributes.Add("onblur", "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');");
            }
            else
            {
                txtInput.Attributes["onblur"] += "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "formatValue_" + this.ClientID,
                "formatValueInteger('" + txtInput.ClientID + "','" + hdnInput.ClientID + "','" + txtInputDummy.ClientID + "');", true);
        }

        public void SetTextBoxWidth(int width)
        {
            txtInput.Width = Unit.Point(width);
        }

        public void EnableTextChanged(string controlId, int index)
        {
            txtInput.AutoPostBack = true;
            this.controlId = controlId;
            this.index = index;
        }

        public void EnableTextChanged()
        {
            txtInput.AutoPostBack = true;
            this.controlId = "";
            this.index = 0;
        }

        public void AllowZeroValue()
        {
            rfvInput.InitialValue = "";
        }
        #endregion
    }
}