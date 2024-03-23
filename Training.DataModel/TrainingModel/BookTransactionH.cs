using AdIns.DataModel.Audit;
using Confins.DataModel.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Training.DataModel.TrainingModel
{
    public partial class BookTransactionH : ITimestampable
    {

        #region Primitive Properties

        public virtual long BookTransactionHId
        {
            get;
            set;
        }

        public virtual long CustId
        {
            get { return _custId; }
            set
            {
                if (_custId != value)
                {
                    if (Cust != null && Cust.CustId != value)
                    {
                        Cust = null;
                    }
                    _custId = value;
                }
            }
        }
        private long _custId;

        public virtual string TransactionNo
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> TransactionDate
        {
            get;
            set;
        }

        public virtual Nullable<decimal> TotalPrice
        {
            get;
            set;
        }

        public virtual string Notes
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

        public virtual ICollection<BookTransactionD> BookTransactionD
        {
            get
            {
                if (_bookTransactionD == null)
                {
                    var newCollection = new FixupCollection<BookTransactionD>();
                    newCollection.CollectionChanged += FixupBookTransactionD;
                    _bookTransactionD = newCollection;
                }
                return _bookTransactionD;
            }
            set
            {
                if (!ReferenceEquals(_bookTransactionD, value))
                {
                    var previousValue = _bookTransactionD as FixupCollection<BookTransactionD>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupBookTransactionD;
                    }
                    _bookTransactionD = value;
                    var newValue = value as FixupCollection<BookTransactionD>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupBookTransactionD;
                    }
                }
            }
        }
        private ICollection<BookTransactionD> _bookTransactionD;

        public virtual Cust Cust
        {
            get { return _cust; }
            set
            {
                if (!ReferenceEquals(_cust, value))
                {
                    var previousValue = _cust;
                    _cust = value;
                    FixupCust(previousValue);
                }
            }
        }
        private Cust _cust;

        #endregion

        #region Association Fixup

        private void FixupCust(Cust previousValue)
        {
            if (previousValue != null && previousValue.BookTransactionH.Contains(this))
            {
                previousValue.BookTransactionH.Remove(this);
            }

            if (Cust != null)
            {
                if (!Cust.BookTransactionH.Contains(this))
                {
                    Cust.BookTransactionH.Add(this);
                }
                if (CustId != Cust.CustId)
                {
                    CustId = Cust.CustId;
                }
            }
        }

        private void FixupBookTransactionD(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (BookTransactionD item in e.NewItems)
                {
                    item.BookTransactionH = this;
                }
            }

            if (e.OldItems != null)
            {
                foreach (BookTransactionD item in e.OldItems)
                {
                    if (ReferenceEquals(item.BookTransactionH, this))
                    {
                        item.BookTransactionH = null;
                    }
                }
            }
        }

        #endregion

    }
}
