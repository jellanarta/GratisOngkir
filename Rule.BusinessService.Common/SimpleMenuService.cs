using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Rule.DataAccess.Common.Entities;
using AdIns.DataAccess;
using System.Data;
using System.Collections;
using Confins.DataModel.Common.NCModel;
using AdIns.Service;
using Confins.BusinessService.Common;
using Rule.DataAccess.Common.Entities;
using Rule.Common;

namespace Rule.BusinessService.Common
{
    public class SimpleMenuService : BaseService
    {
        #region Constructor and Private Variables
        private List<RefFolder> _listRefFolder;
        private List<RefForm> _listRefForm;

        public SimpleMenuService(IUnityContainer container)
            : base(container)
        {
        }

        #endregion
        #region Tree Menu Method

        [Confins.BusinessService.Common.ServiceAttributes.TransactionSupported]
        public virtual List<RefFolder> GetAllFoldersByHierarchyNo(Int32 hierarchyNo)
        {
            MenuEntities ent = (MenuEntities)UnityFactory.Resolve<BaseObjectContext>(RuleEntitiesType.MENU.ToString());
            List<RefFolder> result = (from a in ent.RefFolders
                                      where a.HierarchyNo == hierarchyNo && a.IsHidden == "0"
                                      select a).ToList();
            return result;
        }

        [Confins.BusinessService.Common.ServiceAttributes.TransactionSupported]
        public virtual List<RefFolder> GetAllFoldersByParent(Int64 parent)
        {
            MenuEntities ent = (MenuEntities)UnityFactory.Resolve<BaseObjectContext>(RuleEntitiesType.MENU.ToString());
            List<RefFolder> result = (from a in ent.RefFolders
                                      where a.ParentId == parent && a.IsHidden == "0"
                                      select a).ToList();
            return result;
        }

        [Confins.BusinessService.Common.ServiceAttributes.TransactionSupported]
        public virtual List<RefForm> GetAllFormsByFolder(Int64 folderId)
        {
            MenuEntities ent = (MenuEntities)UnityFactory.Resolve<BaseObjectContext>(RuleEntitiesType.MENU.ToString());
            List<RefForm> result = (from a in ent.RefForms
                                      where a.RefFolderId == folderId && a.IsHidden == "0"
                                      select a).ToList();
            return result;
        }

        #endregion
    }
}
