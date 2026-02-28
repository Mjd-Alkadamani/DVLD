
namespace MjdDVLD
{
    partial class AllPeopleListScreen
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
            this.listAllPeopleControl1 = new MjdDVLD.ListAllPeopleControl();
            this.SuspendLayout();
            // 
            // listAllPeopleControl1
            // 
            this.listAllPeopleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAllPeopleControl1.Location = new System.Drawing.Point(0, 0);
            this.listAllPeopleControl1.Name = "listAllPeopleControl1";
            this.listAllPeopleControl1.Size = new System.Drawing.Size(1346, 450);
            this.listAllPeopleControl1.TabIndex = 0;
            this.listAllPeopleControl1.OnOkButtonClick += new MjdDVLD.ListAllPeopleControl.OkButtenEventHandler(this.listAllPeopleControl1_OnOkButtonClick);
            this.listAllPeopleControl1.OnCancelButtonClick += new MjdDVLD.ListAllPeopleControl.CancelButtenEventHandler(this.listAllPeopleControl1_OnCancelButtonClick);
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 450);
            this.Controls.Add(this.listAllPeopleControl1);
            this.Name = "LoginScreen";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ListAllPeopleControl listAllPeopleControl1;
    }
}

