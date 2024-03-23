using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.DataModel.TrainingModel;

namespace Training.API
{
    public interface IBookTransactionService
    {
        void add(Cust cust);

    }
}
