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
    public partial class PostCust : WebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Send_Cust(object sender,EventArgs e)
        {
            Cust cust = new Cust();
            IPostCust sendCust = (IPostCust)UnityFactory.Resolve<IPostCust>();

            cust.CustName = "Jellan Arta";
            cust.Gender = "L";
            cust.BirthPlace = "Praya";
            cust.BirthDate = DateTime.Now;
            cust.MotherMaidenName = "Ada Dong";

            sendCust.add(cust);
        }
    }
}