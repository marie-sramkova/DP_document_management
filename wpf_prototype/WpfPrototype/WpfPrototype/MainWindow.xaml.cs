using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expanded = false;
        List<DocFile> docFilesInSettingsFile;
        List<string> newFiles;


        public MainWindow()
        {
            docFilesInSettingsFile = new List<DocFile>();
            newFiles = new List<string>();
            InitializeComponent();
            int documentsCount = 0;
            if (UserSettings.directoryPath != null)
            {
                newFiles = Directory.GetFiles(UserSettings.directoryPath, "*", SearchOption.AllDirectories).ToList();
                documentsCount = newFiles.Count - 1;
                if (FileEditor.Instance.SettingsEntity != null)
                {
                    docFilesInSettingsFile = FileEditor.Instance.SettingsEntity.DocFiles;
                    foreach (DocFile docFileInSettingsFile in docFilesInSettingsFile)
                    {
                        if (newFiles.Contains(docFileInSettingsFile.FilePath) && docFileInSettingsFile.DocAttributes.Count != 0)
                        {
                            documentsCount = documentsCount - 1;
                        }
                    }
                }
            }
            if (documentsCount != 0)
            {
                buttonAnalyzeNewDocuments.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffa3a3");
                buttonAnalyzeNewDocuments.FontWeight = FontWeights.Bold;
            }
            buttonAnalyzeNewDocuments.Content = "Analyze new files (" + documentsCount + ")";
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            if (expanded == false)
            {
                expanded = true;
                collapsedForm.Height = new GridLength(17, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(63, GridUnitType.Star);
                listView.Height = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                expanded = false;
                collapsedForm.Height = new GridLength(0, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(0, GridUnitType.Star);
                listView.Height = new GridLength(80, GridUnitType.Star);
            }

        }

        private void ButtonAnalyzeNewDocuments_Click(object sender, RoutedEventArgs e)
        {
            newFiles.Clear();
            docFilesInSettingsFile.Clear();
            List<string> filesToStore = new List<string>();
            newFiles = Directory.GetFiles(UserSettings.directoryPath, "*", SearchOption.AllDirectories).ToList();
            docFilesInSettingsFile = FileEditor.Instance.SettingsEntity.DocFiles;

            foreach (string newFile in newFiles)
            {
                if (!docFilesInSettingsFile.Any(x => x.FilePath == newFile))
                {
                    filesToStore.Add(newFile);
                }
            }
            FileEditor.Instance.AddNewFilesWithoutAttributes(filesToStore);
            //todo: save new documents into settings file
            
            Window1 window1 = new Window1();
            window1.Show();
            Close();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            //todo: open new window with settings
        }
    }
}
