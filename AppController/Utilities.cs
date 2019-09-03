using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppController
{
    public class Utilities
    {
        //public static Task ResizeImage(string path, int width=250, int height=200) 
        //{
        //    return Task.Run(() =>
        //    {
        //        var x = System.IO.Path.GetTempFileName();
        //        var y = System.IO.Path.GetTempPath();
        //        Bitmap newImage;

        //        using (var srcImage = Image.FromFile(path))
        //        {                    
        //            using (newImage = new Bitmap(width, height))
        //            using (var graphics = Graphics.FromImage(newImage))
        //            {
        //                graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                graphics.DrawImage(srcImage, new Rectangle(0, 0, width, height));
        //                System.IO.File.Delete(path);
        //                newImage.Save(path);
        //                //newImage.Save(path);
        //            }                                        
        //        }                                
        //    });
            
        //} 
    }
}
