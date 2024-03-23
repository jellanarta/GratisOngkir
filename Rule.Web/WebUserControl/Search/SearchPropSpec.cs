using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Rule.Web.WebUserControl.Search
{
    public abstract class SearchPropSpec
    {
        #region "PROPERTIES"
        public ValOptType ValueOptionType { get; set; }
        public enum ValOptType
        {
            Default,
            Reference,
            Description,
            Lookup
        }
        public string PropName { get; set; }
        public string Text { get; set; }
        public Type PropType { get; set; }
        public int index { get; set; }

        #region DEFAULT
        public SearchCondition SearchCond { get; set; }
        public enum SearchCondition
        {
            eq, gt, lt, gte, lte
        }
        #endregion

        #region REFERENCE
        public IList Reference_DataSource { get; set; }
        public string Reference_DataTextField { get; set; }
        public string Reference_DataValueField { get; set; }
        public SelectionMode DdlSelectionMode { get; set; }
        #endregion

        #region LOOKUP
        public string LookupPath { get; set; }
        #endregion
        #endregion

        #region "CONSTRUCTOR"
        public SearchPropSpec()
        {
            ValueOptionType = ValOptType.Default;
            SearchCond = SearchCondition.eq;
        }
        public SearchPropSpec(SearchCondition search)
        {
            SearchCond = search;
        }
        #endregion
    }
}