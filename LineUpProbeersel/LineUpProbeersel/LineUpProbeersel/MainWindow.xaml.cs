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
using System.Drawing;
using System.Windows.Threading;

namespace LineUpProbeersel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int mouseX;
        int mouseY;

        public MainWindow()
        {
            InitializeComponent();
            initTimer();
        }

        private void initTimer()
        {
            timer.Interval = new TimeSpan(1);
            timer.Tick += mouseMove;
        }

        private void mouseDownLineUp(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("mouseDown");
            timer.Start();
        }

        private void mouseMove(object sender, EventArgs e)
        {
            Console.WriteLine("mousemove");
            mouseX = System.Windows.Forms.Cursor.Position.X;
            mouseY = System.Windows.Forms.Cursor.Position.Y;
           // Console.WriteLine(mouseX);
            Thickness margin = wouter.Margin;
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                margin.Left = mouseX - Application.Current.MainWindow.Left - (wouter.ActualWidth * 0.5);
                margin.Top = mouseY - Application.Current.MainWindow.Top - (wouter.ActualHeight);
                wouter.Margin = margin;
            }
            else
            {
                //Console.WriteLine("homo");
                margin.Left = mouseX - (wouter.ActualWidth * 0.5);
                margin.Top = mouseY - (wouter.ActualHeight);
                wouter.Margin = margin;
            }
           
        }

        private void mouseUpLineUp(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("mouseup");
            timer.Stop();
        }
    }
}
