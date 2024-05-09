﻿using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;
using System.Xml.Serialization;
using TallComponents.PDF.Rasterizer;
using Tesseract;
using WpfPrototype.additionalLogic;
using WpfPrototype.additionalLogic.entities;
using WpfPrototype.additionalLogic.ocr;
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
        TesseractOCR tesseractORM;
        BtnSaveState btnSaveState = BtnSaveState.CREATE_TEMPLATE;
        bool changeValueOfTextBoxAvailable = false;

        public class Model : NotifyPropertyChangedBase 
        {
            private BindingList<DocAttribute> _BindingAttributes;
            public BindingList<DocAttribute> BindingAttributes { get { return _BindingAttributes; } set { _BindingAttributes = value; RaisePropertyChanged(nameof(BindingAttributes)); } }
            private BindingList<TemplateWithPercentage> _BindingTemplates;
            public BindingList<TemplateWithPercentage> BindingTemplates { get { return _BindingTemplates; } set { _BindingTemplates = value; RaisePropertyChanged(nameof(BindingTemplates)); } }
        }

        public class TemplateWithPercentage : Template
        {
            private double _SimilarityPercentage;
            public double SimilarityPercentage { get { return _SimilarityPercentage; } set { _SimilarityPercentage = value; RaisePropertyChanged(nameof(SimilarityPercentage)); } }

            public TemplateWithPercentage(string text) : base(text)
            {
                Name = text;
                AllDocAttributes = new BindingList<DocAttribute>();
                DocFiles = new BindingList<DocFile>();
                SimilarityPercentage = 0.0;
            }
        }

        private Model model;


        public Window1()
        {
            this.model = new Model();
            model.BindingAttributes = new BindingList<DocAttribute>();
            model.BindingTemplates = new BindingList<TemplateWithPercentage>();
            this.DataContext = model;
            InitializeComponent();

            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = WindowStyle.None;

            tesseractORM = new TesseractOCR();

            foreach (var docFile in FileEditor.Instance.SettingsEntity.DocFiles)
            {
                if (docFile.DocAttributes.Count == 0 && (docFile.FilePath != UserSettings.settingsDocumentsFilePath && docFile.FilePath != UserSettings.settingsTemplatesFilePath))
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


            CreateTemplateButtons();

            //todo: templates buttons
            SelectActualAnalyzedFile();

            ShowImage();


            foreach (var template in this.model.BindingTemplates)
            {
                double finalPercentage = CompareAllImagesFromTemplateWithCurrentImageAndReturnSimilarityPercentage(template);
                template.SimilarityPercentage = finalPercentage;
            }
        }

        private double CompareAllImagesFromTemplateWithCurrentImageAndReturnSimilarityPercentage(Template template)
        {
            double sumOfPercentage = 0;
            int countOfPercentage = 0;
            double finalPercentage = 0.0;
            foreach (DocFile file in template.DocFiles)
            {
                if (file.FilePath.EndsWith("pdf"))
                {
                    ConvertPdfToImage(file.FilePath, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\data\\imageToCompare.jpg");
                }
                else if (file.FilePath.EndsWith("png"))
                {
                    System.Drawing.Image imageToCompare = null;
                    using (FileStream fs = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read))
                    {
                        imageToCompare = System.Drawing.Image.FromStream(fs);
                    }
                    imageToCompare.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/imageToCompare.jpg");
                }
                else
                {
                    try
                    {
                        System.Drawing.Image imageToCompare = null;
                        using (FileStream fs = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read))
                        {
                            imageToCompare = System.Drawing.Image.FromStream(fs);
                        }
                        imageToCompare.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/imageToCompare.jpg");
                    }
                    catch (Exception ex)
                    {
                        //todo: another file formats to show as png
                    }
                }
                try
                {
                    System.Drawing.Image actualImage = null;
                    using (FileStream fs = new FileStream(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.jpg", FileMode.Open, FileAccess.Read))
                    {
                        actualImage = System.Drawing.Image.FromStream(fs);
                    }
                    
                    System.Drawing.Image imageToCompare = null;
                    using (FileStream fs = new FileStream(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/imageToCompare.jpg", FileMode.Open, FileAccess.Read))
                    {
                        imageToCompare = System.Drawing.Image.FromStream(fs);
                    }
                    
                    double similarityPercentage = ImageComparator.CompareImagesAndReturnPercentageOfSimilarity(imageToCompare, actualImage);
                    sumOfPercentage = sumOfPercentage + similarityPercentage;
                    countOfPercentage = countOfPercentage + 1;
                }
                catch (Exception e)
                {
                    //todo: cant compare these to images
                }
            }

            finalPercentage = sumOfPercentage / countOfPercentage;
            return finalPercentage;
        }

        private void ShowImage()
        {
            
            Uri uri = new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.jpg", UriKind.RelativeOrAbsolute);
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
                ConvertPdfToImage(analyzedFiles[pointerToActualAnalyzedFile].FilePath, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\data\\out.jpg");
            }
            else if (analyzedFiles[pointerToActualAnalyzedFile].FilePath.EndsWith("png"))
            {
                System.Drawing.Image imageToCompare = null;
                using (FileStream fs = new FileStream(analyzedFiles[pointerToActualAnalyzedFile].FilePath, FileMode.Open, FileAccess.Read))
                {
                    imageToCompare = System.Drawing.Image.FromStream(fs);
                }
                imageToCompare.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.jpg");
            }
            else
            {
                try
                {
                    System.Drawing.Image imageToCompare = null;
                    using (FileStream fs = new FileStream(analyzedFiles[pointerToActualAnalyzedFile].FilePath, FileMode.Open, FileAccess.Read))
                    {
                        imageToCompare = System.Drawing.Image.FromStream(fs);
                    }
                    imageToCompare.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.jpg");
                }
                catch(Exception ex)
                {
                    //todo: cannot convert file to out.png to show image view
                    if (pointerToActualAnalyzedFile < analyzedFiles.Count - 1)
                    {
                        pointerToActualAnalyzedFile = pointerToActualAnalyzedFile + 1;
                        SelectActualAnalyzedFile();
                    }
                }
                //todo: another file formats to show as png
            }
        }

        private void CreateTemplateButtons()
        {
            buttonSave.Content = "Create new template";

            BindingList<TemplateWithPercentage> templates = new BindingList<TemplateWithPercentage>();

            foreach (Template template in FileEditor.Instance.SettingsEntity.Templates)
            {
                TemplateWithPercentage newTemplate = new TemplateWithPercentage(template.Name);
                newTemplate.DocFiles = template.DocFiles;
                newTemplate.AllDocAttributes = template.AllDocAttributes;
                templates.Add(newTemplate);
            }

            model.BindingTemplates = templates;
        }

        private static void ConvertPdfToImage(String inputPdfPath, String outputPngPath)
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            imgAnalyzedDocument.Source = null;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSaveState == BtnSaveState.CREATE_TEMPLATE)
            {
                ButtonNewTemplate_Click(sender, e);
            }
            else if(btnSaveState == BtnSaveState.SAVE_ANALYZED_FILE)
            {
                BindingList<DocAttribute> docsAttrs = model.BindingAttributes as BindingList<DocAttribute>;
                if (docsAttrs == null)
                {
                    return;
                }
                else
                {
                    FileEditor.Instance.AddAttributesToFileAndTemplate(analyzedFiles[pointerToActualAnalyzedFile].FilePath, docsAttrs);
                    analyzedFiles.RemoveAt(pointerToActualAnalyzedFile);
                    if (analyzedFiles.Count == 0)
                    {
                        templateAndAttributeStackPanel.Children.Clear();
                        ButtonBack_Click(sender, e);
                    }
                    else
                    {
                        if (pointerToActualAnalyzedFile < analyzedFiles.Count - 1)
                        {
                            templateAndAttributeStackPanel.Children.Clear();
                            pointerToActualAnalyzedFile = pointerToActualAnalyzedFile - 1;
                            ButtonRight_Click(sender, e);
                        }
                        else
                        {
                            templateAndAttributeStackPanel.Children.Clear();
                            pointerToActualAnalyzedFile = analyzedFiles.Count;
                            ButtonLeft_Click(sender, e);
                        }
                    }
                }
            }
            //todo: save template or analyzed document
        }

        private void ButtonNewTemplate_Click(object sender, RoutedEventArgs e)
        {
            model.BindingTemplates = new BindingList<TemplateWithPercentage>();
            buttonSave.Visibility = Visibility.Hidden;

            //todo: new template form
            panel.Height = new GridLength(91, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            templateAndAttributeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateAndAttributeStackPanel.Children.Clear();
            Label lbl = new Label();
            lbl.Content = "Template name: ";
            TextBox txtBoxTemplateName = new TextBox();
            templateAndAttributeStackPanel.Children.Add(lbl);
            templateAndAttributeStackPanel.Children.Add(txtBoxTemplateName);
            //todo: change label to space
            templateAndAttributeStackPanel.Children.Add(new Label());
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewTemplate = new Button();
            buttonNewTemplate.Content = "Create";
            buttonNewTemplate.Click += (s, e) =>
            {
                if (txtBoxTemplateName.Text == "")
                {
                    return;
                }
                Template template = new Template(txtBoxTemplateName.Text);
                template.DocFiles.Add(new DocFile(analyzedFiles[pointerToActualAnalyzedFile].FilePath, new BindingList<DocAttribute>()));
                FileEditor.Instance.AddNewTemplate(template);

                panel.Height = new GridLength(0, GridUnitType.Star);
                CreateAttributeListView(template);
                buttonSave.Visibility = Visibility.Visible;
                buttonSave.Content = "Save";
                btnSaveState = BtnSaveState.SAVE_ANALYZED_FILE;
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewTemplate);
        }

        private void CreateAttributeListView(Template selectedTemplateName)
        {
            listView2.Height = new GridLength(81, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            panel.Height = new GridLength(10, GridUnitType.Star);
            templateAndAttributeStackPanel.Children.Clear();

            Template selectedTemplate = FileEditor.Instance.SettingsEntity.Templates.SingleOrDefault(x => x.Name == selectedTemplateName.Name);
            BindingList<DocAttribute> allAttributes = new BindingList<DocAttribute>();
            allAttributes = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes;
            foreach (var attr in selectedTemplate.AllDocAttributes)
            {
                if (!allAttributes.Any(x => x.Name == attr.Name))
                {
                    allAttributes.Add(attr);
                }
            }

            ShowImageWithAllAttributeBoundaries();

            model.BindingAttributes = allAttributes;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Add new attribute";
            buttonNewAttribute.Click += (s, e) =>
            {
                ButtonNewAttribute_Click(selectedTemplate);
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewAttribute);
        }

        private void ShowImageWithAllAttributeBoundaries()
        {
            List<DocAttribute> docAttributes = new List<DocAttribute>();
            Template selectedTemplate = FileEditor.Instance.SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(y => y.FilePath == analyzedFiles[pointerToActualAnalyzedFile].FilePath) != null);
            BindingList<DocAttribute> allAttributes = new BindingList<DocAttribute>();
            allAttributes = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes;
            if (selectedTemplate != null)
            {
                foreach (var attr in selectedTemplate.AllDocAttributes)
                {
                    if (!allAttributes.Any(x => x.Name == attr.Name))
                    {
                        allAttributes.Add(attr);
                    }
                }
            }


            if (selectedTemplate != null && allAttributes.Count > 0)
            {
                foreach (DocAttribute attribute in allAttributes)
                {
                    DocAttribute actualAttribute = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == attribute.Name);
                    if (actualAttribute == null)
                    {
                        CalculateAverageAttributeLocation(attribute);
                        if (attribute != null && (attribute.EndingXLocation == 0 || attribute.EndingYLocation == 0 || attribute.StartingXLocation == attribute.EndingXLocation || attribute.StartingYLocation == attribute.EndingYLocation))
                        {
                            attribute.Value = "";
                        }
                        docAttributes.Add(attribute);
                        if (attribute != null && attribute.EndingXLocation != 0 && attribute.EndingYLocation != 0 && attribute.StartingXLocation != attribute.EndingXLocation && attribute.StartingYLocation != attribute.EndingYLocation)
                        {
                            AddAttributeBoundariesToBitmap(attribute);
                        }
                    }
                    else
                    {
                        docAttributes.Add(actualAttribute);
                        if (actualAttribute != null && actualAttribute.EndingXLocation != 0 && actualAttribute.EndingYLocation != 0 && actualAttribute.StartingXLocation != actualAttribute.EndingXLocation && actualAttribute.StartingYLocation != attribute.EndingYLocation)
                        {
                            AddAttributeBoundariesToBitmap(actualAttribute);
                        }
                    }
                }
            }
        }

        private void ButtonNewAttribute_Click(Template template)
        {
            model.BindingTemplates = new BindingList<TemplateWithPercentage>();
            buttonSave.Visibility = Visibility.Hidden;

            //todo: new template form
            panel.Height = new GridLength(51, GridUnitType.Star);
            listView.Height = new GridLength(0, GridUnitType.Star);
            templateAndAttributeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            templateAndAttributeStackPanel.Children.Clear();
            Label lbl = new Label();
            lbl.Content = "Attribute name: ";
            TextBox txtBoxAttributeName = new TextBox();
            Label lblComboBox = new Label();
            lblComboBox.Content = "Type: ";
            ComboBox attributeComboBox = new ComboBox();
            attributeComboBox.Items.Add("Number");
            attributeComboBox.Items.Add("Text");
            attributeComboBox.Items.Add("Date");
            templateAndAttributeStackPanel.Children.Add(lbl);
            templateAndAttributeStackPanel.Children.Add(txtBoxAttributeName);
            templateAndAttributeStackPanel.Children.Add(lblComboBox);
            templateAndAttributeStackPanel.Children.Add(attributeComboBox);
            //todo: change label to space
            templateAndAttributeStackPanel.Children.Add(new Label());
            //buttonSave.Visibility = Visibility.Visible;

            Button buttonNewAttribute = new Button();
            buttonNewAttribute.Content = "Create";
            buttonNewAttribute.Click += (s, e) =>
            {
                if (attributeComboBox.SelectedValue == null || txtBoxAttributeName.Text == "")
                {
                    return;
                }
                buttonSave.Visibility = Visibility.Visible;
                DocAttribute docAttribute = new DocAttribute(txtBoxAttributeName.Text, "", attributeComboBox.SelectedValue.ToString(), 0, 0, 0, 0);
                FileEditor.Instance.AddAttributeToTemplate(template, docAttribute);
                analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.Add(docAttribute);

                panel.Height = new GridLength(10, GridUnitType.Star);
                CreateAttributeListView(template);
            };
            templateAndAttributeStackPanel.Children.Add(buttonNewAttribute);
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            buttonSave.Visibility = Visibility.Visible;
            btnSaveState = BtnSaveState.CREATE_TEMPLATE;
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
            listView2.Height = new GridLength(0, GridUnitType.Star);
            listView.Height = new GridLength(91, GridUnitType.Star);
            panel.Height = new GridLength(0, GridUnitType.Star);

            CreateTemplateButtons();
            SelectActualAnalyzedFile();

            ShowImage();

            foreach (var template in this.model.BindingTemplates)
            {
                double finalPercentage = CompareAllImagesFromTemplateWithCurrentImageAndReturnSimilarityPercentage(template);
                template.SimilarityPercentage = finalPercentage;
            }
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            buttonSave.Visibility = Visibility.Visible;
            btnSaveState = BtnSaveState.CREATE_TEMPLATE;
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
            listView2.Height = new GridLength(0, GridUnitType.Star);
            listView.Height = new GridLength(91, GridUnitType.Star);
            panel.Height = new GridLength(0, GridUnitType.Star);
            CreateTemplateButtons();
            SelectActualAnalyzedFile();

            ShowImage();

            foreach (var template in this.model.BindingTemplates)
            {
                double finalPercentage = CompareAllImagesFromTemplateWithCurrentImageAndReturnSimilarityPercentage(template);
                template.SimilarityPercentage = finalPercentage;
            }
        }

        private void ListViewTemplatesAndAttributes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            changeValueOfTextBoxAvailable = true;
            buttonSave.Content = "Save";
            buttonSave.Visibility = Visibility.Visible;
            btnSaveState = BtnSaveState.SAVE_ANALYZED_FILE;
            Template selectedTemplate = (Template)listViewTemplates.SelectedItems[0];
            FileEditor.Instance.AddFileToTemplate(selectedTemplate.Name, analyzedFiles[pointerToActualAnalyzedFile]);
            //todo: show listview with attributes
            CreateAttributeListView(selectedTemplate);

            foreach (var attr in model.BindingAttributes)
            {
                CalculateAverageAttributeLocation(attr);
            }
            changeValueOfTextBoxAvailable = false;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DocAttribute attr;
            try
            {
                attr = (sender as FrameworkElement).DataContext as DocAttribute;
            }
            catch (NullReferenceException ex)
            {
                attr = sender as DocAttribute;
            }
            if (attr != null && attr.EndingXLocation != 0 && attr.EndingYLocation != 0 && attr.StartingXLocation != attr.EndingXLocation && attr.StartingYLocation != attr.EndingYLocation)
            {
                ShowImage();
                AddAttributeBoundariesToBitmap(attr);
            }
            else
            {
                ShowImage();
            }
            lastSelectedDocAttribute = attr;
            listViewAttributes.SelectedItem = attr;
        }

        private string GetAttributeValue(DocAttribute attr)
        {
            if(attr.StartingXLocation == attr.EndingXLocation || attr.StartingYLocation == attr.EndingYLocation)
            {
                return "";
            }
            Bitmap source = new Bitmap(ConvertBitmapImageToDrawingBitmap(bitmap));
            Bitmap cuttedBitmap = source.Clone(new System.Drawing.Rectangle(attr.StartingXLocation, attr.StartingYLocation, attr.EndingXLocation - attr.StartingXLocation, attr.EndingYLocation - attr.StartingYLocation), source.PixelFormat);
            cuttedBitmap.Save(UserSettings.directoryPath+"\\tmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            ImageConverter converter = new ImageConverter();
            string text = tesseractORM.GetTextFromImage((byte[])converter.ConvertTo(cuttedBitmap, typeof(byte[])));

            File.Delete(UserSettings.directoryPath + "\\tmp.jpg");
            return text;
        }

        private void AddAttributeBoundariesToBitmap(DocAttribute attr)
        {
            if (attr.EndingXLocation > bitmap.PixelWidth || attr.EndingYLocation > bitmap.PixelHeight)
            {
                attr.Value = "";
                imgAnalyzedDocument.Source = bitmap;
                return;
            }
            Bitmap outputImage = new Bitmap((int)bitmap.PixelWidth, (int)bitmap.PixelHeight);
            Graphics graphics = Graphics.FromImage(outputImage);

            Bitmap bitmap2 = ConvertBitmapImageToDrawingBitmap(bitmap);


            //G.DrawImage(System.Drawing.Image.FromFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/data/out.png"), 0, 0);
            graphics.DrawImage(bitmap2, 0, 0);

            

            if (attr.StartingXLocation < outputImage.Width && attr.EndingXLocation < outputImage.Width && attr.StartingYLocation < outputImage.Height && attr.EndingYLocation < outputImage.Height)
            {
                for (global::System.Int32 i = attr.StartingXLocation; i < attr.EndingXLocation; i++)
                {

                    outputImage.SetPixel(i, attr.StartingYLocation, System.Drawing.Color.FromArgb(255, 0, 0));
                    outputImage.SetPixel(i, attr.EndingYLocation, System.Drawing.Color.FromArgb(255, 0, 0));

                }
                for (global::System.Int32 i = attr.StartingYLocation; i < attr.EndingYLocation; i++)
                {

                    outputImage.SetPixel(attr.StartingXLocation, i, System.Drawing.Color.FromArgb(255, 0, 0));
                    outputImage.SetPixel(attr.EndingXLocation, i, System.Drawing.Color.FromArgb(255, 0, 0));

                }
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
            if (changeValueOfTextBoxAvailable == true && attr != null && attr.StartingXLocation != 0 && attr.StartingYLocation != 0 && attr.EndingXLocation != attr.StartingXLocation && attr.EndingYLocation != attr.StartingYLocation)
            {
                string value = GetAttributeValue(attr);
                attr.Value = value;
                imgAnalyzedDocument.Source = bitmap;
            }
            imgAnalyzedDocument.Source = bitmap;
        }

        private Bitmap ConvertBitmapImageToDrawingBitmap(BitmapImage bitmapInput)
        {
            int stride = bitmapInput.PixelWidth * 4;
            byte[] buffer = new byte[stride * bitmapInput.PixelHeight];
            bitmapInput.CopyPixels(buffer, stride, 0);

            System.Drawing.Bitmap bitmap2 =
                new System.Drawing.Bitmap(
                    bitmapInput.PixelWidth,
                    bitmapInput.PixelHeight,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            System.Drawing.Imaging.BitmapData bitmapData =
                bitmap2.LockBits(
                    new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    bitmap2.PixelFormat);

            System.Runtime.InteropServices.Marshal.Copy(
                buffer, 0, bitmapData.Scan0, buffer.Length);

            bitmap2.UnlockBits(bitmapData);
            return bitmap2;
        }

        private void CalculateAverageAttributeLocation(DocAttribute attr)
        {
            int count = 0;
            int sumOfStartingXLocation = 0;
            int sumOfStartingYLocation = 0;
            int sumOfEndingXLocation = 0;
            int sumOfEndingYLocation = 0;
            Template selectedTemplate = FileEditor.Instance.SettingsEntity.Templates.SingleOrDefault(x => x.DocFiles.SingleOrDefault(y => y.FilePath == analyzedFiles[pointerToActualAnalyzedFile].FilePath) != null);
            foreach (DocFile docFileFromTemplate in selectedTemplate.DocFiles)
            {
                DocFile docFile = FileEditor.Instance.SettingsEntity.DocFiles.SingleOrDefault(x => x.FilePath == docFileFromTemplate.FilePath);
                DocAttribute docAttr = docFile.DocAttributes.SingleOrDefault(x => x.Name == attr.Name);
                if (docAttr != null && docAttr.EndingYLocation != 0 && docAttr.StartingXLocation != docAttr.EndingXLocation && docAttr.StartingYLocation != docAttr.EndingYLocation)
                {
                    sumOfStartingXLocation += docAttr.StartingXLocation;
                    sumOfStartingYLocation += docAttr.StartingYLocation;
                    sumOfEndingXLocation += docAttr.EndingXLocation;
                    sumOfEndingYLocation += docAttr.EndingYLocation;
                    count = count + 1;
                }
            }
            if (count > 0)
            {
                attr.StartingXLocation = sumOfStartingXLocation / count;
                attr.StartingYLocation = sumOfStartingYLocation / count;
                attr.EndingXLocation = sumOfEndingXLocation / count;
                attr.EndingYLocation = sumOfEndingYLocation / count;
            }
            else 
            {
                attr.StartingXLocation = 0;
                attr.StartingYLocation = 0;
                attr.EndingXLocation = 0;
                attr.EndingYLocation = 0;
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
            changeValueOfTextBoxAvailable = true;
            if (lastSelectedDocAttribute != null)
            {
                System.Windows.Point p = e.GetPosition(imgAnalyzedDocument);
                lastSelectedDocAttribute.EndingXLocation = (int)p.X;
                lastSelectedDocAttribute.EndingYLocation = (int)p.Y;
                organizeLocations();

                recalculateNewLocationsFromPixels(e);
                if (lastSelectedDocAttribute != null)
                {
                    string value = GetAttributeValue(lastSelectedDocAttribute);
                    BindingList<DocAttribute> docsAttrs = model.BindingAttributes as BindingList<DocAttribute>;
                    docsAttrs.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).Value = value;
                    docsAttrs.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).StartingXLocation = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).StartingXLocation;
                    docsAttrs.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).StartingYLocation = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).StartingYLocation;
                    docsAttrs.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).EndingXLocation = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).EndingXLocation;
                    docsAttrs.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).EndingYLocation = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name).EndingYLocation;
                    Debug.WriteLine(value);
                    model.BindingAttributes = docsAttrs;
                    listViewAttributes.SelectedItem = lastSelectedDocAttribute;


                    labelSelectedFile.Content = value;
                }
            }
            changeValueOfTextBoxAvailable = false;
        }

        private void recalculateNewLocationsFromPixels(MouseButtonEventArgs e)
        {
            if (lastSelectedDocAttribute.StartingXLocation == lastSelectedDocAttribute.EndingXLocation || lastSelectedDocAttribute.StartingYLocation == lastSelectedDocAttribute.EndingYLocation)
            {
                int indexOfxistingAttribute = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.IndexOf(analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name));
                if (indexOfxistingAttribute >= 0 && (analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingXLocation != analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingXLocation && analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingYLocation != analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingYLocation))
                {
                    lastSelectedDocAttribute = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute];
                    TextBox_GotFocus(lastSelectedDocAttribute, e);
                }
                else
                {
                    lastSelectedDocAttribute = new DocAttribute(lastSelectedDocAttribute.Name, "", lastSelectedDocAttribute.Type, 0, 0, 0, 0);
                    CalculateAverageAttributeLocation(lastSelectedDocAttribute);
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.Add(lastSelectedDocAttribute);
                    TextBox_GotFocus(lastSelectedDocAttribute, e);
                }
            }
            else
            {
                double percentWidth = (double)bitmap.PixelWidth / imgAnalyzedDocument.ActualWidth;
                double percentHeight = (double)bitmap.PixelHeight / imgAnalyzedDocument.ActualHeight;
                lastSelectedDocAttribute.StartingXLocation = (int)(lastSelectedDocAttribute.StartingXLocation * percentWidth);
                lastSelectedDocAttribute.StartingYLocation = (int)(lastSelectedDocAttribute.StartingYLocation * percentHeight);
                lastSelectedDocAttribute.EndingXLocation = (int)(lastSelectedDocAttribute.EndingXLocation * percentWidth);
                lastSelectedDocAttribute.EndingYLocation = (int)(lastSelectedDocAttribute.EndingYLocation * percentHeight);

                TextBox_GotFocus(lastSelectedDocAttribute, e);

                int indexOfxistingAttribute = analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.IndexOf(analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.SingleOrDefault(x => x.Name == lastSelectedDocAttribute.Name));
                if (indexOfxistingAttribute >= 0)
                {
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingXLocation = lastSelectedDocAttribute.StartingXLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].StartingYLocation = lastSelectedDocAttribute.StartingYLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingXLocation = lastSelectedDocAttribute.EndingXLocation;
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes[indexOfxistingAttribute].EndingYLocation = lastSelectedDocAttribute.EndingYLocation;
                }
                else
                {
                    analyzedFiles[pointerToActualAnalyzedFile].DocAttributes.Add(new DocAttribute(lastSelectedDocAttribute.Name, lastSelectedDocAttribute.Value, lastSelectedDocAttribute.Type, lastSelectedDocAttribute.StartingXLocation, lastSelectedDocAttribute.StartingYLocation, lastSelectedDocAttribute.EndingXLocation, lastSelectedDocAttribute.EndingYLocation));
                }
            }
        }

        private void organizeLocations()
        {
            if (lastSelectedDocAttribute.StartingXLocation > lastSelectedDocAttribute.EndingXLocation)
            {
                int tmp = lastSelectedDocAttribute.StartingXLocation;
                lastSelectedDocAttribute.StartingXLocation = lastSelectedDocAttribute.EndingXLocation;
                lastSelectedDocAttribute.EndingXLocation = tmp;
            }
            if (lastSelectedDocAttribute.StartingYLocation > lastSelectedDocAttribute.EndingYLocation)
            {
                int tmp = lastSelectedDocAttribute.StartingYLocation;
                lastSelectedDocAttribute.StartingYLocation = lastSelectedDocAttribute.EndingYLocation;
                lastSelectedDocAttribute.EndingYLocation = tmp;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Model newModel = this.DataContext as Model;
            this.model = newModel;
            this.DataContext = model;
            ShowImage();
            lastSelectedDocAttribute = null;
            ShowImageWithAllAttributeBoundaries();
            buttonSave.Focus();
        }

        private void listViewAttributes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as ListView).SelectedIndex = -1;
            TextBox_LostFocus(sender, e);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListView lv = (sender as ListView);
            if (lv != null)
            {
                return;
            }
            TextBox textBox = (sender as TextBox);
            if (textBox != null)
            {
                return;
            }
            if (imgAnalyzedDocument.IsMouseOver) 
            {
                return;
            }
            listViewAttributes.SelectedIndex = -1;
            TextBox_LostFocus(sender, e);
        }


        private void txtBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox_GotFocus((sender as TextBox), e);
        }


        private void listViewAttributes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DocAttribute docAttribute = (sender as ListView).SelectedItem as DocAttribute;
            int i = (sender as ListView).SelectedIndex;
            if (docAttribute != null)
            {
                TextBox_GotFocus(docAttribute, e);
            }
            else
            {
                TextBox_GotFocus(sender, e);
            }
        }
    }
}
