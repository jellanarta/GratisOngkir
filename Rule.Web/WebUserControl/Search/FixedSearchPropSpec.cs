using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Rule.Web.WebUserControl.Search
{
    public class FixedSearchPropSpec : SearchPropSpec
    {
        #region "PROPERTIES"
        public bool IsRequired { get; set; }
        public bool IsShowSearchCondLabel { get; set; }
        public UCReference.AdditionalSelectionType AdditionalSelectionType { get; set; }
        #endregion

        #region "CONSTRUCTOR"
        //common constructor
        public FixedSearchPropSpec(string Text, string PropName, Type Type,
            bool IsRequired = false, SearchCondition SearchCondition = SearchCondition.eq, bool isShowSearchCondLabel = false, ValOptType ValOptType = ValOptType.Default, int index = 0)
        {
            this.Text = Text;
            this.PropName = PropName;
            this.PropType = Type;
            this.IsRequired = IsRequired;
            this.SearchCond = SearchCondition;
            this.IsShowSearchCondLabel = isShowSearchCondLabel;
            this.ValueOptionType = SearchPropSpec.ValOptType.Default;
            this.index = index;
        }
        public FixedSearchPropSpec(string Text, string PropName, Type Type) : this(Text, PropName, Type, false) { }
        public FixedSearchPropSpec(string Text, string PropName) : this(Text, PropName, typeof(string), false) { }

        //constructor for Reference Type  
        public FixedSearchPropSpec(string Text, string PropName, IList Reference_DataSource, string Reference_DataTextField, string Reference_DataValueField,
            Type Type, bool IsRequired = false, UCReference.AdditionalSelectionType addSelectionType = UCReference.AdditionalSelectionType.None, SearchCondition            SearchCondition = SearchCondition.eq, bool isShowSearchCondLabel = false, ValOptType ValOptType = ValOptType.Reference)
        {
            this.Text = Text;
            this.PropName = PropName;
            this.IsRequired = IsRequired;
            this.PropType = Type;
            this.SearchCond = SearchCondition;
            this.Reference_DataSource = Reference_DataSource;
            this.Reference_DataTextField = Reference_DataTextField;
            this.Reference_DataValueField = Reference_DataValueField;
            this.ValueOptionType = SearchPropSpec.ValOptType.Reference;
            this.AdditionalSelectionType = addSelectionType;
            this.IsShowSearchCondLabel = isShowSearchCondLabel;
        }

        //constructor for LookupType
        public FixedSearchPropSpec(string Text, string PropName, Type Type, bool IsRequired, string LookupPath, ValOptType ValOptType = ValOptType.Lookup)
        {
            this.Text = Text;
            this.PropName = PropName;
            this.PropType = Type;
            this.IsRequired = IsRequired;
            this.LookupPath = LookupPath;
            this.ValueOptionType = SearchPropSpec.ValOptType.Lookup;
        }
        #endregion
    }
}