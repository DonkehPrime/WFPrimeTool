using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using WFPrimeTool.OrderFunctions;

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for MarketHandler.xaml
    /// </summary>
    public partial class MarketHandler : Window
    {

        public string[,] BuyOrders;
        public string[,] SellOrders;
        ObservableCollection<OrderData> slist;
        ObservableCollection<OrderData> blist;
        public event PropertyChangedEventHandler PropertyChanged;
        public static DispatcherTimer checkstat = new DispatcherTimer();
        public static dynamic response;
        public static dynamic logresponse;
        public static string lastresponse = "";
        public static bool closed = false;
        public static bool firstrun = false;
        public MarketHandler()
        {
            InitializeComponent();
            slist = new ObservableCollection<OrderData>();
            blist = new ObservableCollection<OrderData>();
            selllist.ItemsSource = slist;
            buylist.ItemsSource = blist;
            checkstat.Interval = TimeSpan.FromMilliseconds(500);
            checkstat.Tick += new EventHandler(NewTimer);


        }
        public async void MyOrders(bool docheck, string zresponse = "")
        {
            var response = zresponse;
            if (docheck == true)
            {
                string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                response = await Requests.Request("https://api.warframe.market/v1/profile/" + MainWindow.ingame_name + "/orders", "GET", null, headers);
            }

            if (!response.Contains("error") && response != "" && response.Contains("sell_orders"))
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response);
                int count = data["payload"]["sell_orders"].Count;
                var datasell = data["payload"]["sell_orders"];
                int count2 = data["payload"]["buy_orders"].Count;
                var databuy = data["payload"]["buy_orders"];
                if (count > 0)
                {

                    for (int i = 0; i < count; i++)
                    {
                        var nam = datasell[i]["item"]["url_name"];
                        var amn = datasell[i]["quantity"];
                        var plat = datasell[i]["platinum"];
                        var vis = datasell[i]["visible"];
                        var id = datasell[i]["id"];
                        slist.Add(new OrderData() { name = nam, quantity = amn, platinum = plat, visible = vis, id = id });
                    }
                    
                }
                if (count2 > 0)
                {
                   

                    
                    for (int i = 0; i < count2; i++)
                    {

                        var nam = databuy[i]["item"]["url_name"];
                        var amn = databuy[i]["quantity"];
                        var plat = databuy[i]["platinum"];
                        var vis = databuy[i]["visible"];
                        var id = databuy[i]["id"];
                        blist.Add(new OrderData() { name = nam, quantity = amn, platinum = plat, visible = vis, id = id });
                    }
                    

                }
            }
            else if(!response.Contains("sell_orders") && !response.Contains("error"))
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response);
                if (data != null && data.Count > 0)
                {
                    if (data["payload"]["order"]["order_type"] == "sell")
                    {
                        var nam = data["payload"]["order"]["item"]["url_name"];
                        var amn = data["payload"]["order"]["quantity"];
                        var plati = data["payload"]["order"]["platinum"];
                        var visib = data["payload"]["order"]["visible"];
                        var id = data["payload"]["order"]["id"];
                        slist.Add(new OrderData() { name = nam, quantity = amn, platinum = plati, visible = visib, id = id });

                    }
                    else if (data["payload"]["order"]["order_type"] == "buy" && data.Count > 0)
                    {
                        var nam = data["payload"]["order"]["item"]["url_name"];
                        var amn = data["payload"]["order"]["quantity"];
                        var plati = data["payload"]["order"]["platinum"];
                        var visib = data["payload"]["order"]["visible"];
                        var id = data["payload"]["order"]["id"];
                        blist.Add(new OrderData() { name = nam, quantity = amn, platinum = plati, visible = visib, id = id });
                    }
                }

            }
        }
       

        public class OrderData
        {
            public string name { get; set;}
            public int quantity { get; set; }
            public int platinum { get; set; }
            public bool visible { get; set; }
            public string id { get; set; }
        }

        private async void soldb_Click(object sender, RoutedEventArgs e)
        {
            OrderData temp = null;
            if (selllist.SelectedItem != null)
            {
                temp = (OrderData)selllist.SelectedItem;
            
                string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                await Task.Delay(400);
                var response = await Requests.Request("https://api.warframe.market/v1/profile/orders/close/"+temp.id, "PUT", JsonConvert.SerializeObject(new { platinum = temp.platinum, quantity = temp.quantity, visible = temp.visible }), headers);
                slist.Remove(temp);
                MyOrders(false, response);
            }
        }

        private void selllist_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private async void hideb_Click(object sender, RoutedEventArgs e)
        {
            OrderData temp = null;
            if (selllist.SelectedItem != null)
            {
                temp = (OrderData)selllist.SelectedItem;

                string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                await Task.Delay(400);
                var response = await Requests.Request("https://api.warframe.market/v1/profile/orders/" + temp.id, "PUT", JsonConvert.SerializeObject(new { order_id = temp.id, platinum = Convert.ToInt32(platbox.Text), quantity = Convert.ToInt32(quantbox.Text), visible = checkBox.IsChecked}), headers);
                slist.Remove(temp);
                MyOrders(false, response);
            }
            if (buylist.SelectedItem != null)
            {
                temp = (OrderData)buylist.SelectedItem;

                string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                await Task.Delay(400);
                var response = await Requests.Request("https://api.warframe.market/v1/profile/orders/" + temp.id, "PUT", JsonConvert.SerializeObject(new { order_id = temp.id, platinum = Convert.ToInt32(platbox.Text), quantity = Convert.ToInt32(quantbox.Text), visible = checkBox.IsChecked}), headers);
                slist.Remove(temp);
                MyOrders(false, response);
            }
        }

        private void selllist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            quantbox.Visibility = Visibility.Visible;
            platbox.Visibility = Visibility.Visible;
            quantlab.Visibility = Visibility.Visible;
            platlab.Visibility = Visibility.Visible;
            ordbox.Visibility = Visibility.Visible;
            checkBox.Visibility = Visibility.Visible;
            hideb.Visibility = Visibility.Visible;
            hidebclose.Visibility = Visibility.Visible;
            var temp = (OrderData)selllist.SelectedItem;
            var temp2 = (OrderData)buylist.SelectedItem;
            if (temp != null && temp2 == null)
            {
                if (temp.visible == true)
                {
                    checkBox.IsChecked = true;
                    checkBox.Content = "Hide";
                }
                else if (temp.visible == false)
                {
                    checkBox.IsChecked = false;
                    checkBox.Content = "Show";
                }
                platbox.Text = temp.platinum.ToString();
                quantbox.Text = temp.quantity.ToString();
            }
            if (temp2 != null && temp == null)
            {
                if (temp.visible == true)
                {
                    checkBox.IsChecked = true;
                    checkBox.Content = "Hide";
                }
                else if (temp.visible == false)
                {
                    checkBox.IsChecked = false;
                    checkBox.Content = "Show";
                }
                platbox.Text = temp2.platinum.ToString();
                quantbox.Text = temp2.quantity.ToString();
            }
            if(temp == null && temp2 == null)
            {
                quantbox.Visibility = Visibility.Hidden;
                platbox.Visibility = Visibility.Hidden;
                quantlab.Visibility = Visibility.Hidden;
                platlab.Visibility = Visibility.Hidden;
                ordbox.Visibility = Visibility.Hidden;
                checkBox.Visibility = Visibility.Hidden;
                hideb.Visibility = Visibility.Hidden;
                hidebclose.Visibility = Visibility.Hidden;
            }
        }
        public void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            closed = true;
            MainWindow.marketHandler = new MarketHandler();
        }

        private async void boughtb_Click(object sender, RoutedEventArgs e)
        {
            if (selllist.IsFocused != true)
            {
                var temp = (OrderData)selllist.SelectedItem;

                string[] headers = (new[] { "Authorization", "JWT " + Requests.JWT, "language", "en", "accept", "application/json", "platform", "pc", "auth_type", "header" });
                await Task.Delay(400);
                var response = await Requests.Request("https://api.warframe.market/v1/profile/orders/close/" + temp.id, "PUT", JsonConvert.SerializeObject(new { platinum = temp.platinum, quantity = temp.quantity, visible = temp.visible }), headers);
                slist.Remove(temp);
                MyOrders(false, response);
            }  
        }

        private void buylist_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = (OrderData)buylist.SelectedItem;
            if (temp != null && selllist.IsFocused != true)
            {
                if (temp.visible == true)
                {
                    checkBox.IsChecked = true;
                }
                else if (temp.visible == false)
                {
                    checkBox.IsChecked = false;
                }
                platbox.Text = temp.platinum.ToString();
                quantbox.Text = temp.quantity.ToString();
            }
        }

        private async void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MyOrders(true);
            if (MainWindow.ingame_name != "")
            {
                Title = "Market Management - Welcome: " + MainWindow.ingame_name;
            }
            if (MainWindow.ingame_name == "")
            {
                Title = "Market Management - Please Login..";
            }
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Status_win();
            
        }

        public void NewTimer(object sender, EventArgs e)
        {
            if (logresponse.ToLower().Contains("invisible"))
            {
                OrdersStat.Content = "Status: Invisible";
                MainWindow.ingamestatus = logresponse.ToLower();
                MainWindow.SaveSettings(true);
                checkstat.Stop();

            }
            if (logresponse.ToLower().Contains("online"))
            {
                OrdersStat.Content = "Status: Online";
                MainWindow.ingamestatus = logresponse.ToLower();
                MainWindow.SaveSettings(true);
                checkstat.Stop();
            }
            if (logresponse.ToLower().Contains("ingame"))
            {
                OrdersStat.Content = "Status: In Game";
                MainWindow.ingamestatus = logresponse.ToLower();
                MainWindow.SaveSettings(true);
                checkstat.Stop();
            }
        }

        private void hidebclose_Click(object sender, RoutedEventArgs e)
        {
            selllist.SelectedItem = null;
            buylist.SelectedItem = null;
            quantbox.Visibility = Visibility.Hidden;
            platbox.Visibility = Visibility.Hidden;
            quantlab.Visibility = Visibility.Hidden;
            platlab.Visibility = Visibility.Hidden;
            ordbox.Visibility = Visibility.Hidden;
            checkBox.Visibility = Visibility.Hidden;
            hideb.Visibility = Visibility.Hidden;
            hidebclose.Visibility = Visibility.Hidden;
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {

            await Task.Delay(1000);
            if (this.Visibility == Visibility.Visible || this.Visibility == Visibility.Collapsed || this.Visibility == Visibility.Hidden)
            {
                
                
                if (logresponse != null && MainWindow.ingamestatus != null)
                {
                    checkstat.Start();
                    if (!logresponse.ToLower().Contains(MainWindow.ingamestatus) && logresponse != lastresponse && MainWindow.laststat == true && firstrun == false)
                    {
                        await ChangeStatus.SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"" + MainWindow.ingamestatus + "\"}");
                        lastresponse = logresponse.ToLower();
                        firstrun = true;
                    }
                    if (!logresponse.ToLower().Contains("invisible") && logresponse != lastresponse && MainWindow.laststat == false && firstrun == false)
                    {
                        await ChangeStatus.SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
                        lastresponse = logresponse.ToLower();
                        firstrun = true;
                    }
                }

            }
        }
    }
}
