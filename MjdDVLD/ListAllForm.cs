using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessTier;

namespace MjdDVLD
{
    
    public partial class ListAllForm : Form
    {
        public delegate void GeneralReturnEvent(object sender);
            public event GeneralReturnEvent OnAddButtonClick;
        
        public delegate void GeneralReturnDataTableEvent(object sender , ref DataTable RefrashedData);
            public event GeneralReturnDataTableEvent OnRefrashButtonClick;

        public delegate bool BoolReturnIDEvent(object sender, int ID);
            public event BoolReturnIDEvent OnDeletelButtonClick;

        public delegate void GeneralReturnIDEvent(object sender, int ID);
            public event GeneralReturnIDEvent OnEditButtonClick;
            public event GeneralReturnIDEvent OnOkButtonClick;
            public event GeneralReturnIDEvent OnShowCardButtonClick;

        public class Permissions
        {
            public enum Permission { Add = 1, Edit = 2, Delete = 4, ShowCard = 8, Refresh = 16 };


            private byte _Permissions;

            private Permissions(byte bt)
            {
                _Permissions = bt;
            }
            
            public Permissions()
            { }
            public Permissions(Permission p1)
            {
                AddPermission(p1);

            }
            public Permissions(Permission p1, Permission p2)
            {
                AddPermission(p1);
                AddPermission(p2);           }
            public Permissions(Permission p1, Permission p2, Permission p3)
            {
                AddPermission(p1);
                AddPermission(p2);
                AddPermission(p3);
            }
            public Permissions(Permission p1, Permission p2, Permission p3, Permission p4)
            {
                AddPermission(p1);
                AddPermission(p2);
                AddPermission(p3);
                AddPermission(p4);
            }
            public Permissions(Permission p1, Permission p2, Permission p3, Permission p4, Permission p5)
            {
                AddPermission(p1);
                AddPermission(p2);
                AddPermission(p3);
                AddPermission(p4);
                AddPermission(p5);
            }

            public bool DoesHavePermissionTo(Permission P)
            {
                if (((int)this._Permissions & (int)P) == (int)P)
                    return true;
                else
                    return false;
            }

            public void AddPermission(Permission P)
            {
                if (!this.DoesHavePermissionTo(P))
                    this._Permissions += (byte)P;
            }

            public static Permissions operator +(Permissions Ps, Permission P) 
            {
                Ps.AddPermission(P);
                return Ps;
            }

            private static Permissions _AddPermissions(Permissions Ps, Permission P)
            {
                if (!Ps.DoesHavePermissionTo(P))
                { Ps.AddPermission(P); }

                return Ps;
            }

        }

        //////////////////////////////////////////////////////////////////////////////////

        public ListAllForm(DataTable table, string Title, Permissions permissions,bool OkButton)
        {
            InitializeComponent();
            lblListName.Text = $"All {Title} List.";
            _LodedDataTable = table;

            cbColumns.Items.Add("Nan");

            foreach (DataColumn C in table.Columns)
            {
                cbColumns.Items.Add(C.ColumnName);
            }

            cbColumns.SelectedIndex = 0;

            if (!permissions.DoesHavePermissionTo(Permissions.Permission.Add))
                cmsOptions.Items[0].Visible= false  ; // Add
            if (!permissions.DoesHavePermissionTo(Permissions.Permission.Delete))
                cmsOptions.Items[1].Visible= false  ; // Delete
            if (!permissions.DoesHavePermissionTo(Permissions.Permission.Edit))
                cmsOptions.Items[2].Visible= false  ; // Edit
            if (!permissions.DoesHavePermissionTo(Permissions.Permission.ShowCard))
                cmsOptions.Items[3].Visible= false  ; // Show Card
            if (!permissions.DoesHavePermissionTo(Permissions.Permission.Refresh))
                cmsOptions.Items[4].Visible= false  ; // Refresh Datas

            if(!OkButton)
            { btnOk.Visible = false; }

            _ReloadDGV();
        }

        DataTable _LodedDataTable;

        private void _LoadDGV(DataView view)
        {
            dgvMain.DataSource = view;
        }

