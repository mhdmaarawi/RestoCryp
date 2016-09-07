namespace RsaCrypto
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetPublicKey = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.usernameEntry = new System.Windows.Forms.TextBox();
            this.passwordEntry = new System.Windows.Forms.TextBox();
            this.btnIndividualPage = new System.Windows.Forms.Button();
            this.btnGetObjectList = new System.Windows.Forms.Button();
            this.cmboServer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObjectName = new System.Windows.Forms.TextBox();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtParamName = new System.Windows.Forms.TextBox();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetPositionHeld = new System.Windows.Forms.Button();
            this.btnUpdateIndividual = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.txtIndividualId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetAllOfficership = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetPublicKey
            // 
            this.btnGetPublicKey.Location = new System.Drawing.Point(12, 34);
            this.btnGetPublicKey.Name = "btnGetPublicKey";
            this.btnGetPublicKey.Size = new System.Drawing.Size(299, 23);
            this.btnGetPublicKey.TabIndex = 16;
            this.btnGetPublicKey.Text = "Get Pk";
            this.btnGetPublicKey.UseVisualStyleBackColor = true;
            this.btnGetPublicKey.Click += new System.EventHandler(this.btnGetPublicKey_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(317, 12);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(421, 348);
            this.txtResult.TabIndex = 15;
            this.txtResult.Text = "";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 61);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 23);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // usernameEntry
            // 
            this.usernameEntry.Location = new System.Drawing.Point(106, 64);
            this.usernameEntry.Name = "usernameEntry";
            this.usernameEntry.Size = new System.Drawing.Size(100, 20);
            this.usernameEntry.TabIndex = 17;
            this.usernameEntry.Text = "admin";
            // 
            // passwordEntry
            // 
            this.passwordEntry.Location = new System.Drawing.Point(213, 64);
            this.passwordEntry.Name = "passwordEntry";
            this.passwordEntry.Size = new System.Drawing.Size(98, 20);
            this.passwordEntry.TabIndex = 18;
            this.passwordEntry.Text = "admin";
            // 
            // btnIndividualPage
            // 
            this.btnIndividualPage.Location = new System.Drawing.Point(11, 116);
            this.btnIndividualPage.Name = "btnIndividualPage";
            this.btnIndividualPage.Size = new System.Drawing.Size(149, 23);
            this.btnIndividualPage.TabIndex = 19;
            this.btnIndividualPage.Text = "Individual Page";
            this.btnIndividualPage.UseVisualStyleBackColor = true;
            this.btnIndividualPage.Click += new System.EventHandler(this.btnIndividualPage_Click);
            // 
            // btnGetObjectList
            // 
            this.btnGetObjectList.Location = new System.Drawing.Point(12, 146);
            this.btnGetObjectList.Name = "btnGetObjectList";
            this.btnGetObjectList.Size = new System.Drawing.Size(86, 23);
            this.btnGetObjectList.TabIndex = 20;
            this.btnGetObjectList.Text = "Get Object List";
            this.btnGetObjectList.UseVisualStyleBackColor = true;
            this.btnGetObjectList.Click += new System.EventHandler(this.btnGetObjectList_Click);
            // 
            // cmboServer
            // 
            this.cmboServer.FormattingEnabled = true;
            this.cmboServer.Location = new System.Drawing.Point(57, 7);
            this.cmboServer.Name = "cmboServer";
            this.cmboServer.Size = new System.Drawing.Size(254, 21);
            this.cmboServer.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Server";
            // 
            // txtObjectName
            // 
            this.txtObjectName.Location = new System.Drawing.Point(104, 148);
            this.txtObjectName.Name = "txtObjectName";
            this.txtObjectName.Size = new System.Drawing.Size(205, 20);
            this.txtObjectName.TabIndex = 23;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Location = new System.Drawing.Point(11, 176);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(87, 23);
            this.btnUploadFile.TabIndex = 24;
            this.btnUploadFile.Text = "UploadFile";
            this.btnUploadFile.UseVisualStyleBackColor = true;
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(104, 178);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(205, 20);
            this.txtFilePath.TabIndex = 25;
            // 
            // txtParamName
            // 
            this.txtParamName.Location = new System.Drawing.Point(118, 205);
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.Size = new System.Drawing.Size(88, 20);
            this.txtParamName.TabIndex = 26;
            // 
            // txtParamValue
            // 
            this.txtParamValue.Location = new System.Drawing.Point(212, 205);
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(97, 20);
            this.txtParamValue.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Param Name && Value";
            // 
            // btnGetPositionHeld
            // 
            this.btnGetPositionHeld.Location = new System.Drawing.Point(167, 231);
            this.btnGetPositionHeld.Name = "btnGetPositionHeld";
            this.btnGetPositionHeld.Size = new System.Drawing.Size(142, 23);
            this.btnGetPositionHeld.TabIndex = 29;
            this.btnGetPositionHeld.Text = "Get My Position Held";
            this.btnGetPositionHeld.UseVisualStyleBackColor = true;
            this.btnGetPositionHeld.Click += new System.EventHandler(this.btnGetPositionHeld_Click);
            // 
            // btnUpdateIndividual
            // 
            this.btnUpdateIndividual.Location = new System.Drawing.Point(167, 116);
            this.btnUpdateIndividual.Name = "btnUpdateIndividual";
            this.btnUpdateIndividual.Size = new System.Drawing.Size(142, 23);
            this.btnUpdateIndividual.TabIndex = 30;
            this.btnUpdateIndividual.Text = "Update Individual";
            this.btnUpdateIndividual.UseVisualStyleBackColor = true;
            this.btnUpdateIndividual.Click += new System.EventHandler(this.btnUpdateIndividual_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(11, 87);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(87, 23);
            this.btnRegister.TabIndex = 14;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtIndividualId
            // 
            this.txtIndividualId.Location = new System.Drawing.Point(148, 90);
            this.txtIndividualId.Name = "txtIndividualId";
            this.txtIndividualId.Size = new System.Drawing.Size(163, 20);
            this.txtIndividualId.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Ind. Id";
            // 
            // btnGetAllOfficership
            // 
            this.btnGetAllOfficership.Location = new System.Drawing.Point(11, 231);
            this.btnGetAllOfficership.Name = "btnGetAllOfficership";
            this.btnGetAllOfficership.Size = new System.Drawing.Size(149, 23);
            this.btnGetAllOfficership.TabIndex = 33;
            this.btnGetAllOfficership.Text = "Get All Officership";
            this.btnGetAllOfficership.UseVisualStyleBackColor = true;
            this.btnGetAllOfficership.Click += new System.EventHandler(this.btnGetAllOfficership_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 372);
            this.Controls.Add(this.btnGetAllOfficership);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUpdateIndividual);
            this.Controls.Add(this.btnGetPositionHeld);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtParamValue);
            this.Controls.Add(this.txtParamName);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.txtObjectName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboServer);
            this.Controls.Add(this.btnGetObjectList);
            this.Controls.Add(this.btnIndividualPage);
            this.Controls.Add(this.passwordEntry);
            this.Controls.Add(this.txtIndividualId);
            this.Controls.Add(this.usernameEntry);
            this.Controls.Add(this.btnGetPublicKey);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnLogin);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetPublicKey;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox usernameEntry;
        private System.Windows.Forms.TextBox passwordEntry;
        private System.Windows.Forms.Button btnIndividualPage;
        private System.Windows.Forms.Button btnGetObjectList;
        private System.Windows.Forms.ComboBox cmboServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtObjectName;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtParamName;
        private System.Windows.Forms.TextBox txtParamValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetPositionHeld;
        private System.Windows.Forms.Button btnUpdateIndividual;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox txtIndividualId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetAllOfficership;
    }
}