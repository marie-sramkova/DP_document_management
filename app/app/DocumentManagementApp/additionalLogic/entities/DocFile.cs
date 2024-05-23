using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DocumentManagementApp.additionalLogic.entities
{
    public class DocFile : NotifyPropertyChangedBase
    {
        private String _FilePath;

        public String FilePath { get { return _FilePath; } set { _FilePath = value; RaisePropertyChanged(nameof(FilePath)); } }
        private BindingList<DocAttribute> _DocAttributes;
        public BindingList<DocAttribute> DocAttributes { get { return _DocAttributes; } set { _DocAttributes = value; RaisePropertyChanged(nameof(DocAttributes)); } }
        [JsonConstructor]
        public DocFile(String filePaht, BindingList<DocAttribute> docAttributes) { 
            FilePath = filePaht;
            DocAttributes = docAttributes;
        }

        public DocFile(DocFile docFile)
        {
            FilePath = docFile.FilePath;
            DocAttributes = new BindingList<DocAttribute>();
            foreach (var atr in docFile.DocAttributes)
            {
                DocAttributes.Add(new DocAttribute(atr));
            }
        }
    }
}
