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
		public DataTable search;
		public SearchWindow(DataTable input)
		{

			InitializeComponent();
			//set up the database and the temporary copy
			database = input;
			search = input.Copy();
			//get the column names for the combo boxes
			foreach(DataColumn colum in database.Columns)
			{
				column.Add(colum.ColumnName);
			}
			Selection1Combo.ItemsSource = column;
			Selection2Combo.ItemsSource = column;
            //set the base data contex
            DatabaseGrid.IsReadOnly = true;
			DatabaseGrid.DataContext = search.DefaultView;
		}

		//submit button execution
		private void SubmitClick(object sender, RoutedEventArgs e)
		{
			//checks if the first seleciton box is set
			if (Selection1Combo.SelectedIndex > -1)
			{	
				//check if the search entry tb is filled
				if (!string.IsNullOrWhiteSpace(Selection1TextBox.Text))
				{
					//check if there is a second criteria
					if (Selection2Combo.SelectedIndex > -1)
					{   
						//check if the search entry tb is filled
						if (!string.IsNullOrWhiteSpace(Selection2TextBox.Text))
						{
							//check if there is a matching object
							if (database.Select().Where(x => x[Selection1Combo.SelectedIndex].ToString() == Selection1TextBox.Text &&
									x[Selection2Combo.SelectedIndex].ToString() == Selection2TextBox.Text).Count() > 0)
							{
								//find the objects and set the datacontex
								search = database.Select().Where(x => x[Selection1Combo.SelectedIndex].ToString() == Selection1TextBox.Text &&
									x[Selection2Combo.SelectedIndex].ToString() == Selection2TextBox.Text).CopyToDataTable();
								DatabaseGrid.DataContext = search.DefaultView;
							}
							else{
								MessageBox.Show("Search Criteria Found nothing, Check Spelling If you think It exists");
							}
						}
						else
						{
							MessageBox.Show("Fill in the textbox with what you want to find for your second choice");
							DatabaseGrid.DataContext = database.DefaultView;
						}
					}
					else
					{
						//check if there is a matching object
						if (database.Select().Where(x => x[Selection1Combo.SelectedIndex].ToString() == Selection1TextBox.Text).Count() > 0)
						{
							//find the items and set the data context
							search = database.Select().Where(x => x[Selection1Combo.SelectedIndex].ToString() == Selection1TextBox.Text).CopyToDataTable();
							DatabaseGrid.DataContext = search.DefaultView;
						}
						else
						{
							MessageBox.Show("Search Criteria Found nothing, Check Spelling If you think It exists");
						}
					}
				}
				else
				{
					MessageBox.Show("You must fill in the textbox with what you want to find");
					DatabaseGrid.DataContext = database.DefaultView;
				}
			}
			else
			{
				MessageBox.Show("You must sellect the first criteria that you want to find");
				DatabaseGrid.DataContext = database.DefaultView;
			}
		}

		//reset all the combo boxes and textboxes to defaults, and reset the data context to the original
		private void ResetClick(object sender, RoutedEventArgs e)
		{
			Selection1Combo.SelectedIndex = -1;
			Selection2Combo.SelectedIndex = -1;
			Selection1TextBox.Text = "";
			Selection2TextBox.Text = "";

			DatabaseGrid.DataContext = database.DefaultView;

		}
	}
}
