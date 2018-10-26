﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DatabaseCreationWindow.xaml
    /// </summary>
    public partial class DatabaseCreationWindow : Window
    {
        public bool cancelClicked;

        public DatabaseCreationWindow()
        {
            InitializeComponent();
            cancelClicked = false;
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DatabaseNameTextBox.Text))
                this.Close();
            else
                MessageBox.Show("Please fill in name in Name field", "Invalid Operation");
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            cancelClicked = true;
            this.Close();
        }
    }
}
