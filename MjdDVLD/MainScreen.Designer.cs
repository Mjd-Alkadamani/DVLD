
namespace MjdDVLD
{
    partial class MainScreen
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
            this.msMainStrip = new System.Windows.Forms.MenuStrip();
            this.applicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ditenedLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diteningRecordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allDriversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allPeopleListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainStrip
            // 
            this.msMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsToolStripMenuItem,
            this.licensesToolStripMenuItem,
            this.userToolStripMenuItem,
            this.driversToolStripMenuItem,
            this.peopleToolStripMenuItem});
            this.msMainStrip.Location = new System.Drawing.Point(0, 0);
            this.msMainStrip.Name = "msMainStrip";
            this.msMainStrip.Size = new System.Drawing.Size(924, 24);
            this.msMainStrip.TabIndex = 0;
            this.msMainStrip.Text = "menuStrip1";
            // 
            // applicationsToolStripMenuItem
            // 
            this.applicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllApplicationsToolStripMenuItem});
            this.applicationsToolStripMenuItem.Name = "applicationsToolStripMenuItem";
            this.applicationsToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.applicationsToolStripMenuItem.Text = "Applications";
            // 
            // showAllApplicationsToolStripMenuItem
            // 
            this.showAllApplicationsToolStripMenuItem.Name = "showAllApplicationsToolStripMenuItem";
            this.showAllApplicationsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.showAllApplicationsToolStripMenuItem.Text = "Show All Applications";
            this.showAllApplicationsToolStripMenuItem.Click += new System.EventHandler(this.showAllApplicationsToolStripMenuItem_Click);
            // 
            // licensesToolStripMenuItem
            // 
            this.licensesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allLicensesToolStripMenuItem,
            this.ditenedLicensesToolStripMenuItem,
            this.diteningRecordsToolStripMenuItem});
            this.licensesToolStripMenuItem.Name = "licensesToolStripMenuItem";
            this.licensesToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.licensesToolStripMenuItem.Text = "Licenses";
            // 
            // allLicensesToolStripMenuItem
            // 
            this.allLicensesToolStripMenuItem.Name = "allLicensesToolStripMenuItem";
            this.allLicensesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.allLicensesToolStripMenuItem.Text = "All Licenses";
            this.allLicensesToolStripMenuItem.Click += new System.EventHandler(this.allLicensesToolStripMenuItem_Click);
            // 
            // ditenedLicensesToolStripMenuItem
            // 
            this.ditenedLicensesToolStripMenuItem.Name = "ditenedLicensesToolStripMenuItem";
            this.ditenedLicensesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.ditenedLicensesToolStripMenuItem.Text = "Currently Ditened Licenses";
            this.ditenedLicensesToolStripMenuItem.Click += new System.EventHandler(this.ditenedLicensesToolStripMenuItem_Click);
            // 
            // diteningRecordsToolStripMenuItem
            // 
            this.diteningRecordsToolStripMenuItem.Name = "diteningRecordsToolStripMenuItem";
            this.diteningRecordsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.diteningRecordsToolStripMenuItem.Text = "Ditening records";
            this.diteningRecordsToolStripMenuItem.Click += new System.EventHandler(this.diteningRecordsToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allUsersToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.userToolStripMenuItem.Text = "Mange Users";
            // 
            // allUsersToolStripMenuItem
            // 
            this.allUsersToolStripMenuItem.Name = "allUsersToolStripMenuItem";
            this.allUsersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allUsersToolStripMenuItem.Text = "All Users";
            this.allUsersToolStripMenuItem.Click += new System.EventHandler(this.allUsersToolStripMenuItem_Click);
            // 
            // driversToolStripMenuItem
            // 
            this.driversToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allDriversToolStripMenuItem});
            this.driversToolStripMenuItem.Name = "driversToolStripMenuItem";
            this.driversToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.driversToolStripMenuItem.Text = "Drivers";
            // 
            // allDriversToolStripMenuItem
            // 
            this.allDriversToolStripMenuItem.Name = "allDriversToolStripMenuItem";
            this.allDriversToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.allDriversToolStripMenuItem.Text = "All Drivers";
            this.allDriversToolStripMenuItem.Click += new System.EventHandler(this.allDriversToolStripMenuItem_Click);
            // 
            // peopleToolStripMenuItem
            // 
            this.peopleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allPeopleListToolStripMenuItem});
            this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
            this.peopleToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.peopleToolStripMenuItem.Text = "People";
            // 
            // allPeopleListToolStripMenuItem
            // 
            this.allPeopleListToolStripMenuItem.Name = "allPeopleListToolStripMenuItem";
            this.allPeopleListToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.allPeopleListToolStripMenuItem.Text = "All People List";
            this.allPeopleListToolStripMenuItem.Click += new System.EventHandler(this.allPeopleListToolStripMenuItem_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(248)))));
            this.BackgroundImage = global::MjdDVLD.Properties.Resources.heraldry_lion_brand_logo_design_260747_160;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(924, 518);
            this.Controls.Add(this.msMainStrip);
            this.MainMenuStrip = this.msMainStrip;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.msMainStrip.ResumeLayout(false);
            this.msMainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainStrip;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ditenedLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diteningRecordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allDriversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allPeopleListToolStripMenuItem;
    }
}