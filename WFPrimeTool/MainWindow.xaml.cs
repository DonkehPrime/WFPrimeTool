using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using WFPrimeTool.OrderFunctions;
using WFPrimeTool;
using System.Diagnostics;
using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using Tesseract;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static WebClient client = new WebClient();
        public static Dictionary<string, string> ItemUrl = new Dictionary<string, string>();
        public static Dictionary<string, string> ItemImgUrl = new Dictionary<string, string>();
        public static Dictionary<string, int> ownitemcount = new Dictionary<string, int>();
        public static Dictionary<string, string> CustomFilt = new Dictionary<string, string>();
        public static ObservableCollection<OrderData> orddat = new ObservableCollection<OrderData>();
        public static List<string> ItemName = new List<string>();
        public static List<string> ownitemurl = new List<string>();
        public static string ingame_name;
        public static string ingamestatus;
        public static Dictionary<string, dynamic> ifilt = new Dictionary<string, dynamic>();
        public static List<string> prime = new List<string>();
        public static List<string> neuroptics = new List<string>();
        public static List<string> chassis = new List<string>();
        public static List<string> systems = new List<string>();
        public static List<string> receiver = new List<string>();
        public static List<string> barrel = new List<string>();
        public static List<string> ornament = new List<string>();
        public static ChangeStatus changeStatus = new ChangeStatus();

        public static MainWindow mwin;
        public static OrderWindow orderWindow = new OrderWindow();
        public static SettingsWindow settingsWindow = new SettingsWindow();
        public static Dictionary<string, int> iPrice = new Dictionary<string, int>();
        public static Dictionary<string, int> SortDem = new Dictionary<string, int>();
        public static List<string> SortDem2 = new List<string>();
        public static DispatcherTimer d = new DispatcherTimer();
        public static DispatcherTimer titletimer = new DispatcherTimer();
        public static DispatcherTimer worker = new DispatcherTimer();
        public static DispatcherTimer Order = new DispatcherTimer();
        public static int doublecheck = 0;
        public static int quantity = 0;
        public static int sortval = 8;
        public static dynamic oData;
        public static dynamic duData;
        public static dynamic data;
        public static bool cando = false;
        public static bool cando2 = false;
        public static bool Logging = true;
        public static bool laststat = false;
        public static string answer;
        public static string repeats;
        public static string Orderitem;
        public static string customhold;
        public static bool once = false;
        public static bool donce = false;
        public static bool onescreen = false;
        public static bool isdata = false;
        public static bool canorder = false;
        public static bool limitenabled = false;
        public static Bitmap logshot1;
        public static Bitmap logshot2;
        public int countg = 0;
        public static int trylimitc = 0;
        public static int trylimit = 20;
        public static int StartPos = 100;
        public static int StartPos2 = 290;
        public static int zStartPos = 95;
        public static int zStartPos2 = 202;
        public static int StartPosdo;
        public static int StartPos2do;
        public static int zStartPosdo;
        public static int zStartPos2do;
        public static int incrementx = 211;
        public static int incrementy = 200;
        public static int dcount = 0;
        public static int rowcount = 0;
        public static int somecount = 0;
        public static int coundt = 0;
        public static int scancount = 0;
        private int titlecount = 0;
        public static MainWindow mainw;
        public static SetFilterName sfname = new SetFilterName();
        public static MarketHandler marketHandler = new MarketHandler();
        public static Scan_Setup cscan = new Scan_Setup();
        public static DispatcherTimer stopwork = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
            CheckFilter();
            LoadSettings();
            CustomConfig();
            Scan_Setup.LoadSettings();

            itemview.ItemsSource = orddat;
            mainw = this;
            data = client.DownloadString("https://api.warframe.market/v1/items");
            var items = JsonConvert.DeserializeObject(data);
            for (int z = 0; z < items["payload"]["items"].Count; z++)
            {
                ItemUrl.Add(items["payload"]["items"][z]["url_name"].ToString(), items["payload"]["items"][z]["id"].ToString());
                ItemImgUrl.Add(items["payload"]["items"][z]["url_name"].ToString(), "https://api.warframe.market/static/assets/" + items["payload"]["items"][z]["thumb"].ToString());
                string urlname = items["payload"]["items"][z]["url_name"].ToString();
                var splitname = urlname.Split('_');
                ItemName.Add(splitname[0]);

            }
            titletimer.Interval = TimeSpan.FromSeconds(5);
            titletimer.Tick += new EventHandler(titletim);
            stopwork.Tick += new EventHandler(stopworky);
            stopwork.Interval = TimeSpan.FromMilliseconds(500);
            titletimer.Start();
            Loggedin();
            mwin = this;
        }
        public static async void Connect()
        {
            
                await ChangeStatus.ConnectWF();
           
            
        }
        public static void runworker()
        {
            worker.Interval = TimeSpan.FromMilliseconds(150);
            mainw.sethandler();
            worker.Start();
        }
        public void sethandler()
        {
            if(customhold != null)
            {
                if (!textBox.Text.Contains(customhold))
                {
                    textBox.Text += customhold + "\n";
                }
            }
            
            worker.Tick += new EventHandler(Workdowork);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (onescreen == true)
            {
                this.Hide();
            }
            if(worker.IsEnabled == false)
            {
                StartPosdo = StartPos;
                StartPos2do = StartPos2;
                zStartPosdo = zStartPos;
                zStartPos2do = zStartPos2;
                worker.Interval = TimeSpan.FromMilliseconds(150);
                worker.Tick += new EventHandler(Workdowork);
                worker.Start();
                
            }
            else
            {
                worker.Stop();
                worker.Tick -= new EventHandler(Workdowork);
                button.Content = "Scan Prime Parts";
            }


        }
        public static async void OrderDelay()
        {
                canorder = true;
                await Task.Delay(400);
                canorder = false;
        }
        private void titletim(object sender, EventArgs e)
        {
            switch (titlecount)
            {
                case 0:
                    this.Title = "WFPrimeTool - Made By: RonkestDonk#7422 (Discord) Have Any Good Suggestions? Feel Free To Contact.";
                    titlecount = 1;
                    break;
                case 1:
                    this.Title = "WFPrimeTool - Made By: RonkestDonk#7422 (Discord) Need Help? Feel Free To Contact.";
                    titlecount = 2;
                    break;
                case 2:
                    this.Title = "WFPrimeTool - Made By: RonkestDonk#7422 (Discord) Feeling Generous? Feel Free To Contact.";
                    titlecount = 0;
                    break;
            }
                
        }

        public async void stopworky(object sender, EventArgs e)
        {
            worker.Stop();
            worker.Tick -= new EventHandler(Workdowork);
            button.Content = "Error";
            stopwork.Stop();
        }
        public async void Workdowork(object sender, EventArgs e)
        {
            try
            {
                if (rowcount < 4 && trylimitc < trylimit)
                {
                    switch(scancount)
                    {
                        case 0:
                            button.Content = " Scanning.";
                            scancount = 1;
                            break;
                        case 1:
                            button.Content = "  Scanning..";
                            scancount = 2;
                            break;
                        case 2:
                            button.Content = "   Scanning...";
                            scancount = 0;
                            break;
                    }
                    trylimitc++;
                    cando2 = true;
                    string temps = "";
                    temps = OCRHandler.OCRWord();
                    temps = temps.Replace("\n", " ").Replace("\r", "");

                    textBox2.Text = temps;
                    string bname = "";
                    string temp;
                    string[] splittemps = new string[0];
                    if (CustomFilt.TryGetValue(textBox2.Text.Replace(" ", ""), out temp))
                    {
                        if (!textBox.Text.Contains(temp) && ItemUrl.ContainsKey(temp))
                        {
                            bname = temp;
                            splittemps = temp.Split('_');
                        }
                    }
                    else
                    {
                        splittemps = temps.Split(' ');
                    }
                    

                    for (int i = 0; i < splittemps.Length
                    && splittemps[i].ToString() != "Link"
                        && !splittemps[i].ToString().Contains("Blueprint")
                            && !splittemps[i].ToString().Contains("Neuroptic")
                                && !splittemps[i].ToString().Contains("Chassis")
                                    && !splittemps[i].ToString().Contains("Receiver")
                                        && !splittemps[i].ToString().Contains("Barrel")
                                          && !splittemps[i].ToString().Contains("Systems")
                                           && !splittemps[i].ToString().Contains("Blade")
                                               && !splittemps[i].ToString().Contains("String")
                                                  && !splittemps[i].ToString().Contains("Primed"); i++)
                        {
                        if (ItemName.Contains(splittemps[i].ToString().ToLower()) || ItemName.Contains(bname))
                        {
                            if(bname == "")
                            {

                            
                            var cc = 0;
                            for (int z = i; z < splittemps.Length; z++)
                            {
                               
                                if (splittemps[z].Length > 2)
                                {
                                    if (z == splittemps.Length - 1)
                                    {
                                        if (receiver.Contains(splittemps[z].ToLower()))
                                        {
                                                bname += "receiver";
                                        }
                                        else if (barrel.Contains(splittemps[z].ToLower()))
                                        {
                                                bname += "barrel";
                                        }
                                        else if (chassis.Contains(splittemps[z].ToLower()))
                                        {
                                                bname += "chassis";
                                        }
                                        else if (neuroptics.Contains(splittemps[z].ToLower()))
                                        {
                                                bname += "neuroptics";
                                        }
                                        else if (systems.Contains(splittemps[z].ToLower()))
                                        {
                                                bname += "systems";
                                        }
                                        else if (ornament.Contains(splittemps[z].ToLower()) && !splittemps[z].ToLower().Contains("ornament"))
                                        {
                                            bname += "ornament";
                                        }
                                        else
                                        {
                                                bname += splittemps[z].ToLower();
                                        }
                                    }
                                    if (z < splittemps.Length - 1 && cc < 4)
                                    {
                                        cc++;
                                        if (receiver.Contains(splittemps[z].ToLower()))
                                        {
                                            bname += "receiver" + "_";
                                        }
                                        else if (barrel.Contains(splittemps[z].ToLower()))
                                        {
                                            bname += "barrel" + "_";
                                        }
                                        else if (chassis.Contains(splittemps[z].ToLower()))
                                        {
                                            bname += "chassis" + "_";
                                        }
                                        else if (neuroptics.Contains(splittemps[z].ToLower()))
                                        {
                                            bname += "neuroptics" + "_";
                                        }
                                        else if (systems.Contains(splittemps[z].ToLower()) && splittemps[z].ToLower() != "systems")
                                        {
                                            bname += "systems" + "_";
                                        }
                                        else if (ornament.Contains(splittemps[z].ToLower()) && !splittemps[z].ToLower().Contains("ornament"))
                                        {
                                            bname += "ornament" + "_";
                                        }
                                        else
                                        {
                                            bname += splittemps[z].ToLower() +"_";
                                        }
                                    }
                                }
                                
                            }
                            bname = bname.ToLower();
                            if (bname.Contains("___") || bname.Contains("__"))
                            {
                                if (bname.Contains("___"))
                                {
                                    bname = bname.Replace("___", "_");
                                }
                                if (bname.Contains("__"))
                                {
                                    bname = bname.Replace("__", "_");
                                }
                                
                            }
                            foreach (string prim in prime)
                            {
                                if (bname.Contains(prim))
                                {
                                    if (prim == "_prim_")
                                    {
                                        bname = bname.Replace(prim, "_prime_");
                                    }
                                    else
                                    {
                                        bname = bname.Replace(prim, "prime");
                                    }
                                }
                                if (bname.Contains("primee"))
                                {
                                    bname = bname.Replace("primee", "prime");
                                }
                            }
                            foreach (string prim in neuroptics)
                            {
                                if (bname.Contains(prim))
                                {

                                   bname = bname.Replace(prim, "neuroptics");
                                    
                                }
                            }
                            foreach (string prim in chassis)
                            {
                                if (bname.Contains(prim))
                                {

                                    bname = bname.Replace(prim, "chassis");

                                }
                            }
                            foreach (string prim in systems)
                            {
                                if (bname.Contains(prim) && !bname.Contains("systems"))
                                {

                                    bname = bname.Replace(prim, "systems");

                                }
                            }
                            foreach (string prim in barrel)
                            {
                                if (bname.Contains(prim))
                                {

                                    bname = bname.Replace(prim, "barrel");

                                }
                            }
                            foreach (string prim in receiver)
                            {
                                if (bname.Contains(prim))
                                {

                                    bname = bname.Replace(prim, "receiver");

                                }
                            }
                            foreach (string prim in ornament)
                            {
                                if (bname.Contains(prim) && !bname.Contains("ornament"))
                                {

                                    bname = bname.Replace(prim, "ornament");

                                }
                            }
                            if (bname.Contains("neuroptics") || bname.Contains("chassis") || bname.Contains("systems"))
                            {
                                if (bname.Contains("blueprint"))
                                {
                                    bname = bname.Replace("_blueprint", "");
                                }
                            }
                            if (bname.EndsWith("_"))
                            {
                                bname = bname.Remove(bname.Length - 1);
                            }


                            }
                            if (!textBox.Text.Contains(bname) && ItemUrl.ContainsKey(bname))
                            {
                                textBox.Text += bname + "\n";
                            }
                            if (ItemUrl.ContainsKey(bname))
                            {


                               
                                int coundt = 0;
                                int resulty = 0;
                                do
                                {
                                    coundt++;
                                    resulty = OCRHandler.OCRCount();
                                    if (!MainWindow.ownitemcount.ContainsKey(bname) && resulty > 0)
                                    {
                                        MainWindow.ownitemcount.Add(bname, resulty);
                                        quantity = resulty;

                                    }
                                

                                }
                                while (quantity == 0 && coundt < 5);
                                if (dcount < 6)
                                {
                                    StartPosdo += incrementx;
                                    zStartPosdo += incrementx;
                                    trylimitc = 0;
                                    dcount++;
                                    somecount++;
                                    cando2 = false;
                                }
                                if (dcount == 6)
                                {
                                    dcount = 0;
                                    rowcount++;
                                    trylimitc = 0;
                                    StartPosdo = StartPos;
                                    zStartPosdo = zStartPos;
                                    StartPos2do += incrementy;
                                    zStartPos2do += incrementy;
                                    cando2 = false;
                                }
                            }
                            
                        }


                    }
                }
                else
                {
                    if (onescreen == true)
                    {
                        this.Show();
                    }
                    if (rowcount == 4)
                    {
                        worker.Stop();
                        worker.Tick -= new EventHandler(Workdowork);
                        button.Content = "Scan Prime Parts";
                        cando2 = false;
                        StartPosdo = StartPos;
                        StartPos2do = StartPos2;
                        zStartPos = zStartPosdo;
                        zStartPos2 = zStartPos2do;
                        trylimitc = 0;
                        dcount = 0;
                        rowcount = 0;
                        somecount = 0;
                        ItemCount.Content = "ScanCount: " + 0 + "/" + 24;
                        var countinbox1 = textBox.Text.Split('\n');
                        TotItemCount.Content = "Total: " + Convert.ToString(countinbox1.Length - 1);
                    }
                    if (trylimitc > trylimit || trylimitc == trylimit)
                    {

                        LogHandler.LogError(false, trylimit.ToString(), logshot1, logshot2, textBox2.Text);
                        WrongItemName(textBox2.Text, logshot1, logshot2);
                        button.Content = "Paused";
                        if (dcount < 6)
                        {
                            StartPosdo += incrementx;
                            zStartPosdo += incrementx;
                            trylimitc = 0;
                            dcount++;
                            somecount++;
                            cando2 = false;
                            worker.Stop();
                            worker.Tick -= new EventHandler(Workdowork);
                        }
                        if (dcount == 6)
                        {
                            dcount = 0;
                            rowcount++;
                            trylimitc = 0;
                            StartPosdo = 100;
                            zStartPosdo = 95;
                            StartPos2do += incrementy;
                            zStartPos2do += incrementy;
                            cando2 = false;
                            worker.Stop();
                            worker.Tick -= new EventHandler(Workdowork);
                        }
                    }
                }
            }
            catch(Exception d) { LogHandler.LogError(true, d.Message); 
            }
            ItemCount.Content = "ScanCount: " + somecount + "/" + 24;
            var countinbox = textBox.Text.Split('\n');
            TotItemCount.Content = "Total: " + Convert.ToString(countinbox.Length - 1);
        }

        public static void WrongItemName(string name, Bitmap image = null, Bitmap image2 = null)
        {
            if (sfname.IsInitialized == true && sfname.IsActive == false && SetFilterName.isopen == true)
            {
                SetFilterName.wrongname = name;
                if (image != null)
                {
                    SetFilterName.img1 = image;
                }
                if (image2 != null)
                {
                    SetFilterName.img2 = image2;
                }
                sfname.Top = mainw.Top + 120;
                sfname.Left = mainw.Left + 250;
                sfname.Show();
            }
            else if (SetFilterName.isopen == false)
            {
                sfname = new SetFilterName();
                SetFilterName.wrongname = name;
                if (image != null)
                {
                    SetFilterName.img1 = image;
                }
                if (image2 != null)
                {
                    SetFilterName.img2 = image2;
                }
                sfname.Top = mainw.Top + 120;
                sfname.Left = mainw.Left + 250;
                sfname.Show();
            }
            

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


        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
                BitmapImage bitmapimage = new BitmapImage();
                using (MemoryStream memory = new MemoryStream())
                {
                 if (bitmap != null)
                 {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();
                 }
                return bitmapimage;
                }
            

        }
        public class OrderData
        {
            public string name { get; set; }
            public int quantity { get; set; }
            public int platinum { get; set; }
            public int ducats { get; set; }
        }
        private async Task GetItem(string i)
        {

            string Check = i;
            try
            {
                oData = JsonConvert.DeserializeObject<dynamic>(client.DownloadString("https://api.warframe.market/v1/items/" + Check + "/orders?include=item"));

                int orderc = oData["payload"]["orders"].Count;
                foreach (var itemf in oData["payload"]["orders"])
                {
                    if (itemf["visible"] == true && itemf["user"]["status"] == "ingame" && itemf["order_type"] == "sell" && itemf["platinum"] != null && itemf["platinum"] > 0)
                    {
                        if (!iPrice.ContainsKey(itemf["user"]["ingame_name"].ToString()))
                        {
                            iPrice.Add(itemf["user"]["ingame_name"].ToString(), Convert.ToInt32(itemf["platinum"]));
                        }

                    }

                }
                var sortdict = iPrice.OrderBy(e => e.Value);
                try {
                   

                    if (sortdict != null)
                    {


                        if (!listBox.Items.Contains(Check + " Plat: " + sortdict.First().Value + " Ducat: "))
                        {
                            
                            var ducatlink = oData["include"]["item"]["items_in_set"];
                            int ducatprice = 0;
                            for (int vd = 0; vd < ducatlink.Count; vd++)
                            {
                                if (ducatlink[vd]["url_name"] == Check)
                                {
                                    ducatprice = ducatlink[vd]["ducats"];
                                }
                            }
                            int quantert = 0;
                            if (ownitemcount.TryGetValue(Check, out quantert))
                            {
                                orddat.Add(new OrderData() { name = Check, quantity = quantert, ducats = ducatprice, platinum = sortdict.First().Value });
                                listBox.Items.Add(String.Format("Plat: " + sortdict.First().Value + " |" + " Ducat: " + ducatprice + " |" + " " + " X: " + quantert + " |" + " " + Check)); //+ " |" + " " + " X: " + quantert + 
                                answer = " Plat: " + sortdict.First().Value + " Ducat: " + ducatprice + "  " + Check;
                            }
                            else
                            {
                                orddat.Add(new OrderData() { name = Check, quantity = 1, ducats = ducatprice, platinum = sortdict.First().Value });
                                listBox.Items.Add(String.Format("Plat: " + sortdict.First().Value + " |" + " Ducat: " + ducatprice + " |" + " " + " X: " + "1" + " |" + " " + Check)); // " |" + " " + " X: " + "1" + 
                                answer = " Plat: " + sortdict.First().Value + " Ducat: " + ducatprice + "  " + Check;
                            }
                    }


                    iPrice.Clear();
                }
                else
                {
                    if (!listBox.Items.Contains(Check + " Plat: " + 0 + " Ducat: "))
                    {
                        //duData = JsonConvert.DeserializeObject<dynamic>(client.DownloadString("https://api.warframe.market/v1/items/" + Check));
                        var ducatlink = oData["include"]["item"]["items_in_set"];
                        int ducatprice = 0;
                        for (int vd = 0; vd < ducatlink.Count; vd++)
                        {
                            if (ducatlink[vd]["url_name"] == Check)
                            {
                                ducatprice = ducatlink[vd]["ducats"];
                            }
                        }
                        int quantert = 0;
                        if(ownitemcount.TryGetValue(Check, out quantert))
                        {
                                orddat.Add(new OrderData() { name = Check, quantity = quantert, ducats = ducatprice, platinum = 0 });
                                listBox.Items.Add(String.Format("Plat: " + 0 + " |" + " Ducat: " + ducatprice + " |" + " " + " X: " + quantert + " | " + Check)); //  
                                answer = " Plat: " + sortdict.First().Value + " Ducat: " + ducatprice + "  " + " X: " + quantert + "  " + Check;
                        }
                        else
                        {
                                orddat.Add(new OrderData() { name = Check, quantity = 1, ducats = ducatprice, platinum = 0 });
                                listBox.Items.Add(String.Format("Plat: " + 0 + " |" + " Ducat: " + ducatprice + " |" + " " + Check)); // 
                                answer = " Plat: " + sortdict.First().Value + " Ducat: " + ducatprice + Check;
                        }
                        
                    }


                    iPrice.Clear();
                }
                }
                catch(Exception e)
                {
                    LogHandler.LogError(true, e.Message);
                    if (!listBox.Items.Contains(Check + " Plat: " + 0 + " Ducat: "))
                    {
                        try
                        {

                        
                        //duData = JsonConvert.DeserializeObject<dynamic>(client.DownloadString("https://api.warframe.market/v1/items/" + Check));
                        var ducatlink = oData["include"]["item"]["items_in_set"];
                        int ducatprice = 0;
                        for (int vd = 0; vd < ducatlink.Count; vd++)
                        {
                            if (ducatlink[vd]["url_name"] == Check)
                            {
                                ducatprice = ducatlink[vd]["ducats"];
                            }
                        }
                        orddat.Add(new OrderData() { name = Check, quantity = 1, ducats = ducatprice, platinum = 0 });
                        listBox.Items.Add(String.Format("{0}|{1}|{2}", "Plat: " + 0, " Ducat: " + ducatprice, " " + Check));
                        answer = " Plat: " + "X" + " Ducat: " + ducatprice + "  " + Check;
                        }
                        catch (WebException z)
                        {
                            LogHandler.LogError(true, z.Message);
                            orddat.Add(new OrderData() { name = Check, quantity = 1, ducats = 0, platinum = 0 });
                            listBox.Items.Add(String.Format("{0}|{1}|{2}", "Plat: " + 0, " Ducat: " + 0, " " + Check));
                            answer = " Plat: " + "X" + " Ducat: " + 0 + "  " + Check;
                        }
                    }


                    iPrice.Clear();
                }
            }
            catch(JsonReaderException z)
            {
                LogHandler.LogError(true, z.Message);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if(!isdata)
            {
             if (!donce)
             {
                donce = true;
           
                foreach(var item in listBox.Items)
                {
                    var splitthat = item.ToString().Split(' ');
                   int type = 0;
                 try 
                 { 
                       if(Convert.ToInt32(splitthat[sortval]).GetType() == type.GetType())
                       {
                            
                                            SortDem.Add(item.ToString(), Convert.ToInt32(splitthat[sortval]));
                            
                            
                       }

                }
                 catch(FormatException b) { LogHandler.LogError(true, b.Message); }
                        if (sortval == 10)
                        {
                            SortDem2.Add(item.ToString());
                        }
                    }
            var shortdict = SortDem.OrderBy(f => f.Value);
            
            listBox.Items.Clear();
            orddat.Clear();
            if(sortval == 1 || sortval == 4 || sortval == 8)
            {
                        foreach (var item in shortdict)
                        {
                            var tempstring = "";
                            var fixkey = item.Key.Split(' ');
                            foreach (var thing in fixkey)
                            {
                                if (thing != fixkey[sortval] && thing != fixkey.Last())
                                {
                                    tempstring += thing + " ";
                                }
                                else
                                {
                                    if (thing == fixkey.Last())
                                    {
                                        tempstring += thing;
                                    }
                                    if (thing == fixkey[sortval])
                                    {
                                        tempstring += item.Value.ToString() + " ";
                                    }

                                }

                            }
                            var splittemp = tempstring.Split(' ');
                            orddat.Add(new OrderData() { name = splittemp[10], quantity = int.Parse(splittemp[8]), ducats = int.Parse(splittemp[4]), platinum = int.Parse(splittemp[1]) });
                            listBox.Items.Add(tempstring);
                        }
            }
            else
            {   

                        List<string> shortdict2 = new List<string>();
                        foreach(var item in SortDem2)
                        {
                            string tempi = "";
                            var tempz = item.Split(' ');
                            string first = tempz[0];
                            string second = tempz[1];
                            string ducat = tempz[4];
                            for(int i = 0; i < tempz.Length; i++)
                            {
                                if (tempz[i] == first)
                                {
                                    tempi += tempz.Last() + " ";
                                }
                                else if(tempz[i] != tempz.Last() && tempz[i] != first && i != 1)
                                {

                                        tempi += tempz[i] + " ";

                                }
                                if(tempz[i] == tempz.Last())
                                {
                                    tempi += first + " " + second;
                                    if (!shortdict2.Contains(tempi))
                                    {
                                        shortdict2.Add(tempi);
                                    }
                                    
                                }
                                
                            }
                        }
                        SortDem2.Clear();
                        shortdict2.Sort();
                        foreach (var item in shortdict2)
                        {
                            string tempi = "";
                            var tempz = item.Split(' ');
                            string first = tempz[0];
                            string second = tempz[tempz.Length - 2];
                            for(int i = 0; i < tempz.Length; i++)
                            {
                                if (tempz[i] == first)
                                {
                                    tempi += second + " " + tempz.Last() + " ";
                                }
                                else if (i != tempz.Length - 1 && tempz[i] != first && tempz[i] != second)
                                {
                                    tempi += tempz[i] + " ";
                                }
                                if (i == tempz.Length - 1)
                                {
                                    tempi += first;
                                    SortDem2.Add(tempi);

                                }

                            }
                        }
                        foreach(var item in SortDem2)
                        {
                            var splittemp = item.Split(' ');
                            orddat.Add(new OrderData() { name = splittemp[10], quantity = int.Parse(splittemp[8]), ducats = int.Parse(splittemp[4]), platinum = int.Parse(splittemp[1]) });
                            listBox.Items.Add(item);
                        }
                        SortDem2.Clear();
                    }
                    

                 switch (sortval)
                 {
                        case 1:
                            sortval = 4;
                            SortDem.Clear();
                            donce = false;
                            break;
                        case 4:
                            sortval = 8;
                            SortDem.Clear();
                            donce = false;
                            break;
                        case 8:
                            sortval = 10;
                            SortDem.Clear();
                            donce = false;
                            break;
                        case 10:
                            sortval = 1;
                            SortDem2.Clear();
                            donce = false;
                            break;
                 }

                }
            }
        }
        private async void Timertick(object sender, EventArgs e)
        {
            if (cando == false)
            {
                isdata = true;
                cando = true;
                if (countg < ownitemurl.Count && ItemUrl.ContainsKey(ownitemurl[countg]))
                {
                    await GetItem(ownitemurl[countg]);
               
                }
                else
                {
                    
                    d.Stop();
                    d.Tick -= new EventHandler(Timertick);
                    cando = false;
                    if(countg == ownitemurl.Count)
                    {
                        ownitemurl.Clear();
                        countg = 0;
                        GetCount.Content = "GetCount: " + countg + "/" + ownitemurl.Count;
                        isdata = false;
                    }
                    
                }
                
            }
            if (answer != "" && answer != null && cando == true)
            {
                answer = "";
                countg++;
                GetCount.Content = "GetCount: " + countg + "/" + ownitemurl.Count;
                cando = false;
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            worker.Stop();
            worker.Tick -= new EventHandler(Workdowork);
            cando2 = false;
            StartPos = 100;
            StartPos2 = 290;
            dcount = 0;
            rowcount = 0;
            somecount = 0;
            ItemCount.Content = "ScanCount: " + somecount + "/" + 24;
            var splitz = textBox.Text.Split('\n');
            for (int i = 0; i < splitz.Length; i++)
            {
                if (!ownitemurl.Contains(splitz[i]) && splitz[i] != "")
                {
                    ownitemurl.Add(splitz[i]);
                }
               
            }
            textBox.Text = "";
            
                d.Tick += new EventHandler(Timertick);
                d.Interval = TimeSpan.FromMilliseconds(340);
                d.Start();
            
            
            
        }
        public static void CustomConfig()
        {
            var path = @"CustomFilter.txt";
            if (File.Exists(path))
            {
                using (FileStream fstream = File.OpenRead(path))
                {
                    byte[] b = new byte[fstream.Length];
                    var fulltext = fstream.Read(b, 0, b.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string holder = temp.GetString(b);
                    holder = holder.Replace("]\n", "],");
                    var jsons = JsonConvert.DeserializeObject<dynamic>(holder);
                    foreach (var item in jsons)
                    {
                        foreach (var itom in item)
                        {
                          
                                string tempzo;
                                if (!CustomFilt.TryGetValue(itom.Path, out tempzo))
                                {
                                string tempclean = itom.Path.Replace("'", "").Replace("[", "").Replace("]", "");
                                    if (!CustomFilt.ContainsKey(tempclean))
                                    {
                                      CustomFilt.Add(tempclean, itom.ToString());
                                    }
                                    
                                }
                            

                        }

                    }
                }
            }
            else
            {
                SaveSettings(false);
            }
        }
       private void LoadSettings()
       {
            var path = @"Settings.txt";
            if (File.Exists(path))
            {
                using (FileStream sr = File.OpenRead(path))
                {
                    byte[] b = new byte[sr.Length];
                    var fulltext = sr.Read(b, 0, b.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string hold = temp.GetString(b);
                    dynamic datasplit = hold.Split('\n', '=');
                    Logging = bool.Parse(datasplit[1]);
                    onescreen = bool.Parse(datasplit[3]);
                    limitenabled = bool.Parse(datasplit[5]);
                    trylimit = int.Parse(datasplit[7]);
                    ingame_name = datasplit[9];
                    ingamestatus = datasplit[11];
                    laststat = bool.Parse(datasplit[13]);
                }
            }
            else
            {
                SaveSettings(true);
            }
       }
        public static void SaveSettings(bool settings)
        { 
            if(settings == true)
            {
                var path = @"Settings.txt";
                using (FileStream sr = File.OpenWrite(path))
                {
                    AddText(sr, "Logging=" + Logging + "\n" + "HideScreen=" + onescreen + "\n" + "RetryScan=" + limitenabled + "\n" + "RetryLimit=" + trylimit + "\n" + "IngameName=" + ingame_name + "\n" + "status=" + ingamestatus + "\n" + "laststatus=" + laststat + "\n");
                }
            }
            else
            {
                if(CustomFilt.Count > 0)
                {
                    var path = @"CustomFilter.txt";
                    using (FileStream sr = File.OpenWrite(path))
                    {
                        var json = JsonConvert.SerializeObject(CustomFilt);
                        AddText(sr, json.Replace("],", "]\n"));
                    }
                }
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            orddat.Clear();
            listBox.Items.Clear();
            worker.Stop();
            worker.Tick -= new EventHandler(Workdowork);
            sortval = 1;
                        cando2 = false;
                        StartPos = 100;
                        StartPos2 = 290;
                        dcount = 0;
                        rowcount = 0;
                        somecount = 0;
        }
 
        public static void CheckFilter()
        {
            var path = @"filter.txt";
            if (!File.Exists(path))
            {
                using (FileStream fstream = File.Create(path))
                {
                    prime.Add("fime");
                    prime.Add("frime");
                    prime.Add("_prim_");
                    prime.Add("pimey");
                    prime.Add("primdl");
                    prime.Add("primed");
                    prime.Add("primev");
                    prime.Add("pnimey");
                    prime.Add("biime");
                    prime.Add("prirfie");
                    prime.Add("prine");
                    prime.Add("primé");
                    prime.Add("prinfe");
                    prime.Add("ptime");
                    prime.Add("primd");
                    prime.Add("primef");
                    prime.Add("primey");
                    prime.Add("prinid");
                    prime.Add("primid");
                    prime.Add("primi");
                    ornament.Add("rnament");
                    ornament.Add("rhament");
                    neuroptics.Add("nelroptics");
                    neuroptics.Add("Nettoptics");
                    systems.Add("stems");
                    systems.Add("sytems");
                    receiver.Add("recelver");

                    //neuroptics.Add("nopetri");
                    //chassis.Add("cssis");
                    //barrel.Add("parrel");
                    ifilt.Add("prime", prime);
                    ifilt.Add("neuroptics", neuroptics);
                    ifilt.Add("chassis", chassis);
                    ifilt.Add("systems", systems);
                    ifilt.Add("receiver", receiver);
                    ifilt.Add("barrel", barrel);
                    ifilt.Add("ornament", ornament);
                    var json = JsonConvert.SerializeObject(ifilt);
                    //textBox.Text = json.Replace("],", "]\n");
                    AddText(fstream, json.Replace("],", "]\n"));
                }
            }
            else
            {
                string holder = "";
                
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
                                foreach (var filtom in itom)
                                {

                                    if (filtom.Path.Contains("prime") && !prime.Contains(filtom.ToString()))
                                    {
                                        prime.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("neuroptics") && !neuroptics.Contains(filtom.ToString()))
                                    {
                                        neuroptics.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("chassis") && !chassis.Contains(filtom.ToString()))
                                    {
                                        chassis.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("systems") && !systems.Contains(filtom.ToString()))
                                    {
                                        systems.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("receiver") && !receiver.Contains(filtom.ToString()))
                                    {
                                        receiver.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("barrel") && !barrel.Contains(filtom.ToString()))
                                    {
                                        barrel.Add(filtom.ToString());
                                    }
                                    else if (filtom.Path.Contains("ornament") && !barrel.Contains(filtom.ToString()))
                                    {
                                        ornament.Add(filtom.ToString());
                                    }
                            }

                            }

                        }
                    }
                }

        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            foreach(string item in prime)
            {
                textBox.Text += item + "\n";
            }
        }



        private void listView_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (itemview.SelectedItem != null)
            {


                if (itemview.SelectedItem.ToString().Split(' ').Length > 0)
                {
                    var s = itemview.SelectedItem.ToString().Split(' ');
                    OrderData b = (OrderData)itemview.SelectedItem;
                    if (s.Last() != "")
                    {
                        if (openorder.IsChecked == true && repeats != b.name)
                        {
                            System.Diagnostics.Process.Start("https://warframe.market/items/" + b.name);
                            repeats = b.name;
                        }
                        if (copyname.IsChecked == true)
                        {
                            var name = b.name.Replace("_", " ");
                            Clipboard.SetText(name);
                        }
                        if (placeorder.IsChecked == true && Requests.JWT != "")
                        {

                            
                            var active = orderWindow.Visibility;
                            if (active == Visibility.Hidden || active == Visibility.Collapsed)
                            {
                                var name = b.name.Replace("_", " ");
                                OrderWindow.itemname = name;
                                string imgurl;
                                if (ItemImgUrl.TryGetValue(b.name, out imgurl))
                                {
                                    OrderWindow.openorder = "https://warframe.market/items/" + b.name;
                                    OrderWindow.itemimgurl = imgurl;
                                    Orderitem = b.name;
                                    orderWindow.Top = this.Top + 150;
                                    orderWindow.Left = this.Left + 350;
                                    orderWindow.Show();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (copyname.IsChecked == true)
                        {
                            var name = b.name.Replace("_", " ");
                            Clipboard.SetText(name);
                        }
                        if (placeorder.IsChecked == true && Requests.JWT != "")
                        {
                            var active = orderWindow.Visibility;
                            if (active == Visibility.Hidden)
                            {
                                var name = b.name.Replace("_", " ");
                                OrderWindow.itemname = name;
                                string imgurl;
                                int quantit;
                                if (ItemImgUrl.TryGetValue(b.name, out imgurl))
                                {
                                    if (ownitemcount.TryGetValue(b.name, out quantit))
                                    {
                                        OrderWindow.quant = quantit;
                                        OrderWindow.openorder = "https://warframe.market/items/" + b.name;
                                        OrderWindow.itemimgurl = imgurl;
                                        Orderitem = b.name;
                                        orderWindow.Show();
                                    }
                                    else
                                    {
                                        OrderWindow.quant = 1;
                                        OrderWindow.openorder = "https://warframe.market/items/" + b.name;
                                        OrderWindow.itemimgurl = imgurl;
                                        Orderitem = b.name;
                                        orderWindow.Show();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void PlaceOrder(int plat, int quantity)
        {
            string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
            string itemid;
            if (ItemUrl.TryGetValue(Orderitem, out itemid))
            {
                var response = Requests.Request("https://api.warframe.market/v1/profile/orders", "POST", JsonConvert.SerializeObject(new { order_type = "sell", item_id = itemid, platinum = plat, quantity = quantity }), headers);
            }
        }
        static bool flip = false;
        private async void button6_Click(object sender, RoutedEventArgs e)
        {
            if(mailbox.Text != "username")
            {

            
            passbox.PasswordChar = '*';
           
            var visible = Visibility.Visible;
            var hidden = Visibility.Hidden;
            if (passbox.Visibility == visible && passbox.Password.Length > 0 && mailbox.Text.Length > 3)
            {
                flip = true;
                string[] headers = (new[] {"Authorization", "JWT", "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                
                await Requests.Request("https://api.warframe.market/v1/auth/signin", "POST", JsonConvert.SerializeObject(new {  email = mailbox.Text, password = passbox.Password, auth_type = "header" }), headers);
                var data = JsonConvert.DeserializeObject<dynamic>(Requests.responsebody);
                if (Requests.Response == "OK" && data["payload"]["user"]["ingame_name"] != "")
                {
                    
                    ingame_name = data["payload"]["user"]["ingame_name"];
                    SaveSettings(true);
                    button6.Content = "Logout";
                    Connect();
                    mailbox.Visibility = hidden;
                    passbox.Visibility = hidden;
                    passlab.Visibility = hidden;
                    maillab.Visibility = hidden;
                    logwin.Visibility = hidden;
                    savelog.Visibility = hidden;
                    Orders.Visibility = visible;
                    placeorder.Visibility = visible;
                }
                    if (Requests.responsebody.ToString().ToLower().Contains("error"))
                    {
                        ErrorLoggingIn();
                    }

            }
            else if (passbox.Visibility == hidden && flip == true)
            {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WFPrimeTool", true);
                    if (key != null)
                    {
                        if(key.GetValue("JWT") != null)
                        {
                            key.DeleteValue("JWT");
                            key.Close();
                        }
                    }
                    ChangeStatus.Logout();
                    Orders.Visibility = hidden;
                    placeorder.Visibility = hidden;
                    ingame_name = "";
                    SaveSettings(true);
                    flip = false;
                    button6.Content = "Login WFM";
                    Requests.JWT = "";
            }
            else if (passbox.Visibility == visible )
            {
                    
                        mailbox.Visibility = hidden;
                        passbox.Visibility = hidden;
                        passlab.Visibility = hidden;
                        maillab.Visibility = hidden;
                        logwin.Visibility = hidden;
                        savelog.Visibility = hidden;

            }
                else if (!flip && button6.Content != "Logout")
                {

                    mailbox.Visibility = visible;
                    passbox.Visibility = visible;
                    passlab.Visibility = visible;
                    maillab.Visibility = visible;
                    logwin.Visibility = visible;
                    savelog.Visibility = visible;
                    Orders.Visibility = hidden;
                    placeorder.Visibility = hidden;
                }
            }

        }
        public async void ErrorLoggingIn()
        {
            errorpan.Visibility = Visibility.Visible;
            errorlab.Visibility = Visibility.Visible;
            var jsobn = JsonConvert.DeserializeObject<dynamic>(Requests.responsebody);
            errorlab.Content = "Error: " + jsobn["error"];
            await Task.Delay(3000);
            errorpan.Visibility = Visibility.Hidden;
            errorlab.Visibility = Visibility.Hidden;

        }
        public async void Loggedin()
        {
            var visible = Visibility.Visible;
            var hidden = Visibility.Hidden;            
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WFPrimeTool", true);
            if (key != null)
            {
                if (key.GetValue("JWT") != null)
                {
                    dynamic tempo = key.GetValue("JWT");
                    Requests.JWT = Encoding.ASCII.GetString(tempo);
                    
                }
                
            }
            if (Requests.JWT != "" && ingame_name != "")
            {
                flip = true;
                //textBox.Text = Requests.Response;
                button6.Content = "Logout";
                mailbox.Visibility = hidden;
                passbox.Visibility = hidden;
                passlab.Visibility = hidden;
                maillab.Visibility = hidden;
                logwin.Visibility = hidden;
                savelog.Visibility = hidden;
                Orders.Visibility = visible;
                placeorder.Visibility = visible;
                Connect();
            }
            else
            {
                if (key != null)
                {
                    if (key.GetValue("JWT") != null)
                    {
                        key.DeleteValue("JWT");
                        key.Close();
                    }
                }
                ChangeStatus.Logout();
                Orders.Visibility = hidden;
                placeorder.Visibility = hidden;
                ingame_name = "";
                SaveSettings(true);
                flip = false;
                button6.Content = "Login WFM";
                Requests.JWT = "";
            }
            if(key != null)
            {
                key.Close();
            }
        }
        private void OpenOrder_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CopyName_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PlaceOrder_Checked(object sender, RoutedEventArgs e)
        {

        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            await ChangeStatus.SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
            SaveSettings(true);
            await Task.Delay(500);
            Process.GetCurrentProcess().Kill();
        }

        private void testbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            Requests.savejwt = true;
        }
        private void checkBoxsy_Unchecked(object sender, RoutedEventArgs e)
        {
            Requests.savejwt = false;
        }


        private void settings_Click(object sender, RoutedEventArgs e)
        {
            if(settingsWindow.IsActive == false && SettingsWindow.closed == false)
            {
                settingsWindow.Top = this.Top + 80;
                settingsWindow.Left = this.Left + 260;
                SettingsWindow.onescreen = onescreen;
                SettingsWindow.Logging = Logging;
                SettingsWindow.limiton = limitenabled;
                SettingsWindow.laststat = laststat;
                settingsWindow.Show();
            }
            else if(SettingsWindow.closed == true)
            {
                
                settingsWindow.Top = this.Top + 80;
                settingsWindow.Left = this.Left + 260;
                SettingsWindow.onescreen = onescreen;
                SettingsWindow.Logging = Logging;
                SettingsWindow.limiton = limitenabled;
                SettingsWindow.laststat = laststat;
                settingsWindow.Show();
            }
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/ZyXJjbSqqs");
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (Requests.JWT != "" && ingame_name != "")
            {
                if (marketHandler.IsActive == false && MarketHandler.closed == false)
                {
                    MarketHandler.lastresponse = ingamestatus;
                    marketHandler.Top = this.Top + 50;
                    marketHandler.Left = this.Left + 80;
                    marketHandler.Show();
                }
                else if (MarketHandler.closed == true)
                {
                   
                    MarketHandler.lastresponse = ingamestatus;
                  
                    
                    marketHandler.Top = this.Top + 50;
                    marketHandler.Left = this.Left + 80;
                    marketHandler.Show();
                }


            }
            else
            {
                ChangeStatus.Logout();
            }

        }

        public static void Status_win()
        {
            if (Requests.JWT != "" && ingame_name != "")
            {
                if (changeStatus.IsActive == false && ChangeStatus.closed == false)
                {
                    changeStatus.Top = mwin.Top + 180;
                    changeStatus.Left = mwin.Left + 260;
                    changeStatus.Show();
                }
                else if (ChangeStatus.closed == true)
                {
                    changeStatus.Top = mwin.Top + 180;
                    changeStatus.Left = mwin.Left + 260;
                    changeStatus.Show();
                }
            }

        }


    }
}
