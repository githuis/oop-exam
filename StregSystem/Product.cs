using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class Product
    {
        private int _productID;
        private string _name;
        private double _price;
        private bool _active;
        private bool _canBeBoughtOnCredit;

        #region Properties
        public bool CanBeBoughtOnCredit
        {
            get { return _canBeBoughtOnCredit; }
            set { _canBeBoughtOnCredit = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set 
            {
                if (value != null)
                    _name = value;
            }
        }
        #endregion

        public int ProductID
        {
            get { return _productID; }
        }

        public Product(int id, string name, double price, bool active, bool canCredit)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException("Product ID must be higher than 0!");
            if (name == null)
                throw new ArgumentNullException("Product name cannot be null");

            _productID = id;
            _name = name;
            _price = price;
            _active = active;
            _canBeBoughtOnCredit = canCredit;

        }

        public override string ToString()
        {
            return ProductID + " - " + Name;
        }
    }
}
