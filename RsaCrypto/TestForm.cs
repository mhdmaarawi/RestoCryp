using RsaCrypto.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RsaCrypto.Models;
using System.Reflection;
using System.Security.Cryptography;

namespace RsaCrypto
{
    public partial class TestForm : Form
    {
        private Individual ind;

        public TestForm()
        {
            InitializeComponent();
            this.cmboServer.Items.AddRange(GlobalClass.servers);
            this.cmboServer.Text = GlobalClass.ServerApiAddress;
            this.cmboServer.SelectedIndexChanged += CmboServer_SelectedIndexChanged;
            CmboServer_SelectedIndexChanged(null, null);
            this.txtFilePath.Text = @"D:\log.txt";
        }

        private void CmboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalClass.ServerApiAddress = this.cmboServer.Text;
            if (string.IsNullOrWhiteSpace(GlobalClass.ServerApiAddress))
                ControlsEnable(false);
        }

        private async void btnGetPublicKey_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GlobalClass.ServerApiAddress))
                return;
            var resultPublicKey = await Classes.SecurityClass.GetPublicKey();
            if (resultPublicKey.data is MyRSAParameters)
            {
                var rsaParam = (MyRSAParameters)resultPublicKey.data;
                Classes.GlobalObjects.SecurityOp.SetServerPublicKey(rsaParam.Modulus, rsaParam.Exponent);
                this.txtResult.Text += "Server PK:\r\n";
                this.txtResult.Text += Convert.ToBase64String(rsaParam.Modulus) + "\r\n";
                this.txtResult.Text += Convert.ToBase64String(rsaParam.Exponent) + "\r\n";
            }
            ControlsEnable(true);
        }

        private void ControlsEnable(bool v)
        {
            this.btnGetObjectList.Enabled = this.btnIndividualPage.Enabled = this.btnLogin.Enabled = this.btnUploadFile.Enabled = v;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var result = await Classes.LoginClass.Login(usernameEntry.Text, passwordEntry.Text);
            if (result.success)
            {
                Classes.Token t = result.data as Classes.Token;
                this.txtResult.Text += "TokenValid=" + t.ValidToken + "\r\n";
                if (t.ValidToken)
                {
                    Classes.GlobalClass.UserToken = t;
                    Classes.GlobalObjects.SecurityOp.SetAesParams(t.AesKey, t.AesIV);
                    this.txtResult.Text += "TokenValue=" + t.Value + "\r\n";
                    this.txtResult.Text += "AesKey=" + Convert.ToBase64String(t.AesKey) + "\r\n";
                    this.txtResult.Text += "AesIV=" + Convert.ToBase64String(t.AesIV) + "\r\n";
                    this.txtResult.Text += "ClientIP=" + t.ClientIP + "\r\n";
                }
                else
                    this.txtResult.Text += t.ErrorMessage + "\r\n";
            }
            else
                this.txtResult.Text += "LoginPage_ErrorServerConnection" + "\r\n";
        }

        private async void btnIndividualPage_Click(object sender, EventArgs e)
        {
            this.ind = await Classes.Individual.GetById(Classes.GlobalClass.UserToken.IndividualId);
            foreach (var item in ind.GetFields())
            {
                if (item.Key.Contains("Id"))
                    continue;
                this.txtResult.Text += item.Key + "=" + item.Value + "\r\n";
            }
        }

        private async void btnGetObjectList_Click(object sender, EventArgs e)
        {
            try
            {
                string tblName = this.txtObjectName.Text;
                var list = await ListObject.GetList(tblName);
                foreach (var item in list)
                {
                    this.txtResult.Text += item.ToString() + "\r\n";
                }
            }
            catch (Exception ex)
            {
                this.txtResult.Text += "---------------------------------------------------------\r\n";
                this.txtResult.Text += ex.ToString() + "\r\n";
                this.txtResult.Text += "---------------------------------------------------------\r\n";
            }
        }

        private async void btnUploadFile_Click(object sender, EventArgs e)
        {
            string localfilePath = this.txtFilePath.Text;
            var parameters = new Dictionary<string, string>();
            string uri = GlobalClass.ServerApiAddress + "File/postFile";
            if (!string.IsNullOrWhiteSpace(this.txtParamName.Text))
                parameters.Add(this.txtParamName.Text, this.txtParamValue.Text);
            await GlobalObjects.ApiCommunication.UploadFile(uri, parameters, "file", localfilePath);
        }

        private async void btnGetPositionHeld_Click(object sender, EventArgs e)
        {
            var posList = await PositionClass.GetList();
            foreach (var item in posList)
            {
                this.txtResult.Text += item.ToString() + "\r\n";
            }
            //this.txtResult.Text += res.data.ToString() + "\r\n";
        }

        private async void btnUpdateIndividual_Click(object sender, EventArgs e)
        {
            var ind = await Classes.Individual.GetById(Classes.GlobalClass.UserToken.IndividualId);
            foreach (var item in ind.GetType().GetProperties())
            {
                if (item.Name == "Id")
                    continue;
                if (item.PropertyType == typeof(string))
                    item.SetValue(ind, "koko");
                else if (item.PropertyType == typeof(int))
                    item.SetValue(ind, 9999);
            }
            var res = await ind.SendUpdate();
            this.txtResult.Text += res + "\r\n";
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            int IndividualId;
            if (!int.TryParse(this.txtIndividualId.Text, out IndividualId))
            {
                this.txtResult.Text += "Invalid Individual Id\r\n";
                return;
            }
            var result = await Classes.LoginClass.Register(usernameEntry.Text, passwordEntry.Text, IndividualId);
            if (result.success)
            {
                Classes.Token t = result.data as Classes.Token;
                this.txtResult.Text += "TokenValid=" + t.ValidToken + "\r\n";
                if (t.ValidToken)
                {
                    Classes.GlobalClass.UserToken = t;
                    Classes.GlobalObjects.SecurityOp.SetAesParams(t.AesKey, t.AesIV);
                    this.txtResult.Text += "TokenValue=" + t.Value + "\r\n";
                    this.txtResult.Text += "AesKey=" + Convert.ToBase64String(t.AesKey) + "\r\n";
                    this.txtResult.Text += "AesIV=" + Convert.ToBase64String(t.AesIV) + "\r\n";
                    this.txtResult.Text += "ClientIP=" + t.ClientIP + "\r\n";
                }
                else
                    this.txtResult.Text += t.ErrorMessage + "\r\n";
            }
            else
                this.txtResult.Text += "RegisterPage_ErrorServerConnection" + "\r\n";
        }

        private async void btnGetAllOfficership_Click(object sender, EventArgs e)
        {
            var posList = await OfficershipClass.GetList();
            foreach (var item in posList)
            {
                this.txtResult.Text += item.ToString() + "\r\n";
            }
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            if (this.ind != null)
            {
                var bytes = this.ind.Image;
                if (bytes != null && bytes.Length > 0)
                {
                    System.IO.MemoryStream mem = new System.IO.MemoryStream(bytes);
                    var img = System.Drawing.Image.FromStream(mem);
                    Form f = new Form();
                    f.Controls.Add(new PictureBox() { Dock = DockStyle.Fill, Image = img });
                    f.ShowDialog();
                }
            }
        }

        private async void btnGetMemberTree_Click(object sender, EventArgs e)
        {
            string TreeMemberGetByIdUri = GlobalClass.ServerApiAddress + "MemberTree/GetTree";

            var parameter = "companyId=5";// + companyId;
            var r = await GlobalObjects.ApiCommunication.SendRequestAndDecrypt(ApiCommunicationClass.RequestType.Get, ApiCommunicationClass.EncryptionType.AES, TreeMemberGetByIdUri, parameter);
            if (r.success)
            {
                var l = JsonConvert.DeserializeObject<MemberTree>(r.data.ToString());
                this.txtResult.Text += l.GetFullText() ;
            }

        }
    }
}
