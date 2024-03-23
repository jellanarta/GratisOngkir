using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Confins.Common.Exp;
using System.Configuration;
using Microsoft.Practices.Unity;
using System.Data;
using AdIns.Service.QueryService;
using AdIns.Util;
using Confins.Common;
using AdIns.Util.Query;
using System.Text;
using Rule.Web.WebUserControl.Search;
using System.Web.UI.HtmlControls;
using AdIns.Service;

namespace Rule.Web.WebUserControl.GenericLookup
{
    public partial class LookupBase : WebUserControl.UserControlBase
    {
        private const string CUSTOM_SCRIPT = "customScript";
        private const string CUSTOM_SCRIPT_TITLE = "SELECT";

        public LookupBase()
        {
            OnItemSelectAdditionalScripts = new List<OnItemSelectAdditionalScript>();
            _results = new Dictionary<string, Control>();
            MinTypedLetters = 0;
        }

        public string CheckPageLoad
        {
            get { return (string)ViewState["CheckPageLoad"]; }
            set { ViewState["CheckPageLoad"] = value; }
        }

        private int PageSize
        {
            get { return ucGridFooter.PageSize; }
            set { ucGridFooter.PageSize = value; }
        }
        private bool IsCount
        {
            get { return ucGridFooter.IsCount; }
            set { ucGridFooter.IsCount = value; }
        }
        public int TotalRows
        {
            get { return (int)ViewState["TotalRows"]; }
            set { ViewState["TotalRows"] = value; }
        }

        private UCGridFooter ucGridFooter
        {
            get
            {
                return (UCGridFooter)umd.FindControl("ucGF");
            }
        }
        //private UCGridHeader ucGridHeader
        //{
        //    get
        //    {
        //        return (UCGridHeader)umd.FindControl("ucGH");
        //    }
        //}

        private GridView gvList
        {
            get
            {
                return umd.FindControl("gvL") as GridView;
            }
        }
        private UpdatePanel upGrid
        {
            get
            {
                return umd.FindControl("upG") as UpdatePanel;
            }
        }
        public UCSearchSimple UcSearch
        {
            get
            {
                return umd.FindControl("ucS") as UCSearchSimple;
            }
        }
        public Panel pnlGrid
        {
            get
            {
                return umd.FindControl("pnlGrid") as Panel;
            }
        }
        private HiddenField hdnSort
        {
            get
            {
                return umd.FindControl("hdnSort") as HiddenField;
            }
        }
        public Criteria AdditionalCriteria
        {
            get { return (Criteria)ViewState["AdditionalCriteria"]; }
            set { ViewState["AdditionalCriteria"] = value; }
        }

        public bool IsRequiredField
        {
            get { return rfv.Enabled; }
            set { rfv.Enabled = value; }
        }
        public string ValidationGroup
        {
            get { return rfv.ValidationGroup; }
            set { rfv.ValidationGroup = value; }
        }
        public int MaxLength
        {
            get { return txt.MaxLength; }
            set { txt.MaxLength = value; }
        }
        private string orderByQry
        {
            get { return (string)ViewState["orderByQry"]; }
            set { ViewState["orderByQry"] = value; }
        }
        private bool orderByAscQry
        {
            get { return (bool)ViewState["orderByAscQry"]; }
            set { ViewState["orderByAscQry"] = value; }
        }

        private bool _isRequired;
        public bool IsRequired
        {
            set
            {
                _isRequired = value;
            }
            private get
            {
                return _isRequired;
            }
        }
        public string ValidaterMember { set; private get; }
        public string ValidationMessage { set; private get; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _data = UnityFactory.Resolve<GenericLookupData>(MapperName);
            int keyIdCtr = _data.ColumnBinders.Count(a => a.IsKeyId && a.ModalDialogVisibility);
            if (keyIdCtr < 1 || keyIdCtr > 1)
                throw new Exception("Key Id total must be 1 and must be visible.");
                //throw new ConfinsException(ExceptionType.ExOther, "Key Id total must be 1 and must be visible.", null);

            int i = 0;
            foreach (var cbinder in _data.ColumnBinders)
            {
                HiddenField hdnField = new HiddenField();
                hdnField.ID = string.Format("hd{0}", ++i);
                plc.Controls.Add(hdnField);

                _results.Add(cbinder.ColumnName, hdnField);

                if (cbinder.IsKeyId)
                    _results.Add(cbinder.ColumnName + "AsKeyId", txt);
            }

            //ucToggleGrid.affectedID = upGrid.ClientID;
            //ucToggleGrid.subSectionTitle = "Grid - " + LookupTitle;

            ucGridFooter.DataBinder += new UCGridFooter.DelegateDataBind(ucGridFooter_Click);
            //ucGridHeader.DataBinder += new UCGridHeader.DelegateDataBind(ucGridHeader_Click);
            UcSearch.DataBinder += new UCSearchSimple.DelegateDataBind(search_Click);
        }

