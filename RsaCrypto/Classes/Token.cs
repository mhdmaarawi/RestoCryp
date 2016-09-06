using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Classes
{
    class Token
    {
        public bool ValidToken { get; set; }
        public string Value { get; set; }
        public DateTime TokenActiveDateTime { get; set; }
        public DateTime TokenExpireDateTime { get; set; }
        public int? IndividualId { get; set; }
        public string ClientIP { get; set; }
        public string ErrorMessage { get; set; }
        private string _AesKeyBase64;
        private string _AesIVBase64;
        public string AesKeyBase64
        {
            get { return this._AesKeyBase64; }
            set
            {
                this._AesKeyBase64 = value;
                if (value != null)
                    this.AesKey = Convert.FromBase64String(value);
            }
        }
        public string AesIVBase64
        {
            get { return this._AesIVBase64; }
            set
            {
                this._AesIVBase64 = value;
                if (value != null)
                    this.AesIV = Convert.FromBase64String(value);
            }
        }
        internal byte[] AesKey;
        internal byte[] AesIV;

        public Token() { }
    }
}
