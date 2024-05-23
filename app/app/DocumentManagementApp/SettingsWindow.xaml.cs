﻿using System;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public MainWindow mainWindow;
        public SettingsWindow()
        {
            InitializeComponent();
            if (UserSettings.directoryPath == "" && !Directory.Exists(UserSettings.directoryPath)) 
            {
                btnClose.IsEnabled = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window_Closed(sender, e);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFolderPath.Text == "" || !Directory.Exists(textBoxFolderPath.Text))
            {
                MessageBox.Show("Incorrect directory!");
                return;
            }
            if (textBoxFolderPath.Text != "" && Directory.Exists(textBoxFolderPath.Text))
            {
                var dialogResult = MessageBox.Show("By changing the folder, you will lost all your saved analyzes. Are you sure you want to change it?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    UserSettings.directoryPath = textBoxFolderPath.Text;
                }
                else
                {
                    return;
                }
            }
            else
            {
                String newText = "";
                newText = TryToChangePathSlashes(newText);
                if (newText != "" && Directory.Exists(newText))
                {
                    var dialogResult = MessageBox.Show("By changing the folder, you will lost all your saved analyzes. Are you sure you want to change it?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        UserSettings.directoryPath = newText;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (UserSettings.directoryPath == "" && !Directory.Exists(UserSettings.directoryPath))
            {
                btnClose.IsEnabled = false;
                MessageBox.Show("Incorrect directory!");
            }
            else
            {
                SaveDirectoryPathPermanentlyToFileInApp();
                var settingsDocumentsFile = File.Create(UserSettings.settingsDocumentsFilePath);
                settingsDocumentsFile.Close();
                var settingsTemplatesFile = File.Create(UserSettings.settingsTemplatesFilePath);
                settingsTemplatesFile.Close();
                Window_Closed(sender, e);
            }
        }

        private static void SaveDirectoryPathPermanentlyToFileInApp()
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\data\\DocumentManagementApp\\folderPath.txt";
            if (File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
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

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.UpdateWindow();
            }catch (Exception ex) { }
            Close();
        }
    }
}
