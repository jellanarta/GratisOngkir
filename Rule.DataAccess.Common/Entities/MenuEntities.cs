﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;
using AdIns.DataAccess;
using AdIns.DataAccess.Session;
using AdIns.DataAccess.Session.EF;
using Confins.DataModel.Common.NCModel;


namespace Rule.DataAccess.Common.Entities
{
    public partial class MenuEntities : BaseObjectContext
    {

        #region Constructors

        public MenuEntities(ISession session, string entityContainerName)
            : base((EFSession)session, entityContainerName)
        {
        }

        #endregion

        #region ObjectSet Properties

        public ObjectSet<RefFolder> RefFolders
        {
            get { return _refFolders ?? (_refFolders = CreateObjectSet<RefFolder>("RefFolders")); }
        }
        private ObjectSet<RefFolder> _refFolders;

        public ObjectSet<RefForm> RefForms
        {
            get { return _refForms ?? (_refForms = CreateObjectSet<RefForm>("RefForms")); }
        }
        private ObjectSet<RefForm> _refForms;

        #endregion
    }
}
