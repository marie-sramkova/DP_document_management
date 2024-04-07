using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    List<DocAttribute> docAttributes = new List<DocAttribute>();
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
                File.Create(UserSettings.settingsFilePath);

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
            SettingsEntity.DocFiles = new List<DocFile>();
            SettingsEntity.Templates = new List<Template>();
        }

        public void AddFileToTemplate(string templateName, DocFile file)
        {
            Template savedTemplate = SettingsEntity.Templates.Find(x => x.Name == templateName);
            if (!savedTemplate.DocFiles.Any(x => x.FilePath == file.FilePath)) { 
                savedTemplate.DocFiles.Add(file);
                WriteSettingsEntityToFile();
            }
        }
    }
}
