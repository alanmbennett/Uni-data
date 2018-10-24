using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace UniData
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string userFilePath = "login.xml";
        private XmlSerializer Serializer = new XmlSerializer(typeof(List<UserAccount>));
        private List<UserAccount> userList; 

        public LoginWindow()
        {
            ReadUsersFromFile(); // read users from XML file
            InitializeComponent();
        }

        public LoginWindow(string lastUsername)
        {
            ReadUsersFromFile(); // read users from XML file
            InitializeComponent();
            UsernameTextBox.Text = lastUsername;
        }

        private void CreateAccountButtonClick(object sender, RoutedEventArgs e)
        {
            AccountCreationWindow accWin = new AccountCreationWindow(userList);
            accWin.ShowDialog();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            /* Insert code here that checks login credentials before opening the MainWindow*/
            // Hard-coded test user is tentative code so program doesn't crash for now
            UserAccount currentUser;

            try
            {
                currentUser = userList.Find(u => u.Username == UsernameTextBox.Text && u.Password == UserPaswordBox.Password);
                MainWindow main = new MainWindow(new UserAccount("Test", "Tester", "Testing21", "testing@gmail.com", "password"));
                this.Close();
                main.ShowDialog();
            }
            catch(Exception ex)
            {

				Console.WriteLine(ex);

            }
        }

        /* Method: ReadUsersFromFile
         * Parameters: n/a
         * Return: void
         * Description: Reads users from file and adds them to userList, will intialize userList if file does not exists yet,
         *              will also create login.xml for future reference.
         */

        private void ReadUsersFromFile()
        {
			//validate if the user file exists
            if (File.Exists(userFilePath))
			{
				//read in the file if it exists
				using (FileStream filestream = new FileStream(userFilePath, FileMode.Open, FileAccess.Read))
				{
					userList = Serializer.Deserialize(filestream) as List<UserAccount>;
				}
			}else
			{
				//generate a empty user list
				userList = new List<UserAccount>();
			}

		}
    }
}
