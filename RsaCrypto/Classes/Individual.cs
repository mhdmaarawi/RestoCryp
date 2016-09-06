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
        public string Civility { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string UsualName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCountry { get; set; }
        public int? CitizenshipId { get; set; }
        public string Citizenship { get; set; }
        public List<int?> LanguageIds { get; set; }
        public string LanguageNames { get; set; }
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public string Job { get; set; }
        public int? ContractTypeId { get; set; }
        public string ContractType { get; set; }
        public DateTime? EffictiveDateCompanyOutput { get; set; }
        public string Email { get; set; }
        public int? BusinessUnitId { get; set; }
        public string BusinessUnit { get; set; }
        public string Position { get; set; }
        public List<int?> GroupIds { get; set; }
        public string GroupNames { get; set; }
        public System.Data.Linq.Binary Picture { get; set; }
        public System.Data.Linq.Binary Passport { get; set; }
        public System.Data.Linq.Binary Signature { get; set; }
        public Individual()
        {
            this.LanguageIds = new List<int?>();
            this.GroupIds = new List<int?>();
        }
        public async static Task<Individual> GetById(int? Id)
        {
            string parameter = "Id=" + Id;
            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES,
                Individual.GetByIdUri, parameter);
            if (r.success && r.data != null)
            {
                var l =JsonConvert.DeserializeObject<List<Individual>>(r.data.ToString());
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
    }
}
