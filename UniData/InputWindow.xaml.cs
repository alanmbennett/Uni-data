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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    /// 

    public partial class InputWindow : Window
    {
        DatabaseHelper.Input Input;
        public bool cancelClicked;

        public InputWindow(DatabaseHelper.Input input)
        {
            Input = input;
            cancelClicked = false;
            InitializeComponent();
        }

        public void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (Input == DatabaseHelper.Input.Columns && !string.IsNullOrWhiteSpace(ColumnTextBox.Text))
                this.Close();
            else
                MessageBox.Show("An error has occurred", "Cannot add an empty column");

        }

        public void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            cancelClicked = true;
            this.Close();
        }




    }
}
