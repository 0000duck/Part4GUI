using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MjpegProcessor;

namespace iWindow.AR
{
    class cameraFeed
    {
        public static MjpegDecoder MD;

        public static void loadCamera(string URIpi)
        {

            MD = new MjpegDecoder();

            MD.FrameReady += mjpeg_FrameReady;
            System.Uri a = new Uri("http://" + URIpi +  ":8081/");
            MD.ParseStream(a);

        }

        private static void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {

            MjpegDecoder md = (MjpegDecoder)sender;
            render.hasFirst = true;
            render.currentBMP = MD.Bitmap;
            md.FrameReady -= mjpeg_FrameReady;
        }
    }
}
