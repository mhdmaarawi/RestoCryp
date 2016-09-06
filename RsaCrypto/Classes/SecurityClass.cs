using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class SecurityClass
    {
        static string SecurtyControllerUrl = GlobalClass.ServerApiAddress + @"Security";
        public async static Task<Result> GetPublicKey()
        {
            var r = await GlobalObjects.ApiCommunication.SendRequest(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.None, SecurityClass.SecurtyControllerUrl, "");
            if (r.data != null)
                r.data = JsonConvert.DeserializeObject<MyRSAParameters>(r.data.ToString());
            return r;
        }

        internal async static Task<Result> SendPublicKey()
        {
            var pk = GlobalObjects.SecurityOp.GetPublicKey();
            var param = string.Format("publickeyM={0}&publickeyE={1}",
                Convert.ToBase64String(pk.Modulus), Convert.ToBase64String(pk.Exponent));

            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Post, ApiCommunicationClass.EncryptionType.RSA, SecurityClass.SecurtyControllerUrl, param);
            if (r.data != null)
                r.data = JsonConvert.DeserializeObject<MyRSAParameters>(r.data.ToString());
            return r;
        }
    }
}
