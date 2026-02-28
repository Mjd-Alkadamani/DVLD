using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Generale;
using BusinessTier;

namespace MjdDVLD.Licenses_Forms
{
    public partial class AddEditLicenses : Form
    {
        public AddEditLicenses(int LicenseIDToEdit = -1, bool OnlyShow = false)
        {
            InitializeComponent();

            txtbIssueDate.Text = DateTime.Now.ToShortDateString();

            cbLicenceClass.SelectedIndex = 0; // _ResetExpirationDate(); will be called internaly when changing endix //
            cbIssueReason.SelectedIndex = 0;
            lblCreatedByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();


            if (LicenseIDToEdit != -1)
            {
                License LicenseToEdit = DVLDApp.MangeLicenses.Find(LicenseIDToEdit);

                if (LicenseToEdit != null)
                {
                    _LoadLicenseInfo(LicenseToEdit);
                    this.Text = "Edite License";

                    if (OnlyShow)
                    {
                        btnApplicationID.Enabled = false;
                        btnDriverID.Enabled = false;
                        cbLicenceClass.Enabled = false;
                        cbIssueReason.Enabled = false;
                        txtbNotes.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Text = "Ok";
                    }
                }
                else
                {
                    MessageBox.Show("Sory, this License ID does not exist.", "Error");
                    this.Close();
                }
            }
        }

        private void btnApplicationID_Click(object sender, EventArgs e)
        {
            DataTable OrigenalTable = DVLDApp.MangeApplications.ListAllApplications();
            DataView view = new DataView(OrigenalTable, "ApplicationTypeID = " + ((int)ApplicationType.LicenseIssuance).ToString(), "ApplicationID desc", DataViewRowState.Unchanged);

            ListAllForm ApplicationsList = new ListAllForm
             (view.ToTable(), "Applications", new ListAllForm.Permissions(), true);

            ApplicationsList.OnOkButtonClick += _ChangeApplicationID;

            ApplicationsList.ShowDialog();
        }

        private void _ChangeApplicationID(object sender, int ID)
        {
            btnApplicationID.Text = ID.ToString();
        }

        private void btnDriverID_Click(object sender, EventArgs e)
        {
            ListAllForm DriversList = new ListAllForm
             (DVLDApp.MangeDrivers.ListAllDrivers(), "Drivers", new ListAllForm.Permissions(), true);

            DriversList.OnOkButtonClick += _ChangeDriverID;

            DriversList.ShowDialog();
        }

        private void _ChangeDriverID(object sender, int ID)
        {
            btnDriverID.Text = ID.ToString();

            _LoadCurrantDriverImage();
        }

        private void _LoadCurrantDriverImage()
        {
            if (btnDriverID.Text != "??")
                pbPhoto.ImageLocation = DVLDApp.MangeDrivers.GetDriverImagePath(Convert.ToInt32(btnDriverID.Text));
        }

        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtbNotes.Text.Length >= 500)
            {
                txtbNotes.Text = txtbNotes.Text.Substring(0, 500);
            }
        }

        private void _LoadLicenseInfo(License LicenseToLoad)
        {
            lblLicenseID.Text = LicenseToLoad.LicenseID.ToString();
            btnApplicationID.Text = LicenseToLoad.LocalDrivingLicenseApplicationID.ToString();
            btnDriverID.Text = LicenseToLoad.DriverID.ToString();
            cbLicenceClass.SelectedIndex = (int)LicenseToLoad.LicenseClass - 1;
            txtbIssueDate.Text = LicenseToLoad.IssueDate.ToShortDateString();
            txtbExpirationDate.Text = LicenseToLoad.ExpirationDate.ToShortDateString();
            lblCreatedByUserID.Text = LicenseToLoad.CreatedByUserID.ToString();
            cbIsActive.Checked = LicenseToLoad.IsActive;
            cbIssueReason.SelectedIndex = (int)LicenseToLoad.IssueReason - 1;
            txtbNotes.Text = LicenseToLoad.Notes;

            _LoadCurrantDriverImage();
        }

        private void cbLicenceClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ResetExpirationDate();
        }

        private void _ResetExpirationDate()
        {
            txtbExpirationDate.Text = Convert.ToDateTime(txtbIssueDate.Text).AddYears(SettingsClass.HowManyYears((LicenseClass)cbLicenceClass.SelectedIndex + 1)).ToShortDateString();
        }


        //////// Save and Cancel buttons ////////

        private License _ToLicense()
        {


            if (lblLicenseID.Text == "??")
            {
                return new License(Convert.ToInt32(btnApplicationID.Text),
                     Convert.ToInt32(btnDriverID.Text),
                     (LicenseClass)cbLicenceClass.SelectedIndex + 1,
                     Convert.ToDateTime(txtbIssueDate.Text),
                     Convert.ToDateTime(txtbExpirationDate.Text),
                     txtbNotes.Text == null ? null : (string)txtbNotes.Text,
                     cbIsActive.Checked,
                     (IssueReason)cbIssueReason.SelectedIndex + 1);
            }

            else
            {
                License LicenseToLoad;

                LicenseToLoad = DVLDApp.MangeLicenses.Find(Convert.ToInt32(lblLicenseID.Text));

                LicenseToLoad.LocalDrivingLicenseApplicationID = Convert.ToInt32(btnApplicationID.Text);
                LicenseToLoad.DriverID = Convert.ToInt32(btnDriverID.Text);
                LicenseToLoad.LicenseClass = (LicenseClass)cbLicenceClass.SelectedIndex + 1;
                LicenseToLoad.IssueDate = Convert.ToDateTime(txtbIssueDate.Text);
                LicenseToLoad.ExpirationDate = Convert.ToDateTime(txtbExpirationDate.Text);
                LicenseToLoad.Notes = txtbNotes.Text;
                LicenseToLoad.IsActive = cbIsActive.Checked;
                LicenseToLoad.IssueReason = (IssueReason)cbIssueReason.SelectedIndex + 1;

                return LicenseToLoad;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool successed = false;

            ErrorProvider.Clear();

            if (btnApplicationID.Text=="??")
            {
                ErrorProvider.SetError(btnApplicationID, "You have to shose a Driver.");
                return;
            }
            if(btnDriverID.Text=="??")
            {
                ErrorProvider.SetError(btnDriverID, "You have to shose an Application.");
                return;
            }

            if (lblLicenseID.Text == "??")
            {
                successed = DVLDApp.MangeLicenses.Add(_ToLicense());
            }
            else if (DVLDApp.MangeLicenses.IsExist(Convert.ToInt32(lblLicenseID.Text)))
            {
                successed = DVLDApp.MangeLicenses.Update(_ToLicense());
            }
            else if (MessageBox.Show("This License ID does not Exist do you want to add It as new one?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblLicenseID.Text = "??";
                successed = DVLDApp.MangeLicenses.Add(_ToLicense());
            }

            if (successed)
                MessageBox.Show("Added successfully.", "success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Adding Failed.", "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();

        }

    }
}
