using AdIns.DataModel.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.DataModel.TrainingModel
{
    public partial class BookTransactionD : ITimestampable
    {

        #region Primitive Properties

        public virtual long BookTransactionDId
        {
            get;
            set;
        }

        public virtual long BookTransactionHId
        {
            get { return _bookTransactionHId; }
            set
            {
                if (_bookTransactionHId != value)
                {
                    if (BookTransactionH != null && BookTransactionH.BookTransactionHId != value)
                    {
                        BookTransactionH = null;
                    }
                    _bookTransactionHId = value;
                }
            }
        }
        private long _bookTransactionHId;

        public virtual Nullable<int> SeqNo
        {
            get;
            set;
        }

        public virtual long RefBookId
        {
            get;
            set;
        }

        public virtual Nullable<int> Qty
        {
            get;
            set;
        }

        public virtual Nullable<decimal> Price
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> ReturnDt
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

        public virtual BookTransactionH BookTransactionH
        {
            get { return _bookTransactionH; }
            set
            {
                if (!ReferenceEquals(_bookTransactionH, value))
                {
                    var previousValue = _bookTransactionH;
                    _bookTransactionH = value;
                    FixupBookTransactionH(previousValue);
                }
            }
        }
        private BookTransactionH _bookTransactionH;

        #endregion

        #region Association Fixup

        private void FixupBookTransactionH(BookTransactionH previousValue)
        {
            if (previousValue != null && previousValue.BookTransactionD.Contains(this))
            {
                previousValue.BookTransactionD.Remove(this);
            }

            if (BookTransactionH != null)
            {
                if (!BookTransactionH.BookTransactionD.Contains(this))
                {
                    BookTransactionH.BookTransactionD.Add(this);
                }
                if (BookTransactionHId != BookTransactionH.BookTransactionHId)
                {
                    BookTransactionHId = BookTransactionH.BookTransactionHId;
                }
            }
        }

        #endregion


    }
}
