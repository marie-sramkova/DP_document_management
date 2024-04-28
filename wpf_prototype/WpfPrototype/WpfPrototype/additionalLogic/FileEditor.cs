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
                if (!SettingsEntity.DocFiles.Any(x => x.FilePath == file) && !System.IO.Path.GetFileName(file).Equals("settings.txt"))
                {
                    BindingList<DocAttribute> docAttributes = new BindingList<DocAttribute>();
                    DocFile newDoc = new DocFile(file, docAttributes);
                    SettingsEntity.DocFiles.Add(newDoc);
                }
                else if (System.IO.Path.GetFileName(file).Equals("settings.txt")) 
                {
                    
                }
                else
                {
                    //todo: inform that the document has been already analyzed
                    return;
                }
            }
            WriteSettingsEntityToFile();
        }

        //todo: where to convert to entity, here? which string is input?
        //public void AddNewFilesWithAttributes(List<string> files)
        //{
        //    foreach (var file in files)
        //    {
        //        List<DocAttribute> docAttributes = new List<DocAttribute>();
        //        //todo: convert attributes from analyzed file to entities
        //        DocFile newDoc = new DocFile(file, docAttributes);
        //    }
        //    //todo: save to file
        //}

        private void ReadFileToSettingsEntity()
        {
            String fileText = "";
            if (!File.Exists(UserSettings.settingsFilePath))
            {
                using (File.Create(UserSettings.settingsFilePath)) { }
                
            }
            //add try-catch - reading from file!!!
            fileText = File.ReadAllText(UserSettings.settingsFilePath);
            if (fileText.Length > 0)
            {
                SettingsEntity = JsonConvert.DeserializeObject<SettingsEntity>(fileText);
            }
            else 
            {
                CreateEmptySettingsEntity();
            }
        }

        public void WriteSettingsEntityToFile()
        {
            String fileText = "";
            if (!File.Exists(UserSettings.settingsFilePath))
            {
                File.Create(UserSettings.settingsFilePath);
            }
            fileText = Newtonsoft.Json.JsonConvert.SerializeObject(SettingsEntity, Formatting.Indented);
            //add try-catch - writing to file!!!
            File.WriteAllText(UserSettings.settingsFilePath, fileText);
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
                SettingsEntity.Templates.SingleOrDefault(x => x.Name == template.Name).AllDocAttributes.Add(docAttribute);
                WriteSettingsEntityToFile();
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
                    //docFile.DocAttributes = docAttributes;
                    foreach (DocAttribute attribute in docAttributes)
                    {
                        //AddAttributeToTemplate(template, attribute);
                        AddAttributeToFileAndTemplate(fileName, attribute);
                    }

                    //todo: calculate averageDocAttribute for template
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

                    //todo: calculate averageDocAttribute for template
                    WriteSettingsEntityToFile();
                }
            }
        }
    }
}
