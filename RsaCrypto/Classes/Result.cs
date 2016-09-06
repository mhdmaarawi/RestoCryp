using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto
{
    public class Result
    {
        public bool success { get; set; }
        public bool encrypted { get; set; }
        public object data { get; set; }

        public Result() { }
        public Result(bool success, object data)
        {
            this.success = success;
            this.data = data;
            this.encrypted = false;
        }
    }
}
