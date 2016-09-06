namespace RsaCrypto
{
    partial class Form1
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
            this.btnLoginGet = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLoginPost = new System.Windows.Forms.Button();
            this.btnGenerateAesKeys = new System.Windows.Forms.Button();
            this.txtAesKey = new System.Windows.Forms.TextBox();
            this.txtAesIv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoginGet
            // 
            this.btnLoginGet.Location = new System.Drawing.Point(23, 77);
            this.btnLoginGet.Name = "btnLoginGet";
            this.btnLoginGet.Size = new System.Drawing.Size(108, 23);
            this.btnLoginGet.TabIndex = 0;
            this.btnLoginGet.Text = "Log in - Get";
            this.btnLoginGet.UseVisualStyleBackColor = true;
            this.btnLoginGet.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(23, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(210, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(23, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(210, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // btnLoginPost
            // 
            this.btnLoginPost.Location = new System.Drawing.Point(138, 77);
            this.btnLoginPost.Name = "btnLoginPost";
            this.btnLoginPost.Size = new System.Drawing.Size(95, 23);
            this.btnLoginPost.TabIndex = 3;
            this.btnLoginPost.Text = "Login - Post";
            this.btnLoginPost.UseVisualStyleBackColor = true;
            this.btnLoginPost.Click += new System.EventHandler(this.btnLoginPost_Click);
            // 
            // btnGenerateAesKeys
            // 
            this.btnGenerateAesKeys.Location = new System.Drawing.Point(56, 19);
            this.btnGenerateAesKeys.Name = "btnGenerateAesKeys";
            this.btnGenerateAesKeys.Size = new System.Drawing.Size(210, 23);
            this.btnGenerateAesKeys.TabIndex = 4;
            this.btnGenerateAesKeys.Text = "Generate AES Keys";
            this.btnGenerateAesKeys.UseVisualStyleBackColor = true;
            this.btnGenerateAesKeys.Click += new System.EventHandler(this.btnGenerateAesKeys_Click);
            // 
            // txtAesKey
            // 
            this.txtAesKey.Location = new System.Drawing.Point(56, 49);
            this.txtAesKey.Name = "txtAesKey";
            this.txtAesKey.Size = new System.Drawing.Size(210, 20);
            this.txtAesKey.TabIndex = 5;
            // 
            // txtAesIv
            // 
            this.txtAesIv.Location = new System.Drawing.Point(56, 75);
            this.txtAesIv.Name = "txtAesIv";
            this.txtAesIv.Size = new System.Drawing.Size(210, 20);
            this.txtAesIv.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "IV";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAesIv);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAesKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGenerateAesKeys);
            this.groupBox1.Location = new System.Drawing.Point(258, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoginPost);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUsername);
            this.groupBox2.Controls.Add(this.btnLoginGet);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 114);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 342);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoginGet;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLoginPost;
        private System.Windows.Forms.Button btnGenerateAesKeys;
        private System.Windows.Forms.TextBox txtAesKey;
        private System.Windows.Forms.TextBox txtAesIv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

