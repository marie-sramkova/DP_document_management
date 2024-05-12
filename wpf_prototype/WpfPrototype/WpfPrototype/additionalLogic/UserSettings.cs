using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    public class UserSettings
    {
        
        //todo: change in settings
        public static string directoryPath = "";
        public static string settingsDocumentsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp" + "/settings_documents.txt";
        public static string settingsTemplatesFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/data/DocumentManagementApp" + "/settings_templates.txt";

    }
}
