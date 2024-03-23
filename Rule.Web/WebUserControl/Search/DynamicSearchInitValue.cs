using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rule.Web.WebUserControl.Search
{
    public class DynamicSearchInitValue
    {
        #region "PROPERTIES"
        public bool IsShowRelationship { get; set; }
        public bool IsShowCondition { get; set; }
        #endregion

        #region "CONSTRUCTOR"
        public DynamicSearchInitValue(bool IsShowCondition, bool IsShowRelationship)
        {
            this.IsShowCondition = IsShowCondition;
            this.IsShowRelationship = IsShowRelationship;
        }
        public DynamicSearchInitValue(bool IsShowCondition) : this(IsShowCondition, true) { }
        #endregion
    }
}