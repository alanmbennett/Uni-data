using Microsoft.Win32;
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
    /// Interaction logic for DatabaseCreationWindow.xaml
    /// </summary>
    public partial class DatabaseCreationWindow : Window
    {
        MainWindow MainWin;
        public DatabaseCreationWindow(MainWindow mw)
        {
            MainWin = mw;
            InitializeComponent();
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            MainWin.SaveDatabase(DatabaseNameTextBox.Text);
            this.Close();
        }
    }
}
