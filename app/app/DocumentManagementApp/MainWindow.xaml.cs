using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using DocumentManagementApp.additionalLogic;
using DocumentManagementApp.additionalLogic.entities;
using System.Printing;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace DocumentManagementApp
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
            private BindingList<Filter> _Filters;
            public BindingList<Filter> Filters { get { return _Filters; } set { _Filters = value; RaisePropertyChanged(nameof(Filters)); } }
        }

        public Model model;


        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp");
            }
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
                    model.Filters = new BindingList<Filter>();
                    model.Filters.Add(new Filter());


                    ShowListViewWithTemplatesFilesAndAttributes();
                    CalculateAndShowNewDocsToAnalyze();
                    this.DataContext = model;
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
            if (model == null || model.SettingsEntity == null || model.SettingsEntity.Templates == null || model.SettingsEntity.Templates.Count == 0)
            {
                buttonAnalyzeOldDocument.IsEnabled = false;
            }
            else
            {
                buttonAnalyzeOldDocument.IsEnabled = true;
            }
        }

        private void ShowListViewWithTemplatesFilesAndAttributes()
        {
            this.model.SettingsEntity = FileEditor.Instance.SettingsEntity;
            foreach (var template in model.SettingsEntity.Templates)
            {
                foreach (var file in template.DocFiles)
                {
                    if (FileEditor.Instance.SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == file.FilePath) != null)
                    {
                        file.DocAttributes = FileEditor.Instance.SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == file.FilePath).DocAttributes;
                    }
                }
            }

            SettingsEntity tmpSettingsEntity = new SettingsEntity();
            tmpSettingsEntity.Templates = new BindingList<Template>();
            tmpSettingsEntity.DocFiles = new BindingList<DocFile>();
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
                    Template template = FileEditor.Instance.SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(x => x.FilePath.Equals(file.FilePath)) != null);
                    if (template != null && !tmpSettingsEntity.Templates.Any(x => x.Name.Equals(template.Name)))
                    {
                        tmpSettingsEntity.Templates.Add(template);
                        tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Clear();
                        tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Add(file);
                    }
                    else
                    {
                        if (template != null && !tmpSettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).DocFiles.Any(x => x.FilePath == file.FilePath))
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
                documentsCount = newFiles.Count;
                if (FileEditor.Instance.SettingsEntity != null)
                {
                    docFilesInSettingsFile = FileEditor.Instance.SettingsEntity.DocFiles;
                    foreach (DocFile docFileInSettingsFile in docFilesInSettingsFile)
                    {
                        if ((newFiles.Any(x => x.Equals(docFileInSettingsFile.FilePath)) && docFileInSettingsFile.DocAttributes.Count != 0)
                            || (newFiles.Any(x => x.Equals(docFileInSettingsFile.FilePath)) && !docFileInSettingsFile.FilePath.Contains(UserSettings.directoryPath)))
                        {
                            newFiles.Remove(newFiles.First(x => x == docFileInSettingsFile.FilePath));
                        }
                    }
                    documentsCount = newFiles.Count;
                    foreach (var file in newFiles)
                    {
                        if (!file.EndsWith("pdf") && !file.EndsWith("jpg") && !file.EndsWith("png") && !file.EndsWith("jpeg"))
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
            if (documentsCount == 0)
            {
                buttonAnalyzeNewDocuments.IsEnabled = false;
            }
            else
            {
                buttonAnalyzeNewDocuments.IsEnabled = true;
            }
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            if (expanded == false)
            {
                expanded = true;
                collapsedForm.Height = new GridLength(63, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(17, GridUnitType.Star);
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
                if ((!docFilesInSettingsFile.Any(x => x.FilePath == newFile))
                    && (newFile.EndsWith("pdf") || newFile.EndsWith("jpg") || newFile.EndsWith("png") || newFile.EndsWith("jpeg")))
                {
                    filesToStore.Add(newFile);
                }
            }
            FileEditor.Instance.AddNewFilesWithoutAttributes(filesToStore);

            Window1 window1 = new Window1();
            window1.mainWindow = this;
            window1.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.mainWindow = this;
            settingsWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonAnalyzeOldDocument_Click(object sender, RoutedEventArgs e)
        {
            WindowForOldDocPathInput windowForOldDocPathInput = new WindowForOldDocPathInput();
            windowForOldDocPathInput.mainWindow = this;
            windowForOldDocPathInput.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void ApplyFilters()
        {
            SettingsEntity settingsEntityToShow = new SettingsEntity();
            ShowListViewWithTemplatesFilesAndAttributes();
            ApplyOnDocuments(settingsEntityToShow);
            ApplyOnTemplates(settingsEntityToShow);
            model.SettingsEntity = settingsEntityToShow;
        }

        private void ApplyOnDocuments(SettingsEntity settingsEntityToShow)
        {
            List<DocFile> docFilesToRemove = new List<DocFile>();
            foreach (Filter filter in model.Filters)
            {
                if (filter.Title == "Item" && filter.Type == "rgx")
                {
                    String pattern = filter.Value.ToString();
                    try
                    {
                        Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

                        foreach (Template template in model.SettingsEntity.Templates)
                        {
                            foreach (DocFile docFile in template.DocFiles)
                            {
                                String text = ConvertDocFiletToString(docFile);
                                Match m = r.Match(text);
                                if (!m.Success)
                                {
                                    if (!docFilesToRemove.Contains(docFile))
                                    {
                                        docFilesToRemove.Add(docFile);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        settingsEntityToShow = new SettingsEntity();
                        return;
                    }
                }
                AddDocsToListToRemoveIfNotMatchesAmountFilter(docFilesToRemove, filter);
            }
            AddDocsWhichMatchesFilters(settingsEntityToShow, docFilesToRemove);
        }

        private void AddDocsWhichMatchesFilters(SettingsEntity settingsEntityToShow, List<DocFile> docFilesToRemove)
        {
            foreach (Template template in model.SettingsEntity.Templates)
            {
                foreach (DocFile docFile in template.DocFiles)
                {
                    if (!docFilesToRemove.Any(x => x.FilePath.Equals(docFile.FilePath)))
                    {
                        if (!settingsEntityToShow.Templates.Any(x => x.Name.Equals(template.Name)))
                        {
                            Template newTemplate = new Template(template);
                            newTemplate.DocFiles = new BindingList<DocFile>();
                            settingsEntityToShow.Templates.Add(newTemplate);
                            settingsEntityToShow.Templates.SingleOrDefault(x => x.Name.Equals(template.Name)).DocFiles.Add(new DocFile(docFile));
                        }
                        else
                        {
                            settingsEntityToShow.Templates.SingleOrDefault(x => x.Name.Equals(template.Name)).DocFiles.Add(new DocFile(docFile));
                        }
                    }
                }
            }
        }

        private void AddDocsToListToRemoveIfNotMatchesAmountFilter(List<DocFile> docFilesToRemove, Filter filter)
        {
            double outValue;
            if (filter.Title == "Amount" && (filter.Type == ">" || filter.Type == "<") && filter.Value != "" && Double.TryParse(filter.Value, out outValue))
            {
                foreach (Template template in model.SettingsEntity.Templates)
                {
                    foreach (DocFile docFile in template.DocFiles)
                    {
                        bool matchesFilter = false;
                        foreach (DocAttribute attr in docFile.DocAttributes)
                        {
                            if (attr.Type.Equals("Number"))
                            {
                                double value;
                                if (attr.Value.Contains(","))
                                {
                                    attr.Value.Replace(",", ".");
                                }
                                try
                                {
                                    value = Double.Parse(attr.Value);
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                                if (filter.Type == ">" && value > outValue)
                                {
                                    matchesFilter = true;
                                }
                                else if (filter.Type == "<" && value < outValue)
                                {
                                    matchesFilter = true;
                                }
                            }
                        }
                        if (matchesFilter == false)
                        {
                            if (!docFilesToRemove.Contains(docFile))
                            {
                                docFilesToRemove.Add(docFile);
                            }
                        }
                    }
                }
            }
        }

        private string ConvertDocFiletToString(DocFile docFile)
        {
            StringBuilder text = new StringBuilder();
            text.Append(docFile.FilePath).Append("\n");
            foreach (DocAttribute attribute in docFile.DocAttributes)
            {
                text.Append(attribute.Name).Append(" ").Append(attribute.Type).Append(" ").Append(attribute.Value).Append("\n");
            }
            return text.ToString();
        }

        private void ApplyOnTemplates(SettingsEntity settingsEntityToShow)
        {
            if (!model.Filters.Any(x => x.Title.Equals("Amount")))
            {
                List<Template> templatesToRemove = new List<Template>();
                foreach (Filter filter in model.Filters)
                {
                    if (filter.Title == "Item" && filter.Type == "rgx")
                    {
                        String pattern = filter.Value.ToString();
                        try
                        {
                            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                            foreach (Template template in model.SettingsEntity.Templates)
                            {
                                Match m = r.Match(template.Name);
                                if (!m.Success)
                                {
                                    templatesToRemove.Add(new Template(template));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            settingsEntityToShow = new SettingsEntity();
                            return;
                        }
                    }
                }
                foreach (var template in model.SettingsEntity.Templates)
                {
                    if (!templatesToRemove.Any(x => x.Name.Equals(template.Name)) && !settingsEntityToShow.Templates.Any(x => x.Name.Equals(template.Name)))
                    {
                        settingsEntityToShow.Templates.Add(template);
                    }
                    else if (!templatesToRemove.Any(x => x.Name.Equals(template.Name)) && settingsEntityToShow.Templates.Any(x => x.Name.Equals(template.Name)))
                    {
                        settingsEntityToShow.Templates.Remove(settingsEntityToShow.Templates.SingleOrDefault(x => x.Name.Equals(template.Name)));
                        settingsEntityToShow.Templates.Add(template);
                    }
                }
            }
        }

        private void buttonApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            if (model.Filters.Count > 0)
            {
                ApplyFilters();
                expanded = false;
                collapsedForm.Height = new GridLength(0, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(0, GridUnitType.Star);
                listView.Height = new GridLength(80, GridUnitType.Star);
            }
        }

        private void buttonAddNewFilter_Click(object sender, RoutedEventArgs e)
        {
            model.Filters.Add(new Filter());
        }

        private void btnDeleteFilter_Click(object sender, RoutedEventArgs e)
        {
            Filter filterToDelete;
            try
            {
                filterToDelete = (sender as FrameworkElement).DataContext as Filter;
            }
            catch (NullReferenceException ex)
            {
                filterToDelete = sender as Filter;
            }
            if (filterToDelete != null)
            {
                model.Filters.Remove(filterToDelete);
            }
            if (model.Filters.Count == 0)
            {
                model.Filters.Add(new Filter("Item", "rgx", ""));
                ShowListViewWithTemplatesFilesAndAttributes();
                expanded = false;
                collapsedForm.Height = new GridLength(0, GridUnitType.Star);
                blankAfterCollapsedForm.Height = new GridLength(0, GridUnitType.Star);
                listView.Height = new GridLength(80, GridUnitType.Star);
            }
        }
        
        public void UpdateWindow()
        {
            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp/folderPath.txt";
            String dirPath = "";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    dirPath = reader.ReadToEnd();
                }
                if (dirPath == "" || !Directory.Exists(dirPath))
                {
                    Close();
                }
                else
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
            }
            else
            {
                Close();
            }
        }
    }
}
