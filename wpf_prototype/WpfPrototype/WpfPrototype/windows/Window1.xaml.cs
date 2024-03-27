using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tesseract;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            imgAnalyzedDocument.Source = new BitmapImage(new Uri(@"data/image.png", UriKind.RelativeOrAbsolute)); ;
        }

        private void ButtonProcess_Click(object sender, RoutedEventArgs e)
        {
                var path = "D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\tessdata";
                using (var engine = new TesseractEngine(path, "ces", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\image.png"))
                    {
                        // engine.SetVariable("tessedit_char_whitelist", "0123456789");
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.Write(text);
                            //textBoxDocumentType.Text = text;
                        }
                    }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
