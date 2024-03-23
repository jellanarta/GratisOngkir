using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rule.Web.WebUserControl
{
    public enum FooterEventType { FIRST, PREVIOUS, NEXT, LAST, COUNT, PAGESIZE_CHANGED };

    public partial class UCGridFooter : UserControlBase
    {
        #region "PROPERTIES"
        public bool IsCount
        {
            get { return (bool)ViewState["IsCount"]; }
            set { ViewState["IsCount"] = value; }
        }
        public int currentPage
        {
            get { return (int)ViewState["CurrentPage"]; }
            set { ViewState["CurrentPage"] = value; }
        }
        public int PageSize
        {
            get { return (int)ViewState["PageSize"]; }
            set { ViewState["PageSize"] = value; }
        }
        private int totalPages
        {
            get { return (int)ViewState["TotalPages"]; }
            set { ViewState["TotalPages"] = value; }
        }
        private int totalRecord = -1;

        public delegate void DelegateDataBind(FooterEventType eventType, int currentPage);
        public DelegateDataBind DataBinder;
        #endregion

        #region "PAGE LOAD"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetPage();
            }
        }

        #endregion

        #region "NAVIGATION"

        protected void Navigation_Click(object sender, CommandEventArgs e)
        {
            //mengecek apakah ada yang memakai dataBinder
            if (DataBinder != null)
            {
                FooterEventType evType = FooterEventType.FIRST;
                switch (e.CommandName)
                {
                    case "First":
                        currentPage = 1;
                        evType = FooterEventType.FIRST;
                        break;
                    case "Last":
                        currentPage = totalPages;
                        evType = FooterEventType.LAST;
                        break;
                    case "Next":
                        currentPage++;
                        evType = FooterEventType.NEXT;
                        break;
                    case "Prev":
                        currentPage--;
                        evType = FooterEventType.PREVIOUS;
                        break;
                }
                DataBinder(evType, currentPage);
                CheckNavigation();
            }
        }

        protected void CheckNavigation()
        {
            if (currentPage == 1)
            {
                lbPrevRecord.Enabled = false;
                lbFirstRecord.Enabled = false;

                if (totalPages != -1)
                {
                    if (totalPages > 1)
                    {
                        lbNextRecord.Enabled = true;
                        lbLastRecord.Enabled = true;
                    }
                    else
                    {
                        lbNextRecord.Enabled = false;
                        lbLastRecord.Enabled = false;
                    }
                }
            }
            else
            {
                lbPrevRecord.Enabled = true;
                lbFirstRecord.Enabled = true;

                if (totalPages != -1)
                {
                    if (currentPage == totalPages)
                    {
                        lbNextRecord.Enabled = false;
                        lbLastRecord.Enabled = false;
                    }
                    else
                    {
                        lbNextRecord.Enabled = true;
                        lbLastRecord.Enabled = true;
                    }
                }
            }
        }

        #endregion

        #region "CHANGE PAGE SIZE"

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = int.Parse(ddlPageSize.SelectedItem.Value);

            FooterEventType evType = FooterEventType.PAGESIZE_CHANGED;
            DataBinder(evType, currentPage);
        }

        #endregion
        
        #region "SHOW TOTAL RECORDS"

        protected void lbCountRecords_Click(object sender, EventArgs e)
        {
            FooterEventType evType = FooterEventType.COUNT;
            //Command = "count";
            IsCount = true;
            DataBinder(evType, currentPage);

            Decimal totalPagesDec = -1;
            if (totalRecord != -1)
            {
                Decimal totalRecordDec = totalRecord;
                Decimal pageSizeDec = PageSize;
                totalPagesDec = Math.Ceiling(totalRecordDec / pageSizeDec);
                totalPages = int.Parse(totalPagesDec.ToString());
            }
            CheckNavigation();
            //lbLastRecord.Enabled = true;
        }

        public void SetTotalRecrod(int totalRecord)
        {
            this.totalRecord = totalRecord;
            this.RefreshCountLabel();
        }

        private void RefreshCountLabel()
        {
            if (totalRecord != -1)
            {
                lbCountRecords.Text = GetTotalRecordsText(currentPage, PageSize, totalRecord);
            }
        }

        private string GetTotalRecordsText(int CurrentPage, int PageSize, int TotalRows)
        {
            string result = "";
            int startRecords, endRecords;
            startRecords = (CurrentPage - 1) * PageSize + 1;
            endRecords = ((CurrentPage) * PageSize) > TotalRows ? TotalRows : ((CurrentPage) * PageSize);
            result = "Records " + startRecords.ToString() + " until " + endRecords.ToString() + " from " + TotalRows.ToString() + " records";
            return result;
        }

        #endregion

        public void ResetPage()
        {
            Initialize();
            CheckNavigation();
        }

        public void Initialize()
        {
            currentPage = 1;
            PageSize = 10;
            totalPages = -1;
            IsCount = false;
        }
    }
}