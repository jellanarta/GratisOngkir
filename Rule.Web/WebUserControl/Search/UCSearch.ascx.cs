using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Confins.WebLib.UIDataHelper;
using AdIns.Util.Query;
using AdIns.Util.TextFormatter;
using AdIns.Service;
//using Confins.Common.Exp;

namespace Rule.Web.WebUserControl.Search
{
    public partial class UCSearch : UserControlBase
    {
        #region UserControl's Path
        private const string UCINPUTNUMBERPATH = "~/WebUserControl/UCInputNumber.ascx";
        private const string UCDATEPICKERPATH = "~/WebUserControl/UCDatePicker.ascx";
        private const string UCREFERENCEPATH = "~/WebUserControl/UCReference.ascx";
        #endregion

        #region Constanta
        private string ALL = "All";
        #endregion

        #region Delegate Events Handler
        public delegate void DelegateDataBind();
        #endregion

        #region Delegate Properties
        public DelegateDataBind DataBinder;
        #endregion

        #region Properties
        public SearchControlParam ScParam
        {
            get { return _scParam; }
            set { _scParam = value; }
        }
        private SearchControlParam _scParam
        {
            get { return (SearchControlParam)ViewState["_scParam"]; }
            set { ViewState["_scParam"] = value; }
        }

        public Criteria Criteria
        {
            get { return _criteria; }
            set { _criteria = value; }
        }
        private Criteria _criteria
        {
            get { return (Criteria)ViewState["_criteria"]; }
            set { ViewState["_criteria"] = value; }
        }

        //private PlaceHolder _phFixedSearch
        //{
        //    get { return (PlaceHolder)ViewState["_phFixedSearch"]; }
        //    set { ViewState["_phFixedSearch"] = value; }
        //}

        TextFormatterHelper formatter;

        public Button BtnSearch
        {
            get { return btnSearch; }
            set { btnSearch = value; }
        }

        //public Boolean IsLoadDynamically
        //{
        //    get { return _isLoadDynamically; }
        //    set { _isLoadDynamically = value; }
        //}
        //private Boolean _isLoadDynamically
        //{
        //    get { return (Boolean)ViewState["_isLoadDynamically"]; }
        //    set { ViewState["_isLoadDynamically"] = value; }
        //}
        #endregion

        #region Override LoadViewState Event
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            createFixedSearchAppearanceTable();
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                createFixedSearchAppearanceTable();
            }

            //if (_isLoadDynamically)
            //{
            //    createFixedSearchAppearanceTable();
            //    _isLoadDynamically = false;
            //}

