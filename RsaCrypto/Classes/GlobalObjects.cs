using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class GlobalObjects
    {
        public static ApiCommunicationClass ApiCommunication { get; set; }
        public static SecurityOperation SecurityOp { get; set; }
        public static void CreateObjects()
        {
            GlobalObjects.ApiCommunication = new ApiCommunicationClass();
            GlobalObjects.SecurityOp = new SecurityOperation();
        }
    }
}
