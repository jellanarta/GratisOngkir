using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AdIns.Util.Query;
using AdIns.Util.TextFormatter;
using AdIns.Service;
//using Confins.Common.Exp;


namespace Rule.Web.WebUserControl.Search
{
    public partial class UCSearchSimple : System.Web.UI.UserControl
    {
        #region "PROPERTIES"
        //public string Title
        //{
        //    get { return lblTitle.Text; }
        //    set { lblTitle.Text = value; }
        //}
        public string WidthSearchBy { get; set; }
        public string WidthTxtInput { get; set; }
        public SearchControlParam ScParam
        {
            get { return (SearchControlParam)ViewState["scParam"]; }
            set { ViewState["scParam"] = value; }
        }
        public List<ListItem> listOfCategory
        {
            get { return (List<ListItem>)ViewState["listOfCategory"]; }
            set { ViewState["listOfCategory"] = value; }
        }
        private DataTable dtSearch
        {
            get { return (DataTable)ViewState["dtSearch"]; }
            set { ViewState["dtSearch"] = value; }
        }
        public Criteria criteria
        {
            get { return (Criteria)ViewState["criteria"]; }
            set { ViewState["criteria"] = value; }
        }
        public LinkButton LbSearch
        {
            get { return lbSearch; }
        }
        public delegate void DelegateDataBind();
        public DelegateDataBind DataBinder;
        TextFormatterHelper formatter;

        private static readonly string SHOW_ALL = "ShowAll";
        #endregion

        public string CheckPageLoad
        {
            get { return (string)ViewState["CheckPageLoad"]; }
            set { ViewState["CheckPageLoad"] = value; }
        }

        #region "PAGE LOAD"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPageLoad = "1";
                initValue();
            }

