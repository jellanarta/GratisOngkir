using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdIns.Service.QueryService;
using AdIns.Util;
using AdIns.Util.Query;
using AdIns.Service;
using Training.DataModel;


namespace Rule.Web
{
    public partial class TestForm : WebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //QueryService qryOrgSvc = (QueryService)UnityFactory.Resolve<QueryService>();
            //QueryParameter param = new QueryParameter();
            //param.EntitiesTypeName = ConfinsEntitiesType.t;
            //Criteria criteria = new Criteria();
            ////criteria.Add(Restrictions.Eq("RefBankId", refBankId));
            //param.Criteria = criteria;
            //param.IsDetach = false;

            //List<RefBank> listOfRefBank = (List<RefBank>)qryOrgSvc.GenericQueryList<RefBank>(param);
            //gvTest.DataSource = listOfRefBank;
            //gvTest.DataBind();

            ////RefBank bank = (RefBank)qryOrgSvc.GenericQuery<RefBank>(prmGetRefBank);

        }
    }
}