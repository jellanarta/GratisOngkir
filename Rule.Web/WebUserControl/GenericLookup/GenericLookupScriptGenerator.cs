using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Rule.Web.WebUserControl.GenericLookup
{
    public class GenericLookupScriptGenerator
    {
        private FileStream _stream;

        public GenericLookupScriptGenerator(string filePath)
        {
            _stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite);
        }

        private const string TEXT_FIELD = "txt{0}";
        private const string HIDDEN_FIELD = "hdn{0}";
        private const string VALUE = "val{0}";
        private const string CONTROL = "document.getElementById({0}).value";
        public void WriteFunction(ColumnBinder[] columnBinders, string functionName)
        {
            StringBuilder functionDef = new StringBuilder(string.Format("function {0}", functionName));
            StringBuilder functionBody = new StringBuilder();
            functionDef.Append("(");
            functionBody.AppendLine("{");
            foreach (var columnBinder in columnBinders)
            {
                string hiddenField = string.Format(HIDDEN_FIELD, columnBinder.ColumnName);
                string value = string.Format(VALUE, columnBinder.ColumnName);
                string control;

                if (columnBinder.IsKeyId)
                {
                    string textField = string.Format(TEXT_FIELD, columnBinder.ColumnName);

                    functionDef.Append(textField);
                    functionDef.Append(",");

                    control = string.Format(CONTROL, textField);
                    functionBody.AppendLine(string.Format("{0} = {1};", control, value));
                }

                functionDef.Append(hiddenField);
                functionDef.Append(",");
                functionDef.Append(value);
                functionDef.Append(",");

                control = string.Format(CONTROL, hiddenField);
                functionBody.AppendLine(string.Format("{0} = {1};", control, value));
            }
            functionBody.AppendLine("}");
            functionDef.Remove(functionDef.Length - 1, 1);
            functionDef.Append(")");

            UTF8Encoding encoding = new UTF8Encoding();

            byte[] functionDefBytes = encoding.GetBytes(functionDef.ToString());
            _stream.Write(functionDefBytes, 0, functionDefBytes.Length);

            byte[] functionBodyBytes = encoding.GetBytes(functionBody.ToString());
            _stream.Write(functionBodyBytes, 0, functionBodyBytes.Length);
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}