using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using AdIns.Util;

namespace Rule.Common
{
    [DataContract(Name = "ConfinsEntitiesType")]
    [Serializable]
    public class ConfinsEntitiesType : EntitiesType
    {
        public static readonly EntitiesType TRN = new ConfinsEntitiesType("TrainingEntities");

        private ConfinsEntitiesType(String val) : base(val) { }

    }
}
