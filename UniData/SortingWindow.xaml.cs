using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for SortingWindow.xaml
    /// </summary>
    public partial class SortingWindow : Window
    {

		public DataTable database;
		public List<string> column = new List<string>();


		public SortingWindow(DataTable input)
        {

            InitializeComponent();
			//set the database, and set the combo boxes with the column names
			database = input;
			foreach (DataColumn colum in database.Columns)
			{
				column.Add(colum.ColumnName);
			}
			Selection1Combo.ItemsSource = column;
			Selection2Combo.ItemsSource = column;

			DatabaseGrid.DataContext = database.DefaultView;

		}

		//trigger once submitted
		private void SubmitClick(object sender, RoutedEventArgs e)
		{
			//Validate if the combo boxes are selected
			if (Selection1Combo.SelectedIndex > -1)
			{
				if(Selection2Combo.SelectedIndex > -1)
				{
					//check if it is sorted in ascending order
					if (ascending.IsChecked == true)
					{
						database = database.Select().OrderBy(x => x[Selection1Combo.SelectedIndex]).ThenBy(x=> x[Selection2Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
					//descending order
					else
					{
						database = database.Select().OrderByDescending(x => x[Selection1Combo.SelectedIndex]).ThenByDescending(x => x[Selection2Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
				}
				//if there is only one selected criteria
				else
				{
					//ascending order
					if (ascending.IsChecked == true)
					{
						database = database.Select().OrderBy(x => x[Selection1Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
					//desending order
					else
					{
						database = database.Select().OrderByDescending(x => x[Selection1Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
				}
			}
			else
			{
				MessageBox.Show("Please select the first criteria that you want to sort by");
			}
		}

		//reset the combo boxes and radio buttons to defaults
		private void ResetClick(object sender, RoutedEventArgs e)
		{
			Selection1Combo.SelectedIndex = -1;
			Selection2Combo.SelectedIndex = -1;
			ascending.IsChecked = true;
		}

	}
}
