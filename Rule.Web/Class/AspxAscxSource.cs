using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Confins.Common.Exp;
using System.Web.Compilation;
using System.ComponentModel;
using System.Web.UI;

namespace Rule.Web.Class
{
    public class FileDocAlias
    {
        public string File { get; private set; }
        public string DocAlias { get; private set; }

        public FileDocAlias(string file, string docAlias = null)
        {
            this.File = file;
            this.DocAlias = docAlias;
        }
    }

    public class AspxAscxSource
    {
        private FileDocAlias[] _fileDocAliases;

        public AspxAscxSource(FileDocAlias[] fileDocAliases)
        {
            this._fileDocAliases = fileDocAliases;
        }

        public FileDocAlias[] GetAllFiles(string[] dataModels)
        {
            List<FileDocAlias> filteredFiles = new List<FileDocAlias>();
            for (int i = 0; i < _fileDocAliases.Length; i++)
            {
                string file = _fileDocAliases[i].File;
                string extension = file.Substring(file.Length - 5);
                if (extension != ".aspx"
                    && extension != ".ascx")
                    throw new ConfinsException(ExceptionType.ExOther, "The specified file is not a page or usercontrol file.", null);

                IApprovalScreenSource screenSource = null;
                switch (extension)
                {
                    case ".aspx":
                        screenSource = BuildManager.CreateInstanceFromVirtualPath(file, typeof(Page)) as IApprovalScreenSource;
                        break;
                    case ".ascx":
                        screenSource = (new Page()).LoadControl(file) as IApprovalScreenSource;
                        break;
                }
                if (screenSource == null)
                    throw new ConfinsException(ExceptionType.ExOther, string.Format("Approval's views should implement {0}."
                        , typeof(IApprovalScreenSource).Name), null);
                else
                {
                    if (dataModels.Contains(screenSource.MainDataSourceName.Name))
                        filteredFiles.Add(_fileDocAliases[i]);
                }
            }
            return filteredFiles.ToArray();
        }

        public FileDocAlias[] GetAllAliasedFiles(string[] dataModels)
        {
            return GetAllFiles(dataModels).Where(a => a.DocAlias != null).ToArray();
        }

        public FileDocAlias[] GetAllUnaliasedAscxFiles(string[] dataModels)
        {
            return GetAllFiles(dataModels).Where(a => a.DocAlias == null && a.File.Substring(a.File.Length - 5) == ".ascx").ToArray();
        }

        public FileDocAlias[] GetAllUnaliasedAspxFiles(string[] dataModels)
        {
            return GetAllFiles(dataModels).Where(a => a.DocAlias == null && a.File.Substring(a.File.Length - 5) == ".aspx").ToArray();
        }
    }

    public class AspxAscxSourceTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string[] fileDocAlias = value.ToString().Split(new char[1] { ',' });
            if (fileDocAlias.Length > 1)
                return new FileDocAlias(fileDocAlias[0], fileDocAlias[1]);
            else
                return new FileDocAlias(fileDocAlias[0]);
        }
    }
}