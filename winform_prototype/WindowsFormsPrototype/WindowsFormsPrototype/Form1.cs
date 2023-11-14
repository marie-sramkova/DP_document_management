using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsPrototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetButtonOpenIcon();

            //setting of listview - to new method
            listView1.View = View.List;
            listView1.Items.Add("filename1.docx");
            listView1.Items.Add("filename22.docx");


        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string folder = fbd.SelectedPath;
                    textBox1.Text = folder;
//                    string[] files = Directory.GetFiles(fbd.SelectedPath);

//                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }

        }

        private void SetButtonOpenIcon()
        {
            // Assign an image to the button.
            buttonOpen.Image = System.Drawing.Image.FromFile("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\icons\\icons8-browser-windows-64.png");
            // Align the image and text on the button.
            buttonOpen.ImageAlign = ContentAlignment.MiddleRight;
            buttonOpen.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}
