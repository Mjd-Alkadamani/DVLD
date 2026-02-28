
namespace MjdDVLD.Users_Forms
{
    partial class AddEditUser
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
            this.label10 = new System.Windows.Forms.Label();
            this.btnPersonID = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbRenterPassword = new System.Windows.Forms.TextBox();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(31, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(158, 27);
            this.label10.TabIndex = 37;
            this.label10.Text = "User Name  : ";
            // 
            // btnPersonID
            // 
            this.btnPersonID.BackColor = System.Drawing.Color.Bisque;
            this.btnPersonID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPersonID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPersonID.Location = new System.Drawing.Point(179, 96);
            this.btnPersonID.Name = "btnPersonID";
            this.btnPersonID.Size = new System.Drawing.Size(66, 34);
            this.btnPersonID.TabIndex = 35;
            this.btnPersonID.Text = "??";
            this.btnPersonID.UseVisualStyleBackColor = false;
            this.btnPersonID.Click += new System.EventHandler(this.btnPersonID_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(31, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 27);
            this.label2.TabIndex = 34;
            this.label2.Text = "Person ID : ";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblUserID.Location = new System.Drawing.Point(350, 33);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(36, 26);
            this.lblUserID.TabIndex = 33;
            this.lblUserID.Text = "??";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(238, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 27);
            this.label1.TabIndex = 32;
            this.label1.Text = "User ID : ";
            // 
            // txtbUserName
            // 
            this.txtbUserName.Location = new System.Drawing.Point(183, 148);
            this.txtbUserName.Name = "txtbUserName";
            this.txtbUserName.Size = new System.Drawing.Size(132, 20);
            this.txtbUserName.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(31, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 27);
            this.label3.TabIndex = 37;
            this.label3.Text = "Password  : ";
            // 
            // txtbPassword
            // 
            this.txtbPassword.Location = new System.Drawing.Point(168, 194);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(132, 20);
            this.txtbPassword.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(31, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 27);
            this.label4.TabIndex = 37;
            this.label4.Text = "Renter Password  : ";
            // 
            // txtbRenterPassword
            // 
            this.txtbRenterPassword.Location = new System.Drawing.Point(253, 240);
            this.txtbRenterPassword.Name = "txtbRenterPassword";
            this.txtbRenterPassword.Size = new System.Drawing.Size(132, 20);
            this.txtbRenterPassword.TabIndex = 39;
            this.txtbRenterPassword.TextChanged += new System.EventHandler(this.txtbRenterPassword_TextChanged);
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Font = new System.Drawing.Font("Tahoma", 20F);
            this.cbIsActive.Location = new System.Drawing.Point(152, 291);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(15, 14);
            this.cbIsActive.TabIndex = 41;
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(31, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 27);
            this.label7.TabIndex = 40;
            this.label7.Text = "Is Active : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(330, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(492, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 27);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // pbPhoto
            // 
            this.pbPhoto.Location = new System.Drawing.Point(443, 84);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(150, 150);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 38;
            this.pbPhoto.TabStop = false;
            // 
            // AddEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 357);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtbRenterPassword);
            this.Controls.Add(this.txtbPassword);
            this.Controls.Add(this.txtbUserName);
            this.Controls.Add(this.pbPhoto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnPersonID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.label1);
            this.Name = "AddEditUser";
            this.Text = "Add User";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPersonID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbRenterPassword;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}