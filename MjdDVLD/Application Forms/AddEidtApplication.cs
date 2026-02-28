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
using Generale;


/// <summary>
/// the Set-----Mode() functions have to handle Show only, Edit and Add new modes
/// as well as Save. . .Mode() finctions
/// </summary>


namespace MjdDVLD.Application_Forms
{
    public partial class AddEidtApplication : Form
    {
        private bool _ShowOnly;

        public AddEidtApplication(int ApplicationIDToEdit = -1, bool ShowOnly = false)
        {
            InitializeComponent();

            _ResetAllListVisiblty();


            _ShowOnly = ShowOnly;

            if (ApplicationIDToEdit != -1)
            {
                BusinessTier.Application ApplicationToEdit = DVLDApp.MangeApplications.Find(ApplicationIDToEdit);

                if (ApplicationToEdit != null)
                {
                    _LoadApplicationInfo(ApplicationToEdit);

                    btnPersonID.Enabled = false;
                    cbApplicationType.Enabled = false;

                    // The tag will hold the oregenal statuse //
                    cbApplicationStatus.Tag = (Convert.ToInt32(ApplicationToEdit.ApplicationStatus)-1).ToString();

                    if (ApplicationToEdit.ApplicationStatus != ApplicationStatus.New) // Canseled or Complited
                    {
                        cbApplicationStatus.Enabled = false;
                        txtbPaidFees.Enabled = false; 
                    }


                    if (ShowOnly)
                    {
                        btnSave.Visible = false;
                        btnCancel.Text = "Ok";
                        cbApplicationStatus.Enabled = false;
                        txtbPaidFees.Enabled = false;
                    }
                    _CheckTypeAndSetMode();
                }
                else
                {
                    MessageBox.Show("This Application is not found.", "Not found.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }


            }
            else
            {
                txtbApplicationDate.Text = DateTime.Now.ToShortDateString();
                txtbLastStatusDate.Text = DateTime.Now.ToShortDateString();
                cbApplicationType.SelectedIndex = 0;
                cbApplicationStatus.SelectedIndex = 0;
                lblCreatedByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();
            }

        }

        private void AddEidtApplication_Paint(object sender, PaintEventArgs e)
        {

            Color PenColor = Color.Gray;

            Pen Pen = new Pen(PenColor);
            Pen.Width = 2;

            e.Graphics.DrawLine(Pen, new Point(407, 80), new Point(407, 380));
            e.Graphics.DrawLine(Pen, new Point(411, 80), new Point(411, 380));
        }

        private void AddEidtApplication_Load(object sender, EventArgs e)
        {
            this.Size = new Size(790, 480);
            gbLocalDrivingLicense.Location = new Point(415, 71);

        }

        private void _LoadApplicationInfo(BusinessTier.Application ApplicationToLoad)
        {
            lblApplicationID.Text = ApplicationToLoad.ApplicationID.ToString();
            btnPersonID.Text = ApplicationToLoad.ApplicantPersonID.ToString();
            cbApplicationType.SelectedIndex = (int)ApplicationToLoad.ApplicationTypeID - 1;
            cbApplicationStatus.SelectedIndex = (int)ApplicationToLoad.ApplicationStatus - 1;
            txtbApplicationDate.Text = ApplicationToLoad.ApplicationDate.ToShortDateString();
            txtbLastStatusDate.Text = ApplicationToLoad.LastStatusDate.ToShortDateString();
            lblCreatedByUserID.Text = ApplicationToLoad.CreatedByUserID.ToString();
            txtbPaidFees.Text = ApplicationToLoad.PaidFees.ToString();
        }

        private void btnPersonID_Click(object sender, EventArgs e)
        {

        }

        private void btnPersonID_Click_1(object sender, EventArgs e)
        {
            ListAllForm ApplicationsList = new ListAllForm
             (DVLDApp.MangePeople.ListAllPeople(), "Applications", new ListAllForm.Permissions(), true);

            ApplicationsList.OnOkButtonClick += _ChangePersonID;

            ApplicationsList.ShowDialog();

            if (btnPersonID.Text != (string)btnPersonID.Tag) // the tag holds the old value
            {
                btnPersonID.Tag = btnPersonID.Text;
                _CheckTypeAndSetMode();
            }
        }

        private void _ChangePersonID(object sender, int ID)
        {
            btnPersonID.Text = ID.ToString();
        }

        private void txtbPaidFees_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPaidFees.Text))
            {
                txtbPaidFees.Text = "";
                return;
            }


            string AllowedChars = "0123456789.";

            // join if in 0-9 or the dote (.)
            txtbPaidFees.Text = String.Join("", txtbPaidFees.Text.Where(c => AllowedChars.Contains(c)));

            if (txtbPaidFees.Text.Count(n => n == '.') > 1)
            {
                txtbPaidFees.Text = txtbPaidFees.Text.Remove(txtbPaidFees.Text.LastIndexOf('.'), 1);
            }
        }