            formatter = UnityFactory.Resolve<TextFormatterHelper>();
        }
        #endregion

        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            _criteria = GenerateSearchCriteria();
            DataBinder();
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ResetClick();
        }
        #endregion

        #region Public Methods
        public void ResetClick()
        {
            phFixedSearch.Controls.Clear();
            createFixedSearchAppearanceTable();
        }

        public Criteria GenerateSearchCriteria()
        {
            _criteria = new Criteria();

            bool isContainAllCriteria = false;
            int index = 0;
            if (phFixedSearch.HasControls())
            {
                foreach (Control _tbl in phFixedSearch.Controls)
                {
                    foreach (Control tblRow in _tbl.Controls)
                    {
                        foreach (Control tblColl in tblRow.Controls)
                        {
                            
                            foreach (Control ctrl in tblColl.Controls)
                            {
                                if (ctrl.GetType() == typeof(TextBox))
                                {
                                    TextBox txt = (TextBox)ctrl;
                                    string[] idSplit = txt.ID.Split('_');
                                    string propertyName = idSplit[0].Substring(3, idSplit[0].Length - 3);
                                    FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);

                                    if (txt.Text.Contains("%") && fixedSearchPropSpec.PropType != typeof(Int64))
                                        _criteria.Add(Restrictions.Like(propertyName, txt.Text));
                                    else if (txt.Text != null && txt.Text != "" && !txt.Text.Contains("%"))
                                    {
                                        if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.eq)
                                            _criteria.Add(Restrictions.Eq(propertyName, txt.Text));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gt)
                                            _criteria.Add(Restrictions.Gt(propertyName, txt.Text));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gte)
                                            _criteria.Add(Restrictions.Gte(propertyName, txt.Text));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lt)
                                            _criteria.Add(Restrictions.Lt(propertyName, txt.Text));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lte)
                                            _criteria.Add(Restrictions.Lte(propertyName, txt.Text));
                                    }
                                    index++;
                                }
                                else if (ctrl.GetType() == typeof(DropDownList))
                                {
                                    DropDownList ddl = (DropDownList)ctrl;
                                    string[] idSplit = ddl.ID.Split('_');
                                    string propertyName = idSplit[0].Substring(3, idSplit[0].Length - 3);
                                    FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);

                                    if (ddl.SelectedValue == "All")
                                    {
                                        _criteria.Add(Restrictions.Like(propertyName, "%"));
                                        isContainAllCriteria = true;
                                    }
                                    else if (ddl.SelectedValue != "")
                                        _criteria.Add(Restrictions.Eq(propertyName, ddl.SelectedValue));

                                    index++;
                                }
                                else if (ctrl.GetType().ToString().ToLower().Contains("ucinputnumber"))
                                {
                                    UCInputNumber txt = (UCInputNumber)ctrl;
                                    string[] idSplit = txt.ID.Split('_');
                                    string propertyName = idSplit[0].Substring(3, idSplit[0].Length - 3);
                                    FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName  && a.index == index);
                                    
                                    if (txt.Text.Contains("%") && fixedSearchPropSpec.PropType != typeof(Int64))
                                        _criteria.Add(Restrictions.Like(propertyName, txt.Text));
                                    //else if (txt.Text != null && (txt.Text != "0" || txt.Text != "0.00") && !txt.Text.Contains("%"))
                                    else if (txt.Text != null && txt.Text != "0" && txt.Text != "0.00" && !txt.Text.Contains("%"))
                                    {
                                        if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.eq)
                                            _criteria.Add(Restrictions.Eq(propertyName, formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.Text)));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gt)
                                            _criteria.Add(Restrictions.Gt(propertyName, formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.Text)));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gte)
                                            _criteria.Add(Restrictions.Gte(propertyName, formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.Text)));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lt)
                                            _criteria.Add(Restrictions.Lt(propertyName, formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.Text)));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lte)
                                            _criteria.Add(Restrictions.Lte(propertyName, formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.Text)));
                                    }
                                    index++;
                                }
                                else if (ctrl.GetType().ToString().ToLower().Contains("ucdatepicker"))
                                {
                                    UCDatePicker txt = (UCDatePicker)ctrl;
                                    string[] idSplit = txt.ID.Split('_');
                                    string propertyName = idSplit[0].Substring(3, idSplit[0].Length - 3);
                                    FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);

                                    if (txt.Text.Contains("%"))
                                        _criteria.Add(Restrictions.Like(propertyName, txt.Text));
                                    else if (txt.Text != null && txt.Text != "")
                                    {
                                        if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.eq)
                                            _criteria.Add(Restrictions.Eq(propertyName, txt.DateValue));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gt)
                                            _criteria.Add(Restrictions.Gt(propertyName, txt.DateValue));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gte)
                                            _criteria.Add(Restrictions.Gte(propertyName, txt.DateValue));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lt)
                                            _criteria.Add(Restrictions.Lt(propertyName, txt.DateValue));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lte)
                                            _criteria.Add(Restrictions.Lte(propertyName, txt.DateValue));
                                    }
                                    index++;
                                }
                                else if (ctrl.GetType().ToString().ToLower().Contains("ucreference"))
                                {
                                    UCReference txt = (UCReference)ctrl;
                                    string[] idSplit = txt.ID.Split('_');
                                    string propertyName = idSplit[0].Substring(3, idSplit[0].Length - 3);
                                    FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);

                                    object value = new object();
                                    if (txt.SelectedValue != ALL || txt.SelectedValue != "")
                                        value = formatter.ParseFormString(fixedSearchPropSpec.PropType, txt.SelectedValue);

                                    if (txt.SelectedValue == ALL)
                                    {
                                        if (fixedSearchPropSpec.PropType != typeof(Int64))
                                            _criteria.Add(Restrictions.Like(propertyName, "%"));
                                        isContainAllCriteria = true;
                                    }
                                    else if (txt.SelectedValue != null && txt.SelectedValue != "" && txt.SelectedValue != ALL)
                                    {
                                        if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.eq)
                                            _criteria.Add(Restrictions.Eq(propertyName, value));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gt)
                                            _criteria.Add(Restrictions.Gt(propertyName, value));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.gte)
                                            _criteria.Add(Restrictions.Gte(propertyName, value));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lt)
                                            _criteria.Add(Restrictions.Lt(propertyName, value));
                                        else if (fixedSearchPropSpec.SearchCond == SearchPropSpec.SearchCondition.lte)
                                            _criteria.Add(Restrictions.Lte(propertyName, value));
                                    }
                                    index++;
                                }
                                else if (ctrl.GetType().ToString().ToLower().Contains("uclookup"))
                                {
                                    //FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);
                                    //string propertyName = txt.ID.Substring(3, txt.ID.Length - 3);
                                    //Convert.ChangeType(ctrl, 
                                    //UCLookupCust txt = (UCReference)ctrl;
                                    //string propertyName = txt.ID.Substring(3, txt.ID.Length - 3);
                                    //FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch.Find(a => a.PropName == propertyName);

                                    //if (txt.SelectedText.Contains("%"))
                                    //    _criteria.Add(Restrictions.Like(propertyName, txt.SelectedValue));
                                    //else if (txt.SelectedText != null && txt.SelectedText != "")
                                    //    _criteria.Add(Restrictions.Eq(propertyName, txt.SelectedValue));
                                }
                                
                            }
                        }
                    }
                }

                if (_criteria.CriterionList.Count == 0 && !isContainAllCriteria)
                    throw new Exception("Invalid search criteria, please fill at least one search criteria");
            }

            return _criteria;
        }
        #endregion

        #region Private Methods
        //untuk membuat table secara dinamis
        private void createFixedSearchAppearanceTable()
        {
            Table _tbl = new Table();
            _tbl.ID = "tblFixedSearch";
            _tbl.CssClass = "formTable";
            _tbl.EnableViewState = true;

            if (_scParam.FixedSearch.Count > 0)
            {
                if (_scParam.FixedSearch.Count < 4)
                {
                    for (int i = 0; i < _scParam.FixedSearch.Count; i++)
                    {
                        FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch[i];
                        TableRow tRow = new TableRow();
                        TableCell tCell = new TableCell();
                        tCell.Width = Unit.Percentage(20);
                        tCell.CssClass = "tdDesc";

                        Literal ltlDescription = new Literal();
                        //ltlDescription.ID = fixedSearchPropSpec.Text;
                        ltlDescription.Text = fixedSearchPropSpec.Text;
                        tCell.Controls.Add(ltlDescription);

                        if (fixedSearchPropSpec.IsShowSearchCondLabel)
                        {
                            Literal ltlSpace = new Literal();
                            ltlSpace.ID = "ltl_Search_Space";
                            tCell.Controls.Add(ltlSpace);

                            Literal ltlCondDescription = new Literal();
                            ltlCondDescription.ID = generateSearchCondDescriptionId(fixedSearchPropSpec.SearchCond);
                            tCell.Controls.Add(ltlCondDescription);
                        }

                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Width = Unit.Percentage(80);
                        tCell.CssClass = "tdValue";

                        generateControlByType(fixedSearchPropSpec, ref tCell, i);
                        tRow.Cells.Add(tCell);
                        _tbl.Rows.Add(tRow);
                    }
                }
                else
                {
                    TableRow tRow = new TableRow();
                    for (int i = 0; i < _scParam.FixedSearch.Count; i++)
                    {
                        FixedSearchPropSpec fixedSearchPropSpec = _scParam.FixedSearch[i];
                        TableCell tCell = new TableCell();
                        tCell.Width = Unit.Percentage(20);
                        tCell.CssClass = "tdDesc";

                        Literal ltlDescription = new Literal();
                        //ltlDescription.ID = fixedSearchPropSpec.Text;
                        ltlDescription.Text = fixedSearchPropSpec.Text;
                        tCell.Controls.Add(ltlDescription);

                        if (fixedSearchPropSpec.IsShowSearchCondLabel)
                        {
                            Literal ltlSpace = new Literal();
                            ltlSpace.ID = "ltl_Search_Space";
                            tCell.Controls.Add(ltlSpace);

                            Literal ltlCondDescription = new Literal();
                            ltlCondDescription.ID = generateSearchCondDescriptionId(fixedSearchPropSpec.SearchCond);
                            tCell.Controls.Add(ltlCondDescription);
                        }

                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Width = Unit.Percentage(30);
                        tCell.CssClass = "tdValue";

                        generateControlByType(fixedSearchPropSpec, ref tCell, i);
                        tRow.Cells.Add(tCell);

                        if (i % 2 != 0 || i == _scParam.FixedSearch.Count - 1)
                        {
                            _tbl.Rows.Add(tRow);
                            tRow = new TableRow();
                        }
                    }
                }

                phFixedSearch.Controls.Add(_tbl);
                upFixedSearch.Update();

                //UIDataHelper helper = new UIDataHelper();
                //helper.WriteMultilangText(this, Page.LanguageCode);
            }
        }

        //untuk me-generate control sesuai tipe yang didefinisikan
        private void generateControlByType(FixedSearchPropSpec fixedSearchPropSpec, ref TableCell cell, int itemIndex)
        {
            if (fixedSearchPropSpec.ValueOptionType == SearchPropSpec.ValOptType.Default)
            {
                if (fixedSearchPropSpec.PropType == typeof(String))
                {
                    TextBox txtInput = new TextBox();

                    txtInput.ID = "txt" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                    txtInput.CssClass = "txtLongInputStyle";
                    cell.Controls.Add(txtInput);

                    if (fixedSearchPropSpec.IsRequired)
                    {
                        Label lblRequiredMark = new Label();
                        lblRequiredMark.Text = "*";
                        lblRequiredMark.CssClass = "mandatoryStyle";
                        cell.Controls.Add(lblRequiredMark);

                        RequiredFieldValidator rfvString = new RequiredFieldValidator();
                        rfvString.ErrorMessage = "This field is required";
                        rfvString.ControlToValidate = txtInput.ID;
                        rfvString.InitialValue = "";
                        rfvString.Display = ValidatorDisplay.Dynamic;
                        rfvString.SetFocusOnError = true;
                        cell.Controls.Add(rfvString);
                    }
                }
                else if (fixedSearchPropSpec.PropType == typeof(Boolean))
                {
                    DropDownList ddl = new DropDownList();

                    if (fixedSearchPropSpec.AdditionalSelectionType == UCReference.AdditionalSelectionType.SelectOne || fixedSearchPropSpec.IsRequired)
                        ddl.Items.Add(new ListItem("Select One", ""));
                    else if (fixedSearchPropSpec.AdditionalSelectionType == UCReference.AdditionalSelectionType.All)
                        ddl.Items.Add(new ListItem("All", "All"));

                    ddl.Items.Add(new ListItem("True", "1"));
                    ddl.Items.Add(new ListItem("False", "0"));

                    ddl.ID = "ddl" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                    ddl.CssClass = "ddlShortStyle";
                    cell.Controls.Add(ddl);

                    if (fixedSearchPropSpec.IsRequired)
                    {
                        Label lblRequiredMark = new Label();
                        lblRequiredMark.Text = "*";
                        lblRequiredMark.CssClass = "mandatoryStyle";
                        cell.Controls.Add(lblRequiredMark);

                        RequiredFieldValidator rfvString = new RequiredFieldValidator();
                        rfvString.ID = "rfv" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                        rfvString.ErrorMessage = "This field is required";
                        rfvString.ControlToValidate = ddl.ID;
                        rfvString.InitialValue = "";
                        rfvString.Display = ValidatorDisplay.Dynamic;
                        rfvString.SetFocusOnError = true;
                        cell.Controls.Add(rfvString);
                    }

                }
                else if (fixedSearchPropSpec.PropType == typeof(Decimal) || fixedSearchPropSpec.PropType == typeof(Int16)
                    || fixedSearchPropSpec.PropType == typeof(Int32) || fixedSearchPropSpec.PropType == typeof(Int64))
                {
                    UCInputNumber ucNumber = (UCInputNumber)Page.LoadControl(Page.ResolveUrl(UCINPUTNUMBERPATH));
                    ucNumber.ID = "txt" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                    if (fixedSearchPropSpec.IsRequired) ucNumber.IsRequiredField = true;
                    if (fixedSearchPropSpec.PropType == typeof(Int32) || fixedSearchPropSpec.PropType == typeof(Int64)) ucNumber.IsViewInteger = true;
                    cell.Controls.Add(ucNumber);
                }
                else if (fixedSearchPropSpec.PropType == typeof(DateTime))
                {
                    UCDatePicker ucDatePicker = (UCDatePicker)Page.LoadControl(Page.ResolveUrl(UCDATEPICKERPATH));
                    ucDatePicker.ID = "txt" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                    if (fixedSearchPropSpec.IsRequired) ucDatePicker.Required = true;
                    cell.Controls.Add(ucDatePicker);
                }
            }
            else if (fixedSearchPropSpec.ValueOptionType == SearchPropSpec.ValOptType.Reference)
            {
                UCReference ucReference = (UCReference)Page.LoadControl(Page.ResolveUrl(UCREFERENCEPATH));
                ucReference.ID = "ddl" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                ucReference.BindingObject(fixedSearchPropSpec.Reference_DataSource, fixedSearchPropSpec.Reference_DataTextField,
                        fixedSearchPropSpec.Reference_DataValueField, fixedSearchPropSpec.IsRequired, fixedSearchPropSpec.AdditionalSelectionType);
                cell.Controls.Add(ucReference);
            }
            else if (fixedSearchPropSpec.ValueOptionType == SearchPropSpec.ValOptType.Lookup)
            {
                UserControl ucLookup = (UserControl)Page.LoadControl(Page.ResolveUrl(fixedSearchPropSpec.LookupPath));
                ucLookup.ID = "ucl" + fixedSearchPropSpec.PropName + "_" + itemIndex;
                cell.Controls.Add(ucLookup);
            }
        }

        //untuk me-generate label yang memberi keterangan 
        private string generateSearchCondDescriptionId(SearchPropSpec.SearchCondition condition)
        {
            string id = string.Empty;

            if (condition == SearchPropSpec.SearchCondition.eq)
                id = "ltl_Search_EqualText";
            else if (condition == SearchPropSpec.SearchCondition.gt)
                id = "ltl_Search_GreaterThanText";
            else if (condition == SearchPropSpec.SearchCondition.gte)
                id = "ltl_Search_GreaterThanEqualText";
            else if (condition == SearchPropSpec.SearchCondition.lt)
                id = "ltl_Search_LowerThanText";
            else if (condition == SearchPropSpec.SearchCondition.lte)
                id = "ltl_Search_LowerThanEqualText";

            return id;
        }
        #endregion
    }
}