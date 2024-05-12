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
using WpfPrototype.additionalLogic;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expanded = false;
        BindingList<DocFile> docFilesInSettingsFile;
        List<string> newFiles;

        public class Model : NotifyPropertyChangedBase
        {
            private SettingsEntity _SettingsEntity;
            public SettingsEntity SettingsEntity { get { return _SettingsEntity; } set { _SettingsEntity = value; RaisePropertyChanged(nameof(SettingsEntity)); } }

        }

        public Model model;


        public MainWindow()
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp/folderPath.txt";
            String dirPath = "";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    dirPath = reader.ReadToEnd();
                }
                if (dirPath != "" && Directory.Exists(dirPath))
                {
                    UserSettings.directoryPath = dirPath;
                    this.model = new Model();
                    InitializeComponent();
                    ShowListViewWithTemplatesFilesAndAttributes();
                    CalculateAndShowNewDocsToAnalyze();
                }
                else
                {
                    ButtonSettings_Click(null, null);
                }
            } 
            else
            {
                ButtonSettings_Click(null, null);
            }
        }

        private void ShowListViewWithTemplatesFilesAndAttributes()
        {
            this.model.SettingsEntity = FileEditor.Instance.SettingsEntity;
            foreach (var template in model.SettingsEntity.Templates)
            {
                foreach (var file in template.DocFiles)
                {
                    file.DocAttributes = FileEditor.Instance.SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == file.FilePath).DocAttributes;
                }
            }

            SettingsEntity tmpSettingsEntity = new SettingsEntity();
            foreach (var file in FileEditor.Instance.SettingsEntity.DocFiles)
            {
                if (file.DocAttributes.Count != 0)
                {
                    foreach (var attribute in file.DocAttributes)
                    {
                        StringBuilder newValue = new StringBuilder("");
                        string[] valuePieces = attribute.Value.Replace("\n", " ").Split(" ");
                        for (global::System.Int32 i = 0; i < valuePieces.Length; i++)
                        {
                            if (valuePieces[i] != "")
                            {
                                newValue.Append(valuePieces[i]).Append(" ");
                            }
                        }
                        attribute.Value = newValue.ToString();
                    }
                    Template template = FileEditor.Instance.SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(x => x.FilePath == file.FilePath) != null);
                    if (!tmpSettingsEntity.Templates.Any(x => x.Name == template.Name))
                    {
                        tmpSettingsEntity.Templates.Add(template);
                        tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Clear();
                        tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Add(file);
                    }
                    else
                    {
                        if (!tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Any(x => x.FilePath == file.FilePath))
                        {
                            tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Add(file);
                        }
                    }
                }
            }

            model.SettingsEntity = tmpSettingsEntity;
            this.DataContext = model;
        }

        private void CalculateAndShowNewDocsToAnalyze()
        {
            docFilesInSettingsFile = new BindingList<DocFile>();
            newFiles = new List<string>();
            int documentsCount = 0;
            if (UserSettings.directoryPath != null)
            {
                newFiles = Directory.GetFiles(UserSettings.directoryPath, "*", SearchOption.AllDirectories).ToList();
                documentsCount = newFiles.Count - 2;
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
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            Close();
        }
    }
}