        public virtual string MapperName { get; set; }

        public virtual string QueryServiceName { get; set; }

        public virtual int MinTypedLetters { get; set; }

        public virtual string LookupTitle { get; set; }

        protected string ModalDialogName
        {
            get
            {
                return "mdn" + this.ClientID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPageLoad = "1";
                txt.Attributes.Add("onKeyUp", string.Format("keyIdUp(event,this,'{0}','{1}','{2}','{3}');",
                    li.ClientID, lt.ClientID, hdn.ClientID, MinTypedLetters));
                lt.Attributes.Add("onKeyDown", string.Format("return searchDown(event,this,'{0}','{1}');", txt.ClientID, li.ClientID));

                lt.Attributes.Add("onKeyUp", string.Format("return searchUp(event,this,'{0}','{1}','{2}');",
                    txt.ClientID, li.ClientID, ModalDialogName));
                lt.Attributes.Add("onClick", string.Format("return searchSelect(this,'{0}','{1}','{2}');",
                    txt.ClientID, li.ClientID, ModalDialogName));

                //UcSearch.Title = LookupTitle;

                ucGridFooter.Initialize();

                imb.OnClientClick = string.Format("overlay('{0}')", ModalDialogName);

                validationHandling();
            }

            if (CheckPageLoad == null)
            {
                CheckPageLoad = "1";
                txt.Attributes.Add("onKeyUp", string.Format("keyIdUp(event,this,'{0}','{1}','{2}','{3}');",
                    li.ClientID, lt.ClientID, hdn.ClientID, MinTypedLetters));
                lt.Attributes.Add("onKeyDown", string.Format("return searchDown(event,this,'{0}','{1}');", txt.ClientID, li.ClientID));

                lt.Attributes.Add("onKeyUp", string.Format("return searchUp(event,this,'{0}','{1}','{2}');",
                    txt.ClientID, li.ClientID, ModalDialogName));
                lt.Attributes.Add("onClick", string.Format("return searchSelect(this,'{0}','{1}','{2}');",
                    txt.ClientID, li.ClientID, ModalDialogName));

                //UcSearch.Title = LookupTitle;

                ucGridFooter.Initialize();

                imb.OnClientClick = string.Format("overlay('{0}')", ModalDialogName);

                validationHandling();

            }
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "GenericLookupScript",
                ResolveUrl(ConfigurationManager.AppSettings[GenericLookupData.SCRIPT_VIRTUAL_PATH]));

            umd.ModalDialogName = ModalDialogName;
        }

        protected void hdn_ValueChanged(object sender, EventArgs e)
        {
            txtKeyId_TextChanged(sender, e);
            hdn.Value = "";
        }

        public class OnItemSelectAdditionalScript
        {
            public const string COLUMN_REPLACEMENT_MARKER_TEMP = "[{0}]";

            public OnItemSelectAdditionalScript(string functionName, FunctionParameter[] parameters)
            {
                this.FunctionName = functionName;
                this.Parameters = parameters;
            }

            public string FunctionName { get; private set; }

            public FunctionParameter[] Parameters { get; private set; }

            public class FunctionParameter
            {
                public FunctionParameter(string value, ParamType type)
                {
                    this.Value = value;
                    this.Type = type;
                }

                public string Value { get; private set; }

                public ParamType Type { get; private set; }

                public enum ParamType
                {
                    Constant,
                    ColumnName
                }
            }
        }

        private GenericLookupData _data { get; set; }

        private string _keyColumn;
        private string keyColumn
        {
            get
            {
                if (_keyColumn == null)
                    _keyColumn = _data.ColumnBinders.FirstOrDefault(a => a.IsKeyId).ColumnName;
                return _keyColumn;
            }
        }

        public List<OnItemSelectAdditionalScript> OnItemSelectAdditionalScripts { get; set; }

        private Dictionary<string, Control> _results;

        public string this[string keyName]
        {
            get
            {
                return (_results[keyName] as HiddenField).Value;
            }
            set
            {
                (_results[keyName] as HiddenField).Value = value;
            }
        }

        public string Text
        {
            get
            {
                return this[keyColumn];
            }
            set
            {
                this[keyColumn] = value;
                txt.Text = value;
            }
        }

