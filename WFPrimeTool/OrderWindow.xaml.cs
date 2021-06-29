using IronOCRTEst2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public static string itemname;
        public static int quant;
        public static string itemimgurl;
        public static string openorder;
        public string repeats;

        public OrderWindow()
        {
            InitializeComponent();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(price.Text) != null && Convert.ToInt32(price.Text) > 0 && Convert.ToInt32(quantity.Text) != null && Convert.ToInt32(quantity.Text) > 0 && MainWindow.canorder == false)
            {
                MainWindow.PlaceOrder(Convert.ToInt32(price.Text), Convert.ToInt32(quantity.Text));
                MainWindow.OrderDelay();
            }
            MainWindow.orderWindow = new OrderWindow();
            this.Close();
            
        }
        public BitmapImage ToWpfImage(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }


        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (itemname != null && itemimgurl != null && itemname != "" && itemimgurl != "")
            {
                var imnam = itemname.Replace(" ", "_");
                if (MainWindow.ownitemcount.TryGetValue(imnam, out quant))
                {
                    quantity.Text = quant.ToString();
                }
                else
                {
                    quantity.Text = "1";
                }
                
                label1.Content = "Item: " + itemname;
                this.Title = "Place Order Window - Item: " + itemname;
                var fullfilepath = @itemimgurl;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullfilepath, UriKind.Absolute);
                bitmap.EndInit();
                image.Source = bitmap;
                
                itemname = "";
                itemimgurl = "";
            }
        }

        public int Location { get; internal set; }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (repeats != openorder)
            {
                System.Diagnostics.Process.Start(openorder);
                repeats = openorder;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.orderWindow = new OrderWindow();
        }
    }
}
