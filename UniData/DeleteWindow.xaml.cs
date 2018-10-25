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
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        DatabaseHelper.Input Input;

        public DeleteWindow(DatabaseHelper.Input input)
        {
            Input = input;
            InitializeComponent();

            if(Input == DatabaseHelper.Input.Columns)
            {
                TextboxLabel.Content = "Column Name ";
            }
            else if (Input == DatabaseHelper.Input.Rows)
            {
                TextboxLabel.Content = "Row ID ";
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(DeletionTextbox.Text))
                this.Close();
            else
            {
                ErrorMessage.Content = "Error: Unable to delete based on parameter given.";
                ErrorMessage.Visibility = Visibility.Visible; 
            }

        }

        private void UpdateErrorMessage(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DeletionTextbox.Text))
                ErrorMessage.Visibility = Visibility.Hidden;
        }




    }
}
