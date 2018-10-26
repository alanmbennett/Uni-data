using System;
using System.Collections.Generic;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
		public DataTable database;
		public List<string> column = new List<string>();

		public SearchWindow(DataTable input)
		{
            InitializeComponent();
			database = input;

			foreach(DataColumn colum in database.Columns)
			{
				column.Add(colum.ColumnName);
			}
			Selection1Combo.ItemsSource = column;
			Selection2Combo.ItemsSource = column;

			DatabaseGrid.DataContext = database.DefaultView;
		}


		private void SubmitClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
