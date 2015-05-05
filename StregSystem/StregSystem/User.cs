using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class User
    {
        private int _userID;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;

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
        
    }
}