        private void validationHandling()
        {
            if (IsRequired)
            {
                lbl.Visible = true;
                string validationFunction = string.Format(" vgc('{0}', '{1}', '{2}') ",
                    _results[keyColumn].ClientID, lbl.ClientID, ValidationMessage);
                foreach (string controlID in ValidaterMember.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Control current = Parent.FindControl(controlID);
                    if (current is Button)
                    {
                        Button btn = current as Button;
                        if (btn.OnClientClick == "") btn.OnClientClick = "";
                        else btn.OnClientClick += "&&";
                        btn.OnClientClick += validationFunction;
                    }
                    else if (current is ImageButton)
                    {
                        ImageButton btn = current as ImageButton;
                        if (btn.OnClientClick == "") btn.OnClientClick = "";
                        else btn.OnClientClick += "&&";
                        btn.OnClientClick += validationFunction;
                    }
                    else if (current is LinkButton)
                    {
                        LinkButton btn = current as LinkButton;
                        if (btn.OnClientClick == "") btn.OnClientClick = "";
                        else btn.OnClientClick += "&&";
                        btn.OnClientClick += validationFunction;
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (IsRequired)
            {
                foreach (string controlID in ValidaterMember.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Control current = Parent.FindControl(controlID);
                    if (current is Button)
                    {
                        Button btn = current as Button;
                        if (!btn.OnClientClick.Contains("return ( vgc(")) btn.OnClientClick = string.Format("return ({0});", btn.OnClientClick);
                    }
                    else if (current is ImageButton)
                    {
                        ImageButton btn = current as ImageButton;
                        if (!btn.OnClientClick.Contains("return ( vgc(")) btn.OnClientClick = string.Format("return ({0});", btn.OnClientClick);
                    }
                    else if (current is LinkButton)
                    {
                        LinkButton btn = current as LinkButton;
                        if (!btn.OnClientClick.Contains("return ( vgc(")) btn.OnClientClick = string.Format("return ({0});", btn.OnClientClick);
                    }
                }
            }
        }

        #region SHOW DATA
        private DataTable getData(bool isAutoComplete, int currentPage = 1, bool includeData = true)
        {
            QueryService qryOrgSvc = UnityFactory.Resolve<QueryService>();
            PagingResult pagingResult = new PagingResult();
            PagingSpec pagingSpec = new PagingSpec();
            QueryParameter parameter = new QueryParameter();

            pagingSpec.IncludeCount = IsCount;
            pagingSpec.IncludeData = includeData;
            pagingSpec.PageNo = currentPage;

            if (PageSize == null)
            {
                PageSize = 10;
            }

            if (isAutoComplete)
            {
                pagingSpec.IncludeCount = false;
                pagingSpec.RowPerPage = PageSize;

                Criteria criteria = new Criteria();

                criteria.Add(Restrictions.Like(keyColumn, txt.Text + "%"));
                parameter.Criteria = criteria;
            }
            else
            {
                pagingSpec.IncludeCount = IsCount;
                pagingSpec.RowPerPage = PageSize;
                parameter.Criteria = UcSearch.criteria;
            }

            #region ADDITIONAL CRITERIA
            if (AdditionalCriteria != null)
                if (AdditionalCriteria.CriterionList != null)
                    parameter.Criteria.CriterionList.AddRange(AdditionalCriteria.CriterionList);
            #endregion
            if (orderByQry == null)
            {
                orderByQry = string.Format("it.{0}", keyColumn);
                orderByAscQry = true;
            }
            parameter.AddOrderBy(orderByQry, orderByAscQry);

            pagingResult = qryOrgSvc.QueryPaging(QueryServiceName, pagingSpec, parameter);

            if (IsCount)
                TotalRows = pagingResult.Count;

            #region REORDER COLUMNS
            DataTable dt = new DataTable();
            foreach (string colName in _data.ColumnBinders.Select(a => a.ColumnName))
                dt.Columns.Add(colName);

            DataRow drNew;
            foreach (DataRow drOld in pagingResult.Data.Rows)
            {
                drNew = dt.NewRow();
                foreach (DataColumn dcNew in dt.Columns)
                    drNew[dcNew.ColumnName] = drOld[dcNew.ColumnName];
                dt.Rows.Add(drNew);
            }
            #endregion

            #region GENERATE SCRIPT
            dt.Columns.Add(CUSTOM_SCRIPT);

            foreach (DataRow drow in dt.Rows)
                drow[CUSTOM_SCRIPT] = generateScript(drow);
            #endregion

            #region FINAL CLEAN UP
            foreach (ColumnBinder colBinder in _data.ColumnBinders)
            {
                if (!colBinder.ModalDialogVisibility) dt.Columns.Remove(colBinder.ColumnName);
                else dt.Columns[colBinder.ColumnName].ColumnName = colBinder.Title;
            }
            dt.Columns[CUSTOM_SCRIPT].ColumnName = CUSTOM_SCRIPT_TITLE;
            #endregion

            return dt;
        }

        private string generateScript(DataRow drow)
        {
            string scriptTemplate = MapperName + "({0});{1}";
            StringBuilder sb = new StringBuilder();
            string additionalScriptTemplate = formedAdditionalScript;
            foreach (ColumnBinder colBinder in _data.ColumnBinders)
            {
                if (colBinder.IsKeyId) { sb.Append("'"); sb.Append(txt.ClientID); sb.Append("',"); }
                sb.Append("'"); sb.Append(_results[colBinder.ColumnName].ClientID); sb.Append("',");
                sb.Append("'"); sb.Append(drow[colBinder.ColumnName].ToString()); sb.Append("',");

                additionalScriptTemplate = additionalScriptTemplate.Replace(
                    string.Format(OnItemSelectAdditionalScript.COLUMN_REPLACEMENT_MARKER_TEMP, colBinder.ColumnName),
                    drow[colBinder.ColumnName].ToString());
            }
            sb.Remove(sb.Length - 1, 1);

            return string.Format(scriptTemplate, sb.ToString(), additionalScriptTemplate);
        }

        private string formAdditionalScriptFunction(OnItemSelectAdditionalScript obj)
        {
            string scriptTemplate = obj.FunctionName + "({0});";
            StringBuilder sb = new StringBuilder();
            foreach (var parameter in obj.Parameters)
            {
                sb.Append("'");
                switch (parameter.Type)
                {
                    case OnItemSelectAdditionalScript.FunctionParameter.ParamType.ColumnName:
                        sb.Append(string.Format(OnItemSelectAdditionalScript.COLUMN_REPLACEMENT_MARKER_TEMP, parameter.Value));
                        break;
                    case OnItemSelectAdditionalScript.FunctionParameter.ParamType.Constant:
                        sb.Append(parameter.Value);
                        break;
                }
                sb.Append("',");
            }
            sb.Remove(sb.Length - 1, 1);

            return string.Format(scriptTemplate, sb.ToString());
        }
        private string formAdditionalScriptFunctions()
        {
            StringBuilder sb = new StringBuilder("");
            foreach (var item in OnItemSelectAdditionalScripts)
                sb.Append(formAdditionalScriptFunction(item));
            return sb.ToString();
        }

        private string _formedAdditionalScript;
        private string formedAdditionalScript
        {
            get
            {
                if (_formedAdditionalScript == null) _formedAdditionalScript = formAdditionalScriptFunctions();
                return _formedAdditionalScript;
            }
        }

        protected void txtKeyId_TextChanged(object sender, EventArgs e)
        {
            DataTable dtable = getData(true);

            lt.DataTextField = _data.ColumnBinders.FirstOrDefault(a => a.IsKeyId).Title;
            lt.DataValueField = CUSTOM_SCRIPT_TITLE;
            lt.DataSource = dtable;
            lt.DataBind();
            lt.Rows = dtable.Rows.Count + 1;

            lt.Items.Add(new ListItem("See More", "See More"));

            ScriptManager.RegisterClientScriptBlock(lt, lt.GetType(), lt.ClientID, "$('#" + lt.ClientID + "').focus();", true);
            pnlGrid.Visible = false;
        }

        protected void ltSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lt.Items.Count > 0) lt.SelectedIndex = 0;
            gvList.DataSource = getData(true);
            gvList.DataBind();

            rebindGvListHeader();
            pnlGrid.Visible = false;


            //try
            //{
            //    int checkPageSize = PageSize;

            //    gvList.DataSource = getData(true);
            //    gvList.DataBind();

            //    rebindGvListHeader();
            //}
            //catch (System.NullReferenceException ex)
            //{
            //    PageSize = 10;
            //    gvList.DataSource = getData(true);
            //    gvList.DataBind();

            //    rebindGvListHeader();
            //}

        }

        protected void imbLookup_Click(object sender, ImageClickEventArgs e)
        {
            //ltSearch_SelectedIndexChanged(sender, e);
            //gvList.DataSource = getData(false);
            //gvList.DataBind();

            //rebindGvListHeader();
            pnlGrid.Visible = false;
            UcSearch.ResetClick();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                DataTable dataSource = (sender as GridView).DataSource as DataTable;
                int selectColumn = dataSource.Columns.IndexOf(CUSTOM_SCRIPT_TITLE);
                HyperLink hpSelect = new HyperLink();
                hpSelect.Text = "Select";
                hpSelect.ID = "hpSelect";
                TableCell currentSelectCell = e.Row.Cells[selectColumn];
                if (TextChangedEvent == null)
                {
                    hpSelect.NavigateUrl = string.Format("javascript:{0};overlay('{1}');",
                        dataSource.Rows[e.Row.RowIndex][selectColumn], ModalDialogName);
                }
                else
                {
                    hpSelect.NavigateUrl = string.Format("javascript:{0};overlay('{1}');__doPostBack('" + ModalDialogName + "', '')",
                        dataSource.Rows[e.Row.RowIndex][selectColumn], ModalDialogName);
                }
                currentSelectCell.Text = "";
                currentSelectCell.Controls.Add(hpSelect);
                currentSelectCell.HorizontalAlign = HorizontalAlign.Center;
            }
        }
        #endregion

