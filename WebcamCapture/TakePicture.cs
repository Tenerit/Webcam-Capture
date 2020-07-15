using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarrenLee.Media;
using System.IO;
using System.Drawing.Imaging;

namespace WebcamCapture
{
    public partial class TakePicture : Form
    {
        Camera myCamera = new Camera();

        public TakePicture()
        {
            InitializeComponent();

            GetInfo();
            myCamera.OnFrameArrived += myCamera_OnFrameArrived;
        }

        private void myCamera_OnFrameArrived(object source, FrameArrivedEventArgs e)
        {
            Image img = e.GetFrame();
            pictureBox1.Image = img;
        }

        private void GetInfo()
        {
            var cameraDevices = myCamera.GetCameraSources();
            var cameraResolution = myCamera.GetSupportedResolutions();

            foreach(var d in cameraDevices)
            {
                comboBox1.Items.Add(d);
            }
            foreach (var r in cameraResolution)
            {
                comboBox2.Items.Add(r);
            }

            comboBox2.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.ChangeCamera(comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.Start(comboBox2.SelectedIndex);
        }

        private void TakePicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            myCamera.Stop();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(@"D:\db\images"))
                {
                    Directory.CreateDirectory(@"D:\db\images");
                    MessageBox.Show("Image Folder Created ...");
                    string path = @"D:\db\images";

                    pictureBox1.Image.Save(path + @"\" + textBox1.Text + ".Jpg", ImageFormat.Jpeg);
                    MessageBox.Show("Image Saved ...");


                }
                else
                {
                    string path = @"D:\db\images";
                    pictureBox1.Image.Save(path + @"\" + textBox1.Text + ".Jpg", ImageFormat.Jpeg);
                    MessageBox.Show("Image Saved ...");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
