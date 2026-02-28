using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General;
using BusinessTier;

namespace MjdDVLD.Licenses_Forms
{
    public partial class AddEditLicenses : Form
    {
        public AddEditLicenses(int LicenseIDToEdit = -1, bool OnlyShow = false)
        {
            InitializeComponent();

            txtbIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            cbLicenseClass.SelectedIndex = 0; // _ResetExpirationDate(); will be called internaly when changing endix //
            cbIssueReason.SelectedIndex = 0;
            lblCreatedByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();


                    cbIssueReason.Enabled = false;
            if (LicenseIDToEdit != -1)
            { // Add  new //
                License LicenseToEdit = DVLDApp.MangeLicenses.Find(LicenseIDToEdit);

                if (LicenseToEdit != null)
                {
                    _LoadLicenseInfo(LicenseToEdit);

                    btnDriverID.Enabled = false;
                    btnApplicationID.Enabled = false;

                    txtbNotes.Tag = txtbNotes.Text; // the tag will hold the old value //

                    this.Text = "Edite License";

                    if (OnlyShow)
                    {
                        btnApplicationID.Enabled = false;
                        btnDriverID.Enabled = false;
                        cbLicenseClass.Enabled = false;
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
            if(btnDriverID.Text == "??")
            {
                MessageBox.Show("You have to select a Driver first.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable OrigenalTable = DVLDApp.MangeApplications.GetAllPersonApplications(DVLDApp.MangeDrivers.GetPersonID(Convert.ToInt32(btnDriverID.Text)),ApplicationType.LicenseIssuance);


            ListAllForm ApplicationsList = new ListAllForm
             (OrigenalTable, "Person License Issuance Applications", new ListAllForm.Permissions(), true);

            ApplicationsList.OnOkButtonClick += _ChangeApplicationID;

            ApplicationsList.ShowDialog();
        }

        private void _ChangeApplicationID(object sender, int ID)
        {
            if(!DVLDApp.MangeApplications.IsTheApplicationValidToAttachLicense(ID))
            {
                MessageBox.Show("Invalid or incomlit  Application.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnApplicationID.Text = ID.ToString();

            cbLicenseClass.SelectedIndex = ((int)DVLDApp.MangeDrivingTests.FindByApplicationID(ID).TestClass)-1;
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
            btnApplicationID.Text = DVLDApp.MangeLocalDrivingLicenseApplications.Find( LicenseToLoad.LocalDrivingLicenseApplicationID).ApplicationID.ToString();
            btnDriverID.Text = LicenseToLoad.DriverID.ToString();
            cbLicenseClass.SelectedIndex = (int)LicenseToLoad.LicenseClass - 1;
            txtbIssueDate.Text = LicenseToLoad.IssueDate.ToString("dd/MM/yyyy HH:mm");
            txtbExpirationDate.Text = LicenseToLoad.ExpirationDate.ToString("dd/MM/yyyy HH:mm");
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
            txtbExpirationDate.Text = Convert.ToDateTime(txtbIssueDate.Text).AddYears(SettingsClass.HowManyYears((LicenseClass)cbLicenseClass.SelectedIndex + 1)).ToString("dd/MM/yyyy HH:mm");
        }


        //////// Save and Cancel buttons ////////

        private License _ToLicense()
        {


            if (lblLicenseID.Text == "??")
            {
                return new License(DVLDApp.MangeLocalDrivingLicenseApplications.FindByApplicationID(Convert.ToInt32(btnApplicationID.Text)).LocalDrivingLicenseApplicationID,
                     Convert.ToInt32(btnDriverID.Text),
                     (LicenseClass)cbLicenseClass.SelectedIndex + 1,
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

                LicenseToLoad.LocalDrivingLicenseApplicationID = DVLDApp.MangeLocalDrivingLicenseApplications.FindByApplicationID(Convert.ToInt32(btnApplicationID.Text)).LocalDrivingLicenseApplicationID;
                LicenseToLoad.DriverID = Convert.ToInt32(btnDriverID.Text);
                LicenseToLoad.LicenseClass = (LicenseClass)cbLicenseClass.SelectedIndex + 1;
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

            if (lblLicenseID.Text == "??")
            {
                ErrorProvider.Clear();

                if (btnDriverID.Text == "??")
                {
                    ErrorProvider.SetError(btnDriverID, "You have to choose a Driver.");
                    return;
                }
                if (btnApplicationID.Text == "??")
                {
                    ErrorProvider.SetError(btnApplicationID, "You have to shose an Application.");
                    return;
                }


                if (DVLDApp.MangeDrivers.DoesDriverHaveLicenseOfClass(Convert.ToInt32(btnDriverID.Text), (LicenseClass)cbLicenseClass.SelectedIndex + 1))
                {
                    MessageBox.Show("Driver Allready have a license of this class.", "Adding failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime? DriverBirthDate = DVLDApp.MangeDrivers.GetDriverBirthDate(Convert.ToInt32(btnDriverID.Text));

                if (DriverBirthDate == null)
                {
                    MessageBox.Show("Could not find the driver's age.", "Adding failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!SettingsClass.IsDriverOledEnough((DateTime)DriverBirthDate, (LicenseClass)cbLicenseClass.SelectedIndex + 1))
                {
                    MessageBox.Show("Driver not old enough.","Adding failed.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return; 
                }
                
                if (DVLDApp.MangeLicenses.Add(_ToLicense()))
                {
                    MessageBox.Show("Added successfully.", "success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Adding Failed.", "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            { // Edit //

                if (txtbNotes.Tag.ToString() == txtbNotes.Text) 
                {
                    MessageBox.Show("you did not edit any notes.", "Not Edited.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DVLDApp.MangeLicenses.IsExist(Convert.ToInt32(lblLicenseID.Text)))
                {
                    if(DVLDApp.MangeLicenses.Update(_ToLicense()))
                    {
                        MessageBox.Show("Edited Succeed.", "Operation succeeded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Editing failed.", "Operation failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

        }

    }
}
