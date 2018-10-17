using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace UniData
{
    public class UserAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public MailAddress Email { get; set; }
        public SecureString Password { get; set; }  // SecureString for password security

        UserAccount() { }

        public UserAccount(string firstName, string lastName, string username, string email, SecureString password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = new MailAddress(email); // throws exceptions if not valid email address so surround constructor calls in try-catches
            Password = password;
        }
    }
}
