
namespace MjdDVLD
{
    partial class LogInScreen
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.mtbUserNameOrID = new System.Windows.Forms.MaskedTextBox();
            this.mtbPassword = new System.Windows.Forms.MaskedTextBox();
            this.epLoginFailed1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.epLoginFailed2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epLoginFailed1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epLoginFailed2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Screen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(42, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "User Name or ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(42, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Password : ";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(176, 181);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(277, 181);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // mtbUserNameOrID
            // 
            this.mtbUserNameOrID.Font = new System.Drawing.Font("Tahoma", 12F);
            this.mtbUserNameOrID.Location = new System.Drawing.Point(232, 89);
            this.mtbUserNameOrID.Name = "mtbUserNameOrID";
            this.mtbUserNameOrID.Size = new System.Drawing.Size(176, 27);
            this.mtbUserNameOrID.TabIndex = 1;
            this.mtbUserNameOrID.Text = "9";
            // 
            // mtbPassword
            // 
            this.mtbPassword.Font = new System.Drawing.Font("Tahoma", 12F);
            this.mtbPassword.Location = new System.Drawing.Point(232, 128);
            this.mtbPassword.Name = "mtbPassword";
            this.mtbPassword.PasswordChar = '*';
            this.mtbPassword.Size = new System.Drawing.Size(176, 27);
            this.mtbPassword.TabIndex = 2;
            this.mtbPassword.Text = "12345";
            // 
            // epLoginFailed1
            // 
            this.epLoginFailed1.BlinkRate = 300;
            this.epLoginFailed1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.epLoginFailed1.ContainerControl = this;
            // 
            // Timer
            // 
            this.Timer.Interval = 10000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // epLoginFailed2
            // 
            this.epLoginFailed2.BlinkRate = 300;
            this.epLoginFailed2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.epLoginFailed2.ContainerControl = this;
            // 
            // LogInScreen
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 222);
            this.Controls.Add(this.mtbPassword);
            this.Controls.Add(this.mtbUserNameOrID);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LogInScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogInScreen";
            ((System.ComponentModel.ISupportInitialize)(this.epLoginFailed1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epLoginFailed2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.MaskedTextBox mtbUserNameOrID;
        private System.Windows.Forms.MaskedTextBox mtbPassword;
        private System.Windows.Forms.ErrorProvider epLoginFailed1;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.ErrorProvider epLoginFailed2;
    }
}