        private bool _ValidateEditApplication()
        {
            decimal NeededFee = SettingsClass.GetInternationalLicensIssuanceFees
                ((LicenseClass)cbLicenseClassInterntional.SelectedIndex + 1) +
                DVLDApp.MangeApplications.GetApplicationFees((ApplicationType)cbApplicationType.SelectedIndex + 1);

            if (Convert.ToDecimal(txtbPaidFees.Text) < NeededFee)
            {
                MessageBox.Show("Payed fees is not enough\nTotal Application fees is :" + NeededFee.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorProvider.SetError(txtbPaidFees, NeededFee.ToString() + " is needed ");

                return false;
            }

            if (Convert.ToInt32 (cbApplicationStatus.Tag) != 0)
            {
                MessageBox.Show("You can only Edit Payed Fees when that status is \"New (On Going)\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToInt32(cbApplicationStatus.Tag) != 0 && cbApplicationStatus.SelectedIndex != Convert.ToInt32(cbApplicationStatus.Tag))
            {
                MessageBox.Show("You can Not edit the status of the application when it is finshed or canceled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // not validated you have to validate it with _ValidateEditApplicationFee() function
        private bool _EditApplication()
        {
            if (cbApplicationStatus.SelectedIndex == Convert.ToInt32(cbApplicationStatus.Tag))
                return DVLDApp.MangeApplications.EditPaidFees(Convert.ToInt32(lblApplicationID.Text), Convert.ToDecimal(txtbPaidFees.Text));
            else 
                return DVLDApp.MangeApplications.Update(_ToApplication());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorProvider.Clear();

            if(string.IsNullOrEmpty(txtbPaidFees.Text))
            {
                ErrorProvider.SetError(txtbPaidFees, "This value can not be empty.");
                return;
            }

            if(btnPersonID.Text == "??")
            {
                ErrorProvider.SetError(btnPersonID, "You have to choois a person.");
                return;
            }


            switch(cbApplicationType.SelectedIndex) // the _Save...() functions should close the form if secceded // 
            {
                case 0: //License Issuance
                    break;
                case 1: //Retake Test
                    break;
                case 2: //Renew Driving License
                    break;
                case 3: //Missing Replacement
                    break;
                case 4: //Damaged Replacement
                    break;
                case 5: //Release License
                    break;
                case 6: //Issuing International License
                    _SaveInInternationalLicensesMode();
                    break;

            }
                       
        }
        private BusinessTier.Application _ToApplication()
        {
            if(lblApplicationID.Text != "??")
            {
                BusinessTier.Application ApplicationToLoad = DVLDApp.MangeApplications.Find(Convert.ToInt32(lblApplicationID.Text));

                if (ApplicationToLoad.ApplicationStatus != (ApplicationStatus)((byte)cbApplicationStatus.SelectedIndex + 1))
                    ApplicationToLoad.LastStatusDate = DateTime.Now;

                ApplicationToLoad.ApplicationStatus = (ApplicationStatus)((byte)cbApplicationStatus.SelectedIndex + 1);
                ApplicationToLoad.PaidFees = Convert.ToDecimal(txtbPaidFees.Text);

                return ApplicationToLoad;
            }

            return new BusinessTier.Application(
                Convert.ToInt32(btnPersonID.Text)
                , DateTime.Now
                , (ApplicationType)cbApplicationType.SelectedIndex + 1
                , (ApplicationStatus)((byte)cbApplicationStatus.SelectedIndex + 1)
                , DateTime.Now
                , Convert.ToDecimal(txtbPaidFees.Text)
                , Convert.ToInt32(lblCreatedByUserID.Text));

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetAllListVisiblty()
        {
            SecoundErrorProvider.Clear();
            btnSave.Enabled = true;


            //     International License  //
            gbInterNationalLicense.Visible = false;
            gbInterNationalLicense.Enabled = true;
            _LocalLicense = null;
            _DriverID = -1;
            //  Local License //
            gbLocalDrivingLicense.Visible = false;

        }

        // //
        private void _CheckTypeAndSetMode()
        {
            _ResetAllListVisiblty();

            switch (cbApplicationType.SelectedIndex)
            {
                case 1:
                    break;
                case 6:
                    _SetInternationalLicensesMode();
                    break;
            }

        }
        private void cbApplicationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CheckTypeAndSetMode();
        }


        //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //
        //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //
        //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //    //


        // InternationalLicensesMode // 

        private BusinessTier.License _LocalLicense;
        private int _DriverID = -1;

            // the two functions will set the variables if Existed //
        private bool _CheckLocalLicenseExistent()
        {
            SecoundErrorProvider.Clear();
            if (_DriverID == -1)
                return false;

            _LocalLicense = DVLDApp.MangeLicenses.FindLastActiveLicense(_DriverID, (LicenseClass)cbLicenseClassInterntional.SelectedIndex);

            if (_LocalLicense == null)
            {
                SecoundErrorProvider.SetError(cbLicenseClassInterntional, "This person does not have a Local License of this type.");
                txtbExpirationDate.Text = "";
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                txtbExpirationDate.Text =
                  DVLDApp.MangeInternationalLicenses.GetInternationalLicenseExpiretionDate(_LocalLicense.LicenseID).ToShortDateString();
                btnSave.Enabled = true;
                return true;
            }
        }
        private bool _CheckDriverExistent()
        {
            object ID = DVLDApp.MangeDrivers.GetDriverID(Convert.ToInt32(btnPersonID.Text));

            if (ID == null)
            {
                _DriverID = -1;
                MessageBox.Show("This PersonID Does Not have a Driver to his name in the system.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gbInterNationalLicense.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }

            _DriverID = (int)ID;
            return true;

        }
        

        private void _LoadInternationalInfo(InternationalLicense ILicense)
        {
            lblInternationalLicenseID.Text = ILicense.InternationalLicenseID.ToString();
            cbLicenseClassInterntional.SelectedIndex = (int)ILicense.LicenseClass-1;
            txtbIssuedDate.Text = ILicense.IssueDate.ToShortDateString();
            txtbExpirationDate.Text = ILicense.ExpirationDate.ToShortDateString();
            cbIsActive.Checked = ILicense.IsActive;
        }
        private void _SetInternationalLicensesMode()
        {
            gbInterNationalLicense.Visible = true;

            if (lblApplicationID.Text == "??")
            {
                cbApplicationStatus.SelectedIndex = 2;
                cbApplicationStatus.Enabled = false;

                cbLicenseClassInterntional.SelectedIndex = 0;
                txtbIssuedDate.Text = DateTime.Now.ToShortDateString();
                
                if(btnPersonID.Text == "??")
                {
                    MessageBox.Show("You have to select a Person.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gbInterNationalLicense.Enabled = false;
                    btnSave.Enabled = false;

                    return;
                }

                object ID = DVLDApp.MangeDrivers.GetDriverID(Convert.ToInt32(btnPersonID.Text));

                if (!_CheckDriverExistent())
                    return;

                if (!_CheckLocalLicenseExistent())
                    return;

            }
            else
            {
                // can not Edit International License from here // you have to go to its section in the UI //
                cbLicenseClassInterntional.Enabled = false;
                cbIsActive.Enabled = false;
            
                InternationalLicense ILicense = DVLDApp.MangeInternationalLicenses.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));

                if (ILicense != null)
                    _LoadInternationalInfo(ILicense);
                else
                    MessageBox.Show("Could Not Find The International Licens.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void cbLicenseClassInterntional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblApplicationID.Text != "??")
                _CheckLocalLicenseExistent();
        }
        private InternationalLicense _ToInternationalLicense()
        {
            if(lblInternationalLicenseID.Text == "??")
            {
                return  new InternationalLicense(-1, _DriverID, _LocalLicense.LicenseID, DateTime.Now, DVLDApp.MangeInternationalLicenses.GetInternationalLicenseExpiretionDate(_LocalLicense.LicenseID), true, DVLDApp.LogedInUser.UserID);
            }
            else
            {
                InternationalLicense License = DVLDApp.MangeInternationalLicenses.Find(Convert.ToInt32 (lblInternationalLicenseID.Text));
                License.IsActive = cbIsActive.Checked; // Only the Active status could be Edited
                return License;
            }

        }
        private void _SaveInInternationalLicensesMode()
        {
            if (lblApplicationID.Text == "??")
            {
                if (DVLDApp.AddIssuingInternationalLicenseApplication(_ToApplication(), _ToInternationalLicense(), (LicenseClass)cbLicenseClassInterntional.SelectedIndex))
                {
                    MessageBox.Show("Added Sccessfully.", "Success.", MessageBoxButtons.OK,MessageBoxIcon.Information); 
                    this.Close();
                }
                else
                { MessageBox.Show("Adding Failed.","Failed.",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            }
            else
            {
                if(_ValidateEditApplication())
                {
                    if (_EditApplication())
                    {
                        MessageBox.Show("Edited Successfully.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Editing faild.", "Failed.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }


        

        // Local License  sup Application //

        private void btnEyeTestID_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Does the applicant have any valid eye test.","Eye Test",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                ListAllForm EyeTestsList = new ListAllForm
                    ( DVLDApp.MangeEyeTests.ListAllEyeTests(),"Eye Tests List",
                    new ListAllForm.Permissions(ListAllForm.Permissions.Permission.ShowCard,ListAllForm.Permissions.Permission.Refresh),true);

                EyeTestsList.OnOkButtonClick += _ChangeEyeTestID;    
            }
            else
            {
                //EyeTest
            }

             
        }

        private void _ChangeEyeTestID(Object Sender, int ID)
        {
            btnEyeTestID.Text = ID.ToString();
        }







        /*
            public int ApplicantPersonID;
            public int ApplicationID;
            public DateTime ApplicationDate;
            public DateTime LastStatusDate;
            public int CreatedByUserID;
            public ApplicationType ApplicationTypeID;
            public byte ApplicationStatus;


            public decimal PaidFees;
        */

    }
}
