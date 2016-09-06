using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using RsaCrypto.Classes;

namespace RsaCrypto
{
    public partial class Form1 : Form
    {
        private Cryptography clientRsa;

        public Form1()
        {
            InitializeComponent();
        }

        //string serverAddress = @"http://192.168.2.40/api/";
        string serverAddress = @"http://localhost:51770/api/";
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Cryptography.InitilizeCryptography();
            string securityPageUrl = serverAddress + @"Security";
            string GetPublicKeyUrl = securityPageUrl;
            var r = await SendGetRequest(GetPublicKeyUrl);
            if (!r.success)
                return;
            var serverRsa = JsonConvert.DeserializeObject<RSAParameters>(r.data.ToString());
            this.clientRsa.SetServerPublicKey(serverRsa);

            var loginPageUrl = serverAddress + @"login";
            string accountInfo = "username=admin&password=admin";
            var encrypted = this.clientRsa.Encrypt(accountInfo);
            var vParam = ToApiParam(encrypted);
            var tst = FromApiParam(vParam);
            string loginUrl = loginPageUrl + @"?param=" + vParam;
            var r2 = await SendGetRequest(loginUrl);
            var tokData = r2.data.ToString();
            if (r2.success)
            {
                tokData = this.clientRsa.Decrypt(tokData);
                var tok = JsonConvert.DeserializeObject<Token>(tokData);
            }
        }

        List<char> forbiddenChars = ":/?#[]@!$&'()*+,;=".ToCharArray().ToList();
        Dictionary<char, string> substitution;
        private string FromApiParam(string text)
        {
            var res = text;
            foreach (var item in substitution.Keys)
            {
                res = res.Replace(substitution[item], item.ToString());
            }
            return res;
        }
        private string ToApiParam(string text)
        {
            substitution = forbiddenChars.Select(x => new { c = x, val = "%" + string.Format("{0:X}", (int)x) }).ToDictionary(x => x.c, x => x.val);
            var res = text;
            foreach (var item in substitution.Keys)
            {
                res = res.Replace(item.ToString(), substitution[item]);
            }
            return res;
        }

        public async Task<Result> SendGetRequest(string request)
        {
            try
            {
                string response = await GetAsync(request);
                response = response.TrimStart('\"');
                response = response.TrimEnd('\"');
                response = response.Replace("\\", "");
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var r = JsonConvert.DeserializeObject<Result>(response, settings);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(false, ex.ToString());
            }
        }

        private async Task<string> GetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        private async Task<string> PostAsync(string url, string dataT)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            var dataBytes = Convert.FromBase64String(dataT);
            request.ContentLength = dataBytes.Length;
            using (var ds = request.GetRequestStream())
            {
                ds.Write(dataBytes, 0, dataBytes.Length);
                ds.Close();
            }

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        public async Task<Result> SendPostRequest(string url, string param)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(param);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                //request.ContentLength = data.Length;

                using (var stream = await request.GetRequestStreamAsync())
                {
                    stream.Write(data, 0, data.Length);
                }

                // Send the request to the server and wait for the response:
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            var re = sr.ReadToEnd();
                            re = re.TrimStart('\"');
                            re = re.TrimEnd('\"');
                            re = re.Replace("\\", "");
                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
                            var r = JsonConvert.DeserializeObject<Result>(re, settings);
                            return r;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result(false, ex.ToString());
            }
        }
        private async void btnLoginPost_Click(object sender, EventArgs e)
        {
            string securityPageUrl = serverAddress + @"Login/Login";
            string GetPublicKeyUrl = securityPageUrl;
            string param = "param=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";
            var r = await SendPostRequest(GetPublicKeyUrl, param);
            if (!r.success)
                return;
        }

        private void btnGenerateAesKeys_Click(object sender, EventArgs e)
        {
            using (AesManaged a = new AesManaged())
            {
                a.GenerateIV();
                a.GenerateKey();
                this.txtAesKey.Text = Convert.ToBase64String(a.Key);
                this.txtAesIv.Text = Convert.ToBase64String(a.IV);
            }
        }
    }
}
