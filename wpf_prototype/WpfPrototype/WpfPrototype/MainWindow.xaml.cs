using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Tesseract;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool expanded = false;


        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            if (expanded == false)
            {
                expanded = true;
                collapsedForm.Height = new GridLength(17, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(69, GridUnitType.Star);
                listView.Height = new GridLength(0, GridUnitType.Star);
            }
            else {
                expanded = false;
                collapsedForm.Height = new GridLength(0, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(0, GridUnitType.Star);
                listView.Height = new GridLength(86, GridUnitType.Star);
            }
            
        }

        private void buttonAnalyzeNewDocuments_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}
