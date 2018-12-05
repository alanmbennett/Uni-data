using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data;
using System.Xml;

namespace UniData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserAccount User; 

        public MainWindow(UserAccount user)
        {
            User = user;
            MainWindowVM mainWindow = new MainWindowVM(user);
            mainWindow.MainWin = this;
            InitializeComponent();
            DataContext = mainWindow;
        }

        public MainWindow(UserAccount user, string loadPath)
        {
            User = user;
            MainWindowVM mainWindow = new MainWindowVM(user, loadPath);
            mainWindow.MainWin = this;
            InitializeComponent();
            DataContext = mainWindow;
        }
    }
}
