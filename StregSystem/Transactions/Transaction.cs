using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    abstract class Transaction : IComparable
    {
        private int _transactionID;
        private User _transUser;
        private DateTime _date;
        private double _amount;

        #region Properties
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        internal User TransUser
        {
            get { return _transUser; }
            set { _transUser = value; }
        }
        
        public int TransactionID
        {
            get { return _transactionID; }
            set { _transactionID = value; }
        }
        #endregion

        public Transaction(int id, User user, DateTime date, double amount)
        {
            _transactionID = id;
            _transUser = user;
            _date = date;
            _amount = amount;
        }

        public abstract void Execute();
     

        //Consider Interface for objects able to log
        public void LogTransaction(string logEntry, System.IO.StreamWriter w)
        {
            w.WriteLine("*///////////////////////////////");
            w.WriteLine("Transaction:\n");
            w.WriteLine(Date.ToShortDateString() + " " + Date.ToShortTimeString());
            w.WriteLine(logEntry);
            w.WriteLine("");
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            Transaction otherTrans = obj as Transaction;
            if (otherTrans != null)
                return this.TransactionID.CompareTo(otherTrans.TransactionID);
            else
                throw new ArgumentException("Object is not of type 'Transction'");
        }
    }
}
