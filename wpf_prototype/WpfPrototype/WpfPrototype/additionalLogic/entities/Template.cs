using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class Template : NotifyPropertyChangedBase
    {
        private String _Name;

        public String Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(nameof(Name)); } }
        private BindingList<DocAttribute> _AverageDocAttribute;

        public BindingList<DocAttribute> AverageDocAttribute { get { return _AverageDocAttribute; } set { _AverageDocAttribute = value; RaisePropertyChanged(nameof(AverageDocAttribute)); } }
        private BindingList<DocAttribute> _AllDocAttributes;

        public BindingList<DocAttribute> AllDocAttributes { get { return _AllDocAttributes; } set { _AllDocAttributes = value; RaisePropertyChanged(nameof(AllDocAttributes)); } }
        private BindingList<DocFile> _DocFiles;

        public BindingList<DocFile> DocFiles { get { return _DocFiles; } set { _DocFiles = value; RaisePropertyChanged(nameof(DocFiles)); } }

        public Template(string text)
        {
            Name = text;
            AverageDocAttribute = new BindingList<DocAttribute>();
            AllDocAttributes = new BindingList<DocAttribute>();
            DocFiles = new BindingList<DocFile>();
        }

    }
}
