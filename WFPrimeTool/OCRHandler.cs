using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tesseract;

namespace WFPrimeTool
{
    class OCRHandler
    {

        public static bool blackwhite = true;
        public static bool invert = true;
        public static bool blackwhite2 = true;
        public static bool invert2 = true;
        public static int qualit = 94;
        public static int qualit2 = 150;

        public static int sizel = 222;
        public static int sizer = 163;

        public static int sizelb = 170;
        public static int sizerb = 80;

        public static int sizelb2 = 35;
        public static int sizerb2 = 25;

        public static int sizel2 = 180;
        public static int sizer2 = 230;

        public static int contrast = -61;
        public static int gamma = 21;
        public static int hue = 17;
        public static int gblur = 0;
        public static int gsharp = 28;

        public static int contrast2 = -61;
        public static int gamma2 = 21;
        public static int hue2 = 17;
        public static int gblur2 = -12;
        public static int gsharp2 = 37;

        public static Bitmap printscreen;
        public static Bitmap printscreen2;
        public static dynamic bmp;
        public static dynamic bmp2;

        public static string OCRWord()
        {
            printscreen = new Bitmap(sizelb, sizerb);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(MainWindow.StartPosdo, MainWindow.StartPos2do, 0, 0, printscreen.Size);

           
            

            string temps = "";
            MainWindow.logshot1 = printscreen;
            byte[] photoBytes2 = MainWindow.BitmapToBytes(printscreen);
            ISupportedImageFormat format2 = new JpegFormat { Quality = qualit };
            Size size2 = new Size(sizel, sizer);
       
            string answer;
            try
            {
                using (MemoryStream inStream = new MemoryStream(photoBytes2))
                {
                using (MemoryStream outStream = new MemoryStream())
                {
                    

                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               if(blackwhite == true && invert == true)
                        {
                            imageFactory.Load(inStream).Resize(size2).Format(format2).Contrast(contrast).Gamma(gamma).Hue(hue).Filter(MatrixFilters.Invert).Filter(MatrixFilters.BlackWhite).GaussianBlur(gblur).GaussianSharpen(gsharp).Save(outStream);
                            bmp = new Bitmap(outStream);

                        }
                        if (blackwhite == false && invert == true)
                        {
                            imageFactory.Load(inStream).Resize(size2).Format(format2).Contrast(contrast).Gamma(gamma).Hue(hue).Filter(MatrixFilters.Invert).Filter(MatrixFilters.BlackWhite).GaussianBlur(gblur).GaussianSharpen(gsharp).Save(outStream);
                            bmp = new Bitmap(outStream);

                        }
                        if (blackwhite == true && invert == false)
                        {
                            imageFactory.Load(inStream).Resize(size2).Format(format2).Contrast(contrast).Gamma(gamma).Hue(hue).Filter(MatrixFilters.BlackWhite).GaussianBlur(gblur).GaussianSharpen(gsharp).Save(outStream);
                            bmp = new Bitmap(outStream);
                        }
                        if (blackwhite == false && invert == false)
                        {
                            imageFactory.Load(inStream).Resize(size2).Format(format2).Contrast(contrast).Gamma(gamma).Hue(hue).GaussianBlur(gblur).GaussianSharpen(gsharp).Save(outStream);
                            bmp = new Bitmap(outStream);

                        }


                    }
                    Scan_Setup.check1 = MainWindow.BitmapToImageSource(bmp);
                    //image.Source = BitmapToImageSource(bmp);
                    


                        var Input2 = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                        Input2.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
                        using (var graph = Graphics.FromImage(bmp))
                        {
                            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace); // splits letters if Capitalization is attached
                            var Result = Input2.Process((Bitmap)bmp, PageSegMode.Auto);
                            //textBox2.Text += r.Replace(Result.GetText(), " ");
                            answer = r.Replace(Result.GetText(), " ");

                        }
                    }
                    
                }
                return answer;
            }
            catch (TesseractException n)
            {
                LogHandler.LogError(true, n.Message);
                
            }
            return "";

        }

        public static int OCRCount()
        {
            printscreen2 = new Bitmap(sizelb2, sizerb2);
            MainWindow.logshot2 = printscreen2;
            Graphics graphics = Graphics.FromImage(printscreen2 as Image);

            graphics.CopyFromScreen(MainWindow.zStartPosdo, MainWindow.zStartPos2do, 0, 0, printscreen2.Size);
            byte[] photoBytes2 = BitmapToBytes(printscreen2);
            ISupportedImageFormat format = new JpegFormat { Quality = qualit2 };
            Size size = new Size(sizel2, sizer2);
            int resulty = 0;
            try { 
            using (MemoryStream inStream = new MemoryStream(photoBytes2))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        if(blackwhite2 == true && invert2 == true)
                        {
                            imageFactory.Load(inStream).Resize(size).Format(format).Contrast(contrast2).Gamma(gamma2).Hue(hue2).Filter(MatrixFilters.Invert).Filter(MatrixFilters.BlackWhite).GaussianBlur(gblur2).GaussianSharpen(gsharp2).Save(outStream);
                            bmp2 = new Bitmap(outStream);
                        }
                        if (blackwhite2 == false && invert2 == true)
                        {
                            imageFactory.Load(inStream).Resize(size).Format(format).Contrast(contrast2).Gamma(gamma2).Hue(hue2).Filter(MatrixFilters.Invert).GaussianBlur(gblur2).GaussianSharpen(gsharp2).Save(outStream);
                            bmp2 = new Bitmap(outStream);
                        }
                        if (blackwhite2 == true && invert2 == false)
                        {
                            imageFactory.Load(inStream).Resize(size).Format(format).Contrast(contrast2).Gamma(gamma2).Hue(hue2).Filter(MatrixFilters.BlackWhite).GaussianBlur(gblur2).GaussianSharpen(gsharp2).Save(outStream);
                            bmp2 = new Bitmap(outStream);
                        }
                        if (blackwhite2 == false && invert2 == false)
                        {
                            imageFactory.Load(inStream).Resize(size).Format(format).Contrast(contrast2).Gamma(gamma2).Hue(hue2).GaussianBlur(gblur2).GaussianSharpen(gsharp2).Save(outStream);
                            bmp2 = new Bitmap(outStream);
                        }

                    }
                    //bmp2 = new Bitmap(outStream);
                    Scan_Setup.check2 = MainWindow.BitmapToImageSource(bmp2);
                    //image2.Source = BitmapToImageSource(bmp);
                    var Input2 = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                    Input2.SetVariable("tessedit_char_whitelist", "1234567890");
                    using (var graph = Graphics.FromImage(bmp2))
                    {
                        var Result = Input2.Process((Bitmap)bmp2, PageSegMode.Count);
                        //textBox2.Text = Result.GetText();
                        if (Result.GetText() != null && Result.GetText() != "")
                        {
                            resulty = Convert.ToInt32(Result.GetText().Replace("\n", ""));

                            //if(coundt)
                        }
                    }
                }
            }
            return resulty;
            }
            catch (TesseractException n)
            {
                LogHandler.LogError(true, n.Message);
            }
            return resulty;
        }

        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                return memory.ToArray();

            }
        }
    }
}
