using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UniData
{
    [XmlRoot(ElementName = "UserAccount")]
    public class UserAccount
    {
        [XmlAttribute(DataType ="string")]
        public string FirstName { get; set; }
        [XmlAttribute(DataType = "string")]
        public string LastName { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Username { get; set; }
        [XmlAttribute(DataType = "string")]
        public MailAddress Email { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Password { get; set; } 

        UserAccount() { }

        public UserAccount(string firstName, string lastName, string username, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = new MailAddress(email); // throws exceptions if not valid email address so surround constructor calls in try-catches
            Password = password;
        }
    }
}
