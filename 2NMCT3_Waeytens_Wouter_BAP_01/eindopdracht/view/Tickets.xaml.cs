﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eindopdracht.view
{
    /// <summary>
    /// Interaction logic for Tickets.xaml
    /// </summary>
    public partial class Tickets : UserControl
    {
        public Tickets()
        {
            InitializeComponent();
        }

        private void FocusSearch(object sender, RoutedEventArgs e)
        {
            if(SearchField.Text == "Search")
            {
                SearchField.Text = "";
            }
        }
    }
}
