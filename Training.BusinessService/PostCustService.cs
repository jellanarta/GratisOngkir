using AdIns.DataAccess;
using Confins.BusinessService.Common;
using Microsoft.Practices.Unity;
using Rule.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Training.DataModel.TrainingModel;

namespace Training.BusinessService
{
    public class PostCustService : BaseService, IPostCust
    {
        public PostCustService(IUnityContainer container):base(container) 
        { 
        }

        public void add(Cust cust)
        {
            IRepository repository = container.Resolve<IRepository>(ConfinsEntitiesType.TRN.ToString());
            repository.Add(cust);
            repository.SaveChanges();
        }
    }
}
