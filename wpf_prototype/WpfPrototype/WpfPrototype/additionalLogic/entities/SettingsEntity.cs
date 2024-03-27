using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPrototype.additionalLogic.entities
{
    public class SettingsEntity
    {
        public List<Template> Templates {  get; set; }
        public List<DocFile> DocFiles { get; set; }

        public void AddDoc(DocFile docFile)
        {
            DocFiles.Add(docFile);
        }

    }
}
