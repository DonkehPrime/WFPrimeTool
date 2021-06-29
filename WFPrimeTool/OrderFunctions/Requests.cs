using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Authentication;
using Microsoft.Win32;

namespace WFPrimeTool.OrderFunctions
{
    public class Requests
    {
        
        public static string JWT = "";
        public static string Response;
        public static string responsebody;
        public static bool savejwt = false;
        public static async Task<string> Request(string url, dynamic method, dynamic content = null, dynamic headers = null, string contype = "application/json", dynamic useragent = null)
        {
            var client = new HttpClient();
            dynamic Data = content;
            dynamic Result;
            if(method.GetType() == "".GetType())
            {
                if (method == "POST")
                {
                    method = HttpMethod.Post;
                }
                else if (method == "GET")
                {
                    method = HttpMethod.Get;
                }
                else if (method == "PUT")
                {
                    method = HttpMethod.Put;
                }
            }
            
            var httpWebRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = method,
            };
            if(method == HttpMethod.Post || method == HttpMethod.Put)
            {
                httpWebRequest.Content = new StringContent(Data, Encoding.UTF8, contype);
            }
            if (method == HttpMethod.Get)
            {
                //httpWebRequest.Content = new StringContent(Data, Encoding.UTF8, contype);
            }
            if (headers  !=  null) { for (int i = 0; i < headers.Length / 2;i++)
                { 
                    
                    if (!client.DefaultRequestHeaders.Contains(headers[i]))
                    {
                        client.DefaultRequestHeaders.Add(headers[i], headers[i + 1]);
                    }
                    i++;
                } }//httpWebRequest.Headers.Add(headers[i], headers[i + 1]); 
            HttpResponseMessage response = new HttpResponseMessage();
            if(method == HttpMethod.Post)
            {
                response = await client.SendAsync(httpWebRequest);
                responsebody = await response.Content.ReadAsStringAsync();
            }
            if (method == HttpMethod.Get)
            {
                response = await client.GetAsync(url);
                responsebody = await response.Content.ReadAsStringAsync();
            }
            if (method == HttpMethod.Put)
            {
                response = await client.PutAsync(url, httpWebRequest.Content);
                responsebody = await response.Content.ReadAsStringAsync();
            }


            if (response.IsSuccessStatusCode && method == HttpMethod.Post)
            {
                SetJWT(response.Headers);
                Response = response.StatusCode.ToString();
            }
            else if(method == HttpMethod.Get)
            {
                Response = response.StatusCode.ToString();
            }
            httpWebRequest.Dispose();


            return responsebody;

            
        }
        public static void SetJWT(HttpResponseHeaders headers)
        {
            foreach (var item in headers)
            {
                if (!item.Key.Contains("Authorization")) continue;
                var temp = item.Value.First();
                JWT = temp.Substring(4);
                if (savejwt == true)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WFPrimeTool", true);
                    if (key == null)
                    {

                        RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WFPrimeTool");

                        key2.SetValue("JWT", UTF8Encoding.ASCII.GetBytes(temp.Substring(4)));
                        key2.Close();
                    }
                    else if(key.GetValue("JWT") == null)
                    {
                                key.SetValue("JWT", UTF8Encoding.ASCII.GetBytes(temp.Substring(4)));
                                key.Close();
                    }
                    else if (key.GetValue("JWT") != UTF8Encoding.ASCII.GetString((dynamic)JWT))
                    {
                        key.SetValue("JWT", UTF8Encoding.ASCII.GetBytes(temp.Substring(4)));
                        key.Close();
                    }
                }
                return;
            }
        }
    }
}
