using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class ApiCommunicationClass
    {
        //HttpClient client;
        Dictionary<string, string> MimeTypes;

        public ApiCommunicationClass()
        {
            this.forbiddenChars = ":/?#[]@!$&'()*+,;=".ToCharArray().ToList();
            this.substitution = forbiddenChars.Select(x => new { c = x, val = "%" + string.Format("{0:X}", (int)x) }).ToDictionary(x => x.c, x => x.val);
            FillMimeType();
            System.Net.ServicePointManager.Expect100Continue = false;
            //this.client = new HttpClient();
        }

        private void FillMimeType()
        {
            this.MimeTypes = new Dictionary<string, string>();
            var txt = System.IO.File.ReadAllText("./MimeType.txt");
            var v = txt.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in v)
            {
                var r = item.Split(new char[] { ',' });
                this.MimeTypes.Add(r[0], r[1]);
            }
        }

        internal static Dictionary<string, object> GetFieldsFromJson(string jsonString)
        {
            //string s = "[{\"Id\":1,\"Code\":\"54OHSoIUo0\",\"TaxNumber\":\"JinfSBkZBt\",\"Civility\":\"FMGoRKwy6u\",\"FirstName\":\"kxgsKPvZF9\",\"LastName\":\"66n9x71E8I\",\"MaidenName\":\"4z64jTY0pH\",\"UsualName\":\"rJoufLCu5C\",\"Birthdate\":\"2000-05-10T00:00:00\",\"BirthPlace\":\"2W9Z6qkir0\",\"BirthCountry\":\"YZk0LJrr59\",\"CitizenshipId\":190,\"Citizenship\":\"Vanuatu\",\"Languages\":[71,36,70],\"LanguagesName\":\"Saraiki,Ilocano,Russian\",\"CountryId\":175,\"Country\":\"Timor-Leste\",\"Job\":\"tl5795CJfE\",\"ContractTypeId\":0,\"ContractType\":\"AqIWlQOVOf\",\"EffictiveDateCompanyOutput\":\"2007-11-25T00:00:00\",\"Email\":\"9FYzj04vlq\",\"BusinessUnitId\":3,\"BusinessUnit\":\"V1Qvi0YFiI\",\"Position\":\"eLalWjzs6d\",\"Groups\":[0,1],\"Picture\":null,\"Passport\":null,\"Signature\":null}]";
            var dic = new Dictionary<string, object>();
            jsonString = jsonString.TrimStart('[', '{');
            jsonString = jsonString.TrimEnd(']', '}');
            var p = jsonString.Split(new string[] { ",\"" }, StringSplitOptions.RemoveEmptyEntries);
            int intVal;
            DateTime dateVal;
            foreach (var item in p)
            {
                var pair = item.Split(':');
                var n = pair[0].Replace("\\", "").Replace("\"", "");
                var v = pair[1];

                if (v.Contains("[") && v.Contains("\""))
                {
                    v = v.Trim('[', ']');
                    dic.Add(n, v.Split(','));
                }
                else if (v.Contains("["))
                {
                    v = v.Trim('[', ']');
                    dic.Add(n, v.Split(',').Select(x => int.Parse(x)).ToArray());
                }
                else if (v.Contains("\""))
                    dic.Add(n, v.Replace("\"", ""));
                else if (int.TryParse(v, out intVal))
                    dic.Add(n, intVal);
                else if (DateTime.TryParse(v, out dateVal))
                    dic.Add(n, dateVal);
            }
            return dic;
        }

        public enum RequestType
        { Get = 0, Post = 1, }

        internal string GenerateUri(string tblName)
        {
            if (tblName == "Citizenship")
                tblName = "Country";
            return string.Format("{0}{1}/GetAll", GlobalClass.ServerApiAddress, tblName);
        }

        public enum EncryptionType
        { None = 0, RSA = 1, AES = 2, }
        public async Task<Result> SendRequest(RequestType reqType, EncryptionType encryptionType, string address, string parameter)
        {
            try
            {
                string encryptedParameter = "";
                Dictionary<string, string> uriParamDic = new Dictionary<string, string>();
                Dictionary<string, string> contentParamDic = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(parameter))
                {
                    //Commented by ghaith 2016-08-15 16:10:00
                    //var temp = ToApiParam(parameter);
                    switch (encryptionType)
                    {
                        case EncryptionType.None:
                            encryptedParameter = parameter;
                            break;
                        case EncryptionType.RSA:
                            encryptedParameter = GlobalObjects.SecurityOp.Encrept(parameter);
                            break;
                        case EncryptionType.AES:
                            encryptedParameter = GlobalObjects.SecurityOp.EncreptAes(parameter);
                            break;
                        default:
                            break;
                    }

                }
                if (encryptionType == EncryptionType.AES)
                    uriParamDic.Add("token", GlobalClass.UserToken.Value);
                //Commented by ghaith 2016-08-15 16:24:00
                //param = "param=" + encryptedParameter;
                
                string uriParam = "";
                string contentParam = "";
                string response = "";
                switch (reqType)
                {
                    case RequestType.Get:
                        if (!string.IsNullOrWhiteSpace(encryptedParameter))
                            uriParamDic.Add("param", encryptedParameter);
                        uriParam = string.Join("&", uriParamDic.Select(x => x.Key + "=" + ToApiParam(x.Value)));
                        response = await GetAsync(address, uriParam);
                        break;
                    case RequestType.Post:
                        if (!string.IsNullOrWhiteSpace(encryptedParameter))
                            contentParamDic.Add("param", encryptedParameter);
                        uriParam = string.Join("&", uriParamDic.Select(x => x.Key + "=" + x.Value));
                        contentParam = string.Join("&", contentParamDic.Select(x => x.Key + "=" + x.Value));
                        response = await PostAsync(address, uriParam, contentParam);
                        break;
                    default:
                        break;
                }
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

        internal async Task<bool> UploadFile(string uri, Dictionary<string, string> parameters, string fileKey, string localfilePath)
        {
            try
            {
                byte[] fileBytes = null;
                string fileContentType = "";
                string ext = "";
                string fileName = "";
                if (!string.IsNullOrWhiteSpace(localfilePath) && System.IO.File.Exists(localfilePath))
                {
                    fileBytes = System.IO.File.ReadAllBytes(localfilePath);
                    ext = System.IO.Path.GetExtension(localfilePath);
                    ext = ext.Replace(".", "").ToLower();
                    if (!this.MimeTypes.ContainsKey(ext))
                        throw new Exception("File type not supported");
                    fileContentType = this.MimeTypes[ext];
                    fileName = Path.GetFileName(localfilePath);
                }
                PostFileAsync(uri, parameters, fileKey, fileName, fileContentType, fileBytes);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Result> SendRequestAndDecrypt(RequestType reqType, EncryptionType encryptionType, string address, string parameter)
        {
            try
            {
                var r = await SendRequest(reqType, encryptionType, address, parameter);
                if (!r.success)
                    return r;
                if (!r.encrypted)
                    r.data = r.data.ToString();
                else
                {
                    switch (encryptionType)
                    {
                        case EncryptionType.None:
                            r.data = r.data.ToString();
                            break;
                        case EncryptionType.RSA:
                            r.data = GlobalObjects.SecurityOp.Decrypt(r.data.ToString());
                            break;
                        case EncryptionType.AES:
                            r.data = GlobalObjects.SecurityOp.DecryptAes(r.data.ToString());
                            break;
                        default:
                            break;
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                return new Result(false, ex.ToString());
            }
        }

        #region Post File
        private async void PostFileAsync(string uri, Dictionary<string, string> data, string fileKey, string fileName, string fileContentType, byte[] fileData)
        {
            string boundary = "-------------------------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest.Method = "POST";

            httpWebRequest.BeginGetRequestStream((result) =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                    using (Stream requestStream = request.EndGetRequestStream(result))
                    {
                        WriteMultipartForm(requestStream, boundary, data, fileKey, fileName, fileContentType, fileData);
                    }
                    request.BeginGetResponse(a =>
                    {
                        try
                        {
                            var response = request.EndGetResponse(a);
                            var responseStream = response.GetResponseStream();
                            using (var sr = new StreamReader(responseStream))
                            {
                                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                                {
                                    string responseString = streamReader.ReadToEnd();
                                    //responseString is depend upon your web service.
                                    if (responseString == "Success")
                                    {
                                        throw new Exception("Backup stored successfully on server.");
                                    }
                                    else
                                    {
                                        throw new Exception("Error occurred while uploading backup on server.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex1)
                        {

                        }
                    }, null);
                }
                catch (Exception ex2)
                {

                }
            }, httpWebRequest);
        }

        private void WriteMultipartForm(Stream s, string boundary, Dictionary<string, string> data, string fileKey, string fileName, string fileContentType, byte[] fileData)
        {
            string tempString = "";
            /// The first boundary
            byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary);
            /// the last boundary.
            byte[] trailer = Encoding.UTF8.GetBytes("--" + boundary + "--");
            /// the form data, properly formatted
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            /// the form-data file upload, properly formatted
            string fileheaderTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

            /// Added to track if we need a CRLF or not.
            bool bNeedsCRLF = false;

            #region Writing the token
            WriteToStream(s, boundarybytes);
            WriteToStream(s, "\r\n");
            tempString = string.Format(formdataTemplate, "token", GlobalClass.UserToken.Value);
            WriteToStream(s, tempString);
            bNeedsCRLF = true;
            #endregion

            #region Writing the data
            if (data != null)
            {
                foreach (string key in data.Keys)
                {
                    /// if we need to drop a CRLF, do that.
                    if (bNeedsCRLF)
                        WriteToStream(s, "\r\n");

                    /// Write the boundary.
                    WriteToStream(s, boundarybytes);
                    WriteToStream(s, "\r\n");

                    //Encrypting Content
                    var _key = GlobalObjects.SecurityOp.EncreptAes(key);
                    var _data = GlobalObjects.SecurityOp.EncreptAes(data[key]);
                    /// Write the key.
                    tempString = string.Format(formdataTemplate, _key, _data);
                    WriteToStream(s, tempString);
                    bNeedsCRLF = true;
                }
            }
            #endregion

            /// If we don't have keys, we don't need a crlf.
            if (bNeedsCRLF)
                WriteToStream(s, "\r\n");


            if (fileData != null && fileData.Length != 0 && !string.IsNullOrWhiteSpace(fileKey))
            {
                WriteToStream(s, boundarybytes);
                WriteToStream(s, "\r\n");

                //Encrypting Content
                var _fileKey = GlobalObjects.SecurityOp.EncreptAes(fileKey);
                var _fileName = GlobalObjects.SecurityOp.EncreptAes(fileName);
                var _fileData = GlobalObjects.SecurityOp.EncreptAes(fileData);

                tempString = string.Format(fileheaderTemplate, _fileKey, _fileName, fileContentType);
                WriteToStream(s, tempString);
                /// Write the file data to the stream.
                WriteToStream(s, _fileData);
                WriteToStream(s, "\r\n");
            }
            WriteToStream(s, trailer);
            WriteToStream(s, "\r\n");
            s.Flush();
        }
        /// <summary>
        /// Writes string to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, string txt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            s.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes byte array to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, byte[] bytes)
        {
            s.Write(bytes, 0, bytes.Length);
        }

        #endregion

        private async Task<string> PostAsync(string url, string uriParam,string contentParam)
        {
            if (!string.IsNullOrWhiteSpace(uriParam))
                uriParam = "?" + uriParam;
            var data = Encoding.Unicode.GetBytes(contentParam);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+ uriParam);
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
                using (var stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        private async Task<string> GetAsync(string url, string uriParam)
        {
            if (!string.IsNullOrWhiteSpace(uriParam))
                uriParam = "?" + uriParam;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url + uriParam));
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

        public List<char> forbiddenChars { get; private set; }
        public Dictionary<char, string> substitution { get; private set; }
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
            var res = text;
            foreach (var item in substitution.Keys)
            {
                res = res.Replace(item.ToString(), substitution[item]);
            }
            return res;
        }

    }
}
