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
        private UserAccount User;
        private string fileFilter = "XML Files(*.xml)|*.xml";
        private string dbName;
        private SaveFileDialog saveDialog;
        private OpenFileDialog loadDialog;
        private DataTable database;
        private List<string> Columns; 

        public MainWindow(UserAccount user)
        {
            InitializeComponent();
            UserMenuItem.Header = $"Logged in as: {user}";
            User = user;
            dbName = null;
            database = new DataTable();
            Columns = new List<string>();

            DatabaseGrid.CanUserAddRows = false;
            DatabaseGrid.DataContext = database.DefaultView;
        }

        public MainWindow(UserAccount user, string loadPath)
        {
            InitializeComponent();
            UserMenuItem.Header = $"Logged in as: {user.Username}";
            User = user;  
            dbName = null;
            database = new DataTable();
   
            Columns = new List<string>();

            database.ReadXml(loadPath);

            this.Title = loadPath;

            foreach(DataColumn col in database.Columns)
            {
                Columns.Add(col.ColumnName);
            }
           
            DatabaseGrid.CanUserAddRows = false;
            DatabaseGrid.DataContext = database.DefaultView;
        }
		

        private void LoadDatabaseClick(object sender, RoutedEventArgs e)
        {
            loadDialog = new OpenFileDialog();
            loadDialog.Filter = fileFilter;
            loadDialog.FileOk += LoadDialogFileOK; // set event handler to FileOK event
            loadDialog.Title = "Load a Database";

            loadDialog.ShowDialog();
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
            saveDialog.FileOk += SaveDialogFileOK; // set event handler to FileOK event

            /* If file has been saved before, its name will populate the FileName box on SaveFileDialog*/
            if (name != null)
                saveDialog.FileName = $"{name}.xml";
            else if(name == null && dbName != null)
                saveDialog.FileName = $"{dbName}.xml";

            saveDialog.ShowDialog();
        }

        /* Method: SaveDialogFileOK
         * Description: Event handler for when user presses Save button on SaveFileDialog window
         */

        private void SaveDialogFileOK(object sender, CancelEventArgs e)
        {
            database.TableName = saveDialog.FileName;
            this.Title = database.TableName;
            database.WriteXml(database.TableName, XmlWriteMode.WriteSchema); // will write database to XML file specified in SaveFileDialog
        }

        /* Method: LoadDialogFileOK
         * Description: Event handler for when user presses Load button on OpenFileDialog window
         */

        private void LoadDialogFileOK(object sender, CancelEventArgs e)
        {
            try
            {
                MainWindow newMain = new MainWindow(User, loadDialog.FileName);
                this.Close();
                newMain.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Load Error");
            }
        }

        private void AddColumnClick(object sender, RoutedEventArgs e)
        {
            InputWindow inputWin = new InputWindow(DatabaseHelper.Input.Columns); // specifies adding column
            inputWin.ShowDialog();

            if(!string.IsNullOrWhiteSpace(inputWin.ColumnTextBox.Text))
            {
                database.Columns.Add(inputWin.ColumnTextBox.Text, typeof(string));
                Columns.Add(inputWin.ColumnTextBox.Text);
                GridRefresh();
            }
        }

        private void AddRowClick(object sender, RoutedEventArgs e)
        {
            if (Columns.Count != 0)
                database.Rows.Add();
            else
                MessageBox.Show("Cannot add rows to column-less table, please add a column first", "Invalid operation");
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
            DeleteWindow deleteWin = new DeleteWindow(DatabaseHelper.Input.Columns, Columns);
            deleteWin.ShowDialog();

            string toDelete = null;

            if (deleteWin.ColumnCombobox.SelectedItem != null)
                toDelete = deleteWin.ColumnCombobox.SelectedItem.ToString();

            if (!string.IsNullOrWhiteSpace(toDelete))
            {
                DeleteColumn(database.Columns[toDelete]);
                Columns.Remove(toDelete);

                /* Below code ensures that deleted column is no longer displayed in DataGrid */
                GridRefresh();
            }
        }

		private void SearchDatabaseClick(object sender, RoutedEventArgs e)
		{
			SearchWindow search = new SearchWindow(database);
			search.ShowDialog();
		}

		private void SortDataClick(object sender, RoutedEventArgs e)
		{
			SortingWindow sort = new SortingWindow(database);
			sort.ShowDialog();
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

        private void GridRefresh()
        {
            DatabaseGrid.DataContext = null;
            DatabaseGrid.DataContext = database.DefaultView;
        }

    }
}
