using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml;
using TallComponents.PDF.Rasterizer;
using Tesseract;
using WpfPrototype.additionalLogic.entities;
using WpfPrototype.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private List<DocFile> analyzedFiles = new List<DocFile>();
        private int pointerToActualAnalyzedFile = 0;
        private DocAttribute lastSelectedDocAttribute;
        private BitmapImage bitmap;

        public Window1()
        {
            InitializeComponent();
            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            CreateTemplateButtons();

            foreach (var docFile in FileEditor.Instance.SettingsEntity.DocFiles)
            {
                if (docFile.DocAttributes.Count == 0)
                {
                    analyzedFiles.Add(docFile);
                }
            }

            //buttonSave.Visibility = Visibility.Hidden;
            if (pointerToActualAnalyzedFile == analyzedFiles.Count - 1)
            {
                buttonRight.IsEnabled = false;
            }
            buttonLeft.IsEnabled = false;

            //todo: templates buttons
            SelectActualAnalyzedFile();

            ShowImage();
        }

        private void ShowImage()
        {
            Uri uri = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png", UriKind.RelativeOrAbsolute);
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = uri;
            bitmap.EndInit();
            imgAnalyzedDocument.Source = bitmap;
            //File.Delete(uri.AbsolutePath);
        }

        private void SelectActualAnalyzedFile()
        {

            if (analyzedFiles[pointerToActualAnalyzedFile].FilePath.EndsWith("pdf"))
            {
                ConvertPdfToPng(analyzedFiles[pointerToActualAnalyzedFile].FilePath, "D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\out.png");
            }
            else if (analyzedFiles[pointerToActualAnalyzedFile].FilePath.EndsWith("png"))
            {
                File.Delete(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png");
                File.Copy(analyzedFiles[pointerToActualAnalyzedFile].FilePath, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png");
            }
            else
            {
                //todo: another file formats to show us png
                if (pointerToActualAnalyzedFile < analyzedFiles.Count - 1)
                {
                    pointerToActualAnalyzedFile = pointerToActualAnalyzedFile + 1;
                    SelectActualAnalyzedFile();
                }
            }
        }

        private void CreateTemplateButtons()
        {
            buttonSave.Content = "Create new template";

            List<Template> templates = new List<Template>();

            foreach (Template template in FileEditor.Instance.SettingsEntity.Templates)
            {
                templates.Add(template);
            }

            listViewTemplates.ItemsSource = templates;
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
            if (listViewTemplates.SelectedItem == null)
            {
                ButtonNewTemplate_Click(sender, e);
            }
            else 
            {
                var docAttrsTemp = listViewAttributes.ItemsSource as List<DocAttribute>;
                List<DocAttribute> docsAttrs = docAttrsTemp.Cast<DocAttribute>().ToList();
                Debug.WriteLine(docsAttrs[0].Name + ", " + docsAttrs[0].Value + ", " + docsAttrs[0].Type + " - " + listViewAttributes.Items.Count);
                if (docsAttrs == null)
                {
                    FileEditor.Instance.AddAttributesToFileAndTemplate(analyzedFiles[pointerToActualAnalyzedFile].FilePath, new List<DocAttribute>());
                }
                else
                {
                    FileEditor.Instance.AddAttributesToFileAndTemplate(analyzedFiles[pointerToActualAnalyzedFile].FilePath, docsAttrs);
                }
            }
            //todo: save template or analyzed document
        }

        private void ButtonNewTemplate_Click(object sender, RoutedEventArgs e)
        {
            listViewTemplates.ItemsSource = new List<Template>();
            buttonSave.Visibility = Visibility.Hidden;

            //todo: new template form
            panel.Height = new GridLength(91, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            templateAndAttributeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateAndAttributeStackPanel.Children.Clear();
            Label lbl = new Label();
            lbl.Content = "Template name: ";
            TextBox txtBox = new TextBox();
            templateAndAttributeStackPanel.Children.Add(lbl);
            templateAndAttributeStackPanel.Children.Add(txtBox);
            //todo: change label to space
            templateAndAttributeStackPanel.Children.Add(new Label());
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewTemplate = new Button();
            buttonNewTemplate.Content = "Create";
            buttonNewTemplate.Click += (s, e) =>
            {
                Template template = new Template(txtBox.Text);
                template.DocFiles.Add(new DocFile(analyzedFiles[pointerToActualAnalyzedFile].FilePath, new List<DocAttribute>()));
                FileEditor.Instance.AddNewTemplate(template);

                panel.Height = new GridLength(0, GridUnitType.Star);
                CreateAttributeListView(template);
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewTemplate);
        }

        private void CreateAttributeListView(Template selectedTemplateName)
        {
            listView2.Height = new GridLength(81, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            panel.Height = new GridLength(10, GridUnitType.Star);
            templateAndAttributeStackPanel.Children.Clear();
            List<DocAttribute> docAttributes = new List<DocAttribute>();

            Template selectedTemplate = FileEditor.Instance.SettingsEntity.Templates.Find(x => x.Name == selectedTemplateName.Name);
            if (selectedTemplate.AllDocAttributes.Count > 0)
            {
                foreach (DocAttribute atribute in selectedTemplate.AllDocAttributes)
                {
                    docAttributes.Add(atribute);
                }
            }

            listViewAttributes.ItemsSource = docAttributes;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Add new attribute";
            buttonNewAttribute.Click += (s, e) =>
            {
                ButtonNewAttribute_Click(selectedTemplate);
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewAttribute);

        }

        private void ButtonNewAttribute_Click(Template template)
        {
            listViewTemplates.ItemsSource = new List<Template>();
            //buttonSave.Visibility = Visibility.Hidden;

            //todo: new template form
            panel.Height = new GridLength(51, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            templateAndAttributeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateAndAttributeStackPanel.Children.Clear();
            Label lbl = new Label();
            lbl.Content = "Attribute name: ";
            TextBox txtBox = new TextBox();
            Label lblComboBox = new Label();
            lblComboBox.Content = "Type: ";
            ComboBox attributeComboBox = new ComboBox();
            attributeComboBox.Items.Add("Number");
            attributeComboBox.Items.Add("Text");
            attributeComboBox.Items.Add("Date");
            attributeComboBox.Items.Add("Picture");
            templateAndAttributeStackPanel.Children.Add(lbl);
            templateAndAttributeStackPanel.Children.Add(txtBox);
            templateAndAttributeStackPanel.Children.Add(lblComboBox);
            templateAndAttributeStackPanel.Children.Add(attributeComboBox);
            //todo: change label to space
            templateAndAttributeStackPanel.Children.Add(new Label());
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Create";
            buttonNewAttribute.Click += (s, e) =>
            {
                DocAttribute docAttribute = new DocAttribute(txtBox.Text, "", attributeComboBox.SelectedValue.ToString(), 0, 0, 0, 0);
                FileEditor.Instance.AddAttributeToTemplate(template, docAttribute);
                analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.Add(docAttribute);

                panel.Height = new GridLength(10, GridUnitType.Star);
                CreateAttributeListView(template);
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewAttribute);
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            pointerToActualAnalyzedFile = pointerToActualAnalyzedFile + 1;
            if (pointerToActualAnalyzedFile == analyzedFiles.Count - 1)
            {
                buttonRight.IsEnabled = false;
            }
            if (pointerToActualAnalyzedFile == 0)
            {
                buttonLeft.IsEnabled = false;
            }
            else
            {
                buttonLeft.IsEnabled = true;
            }
            SelectActualAnalyzedFile();

            ShowImage();
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            pointerToActualAnalyzedFile = pointerToActualAnalyzedFile - 1;
            if (pointerToActualAnalyzedFile == analyzedFiles.Count - 1)
            {
                buttonRight.IsEnabled = false;
            }
            else 
            {
                buttonRight.IsEnabled = true;
            }
            if (pointerToActualAnalyzedFile == 0)
            {
                buttonLeft.IsEnabled = false;
            }
            else
            {
                buttonLeft.IsEnabled = true;
            }
            SelectActualAnalyzedFile();

            ShowImage();
        }

        private void ListViewTemplatesAndAttributes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            buttonSave.Content = "Save";
            buttonSave.Visibility = Visibility.Visible;
            Template selectedTemplate = (Template)listViewTemplates.SelectedItems[0];
            FileEditor.Instance.AddFileToTemplate(selectedTemplate.Name, analyzedFiles[pointerToActualAnalyzedFile]);
            //todo: show listview with attributes
            CreateAttributeListView(selectedTemplate);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DocAttribute atr;
            try
            {
                atr = (sender as FrameworkElement).DataContext as DocAttribute;
            }catch (NullReferenceException ex)
            {
                atr = sender as DocAttribute;
            }
            Debug.WriteLine("selected attribute: " + atr.Name);
            CalculateAverageAttributeLocation(atr);
            if (atr != null && atr.EndingXLocation != 0 && atr.EndingYLocation != 0 && atr.StartingXLocation != atr.EndingXLocation && atr.StartingYLocation != atr.EndingYLocation)
            {
                Bitmap outputImage = new Bitmap((int)bitmap.PixelWidth, (int)bitmap.PixelHeight);
                Graphics G = Graphics.FromImage(outputImage);


                G.DrawImage(System.Drawing.Image.FromFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png"), 0, 0);


                for (global::System.Int32 i = atr.StartingXLocation; i < atr.EndingXLocation; i++)
                {
                    outputImage.SetPixel(i, atr.StartingYLocation, System.Drawing.Color.FromArgb(255, 0, 0));
                    outputImage.SetPixel(i, atr.EndingYLocation, System.Drawing.Color.FromArgb(255, 0, 0));
                }
                for (global::System.Int32 i = atr.StartingYLocation; i < atr.EndingYLocation; i++)
                {
                    outputImage.SetPixel(atr.StartingXLocation, i, System.Drawing.Color.FromArgb(255, 0, 0));
                    outputImage.SetPixel(atr.EndingXLocation, i, System.Drawing.Color.FromArgb(255, 0, 0));
                }

                using (var memory = new MemoryStream())
                {
                    outputImage.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memory.Position = 0;

                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = memory;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }
                imgAnalyzedDocument.Source = bitmap;
                Debug.WriteLine("changedPNG saved");
            }
            else 
            {
                ShowImage();
            }
            lastSelectedDocAttribute = atr;
        }

        private void CalculateAverageAttributeLocation(DocAttribute atr)
        {
            int count = 0;
            int sumOfStartingXLocation = 0;
            int sumOfStartingYLocation = 0;
            int sumOfEndingXLocation = 0;
            int sumOfEndingYLocation = 0;
            foreach (DocFile docFile in FileEditor.Instance.SettingsEntity.DocFiles)
            {
                DocAttribute  docAttr = docFile.DocAttributes.Find(x => x.Name == atr.Name);
                if (docAttr != null && docAttr.EndingYLocation != 0 && docAttr.StartingXLocation != docAttr.EndingXLocation && docAttr.StartingYLocation != docAttr.EndingYLocation)
                {
                    sumOfStartingXLocation += docAttr.StartingXLocation;
                    sumOfStartingYLocation += docAttr.StartingYLocation;
                    sumOfEndingXLocation += docAttr.EndingXLocation;
                    sumOfEndingYLocation += docAttr.EndingYLocation;
                    count = count + 1;
                }
            }
            if(count > 0)
            {
                atr.StartingXLocation = sumOfStartingXLocation / count;
                atr.StartingYLocation = sumOfStartingYLocation / count;
                atr.EndingXLocation = sumOfEndingXLocation / count;
                atr.EndingYLocation = sumOfEndingYLocation / count;
            }
        }

        private void imgAnalyzedDocument_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lastSelectedDocAttribute != null)
            {
                System.Windows.Point p = e.GetPosition(imgAnalyzedDocument);
                lastSelectedDocAttribute.StartingXLocation = (int)p.X;
                lastSelectedDocAttribute.StartingYLocation = (int)p.Y;

                //todo: show what is selected
            }
        }
        
        private void imgAnalyzedDocument_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lastSelectedDocAttribute != null)
            {
                System.Windows.Point p = e.GetPosition(imgAnalyzedDocument);
                lastSelectedDocAttribute.EndingXLocation = (int)p.X;
                lastSelectedDocAttribute.EndingYLocation = (int)p.Y;

                double percentWidth = (double)bitmap.PixelWidth / imgAnalyzedDocument.ActualWidth;
                double percentHeight = (double)bitmap.PixelHeight / imgAnalyzedDocument.ActualHeight;
                lastSelectedDocAttribute.StartingXLocation = (int)(lastSelectedDocAttribute.StartingXLocation * percentWidth);
                lastSelectedDocAttribute.StartingYLocation = (int)(lastSelectedDocAttribute.StartingYLocation * percentHeight);
                lastSelectedDocAttribute.EndingXLocation = (int)(lastSelectedDocAttribute.EndingXLocation * percentWidth);
                lastSelectedDocAttribute.EndingYLocation = (int)(lastSelectedDocAttribute.EndingYLocation * percentHeight);

                Debug.WriteLine("starting x = " + lastSelectedDocAttribute.StartingXLocation + "\tending x = " + lastSelectedDocAttribute.EndingXLocation);
                Debug.WriteLine("starting y = " + lastSelectedDocAttribute.StartingYLocation + "\tending y = " + lastSelectedDocAttribute.EndingYLocation);


                TextBox_GotFocus(lastSelectedDocAttribute, e);

                //todo: save that when you click on save button - NOT NOW
                int indexOfxistingAttribute = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.FindIndex(x => x.Name == lastSelectedDocAttribute.Name);
                if (indexOfxistingAttribute >= 0)
                {
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingXLocation = lastSelectedDocAttribute.StartingXLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingYLocation = lastSelectedDocAttribute.StartingYLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingXLocation = lastSelectedDocAttribute.EndingXLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingYLocation = lastSelectedDocAttribute.EndingYLocation;
                }
                else
                {
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.Add(lastSelectedDocAttribute);
                }
                //FileEditor.Instance.AddAttributeToFileAndTemplate(analyzedFiles[pointerToActualAnalyzedFile].FilePath, lastSelectedDocAttribute);
                //todo: read selected part of image to value (textBox)
                //show what is selected
            }
        }
    }
}
