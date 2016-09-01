using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MjpegProcessor;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MjpegDecoder MD = new MjpegDecoder();

            MD.FrameReady += mjpeg_FrameReady;
            System.Uri a = new Uri("http://169.254.6.232:8081/");
            MD.ParseStream(a);
            ;
        }

        private void mjpeg_FrameReady(object sender,FrameReadyEventArgs e)
        {
            pictureBox1.Image = e.Bitmap;
        }

    }
}
