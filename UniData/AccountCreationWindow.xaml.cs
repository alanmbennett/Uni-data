using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Net.Mail;

namespace UniData
{
    /// <summary>
    /// Interaction logic for AccountCreationWindow.xaml
    /// </summary>
    public partial class AccountCreationWindow : Window
    {

		private const string userFilePath = "login.xml";
		private XmlSerializer Serializer = new XmlSerializer(typeof(List<UserAccount>));
		private List<UserAccount> userList;

		public AccountCreationWindow(List<UserAccount> userAccounts)
        {
            InitializeComponent();
			userList = userAccounts;
        }
		//write the account info to the login.xml
		private void WriteFile()
		{
			using (FileStream filestream = new FileStream(userFilePath, FileMode.Create, FileAccess.Write))
			{
				Serializer.Serialize(filestream, userList);
			}
		}
		//cancel account creation
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		//when submitted the input validation check with the account added
        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
			//validate input
			if (InputValidation())
			{
				//create user and exit
				userList.Add(new UserAccount(FirstNameTextBox.Text, LastNameTextBox.Text, UsernameTextBox.Text, EmailTextBox.Text, UserPasswordBox.Password));
				WriteFile();
				this.Close();
			}
			else
			{
				//trigger what input is invalid
				//MessageBox.Show("Input is invalid");
			}
        }


		private bool InputValidation()
		{
			bool output = true;

			//column one
			//user name
			if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
			{
				output = false;
				UsernameErrorMsg.Visibility = Visibility.Visible;
			}else if (userList.Any(x => x.Username == UsernameTextBox.Text))//Checks if the user already exists
			{
				//check if user accounnt exists already
				output = false;
				UsernameErrorMsg.Visibility = Visibility.Visible;
				MessageBox.Show("User exists in the System Use a different Username or login with that Username");
			}
			//first password check
			if (string.IsNullOrWhiteSpace(UserPasswordBox.Password))
			{
				output = false;
				PasswordErrorMsg.Visibility = Visibility.Visible;
			}//second password check
			if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
			{
				output = false;
				ConfirmPWErrorMsg.Visibility = Visibility.Visible;
			}else if(UserPasswordBox.Password != ConfirmPasswordBox.Password)
			{
				//matching passwords check
				output = false;
				ConfirmPWErrorMsg.Visibility = Visibility.Visible;
				MessageBox.Show("Passwords Do not Match");
			}
			//column two
			//first name check
			if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
			{
				output = false;
				FirstNameErrorMsg.Visibility = Visibility.Visible;
			}
			//last name check
			if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
			{
				output = false;
				LastNameErrorMsg.Visibility = Visibility.Visible;
			}
			//email check
			try
			{
				MailAddress t = new MailAddress(EmailTextBox.Text);
			}
			catch{
				output = false;
				EmailErrorMsg.Visibility = Visibility.Visible;
			}
			return output;

		}

		//Error reseting
		private void UsernameTextBoxFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			UsernameErrorMsg.Visibility = Visibility.Hidden;
		}

		private void UserPasswordBoxFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			PasswordErrorMsg.Visibility = Visibility.Hidden;
		}

		private void PasswordValidationFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			ConfirmPWErrorMsg.Visibility = Visibility.Hidden;
		}

		//column 2 error resetting
		private void FirstNameTextBoxFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			FirstNameErrorMsg.Visibility = Visibility.Hidden;
		}

		private void LastNameTextBoxFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			LastNameErrorMsg.Visibility = Visibility.Hidden;
		}

		private void EmailTextBoxFocus(object sender, DependencyPropertyChangedEventArgs e)
		{
			EmailErrorMsg.Visibility = Visibility.Hidden;
		}



	}
}
