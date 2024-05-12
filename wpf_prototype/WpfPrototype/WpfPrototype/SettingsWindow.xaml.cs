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

namespace WpfPrototype
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            if (UserSettings.directoryPath == "" && !Directory.Exists(UserSettings.directoryPath)) 
            {
                btnClose.IsEnabled = false;
                //btnClose.Visibility = Visibility.Hidden;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFolderPath.Text != "" && Directory.Exists(textBoxFolderPath.Text))
            {
                UserSettings.directoryPath = textBoxFolderPath.Text;
            }
            else
            {
                String newText = "";
                newText = TryToChangePathSlashes(newText);
                if (newText != "" && Directory.Exists(newText))
                {
                    UserSettings.directoryPath = newText;
                }
            }
            if (UserSettings.directoryPath == "" && !Directory.Exists(UserSettings.directoryPath))
            {
                btnClose.IsEnabled = false;
                //btnClose.Visibility = Visibility.Hidden;
                MessageBox.Show("Incorrect directory!");
            }
            else
            {
                SaveDirectoryPathPermanentlyToFileInApp();
                var settingsDocumentsFile = File.Create(UserSettings.settingsDocumentsFilePath);
                settingsDocumentsFile.Close();
                var settingsTemplatesFile = File.Create(UserSettings.settingsTemplatesFilePath);
                settingsTemplatesFile.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private static void SaveDirectoryPathPermanentlyToFileInApp()
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\data\\DocumentManagementApp\\folderPath.txt";
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(UserSettings.directoryPath);
                    fs.Write(info, 0, info.Length);
                }
            }
            else
            {
                var file = File.Create(filePath);
                file.Close();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(UserSettings.directoryPath);
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        private string TryToChangePathSlashes(string newText)
        {
            if (textBoxFolderPath.Text.Contains("/"))
            {
                newText = textBoxFolderPath.Text.Replace("/", "\\");
            }
            else if (textBoxFolderPath.Text.Contains("\\"))
            {
                newText = textBoxFolderPath.Text.Replace("\\", "/");
            }
            return newText;
        }
    }
}
