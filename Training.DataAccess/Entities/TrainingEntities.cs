using AdIns.DataAccess;
using AdIns.DataAccess.Session;
using AdIns.DataAccess.Session.EF;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Training.DataModel.TrainingModel;

namespace Training.DataAccess.Entities
{
    public class TrainingEntities : BaseObjectContext
    {
        #region Constructors

        public TrainingEntities(ISession session, string entityContainerName)
            : base((EFSession)session, entityContainerName)
        {

        }

        #endregion
        #region ObjectSet Properties
        public ObjectSet<BookTransactionD> BookTransactionDs
        {
            get { return _bookTransactionDs ?? (_bookTransactionDs = CreateObjectSet<BookTransactionD>("BookTransactionDs")); }
        }
        private ObjectSet<BookTransactionD> _bookTransactionDs;

        public ObjectSet<BookTransactionH> BookTransactionHs
        {
            get { return _bookTransactionHs ?? (_bookTransactionHs = CreateObjectSet<BookTransactionH>("BookTransactionHs")); }
        }
        private ObjectSet<BookTransactionH> _bookTransactionHs;

        public ObjectSet<RefBook> RefBooks
        {
            get { return _refBooks ?? (_refBooks = CreateObjectSet<RefBook>("RefBooks")); }
        }
        private ObjectSet<RefBook> _refBooks;

        public ObjectSet<Cust> Custs
        {
            get { return _custs ?? (_custs = CreateObjectSet<Cust>("Custs")); }
        }
        private ObjectSet<Cust> _custs;

        public ObjectSet<Book> Books
        {
            get { return _books ?? (_books = CreateObjectSet<Book>("Books")); }
        }
        private ObjectSet<Book> _books;

        #endregion
    }
}
