using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WFPrimeTool
{
    class LogHandler
    {

        public static bool LogError(bool except, string errorinfo, Image image = null, Image image2 = null, string itemname = "")
        {
            var pathex = @"Logs/exceptions.txt";
            if (errorinfo.Contains("tesseract/wiki/Error-1"))
            {
                if (MainWindow.worker.IsEnabled)
                {
                    MainWindow.worker.Stop();
                }
                MessageBox.Show("Please get the required Tessdata folder from github,\nOr make sure you put it in the correct Directory,\nSame folder containing this Application.", "Error: " + pathex, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            bool logged = false;
            if (MainWindow.Logging == true)
            {
                int imgcount = 0;
                
                var path = @"Logs/log.txt";
                var pathp = @"Logs/images/Error Item " + imgcount + ".bmp";
                if (!Directory.Exists(@"Logs/images"))
                {
                    Directory.CreateDirectory(@"Logs/images");
                }

                if (image != null)
                {
                    while (File.Exists(pathp))
                    {
                        imgcount++;
                        pathp = @"Logs/images/Error Item " + imgcount + ".bmp";
                    }
                    if (!File.Exists(pathp))
                    {
                        image.Save(pathp, System.Drawing.Imaging.ImageFormat.Bmp);
                        logged = true;
                    }

                }
                if (image2 != null)
                {
                    while (File.Exists(pathp))
                    {
                        imgcount++;
                        pathp = @"Logs/images/Error Item " + imgcount + ".bmp";
                    }
                    if (!File.Exists(pathp))
                    {
                        image2.Save(pathp, System.Drawing.Imaging.ImageFormat.Bmp);
                        logged = true;
                    }

                }
                if (except == false)
                {
                    if (File.Exists(path))
                    {
                        string prevtext;
                        using (FileStream fstr = File.OpenRead(path))
                        {
                            byte[] b = new byte[fstr.Length];
                            var fulltext = fstr.Read(b, 0, b.Length);
                            UTF8Encoding temp = new UTF8Encoding(true);
                            prevtext = temp.GetString(b);
                            fstr.Close();
                        }
                        using (FileStream fstr = File.OpenWrite(path))
                        {
                            AddText(fstr, prevtext + " Retry Count: {" + errorinfo + "} Error Count: {" + imgcount + "}  Error String: {" + itemname + "}\n");
                            fstr.Close();
                            logged = true;
                        }
                    }
                    else
                    {
                        using (FileStream fstr = File.Create(path))
                        {
                            AddText(fstr, " Retry Count: {" + errorinfo + "} Error Count: {" + imgcount + "}  Error String: {" + itemname + "}\n");
                            fstr.Close();
                            logged = true;
                        }
                    }
                }
                else
                {
                    if (File.Exists(pathex))
                    {
                        string prevtext;
                        using (FileStream fstr = File.OpenRead(pathex))
                        {
                            byte[] b = new byte[fstr.Length];
                            var fulltext = fstr.Read(b, 0, b.Length);
                            UTF8Encoding temp = new UTF8Encoding(true);
                            prevtext = temp.GetString(b);
                            fstr.Close();
                        }
                        using (FileStream fstr = File.OpenWrite(pathex))
                        {
                            AddText(fstr, prevtext + errorinfo + "\n");
                            fstr.Close();
                            logged = true;
                        }
                    }
                    else
                    {
                        using (FileStream fstr = File.Create(pathex))
                        {
                            AddText(fstr, errorinfo + "\n");
                            fstr.Close();
                            logged = true;
                        }
                    }
                }

            }
            return logged;
               
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
