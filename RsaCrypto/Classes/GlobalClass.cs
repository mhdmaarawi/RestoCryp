using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class GlobalClass
    {
        public static string[] servers = { @"http://localhost:51770/api/", @"http://mhdmaarawi-001-site2.dtempurl.com/api/", @"http://192.168.2.40/api/" };
        public static string ServerApiAddress = "";
        //public static string ServerApiAddress = @"http://localhost:51770/api/";
        //public static string ServerApiAddress = @"http://mhdmaarawi-001-site2.dtempurl.com/api/";
        // public static string ServerApiAddress = @"http://192.168.2.40/api/";
        public static Token UserToken { get; set; }
    }
}
