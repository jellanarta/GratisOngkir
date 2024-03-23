using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rule.Web.Class
{
    public interface IApprovalScreenSource
    {
        Type MainDataSourceName { get; }
        void AssignId(long id);
    }
}