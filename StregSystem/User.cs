using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class User : IComparable
    {
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
            set { _balance = value; }
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

        public User(int id, string firstname, string lastname, string email, int bal)
        {
            _userID = id;
            _firstname = firstname;
            _lastname = lastname;
            //TODO Gyldighed
            _email = email;
            _balance = bal;
            _username = GenerateUsername(lastname, firstname, id);
        }

        public override string ToString()
        {
            return Firstname + ", " + Email;
        }

        public int CompareTo(Object obj)
        {
            if (obj == null)
                return 1;

            User otherUser = obj as User;
            if (otherUser != null)
                return this.UserID.CompareTo(otherUser.UserID);
            else
                throw new ArgumentException("Object is not of type 'User'");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
                return false;
            else
                return UserID == ((User)obj).UserID;
        }

        public override int GetHashCode()
        {
            //8 is an arbitrary number, but it is the same for every instance of user, which is the important part.
            return this.UserID.GetHashCode() * 16;
        }

        private string GenerateUsername(string nameOne, string nameTwo, int num)
        {
            return nameOne + num.ToString() + nameTwo[0];
        }

    }
}