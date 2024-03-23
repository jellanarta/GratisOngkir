using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Rule.Web.WebUserControl.Search
{
    public class DynamicSearchPropSpec : SearchPropSpec
    {
        #region "CONSTRUCTOR"
        public DynamicSearchPropSpec(string Text, string PropName, Type Type)
        {
            this.Text = Text;
            this.PropName = PropName;
            this.PropType = Type;                
        }
        public DynamicSearchPropSpec(string Text, string PropName) : this(Text, PropName, typeof(string)) { }
        #endregion
    }
}