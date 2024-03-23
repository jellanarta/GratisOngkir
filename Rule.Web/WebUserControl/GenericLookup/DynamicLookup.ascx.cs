using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rule.Web.WebUserControl.Search;
using AdIns.Service.QueryService;
using AdIns.Util;
using AdIns.Service;

namespace Rule.Web.WebUserControl.GenericLookup
{
    public partial class DynamicLookup : UserControlBase
    {
        #region User Control's path
        private const string UCSEARCHPATH = "~/WebUserControl/Search/UCSearch.ascx";
        #endregion

        #region Public Properties

        public virtual string QueryServiceName
        {
            get { return _queryServiceName; }
            set { _queryServiceName = value; }
        }
        private string _queryServiceName
        {
            get { return ViewState["_queryPagingName"].ToString(); }
            set { ViewState["_queryPagingName"] = value; }
        }

        private GenericLookupData _data { get; set; }

        public virtual string MapperName
        {
            get { return _mapperName; }
            set { _mapperName = value; }
        }
        private string _mapperName { get; set; }


        #endregion

        #region On Init
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _data = UnityFactory.Resolve<GenericLookupData>(_mapperName);
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Events
        protected void ib_Click(object sender, EventArgs e)
        {
            Page parent = this.Parent.Page;
            PlaceHolder plc;
            string placeholderId = "plc" + _mapperName;

            //if (parent.Form.FindControl(placeholderId) != null)
            //{
            //    plc = (PlaceHolder)parent.Form.FindControl(placeholderId);
            //}
            //else
            //{
            plc = new PlaceHolder();
            plc.ID = placeholderId;
            //}

            UpdatePanel upModalDialog = new UpdatePanel();
            upModalDialog.ID = "up" + _mapperName;
            upModalDialog.UpdateMode = UpdatePanelUpdateMode.Conditional;
            upModalDialog.ChildrenAsTriggers = false;

            plc.Controls.Add(upModalDialog);

            UCSearch ucSearch = (UCSearch)Page.LoadControl(Page.ResolveUrl(UCSEARCHPATH));
            //ucSearch.IsLoadDynamically = true;
            ucSearch.ScParam = new SearchControlParam();
            ucSearch.ScParam.AddFixedSearchPropSpec(new FixedSearchPropSpec[] {
                new FixedSearchPropSpec("ltl_Cust_CustName", "CustName")
            });

            upModalDialog.ContentTemplateContainer.Controls.Add(ucSearch);
            upModalDialog.Update();

            parent.Form.Controls.Add(plc);
        }
        #endregion

        #region Public Methods
        public void InitalizeProperties(string QueryPagingName)
        {

        }

        private void searchClick(int currentPage, bool isData = true)
        {
            //QueryService qryOrgSvc = UnityFactory.Resolve<QueryService>();
            //PagingResult pagingResult = new PagingResult();
            //PagingSpec pagingSpec = new PagingSpec();
            //QueryParameter parameter = new QueryParameter();

            //pagingSpec.IncludeCount = isCount;
            //pagingSpec.IncludeData = isData;
            //pagingSpec.PageNo = currentPage;
            //pagingSpec.RowPerPage = pageSize;

            //if (OrderBy == null)
            //{
            //    parameter.AddOrderBy("AppNo", true);
            //}
            //else
            //{
            //    parameter.AddOrderBy(OrderBy.Text, bool.Parse(OrderBy.Value));
            //}

            ////criteria.Add(Restrictions.Eq("FlagInsStat", "ACT"));
            ////criteria.Add(Restrictions.Eq("ContractStat", "RRD"));
            ////criteria.Add(Restrictions.Eq("RefOfficeId", Page.CurrentUserContext.RefOfficeId));
            ////criteria.Add(Restrictions.Or(Restrictions.Eq("InsAssetCoveredBy", COMPANY), Restrictions.Eq("InsAssetCoveredBy", MIX)));

            //parameter.EntitiesTypeName = ConfinsEntitiesType.INS;
            //parameter.Criteria = criteria;

            //pagingResult = qryOrgSvc.QueryPaging("QryPagingInsTermination", pagingSpec, parameter);

            //if (isCount)
            //{
            //    ucGridFooter.SetTotalRecrod(pagingResult.Count);
            //}

            //if (isData)
            //{
            //    if (pagingResult.Data.Rows.Count == 0)
            //        throw new ConfinsDbException(ConfinsDbExceptionType.NoData, "", null, null);

            //    gvInsTermination.DataSource = pagingResult.Data;
            //    gvInsTermination.DataBind();

            //    UIDataHelper helper = new UIDataHelper();
            //    helper.WriteGridHeaders(gvInsTermination, base.LanguageCode);
            //    ItalicizeHeaderRow(gvInsTermination);
            //}

            //dGridSection.Visible = true;
            //upGrid.Update();
        }
        #endregion
    }
}