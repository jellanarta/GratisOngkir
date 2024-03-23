using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Rule.Web.WebUserControl.GenericLookup
{
    public class GenericLookupData
    {
        public const string SCRIPT_VIRTUAL_PATH = "GenericLookupScriptDumpVirtualFilePath";

        public GenericLookupData(ColumnBinder[] columnBinders, string functionName)
        {
            this.ColumnBinders = columnBinders;
            this.FunctionName = functionName;
        }

        public ColumnBinder[] ColumnBinders { get; private set; }

        public string FunctionName { get; private set; }
    }

    public class ColumnBinder
    {
        public ColumnBinder(string columnName, string title, bool modalDialogVisibility, bool isKeyId)
        {
            this.ColumnName = columnName;
            this.Title = title;
            this.ModalDialogVisibility = modalDialogVisibility;
            this.IsKeyId = isKeyId;
        }

        public ColumnBinder(string columnName, string title, bool modalDialogVisibility)
            : this(columnName, title, modalDialogVisibility, false)
        {
        }

        public ColumnBinder(string columnName, string title)
            : this(columnName, title, true, false)
        {
        }

        public string ColumnName { get; set; }
        public string Title { get; set; }
        public bool ModalDialogVisibility { get; set; }
        public bool IsKeyId { get; set; }
    }

    public class ColumnBinderTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string[] args = value.ToString().Split(new char[1] { ',' });
            switch (args.Length)
            {
                case 4:
                    return new ColumnBinder(args[0], args[1], bool.Parse(args[2]), bool.Parse(args[3]));
                case 3:
                    return new ColumnBinder(args[0], args[1], bool.Parse(args[2]));
                case 2:
                    return new ColumnBinder(args[0], args[1]);
                default:
                    return null;
            }
        }
    }
}