using AdIns.DataAccess;
using Confins.BusinessService.Common;
using Microsoft.Practices.Unity;
using Rule.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Training.DataAccess.Entities;
using Training.DataModel.TrainingModel;

namespace Training.BusinessService
{
    public class AddCustService : BaseService, IAddCustService
    {
        public AddCustService(IUnityContainer container) : base(container)
        {
        }

        public void add(Cust cust)
        {
            IRepository repository = container.Resolve<IRepository>(ConfinsEntitiesType.TRN.ToString());

            repository.Add(cust);
            repository.SaveChanges();
        }

        public Cust getCustById(long id) 
        {
            TrainingEntities context = (TrainingEntities)container.Resolve<TrainingEntities>(ConfinsEntitiesType.TRN.ToString());
            Cust cust =(from c in context.Custs
                        where c.CustId == id
                        select c).FirstOrDefault();
            return cust;
        }

        public void deleteCustById(Int64 id)
        {
            IRepository repository = container.Resolve<IRepository>(ConfinsEntitiesType.TRN.ToString());
            repository.Delete(new Cust(id));
            repository.SaveChanges();   
        }
    }
}
