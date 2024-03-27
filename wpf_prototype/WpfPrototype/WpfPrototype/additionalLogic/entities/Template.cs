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
        public DocAttribute AverageDocAttribute { get; set; }
        public List<DocFile> DocFiles { get; set; }
    }
}
