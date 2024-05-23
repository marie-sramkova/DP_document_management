using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementApp.additionalLogic.entities
{
    public class SettingsEntity : NotifyPropertyChangedBase
    {
        private BindingList<Template> _templates;
        public BindingList<Template> Templates { get { return _templates; } set { _templates = value; RaisePropertyChanged(nameof(Templates)); } }
        private BindingList<DocFile> _docFiles;

        public BindingList<DocFile> DocFiles { get { return _docFiles; } set { _docFiles = value; RaisePropertyChanged(nameof(DocFiles)); } }

        [JsonConstructor]
        public SettingsEntity() 
        {
            Templates = new BindingList<Template>();
            DocFiles = new BindingList<DocFile>();
        }

        public SettingsEntity(SettingsEntity settingsEntity)
        {
            Templates = new BindingList<Template>();
            foreach (var template in settingsEntity.Templates)
            {
                Templates.Add(new Template(template));
            }
            DocFiles = new BindingList<DocFile>();
            foreach (var file in settingsEntity.DocFiles)
            {
                DocFiles.Add(new DocFile(file));
            }
        }

        public void AddDoc(DocFile docFile)
        {
            DocFiles.Add(docFile);
        }

        public void AddTemplate(Template template)
        {
            Templates.Add(template);
        }
    }
}
