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
using General;


/// <summary>
/// the Set-----Mode() functions have to handle Show only, Edit and Add new modes
/// as well as Save. . .Mode() finctions
/// </summary>


namespace MjdDVLD.Application_Forms
{

    public partial class AddEidtApplication : Form
    {

        private bool _ShowOnly = false;

        public AddEidtApplication(int ApplicationIDToEdit = -1, bool ShowOnly = false)
        {
            InitializeComponent();

            _ResetAllListVisiblty();

            _ShowOnly = ShowOnly;

            if (ApplicationIDToEdit != -1)
            { // Edit //
                BusinessTier.Application ApplicationToEdit = DVLDApp.MangeApplications.Find(ApplicationIDToEdit);

                if (ApplicationToEdit != null)
                {
                    _LoadApplicationInfo(ApplicationToEdit);

                    btnPersonID.Enabled = false;
                    cbApplicationType.Enabled = false;

                    
                    if (ApplicationToEdit.ApplicationStatus != ApplicationStatus.New) // Canseled or Complited
                    {
                        cbApplicationStatus.Enabled = false;
                        txtbPaidFees.Enabled = false; 
                    }


                    if (ShowOnly)
                    {
                        cbApplicationStatus.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Text = "Ok";
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
            { // Add New //
                cbApplicationStatus.SelectedIndex = 1;
                cbApplicationStatus.Enabled = false;
                txtbApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                txtbLastStatusDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
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
            gbRetakeTest.Location = new Point(415, 71);

        }

        private void _LoadApplicationInfo(BusinessTier.Application ApplicationToLoad)
        {
            lblApplicationID.Text = ApplicationToLoad.ApplicationID.ToString();
            btnPersonID.Text = ApplicationToLoad.ApplicantPersonID.ToString();
            cbApplicationType.SelectedIndex = (int)ApplicationToLoad.ApplicationType - 1;
            cbApplicationStatus.SelectedIndex = (int)ApplicationToLoad.ApplicationStatus - 1;

            cbApplicationStatus.Tag = ((int)ApplicationToLoad.ApplicationStatus - 1).ToString(); // the tag will always store the old values // 

            txtbApplicationDate.Text = ApplicationToLoad.ApplicationDate.ToString("dd/MM/yyyy HH:mm");
            txtbLastStatusDate.Text = ApplicationToLoad.LastStatusDate.ToString("dd/MM/yyyy HH:mm");
            lblCreatedByUserID.Text = ApplicationToLoad.CreatedByUserID.ToString();

            txtbPaidFees.Text = ApplicationToLoad.PaidFees.ToString();
            txtbPaidFees.Tag = ApplicationToLoad.PaidFees.ToString();
        }

        private void btnPersonID_Click(object sender, EventArgs e)
        {

        }

        private void btnPersonID_Click_1(object sender, EventArgs e)
        {
            ListAllForm ApplicationsList = new ListAllForm
             (DVLDApp.MangePeople.ListAllPeople(), "People", new ListAllForm.Permissions(), true);

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
            SecoundErrorProvider.Clear();

            decimal NeededFee = SettingsClass.GetInternationalLicenseIssuanceFees
                ((LicenseClass)cbLicenseClassInterntional.SelectedIndex + 1) +
                DVLDApp.MangeApplications.GetApplicationCost((ApplicationType)cbApplicationType.SelectedIndex + 1);

            if (Convert.ToDecimal(txtbPaidFees.Text) < NeededFee)
            {
                MessageBox.Show("Payed fees is not enough\nTotal Application fees is :" + NeededFee.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SecoundErrorProvider.SetError(txtbPaidFees, NeededFee.ToString() + " is needed ");

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
            
            if (lblApplicationID.Text != "??")
            { // Edit //

                if (cbApplicationStatus.SelectedIndex == 1)
                    if (MessageBox.Show("Are you sure you want to Cancele this application out.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                // it will never happen cus the paid fees is unEnabled //
                if (Convert.ToDouble(txtbPaidFees.Tag.ToString()) > Convert.ToDouble(txtbPaidFees.Text))
                {
                    MessageBox.Show("You can not decrees the amont of the mony on payed on the aaplication.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbApplicationStatus.SelectedIndex == 2 && Convert.ToDecimal(txtbPaidFees.Text) < SettingsClass.GetApplicationFees((ApplicationType)(cbApplicationType.SelectedIndex + 1)))
                { // 225 2079 //
                    ErrorProvider.SetError(txtbPaidFees, "You have to pay the application full fees.");
                    return;
                }

                switch (cbApplicationType.SelectedIndex)  
                {
                    case 0: //License Issuance
                        _EditOnIssuingLocalLicenseMode();
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
                        
                        break;

                }
            }
            else
            { // Add New //

                if (cbApplicationStatus.SelectedIndex == 1)
                    if (MessageBox.Show("Are you sure you want to Cancele this application out.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;


                if (string.IsNullOrEmpty(txtbPaidFees.Text))
                {
                    ErrorProvider.SetError(txtbPaidFees, "Invalid value.");
                    return;
                }

                if (btnPersonID.Text == "??")
                {
                    ErrorProvider.SetError(btnPersonID, "You have to choois a person.");
                    return;
                }

                if (cbApplicationStatus.SelectedIndex != 0 && cbApplicationType.SelectedIndex != 1) // just in case //
                {
                    ErrorProvider.SetError(cbApplicationStatus, "If the appliction if new you have to select the \" (New) On Going\".");
                    return;
                }

                if(cbApplicationStatus.SelectedIndex == 2 && Convert.ToDecimal(txtbPaidFees.Text) < SettingsClass.GetApplicationFees((ApplicationType)(cbApplicationType.SelectedIndex + 1)))
                {
                    ErrorProvider.SetError(txtbPaidFees, "You have to pay the application full fees.");
                    return;
                }


                switch (cbApplicationType.SelectedIndex) // the _Save...() functions should close the form if secceded // 
                {
                    case 0: //License Issuance
                        _SaveInLocalLicensesMode();
                        break;
                    case 1: //Retake Test
                        _SaveInRetakeTestMode();
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

            cbApplicationStatus.SelectedIndex = 0;
            cbApplicationStatus.Enabled = false;

            //  Local License //
            gbLocalDrivingLicense.Visible = false;
            //     International License  //
            gbInterNationalLicense.Visible = false;
            gbInterNationalLicense.Enabled = true;
            _LocalLicense = null;
            _DriverID = -1;

            //     Issuing Local License  //
            gbLocalDrivingLicense.Visible = false;
            gbLocalDrivingLicense.Enabled = true;
            NewEyeTest = null;
            NewTheoreticalTest = null;
            NewDrivingTest = null;

            //     Retake Test  //
            gbRetakeTest.Visible = false;
            gbRetakeTest.Enabled = true;
            RetakeTestTest = null;


        }

        // //
        private void _CheckTypeAndSetMode()
        {
            _ResetAllListVisiblty();

            switch (cbApplicationType.SelectedIndex)
            {
                case 0:
                    _SetLicenseIssuanceMode();
                    break;
                case 1:
                    _SetRetakeTestMode();
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
            cbLicenseClassInterntional.SelectedIndex = ((int)ILicense.GetLicenseClass())-1;
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
                    SecoundErrorProvider.SetError(btnPersonID, "You have to select a Person.");
                    //MessageBox.Show("You have to select a Person.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gbInterNationalLicense.Enabled = false;
                    btnSave.Enabled = false;

                    return;
                }


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
                if (DVLDApp.MangeApplications.Add.IssuingInternationalLicenseApplication(_ToApplication(), _ToInternationalLicense()))
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




        // Local License Issuance mode (the sup Application) //

        EyeTest NewEyeTest = null;
        TheoreticalTest NewTheoreticalTest = null;
        DrivingTest NewDrivingTest = null;

        private LocalDrivingLicenseApplication _ToLocalLicenseApplication()
        {
            if (lblLocalDrivingLicenseApplicationID.Text != "??")
            {
                LocalDrivingLicenseApplication LocalApplication = DVLDApp.MangeLocalDrivingLicenseApplications.Find(Convert.ToInt32(lblLocalDrivingLicenseApplicationID.Text));

                LocalApplication.ApplicationID = Convert.ToInt32(lblApplicationID.Text);
                LocalApplication.LicenseClass = (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1);
                LocalApplication.EyeTestID = string.IsNullOrEmpty(btnEyeTestID.Text) ? null :(int?) Convert.ToInt32(btnEyeTestID.Text);
                LocalApplication.DrivingTestID = string.IsNullOrEmpty(btnDrivingTestID.Text) ? null : (int?) Convert.ToInt32(btnDrivingTestID.Text);
                LocalApplication.TheoritecalTestID = string.IsNullOrEmpty(btnTheoreticalTestID.Text) ? null : (int?) Convert.ToInt32(btnTheoreticalTestID.Text);

                return LocalApplication;
            }
            else
                return new LocalDrivingLicenseApplication(-1, (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1), null, null, null);
        }

        private void _SetLicenseIssuanceMode()
        {
            gbLocalDrivingLicense.Visible = true;

            if (lblApplicationID.Text == "??")
            { // Add New //

                cbLicenseClassLocal.SelectedIndex = 0;
                cbLicenseClassLocal.Tag = "0";
            }
            else
            { // Edit //
                LocalDrivingLicenseApplication LocalApplication = DVLDApp.MangeLocalDrivingLicenseApplications.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));

                lblLocalDrivingLicenseApplicationID.Text = LocalApplication.LocalDrivingLicenseApplicationID.ToString();
                
                cbLicenseClassLocal.SelectedIndex = (int)LocalApplication.LicenseClass - 1;
                cbLicenseClassLocal.Enabled = false;

                btnEyeTestID.Text = string.IsNullOrEmpty(LocalApplication.EyeTestID.ToString()) ? "Nan" : LocalApplication.EyeTestID.ToString();
                btnDrivingTestID.Text = string.IsNullOrEmpty(LocalApplication.DrivingTestID.ToString()) ? "Nan" : LocalApplication.DrivingTestID.ToString();
                btnTheoreticalTestID.Text = string.IsNullOrEmpty(LocalApplication.TheoritecalTestID.ToString()) ? "Nan" : LocalApplication.TheoritecalTestID.ToString();
                    // Old Origenal values will be stored in the Tags //
                btnEyeTestID.Tag = string.IsNullOrEmpty(LocalApplication.EyeTestID.ToString()) ? "Nan" : LocalApplication.EyeTestID.ToString();
                btnDrivingTestID.Tag = string.IsNullOrEmpty(LocalApplication.DrivingTestID.ToString()) ? "Nan" : LocalApplication.DrivingTestID.ToString();
                btnTheoreticalTestID.Tag = string.IsNullOrEmpty(LocalApplication.TheoritecalTestID.ToString()) ? "Nan" : LocalApplication.TheoritecalTestID.ToString();

                if (!_ShowOnly)
                    cmsLocalLicenseTestsChoice.Items[1].Visible = true;
            }
        }
        
                // Eye Test //
        private void btnEyeTestID_Click(object sender, EventArgs e)
        {
            if (lblApplicationID.Text != "??")
            { // Edit //

                if (btnEyeTestID.Text == "Nan")
                    return;

                Test_Forms.AddEditTest EditTest = new Test_Forms.AddEditTest(TestType.EyeTest, null, Convert.ToInt32(btnEyeTestID.Text), true);

                if (EditTest != null)
                    EditTest.ShowDialog();
            }
            else
            { // Add New //
                if (btnPersonID.Text != "??")
                {

                    if (MessageBox.Show("Does the applicant have any valid eye test.", "Eye Test", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        DataTable ValidEyeTests = DVLDApp.MangeEyeTests.ListAllPersonEyeTests(Convert.ToInt32(btnPersonID.Text));

                        DateTime AcceptedDate = DateTime.Now.Subtract(new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0));

                        foreach (DataRow Row in ValidEyeTests.Select("AppointmentDate < #" + AcceptedDate.ToShortDateString() + "#"))
                        {
                            ValidEyeTests.Rows.Remove(Row);
                        }

                        ValidEyeTests.AcceptChanges();

                        if (ValidEyeTests.Rows.Count == 0)
                        {
                            MessageBox.Show("The person does not have any valid Eye Test.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        ListAllForm EyeTestsList = new ListAllForm
                            (ValidEyeTests, "Eye Tests List",
                                new ListAllForm.Permissions(ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), true);

                        EyeTestsList.ShowDialog();

                        EyeTestsList.OnOkButtonClick += _ChangeEyeTestID;
                    }
                    else
                    {
                        Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.EyeTest);

                        AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                        AddNewTest.ReturnTestToSave += _SetTheNewEyeTest;
                        if (AddNewTest != null)
                            AddNewTest.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void _SetTheNewEyeTest(object Sender,Test Test)
        {
            NewEyeTest = (EyeTest)Test;
            btnEyeTestID.Text = "OK";
        }
        private void _ChangeEyeTestID(Object Sender, int ID)
        {
            btnEyeTestID.Text = ID.ToString();
        }
        private void _ChooseAnotherEyeTest()
        {
            if (lblApplicationID.Text == "??")
                return;

            DataTable ValidEyeTests = DVLDApp.MangeEyeTests.ListAllPersonEyeTests(Convert.ToInt32(btnPersonID.Text));

            DateTime AcceptedDate = DateTime.Now.Subtract(new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0));

            foreach (DataRow Row in ValidEyeTests.Select("AppointmentDate < #" + AcceptedDate.ToShortDateString() + "#"))
            {
                ValidEyeTests.Rows.Remove(Row);
            }

            ValidEyeTests.AcceptChanges();

            if (ValidEyeTests.Rows.Count == 0)
            {
                MessageBox.Show("The person does not have any valid Eye Test.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm EyeTestsList = new ListAllForm
                (ValidEyeTests, "Eye Tests",
                    new ListAllForm.Permissions(ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), true);

            EyeTestsList.ShowDialog();

            EyeTestsList.OnOkButtonClick += _ChangeEyeTestID;
        }

        // Driving Test //
        private void btnDrivingTestID_Click(object sender, EventArgs e)
        {
            if (lblApplicationID.Text != "??")
            { // Edit //

                if (btnDrivingTestID.Text == "Nan")
                    return;

                Test_Forms.AddEditTest EditTest = new Test_Forms.AddEditTest(TestType.DrivingTest, null, Convert.ToInt32(btnDrivingTestID.Text), true);

                if (EditTest != null)
                    EditTest.ShowDialog();
            }
            else
            { // Add New //
                if (btnPersonID.Text != "??")
                {
                    Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.DrivingTest);

                    AddNewTest.TestClass = (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1);
                    AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                    AddNewTest.ReturnTestToSave += _SetTheNewDrivingTest;

                    if (AddNewTest != null)
                        AddNewTest.ShowDialog();

                }
                else
                {
                    MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void _SetTheNewDrivingTest(object Sender, Test Test)
        {
            NewDrivingTest = (DrivingTest)Test;
            btnDrivingTestID.Text = "OK";
        }
        private void _ChangeDrivingTestID(Object Sender, int ID)
        {
            btnDrivingTestID.Text = ID.ToString();
        }
        private void _ChooseAnotherDrivingTesr()
        {
            if (lblApplicationID.Text == "??")
                return;

            DataTable ValidDrivingTests = DVLDApp.MangeDrivingTests.ListAllPersonDrivingTests(Convert.ToInt32(btnPersonID.Text));

            DateTime AcceptedDate = DateTime.Now.Subtract(new TimeSpan(SettingsClass.DrivingTestExpirationPeriod, 0, 0, 0));

            foreach (DataRow Row in ValidDrivingTests.Select("AppointmentDate < #" + AcceptedDate.ToShortDateString() + "#"))
            {
                ValidDrivingTests.Rows.Remove(Row);
            }

            ValidDrivingTests.AcceptChanges();

            if (ValidDrivingTests.Rows.Count == 0)
            {
                MessageBox.Show("The person does not have any valid Driving Test.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm EyeTestsList = new ListAllForm
                (ValidDrivingTests, "Driving Tests",
                    new ListAllForm.Permissions(ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), true);

            EyeTestsList.ShowDialog();

            EyeTestsList.OnOkButtonClick += _ChangeDrivingTestID;
        }

        // Theoretical Test //
        private void btnTheoreticalTestID_Click(object sender, EventArgs e)
        {
            if (lblApplicationID.Text != "??")
            { // Edit //

                if (btnTheoreticalTestID.Text == "Nan")
                    return;

                Test_Forms.AddEditTest EditTest = new Test_Forms.AddEditTest(TestType.TheoreticalTest,null,Convert.ToInt32(btnTheoreticalTestID.Text),true);

                if (EditTest != null)
                    EditTest.ShowDialog();
            }
            else
            { // Add New //
                if (btnPersonID.Text != "??")
                {
                    Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.TheoreticalTest);

                    AddNewTest.TestClass = (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1);
                    AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                    AddNewTest.ReturnTestToSave += _SetTheNewTheoreticalTest;

                    if (AddNewTest != null)
                        AddNewTest.ShowDialog();

                }
                else
                {
                    MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void _SetTheNewTheoreticalTest(object Sender, Test Test)
        {
            NewTheoreticalTest = (TheoreticalTest)Test;
            btnTheoreticalTestID.Text = "OK";
        }
        private void _ChangeTheoreticalTestID(Object Sender, int ID)
        {
            btnTheoreticalTestID.Text = ID.ToString();
        }
        private void _ChooseAnotherTheoreticalTest()
        {
            if (lblApplicationID.Text == "??")
                return;

            DataTable ValidTheoreticalTests = DVLDApp.MangeTheoreticalTests.ListAllPersonTheoreticalTests(Convert.ToInt32(btnPersonID.Text));

            DateTime AcceptedDate = DateTime.Now.Subtract(new TimeSpan(SettingsClass.TheoreticalTestExpirationPeriod, 0, 0, 0));

            foreach (DataRow Row in ValidTheoreticalTests.Select("AppointmentDate < #" + AcceptedDate.ToShortDateString() + "#"))
            {
                ValidTheoreticalTests.Rows.Remove(Row);
            }

            ValidTheoreticalTests.AcceptChanges();

            if (ValidTheoreticalTests.Rows.Count == 0)
            {
                MessageBox.Show("The person does not have any valid Theoretical Tests.", "Nan.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListAllForm EyeTestsList = new ListAllForm
                (ValidTheoreticalTests, "Theoretical Tests",
                    new ListAllForm.Permissions(ListAllForm.Permissions.Permission.ShowCard, ListAllForm.Permissions.Permission.Refresh), true);

            EyeTestsList.ShowDialog();

            EyeTestsList.OnOkButtonClick += _ChangeTheoreticalTestID;
        }



        private void cbLicenseClassLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnDrivingTestID.Text != "??" || btnEyeTestID.Text != "??" || btnTheoreticalTestID.Text != "??")
            {
                if(MessageBox.Show("Are you sure you want to change the License class, you will need to renter the information.", "Attention",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                {
                    cbLicenseClassLocal.SelectedIndex = Convert.ToInt32(cbLicenseClassLocal.Tag);
                    return;
                }
            }

            cbLicenseClassLocal.Tag = cbLicenseClassLocal.SelectedIndex.ToString();

            NewEyeTest = null;
            NewTheoreticalTest = null;
            NewDrivingTest = null;

            btnEyeTestID.Text = "??";
            btnTheoreticalTestID.Text = "??";
            btnDrivingTestID.Text = "??";
        }
       
        private void _SaveInLocalLicensesMode()
        {
            SecoundErrorProvider.Clear();
            // Theoretical test //

            bool Failed = false;

            if (btnTheoreticalTestID.Text == "??")
            {
                SecoundErrorProvider.SetError(btnTheoreticalTestID, "You have to enter a valid Theoretical Test.");
                Failed = true;
            }

            else if (btnTheoreticalTestID.Text == "OK")
            {
                if (NewTheoreticalTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0))
                {
                    MessageBox.Show("the Theoretical test should be in the future.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Failed = true;
                }

                if (NewTheoreticalTest.TestClass != (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1))
                {
                    MessageBox.Show("Unvaild Theoretical Test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnTheoreticalTestID.Text = "??";
                    NewTheoreticalTest = null;
                    Failed = true;
                }
            }

            // Driving test //

            if (btnDrivingTestID.Text == "??")
            {
                SecoundErrorProvider.SetError(btnDrivingTestID, "You have to enter a valid Driving Test.");
                Failed = true;
            }
            else if (btnDrivingTestID.Text == "OK")
            {
                if (NewDrivingTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0))
                {
                    MessageBox.Show("the Driving test should be in the future.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Failed = true;
                }

                if (NewDrivingTest.TestClass != (LicenseClass)(cbLicenseClassLocal.SelectedIndex + 1))
                {
                    MessageBox.Show("Unvaild Driving Test.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDrivingTestID.Text = "??";
                    NewDrivingTest = null;
                    Failed = true;
                }
            }


            // Eye test //

            if (btnEyeTestID.Text == "??")
            {
                SecoundErrorProvider.SetError(btnEyeTestID, "You have to enter a valid Eye Test.");
                Failed = true;
            }
            else if (btnEyeTestID.Text == "OK")
            {
                if (NewEyeTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0))
                {
                    MessageBox.Show("the Eye test should be in the future.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Failed = true;
                }
            }
            else
            {
                if (!DVLDApp.MangeEyeTests.IsExist(Convert.ToInt32(btnEyeTestID.Text)))
                {
                    MessageBox.Show("Could not find the Eye test", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Failed = true;
                }

                EyeTest FindedEyeTest = DVLDApp.MangeEyeTests.Find(Convert.ToInt32(btnEyeTestID.Text));

                if (FindedEyeTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0) && FindedEyeTest.TestResult != true)
                {
                    MessageBox.Show("the selected Eye test resulte should be Passed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Failed = true;
                }

            }

            if (Failed)
                return;



            if (DVLDApp.MangeApplications.Add.IssuingLocalLicenseApplication(_ToApplication(), _ToLocalLicenseApplication(), NewEyeTest, NewDrivingTest, NewTheoreticalTest) != -1) 
            {
                MessageBox.Show("Application Added successfully.", "Operation succeeded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Application Adding failed.", "Operation failed.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     
        }

        private void _EditOnIssuingLocalLicenseMode()
        {

            if (btnEyeTestID.Text == btnEyeTestID.Tag.ToString() && btnDrivingTestID.Text == btnDrivingTestID.Tag.ToString() && btnTheoreticalTestID.Text == btnTheoreticalTestID.Tag.ToString() && txtbPaidFees.Text == txtbPaidFees.Tag.ToString() && cbApplicationStatus.SelectedIndex.ToString() == cbApplicationStatus.Tag.ToString()) 
            {
                MessageBox.Show("You did not change any thing.", "Information.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if(cbApplicationStatus.SelectedIndex == 2)
                {
                    if (DVLDApp.MangeEyeTests.IsExist(Convert.ToInt32(btnEyeTestID.Text)))
                    {
                        if (!DVLDApp.MangeEyeTests.IsPassed(Convert.ToInt32(btnEyeTestID.Text)))
                        {
                            MessageBox.Show("Eye Test has to be passed to complete the Application.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Eye Test Does Not Exist.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (DVLDApp.MangeDrivingTests.IsExist(Convert.ToInt32(btnDrivingTestID.Text)))
                    {
                        if (!DVLDApp.MangeDrivingTests.IsPassed(Convert.ToInt32(btnDrivingTestID.Text)))
                        {
                            MessageBox.Show("Driving Test has to be passed to complete the Application.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Driving Test Does Not Exist.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (DVLDApp.MangeTheoreticalTests.IsExist(Convert.ToInt32(btnTheoreticalTestID.Text)))
                    {
                        if (!DVLDApp.MangeTheoreticalTests.IsPassed(Convert.ToInt32(btnTheoreticalTestID.Text)))
                        {
                            MessageBox.Show("Theoretical Test has to be passed to complete the Application.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Theoretical Test Does Not Exist.", "Abort.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (DVLDApp.EditLocalLicenseApplication(_ToApplication(), _ToLocalLicenseApplication()))
                    {
                        MessageBox.Show("Application Edited successfully.", "Operation succeeded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Application Adding failed.", "Operation failed.", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
        }

        private void choiseElseOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            ContextMenuStrip MenuStrip = (ContextMenuStrip)Item.Owner;
            
           switch (MenuStrip.SourceControl.Name)
            {
                case "btnEyeTestID":
                    _ChooseAnotherEyeTest();
                    break;

                case "btnDrivingTestID":
                    _ChooseAnotherDrivingTesr();
                    break;

                case "btnTheoreticalTestID":
                    _ChooseAnotherTheoreticalTest();
                    break;
            }

        }

        // Retake Test mode //

        Test RetakeTestTest;

        private void _SetRetakeTestMode()
        {
            cbApplicationStatus.SelectedIndex = 2;
            cbApplicationStatus.Enabled = false;
            gbRetakeTest.Visible = true;

            if (lblApplicationID.Text == "??")
            {
                cbRetakeTestTestType.SelectedIndex = 0;
            }
            else
            {
                cbRetakeTestTestType.Enabled = false;

                _LoadRetakeTestInfo();
            }
        }
        private void _LoadRetakeTestInfo()
        {
            Test FindedTest = null;

            FindedTest =  DVLDApp.MangeEyeTests.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));

            if (FindedTest != null)
            {
                cbRetakeTestTestType.SelectedIndex = 2;
                RetakeTestTest = FindedTest;
            }
            else
            {
                FindedTest = DVLDApp.MangeDrivingTests.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));
                
                if (FindedTest != null)
                {
                    cbRetakeTestTestType.SelectedIndex = 1;
                    RetakeTestTest = FindedTest;
                }
                else
                {
                    FindedTest = DVLDApp.MangeTheoreticalTests.FindByApplicationID(Convert.ToInt32(lblApplicationID.Text));

                    if (FindedTest != null)
                    {
                        cbRetakeTestTestType.SelectedIndex = 0;
                        RetakeTestTest = FindedTest;

                    }
                    else
                    {
                        MessageBox.Show("Error entered Incorect Data.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gbRetakeTest.Enabled = false;
                    }
                }
            }

            btnRetakeTestTestID.Text = FindedTest.TestID.ToString();
            

        }
        private void btnRetakeTest_Click(object sender, EventArgs e)
        {
            if (lblApplicationID.Text == "??")
            {
                switch (cbRetakeTestTestType.SelectedIndex)
                {
                    case 2:
                        // Add New Eye Test //
                        if (btnPersonID.Text != "??")
                        {

                            Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.EyeTest);

                            AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                            AddNewTest.ReturnTestToSave += _SetRetackTestTest;
                            if (AddNewTest != null)
                                AddNewTest.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 1:
                        if (btnPersonID.Text != "??")
                        {
                            Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.DrivingTest);

                            AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                            AddNewTest.ReturnTestToSave += _SetRetackTestTest;

                            if (AddNewTest != null)
                                AddNewTest.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    default: // 0
                        if (btnPersonID.Text != "??")
                        {
                            Test_Forms.AddEditTest AddNewTest = new Test_Forms.AddEditTest(TestType.TheoreticalTest);

                            AddNewTest.PersonID = Convert.ToInt32(btnPersonID.Text);

                            AddNewTest.ReturnTestToSave += _SetRetackTestTest;

                            if (AddNewTest != null)
                                AddNewTest.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("You have To Select a Person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                }
            }
            else
            { 
                Test_Forms.AddEditTest EditTest = new Test_Forms.AddEditTest((TestType)cbRetakeTestTestType.SelectedIndex, null, Convert.ToInt32(btnRetakeTestTestID.Text), true);

                if (EditTest != null)
                    EditTest.ShowDialog();
            }
        }

        private void _SetRetackTestTest(object sender, Test Test)
        {
            RetakeTestTest = Test;
            btnRetakeTestTestID.Text = "OK";
        }

        private void _SaveInRetakeTestMode()
        {
            SecoundErrorProvider.Clear();

            if(btnRetakeTestTestID.Text == "??")
            {
                SecoundErrorProvider.SetError(btnRetakeTestTestID,"You have to set the tests values");
                return;
            }    
            
            if(DVLDApp.AddRetakeTestApplication(_ToApplication(), (TestType)(cbRetakeTestTestType.SelectedIndex), RetakeTestTest))
            {
                MessageBox.Show("Application Added Successfully","Success.",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            { 
                MessageBox.Show("Application Adding failed", "failed.", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    
    }
}
