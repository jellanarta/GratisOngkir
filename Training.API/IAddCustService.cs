using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataModel.TrainingModel;

namespace Training.API
{
    public interface IAddCustService
    {
        void add(Cust cust);
        Cust getCustById(long id);
        void deleteCustById(Int64 id);   
    }
}
