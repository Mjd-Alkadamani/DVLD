
namespace MjdDVLD.Licenses_Forms
{
    partial class AddEditLicenses
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
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnApplicationID = new System.Windows.Forms.Button();
            this.btnDriverID = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbNotes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCreatedByUserID = new System.Windows.Forms.Label();
            this.cbIssueReason = new System.Windows.Forms.ComboBox();
            this.txtbIssueDate = new System.Windows.Forms.TextBox();
            this.txtbExpirationDate = new System.Windows.Forms.TextBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(300, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "License ID : ";
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblLicenseID.Location = new System.Drawing.Point(442, 42);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(36, 26);
            this.lblLicenseID.TabIndex = 19;
            this.lblLicenseID.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(27, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Application ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(27, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Driver ID : ";
            // 
            // btnApplicationID
            // 
            this.btnApplicationID.BackColor = System.Drawing.Color.Bisque;
            this.btnApplicationID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApplicationID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnApplicationID.Location = new System.Drawing.Point(214, 157);
            this.btnApplicationID.Name = "btnApplicationID";
            this.btnApplicationID.Size = new System.Drawing.Size(66, 34);
            this.btnApplicationID.TabIndex = 1;
            this.btnApplicationID.Text = "??";
            this.btnApplicationID.UseVisualStyleBackColor = false;
            this.btnApplicationID.Click += new System.EventHandler(this.btnApplicationID_Click);
            // 
            // btnDriverID
            // 
            this.btnDriverID.BackColor = System.Drawing.Color.Bisque;
            this.btnDriverID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDriverID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnDriverID.Location = new System.Drawing.Point(156, 105);
            this.btnDriverID.Name = "btnDriverID";
            this.btnDriverID.Size = new System.Drawing.Size(66, 34);
            this.btnDriverID.TabIndex = 2;
            this.btnDriverID.Text = "??";
            this.btnDriverID.UseVisualStyleBackColor = false;
            this.btnDriverID.Click += new System.EventHandler(this.btnDriverID_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(27, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Licence Class : ";
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.Enabled = false;
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Items.AddRange(new object[] {
            "Motorcycles",
            "LargeMotorcycles",
            "RegularCar",
            "PublicVehicles",
            "AgriculturalVehicles",
            "Buses",
            "HeavyVhicles"});
            this.cbLicenseClass.Location = new System.Drawing.Point(202, 214);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(126, 21);
            this.cbLicenseClass.TabIndex = 3;
            this.cbLicenseClass.SelectedIndexChanged += new System.EventHandler(this.cbLicenceClass_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(27, 259);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 27);
            this.label10.TabIndex = 22;
            this.label10.Text = "Issue Date : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(27, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 27);
            this.label5.TabIndex = 22;
            this.label5.Text = "Expiration Date : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(427, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 27);
            this.label6.TabIndex = 22;
            this.label6.Text = "Notes : ";
            // 
            // txtbNotes
            // 
            this.txtbNotes.Location = new System.Drawing.Point(514, 215);
            this.txtbNotes.Multiline = true;
            this.txtbNotes.Name = "txtbNotes";
            this.txtbNotes.Size = new System.Drawing.Size(267, 150);
            this.txtbNotes.TabIndex = 8;
            this.txtbNotes.TextChanged += new System.EventHandler(this.txbNotes_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(27, 412);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 27);
            this.label7.TabIndex = 0;
            this.label7.Text = "IsActive : ";
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Enabled = false;
            this.cbIsActive.Font = new System.Drawing.Font("Tahoma", 20F);
            this.cbIsActive.Location = new System.Drawing.Point(148, 422);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(15, 14);
            this.cbIsActive.TabIndex = 6;
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(427, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 27);
            this.label8.TabIndex = 0;
            this.label8.Text = "Issue Reason : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(27, 361);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 27);
            this.label9.TabIndex = 0;
            this.label9.Text = "Created By User ID : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(501, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(663, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCreatedByUserID
            // 
            this.lblCreatedByUserID.AutoSize = true;
            this.lblCreatedByUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblCreatedByUserID.Location = new System.Drawing.Point(267, 363);
            this.lblCreatedByUserID.Name = "lblCreatedByUserID";
            this.lblCreatedByUserID.Size = new System.Drawing.Size(36, 26);
            this.lblCreatedByUserID.TabIndex = 19;
            this.lblCreatedByUserID.Text = "??";
            // 
            // cbIssueReason
            // 
            this.cbIssueReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssueReason.FormattingEnabled = true;
            this.cbIssueReason.Items.AddRange(new object[] {
            "NewIssuance",
            "Renewal",
            "DamagedReplacement",
            "LostReplacement"});
            this.cbIssueReason.Location = new System.Drawing.Point(599, 162);
            this.cbIssueReason.Name = "cbIssueReason";
            this.cbIssueReason.Size = new System.Drawing.Size(173, 21);
            this.cbIssueReason.TabIndex = 7;
            // 
            // txtbIssueDate
            // 
            this.txtbIssueDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbIssueDate.Location = new System.Drawing.Point(176, 265);
            this.txtbIssueDate.Name = "txtbIssueDate";
            this.txtbIssueDate.ReadOnly = true;
            this.txtbIssueDate.Size = new System.Drawing.Size(127, 20);
            this.txtbIssueDate.TabIndex = 4;
            // 
            // txtbExpirationDate
            // 
            this.txtbExpirationDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbExpirationDate.Location = new System.Drawing.Point(231, 316);
            this.txtbExpirationDate.Name = "txtbExpirationDate";
            this.txtbExpirationDate.ReadOnly = true;
            this.txtbExpirationDate.Size = new System.Drawing.Size(127, 20);
            this.txtbExpirationDate.TabIndex = 5;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // pbPhoto
            // 
            this.pbPhoto.Location = new System.Drawing.Point(562, 6);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(150, 150);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 30;
            this.pbPhoto.TabStop = false;
            // 
            // AddEditLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbPhoto);
            this.Controls.Add(this.txtbExpirationDate);
            this.Controls.Add(this.txtbIssueDate);
            this.Controls.Add(this.cbIssueReason);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.txtbNotes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbLicenseClass);
            this.Controls.Add(this.btnDriverID);
            this.Controls.Add(this.btnApplicationID);
            this.Controls.Add(this.lblCreatedByUserID);
            this.Controls.Add(this.lblLicenseID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "AddEditLicenses";
            this.Text = "Add New License";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnApplicationID;
        private System.Windows.Forms.Button btnDriverID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbNotes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCreatedByUserID;
        private System.Windows.Forms.ComboBox cbIssueReason;
        private System.Windows.Forms.TextBox txtbIssueDate;
        private System.Windows.Forms.TextBox txtbExpirationDate;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.PictureBox pbPhoto;
    }
}