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


        public MainWindow()
        {
            InitializeComponent();
            int documentsCount = 0;
            Console.WriteLine("directory path: " + UserSettings.directoryPath);
            if (UserSettings.directoryPath != null)
            {
                List<string> newFiles = Directory.GetFiles(UserSettings.directoryPath, "*", SearchOption.AllDirectories).ToList();
                documentsCount = newFiles.Count - 1;
                if (FileEditor.Instance.SettingsEntity != null)
                {
                    List<DocFile> docFiles = FileEditor.Instance.SettingsEntity.DocFiles;
                    foreach (DocFile file in docFiles)
                    {
                        if (newFiles.Contains(file.FilePath))
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
            Window1 window1 = new Window1();
            window1.Show();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            //open new window with settings
        }
    }
}
