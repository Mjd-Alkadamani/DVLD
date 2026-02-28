
namespace MjdDVLD.Driver_Forms
{
    partial class AddEidtDriver
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
            this.lblDriverID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPersonID = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCreatedByUserID = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtbCreatDate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDriverID
            // 
            this.lblDriverID.AutoSize = true;
            this.lblDriverID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblDriverID.Location = new System.Drawing.Point(391, 39);
            this.lblDriverID.Name = "lblDriverID";
            this.lblDriverID.Size = new System.Drawing.Size(36, 26);
            this.lblDriverID.TabIndex = 21;
            this.lblDriverID.Text = "??";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(249, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 27);
            this.label1.TabIndex = 20;
            this.label1.Text = "Driver ID : ";
            // 
            // btnPersonID
            // 
            this.btnPersonID.BackColor = System.Drawing.Color.Bisque;
            this.btnPersonID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPersonID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPersonID.Location = new System.Drawing.Point(190, 116);
            this.btnPersonID.Name = "btnPersonID";
            this.btnPersonID.Size = new System.Drawing.Size(66, 34);
            this.btnPersonID.TabIndex = 23;
            this.btnPersonID.Text = "??";
            this.btnPersonID.UseVisualStyleBackColor = false;
            this.btnPersonID.Click += new System.EventHandler(this.btnPersonID_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(42, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 27);
            this.label2.TabIndex = 22;
            this.label2.Text = "Person ID : ";
            // 
            // lblCreatedByUserID
            // 
            this.lblCreatedByUserID.AutoSize = true;
            this.lblCreatedByUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblCreatedByUserID.Location = new System.Drawing.Point(282, 183);
            this.lblCreatedByUserID.Name = "lblCreatedByUserID";
            this.lblCreatedByUserID.Size = new System.Drawing.Size(36, 26);
            this.lblCreatedByUserID.TabIndex = 25;
            this.lblCreatedByUserID.Text = "??";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(42, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(243, 27);
            this.label9.TabIndex = 24;
            this.label9.Text = "Created By User ID : ";
            // 
            // txtbCreatDate
            // 
            this.txtbCreatDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtbCreatDate.Location = new System.Drawing.Point(222, 249);
            this.txtbCreatDate.Name = "txtbCreatDate";
            this.txtbCreatDate.ReadOnly = true;
            this.txtbCreatDate.Size = new System.Drawing.Size(127, 20);
            this.txtbCreatDate.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(42, 243);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 27);
            this.label10.TabIndex = 27;
            this.label10.Text = "Creation Date : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(338, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(500, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 27);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbPhoto
            // 
            this.pbPhoto.Location = new System.Drawing.Point(480, 96);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(150, 150);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPhoto.TabIndex = 31;
            this.pbPhoto.TabStop = false;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // AddEidtDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 338);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbPhoto);
            this.Controls.Add(this.txtbCreatDate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblCreatedByUserID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnPersonID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDriverID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddEidtDriver";
            this.Text = "AddEidtDriver";
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDriverID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPersonID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCreatedByUserID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbCreatDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}