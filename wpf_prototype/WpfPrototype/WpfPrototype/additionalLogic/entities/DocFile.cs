using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace WpfPrototype.additionalLogic.entities
{
    public class DocFile : NotifyPropertyChangedBase
    {
        private String _FilePath;

        public String FilePath { get { return _FilePath; } set { _FilePath = value; RaisePropertyChanged(nameof(FilePath)); } }
        private BindingList<DocAttribute> _DocAttributes;
        public BindingList<DocAttribute> DocAttributes { get { return _DocAttributes; } set { _DocAttributes = value; RaisePropertyChanged(nameof(DocAttributes)); } }
        public DocFile(String filePaht, BindingList<DocAttribute> docAttributes) { 
            FilePath = filePaht;
            DocAttributes = docAttributes;
        }
    }
}
