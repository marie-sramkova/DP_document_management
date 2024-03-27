using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    public class FileEditor
    {

        private FileEditor() {
            String fileText = "";
            if (!File.Exists(UserSettings.settingsFilePath))
            {
                File.Create(UserSettings.settingsFilePath);
                
            }
            fileText = File.ReadAllText(UserSettings.settingsFilePath);
            Console.WriteLine(fileText);
            SettingsEntity = JsonConvert.DeserializeObject<SettingsEntity>(fileText);
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
                return instance;
            }
        }

        public SettingsEntity SettingsEntity { get; }

        public void AddNewFiles(List<String> files)
            {
                foreach (var file in files)
                {
                    List<DocAttribute> docAttributes = new List<DocAttribute>();
                    //todo: convert attributes from analyzed file to entities
                    DocFile newDoc = new DocFile(file, docAttributes);
                }
            //todo: save to file
            }

        public Boolean CheckIfSettingsFileIsEmpty()
        {
            if (SettingsEntity == null) { 
                return true;
            }
            return false;
        }


    }
}