            if (CheckPageLoad == null)
            {
                CheckPageLoad = "1";
                initValue();
            }
            formatter = UnityFactory.Resolve<TextFormatterHelper>();
            lbSearch.ValidationGroup = lbSearch.ClientID + "VldtnGrp";
        }
        #endregion

        #region "FIXED SEARCH"
        protected void rptFixedSearch_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                #region "LOCAL VARIABLE"
                Label lblText = (Label)e.Item.FindControl("lblText");
                //CheckBox chkBool = (CheckBox)e.Item.FindControl("chkBool");
                DropDownList ddlBool = (DropDownList)e.Item.FindControl("ddlBool");
                DropDownList ddlSearchCond = ((DropDownList)e.Item.FindControl("ddlSearchCond"));
                TextBox txtSearchValue = (TextBox)e.Item.FindControl("txtSearchValue");
                UCDatePicker ucDatePicker = (UCDatePicker)e.Item.FindControl("ucDatePicker");
                FixedSearchPropSpec Collection = ScParam.FixedSearch.Find(d => d.Text == lblText.Text);
                RequiredFieldValidator rfvInput = (RequiredFieldValidator)e.Item.FindControl("rfvInput");
                RangeValidator rvNumber = (RangeValidator)e.Item.FindControl("rvNumber");
                UCReference ucReference = ((UCReference)e.Item.FindControl("ucReference"));
                Label lblDescription = (Label)e.Item.FindControl("lblDescription");
                Type propType = Collection.PropType;
                #endregion

                rfvInput.ValidationGroup = lbSearch.ClientID + "VldtnGrp";
                rvNumber.ValidationGroup = lbSearch.ClientID + "VldtnGrp";

                #region "CONFIGURE REQUIRED FIELD VALIDATOR"
                if (Collection.IsRequired)
                {
                    if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Default)
                    {
                        if (typeof(String).IsAssignableFrom(propType)
                            || typeof(Int32).IsAssignableFrom(propType)
                            || typeof(Int64).IsAssignableFrom(propType)
                            || typeof(Decimal).IsAssignableFrom(propType))
                        {
                            setEnabledVisible(rfvInput, true);
                            //rfvInput.ControlToValidate = txtSearchValue.ID;
                            //setEnabledVisible(rvNumber, false);
                        }
                        else if (typeof(DateTime).IsAssignableFrom(propType))
                        {
                            ucDatePicker.Required = true;
                            setEnabledVisible(rfvInput, false);
                            //setEnabledVisible(rvNumber, false);
                        }
                        else if (typeof(Boolean).IsAssignableFrom(propType))
                        {
                            setEnabledVisible(rfvInput, false);
                            //setEnabledVisible(rfvInput, false);
                            //setEnabledVisible(rvNumber, false);
                        }
                    }
                }
                else if (!Collection.IsRequired)
                {
                    if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Default)
                    {
                        if (typeof(String).IsAssignableFrom(propType)
                            || typeof(Int32).IsAssignableFrom(propType)
                            || typeof(Int64).IsAssignableFrom(propType)
                            || typeof(Decimal).IsAssignableFrom(propType))
                        {
                            setEnabledVisible(rfvInput, false);
                            //rfvInput.ControlToValidate = txtSearchValue.ID;
                            //setEnabledVisible(rvNumber, false);
                        }
                        else if (typeof(DateTime).IsAssignableFrom(propType))
                        {
                            ucDatePicker.Required = false;
                            setEnabledVisible(rfvInput, false);
                            //setEnabledVisible(rvNumber, false);
                        }
                        else if (typeof(Boolean).IsAssignableFrom(propType))
                        {
                            setEnabledVisible(rfvInput, false);
                            //setEnabledVisible(rvNumber, false);
                        }
                    }
                }
                #endregion

                #region "CONFIGURE SEARCH CONDITION"
                ddlSearchCond.DataSource = bindSearchCond(propType);
                ddlSearchCond.DataTextField = "Text";
                ddlSearchCond.DataValueField = "Value";
                ddlSearchCond.DataBind();
                #endregion

                #region "CONFIGURE APPEARANCE"
                if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Default)
                {
                    if (typeof(String).IsAssignableFrom(propType))
                    {
                        txtSearchValue.Visible = true;
                        ddlBool.Visible = false;
                        ucDatePicker.Visible = false;
                        setEnabledVisible(rvNumber, false);
                    }
                    else if (typeof(Boolean).IsAssignableFrom(propType))
                    {
                        txtSearchValue.Visible = false;
                        ddlBool.Visible = true;
                        ucDatePicker.Visible = false;
                        setEnabledVisible(rvNumber, false);
                    }
                    else if (typeof(DateTime).IsAssignableFrom(propType))
                    {
                        ucDatePicker.Visible = true;
                        txtSearchValue.Visible = false;
                        ddlBool.Visible = false;
                        setEnabledVisible(rvNumber, false);
                    }
                    else if (typeof(Int32).IsAssignableFrom(propType) || typeof(Int64).IsAssignableFrom(propType))
                    {
                        txtSearchValue.Visible = true;
                        ddlBool.Visible = false;
                        ucDatePicker.Visible = false;
                        setEnabledVisible(rvNumber, true);
                        rvNumber.Type = ValidationDataType.Integer;
                    }
                    else if (typeof(Decimal).IsAssignableFrom(propType))
                    {
                        txtSearchValue.Visible = true;
                        ddlBool.Visible = false;
                        ucDatePicker.Visible = false;
                        setEnabledVisible(rvNumber, true);
                        rvNumber.Type = ValidationDataType.Double;
                    }
                }
                else if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Reference)
                {
                    ucReference.BindingObject(Collection.Reference_DataSource, Collection.Reference_DataTextField,
                        Collection.Reference_DataValueField, Collection.IsRequired);

                    lblDescription.Visible = false;
                    ucReference.Visible = true;
                    ucDatePicker.Visible = false;
                    txtSearchValue.Visible = false;
                    ddlBool.Visible = false;
                    setEnabledVisible(rvNumber, false);
                }
                else if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Description)
                {
                    lblDescription.Text = Collection.PropName;

                    lblDescription.Visible = true;
                    ucReference.Visible = false;
                    ucDatePicker.Visible = false;
                    txtSearchValue.Visible = false;
                    ddlBool.Visible = false;
                    setEnabledVisible(rvNumber, false);
                }

                ddlSearchCond.SelectedValue = ScParam.FixedSearch[e.Item.ItemIndex].SearchCond.ToString();
                #endregion
            }
        }
        #endregion

        #region "BUTTON EVENTS"

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            criteria = new Criteria();
            foreach (RepeaterItem rItem in rptFixedSearch.Items)
            {
                #region "LOCAL PROPERTIES"
                Label lblText = (Label)rItem.FindControl("lblText");
                Label lblDescription = (Label)rItem.FindControl("lblDescription");
                FixedSearchPropSpec Collection = ScParam.FixedSearch.Find(d => d.Text == lblText.Text);
                UCReference ucReference = ((UCReference)rItem.FindControl("ucReference"));
                string searchCond = ((DropDownList)rItem.FindControl("ddlSearchCond")).SelectedValue;
                string propName = Collection.PropName;
                object value = new object();
                Type propType = Collection.PropType;
                #endregion

                if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Default)
                {

                    if (typeof(String).IsAssignableFrom(propType))
                    {
                        value = ((TextBox)rItem.FindControl("txtSearchValue")).Text;
                    }
                    else if (typeof(DateTime).IsAssignableFrom(propType))
                    {
                        UCDatePicker ucDatePicker = ((UCDatePicker)rItem.FindControl("ucDatePicker"));
                        if (ucDatePicker.DateValue != DateTime.MinValue)
                            value = ucDatePicker.DateValue;
                        else
                            value = "";
                    }
                    else if (typeof(Int16).IsAssignableFrom(propType) || typeof(Int32).IsAssignableFrom(propType)
                        || typeof(Int64).IsAssignableFrom(propType))
                    {
                        value = formatter.ParseFormString(propType, ((TextBox)rItem.FindControl("txtSearchValue")).Text);
                    }
                    else if (typeof(Boolean).IsAssignableFrom(propType))
                    {
                        if (((DropDownList)rItem.FindControl("ddlBool")).SelectedValue != "All")
                        {
                            value = ((DropDownList)rItem.FindControl("ddlBool")).SelectedValue;
                        }
                        else
                        {
                            value = "";
                        }

                    }
                }
                else if (Collection.ValueOptionType == SearchPropSpec.ValOptType.Reference)
                {
                    if (typeof(Int16).IsAssignableFrom(propType) || typeof(Int32).IsAssignableFrom(propType)
                        || typeof(Int64).IsAssignableFrom(propType))
                    {
                        if (ucReference.SelectedValue != SHOW_ALL)
                            value = formatter.ParseFormString(propType, ucReference.SelectedValue);
                        else
                            value = "";
                    }
                    else if (typeof(String).IsAssignableFrom(propType))
                    {
                        if (ucReference.SelectedValue != SHOW_ALL)
                            value = ucReference.SelectedValue;
                        else
                            value = "";
                    }
                }

                if (value != "" && value != null)
                {
                    if (searchCond == "eq")
                    {
                        if (typeof(String).IsAssignableFrom(propType))
                        {
                            string val = value.ToString();
                            if (val.Contains("%"))
                                criteria.Add(Restrictions.Like(propName, val));
                            else
                                criteria.Add(Restrictions.Eq(propName, value));
                        }
                        else
                        {
                            criteria.Add(Restrictions.Eq(propName, value));
                        }
                    }
                    else if (searchCond == "gt")
                        criteria.Add(Restrictions.Gt(propName, value));
                    else if (searchCond == "lt")
                        criteria.Add(Restrictions.Lt(propName, value));
                    else if (searchCond == "gte")
                        criteria.Add(Restrictions.Gte(propName, value));
                    else if (searchCond == "lte")
                        criteria.Add(Restrictions.Lte(propName, value));
                }
            }

            if (criteria.CriterionList.Count == 0)
                throw new Exception("Invalid search criteria, please fill at least one search criteria");

            DataBinder();
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ResetClick();
        }
        #endregion

        #region "METHOD"
        public void ResetClick()
        {
            initValue();
            upFixedSearch.Update();
        }

        private void initValue()
        {
            if (ScParam.FixedSearch.Count > 0)
            {
                rptFixedSearch.DataSource = ScParam.FixedSearch;
                rptFixedSearch.DataBind();
            }
        }

        private List<ListItem> bindSearchCond(Type type)
        {
            List<ListItem> listOfCondition = new List<ListItem>();
            if (typeof(Int16).IsAssignableFrom(type) || typeof(Int32).IsAssignableFrom(type) || typeof(Int64).IsAssignableFrom(type) || typeof(DateTime).IsAssignableFrom(type))
            {
                listOfCondition = new List<ListItem>(
                            new ListItem[] { 
                                new ListItem("=","eq"),
                                new ListItem(">","gt"),
                                new ListItem("<","lt"),
                                new ListItem(">=","gte"),
                                new ListItem("<=","lte")
                            });
            }
            else if (typeof(Boolean).IsAssignableFrom(type) || typeof(String).IsAssignableFrom(type))
            {
                listOfCondition = new List<ListItem>(
                            new ListItem[] { 
                                new ListItem("=","eq")
                            });
            }
            return listOfCondition;
        }

        private void initDtSearchColumns()
        {
            dtSearch.Columns.Add("Relationship");
            dtSearch.Columns.Add("SearchBy");
            dtSearch.Columns.Add("SearchCond");
            dtSearch.Columns.Add("SearchValue");
        }

        private void setEnabledVisible(WebControl obj, bool availability)
        {
            obj.Visible = availability;
            obj.Enabled = availability;
        }

        private void checkPercentage(string value)
        {

        }
        #endregion
    }
}