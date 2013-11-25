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
using eindopdracht.model;

namespace eindopdracht.view
{
    /// <summary>
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : UserControl
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void FocusSearch(object sender, RoutedEventArgs e)
        {
            if (searchbox.Text == "Search")
            {
                searchbox.Text = "";
            }
        }
    }
}
