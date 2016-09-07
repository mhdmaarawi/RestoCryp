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
        public int? ModeId { get; set; }

        public int? BoardofDirectorsTypeId { get; set; }
        public string BoardofDirectorsType { get; set; }
        public int? PositionId { get; set; }
        public List<int?> BoardTypeIds { get; set; }
        public string BoardTypeNames { get; set; }

        public DateTime? RenewalDate { get; set; }
        public int? Renewal_ModeId { get; set; }
        public DateTime? CooperationDate { get; set; }
        public int? Cooperation_ModeId { get; set; }
        public DateTime? Term { get; set; }
        public int? Term_ModeId { get; set; }
        public DateTime? Ratification { get; set; }
        public int? Ratification_ModeId { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndDate_ModeId { get; set; }
        public bool? Illimited { get; set; }
        public OfficershipClass()
        {
            this.BoardTypeIds = new List<int?>();
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
