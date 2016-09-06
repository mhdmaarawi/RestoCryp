using Newtonsoft.Json;
using RsaCrypto.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Models
{
    class PositionClass
    {
        static string PositionControllerUri = GlobalClass.ServerApiAddress + @"PositionHeld";

        public int Id { get; set; }
        public int IndividualId { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime? NominationDate { get; set; }
        public string Direction { get; set; }
        public string RepresentingOf { get; set; }
        public bool? IsRepresenter { get; set; }
        public PositionClass() { }

        internal async static Task<List<PositionClass>> GetList()
        {
            try
            {
                string uri = PositionControllerUri + "/GetMyPosition";

                string parameter = "";
                var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES,
                    uri, parameter);
                if (r.success &&  r.data != null)
                {
                    var v = JsonConvert.DeserializeObject<List<PositionClass>>(r.data.ToString());
                    return v;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return new List<PositionClass>();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.GetType().GetProperties())
            {
                sb.AppendLine(item.Name + "=" + item.GetValue(this, null).ToString());
            }
            return sb.ToString();
        }
    }
}
