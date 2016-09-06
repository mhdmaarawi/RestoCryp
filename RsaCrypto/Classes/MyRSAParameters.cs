using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class MyRSAParameters
    {
        public byte[] Exponent { get; set; }
        public byte[] Modulus { get; set; }
        public MyRSAParameters()
        { }
        public MyRSAParameters(byte[] modulus, byte[] exponent)
        {
            this.Modulus = modulus;
            this.Exponent = exponent;
        }

    }
}
