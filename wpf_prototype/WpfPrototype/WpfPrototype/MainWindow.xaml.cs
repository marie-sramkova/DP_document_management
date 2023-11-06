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

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MyDataContext : INotifyPropertyChanged
        {
            private int _MyProperty;
            public int MyProperty { get => this._MyProperty; set
                {
                    this._MyProperty = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyProperty)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public MyDataContext Context { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Context = new MyDataContext();
            this.Context.MyProperty = 8;
            this.DataContext = this.Context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Context.MyProperty = int.Parse(txtBox1.Text);
            //lbl1.Content = txtBox1.Text;
        }
    }
}
