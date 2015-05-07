using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class SeasonalProduct : Product
    {
        private DateTime? _seasonStartDate;
        private DateTime? _seasonEndDate;

        #region Properties
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
        #endregion

        public SeasonalProduct(int id, string name, int price, bool active, bool canCredit)
            : base(id, name, price, active, canCredit)
        {
            //Calls procuct (base) constructor only
        }

        public SeasonalProduct(int id, string name, int price, bool active, bool canCredit, DateTime end)
            : base(id, name, price, active, canCredit)
        {
            _seasonEndDate = end;
        }


        public SeasonalProduct(int id, string name, int price, bool active, bool canCredit, DateTime start, DateTime end) 
            : base(id, name, price, active, canCredit)
        {
            _seasonStartDate = start;
            _seasonEndDate = end;
        }
    }
}
