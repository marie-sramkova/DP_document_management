using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class DocFile
    {
        public String FilePath { get; set; }
        public List<DocAttribute> DocAttributes { get; set; }
        
        public DocFile(String filePaht, List<DocAttribute> docAttributes) { 
            FilePath = filePaht;
            DocAttributes = docAttributes;
        }
    }
}
