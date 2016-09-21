using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Models
{
    class MemberTree
    {
        public int? CompanyId { get; set; }
        public int? OfficerId { get; set; }
        public string PositionName { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int? TagId
        {
            get { return this.OfficerId; }
            set { this.OfficerId = value; }
        }
        public string LinkText
        {
            get
            {
                return "";
            }
        }
        public double? RootPercentage { get; set; }
        public string Text { get; set; }
        public string Color2Value { get; set; }
        public string ColorValue { get; set; }
        public string ShapeName { get; set; }
        public List<MemberTree> Children { get; set; }
        public MemberTree()
        {
            this.Children = new List<MemberTree>();
        }

        public string GetFullText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name = "+this.Name);
            sb.AppendLine("Position" + this.PositionName);
            foreach (var item in this.Children)
            {
                sb.AppendLine(item.GetFullText());
            }
            return sb.ToString();
        }
    }
}
