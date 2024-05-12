using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace WpfPrototype.additionalLogic.ocr
{
    internal class TesseractOCR : OCR
    {
        public string GetTextFromImage(byte[] bytes)
        {
            var path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString()).ToString() + "\\tessdata";
            using (var engine = new TesseractEngine(path, "ces", EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(bytes))
                {
                    // engine.SetVariable("tessedit_char_whitelist", "0123456789");
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        return text;
                    }
                }
            }
        }
    }
}
