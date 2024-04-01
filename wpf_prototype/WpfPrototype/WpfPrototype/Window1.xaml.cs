using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TallComponents.PDF.Rasterizer;
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

            CreateTemplateButtons();

            buttonSave.Visibility = Visibility.Hidden;



            //todo: read files from settings file and save iteratively
            ConvertPdfToPng("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\temaP22085.pdf", "D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\out.png");

            //imgAnalyzedDocument.Source = new BitmapImage(new Uri("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\out.png", UriKind.RelativeOrAbsolute));
            //imgAnalyzedDocument.Source = new BitmapImage(new Uri(@"data/out.png", UriKind.RelativeOrAbsolute));

            Uri uri = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png", UriKind.RelativeOrAbsolute);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = uri;
            bitmap.EndInit();
            imgAnalyzedDocument.Source = bitmap;
            File.Delete(uri.AbsolutePath);
        }

        private void CreateTemplateButtons()
        {
            panel.Height = new GridLength(91, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);


            //todo: iteration by templates
            for (int i = 0; i < 5; i++)
            {
                Button buttonTemplate = new Button();
                buttonTemplate.Content = "Template name";
                buttonTemplate.Width = 250;
                buttonTemplate.Click += (s, e) =>
                {
                    //save template and continue with analyzing document by choosen template
                };
                templateStackPanel.Children.Add(buttonTemplate);
                templateStackPanel.VerticalAlignment = VerticalAlignment.Center;
                //todo: change labels to spaces!!!!!
                templateStackPanel.Children.Add(new Label());
            }

            Button buttonNewTemplate = new Button();
            buttonNewTemplate.Content = "Create new template";
            buttonNewTemplate.Width = 250;
            buttonNewTemplate.Click += (s, e) =>
            {
                ButtonNewTemplate_Click(s, e);
            };
            templateStackPanel.Children.Add(buttonNewTemplate);
            templateStackPanel.VerticalAlignment = VerticalAlignment.Center;




            //Button buttonNewTemplate = new Button();
            //buttonNewTemplate.Content = "Create new template";
            //buttonNewTemplate.Click += (s, e) =>
            //{
            //    ButtonNewTemplate_Click(s, e);
            //};
            //listViewTemplatesAndAttributes.Items.Add(buttonNewTemplate);
            //listViewTemplatesAndAttributes.HorizontalContentAlignment = HorizontalAlignment.Center;
        }

        private static void ConvertPdfToPng(String inputPdfPath, String outputPngPath)
        {
            using (FileStream fileIn = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
            {
                var pdf = new Document(fileIn);
                var page = pdf.Pages[0];
                float resolution = 150f;
                using (FileStream fileOut = new FileStream(outputPngPath, FileMode.Create, FileAccess.Write))
                {
                    page.SaveAsBitmap(fileOut, ImageEncoding.Png, resolution);
                }
            }
            //conversion to tiff - need to edit outputTiffPath
            //using (FileStream fileIn = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
            //{
            //    Document document = new Document(fileIn);

            //    ConvertToTiffOptions options = new ConvertToTiffOptions();
            //    options.Compression = TiffCompression.CcittG4;
            //    options.Resolution = 150;
            //    options.PixelFormat = TallComponents.PDF.Rasterizer.PixelFormat.Bw1Bpp;

            //    using (FileStream fileOut = new FileStream(outputTiffPath , FileMode.Create, FileAccess.Write))
            //    {
            //        document.ConvertToTiff(fileOut, options);
            //    }
            //}

        }

        private void ButtonProcess_Click(object sender, RoutedEventArgs e)
        {
            var path = "D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\tessdata";
            using (var engine = new TesseractEngine(path, "ces", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\out.png"))
                {
                    // engine.SetVariable("tessedit_char_whitelist", "0123456789");
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Debug.WriteLine("text: " + text);
                        labelSelectedFile.Content = text;
                        //textBoxDocumentType.Text = text;
                    }
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            imgAnalyzedDocument.Source = null;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //todo: save template or analyzed document
        }
        private void ButtonNewTemplate_Click(object sender, RoutedEventArgs e)
        {
            //todo: create template
        }
    }
}