        #region PAGING
        private void SearchClick(int currentPage = 1, bool includeData = true)
        {
            DataTable dt = getData(false, currentPage, includeData);
            if (includeData)
            {
                gvList.DataSource = dt;
                gvList.DataBind();


                rebindGvListHeader();
                pnlGrid.Visible = true;
                upGrid.Update();
            }

            if (IsCount)
            {
                ucGridFooter.SetTotalRecrod(TotalRows);
            }
        }

        private void rebindGvListHeader()
        {
            if (gvList.HeaderRow != null)
            {
                foreach (TableCell cell in gvList.HeaderRow.Cells)
                {
                    string cellText = cell.Text.ToString();
                    ColumnBinder cb = _data.ColumnBinders.Where(x => x.Title == cellText).FirstOrDefault();
                    if (cb != null && cellText != "SELECT")
                    {
                        string script = "document.getElementById('" + hdnSort.ClientID + "').value='" + cb.ColumnName + "';doLookupSort('" + hdnSort.ClientID + "');";
                        cell.Text = "<a href=\"javascript:" + script + "\">" + cellText + "</a>";
                    }
                }
            }

        }

        protected void hdnSort_ValueChanged(object sender, EventArgs e)
        {
            string columnName = hdnSort.Value;
            orderByQry = columnName;
            if (orderByQry == null) orderByAscQry = true;
            else if (orderByQry == columnName) orderByAscQry = !orderByAscQry;
            else orderByAscQry = true;
            SearchClick(1, true);
            hdnSort.Value = "";
        }

