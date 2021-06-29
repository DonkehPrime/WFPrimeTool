using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
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
using System.Security.Authentication;
using Newtonsoft.Json;
using WFPrimeTool.OrderFunctions;
using System.Threading;
using System.Diagnostics;

namespace WFPrimeTool
{
    /// <summary>
    /// Interaction logic for ChangeStatus.xaml
    /// </summary>
    /// 

    public partial class ChangeStatus : Window
    {
        public static WebSocket marketSocket;
        public static bool connecting = false;
        public static bool firstime = false;
        public static bool firstime2 = false;
        public static bool autoingame = false;
        public static bool closed = false;

        public ChangeStatus()
        {
            InitializeComponent();

        }



        public static async Task<bool> ConnectWF()
        {
            if(firstime == false)
            {
                marketSocket = new WebSocket("wss://warframe.market/socket?platform=pc");
                marketSocket.SetCookie(new WebSocketSharp.Net.Cookie("JWT", Requests.JWT));
                marketSocket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
                firstime = true;
            }

            if (marketSocket.IsAlive || marketSocket.ReadyState == WebSocketState.Connecting)
            {
                SendMessage("");
                return false;
            }
            marketSocket.OnMessage += (sender, e) =>
            {
                
                var message = JsonConvert.DeserializeObject<dynamic>(e.Data);
                if (message.GetValue("type").ToString().Contains("ONLINE_COUNT") && firstime2 == false)
                {
                    CheckIfWFExists();
                    firstime2 = true;
                }
                message.GetValue("payload").ToString();
            };
            marketSocket.OnMessage += (sender, e) =>
            {
                if (e.Data.Contains("@WS/chats/NEW_MESSAGE")) 
                {
                    //SendMessage("{\"type\":\"@WS/chats/NEW_MESSAGE\",\"payload\":\"invisible\"}");
                    
                }
            };
            marketSocket.OnMessage += (sender, e) =>
            {
                var message = JsonConvert.DeserializeObject<dynamic>(e.Data);
                if (e.Data.Contains("@WS/USER/SET_STATUS"));
                {
                    MarketHandler.logresponse = message.GetValue("payload").ToString();
                }
            };
            marketSocket.OnMessage += (sender, e) =>
            {
                if (e.Data.Contains("@WS/ERROR"))
                {
                    SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
                    marketSocket.Close(1006);
                }
            };
            try
            {
                marketSocket.SetCookie(new WebSocketSharp.Net.Cookie("JWT", Requests.JWT));
                marketSocket.ConnectAsync();
            }
            catch (Exception e)
            {
                
                return false;
            }
            return true;
        }
        public static void CheckIfWFExists()
        {
            if(MainWindow.laststat == true)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.ProcessName == "Warframe.x64" && proc.MainWindowTitle == "Warframe")
                    {
                        SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\""+MainWindow.ingamestatus+"\"}");
                        return;
                    }
                }
               
            }
            else
            {
                SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
            }
        }
        public static async void Logout()
        {
            if(marketSocket != null)
            {
                if (marketSocket.ReadyState == WebSocketState.Open || marketSocket.ReadyState == WebSocketState.Connecting)
                {
                    await SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
                    marketSocket.Close();
                }
            }

        }
        public static bool CheckStatus()
        {
            if(marketSocket.ReadyState == WebSocketState.Open || marketSocket.ReadyState == WebSocketState.Connecting)
            {
                if(marketSocket.ReadyState == WebSocketState.Connecting)
                {
                    connecting = true;
                    return false;
                }
                return true;
            }
                

          return false;
        }
        public static async Task<bool> SendMessage(string data)
        {
            if (marketSocket.ReadyState == WebSocketState.Closed || marketSocket.ReadyState != WebSocketState.Open)
            {
                marketSocket.Close();
                ConnectWF();
            }
            while (marketSocket.ReadyState == WebSocketState.Connecting)
            {
                Thread.Sleep(1);
            }
            if(marketSocket.ReadyState == WebSocketState.Open)
            {
                marketSocket.Send(data);
                return true;
            }
            return false;
        }

        private void Online_Click(object sender, RoutedEventArgs e)
        {
            MarketHandler.checkstat.Start();
            SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"online\"}");
        }

        private void Invisible_Click(object sender, RoutedEventArgs e)
        {
            MarketHandler.checkstat.Start();
            SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"invisible\"}");
        }
        private void Ingame_Click(object sender, RoutedEventArgs e)
        {
            MarketHandler.checkstat.Start();
            SendMessage("{\"type\":\"@WS/USER/SET_STATUS\",\"payload\":\"ingame\"}");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            closed = true;
            MainWindow.changeStatus = new ChangeStatus();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.Visibility  == Visibility.Visible)
            {
                Title = "Warframe Market Status - Welcome: " + MainWindow.ingame_name;
            }
        }
    }
}
