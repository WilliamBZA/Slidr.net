using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Slidr.net
{
    public class ImageResult : FileStreamResult
    {
        public ImageResult(Image input)
            : this(input, input.Width, input.Height) 
        {
        }

        public ImageResult(Image input, int width, int height)
            :
            base(GetMemoryStream(input, width, height, ImageFormat.Png), "image/png")
        {
        }

        static MemoryStream GetMemoryStream(Image input, int width, int height, ImageFormat fmt)
        {
            // maintain aspect ratio
            if (input.Width > input.Height)
                height = input.Height * width / input.Width;
            else
                width = input.Width * height / input.Height;

            var bmp = new Bitmap(input, width, height);
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }
    }
}