        protected void ucGridHeader_Click(HeaderEventType eventType)
        {
            search_Click();
        }

        protected void ucGridFooter_Click(FooterEventType eventType, int currentPage)
        {
            SearchClick(currentPage);
        }

        private void search_Click()
        {
            ucGridFooter.ResetPage();
            SearchClick();
            //upGrid.Update();
        }

        //private string GetTotalRecordsText(int CurrentPage, int PageSize, int TotalRows)
        //{
        //    string result = "";
        //    int startRecords, endRecords;
        //    startRecords = (CurrentPage - 1) * PageSize + 1;
        //    endRecords = ((CurrentPage) * PageSize) > TotalRows ? TotalRows : ((CurrentPage) * PageSize);
        //    result = "Records " + startRecords.ToString() + " until " + endRecords.ToString() + " from " + TotalRows.ToString() + " records";
        //    return result;
        //}
        #endregion

        #region "EVENTS"

        public delegate void TextChangedDelegate(string compId, int index);
        public TextChangedDelegate TextChangedEvent { get; set; }

        public TextBox txtInputObj
        {
            get
            {
                return txt;
            }
            set
            {
                txt = value;
            }
        }

        public void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (txt.AutoPostBack == true && TextChangedEvent != null)
            {
                TextChangedEvent(hdnControlId.Value, Int32.Parse(hdnIndex.Value));
            }
        }

        public void EnableTextChanged(string controlId, int index)
        {
            txt.AutoPostBack = true;
            this.hdnControlId.Value = controlId;
            this.hdnIndex.Value = index.ToString();
        }

        public void EnableTextChanged()
        {
            txt.AutoPostBack = true;
            this.hdnControlId.Value = "";
            this.hdnIndex.Value = "0";
        }

        public void AssignEvent(TextChangedDelegate tcd)
        {
            TextChangedEvent += new TextChangedDelegate(tcd);
        }
        #endregion

        public ImageButton imbObj
        {
            get
            {
                return imb;
            }
            set
            {
                imb = value;
            }
        }
    }
}