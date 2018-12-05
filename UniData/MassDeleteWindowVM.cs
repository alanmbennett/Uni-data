using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UniData
{
    class MassDeleteWindowVM : INotifyPropertyChanged
    {
        MassDeleteWindow Win;
        public bool cancel = true;

        private string _toDelete;
        public string ToDelete
        {
            get { return _toDelete; }
            set
            {
                _toDelete = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ToDelete"));
            }
        }

        private string _selectedColumn;
        public string SelectedColumn
        {
            get { return _selectedColumn; }
            set
            {
                _selectedColumn = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedColumn"));
            }
        }

        private List<string> _columns;
        public List<string> Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MassDeleteWindowVM(List<string> columns, MassDeleteWindow win)
        {
            Columns = columns;
            Win = win;
            if (Columns.Count > 0)
                SelectedColumn = Columns[0];
        }

        DelegateCommand _deleteEvent;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteEvent == null)
                {
                    _deleteEvent = new DelegateCommand(DeleteClick);
                }

                return _deleteEvent;
            }
        }

        private void DeleteClick(object sender)
        { 
            cancel = false;
            if (Columns.Count == 0)
            {
                cancel = true;
                MessageBox.Show("No columns to select, please add some.", "An error has occurred!");
            }
            Win.Close();
        }

        DelegateCommand _cancelEvent;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelEvent == null)
                {
                    _cancelEvent = new DelegateCommand(CancelClick);
                }

                return _cancelEvent;
            }
        }

        private void CancelClick(object sender)
        {
            cancel = true;
            Win.Close();
        }
    }


}
