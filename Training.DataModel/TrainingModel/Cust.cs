using AdIns.DataModel.Audit;
using Confins.DataModel.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Training.DataModel.TrainingModel
{
    public class Cust : ITimestampable
    {
        #region Constructor
        public Cust() { }

        public Cust(long CustId)
        {
            this.CustId = CustId;
        }


        #endregion

        #region Primitive Properties

        public virtual long CustId
        {
            get;
            set;
        }

        public virtual string CustName
        {
            get;
            set;
        }

        public virtual string Gender
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> BirthDate
        {
            get;
            set;
        }

        public virtual string BirthPlace
        {
            get;
            set;
        }

        public virtual string MotherMaidenName
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

        #region Navigation Properties

        public virtual ICollection<BookTransactionH> BookTransactionH
        {
            get
            {
                if (_bookTransactionH == null)
                {
                    var newCollection = new FixupCollection<BookTransactionH>();
                    newCollection.CollectionChanged += FixupBookTransactionH;
                    _bookTransactionH = newCollection;
                }
                return _bookTransactionH;
            }
            set
            {
                if (!ReferenceEquals(_bookTransactionH, value))
                {
                    var previousValue = _bookTransactionH as FixupCollection<BookTransactionH>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupBookTransactionH;
                    }
                    _bookTransactionH = value;
                    var newValue = value as FixupCollection<BookTransactionH>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupBookTransactionH;
                    }
                }
            }
        }
        private ICollection<BookTransactionH> _bookTransactionH;

        #endregion

        #region Association Fixup

        private void FixupBookTransactionH(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (BookTransactionH item in e.NewItems)
                {
                    item.Cust = this;
                }
            }

            if (e.OldItems != null)
            {
                foreach (BookTransactionH item in e.OldItems)
                {
                    if (ReferenceEquals(item.Cust, this))
                    {
                        item.Cust = null;
                    }
                }
            }
        }

        #endregion
    }
}
