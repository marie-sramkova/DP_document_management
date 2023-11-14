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

namespace WindowsFormsPrototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetButtonOpenIcon();
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
            buttonOpen.Image = Image.FromFile("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\icons\\icons8-browser-windows-64.png");
            // Align the image and text on the button.
            buttonOpen.ImageAlign = ContentAlignment.MiddleRight;
            buttonOpen.TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}
