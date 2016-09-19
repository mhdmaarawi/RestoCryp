using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    public class Individual
    {
        private static string GetByIdUri = GlobalClass.ServerApiAddress + "Individual/Get";
        private static string PostUpdateUri = GlobalClass.ServerApiAddress + "Individual/Update";
        public int Id { get; set; }
        public string Code { get; set; }
        public string TaxNumber { get; set; }
        public ListObject Civility { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string UsualName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string BirthPlace { get; set; }
        public ListObject BirthCountry { get; set; }
        public ListObject Citizenship { get; set; }
        public List<ListObject> Languages { get; set; }
        public ListObject Country { get; set; }
        public string Job { get; set; }
        public ListObject ContractType { get; set; }
        public DateTime? EffictiveDateCompanyOutput { get; set; }
        public string Email { get; set; }
        public ListObject BusinessUnit { get; set; }
        public ListObject Position { get; set; }
        public List<ListObject> Groups { get; set; }
        public System.Data.Linq.Binary Picture { get; set; }
        public System.Data.Linq.Binary Passport { get; set; }
        public System.Data.Linq.Binary Signature { get; set; }
        public Individual()
        {
            this.Languages = new List<ListObject>();
            this.Groups = new List<ListObject>();
        }
        public async static Task<Individual> GetById(int? Id)
        {
            string parameter = "Id=" + Id;
            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES,
                Individual.GetByIdUri, parameter);
            if (r.success && r.data != null)
            {
                var l = JsonConvert.DeserializeObject<List<Individual>>(r.data.ToString());
                return l.FirstOrDefault();
            }
            return null;
        }
        internal async Task<string> SendUpdate()
        {
            var parameter = "individual=" + JsonConvert.SerializeObject(this);
            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Post, ApiCommunicationClass.EncryptionType.AES,
                PostUpdateUri, parameter);
            return r.data.ToString();
        }

        internal Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>();
            foreach (var item in typeof(Individual).GetProperties())
            {
                string s = null;
                var v = item.GetValue(this, null);
                if (v != null)
                {
                    if (v.GetType() == typeof(List<ListObject>))
                        s = string.Join(",", (v as List<ListObject>).Select(x => x.Name));
                    else
                        s = v.ToString();
                }
                fields.Add(item.Name, s);
            }
            return fields;
        }
    }
}