        private void _ReloadDGV()
        {
            if (cbColumns.SelectedIndex - 1 == -1)
            {
                _LoadDGV(_LodedDataTable.DefaultView);
                mtbInput.Mask = " ";
                return;
            }

            Type type = _LodedDataTable.Columns[cbColumns.SelectedIndex - 1].DataType;
            DataColumn SelectedColumn = _LodedDataTable.Columns[cbColumns.SelectedIndex - 1];
            string Order = cbDescending.Checked ? "desc" : "asc";


            if (type == typeof(Int32) || (type == typeof(Int16)) || type == typeof(byte) || type == typeof(Int64))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Text = "";
                mtbInput.Mask = "9999999999";
            }
            else if (type == typeof(string))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Text = "";
                mtbInput.Mask = "CCCCCCCCCCCCCCC";
            }
            else if (type == typeof(char))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Text = "";
                mtbInput.Mask = "C";
            }
            else if (type == typeof(decimal) || type == typeof(float) || type == typeof(Double))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Text = "";
                mtbInput.Mask = "###############";
            }
            else if (type == typeof(bool))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Visible = false;
                cbColumns.Visible = true;
            }
            else if (type == typeof(DateTime))
            {
                DataView view = _LodedDataTable.DefaultView;
                view.Sort = $"{SelectedColumn.ColumnName} {Order}";
                _LoadDGV(view);

                mtbInput.Text = "";
                mtbInput.Mask = "00 / 00 / 0000";

            }

        }
        
        private void cbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ReloadDGV();
        }

        private void cbDescending_CheckedChanged(object sender, EventArgs e)
        {
            _ReloadDGV();
        }

        private void ListAllForm_Resize(object sender, EventArgs e)
        {
            lblListName.Location = new Point((this.Size.Width * 12)/( 800), (this.Size.Height * 9) / (475)); 
            cbDescending.Location = new Point((this.Size.Width * 75) / (800), (this.Size.Height * 54) / (475));
            cbColumns.Location = new Point((this.Size.Width * 183) / (800), (this.Size.Height * 51) / (475));
            mtbInput.Location = new Point((this.Size.Width * 344) / (800), (this.Size.Height * 52) / (475));
            btnOk.Location = new Point((this.Size.Width * 655) / (800), (this.Size.Height * 392) / (475)); ; 

            dgvMain.Size = new Size(this.Size.Width - 16,this.Size.Height - (this.Size.Height * 89) / (475));
            dgvMain.Location = new Point((this.Size.Width * 0) / (800), (this.Size.Height * 89) / (475));
           
        }

        private void mtbInput_TextChanged(object sender, EventArgs e)
        {
            if (cbColumns.SelectedIndex == 0 || string.IsNullOrEmpty(mtbInput.Text) || mtbInput.Text == "," || mtbInput.Text == "   /    / ")
            {
                _ReloadDGV();
                return;
            }

            if (mtbInput.Mask.Contains("C"))
                _LoadDGV(new DataView(_LodedDataTable, cbColumns.Items[cbColumns.SelectedIndex].ToString() + " Like \'%" + mtbInput.Text + "%\'",
                   cbColumns.Items[cbColumns.SelectedIndex].ToString() + (cbDescending.Checked ? " desc" : " asc"), DataViewRowState.Unchanged));

            else if (mtbInput.Mask.Contains("/"))
            {
                if(DateTime.TryParse(mtbInput.Text,out DateTime dateTime))
                _LoadDGV(new DataView(_LodedDataTable,
                    cbColumns.Items[cbColumns.SelectedIndex].ToString() + " > " + $"Convert('{(dateTime - new TimeSpan(24, 0, 0)).ToShortDateString()}','System.DateTime') " + " and " +
                    cbColumns.Items[cbColumns.SelectedIndex].ToString() + " < " + $"Convert('{(dateTime + new TimeSpan(24, 0, 0)).ToShortDateString()}','System.DateTime') ",
                    cbColumns.Items[cbColumns.SelectedIndex].ToString() + (cbDescending.Checked ? " desc" : " asc"), DataViewRowState.Unchanged));
            }

            else
                _LoadDGV(new DataView(_LodedDataTable,
                    cbColumns.Items[cbColumns.SelectedIndex].ToString() + " > " + (Convert.ToInt32(mtbInput.Text) - 1).ToString() + " and " +
                    cbColumns.Items[cbColumns.SelectedIndex].ToString() + " < " + (Convert.ToInt32(mtbInput.Text) + 1).ToString(),
                   cbColumns.Items[cbColumns.SelectedIndex].ToString() + (cbDescending.Checked ? " desc" : " asc"), DataViewRowState.Unchanged));
            
        }

        // // // // // contecst minu strip // // // // // 



        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddButtonClick?.Invoke(this);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedCells.Count > 0)
                if (MessageBox.Show("Are you sure you want to delete this Row?", "Worning.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    if(!(bool)OnDeletelButtonClick?.Invoke(this, Convert.ToInt32(dgvMain.Rows[dgvMain.SelectedCells[0].RowIndex].Cells[0].Value)))
                        MessageBox.Show("Deletion Failed.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedCells.Count > 0)
                OnEditButtonClick?.Invoke(this, Convert.ToInt32(dgvMain.Rows[dgvMain.SelectedCells[0].RowIndex].Cells[0].Value));
        }

        private void showCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedCells.Count > 0)
                OnShowCardButtonClick?.Invoke(this, Convert.ToInt32(dgvMain.Rows[dgvMain.SelectedCells[0].RowIndex].Cells[0].Value));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedCells.Count > 0)
                OnOkButtonClick?.Invoke(this, Convert.ToInt32(dgvMain.Rows[dgvMain.SelectedCells[0].RowIndex].Cells[0].Value));
            this.Close();
        }

        private void refrashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRefrashButtonClick?.Invoke(this, ref _LodedDataTable);

            _ReloadDGV();
        }
    }

}
