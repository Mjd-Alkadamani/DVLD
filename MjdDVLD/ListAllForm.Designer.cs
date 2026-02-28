
namespace MjdDVLD
{
    partial class ListAllForm
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
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.cmsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblListName = new System.Windows.Forms.Label();
            this.cbColumns = new System.Windows.Forms.ComboBox();
            this.mtbInput = new System.Windows.Forms.MaskedTextBox();
            this.cbTrueFalse = new System.Windows.Forms.CheckBox();
            this.cbDescending = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.refrashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.cmsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.ContextMenuStrip = this.cmsOptions;
            this.dgvMain.Location = new System.Drawing.Point(0, 89);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.Size = new System.Drawing.Size(784, 350);
            this.dgvMain.StandardTab = true;
            this.dgvMain.TabIndex = 0;
            // 
            // cmsOptions
            // 
            this.cmsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.editToolStripMenuItem,
            this.showCardToolStripMenuItem,
            this.refrashToolStripMenuItem});
            this.cmsOptions.Name = "cmsOptions";
            this.cmsOptions.Size = new System.Drawing.Size(181, 136);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // showCardToolStripMenuItem
            // 
            this.showCardToolStripMenuItem.Name = "showCardToolStripMenuItem";
            this.showCardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showCardToolStripMenuItem.Text = "Show Card";
            this.showCardToolStripMenuItem.Click += new System.EventHandler(this.showCardToolStripMenuItem_Click);
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblListName.Location = new System.Drawing.Point(12, 9);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(116, 29);
            this.lblListName.TabIndex = 1;
            this.lblListName.Text = "All X List";
            // 
            // cbColumns
            // 
            this.cbColumns.FormattingEnabled = true;
            this.cbColumns.Location = new System.Drawing.Point(183, 51);
            this.cbColumns.Name = "cbColumns";
            this.cbColumns.Size = new System.Drawing.Size(128, 21);
            this.cbColumns.TabIndex = 2;
            this.cbColumns.SelectedIndexChanged += new System.EventHandler(this.cbColumns_SelectedIndexChanged);
            // 
            // mtbInput
            // 
            this.mtbInput.Location = new System.Drawing.Point(344, 52);
            this.mtbInput.Mask = " ";
            this.mtbInput.Name = "mtbInput";
            this.mtbInput.Size = new System.Drawing.Size(122, 20);
            this.mtbInput.TabIndex = 3;
            this.mtbInput.TextChanged += new System.EventHandler(this.mtbInput_TextChanged);
            // 
            // cbTrueFalse
            // 
            this.cbTrueFalse.AutoSize = true;
            this.cbTrueFalse.Location = new System.Drawing.Point(344, 55);
            this.cbTrueFalse.Name = "cbTrueFalse";
            this.cbTrueFalse.Size = new System.Drawing.Size(15, 14);
            this.cbTrueFalse.TabIndex = 4;
            this.cbTrueFalse.UseVisualStyleBackColor = true;
            this.cbTrueFalse.Visible = false;
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.cbDescending.Location = new System.Drawing.Point(75, 54);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbDescending.Size = new System.Drawing.Size(91, 17);
            this.cbDescending.TabIndex = 5;
            this.cbDescending.Text = "Descending";
            this.cbDescending.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDescending.UseVisualStyleBackColor = true;
            this.cbDescending.CheckedChanged += new System.EventHandler(this.cbDescending_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(655, 392);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // refrashToolStripMenuItem
            // 
            this.refrashToolStripMenuItem.Name = "refrashToolStripMenuItem";
            this.refrashToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refrashToolStripMenuItem.Text = "Refrash";
            this.refrashToolStripMenuItem.Click += new System.EventHandler(this.refrashToolStripMenuItem_Click);
            // 
            // ListAllForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 437);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbDescending);
            this.Controls.Add(this.mtbInput);
            this.Controls.Add(this.cbColumns);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.cbTrueFalse);
            this.Name = "ListAllForm";
            this.Text = "ListAllForm";
            this.Resize += new System.EventHandler(this.ListAllForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.cmsOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.ComboBox cbColumns;
        private System.Windows.Forms.MaskedTextBox mtbInput;
        private System.Windows.Forms.CheckBox cbTrueFalse;
        private System.Windows.Forms.CheckBox cbDescending;
        private System.Windows.Forms.ContextMenuStrip cmsOptions;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolStripMenuItem showCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refrashToolStripMenuItem;
    }
}