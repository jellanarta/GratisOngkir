using AdIns.Service;
using Rule.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Training.API;
using Training.DataModel.TrainingModel;

namespace Training
{
    public partial class AddCust : WebFormBase
    {
        IAddCustService custService = (IAddCustService)UnityFactory.Resolve<IAddCustService>();
        protected global::Rule.Web.WebUserControl.UCDatePicker UCBirthDate;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            Cust cust = new Cust();
            cust.CustName=txt_CustName.Text;
            cust.Gender=rbl_Gender.SelectedValue;
            cust.BirthDate= Convert.ToDateTime(UCBirthDate.Text);
            custService.add(cust);

        } 
        protected void lbReset_Click(object sender, EventArgs e)
        {
            clearScreen();
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            Cust cust = new Cust();
            cust = custService.getCustById(Convert.ToInt64(txt_id.Text));
            if (cust != null)
            {
                txt_CustName.Text = cust.CustName;
                UCBirthDate.Text= Convert.ToString(cust.BirthDate); 
                rbl_Gender.SelectedValue= cust.Gender;
            }
            upForm.Update();
        }
        protected void lbDel_Click(object sender, EventArgs e)
        {
            custService.deleteCustById(Convert.ToInt64(txt_id.Text));
        }
        protected void lbRedirect_Click(object sender, EventArgs e)
        {
            RedirectUrl("~/Training/ViewCust/ViewCust.aspx",PageRedirectionState.InitAdd("custId",txt_id.Text));
        }

        public void clearScreen() 
        {
            txt_CustName.Text = "";
            UCBirthDate.Text = "";
            upForm.Update();
        }
    }
}