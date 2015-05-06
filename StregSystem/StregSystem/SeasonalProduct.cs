using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class SeasonalProduct : Product
    {
        private DateTime? _seasonStartDate;
        private DateTime? _seasonEndDate;

        public DateTime SeasonEndDate
        {
            get { return (DateTime) _seasonEndDate; }
            set { _seasonEndDate = value; }
        }
        

        public DateTime SeasonStartDate
        {
            get { return (DateTime) _seasonStartDate; }
            set { _seasonStartDate = value; }
        }

        public SeasonalProduct(int id, string name, int price, bool active, bool canCredit)
            : base(id, name, price, active, canCredit)
        {
            //Calls base constructor
        }

        public SeasonalProduct(int id, string name, int price, bool active, bool canCredit, DateTime start, DateTime end) 
            : base(id, name, price, active, canCredit)
        {
            _seasonStartDate = start;
            _seasonEndDate = end;
        }
    }
}
