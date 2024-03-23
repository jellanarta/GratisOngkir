using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rule.Web.WebUserControl
{
    public enum HeaderEventType { REFRESH, ADD };

    public partial class UCGridHeader : UserControlBase
    {
        public bool isAddVisible
        {
            get { return lbAdd.Visible; }
            set { lbAdd.Visible = value; }
        }
        public delegate void DelegateDataBind(HeaderEventType eventType);
        public DelegateDataBind DataBinder;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void pagerHeader_Click(object sender, CommandEventArgs e)
        {
            HeaderEventType evType = HeaderEventType.REFRESH;
            switch (e.CommandName)
            {
                case "Refresh":
                    evType = HeaderEventType.REFRESH;
                    break;
                case "Add" :
                    evType = HeaderEventType.ADD;
                    break;
            }
            DataBinder(evType);
        }
    }
}