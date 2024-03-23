using AdIns.DataModel.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.DataModel.TrainingModel
{
    public partial class RefBook : ITimestampable
    {
        #region Constructor
        public RefBook() { }

        public RefBook(long RefBookId)
        {
            this.RefBookId = RefBookId;
        }


        #endregion

        #region Primitive Properties

        public virtual long RefBookId
        {
            get;
            set;
        }

        public virtual Nullable<long> AuthorId
        {
            get;
            set;
        }

        public virtual string BookName
        {
            get;
            set;
        }

        public virtual string BookNo
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> ReleaseDate
        {
            get;
            set;
        }

        public virtual Nullable<int> BookPage
        {
            get;
            set;
        }

        public virtual Nullable<decimal> BookPrice
        {
            get;
            set;
        }

        #endregion

        #region Complex Properties

        public virtual UserTimestamp LastUserTimestamp
        {
            get { return _lastUserTimestamp; }
            set { _lastUserTimestamp = value; }
        }
        private UserTimestamp _lastUserTimestamp = new UserTimestamp();

        #endregion

    }
}
