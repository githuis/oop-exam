using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StregSystemProject
{
    class User : IComparable
    {
        private int _userID;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;
        private double _balance;

        public static List<User> All = new List<User>();

        #region Properties
        public double Balance
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

        public User(string firstname, string lastname, string email)
        {
            if (ValidEmail(email))
                _email = email;
            else
                throw new ArgumentException("Email is not valid");

            if (ValidUsername(GenerateUsername(lastname, UserID)))
                _username = GenerateUsername(lastname, UserID);
            else
                throw new ArgumentException("Invalid username Generated");

            if (firstname == null || lastname == null)
                throw new ArgumentNullException("User firstname and/or lastname cannot be null");

            _userID = All.Count();
            _firstname = firstname;
            _lastname = lastname;
            _balance = 0;
            
            User.All.Add(this);
        }

        public User(string firstname, string lastname, string email, int bal)
        {
            if (ValidEmail(email))
                _email = email;
            else
                throw new ArgumentException("Email is not valid");

            if (ValidUsername(GenerateUsername(lastname, UserID)))
                _username = GenerateUsername(lastname, UserID);
            else
                throw new ArgumentException("Invalid username Generated");

            if (firstname == null || lastname == null)
                throw new ArgumentNullException("User firstname and/or lastname cannot be null");

            _userID = All.Count();
            _firstname = firstname;
            _lastname = lastname;
            _balance = bal;

            User.All.Add(this);
        }

        public override string ToString()
        {
            return Firstname + " " + Lastname + " " + Email;
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
            return this.UserID.GetHashCode() * 16 + this.Username.GetHashCode();
        }

        private string GenerateUsername(string name, int num)
        {
            return name.ToLower() + num.ToString();
        }

        private bool ValidEmail(string mail)
        {
            bool local = false, domain = false;
            string[] split = mail.Split('@');
            if (split.Length != 2)
                return false;

            //Local check
            Regex check = new Regex("[a-zA-Z0-9-_.]");
            local = check.IsMatch(split[0]);

            //Domain check
            check = new Regex("[a-zA-Z0-9-._]");
            domain = check.IsMatch(split[1]) && split[1].Contains('.') 
                && !CharEquals(split[1][0], '.', '-', '_') && !CharEquals(split[1][split[1].Length-1], '.', '-', '_');
            
            return (local && domain);
        }

        private bool CharEquals(char compareTo, params char[] chars)
        {
            foreach (char ch in chars)
	        {
		        if(ch == compareTo)
                    return true;
	        }
            return false;
        }

        private bool ValidUsername(string username)
        {
            Regex check = new Regex("[a-z0-9_]");
            return check.IsMatch(username);
        }

    }
}