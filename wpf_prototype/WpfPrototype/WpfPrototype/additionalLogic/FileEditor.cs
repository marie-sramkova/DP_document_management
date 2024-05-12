using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    public class FileEditor
    {
        public SettingsEntity SettingsEntity {get; set;}

        private FileEditor()
        {
            CreateEmptySettingsEntity();
            ReadFileToSettingsEntity();
        }

        private static FileEditor instance = null;

        public static FileEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileEditor();
                }
                instance.ReadFileToSettingsEntity();
                return instance;
            }
        }

        public void AddNewTemplate(Template template) 
        {
            if (!SettingsEntity.Templates.Any(x => x.Name == template.Name))
            {
                SettingsEntity.Templates.Add(template);
                WriteSettingsEntityToFile();
            }
            else 
            {
                //todo: tell user that the template already exists
            }
        }

        public void AddNewFilesWithoutAttributes(List<String> files)
        {
            ReadFileToSettingsEntity();
            foreach (var file in files)
            {
                if (!SettingsEntity.DocFiles.Any(x => x.FilePath == file) && (file != UserSettings.settingsDocumentsFilePath) || file != UserSettings.settingsTemplatesFilePath)
                {
                    BindingList<DocAttribute> docAttributes = new BindingList<DocAttribute>();
                    DocFile newDoc = new DocFile(file, docAttributes);
                    SettingsEntity.DocFiles.Add(newDoc);
                }
            }
            WriteSettingsEntityToFile();
        }

        public void RemoveFileFromDocs(DocFile file)
        {
            if (SettingsEntity.DocFiles.Any( x => x.FilePath.Equals(file.FilePath)))
            {
                SettingsEntity.DocFiles.Remove(SettingsEntity.DocFiles.First(x => x.FilePath.Equals(file.FilePath)));
            }
        }

        private void ReadFileToSettingsEntity()
        {
            CreateEmptySettingsEntity();
            String fileTextDocuments = "";
            String fileTextTemplates = "";
            if (!File.Exists(UserSettings.settingsDocumentsFilePath))
            {
                using (File.Create(UserSettings.settingsDocumentsFilePath)) { }
                
            }
            if (!File.Exists(UserSettings.settingsTemplatesFilePath))
            {
                using (File.Create(UserSettings.settingsTemplatesFilePath)) { }

            }
            //add try-catch - reading from file!!!
            fileTextDocuments = File.ReadAllText(UserSettings.settingsDocumentsFilePath);
            fileTextTemplates = File.ReadAllText(UserSettings.settingsTemplatesFilePath);
            if (fileTextDocuments.Length > 0)
            {
                SettingsEntity.DocFiles = JsonConvert.DeserializeObject<BindingList<DocFile>>(fileTextDocuments);
                if (SettingsEntity.DocFiles == null)
                {
                    SettingsEntity.DocFiles = new BindingList<DocFile>();
                }
            }
            if (fileTextTemplates.Length > 0)
            {
                SettingsEntity.Templates = JsonConvert.DeserializeObject<BindingList<Template>>(fileTextTemplates);
                if (SettingsEntity.Templates == null)
                {
                    SettingsEntity.Templates = new BindingList<Template>();
                }
            }
        }

        public void WriteSettingsEntityToFile()
        {
            String fileTextDocuments = "";
            String fileTextTemplates = "";
            if (!File.Exists(UserSettings.settingsDocumentsFilePath))
            {
                File.Create(UserSettings.settingsDocumentsFilePath);
            }
            if (!File.Exists(UserSettings.settingsTemplatesFilePath))
            {
                File.Create(UserSettings.settingsTemplatesFilePath);
            }
            fileTextDocuments = Newtonsoft.Json.JsonConvert.SerializeObject(SettingsEntity.DocFiles, Formatting.Indented);
            fileTextTemplates = Newtonsoft.Json.JsonConvert.SerializeObject(SettingsEntity.Templates, Formatting.Indented);
            //add try-catch - writing to file!!!
            File.WriteAllText(UserSettings.settingsDocumentsFilePath, fileTextDocuments);
            File.WriteAllText(UserSettings.settingsTemplatesFilePath, fileTextTemplates);
        }

        private void CreateEmptySettingsEntity()
        {
            SettingsEntity = new SettingsEntity();
            SettingsEntity.DocFiles = new BindingList<DocFile>();
            SettingsEntity.Templates = new BindingList<Template>();
        }

        public void AddFileToTemplate(string templateName, DocFile file)
        {
            Template savedTemplate = SettingsEntity.Templates.SingleOrDefault(x => x.Name == templateName);
            if (!savedTemplate.DocFiles.Any(x => x.FilePath == file.FilePath)) { 
                savedTemplate.DocFiles.Add(file);
                WriteSettingsEntityToFile();
            }
        }

        public void AddAttributeToTemplate(Template template, DocAttribute docAttribute) 
        {
            if (!template.AllDocAttributes.Any(x => x.Name == docAttribute.Name))
            {
                template.AllDocAttributes.Add(docAttribute);
            }
            else
            {
                int index = template.AllDocAttributes.IndexOf(template.AllDocAttributes.SingleOrDefault(x => x.Name == docAttribute.Name));
                if (index >= 0)
                {
                    SettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).AllDocAttributes[index] = docAttribute;
                }
            }
        }

        public void AddAttributesToFileAndTemplate(string fileName, BindingList<DocAttribute> docAttributes)
        {
            Template template = SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(y => y.FilePath == fileName) != null);
            if (template != null) {
                DocFile docFile = SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == fileName);
                if (docFile != null)
                {
                    foreach (DocAttribute attribute in docAttributes)
                    {
                        AddAttributeToFileAndTemplate(fileName, attribute);
                    }
                    WriteSettingsEntityToFile();
                }
            }
        }

        public void AddAttributeToFileAndTemplate(string fileName, DocAttribute docAttribute)
        {
            Template template = SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(y => y.FilePath == fileName) != null);
            if (template != null)
            {
                DocFile docFile = SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == fileName);
                if (docFile != null)
                {
                    int index = docFile.DocAttributes.IndexOf(docFile.DocAttributes.SingleOrDefault(x => x.Name == docAttribute.Name));
                    if (index >= 0)
                    {
                        SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == fileName).DocAttributes[index] = docAttribute;
                    }
                    else 
                    {
                        SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == fileName).DocAttributes.Add(docAttribute);
                    }

                    AddAttributeToTemplate(template, docAttribute);
                    WriteSettingsEntityToFile();
                }
            }
        }
    }
}
