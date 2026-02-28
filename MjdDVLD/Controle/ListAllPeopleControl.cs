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
    public partial class ListAllPeopleControl : UserControl
    {

        public delegate void OkButtenEventHandler(object sender, Person PersonID);
        public event OkButtenEventHandler OnOkButtonClick;

        public delegate void CancelButtenEventHandler(object sender);
        public event CancelButtenEventHandler OnCancelButtonClick; // Parameters : (object sender)

        public DataView MainView;

        public ListAllPeopleControl()
        {
            InitializeComponent();

            _ReLoadPeopleList();

        }

        private void _ReLoadPeopleList()
        {
            MainView = DVLDApp.MangePeople.ListAllPeople().DefaultView;
            dgvTable.DataSource = MainView;
        }

        private void btnCancele_Click(object sender, EventArgs e)
        {
            OnCancelButtonClick?.Invoke(this);
        }

        private void ListAllPeopleControl_SizeChanged(object sender, EventArgs e)
        {
            btnCancele.Location = new Point(this.Size.Width * 500 / 600, this.Size.Height * 365 / 400);
            btnOk.Location = new Point(this.Size.Width * 410 / 600, this.Size.Height * 365 / 400);
            lblHeader.Location = new Point(this.Size.Width * 215 / 600, this.Size.Height * 8 / 400);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedCells.Count > 0)
            {
                Person SelectedPerson = (Person)DVLDApp.MangePeople.Find((int)dgvTable.Rows[dgvTable.SelectedCells[0].RowIndex].Cells[0].Value);
                
                if((object)SelectedPerson == null)
                {
                    MessageBox.Show("Error.","Person Not Found.",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    _ReLoadPeopleList();

                    return;
                }

                OnOkButtonClick?.Invoke(this, SelectedPerson);
            }
        }
    

    }
}
