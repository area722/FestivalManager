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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eindopdracht.view
{
    /// <summary>
    /// Interaction logic for SettingsBands.xaml
    /// </summary>
    public partial class SettingsBands : UserControl
    {
        public SettingsBands()
        {
            InitializeComponent();
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            if (NewStageTxt.Text == "New Stage")
            {
                NewStageTxt.Text = "";
            }
        }

        private void ClearTextGenre(object sender, RoutedEventArgs e)
        {
            if (NewGenreTxt.Text == "New Genre")
            {
                NewGenreTxt.Text = "";
            }
        }

        private void Focus(object sender, RoutedEventArgs e)
        {
            if (SearchBand.Text == "zoek een band")
            {
                SearchBand.Text = "";
            }
        }
    }
}
