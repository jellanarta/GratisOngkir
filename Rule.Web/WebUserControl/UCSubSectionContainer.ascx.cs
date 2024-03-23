using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rule.Web.WebUserControl
{
    public partial class UCSectionContainer : System.Web.UI.UserControl
    {
        public string toggleID { get; set; }
        public string affectedID { get; set; }
        public string subSectionTitle
        {
            set { subSectionID.Text = value; }
        }
        public bool IsHideContent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsHideContent)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ToggleControl" + affectedID, "<script type='text/javascript'>ExpandUnexpandMenu('" + toggleID + "','" + affectedID + "', '" + subSectionID.ClientID + "')</script>", false);


                }
            }
        }
    }
}