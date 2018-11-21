using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UniData
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private string FileFilter = "XML Files(*.xml)|*.xml";
        private SaveFileDialog SaveDialog;
        private OpenFileDialog LoadDialog;
        
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MainWindow MainWin;

        private DataTable _database;
        public DataTable Database
        {
            get { return _database; }
            set
            {
                _database = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Database"));
            }
        }

        private UserAccount _user;
        public UserAccount User
        {
            get { return _user; }
            set
            {
                _user = value;
                PropertyChanged(this, new PropertyChangedEventArgs("User"));
            }
        }

        private string _dbName;
        public string DbName
        {
            get { return _dbName; }
            set
            {
                _dbName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DbName"));
            }
        }

        private DataRowView _selectedRow;
        public DataRowView SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedRow"));
            }
        }

        private DataView _databaseView;
        public DataView DatabaseView
        {
            get { return _databaseView; }
            set
            {
                _databaseView = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DatabaseView"));
            }
        }

        private List<string> Columns;

        public MainWindowVM(UserAccount user)
        {
            //UserMenuItem.Header = $"Logged in as: {User}";
            User = user;
            DbName = "Blank Database";

            if(Database == null)
                Database = new DataTable();

            Columns = new List<string>();
        }

        public MainWindowVM(UserAccount user, string loadPath) : this(user)
        { 
            ///* Load DataTable from given XML document */
            DataSet dataset = new DataSet();
            dataset.ReadXml(loadPath, XmlReadMode.ReadSchema);
            Database = dataset.Tables[0];

            DbName = loadPath;

            ///* Copy column info into Columns List */
            foreach (DataColumn col in Database.Columns)
            {
                Columns.Add(col.ColumnName);
            }

            DBRefresh();
        }

        DelegateCommand _loadDatabaseEvent;
        public ICommand LoadDatabaseCommand
        {
            get
            {
                if (_loadDatabaseEvent == null)
                {
                    _loadDatabaseEvent = new DelegateCommand(LoadDatabaseClick);
                }

                return _loadDatabaseEvent;
            }
        }

        private void LoadDatabaseClick(object sender)
        {
            LoadDialog = new OpenFileDialog();
            LoadDialog.Filter = FileFilter;
            LoadDialog.FileOk += LoadDialogFileOK; // set event handler to FileOK event
            LoadDialog.Title = "Load a Database";

            LoadDialog.ShowDialog();
        }

        DelegateCommand _createDatabaseEvent;
        public ICommand CreateDatabaseCommand
        {
            get
            {
                if (_createDatabaseEvent == null)
                {
                    _createDatabaseEvent = new DelegateCommand(CreateDatabaseClick);
                }

                return _createDatabaseEvent;
            }
        }

        private void CreateDatabaseClick(object sender)
        {
            DatabaseCreationWindow createWin = new DatabaseCreationWindow();
            createWin.ShowDialog();

            if (!createWin.cancelClicked && !string.IsNullOrEmpty(createWin.DatabaseNameTextBox.Text))
            { 
               /* Save new intiail database to a XML file and open it up in a brand new window*/
               SaveDatabase(createWin.DatabaseNameTextBox.Text);
               MainWindow mw = new MainWindow(User);
               mw.Title = SaveDialog.FileName;
               MainWin.Close();
               mw.ShowDialog();
            }
        }

        DelegateCommand _saveDatabaseEvent;
        public ICommand SaveDatabaseCommand
        {
            get
            {
                if (_saveDatabaseEvent == null)
                {
                    _saveDatabaseEvent = new DelegateCommand(SaveDatabaseClick);
                }

                return _saveDatabaseEvent;
            }
        }

        private void SaveDatabaseClick(object sender)
        {
            SaveDatabase();
        }

        public void SaveDatabase(string name = null)
        {
            SaveDialog = new SaveFileDialog();
            SaveDialog.Filter = FileFilter;
            SaveDialog.Title = "Save a Database";
            SaveDialog.FileOk += SaveDialogFileOK; // set event handler to FileOK event

            ///* If file has been saved before, its name will populate the FileName box on SaveFileDialog*/
            if (name != null)
               SaveDialog.FileName = $"{name}.xml";
            else if (name == null && DbName != null)
               SaveDialog.FileName = $"{DbName}.xml";

            SaveDialog.ShowDialog();
        }

        /* Method: SaveDialogFileOK
         * Description: Event handler for when user presses Save button on SaveFileDialog window
         */

        private void SaveDialogFileOK(object sender, CancelEventArgs e)
        {
            Database.TableName = SaveDialog.FileName;
            DbName = Database.TableName;
            Database.WriteXml(Database.TableName, XmlWriteMode.WriteSchema); // will write database to XML file specified in SaveFileDialog
        }

        /* Method: LoadDialogFileOK
         * Description: Event handler for when user presses Load button on OpenFileDialog window
         */

        private void LoadDialogFileOK(object sender, CancelEventArgs e)
        {
            try
            {
                MainWindow newMain = new MainWindow(User, LoadDialog.FileName);
                MainWin.Close();
                newMain.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Load Error");
            }
        }

        DelegateCommand _addColumnEvent;
        public ICommand AddColumnCommand
        {
            get
            {
                if (_addColumnEvent == null)
                {
                    _addColumnEvent = new DelegateCommand(AddColumnClick);
                }

                return _addColumnEvent;
            }
        }

        private void AddColumnClick(object sender)
        {
            InputWindow inputWin = new InputWindow(DatabaseHelper.Input.Columns); // specifies adding column
            inputWin.ShowDialog();

            if (!inputWin.cancelClicked && !string.IsNullOrWhiteSpace(inputWin.ColumnTextBox.Text))
            {
                try
                {
                    Database.Columns.Add(inputWin.ColumnTextBox.Text, typeof(string));
                    Columns.Add(inputWin.ColumnTextBox.Text);
                    DBRefresh(); 
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error has occurred!");
                }
            }
        }

        DelegateCommand _addRowEvent;
        public ICommand AddRowCommand
        {
            get
            {
                if (_addRowEvent == null)
                {
                    _addRowEvent = new DelegateCommand(AddRowClick);
                }

                return _addRowEvent;
            }
        }

        public void AddRowClick(object sender)
        {
            if (Columns.Count > 0) // Don't allow user to add row until database has at least one columns
            {
               Database.Rows.Add();
            }
            else
               MessageBox.Show("Cannot add rows to column-less table, please add a column first", "Invalid operation");
        }

        DelegateCommand _deleteRowEvent;
        public ICommand DeleteRowCommand
        {
            get
            {
                if (_deleteRowEvent == null)
                {
                    _deleteRowEvent = new DelegateCommand(DeleteRowClick);
                }

                return _deleteRowEvent;
            }
        }

        private void DeleteRowClick(object sender)
        {
            try
            {
                DeleteRow(SelectedRow.Row); // note: SelectedRow binding not updating so DeleteRowClick will throw an exception
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
                Database.Rows.Remove(toDelete);
                DBRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured!");
            }
        }

        DelegateCommand _deleteColumnEvent;
        public ICommand DeleteColumnCommand
        {
            get
            {
                if (_deleteColumnEvent == null)
                {
                    _deleteColumnEvent = new DelegateCommand(DeleteColumnClick);
                }

                return _deleteColumnEvent;
            }
        }

        private void DeleteColumnClick(object sender)
        {
            DeleteWindow deleteWin = new DeleteWindow(DatabaseHelper.Input.Columns, Columns);
            deleteWin.ShowDialog();

            string toDelete = null;

            if (deleteWin.ColumnCombobox.SelectedItem != null)
                toDelete = deleteWin.ColumnCombobox.SelectedItem.ToString(); // sets toDelete to seleted item in combobox

            if (!deleteWin.cancelClicked && !string.IsNullOrWhiteSpace(toDelete))
            {
                /* Delete column from database */
                DeleteColumn(Database.Columns[toDelete]);
                Columns.Remove(toDelete);

                /* Below code ensures that deleted column is no longer displayed in DataGrid */
                DBRefresh();
            }
        }

        DelegateCommand _searchDatabaseEvent;
        public ICommand SearchDatabaseCommand
        {
            get
            {
                if (_searchDatabaseEvent == null)
                {
                    _searchDatabaseEvent = new DelegateCommand(SearchDatabaseClick);
                }

                return _searchDatabaseEvent;
            }
        }

        private void SearchDatabaseClick(object sender)
        {
            ////confirm that the database has no pending changes
            Database.AcceptChanges();
            ////open the search Window
            SearchWindow search = new SearchWindow(Database);
            search.ShowDialog();
        }

        DelegateCommand _sortDataEvent;
        public ICommand SortDataCommand
        {
            get
            {
                if (_sortDataEvent == null)
                {
                    _sortDataEvent = new DelegateCommand(SortDataClick);
                }

                return _sortDataEvent;
            }
        }

        private void SortDataClick(object sender)
        {
            ////confirm that the database has no pending changes
            Database.AcceptChanges();
            ////open the sorting Window
            SortingWindow sort = new SortingWindow(Database);
            sort.ShowDialog();

        }

        private void DeleteColumn(DataColumn toDelete)
        {
            try
            {
                Database.Columns.Remove(toDelete);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured!");
                
            }
        }

        DelegateCommand _logoutEvent;
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutEvent == null)
                {
                    _logoutEvent = new DelegateCommand(LogoutClick);
                }

                return _logoutEvent;
            }
        }

        private void LogoutClick(object sender)
        {
            LoginWindow loginWin = new LoginWindow(User.Username);
            MainWin.Close();
            loginWin.ShowDialog();
        }

        private void DBRefresh()
        {
            DatabaseView = null;
            DatabaseView = Database.DefaultView;
        }  

    }
}
