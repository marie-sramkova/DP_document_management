using DocumentManagementApp.additionalLogic.entities;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DocumentManagementApp
{
    /// <summary>
    /// Interaction logic for WindowForOldDocPathInput.xaml
    /// </summary>
    public partial class WindowForOldDocPathInput : Window
    {
        public static DocFile oldDocPath;
        public MainWindow mainWindow;
        public WindowForOldDocPathInput()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window_Closed(sender, e);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DocFile savedFile = FileEditor.Instance.SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == textBoxFilePath.Text);
            if (textBoxFilePath.Text != "" && File.Exists(textBoxFilePath.Text) && savedFile != null && savedFile.DocAttributes != null && savedFile.DocAttributes.Count > 0)
            {
                oldDocPath = savedFile;
                Window1 window1 = new Window1();
                window1.mainWindow = mainWindow;
                window1.Show();
                this.mainWindow = null;
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect file path or file has not been analyzed yet. Please write different path.");
                return;
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.UpdateWindow();
            }catch(Exception ex) { }
            Close();
        }
    }
}
