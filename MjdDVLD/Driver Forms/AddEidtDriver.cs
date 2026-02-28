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

namespace MjdDVLD.Driver_Forms
{
    public partial class AddEidtDriver : Form
    {
        public AddEidtDriver(int DriverIDToEdit = -1, bool ShowOnly = false)
        {
            InitializeComponent();

            txtbCreatDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();

            if (DriverIDToEdit != -1)
            {
                Driver DriverToEdit = DVLDApp.MangeDrivers.Find(DriverIDToEdit);

                if (DriverToEdit != null)
                {
                    _LoadDriverInfo(DriverToEdit);
                    if(ShowOnly)
                    {
                        btnPersonID.Enabled = false;
                        btnSave.Visible = false;
                        txtbCreatDate.Enabled = false;
                        btnCancel.Text = "Ok";
                    }
                }

            }
        }

        private void _LoadCurrantDriverImage()
        {
            if (lblDriverID.Text != "??")
                pbPhoto.ImageLocation = DVLDApp.MangePeople.GetPersonImagePath(Convert.ToInt32(btnPersonID.Text));
        }

        private void _LoadDriverInfo(Driver DriverToLoad)
        {
            lblDriverID.Text = DriverToLoad.DriverID.ToString();
            btnPersonID.Text = DriverToLoad.PersonID.ToString();
            lblCreatedByUserID.Text = DriverToLoad.CreatedByUserID.ToString();
            txtbCreatDate.Text = DriverToLoad.CreatedDate.ToShortDateString();
            _LoadCurrantDriverImage();
        }

        private void btnPersonID_Click(object sender, EventArgs e)
        {
            ListAllForm ApplicationsList = new ListAllForm
             (DVLDApp.MangePeople.ListAllPeople(), "Applications", new ListAllForm.Permissions(), true);

            ApplicationsList.OnOkButtonClick += _ChangePersonID;

            ApplicationsList.ShowDialog();
        }

        private void _ChangePersonID(object sender, int ID)
        {
            btnPersonID.Text = ID.ToString();
            pbPhoto.ImageLocation = DVLDApp.MangePeople.GetPersonImagePath(ID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Driver _LoadDriver()
        {
            if (lblDriverID.Text == "??")
                return new Driver(Convert.ToInt32(btnPersonID.Text));
            else
            {
                Driver FindedDriver = DVLDApp.MangeDrivers.Find(Convert.ToInt32(btnPersonID.Text));

                FindedDriver.PersonID = Convert.ToInt32(btnPersonID.Text);

                return FindedDriver;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorProvider.Clear();

            if (btnPersonID.Text == "??")
            {
                ErrorProvider.SetError(btnPersonID, "You have to Choose a Person.");
                return;
            }

            if (lblDriverID.Text == "??")
            {

                if (DVLDApp.MangeDrivers.IsExistByPersonID(Convert.ToInt32 (btnPersonID.Text)))
                { MessageBox.Show("This Person already have a Driver Card.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                { DVLDApp.MangeDrivers.Add(_LoadDriver()); }
            }
            else
            {
                if(DVLDApp.MangeDrivers.IsExist(Convert.ToInt32 (lblDriverID.Text)))
                {
                    DVLDApp.MangeDrivers.Update(_LoadDriver());
                }

            }

            this.Close();
        }
    
    }
}
