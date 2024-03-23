using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rule.Web.WebUserControl.Search
{
    public enum SelectionMode { All, SelectOne, None }
    public class SearchControlParam
    {
        public List<DynamicSearchPropSpec> DynamicSearch;
        public List<FixedSearchPropSpec> FixedSearch;
        public List<DynamicSearchInitValue> DynamicInitValue;

        public SearchControlParam()
        {
            FixedSearch = new List<FixedSearchPropSpec>();
            DynamicSearch = new List<DynamicSearchPropSpec>();
            DynamicInitValue = new List<DynamicSearchInitValue>();
        }

        public SearchControlParam AddFixedSearchPropSpec(FixedSearchPropSpec[] fixedPropSpecs)
        {
            int ind = 0;
            foreach (FixedSearchPropSpec fixedPropSpec in fixedPropSpecs)
            {
                fixedPropSpec.index = ind;
                this.FixedSearch.Add(fixedPropSpec);
                ind++;
            }
            return this;
        }

        public SearchControlParam AddDynamicSearchPropSpec(DynamicSearchPropSpec[] dynamicPropSpecs)
        {
            foreach (DynamicSearchPropSpec dynamicPropSpec in dynamicPropSpecs)
            {
                this.DynamicSearch.Add(dynamicPropSpec);
            }
            return this;
        }

        public SearchControlParam AddDynamicInitValue(DynamicSearchInitValue[] dynamicInitValues)
        {
            foreach (DynamicSearchInitValue dynamicInitValue in dynamicInitValues)
            {
                this.DynamicInitValue.Add(dynamicInitValue);
            }
            return this;
        }

    }
}