using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow(UserAccount user)
        { 
            InitializeComponent();
            UserMenuItem.Header = $"Logged in as: {user.Username}";

        }

        private void AddToDatabaseClick(object sender, RoutedEventArgs e)
        {
            InputWindow inputWin = new InputWindow();
            inputWin.ShowDialog();
        }

        private void LoadDatabaseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "XML Files(*.xml)|*.xml";
            
            if(fileDialog.ShowDialog() == true)
            {
                /*using (FileStream readStream = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read))
                {

                }*/
            }
        }
    }
}
