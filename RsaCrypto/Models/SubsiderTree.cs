using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaCrypto.Models
{
    class SubsiderTree
    {
        public int? CompanyId { get; set; }
        public int? TagId
        {
            get { return this.CompanyId; }
            set { this.CompanyId = value; }
        }
        public string CompanyName { get; set; }
        public string CEO { get; set; }
        public double? RootPercentage { get; set; }
        public double? ParentPercentage { get; set; }
        public string LinkText
        {
            get
            {
                if (this.ParentPercentage != null)
                    return this.ParentPercentage.ToString();
                return "";
            }
        }
        public string Text { get; set; }
        public string ColorValue { get; set; }
        public string Color2Value { get; set; }
        public string ShapeName { get; set; }
        public List<SubsiderTree> Children { get; set; }
        public SubsiderTree()
        {
            this.Children = new List<SubsiderTree>();
        }


        public string GetFullText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CompnyID = " + this.CompanyId);
            sb.AppendLine("Text = " + this.Text);
            sb.AppendLine("% = " + this.ParentPercentage);
            sb.AppendLine("Root% = " + this.RootPercentage);
            foreach (var item in this.Children)
            {
                sb.AppendLine(item.GetFullText());
            }
            return sb.ToString();
        }
    }
}
