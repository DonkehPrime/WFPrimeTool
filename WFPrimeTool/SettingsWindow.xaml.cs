using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WFPrimeTool.OrderFunctions;

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        //public static bool retrylimit = false;
        public static int retrylimitcount = 20;
        public static bool closed = false;
        public static bool Logging = true;
        public static bool onescreen = false;
        public static bool limiton = false;
        public static bool laststat = false;
        public SettingsWindow()
        {
            InitializeComponent();

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(this.Visibility == Visibility.Visible)
            {
                checkBox.Content = "Max Scan Retry's: " + Math.Round(slider.Value);
                MainWindow.trylimit = Convert.ToInt32(Math.Round(slider.Value));
                retrylimitcount = Convert.ToInt32(Math.Round(slider.Value));
            }
            
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            slider.IsEnabled = true;
            MainWindow.limitenabled = true;
            //MainWindow.trylimit = Convert.ToInt32(Math.Round(slider.Value));
        }
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            slider.IsEnabled = false;
            MainWindow.limitenabled = false;
            MainWindow.trylimit = 20;
        }
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.onescreen = true;
        }
        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.onescreen = false;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {


            var path = @"filter.txt";
            string holder = "";
            if (File.Exists(path))
            {
                MainWindow.prime.Clear();
                MainWindow.neuroptics.Clear();
                MainWindow.systems.Clear();
                MainWindow.chassis.Clear();
                MainWindow.receiver.Clear();
                MainWindow.barrel.Clear();
                MainWindow.CustomFilt.Clear();
                using (FileStream fstream = File.OpenRead(path))
                {
                    byte[] b = new byte[1024];
                    var fulltext = fstream.Read(b, 0, b.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    holder += temp.GetString(b);
                    holder = holder.Replace("]\n", "],");
                    var jsons = JsonConvert.DeserializeObject<dynamic>(holder);
                    foreach (var item in jsons)
                    {
                        foreach (var itom in item)
                        {
                            foreach (var filtomy in itom)
                            {
                                string filtom = filtomy.ToString();
                                if (filtomy.Path.Contains("prime") && !MainWindow.prime.Contains(filtom.ToString()))
                                {
                                    MainWindow.prime.Add(filtom);
                                }
                                if (filtomy.Path.Contains("neuroptics") && !MainWindow.neuroptics.Contains(filtom.ToString()))
                                {
                                    MainWindow.neuroptics.Add(filtom);
                                }
                                if (filtomy.Path.Contains("chassis") && !MainWindow.chassis.Contains(filtom.ToString()))
                                {
                                    MainWindow.chassis.Add(filtom);
                                }
                                if (filtomy.Path.Contains("systems") && !MainWindow.systems.Contains(filtom.ToString()))
                                {
                                    MainWindow.systems.Add(filtom);
                                }
                                if (filtomy.Path.Contains("receiver") && !MainWindow.receiver.Contains(filtom.ToString()))
                                {
                                    MainWindow.receiver.Add(filtom);
                                }
                                if (filtomy.Path.Contains("barrel") && !MainWindow.barrel.Contains(filtom.ToString()))
                                {
                                    MainWindow.barrel.Add(filtom);
                                }
                                if (filtomy.Path.Contains("ornament") && !MainWindow.barrel.Contains(filtom.ToString()))
                                {
                                    MainWindow.ornament.Add(filtom);
                                }

                            }

                        }

                    }
                }
            }
            else
            {
                MainWindow.CheckFilter();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.settingsWindow = new SettingsWindow();
            MainWindow.SaveSettings(true);
            closed = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Bitmap printscreen = new Bitmap(170, 80);
            Bitmap printscreen2 = new Bitmap(35, 25);

            Graphics graphics = Graphics.FromImage(printscreen as Image);
            Graphics graphics2 = Graphics.FromImage(printscreen2 as Image);

            graphics2.CopyFromScreen(MainWindow.zStartPos, MainWindow.zStartPos2, 0, 0, printscreen2.Size);
            graphics.CopyFromScreen(MainWindow.StartPos, MainWindow.StartPos2, 0, 0, printscreen.Size);
            //LogError("test", printscreen);
            image2.Source = MainWindow.BitmapToImageSource(printscreen2);
            image.Source = MainWindow.BitmapToImageSource(printscreen);
        }

        private void checkBox2_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded && this.IsVisible)
            {
                Logging = true;
                checkBox2.Content = "Disable Error Logging";
                MainWindow.Logging = true;
            }
            
        }
        private void checkBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded && this.IsVisible)
            {
                Logging = false;
                checkBox2.Content = "Enable Error Logging";
                MainWindow.Logging = false;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            checkBox1.IsChecked = MainWindow.onescreen;
            checkBox2.IsChecked = Logging;
            laststatus.IsChecked = laststat;
            checkBox.IsChecked = MainWindow.limitenabled;
            slider.IsEnabled = MainWindow.limitenabled;
            if(Logging)
            {
                checkBox2.Content = "Disable Error Logging";
            }
            if (!Logging)
            {
                checkBox2.Content = "Enable Error Logging";
            }
            if (laststat)
            {
                laststatus.Content = "Disable Last Status";
            }
            if (!laststat)
            {
                laststatus.Content = "Enable Last Status";
            }
            if (MainWindow.trylimit != 20 && MainWindow.trylimit != 0)
            {
                checkBox.Content = "Max Scan Retry's: " + MainWindow.trylimit;
            }
            retrylimitcount = MainWindow.trylimit;
            slider.Value = MainWindow.trylimit;
        }

        private void lastatusChecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded && this.IsVisible)
            {
                laststat = true;
                laststatus.Content = "Disable Last Status";
                MainWindow.laststat = true;
            }
        }
        private void lastatusUnChecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded && this.IsVisible)
            {
                laststat = false;
                laststatus.Content = "Disable Last Status";
                MainWindow.laststat = false;
            }
        }

        private void CustScan_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.cscan.Top = this.Top;
            MainWindow.cscan.Left = this.Left;
            MainWindow.cscan.Show();
        }
    }
}
