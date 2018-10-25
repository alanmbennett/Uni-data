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

namespace UniData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserAccount User;
        private string fileFilter = "XML Files(*.xml)|*.xml";
        private string dbName;
        private SaveFileDialog saveDialog;
        private DataTable database;

        public MainWindow(UserAccount user)
        {
            InitializeComponent();
            UserMenuItem.Header = $"Logged in as: {user.Username}";
            User = user;
            dbName = null;
            database = new DataTable();

            // Test data
            database.Columns.Add("First Name", typeof(string));
            database.Columns.Add("Last Name", typeof(string));
            database.Columns.Add("Age", typeof(int));

            database.Rows.Add("Alan", "Bennett", 24);

            DatabaseGrid.CanUserAddRows = false;
            DatabaseGrid.DataContext = database.DefaultView;
        }

        public MainWindow(UserAccount user, DataTable data)
        {
            InitializeComponent();
            UserMenuItem.Header = $"Logged in as: {user.Username}";
            User = user;
            dbName = null;
            database = data;

            DatabaseGrid.CanUserAddRows = false;
            DatabaseGrid.DataContext = database.DefaultView;
        }


        private void LoadDatabaseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = fileFilter;
    
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWin = new LoginWindow(User.Username);
            this.Close();
            loginWin.ShowDialog();
        }

        private void CreateDatabaseClick(object sender, RoutedEventArgs e)
        {
            DatabaseCreationWindow createWin = new DatabaseCreationWindow(this);
            createWin.ShowDialog();
        }

        private void SaveDatabaseClick(object sender, RoutedEventArgs e)
        {
            SaveDatabase();
        }

        public void SaveDatabase(string name = null)
        {
            saveDialog = new SaveFileDialog();
            saveDialog.Filter = fileFilter;
            saveDialog.Title = "Save a Database";
            if(name != null)
                saveDialog.FileName = $"{name}.xml";
            else if(name == null && dbName != null)
                saveDialog.FileName = $"{dbName}.xml";

            saveDialog.ShowDialog();
        }

        /* Method: SaveDialogFileOK
         * Description: Event handler for when user presses Save button on SaveFileDialog window
         * CURRENTLY DOES NOT WORK CORRECTLY
         */

        private void SaveDialogFileOK(object sender, CancelEventArgs e)
        {
            this.dbName = saveDialog.FileName;
            this.Title = dbName;
            database.WriteXml(dbName); // will write database to XML file specified in SaveFileDialog
        }

        private void AddColumnClick(object sender, RoutedEventArgs e)
        {
            InputWindow inputWin = new InputWindow(DatabaseHelper.Input.Columns); // specifies adding column
            inputWin.ShowDialog();
        }

        private void AddRowClick(object sender, RoutedEventArgs e)
        {
            InputWindow inputWin = new InputWindow(DatabaseHelper.Input.Rows); // specifies adding row
            inputWin.ShowDialog();
        }

        private void DeleteRowClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteRow((DatabaseGrid.SelectedItem as DataRowView).Row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured!");
            }
        }

        private void DeleteRow(DataRow toDelete)
        {
            try
            {
                database.Rows.Remove(toDelete);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured!");
            }
        }

        private void DeleteColumnClick(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteWin = new DeleteWindow(DatabaseHelper.Input.Columns);
            deleteWin.ShowDialog();

            if(!string.IsNullOrWhiteSpace(deleteWin.DeletionTextbox.Text))
            {
                DeleteColumn(database.Columns[deleteWin.DeletionTextbox.Text]);

                /* Below code ensures that deleted column is no longer displayed in DataGrid */
                DatabaseGrid.DataContext = null;
                DatabaseGrid.DataContext = database.DefaultView;
            }
        }

        private void DeleteColumn(DataColumn toDelete)
        {
            try
            {
                database.Columns.Remove(toDelete);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured!");
            }
        }

    }
}
