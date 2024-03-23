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
    public partial class testbook : WebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Insert_Click(object sender, EventArgs e)
        {
            Cust cust = new Cust();
            IBookTransactionService btisvc = (IBookTransactionService)UnityFactory.Resolve<IBookTransactionService>();

            cust.CustName = "Nama";
            cust.Gender = "L";
            cust.BirthPlace = "ataram";
            cust.BirthDate = DateTime.Now;
            cust.MotherMaidenName = "Abc";

            btisvc.add(cust);

        }

    }
}