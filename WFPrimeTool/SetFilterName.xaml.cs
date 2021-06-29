using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for SetFilterName.xaml
    /// </summary>
    public partial class SetFilterName : Window
    {
        public static string wrongname = "";
        public static Bitmap img1 = null;
        public static Bitmap img2 = null;
        public static bool isopen = false;
        public static bool save = false;


        public SetFilterName()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string fixedname = textBox.Text.ToLower();
            if(textBox.Text.Contains(" "))
            {
                fixedname = fixedname.Replace(" ", "_");
            }
            if (fixedname.Contains("chassis") || fixedname.Contains("neuroptics") || fixedname.Contains("systems"))
            {
                if(fixedname.EndsWith("_blueprint"))
                fixedname = fixedname.Replace("_blueprint", "");
            }
            wrongname = wrongname.Replace(" ", "");
            if (!MainWindow.CustomFilt.ContainsKey(wrongname) && save == true)
            {
                MainWindow.CustomFilt.Add(wrongname, fixedname);
                MainWindow.SaveSettings(false);

            }
            MainWindow.customhold = fixedname;
            MainWindow.CustomConfig();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.runworker();
            isopen = false;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility == Visibility.Hidden)
            {
                isopen = true;
            }
            if (this.Visibility == Visibility.Collapsed)
            {
                isopen = true;
            }
            if (this.Visibility == Visibility.Visible)
            {
                label.Content = wrongname;
                image.Source = MainWindow.BitmapToImageSource(img1);
                image1.Source = MainWindow.BitmapToImageSource(img2);
                isopen = true;
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            save = true;
        }
        private void checkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            save = false;
        }
    }
}
