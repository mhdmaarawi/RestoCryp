using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class ListObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ListObject()
        { }
        public ListObject(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public override string ToString()
        {
            return this.Name;
        }
        internal async static Task<List<ListObject>> GetList(string tblName)
        {
            string uri = GlobalObjects.ApiCommunication.GenerateUri(tblName);
            var res = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES, uri, "");
            List<ListObject> list = null;
            if (res.success)
            {
                list = JsonConvert.DeserializeObject<List<ListObject>>(res.data.ToString());
            }
            return list;
        }
    }
}
