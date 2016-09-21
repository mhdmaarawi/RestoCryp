using Newtonsoft.Json;
using RsaCrypto.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Models
{
    class OfficershipClass
    {
        static string officershipControllerUri = GlobalClass.ServerApiAddress + @"Officership";

        public int Id { get; set; }
        public string Mandatory { get; set; }
        public int? IndividualId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? NomitatedDate { get; set; }
        public ListObject NomintationMode { get; set; }
        public ListObject BoardofDirectorsType { get; set; }
        public ListObject Position { get; set; }
        public List<ListObject> BoardTypes { get; set; }
        public DateTime? RenewalDate { get; set; }
        public ListObject RenewalMode { get; set; }
        public DateTime? CooperationDate { get; set; }
        public ListObject CooperationMode { get; set; }
        public DateTime? TermDate { get; set; }
        public ListObject TermMode { get; set; }
        public DateTime? RatificationDate { get; set; }
        public ListObject RatificationMode { get; set; }
        public DateTime? EndDate { get; set; }
        public ListObject EndMode { get; set; }
        public bool? Illimited { get; set; }
        public OfficershipClass()
        {
            this.BoardTypes = new List<ListObject>();
        }

        internal async static Task<List<OfficershipClass>> GetList()
        {
            try
            {
                string uri = officershipControllerUri + "/GetAll";

                string parameter = "";
                var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES,
                    uri, parameter);
                if (r.success && r.data != null)
                {
                    var v = JsonConvert.DeserializeObject<List<OfficershipClass>>(r.data.ToString());
                    return v;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return new List<OfficershipClass>();
        }
    }
}
