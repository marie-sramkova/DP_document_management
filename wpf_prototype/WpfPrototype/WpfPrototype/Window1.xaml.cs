﻿using System;
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

            CreateTemplateButtons();

            foreach (var docFile in FileEditor.Instance.SettingsEntity.DocFiles)
            {
                if (docFile.DocAttributes.Count == 0)
                {
                    analyzedFiles.Add(docFile);
                }
            }

            buttonSave.Visibility = Visibility.Hidden;
            if (pointerToActualAnalyzedFile == analyzedFiles.Count - 1)
            {
                buttonRight.IsEnabled = false;
            }
            buttonLeft.IsEnabled = false;

            //todo: templates buttons
            SelectingActualAnalyzedFile();

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

        private void SelectingActualAnalyzedFile()
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
                    SelectingActualAnalyzedFile();
                }
            }
        }

        private void CreateTemplateButtons()
        {
            //todo: delete if panel wont be used
            //panel.Height = new GridLength(91, GridUnitType.Star);
            //listView.Height = new GridLength(0, GridUnitType.Star);

            ////todo: iteration by templates
            //for (int i = 0; i < 5; i++)
            //{
            //    Button buttonTemplate = new Button();
            //    buttonTemplate.Content = "Template name";
            //    buttonTemplate.Width = 250;
            //    buttonTemplate.Click += (s, e) =>
            //    {
            //        //save template and continue with analyzing document by choosen template
            //    };
            //    templateStackPanel.Children.Add(buttonTemplate);
            //    templateStackPanel.VerticalAlignment = VerticalAlignment.Center;
            //    //todo: change labels to spaces!!!!!
            //    templateStackPanel.Children.Add(new Label());
            //}

            //Button buttonNewTemplate = new Button();
            //buttonNewTemplate.Content = "Create new template";
            //buttonNewTemplate.Width = 250;
            //buttonNewTemplate.Click += (s, e) =>
            //{
            //    ButtonNewTemplate_Click(s, e);
            //};
            //templateStackPanel.Children.Add(buttonNewTemplate);
            //templateStackPanel.VerticalAlignment = VerticalAlignment.Center;


            foreach (Template template in FileEditor.Instance.SettingsEntity.Templates)
            {
                Label labelTemplate = new Label();
                labelTemplate.Content = template.Name;

                listViewTemplatesAndAttributes.Items.Add(labelTemplate);
                listViewTemplatesAndAttributes.VerticalAlignment = VerticalAlignment.Center;
            }

            Button buttonNewTemplate = new Button();
            buttonNewTemplate.Content = "Create new template";
            buttonNewTemplate.Click += (s, e) =>
            {
                ButtonNewTemplate_Click(s, e);
            };
            listViewTemplatesAndAttributes.Items.Add(buttonNewTemplate);
            listViewTemplatesAndAttributes.HorizontalContentAlignment = HorizontalAlignment.Center;
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
            
            //todo: save template or analyzed document
        }
        private void ButtonNewTemplate_Click(object sender, RoutedEventArgs e)
        {
            listViewTemplatesAndAttributes.Items.Clear();

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
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewTemplate = new Button();
            buttonNewTemplate.Content = "Create";
            buttonNewTemplate.Click += (s, e) =>
            {
                Template template = new Template(txtBox.Text);
                template.DocFiles.Add(new DocFile(analyzedFiles[pointerToActualAnalyzedFile].FilePath, new List<DocAttribute>()));
                FileEditor.Instance.AddNewTemplate(template);

                CreateAttributeListView();
            };
            templateStackPanel.Children.Add(buttonNewTemplate);
        }

        private void CreateAttributeListView()
        {
            panel.Height = new GridLength(0, GridUnitType.Star);
            listView.Height = new GridLength(91, GridUnitType.Star);
            //throw new NotImplementedException();
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
            SelectingActualAnalyzedFile();

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
            SelectingActualAnalyzedFile();

            ShowImage();
        }

        private void ListViewTemplatesAndAttributes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            string selectedTemplate = listViewTemplatesAndAttributes.SelectedValue.ToString();
            if (selectedTemplate.StartsWith("System.Windows.Controls.Label: "))
            {
                selectedTemplate = selectedTemplate.Substring(31);
                //todo: add actual file to selectedTemplate
            }
            else if (selectedTemplate.StartsWith("System.Windows.Controls.Button: "))
            {
                ButtonNewTemplate_Click(sender, e);
            }
        }
    }
}
