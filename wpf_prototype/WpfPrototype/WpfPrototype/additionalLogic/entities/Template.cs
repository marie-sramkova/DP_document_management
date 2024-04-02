using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class Template
    {
        public String Name { get; set; }
        public List<DocAttribute> AverageDocAttribute { get; set; }
        public List<DocAttribute> AllDocAttributes { get; set; }
        public List<DocFile> DocFiles { get; set; }

        public Template(string text)
        {
            Name = text;
            AverageDocAttribute = new List<DocAttribute>();
            AllDocAttributes = new List<DocAttribute>();
            DocFiles = new List<DocFile>();
        }

    }
}
