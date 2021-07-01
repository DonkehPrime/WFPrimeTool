using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for Scan_Setup.xaml
    /// </summary>
    
    public partial class Scan_Setup : Window
    {
        public static dynamic check1;
        public static dynamic check2;
        public static DispatcherTimer tickin = new DispatcherTimer();
        public static DispatcherTimer testtimer = new DispatcherTimer();
        public Scan_Setup()
        {
            InitializeComponent();
            tickin.Tick += new EventHandler(ticking);
            tickin.Interval = TimeSpan.FromMilliseconds(50);
            tickin.Start();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loadname_Click(object sender, RoutedEventArgs e)
        {
            setn.IsEnabled = true;
            setq.IsEnabled = false;
            qualbox.Text = OCRHandler.qualit.ToString();
            sizelbox.Text = OCRHandler.sizel.ToString();
            sizerb.Text = OCRHandler.sizer.ToString();
            contrastbox.Text = OCRHandler.contrast.ToString();
            gammabox.Text = OCRHandler.gamma.ToString();
            huebox.Text = OCRHandler.hue.ToString();
            gblurbox.Text = OCRHandler.gblur.ToString();
            gsharpbox.Text = OCRHandler.gsharp.ToString();
            posl.Text = MainWindow.StartPos.ToString();
            posr.Text = MainWindow.StartPos2.ToString();
            sizelbox_Copy.Text = OCRHandler.sizelb.ToString();
            sizerb_Copy.Text = OCRHandler.sizerb.ToString();
            IncrX.Text = MainWindow.incrementx.ToString();
            IncrY.Text = MainWindow.incrementy.ToString();
            bnw.IsChecked = OCRHandler.blackwhite;
            inv.IsChecked = OCRHandler.invert;
        }

        private void loadqua_Click(object sender, RoutedEventArgs e)
        {
            setn.IsEnabled = false;
            setq.IsEnabled = true;
            qualbox.Text = OCRHandler.qualit2.ToString();
            sizelbox.Text = OCRHandler.sizel2.ToString();
            sizerb.Text = OCRHandler.sizer2.ToString();
            contrastbox.Text = OCRHandler.contrast2.ToString();
            gammabox.Text = OCRHandler.gamma2.ToString();
            huebox.Text = OCRHandler.hue2.ToString();
            gblurbox.Text = OCRHandler.gblur2.ToString();
            gsharpbox.Text = OCRHandler.gsharp2.ToString();
            posl.Text = MainWindow.zStartPos.ToString();
            posr.Text = MainWindow.zStartPos2.ToString();
            sizelbox_Copy.Text = OCRHandler.sizelb2.ToString();
            sizerb_Copy.Text = OCRHandler.sizerb2.ToString();
            IncrX.Text = MainWindow.incrementx.ToString();
            IncrY.Text = MainWindow.incrementy.ToString();
            bnw.IsChecked = OCRHandler.blackwhite2;
            inv.IsChecked = OCRHandler.invert2;
        }

        private void saven_Click(object sender, RoutedEventArgs e)
        {
            OCRHandler.qualit = int.Parse(qualbox.Text);
            OCRHandler.sizel = int.Parse(sizelbox.Text);
            OCRHandler.sizer = int.Parse(sizerb.Text);
            OCRHandler.contrast = int.Parse(contrastbox.Text);
            OCRHandler.gamma =  int.Parse(gammabox.Text);
            OCRHandler.hue = int.Parse(huebox.Text);
            OCRHandler.gblur = int.Parse(gblurbox.Text);
            OCRHandler.gsharp = int.Parse(gsharpbox.Text);
            MainWindow.StartPos = int.Parse(posl.Text);
            MainWindow.StartPos2 = int.Parse(posr.Text);
            OCRHandler.sizelb = int.Parse(sizelbox_Copy.Text);
            OCRHandler.sizerb = int.Parse(sizerb_Copy.Text);
            MainWindow.incrementx = int.Parse(IncrX.Text);
            MainWindow.incrementy = int.Parse(IncrY.Text);
            OCRHandler.blackwhite = (bool)bnw.IsChecked;
            OCRHandler.invert = (bool)inv.IsChecked;
        }

        private void saveq_Click(object sender, RoutedEventArgs e)
        {
            OCRHandler.qualit2 = int.Parse(qualbox.Text);
            OCRHandler.sizel2 = int.Parse(sizelbox.Text);
            OCRHandler.sizer2 = int.Parse(sizerb.Text);
            OCRHandler.contrast2 = int.Parse(contrastbox.Text);
            OCRHandler.gamma2 = int.Parse(gammabox.Text);
            OCRHandler.hue2 = int.Parse(huebox.Text);
            OCRHandler.gblur2 = int.Parse(gblurbox.Text);
            OCRHandler.gsharp2 = int.Parse(gsharpbox.Text);
            MainWindow.zStartPos = int.Parse(posl.Text);
            MainWindow.zStartPos2 = int.Parse(posr.Text);
            OCRHandler.sizelb2 = int.Parse(sizelbox_Copy.Text);
            OCRHandler.sizerb2 = int.Parse(sizerb_Copy.Text);
            MainWindow.incrementx = int.Parse(IncrX.Text);
            MainWindow.incrementy = int.Parse(IncrY.Text);
            OCRHandler.blackwhite2 = (bool)bnw.IsChecked;
            OCRHandler.invert2 = (bool)inv.IsChecked;
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StartPosdo = MainWindow.StartPos;
            MainWindow.zStartPosdo = MainWindow.zStartPos;
            MainWindow.StartPos2do = MainWindow.StartPos2;
            MainWindow.zStartPos2do = MainWindow.zStartPos2;
            textBox1.Text = OCRHandler.OCRWord();
            textBox1_Copy.Text = OCRHandler.OCRCount().ToString();
            image.Source = check1;
            image2.Source = check2;
        }
        private void ticking(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                posl.Text = System.Windows.Forms.Control.MousePosition.X.ToString();
                posr.Text = System.Windows.Forms.Control.MousePosition.Y.ToString();
                tleft = System.Windows.Forms.Control.MousePosition.X;
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                tup = System.Windows.Forms.Control.MousePosition.Y;
            }
            if (Keyboard.IsKeyDown(Key.Right) && tleft != null)
            {
                IncrX.Text = (System.Windows.Forms.Control.MousePosition.X - tleft).ToString();
            }
            if (Keyboard.IsKeyDown(Key.Down) && tup != null)
            {
                IncrY.Text = (System.Windows.Forms.Control.MousePosition.Y - tup).ToString();
            }
            Title = "Scan Setup - Mouse Pos:" + System.Windows.Forms.Control.MousePosition;
        }
        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }


        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            
            if(testtimer.IsEnabled == false)
            {
                testtimer.Tick += new EventHandler(testtick);
                testtimer.Interval = TimeSpan.FromSeconds(2);
                MainWindow.StartPosdo = MainWindow.StartPos;
                MainWindow.zStartPosdo = MainWindow.zStartPos;
                MainWindow.StartPos2do = MainWindow.StartPos2;
                MainWindow.zStartPos2do = MainWindow.zStartPos2;
                testtimer.Start();
            }
            else
            {
                testtimer.Tick -= new EventHandler(testtick);
                testtimer.Stop();
            }

        }
        private int dcount = 0;
        private int rowcount = 0;
        private void testtick(object sender, EventArgs e)
        {
            textBox1.Text = OCRHandler.OCRWord();
            textBox1_Copy.Text = OCRHandler.OCRCount().ToString();
            image.Source = check1;
            image2.Source = check2;
            if (dcount < 6)
            {
                MainWindow.StartPosdo += MainWindow.incrementx;
                MainWindow.zStartPosdo += MainWindow.incrementx;
                dcount++;
            }
            if (dcount == 6)
            {
                dcount = 0;
                rowcount++;
                MainWindow.StartPosdo = MainWindow.StartPos;
                MainWindow.zStartPosdo = MainWindow.zStartPos;
                MainWindow.StartPos2do += MainWindow.incrementy;
                MainWindow.zStartPos2do += MainWindow.incrementy;
            }
            if(rowcount == 3)
            {
                dcount = 0;
                rowcount = 0;
                MainWindow.StartPosdo = MainWindow.StartPos;
                MainWindow.zStartPosdo = MainWindow.zStartPos;
                MainWindow.StartPos2do = MainWindow.StartPos2;
                MainWindow.zStartPos2do = MainWindow.zStartPos2;
                testtimer.Tick -= new EventHandler(testtick);
                testtimer.Stop();
            }
        }
        public static int tleft;
        public static int tup;
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.LeftCtrl)
            {
                posl.Text = System.Windows.Forms.Control.MousePosition.X.ToString();
                posr.Text = System.Windows.Forms.Control.MousePosition.Y.ToString();
            }
            if(e.Key == Key.Left)
            {
                tleft = System.Windows.Forms.Control.MousePosition.X;
            }
            if (e.Key == Key.Up)
            {
                tup = System.Windows.Forms.Control.MousePosition.Y;
            }
            if (e.Key == Key.Right && tleft != null)
            {
                IncrX.Text = (System.Windows.Forms.Control.MousePosition.X - tleft).ToString();
            }
            if (e.Key == Key.Down && tup != null)
            {
                IncrY.Text = (System.Windows.Forms.Control.MousePosition.Y - tup).ToString();
            }

        }



        public static void LoadSettings()
        {
            var path = @"OCRSettings.txt";
            if (File.Exists(path))
            {
                using (FileStream sr = File.OpenRead(path))
                {
                    byte[] b = new byte[sr.Length];
                    var fulltext = sr.Read(b, 0, b.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string hold = temp.GetString(b);
                    dynamic datasplit = hold.Split('\n', '=');
                    OCRHandler.qualit = int.Parse(datasplit[1]);
                    OCRHandler.contrast = int.Parse(datasplit[3]);
                    OCRHandler.gamma = int.Parse(datasplit[5]);
                    OCRHandler.hue = int.Parse(datasplit[7]);
                    OCRHandler.gblur = int.Parse(datasplit[9]);
                    OCRHandler.gsharp = int.Parse(datasplit[11]);
                    OCRHandler.qualit2 = int.Parse(datasplit[13]);
                    OCRHandler.sizel2 = int.Parse(datasplit[15]);
                    OCRHandler.sizer2 = int.Parse(datasplit[17]);
                    OCRHandler.contrast2 = int.Parse(datasplit[19]);
                    OCRHandler.gamma2 = int.Parse(datasplit[21]);
                    OCRHandler.hue2 = int.Parse(datasplit[23]);
                    OCRHandler.gblur2 = int.Parse(datasplit[25]);
                    OCRHandler.gsharp2 = int.Parse(datasplit[27]);
                    OCRHandler.blackwhite = bool.Parse(datasplit[29]);
                    OCRHandler.blackwhite2 = bool.Parse(datasplit[31]);
                    OCRHandler.invert = bool.Parse(datasplit[33]);
                    OCRHandler.invert2 = bool.Parse(datasplit[35]);
                    OCRHandler.sizel = int.Parse(datasplit[37]);
                    OCRHandler.sizer = int.Parse(datasplit[39]);
                    OCRHandler.sizelb = int.Parse(datasplit[41]);
                    OCRHandler.sizerb = int.Parse(datasplit[43]);
                    OCRHandler.sizel2 = int.Parse(datasplit[45]);
                    OCRHandler.sizer2 = int.Parse(datasplit[47]);
                    OCRHandler.sizelb2 = int.Parse(datasplit[49]);
                    OCRHandler.sizerb2 = int.Parse(datasplit[51]);
                    MainWindow.StartPos = int.Parse(datasplit[53]);
                    MainWindow.StartPos2 = int.Parse(datasplit[55]);
                    MainWindow.zStartPos = int.Parse(datasplit[57]);
                    MainWindow.incrementx = int.Parse(datasplit[59]);
                    MainWindow.incrementy = int.Parse(datasplit[61]);
                }
            }
            else
            {
                LoadDefSettings();
            }
        }

        public static void LoadDefSettings()
        {
            var path = @"DefaultOCRSettings.txt";
            if (File.Exists(path))
            {
                using (FileStream sr = File.OpenRead(path))
                {
                    byte[] b = new byte[sr.Length];
                    var fulltext = sr.Read(b, 0, b.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string hold = temp.GetString(b);
                    dynamic datasplit = hold.Split('\n', '=');
                    OCRHandler.qualit = int.Parse(datasplit[1]);
                    OCRHandler.contrast = int.Parse(datasplit[3]);
                    OCRHandler.gamma = int.Parse(datasplit[5]);
                    OCRHandler.hue = int.Parse(datasplit[7]);
                    OCRHandler.gblur = int.Parse(datasplit[9]);
                    OCRHandler.gsharp = int.Parse(datasplit[11]);
                    OCRHandler.qualit2 = int.Parse(datasplit[13]);
                    OCRHandler.sizel2 = int.Parse(datasplit[15]);
                    OCRHandler.sizer2 = int.Parse(datasplit[17]);
                    OCRHandler.contrast2 = int.Parse(datasplit[19]);
                    OCRHandler.gamma2 = int.Parse(datasplit[21]);
                    OCRHandler.hue2 = int.Parse(datasplit[23]);
                    OCRHandler.gblur2 = int.Parse(datasplit[25]);
                    OCRHandler.gsharp2 = int.Parse(datasplit[27]);
                    OCRHandler.blackwhite = bool.Parse(datasplit[29]);
                    OCRHandler.blackwhite2 = bool.Parse(datasplit[31]);
                    OCRHandler.invert = bool.Parse(datasplit[33]);
                    OCRHandler.invert2 = bool.Parse(datasplit[35]);
                    OCRHandler.sizel = int.Parse(datasplit[37]);
                    OCRHandler.sizer = int.Parse(datasplit[39]);
                    OCRHandler.sizelb = int.Parse(datasplit[41]);
                    OCRHandler.sizerb = int.Parse(datasplit[43]);
                    OCRHandler.sizel2 = int.Parse(datasplit[45]);
                    OCRHandler.sizer2 = int.Parse(datasplit[47]);
                    OCRHandler.sizelb2 = int.Parse(datasplit[49]);
                    OCRHandler.sizerb2 = int.Parse(datasplit[51]);
                    MainWindow.StartPos = int.Parse(datasplit[53]);
                    MainWindow.StartPos2 = int.Parse(datasplit[55]);
                    MainWindow.zStartPos = int.Parse(datasplit[57]);
                    MainWindow.incrementx = int.Parse(datasplit[59]);
                    MainWindow.incrementy = int.Parse(datasplit[61]);
                }
            }
        }
        public static void SaveSettings()
        {
            var path = @"OCRSettings.txt";
                using (FileStream sr = File.OpenWrite(path))
                {
                        AddText(sr, "qual=" + OCRHandler.qualit + "\n"+
                            "contrast=" + OCRHandler.contrast + "\n"+
                            "gamma=" + OCRHandler.gamma + "\n"+
                            "hue=" + OCRHandler.hue + "\n"+
                            "gblur=" + OCRHandler.gblur + "\n"+
                            "gsharp=" + OCRHandler.gsharp + "\n"+
                            "qualit2=" + OCRHandler.qualit2 + "\n" +
                            "sizel2=" + OCRHandler.sizel2 + "\n" +
                            "sizer2=" + OCRHandler.sizer2 + "\n" +
                            "contrast2=" + OCRHandler.contrast2 + "\n" +
                            "gamma2=" + OCRHandler.gamma2 + "\n" +
                            "hue2=" + OCRHandler.hue2 + "\n" +
                            "gblur2=" + OCRHandler.gblur2 + "\n" +
                            "gsharp2=" + OCRHandler.gsharp2 + "\n" +
                            "blackwhite=" + OCRHandler.blackwhite + "\n"+
                            "blackwhite2=" + OCRHandler.blackwhite2 + "\n"+
                            "invert=" + OCRHandler.invert + "\n"+
                            "invert2=" + OCRHandler.invert2 + "\n"+
                            "sizel=" + OCRHandler.sizel + "\n" +
                            "sizer=" + OCRHandler.sizer + "\n"+
                            "sizelb=" + OCRHandler.sizelb + "\n"+
                            "sizerb=" + OCRHandler.sizerb + "\n"+
                            "sizel2=" + OCRHandler.sizel2 + "\n"+
                            "sizer2=" + OCRHandler.sizer2 + "\n"+
                            "sizelb2=" + OCRHandler.sizelb2 + "\n"+
                            "sizerb2=" + OCRHandler.sizerb2 + "\n" +
                            "StartPos=" + MainWindow.StartPos + "\n" +
                            "StartPos2=" + MainWindow.StartPos2 + "\n" +
                            "zStartPos=" + MainWindow.zStartPos + "\n" +
                            "incrementx=" + MainWindow.incrementx + "\n" +
                            "incrementy=" + MainWindow.incrementy + "\n");
                }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void SaveSettingsz(object sender, RoutedEventArgs e)
        {
            //LoadSettings();
            SaveSettings();
        }

        private void ResetSettingsz(object sender, RoutedEventArgs e)
        {
            LoadDefSettings();
        }

        private void windo_Closed(object sender, EventArgs e)
        {
            MainWindow.cscan = new Scan_Setup();
        }
    }
}
