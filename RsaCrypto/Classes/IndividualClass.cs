using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RsaCrypto.Classes
{
    class IndividualClass
    {
        public Dictionary<string, object> Fields { get; set; }
        private static string GetByIdUri = GlobalClass.ServerApiAddress + "Individual/Get";

        public IndividualClass()
        { this.Fields = new Dictionary<string, object>(); }
        public IndividualClass(string jsonResponse)
        {
            this.Fields = ApiCommunicationClass.GetFieldsFromJson(jsonResponse);

        }
        public async static Task<IndividualClass> GetById(int? Id)
        {
            string parameter = "Id=" + Id;
            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES,
                IndividualClass.GetByIdUri, parameter);
            if (r.success && r.data != null)
            {
                return new IndividualClass(r.data.ToString());
            }
            return null;
        }

      
    }
}
