using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class LoginClass
    {
        static string loginControllerUrl = GlobalClass.ServerApiAddress + @"Login";
        public async static Task<Result> Login(string username, string password)
        {
            string address = LoginClass.loginControllerUrl + "/Login";
            var pk = GlobalObjects.SecurityOp.GetPublicKey();
            var param = string.Format("username={0}&password={1}&publickeyM={2}&publickeyE={3}",
                username, password, Convert.ToBase64String(pk.Modulus), Convert.ToBase64String(pk.Exponent));

            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Post, ApiCommunicationClass.EncryptionType.RSA, address, param);
            if (r.success && r.data != null)
                r.data = JsonConvert.DeserializeObject<Token>(r.data.ToString());
            return r;
        }

        public async static Task<Result> Register(string username, string password, int IndividualId)
        {
            string address = LoginClass.loginControllerUrl + "/Register";
            var pk = GlobalObjects.SecurityOp.GetPublicKey();
            var param = string.Format("username={0}&password={1}&publickeyM={2}&publickeyE={3}&IndividualId={4}",
                username, password, Convert.ToBase64String(pk.Modulus), Convert.ToBase64String(pk.Exponent),IndividualId);

            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Post, ApiCommunicationClass.EncryptionType.RSA, address, param);
            if (r.success && r.data != null)
                r.data = JsonConvert.DeserializeObject<Token>(r.data.ToString());
            return r;
        }
    }
}
