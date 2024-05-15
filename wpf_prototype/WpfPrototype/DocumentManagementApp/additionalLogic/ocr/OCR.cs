using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementApp.additionalLogic.ocr
{
    internal interface OCR
    {
        string GetTextFromImage(byte[] bytes);
    }
}
