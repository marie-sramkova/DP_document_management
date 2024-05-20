using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementApp.additionalLogic.entities
{
    public class Template : NotifyPropertyChangedBase
    {
        private String _Name;

        public String Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(nameof(Name)); } }
        private BindingList<DocAttribute> _AllDocAttributes;

        public BindingList<DocAttribute> AllDocAttributes { get { return _AllDocAttributes; } set { _AllDocAttributes = value; RaisePropertyChanged(nameof(AllDocAttributes)); } }
        private BindingList<DocFile> _DocFiles;

        public BindingList<DocFile> DocFiles { get { return _DocFiles; } set { _DocFiles = value; RaisePropertyChanged(nameof(DocFiles)); } }

        [JsonConstructor]
        public Template(string name)
        {
            Name = name;
            AllDocAttributes = new BindingList<DocAttribute>();
            DocFiles = new BindingList<DocFile>();
        }

        public Template(Template template)
        {
            Name = template.Name;
            AllDocAttributes = new BindingList<DocAttribute>();
            foreach (var atr in template.AllDocAttributes)
            {
                AllDocAttributes.Add(new DocAttribute(atr));
            }
            DocFiles = new BindingList<DocFile>();
            foreach (var file in template.DocFiles)
            {
                DocFiles.Add(new DocFile(file));
            }
        }
    }
}
