
namespace MjdDVLD
{
    partial class ListAllPeopleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnCancele = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTable.Location = new System.Drawing.Point(0, 46);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.Size = new System.Drawing.Size(600, 354);
            this.dgvTable.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Trebuchet MS", 18F);
            this.lblHeader.Location = new System.Drawing.Point(215, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(166, 29);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "All People List";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancele
            // 
            this.btnCancele.Location = new System.Drawing.Point(500, 365);
            this.btnCancele.Name = "btnCancele";
            this.btnCancele.Size = new System.Drawing.Size(75, 23);
            this.btnCancele.TabIndex = 2;
            this.btnCancele.Text = "Cancel";
            this.btnCancele.UseVisualStyleBackColor = true;
            this.btnCancele.Click += new System.EventHandler(this.btnCancele_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(410, 365);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ListAllPeopleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancele);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.dgvTable);
            this.Name = "ListAllPeopleControl";
            this.Size = new System.Drawing.Size(600, 400);
            this.SizeChanged += new System.EventHandler(this.ListAllPeopleControl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCancele;
        private System.Windows.Forms.Button btnOk;
    }
}
