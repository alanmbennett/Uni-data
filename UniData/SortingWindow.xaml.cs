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

			database = input;
			foreach (DataColumn colum in database.Columns)
			{
				column.Add(colum.ColumnName);
			}
			Selection1Combo.ItemsSource = column;
			Selection2Combo.ItemsSource = column;

			DatabaseGrid.DataContext = database.DefaultView;

		}

		private void SubmitClick(object sender, RoutedEventArgs e)
		{

			if (Selection1Combo.SelectedIndex > -1)
			{
				if(Selection2Combo.SelectedIndex > -1)
				{
					if (ascending.IsChecked == true)
					{
						database = database.Select().OrderBy(x => x[Selection1Combo.SelectedIndex]).ThenBy(x=> x[Selection2Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
					else
					{
						database = database.Select().OrderByDescending(x => x[Selection1Combo.SelectedIndex]).ThenByDescending(x => x[Selection2Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
				}
				else
				{
					if (ascending.IsChecked == true)
					{
						database = database.Select().OrderBy(x => x[Selection1Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
					else
					{
						database = database.Select().OrderByDescending(x => x[Selection1Combo.SelectedIndex]).CopyToDataTable();
						DatabaseGrid.DataContext = null;
						DatabaseGrid.DataContext = database.DefaultView;
					}
				}
			}
		}

		private void ResetClick(object sender, RoutedEventArgs e)
		{
			Selection1Combo.SelectedIndex = -1;
			Selection2Combo.SelectedIndex = -1;
			ascending.IsChecked = true;
		}

	}
}
