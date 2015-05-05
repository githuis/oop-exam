using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class User : IComparable
    {
        //Test
        private int _userID;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;
        private int _balance;

        #region Properties
        
        public int Balance
        {
            get { return _balance; }
        }
        
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        
        public string Username
        {
            get { return _username; }
        }
        
        public string Lastname
        {
            get { return _lastname; }
        }
        
        public string Firstname
        {
            get { return _firstname; }
        }

        public int UserID
        {
            get { return _userID; }
        }
        #endregion

        public User()
        {

        }

        public override string ToString()
        {
            return Firstname + ", " + Email;
        }

        public override int CompareTo(Object obj)
        {
            //TODO
            return 1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
                return false;
            else
                return UserID == ((User)obj).UserID;
        }

        


    }
}
