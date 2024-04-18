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
using System.Xml;
using TallComponents.PDF.Rasterizer;
using Tesseract;
using WpfPrototype.additionalLogic.entities;

namespace WpfPrototype
{
    /// <summary>
    /// Interakční logika pro Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private List<DocFile> analyzedFiles = new List<DocFile>();
        private int pointerToActualAnalyzedFile = 0;

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
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = uri;
            bitmap.EndInit();
            imgAnalyzedDocument.Source = bitmap;
            File.Delete(uri.AbsolutePath);
        }

        private void SelectActualAnalyzedFile()
        {

            if (analyzedFiles[pointerToActualAnalyzedFile].FilePath.EndsWith("pdf"))
            {
                ConvertPdfToPng(analyzedFiles[pointerToActualAnalyzedFile].FilePath, "D:\\sramk\\Documents\\vysoka_skola\\diplomka\\git_official\\DP_document_management\\wpf_prototype\\WpfPrototype\\WpfPrototype\\data\\out.png");
            }
            else if (analyzedFiles[pointerToActualAnalyzedFile].FilePath.EndsWith("png"))
            {
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
                var filterResult = listViewAttributes.ItemsSource as List<DocAttribute>;
                List<DocAttribute> docsAttrs = filterResult.Cast<DocAttribute>().ToList();
                Debug.WriteLine(docsAttrs[0].Name + ", " + docsAttrs[0].Value + ", " + docsAttrs[0].Type + " - " + listViewAttributes.Items.Count);
                //FileEditor.Instance.AddAttributesToFile(analyzedFiles[pointerToActualAnalyzedFile].FilePath, new List<DocAttribute>());
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
            templateStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateStackPanel.Children.Clear();
            Label lbl = new Label();
            lbl.Content = "Template name: ";
            TextBox txtBox = new TextBox();
            templateStackPanel.Children.Add(lbl);
            templateStackPanel.Children.Add(txtBox);
            //todo: change label to space
            templateStackPanel.Children.Add(new Label());
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
            templateStackPanel.Children.Add(buttonNewTemplate);
        }

        private void CreateAttributeListView(Template selectedTemplateName)
        {
            listView2.Height = new GridLength(81, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            panel.Height = new GridLength(10, GridUnitType.Star);
            templateStackPanel.Children.Clear();
            List<DocAttribute> docAttributes = new List<DocAttribute>();
            //todo: read attributes from template
            docAttributes.Add(new DocAttribute("attr", "value", "type", 0, 0, 0, 0));
            docAttributes.Add(new DocAttribute("attr", "value", "type", 0, 0, 0, 0));

            Template selectedTemplate = FileEditor.Instance.SettingsEntity.Templates.Find(x => x.Name == selectedTemplateName.Name);
            if (selectedTemplate.AllDocAttributes.Count > 0)
            {
                foreach (DocAttribute atribute in selectedTemplate.AllDocAttributes)
                {
                    docAttributes.Add(atribute);
                }
            }
            else
            {

                //todo: not added attributes yet in template -> add new attribute
            }

            listViewAttributes.ItemsSource = docAttributes;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Add new attribute";
            buttonNewAttribute.Click += (s, e) =>
            {
                ButtonNewAttribute_Click(selectedTemplate);
            };
            templateStackPanel.Children.Add(buttonNewAttribute);

        }

        private void ButtonNewAttribute_Click(Template template)
        {
            listViewTemplates.ItemsSource = new List<Template>();
            //buttonSave.Visibility = Visibility.Hidden;

            //todo: new template form
            panel.Height = new GridLength(71, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            templateStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateStackPanel.Children.Clear();
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
            templateStackPanel.Children.Add(lbl);
            templateStackPanel.Children.Add(txtBox);
            templateStackPanel.Children.Add(lblComboBox);
            templateStackPanel.Children.Add(attributeComboBox);
            //todo: change label to space
            templateStackPanel.Children.Add(new Label());
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Create";
            buttonNewAttribute.Click += (s, e) =>
            {
                DocAttribute docAttribute = new DocAttribute(txtBox.Text, "", attributeComboBox.SelectedValue.ToString(), 0, 0, 0, 0);
                FileEditor.Instance.AddAttributeToTemplate(template, docAttribute);

                panel.Height = new GridLength(10, GridUnitType.Star);
                CreateAttributeListView(template);
            };
            templateStackPanel.Children.Add(buttonNewAttribute);
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
            DocAttribute atr = (sender as FrameworkElement).DataContext as DocAttribute;
            Debug.WriteLine("selected attribute: " + atr.Name);
        }
    }
}
