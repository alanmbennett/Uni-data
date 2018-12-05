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
    class CreateFillWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        CreateFillWindow Win;

        public bool cancel = true;

        private string _toFill;
        public string ToFill
        {
            get { return _toFill; }
            set
            {
                _toFill = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ToFill"));
            }
        }

        private string _columnToCreate;
        public string ColumnToCreate
        {
            get { return _columnToCreate; }
            set
            {
                _columnToCreate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ColumnToCreate"));
            }
        }

        public CreateFillWindowVM(CreateFillWindow win)
        {
            Win = win;
        }

        DelegateCommand _createFillEvent;
        public ICommand CreateFillCommand
        {
            get
            {
                if (_createFillEvent == null)
                {
                    _createFillEvent = new DelegateCommand(CreateFillClick);
                }

                return _createFillEvent;
            }
        }

        private void CreateFillClick(object sender)
        {
            cancel = false;
            try
            {
                if (string.IsNullOrEmpty(ToFill))
                    throw new Exception("Please fill in a value to fill column with.");
                else if (string.IsNullOrEmpty(ColumnToCreate))
                    throw new Exception("Please fill in a name for column to be created.");

                Win.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occurred!");
            }
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
