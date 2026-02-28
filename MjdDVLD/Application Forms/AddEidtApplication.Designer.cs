
namespace MjdDVLD.Application_Forms
{
    partial class AddEidtApplication
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
            this.txtbLastStatusDate = new System.Windows.Forms.TextBox();
            this.txtbApplicationDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbApplicationStatus = new System.Windows.Forms.ComboBox();
            this.lblCreatedByUserID = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbApplicationType = new System.Windows.Forms.ComboBox();
            this.btnPersonID = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbPaidFees = new System.Windows.Forms.TextBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbLicenseClassInterntional = new System.Windows.Forms.ComboBox();
            this.List6_2 = new System.Windows.Forms.Label();
            this.List6_3 = new System.Windows.Forms.Label();
            this.List6_4 = new System.Windows.Forms.Label();
            this.txtbIssuedDate = new System.Windows.Forms.TextBox();
            this.txtbExpirationDate = new System.Windows.Forms.TextBox();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.List6_5 = new System.Windows.Forms.Label();
            this.lblInternationalLicenseID = new System.Windows.Forms.Label();
            this.List6_1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.List0_2 = new System.Windows.Forms.Label();
            this.cbLicenseClassLocal = new System.Windows.Forms.ComboBox();
            this.List0_1 = new System.Windows.Forms.Label();
            this.lblLocalDrivingLicenseApplicationID = new System.Windows.Forms.Label();
            this.btnEyeTestID = new System.Windows.Forms.Button();
            this.btnTheoreticalTestID = new System.Windows.Forms.Button();
            this.btnDrivingTestID = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gbInterNationalLicense = new System.Windows.Forms.GroupBox();
            this.gbLocalDrivingLicense = new System.Windows.Forms.GroupBox();
            this.SecoundErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.gbInterNationalLicense.SuspendLayout();
            this.gbLocalDrivingLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecoundErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbLastStatusDate
            // 
            this.txtbLastStatusDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbLastStatusDate.Location = new System.Drawing.Point(236, 349);
            this.txtbLastStatusDate.Name = "txtbLastStatusDate";
            this.txtbLastStatusDate.ReadOnly = true;
            this.txtbLastStatusDate.Size = new System.Drawing.Size(127, 20);
            this.txtbLastStatusDate.TabIndex = 30;
            // 
            // txtbApplicationDate
            // 
            this.txtbApplicationDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbApplicationDate.Location = new System.Drawing.Point(242, 300);
            this.txtbApplicationDate.Name = "txtbApplicationDate";
            this.txtbApplicationDate.ReadOnly = true;
            this.txtbApplicationDate.Size = new System.Drawing.Size(127, 20);
            this.txtbApplicationDate.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(32, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 27);
            this.label5.TabIndex = 33;
            this.label5.Text = "Last Status Date : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(32, 294);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 27);
            this.label10.TabIndex = 34;
            this.label10.Text = "Application Date : ";
            // 
            // cbApplicationStatus
            // 
            this.cbApplicationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApplicationStatus.FormattingEnabled = true;
            this.cbApplicationStatus.Items.AddRange(new object[] {
            "New ( On Going )",
            "Canceled",
            "Completed"});
            this.cbApplicationStatus.Location = new System.Drawing.Point(260, 200);
            this.cbApplicationStatus.Name = "cbApplicationStatus";
            this.cbApplicationStatus.Size = new System.Drawing.Size(126, 21);
            this.cbApplicationStatus.TabIndex = 28;
            // 
            // lblCreatedByUserID
            // 
            this.lblCreatedByUserID.AutoSize = true;
            this.lblCreatedByUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblCreatedByUserID.Location = new System.Drawing.Point(267, 396);
            this.lblCreatedByUserID.Name = "lblCreatedByUserID";
            this.lblCreatedByUserID.Size = new System.Drawing.Size(36, 26);
            this.lblCreatedByUserID.TabIndex = 31;
            this.lblCreatedByUserID.Text = "??";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblApplicationID.Location = new System.Drawing.Point(449, 30);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(36, 26);
            this.lblApplicationID.TabIndex = 32;
            this.lblApplicationID.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(32, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 27);
            this.label4.TabIndex = 23;
            this.label4.Text = "Application Status : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(32, 394);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 27);
            this.label9.TabIndex = 24;
            this.label9.Text = "Created By User ID : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(261, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 27);
            this.label1.TabIndex = 26;
            this.label1.Text = "Application ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(32, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 27);
            this.label3.TabIndex = 23;
            this.label3.Text = "Application Type : ";
            // 
            // cbApplicationType
            // 
            this.cbApplicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApplicationType.FormattingEnabled = true;
            this.cbApplicationType.Items.AddRange(new object[] {
            "License Issuance",
            "Retake Test",
            "Renew Driving License",
            "Missing Replacement",
            "Damaged Replacement",
            "Release License",
            "Issuing International License"});
            this.cbApplicationType.Location = new System.Drawing.Point(242, 149);
            this.cbApplicationType.Name = "cbApplicationType";
            this.cbApplicationType.Size = new System.Drawing.Size(130, 21);
            this.cbApplicationType.TabIndex = 28;
            this.cbApplicationType.SelectedIndexChanged += new System.EventHandler(this.cbApplicationType_SelectedIndexChanged);
            // 
            // btnPersonID
            // 
            this.btnPersonID.BackColor = System.Drawing.Color.Bisque;
            this.btnPersonID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPersonID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPersonID.Location = new System.Drawing.Point(186, 93);
            this.btnPersonID.Name = "btnPersonID";
            this.btnPersonID.Size = new System.Drawing.Size(66, 34);
            this.btnPersonID.TabIndex = 36;
            this.btnPersonID.Tag = "??";
            this.btnPersonID.Text = "??";
            this.btnPersonID.UseVisualStyleBackColor = false;
            this.btnPersonID.Click += new System.EventHandler(this.btnPersonID_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 27);
            this.label2.TabIndex = 35;
            this.label2.Text = "Person ID : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(417, 397);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(579, 397);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 27);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(32, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 27);
            this.label6.TabIndex = 34;
            this.label6.Text = "Paid Fees :";
            // 
            // txtbPaidFees
            // 
            this.txtbPaidFees.BackColor = System.Drawing.SystemColors.Window;
            this.txtbPaidFees.Location = new System.Drawing.Point(164, 250);
            this.txtbPaidFees.Name = "txtbPaidFees";
            this.txtbPaidFees.Size = new System.Drawing.Size(127, 20);
            this.txtbPaidFees.TabIndex = 29;
            this.txtbPaidFees.TextChanged += new System.EventHandler(this.txtbPaidFees_TextChanged);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // cbLicenseClassInterntional
            // 
            this.cbLicenseClassInterntional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClassInterntional.FormattingEnabled = true;
            this.cbLicenseClassInterntional.Items.AddRange(new object[] {
            "Motorcycles",
            "LargeMotorcycles",
            "RegularCar",
            "PublicVehicles",
            "AgriculturalVehicles",
            "Buses",
            "HeavyVhicles"});
            this.cbLicenseClassInterntional.Location = new System.Drawing.Point(182, 79);
            this.cbLicenseClassInterntional.Name = "cbLicenseClassInterntional";
            this.cbLicenseClassInterntional.Size = new System.Drawing.Size(133, 21);
            this.cbLicenseClassInterntional.TabIndex = 28;
            this.cbLicenseClassInterntional.SelectedIndexChanged += new System.EventHandler(this.cbLicenseClassInterntional_SelectedIndexChanged);
            // 
            // List6_2
            // 
            this.List6_2.AutoSize = true;
            this.List6_2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List6_2.Location = new System.Drawing.Point(9, 73);
            this.List6_2.Name = "List6_2";
            this.List6_2.Size = new System.Drawing.Size(172, 27);
            this.List6_2.TabIndex = 26;
            this.List6_2.Text = "License Class :";
            // 
            // List6_3
            // 
            this.List6_3.AutoSize = true;
            this.List6_3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List6_3.Location = new System.Drawing.Point(9, 123);
            this.List6_3.Name = "List6_3";
            this.List6_3.Size = new System.Drawing.Size(158, 27);
            this.List6_3.TabIndex = 23;
            this.List6_3.Text = "Issued Date :";
            // 
            // List6_4
            // 
            this.List6_4.AutoSize = true;
            this.List6_4.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List6_4.Location = new System.Drawing.Point(9, 173);
            this.List6_4.Name = "List6_4";
            this.List6_4.Size = new System.Drawing.Size(199, 27);
            this.List6_4.TabIndex = 23;
            this.List6_4.Text = "Expiration Date :";
            // 
            // txtbIssuedDate
            // 
            this.txtbIssuedDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbIssuedDate.Location = new System.Drawing.Point(167, 129);
            this.txtbIssuedDate.Name = "txtbIssuedDate";
            this.txtbIssuedDate.ReadOnly = true;
            this.txtbIssuedDate.Size = new System.Drawing.Size(127, 20);
            this.txtbIssuedDate.TabIndex = 29;
            // 
            // txtbExpirationDate
            // 
            this.txtbExpirationDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbExpirationDate.Location = new System.Drawing.Point(207, 179);
            this.txtbExpirationDate.Name = "txtbExpirationDate";
            this.txtbExpirationDate.ReadOnly = true;
            this.txtbExpirationDate.Size = new System.Drawing.Size(127, 20);
            this.txtbExpirationDate.TabIndex = 30;
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Enabled = false;
            this.cbIsActive.Location = new System.Drawing.Point(109, 233);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(15, 14);
            this.cbIsActive.TabIndex = 41;
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // List6_5
            // 
            this.List6_5.AutoSize = true;
            this.List6_5.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List6_5.Location = new System.Drawing.Point(9, 223);
            this.List6_5.Name = "List6_5";
            this.List6_5.Size = new System.Drawing.Size(95, 27);
            this.List6_5.TabIndex = 23;
            this.List6_5.Text = "Active :";
            // 
            // lblInternationalLicenseID
            // 
            this.lblInternationalLicenseID.AutoSize = true;
            this.lblInternationalLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblInternationalLicenseID.Location = new System.Drawing.Point(149, 24);
            this.lblInternationalLicenseID.Name = "lblInternationalLicenseID";
            this.lblInternationalLicenseID.Size = new System.Drawing.Size(36, 26);
            this.lblInternationalLicenseID.TabIndex = 43;
            this.lblInternationalLicenseID.Text = "??";
            // 
            // List6_1
            // 
            this.List6_1.AutoSize = true;
            this.List6_1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List6_1.Location = new System.Drawing.Point(9, 22);
            this.List6_1.Name = "List6_1";
            this.List6_1.Size = new System.Drawing.Size(143, 27);
            this.List6_1.TabIndex = 42;
            this.List6_1.Text = "License ID :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(760, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 441);
            this.button1.TabIndex = 44;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // List0_2
            // 
            this.List0_2.AutoSize = true;
            this.List0_2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List0_2.Location = new System.Drawing.Point(20, 87);
            this.List0_2.Name = "List0_2";
            this.List0_2.Size = new System.Drawing.Size(172, 27);
            this.List0_2.TabIndex = 26;
            this.List0_2.Text = "License Class :";
            // 
            // cbLicenseClassLocal
            // 
            this.cbLicenseClassLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClassLocal.FormattingEnabled = true;
            this.cbLicenseClassLocal.Items.AddRange(new object[] {
            "Motorcycles",
            "LargeMotorcycles",
            "RegularCar",
            "PublicVehicles",
            "AgriculturalVehicles",
            "Buses",
            "HeavyVhicles"});
            this.cbLicenseClassLocal.Location = new System.Drawing.Point(193, 92);
            this.cbLicenseClassLocal.Name = "cbLicenseClassLocal";
            this.cbLicenseClassLocal.Size = new System.Drawing.Size(133, 21);
            this.cbLicenseClassLocal.TabIndex = 28;
            // 
            // List0_1
            // 
            this.List0_1.AutoSize = true;
            this.List0_1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.List0_1.Location = new System.Drawing.Point(20, 24);
            this.List0_1.Name = "List0_1";
            this.List0_1.Size = new System.Drawing.Size(234, 27);
            this.List0_1.TabIndex = 42;
            this.List0_1.Text = "Sub Application ID :";
            // 
            // lblLocalDrivingLicenseApplicationID
            // 
            this.lblLocalDrivingLicenseApplicationID.AutoSize = true;
            this.lblLocalDrivingLicenseApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblLocalDrivingLicenseApplicationID.Location = new System.Drawing.Point(253, 26);
            this.lblLocalDrivingLicenseApplicationID.Name = "lblLocalDrivingLicenseApplicationID";
            this.lblLocalDrivingLicenseApplicationID.Size = new System.Drawing.Size(36, 26);
            this.lblLocalDrivingLicenseApplicationID.TabIndex = 43;
            this.lblLocalDrivingLicenseApplicationID.Text = "??";
            // 
            // btnEyeTestID
            // 
            this.btnEyeTestID.BackColor = System.Drawing.Color.Bisque;
            this.btnEyeTestID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEyeTestID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnEyeTestID.Location = new System.Drawing.Point(159, 209);
            this.btnEyeTestID.Name = "btnEyeTestID";
            this.btnEyeTestID.Size = new System.Drawing.Size(66, 34);
            this.btnEyeTestID.TabIndex = 47;
            this.btnEyeTestID.Text = "??";
            this.btnEyeTestID.UseVisualStyleBackColor = false;
            this.btnEyeTestID.Click += new System.EventHandler(this.btnEyeTestID_Click);
            // 
            // btnTheoreticalTestID
            // 
            this.btnTheoreticalTestID.BackColor = System.Drawing.Color.Bisque;
            this.btnTheoreticalTestID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTheoreticalTestID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnTheoreticalTestID.Location = new System.Drawing.Point(39, 209);
            this.btnTheoreticalTestID.Name = "btnTheoreticalTestID";
            this.btnTheoreticalTestID.Size = new System.Drawing.Size(66, 34);
            this.btnTheoreticalTestID.TabIndex = 47;
            this.btnTheoreticalTestID.Text = "??";
            this.btnTheoreticalTestID.UseVisualStyleBackColor = false;
            // 
            // btnDrivingTestID
            // 
            this.btnDrivingTestID.BackColor = System.Drawing.Color.Bisque;
            this.btnDrivingTestID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDrivingTestID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnDrivingTestID.Location = new System.Drawing.Point(260, 209);
            this.btnDrivingTestID.Name = "btnDrivingTestID";
            this.btnDrivingTestID.Size = new System.Drawing.Size(66, 34);
            this.btnDrivingTestID.TabIndex = 47;
            this.btnDrivingTestID.Text = "??";
            this.btnDrivingTestID.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.Location = new System.Drawing.Point(153, 189);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 17);
            this.label11.TabIndex = 50;
            this.label11.Text = "Eye Test ID:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(12, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 17);
            this.label12.TabIndex = 50;
            this.label12.Text = "Theoretical Test  ID:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(247, 189);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 17);
            this.label13.TabIndex = 50;
            this.label13.Text = "Driving Test ID:";
            // 
            // gbInterNationalLicense
            // 
            this.gbInterNationalLicense.Controls.Add(this.List6_1);
            this.gbInterNationalLicense.Controls.Add(this.List6_2);
            this.gbInterNationalLicense.Controls.Add(this.List6_4);
            this.gbInterNationalLicense.Controls.Add(this.List6_5);
            this.gbInterNationalLicense.Controls.Add(this.List6_3);
            this.gbInterNationalLicense.Controls.Add(this.cbLicenseClassInterntional);
            this.gbInterNationalLicense.Controls.Add(this.txtbIssuedDate);
            this.gbInterNationalLicense.Controls.Add(this.lblInternationalLicenseID);
            this.gbInterNationalLicense.Controls.Add(this.txtbExpirationDate);
            this.gbInterNationalLicense.Controls.Add(this.cbIsActive);
            this.gbInterNationalLicense.Location = new System.Drawing.Point(415, 71);
            this.gbInterNationalLicense.Name = "gbInterNationalLicense";
            this.gbInterNationalLicense.Size = new System.Drawing.Size(350, 320);
            this.gbInterNationalLicense.TabIndex = 51;
            this.gbInterNationalLicense.TabStop = false;
            this.gbInterNationalLicense.Text = "International License Application";
            // 
            // gbLocalDrivingLicense
            // 
            this.gbLocalDrivingLicense.Controls.Add(this.label13);
            this.gbLocalDrivingLicense.Controls.Add(this.List0_1);
            this.gbLocalDrivingLicense.Controls.Add(this.label12);
            this.gbLocalDrivingLicense.Controls.Add(this.btnEyeTestID);
            this.gbLocalDrivingLicense.Controls.Add(this.label11);
            this.gbLocalDrivingLicense.Controls.Add(this.List0_2);
            this.gbLocalDrivingLicense.Controls.Add(this.btnDrivingTestID);
            this.gbLocalDrivingLicense.Controls.Add(this.cbLicenseClassLocal);
            this.gbLocalDrivingLicense.Controls.Add(this.btnTheoreticalTestID);
            this.gbLocalDrivingLicense.Controls.Add(this.lblLocalDrivingLicenseApplicationID);
            this.gbLocalDrivingLicense.Location = new System.Drawing.Point(775, 71);
            this.gbLocalDrivingLicense.Name = "gbLocalDrivingLicense";
            this.gbLocalDrivingLicense.Size = new System.Drawing.Size(350, 320);
            this.gbLocalDrivingLicense.TabIndex = 52;
            this.gbLocalDrivingLicense.TabStop = false;
            this.gbLocalDrivingLicense.Text = "Local License Application";
            // 
            // SecoundErrorProvider
            // 
            this.SecoundErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.SecoundErrorProvider.ContainerControl = this;
            // 
            // AddEidtApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1134, 442);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPersonID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbLastStatusDate);
            this.Controls.Add(this.txtbPaidFees);
            this.Controls.Add(this.txtbApplicationDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbApplicationType);
            this.Controls.Add(this.cbApplicationStatus);
            this.Controls.Add(this.lblCreatedByUserID);
            this.Controls.Add(this.lblApplicationID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbInterNationalLicense);
            this.Controls.Add(this.gbLocalDrivingLicense);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddEidtApplication";
            this.Text = "AddEidtApplication";
            this.Load += new System.EventHandler(this.AddEidtApplication_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AddEidtApplication_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.gbInterNationalLicense.ResumeLayout(false);
            this.gbInterNationalLicense.PerformLayout();
            this.gbLocalDrivingLicense.ResumeLayout(false);
            this.gbLocalDrivingLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecoundErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbLastStatusDate;
        private System.Windows.Forms.TextBox txtbApplicationDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbApplicationStatus;
        private System.Windows.Forms.Label lblCreatedByUserID;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbApplicationType;
        private System.Windows.Forms.Button btnPersonID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbPaidFees;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.ComboBox cbLicenseClassInterntional;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.TextBox txtbExpirationDate;
        private System.Windows.Forms.TextBox txtbIssuedDate;
        private System.Windows.Forms.Label List6_3;
        private System.Windows.Forms.Label List6_5;
        private System.Windows.Forms.Label List6_4;
        private System.Windows.Forms.Label List6_2;
        private System.Windows.Forms.Label lblInternationalLicenseID;
        private System.Windows.Forms.Label List6_1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblLocalDrivingLicenseApplicationID;
        private System.Windows.Forms.Label List0_1;
        private System.Windows.Forms.ComboBox cbLicenseClassLocal;
        private System.Windows.Forms.Label List0_2;
        private System.Windows.Forms.Button btnEyeTestID;
        private System.Windows.Forms.Button btnTheoreticalTestID;
        private System.Windows.Forms.Button btnDrivingTestID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbInterNationalLicense;
        private System.Windows.Forms.GroupBox gbLocalDrivingLicense;
        private System.Windows.Forms.ErrorProvider SecoundErrorProvider;
    }
}