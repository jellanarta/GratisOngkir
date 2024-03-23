using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdIns.Util;
using System.Runtime.Serialization;

namespace Rule.Common
{
    [DataContract(Name = "RuleEntitiesType")]
    [Serializable]
    public class RuleEntitiesType : EntitiesType
    {
        public static readonly EntitiesType MENU = new RuleEntitiesType("MenuEntities");
        public static readonly EntitiesType TRAINING = new RuleEntitiesType("TrainingEntities");
        private RuleEntitiesType(String val) : base(val) { }
    }
}
