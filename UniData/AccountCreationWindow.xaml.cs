using System;
using System.Collections.Generic;
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

namespace UniData
{
    /// <summary>
    /// Interaction logic for AccountCreationWindow.xaml
    /// </summary>
    public partial class AccountCreationWindow : Window
    {

		private List<UserAccount> userList;

		public AccountCreationWindow(List<UserAccount> userAccounts)
        {
            InitializeComponent();
			userList = userAccounts;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
			//validate input
			if (InputValidation())
			{

				//validate if account exists
				//create user and exit
			}
			else
			{
				//trigger what input is invalid
				MessageBox.Show("Input is invalid");
			}
        }

		private bool InputValidation()
		{
			bool output = true;

			//column one
			if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
			{
				output = false;
				UsernameErrorMsg.Visibility = Visibility.Visible;
			}
			if (string.IsNullOrWhiteSpace(UserPasswordBox.Password))
			{
				output = false;
				PasswordErrorMsg.Visibility = Visibility.Visible;
			}
			if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
			{
				output = false;
				ConfirmPWErrorMsg.Visibility = Visibility.Visible;
			}else if(UserPasswordBox.Password != ConfirmPasswordBox.Password)
			{
				output = false;
				ConfirmPWErrorMsg.Visibility = Visibility.Visible;
				MessageBox.Show("Passwords Do not Match");
			}
			//column two
			if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
			{
				output = false;
				FirstNameErrorMsg.Visibility = Visibility.Visible;
			}
			if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
			{
				output = false;
				LastNameErrorMsg.Visibility = Visibility.Visible;
			}
			if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
			{
				output = false;
				EmailErrorMsg.Visibility = Visibility.Visible;
			}


			return output;

		}

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

		//col 2
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
