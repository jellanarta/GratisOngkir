using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Collections;

namespace Rule.Web.WebUserControl
{
    public partial class UCReference : UserControlBase
    {
        #region "ENUM"
        public enum AdditionalSelectionType
        {
            All,
            None,
            SelectOne
        }
        #endregion

        #region "PROPERTIES"
        public string SelectedValue
        {
            get { return hdnReference.Value == "" ? ddlReference.SelectedItem.Value : hdnReference.Value; }
        }

        public string SelectedText
        {
            get { return ltlReference.Text == "" ? ddlReference.SelectedItem.Text : ltlReference.Text; }
        }
        #endregion

        #region "PAGE LOAD"
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region "METHOD"
        public void BindingObject(IList listOfReference, string text, string value, bool isRequired = false, AdditionalSelectionType addSelectionType = AdditionalSelectionType.None)
        {
            if (listOfReference.Count > 1)
            {
                ddlReference.DataSource = listOfReference;
                ddlReference.DataTextField = text;
                ddlReference.DataValueField = value;
                ddlReference.DataBind();

                ddlReference.Visible = true;
                ltlReference.Visible = false;
                hdnReference.Value = string.Empty;

                if (addSelectionType == AdditionalSelectionType.SelectOne || isRequired)
                    ddlReference.Items.Insert(0, new ListItem("Select One", ""));
                else if (addSelectionType == AdditionalSelectionType.All)
                    ddlReference.Items.Insert(0, new ListItem("All", "All"));

                if (isRequired)
                    setEnabledVisible(rfvDdlReference, true);
                else
                    setEnabledVisible(rfvDdlReference, false);
            }
            else if (listOfReference.Count == 1)
            {
                DataTable dTable = convertToDataTable(listOfReference);
                DataRow dRow = dTable.Rows[0];

                ltlReference.Text = dRow[text].ToString();
                hdnReference.Value = dRow[value].ToString();

                ddlReference.DataSource = null;
                ddlReference.DataBind();
                ddlReference.Visible = false;
                ltlReference.Visible = true;
                setEnabledVisible(rfvDdlReference, false);
            }
        }

        public void ResetDropDownList()
        {
            ddlReference.SelectedIndex = 0;
        }

        private DataTable convertToDataTable(IList items)
        {
            Type type = items.GetType().GetGenericArguments()[0];

            var tb = new DataTable(type.Name);

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = getCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        private static bool isNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private static Type getCoreType(Type t)
        {
            if (t != null && isNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        private void setEnabledVisible(WebControl obj, bool avaiability)
        {
            obj.Visible = avaiability;
            obj.Enabled = avaiability;
        }
        #endregion
    }
}