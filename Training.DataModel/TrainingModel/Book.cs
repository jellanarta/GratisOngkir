using AdIns.DataModel.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.DataModel.TrainingModel
{
    public partial class Book : ITimestampable
    {
        #region Constructor
        public Book() { }

        public Book(long RefBookId)
        {
            this.RefBookId = RefBookId;
        }


        # endregion Primitive Properties

        public virtual long RefBookId
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }

        public virtual string Author
        {
            get;
            set;
        }

        public virtual int Year
        {
            get;
            set;
        }

        public virtual int Stock
        {
            get;
            set;
        }

        public virtual decimal Price
        {
            get;
            set;
        }


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
