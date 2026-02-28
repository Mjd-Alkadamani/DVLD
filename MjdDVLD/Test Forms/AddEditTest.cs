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

namespace MjdDVLD.Test_Forms
{
    /*
     * This Form has four modes: Add - Add without saving - Edit - Show only
     * For each mode you have to send appropriate parameters to the constructor as shown
     * Add with out saving: (TestType)
     * Add: (TestType, CreatedAcordingToApplication)
     * Edit: (TestType, null, TestIDToEdit)
     * Show only: (TestType, null, TestIDToEdit, true)
     */

    
    public partial class AddEditTest : Form
    {

        public delegate void GeneralReturnEvent(object sender, Test Test);
        public event GeneralReturnEvent ReturnTestToSave;

        private bool _GetOut = false;

        // returning The EyeTest to out size the class without saving it. (works only in add new mode)
        private bool _ReturnTheTestWithOutSavingIt = false;
        private int _CreatedAcordingToApplication = -1;
        private TestType _TestType;

        public LicenseClass? TestClass = null; // you can set the value of the TestClass before opening the form (if) you where in return the test withuot saving it //
        public int? PersonID = null; // you can set the value of the PersonID before opening the form (if) you where in return the test withuot saving it //


        // You have to send the "CreatedAcordingToApplication" if you want to add the test to the db from this form //
        // warning sending value to both "CreatedAcordingToApplication" and "EyeTestIDToEdit" would cuse the form to close //
        public AddEditTest(TestType Type, int? CreatedAcordingToApplication = null, int TestIDToEdit = -1, bool ShowOnly = false)
        {
            if (CreatedAcordingToApplication != null && TestIDToEdit != -1)
            {
                MessageBox.Show("Incorrect parameters have been sent to the form", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _GetOut = true;
            }

            InitializeComponent();

            _ReturnTheTestWithOutSavingIt = CreatedAcordingToApplication == null && TestIDToEdit == -1;
            _CreatedAcordingToApplication = CreatedAcordingToApplication == null ? -1 : (int)CreatedAcordingToApplication;
            _TestType = Type;

            cbTestClass.SelectedIndex = 0;

            switch (Type)
            {
                case TestType.EyeTest:
                    lblTestType.Text = "Eye Test";
                    cbTestClass.Items.Add("Neutral");
                    cbTestClass.SelectedItem = "Neutral";
                    cbTestClass.Enabled = false;
                    break;
                case TestType.DrivingTest:
                    lblTestType.Text = "Driving Test";
                    break;
                default: // TestType.TheoreticalTest
                    lblTestType.Text = "Theoretical Test";

                    if (TestClass != null && _ReturnTheTestWithOutSavingIt)
                    {
                        cbTestClass.SelectedIndex = (int)TestClass;
                        cbTestClass.Enabled = false;
                    }

                    break;
            }

            if (CreatedAcordingToApplication != null || _ReturnTheTestWithOutSavingIt)
            {
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                rbNotTacken.Checked = true;


                if (!_ReturnTheTestWithOutSavingIt)
                {
                    if (CreatedAcordingToApplication < 0)
                    {
                        MessageBox.Show("Could Not find the Application ID to attach the test to it.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = false;
                    }
                    if (!DVLDApp.MangeApplications.IsExist((int)CreatedAcordingToApplication))
                    {
                        MessageBox.Show("Cuold Not find the Application ID to attach the test to it.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Enabled = false;
                    }

                    switch (_TestType)
                    {
                        case TestType.EyeTest:
                            if (!DVLDApp.MangeEyeTests.CouldAttachEyeTestToApplicationID(TestIDToEdit))
                            {
                                MessageBox.Show("Error, Could not Attach Eye test to this Application.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Enabled = false;
                            }
                            break;
                        case TestType.DrivingTest:
                            if (!DVLDApp.MangeDrivingTests.CouldAttachDrivingTestToApplicationID(TestIDToEdit, (LicenseClass)(cbTestClass.SelectedIndex + 1)))
                            {
                                MessageBox.Show("Error, Could not Attach the Driving test to this Application.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Enabled = false;
                            }
                            break;
                        default: // TestType.TheoreticalTest:
                            if (!DVLDApp.MangeTheoreticalTests.CouldAttachTheoreticalTestToApplicationID(TestIDToEdit, (LicenseClass)(cbTestClass.SelectedIndex + 1)))
                            {
                                MessageBox.Show("Error, Could not Attach the Theoretical test to this Application.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Enabled = false;
                            }
                            break;
                    }

                    lblCreatedAcordingToApplication.Text = ((int)CreatedAcordingToApplication).ToString();
                    lblPersonID.Text = DVLDApp.MangeApplications.GetPersonID((int)CreatedAcordingToApplication).ToString();
                }


                // Add new

                lblAppointmentMadeByUserID.Text = DVLDApp.LogedInUser.UserID.ToString();
                mtbAppointmentDate.Text = (DateTime.Now + new TimeSpan(24, 0, 0)).ToString(); // it could be changed by the User 

            }
            else
            {
                if (!_DoesTheTestToEditExist(TestIDToEdit))
                {
                    MessageBox.Show("Error, Counld not Find The test to Edit.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _GetOut = true;
                }
                else
                {

                    cbTestClass.Enabled = false;

                    _LoadTestInfo(TestIDToEdit);

                    if (ShowOnly)
                    {
                        cbTestClass.Enabled = false;
                        mtbAppointmentDate.Enabled = false;
                        txtbPaidFees.Enabled = false;
                        rbFail.Enabled = false;
                        rbPass.Enabled = false;
                        rbNotTacken.Enabled = false;
                        txtbNotes.Enabled = false;
                        btnCancel.Text = "Ok";
                        btnSave.Visible = false;
                    }

                }
            }
        }

        public new DialogResult  ShowDialog()
        {
            if (_ReturnTheTestWithOutSavingIt)
            { 
                if (TestClass != null)
                {
                    cbTestClass.SelectedIndex = (int)(TestClass - 1);
                    cbTestClass.Enabled = false;
                }

                if (PersonID != null)
                    lblPersonID.Text = ((int)PersonID).ToString();
            }

            if (!_GetOut)
                return base.ShowDialog();

            return DialogResult.Abort;
        }

        private bool _DoesTheTestToEditExist(int TestID)
        {
            switch(_TestType)
            {
                case TestType.EyeTest:
                    return DVLDApp.MangeEyeTests.IsExist(TestID);

                case TestType.DrivingTest:
                    return DVLDApp.MangeDrivingTests.IsExist(TestID);                    
                
                default: // TestType.TheoreticalTest:
                    return DVLDApp.MangeTheoreticalTests.IsExist(TestID);                    
            }    
        }

        private void _LoadTestInfo(int TestID)
        {
            DateTime ?AppointmentDate = null;

            if (_TestType == TestType.EyeTest)
            {
                EyeTest Eye = DVLDApp.MangeEyeTests.Find(TestID);
                lblTestID.Text = Eye.TestID.ToString();
                lblCreatedAcordingToApplication.Text = Eye.TestApplicationID.ToString();
                lblPersonID.Text = Eye.PersonID.ToString();
                lblAppointmentMadeByUserID.Text = Eye.AppointmentMadeByUserID.ToString();
                mtbAppointmentDate.Text = Eye.AppointmentDate.ToString("dd/MM/yyyy HH:mm");
                txtbPaidFees.Text = Eye.PaidFees.ToString();

                AppointmentDate = Eye.AppointmentDate;

                lblResultAddedByUser.Text = Eye.ResultAddedByUserID == null ? (string)"??" : Eye.ResultAddedByUserID.ToString();
                if (string.IsNullOrEmpty(Eye.Notes))
                    txtbNotes.Text = "";
                else
                    txtbNotes.Text = Eye.Notes;

                switch (Eye.TestResult)
                {
                    case null:
                        rbNotTacken.Checked = true;
                        break;
                    case true:
                        rbPass.Checked = true;
                        break;
                    case false:
                        rbFail.Checked = true;
                        break;
                }
            }
            else if (_TestType == TestType.DrivingTest)
            {
                DrivingTest Driving = DVLDApp.MangeDrivingTests.Find(TestID);

                lblTestID.Text = Driving.TestID.ToString();
                lblCreatedAcordingToApplication.Text = Driving.TestApplicationID.ToString();
                lblPersonID.Text = Driving.PersonID.ToString();
                lblAppointmentMadeByUserID.Text = Driving.AppointmentMadeByUserID.ToString();
                mtbAppointmentDate.Text = Driving.AppointmentDate.ToString("dd/MM/yyyy HH:mm");
                txtbPaidFees.Text = Driving.PaidFees.ToString();
                cbTestClass.SelectedIndex = (int)Driving.TestClass - 1;

                AppointmentDate = Driving.AppointmentDate;

                lblResultAddedByUser.Text = Driving.ResultAddedByUserID == null ? (string)"??" : Driving.ResultAddedByUserID.ToString();
                if (string.IsNullOrEmpty(Driving.Notes))
                    txtbNotes.Text = "";
                else
                    txtbNotes.Text = Driving.Notes;

                switch (Driving.TestResult)
                {
                    case null:
                        rbNotTacken.Checked = true;
                        break;
                    case true:
                        rbPass.Checked = true;
                        break;
                    case false:
                        rbFail.Checked = true;
                        break;
                }

            }
            else // (TestType == TestType.TheoreticalTest)
            {
                TheoreticalTest Theoretical = DVLDApp.MangeTheoreticalTests.Find(TestID);

                lblTestID.Text = Theoretical.TestID.ToString();
                lblCreatedAcordingToApplication.Text = Theoretical.TestApplicationID.ToString();
                lblPersonID.Text = Theoretical.PersonID.ToString();
                lblAppointmentMadeByUserID.Text = Theoretical.AppointmentMadeByUserID.ToString();
                mtbAppointmentDate.Text = Theoretical.AppointmentDate.ToString("dd/MM/yyyy HH:mm");
                txtbPaidFees.Text = Theoretical.PaidFees.ToString();
                cbTestClass.SelectedIndex = (int)Theoretical.TestClass - 1;

                AppointmentDate = Theoretical.AppointmentDate;

                lblResultAddedByUser.Text = Theoretical.ResultAddedByUserID == null ? (string)"??" : Theoretical.ResultAddedByUserID.ToString();
                if (string.IsNullOrEmpty(Theoretical.Notes))
                    txtbNotes.Text = "";
                else
                    txtbNotes.Text = Theoretical.Notes;

                switch (Theoretical.TestResult)
                {
                    case null:
                        rbNotTacken.Checked = true;
                        break;
                    case true:
                        rbPass.Checked = true;
                        break;
                    case false:
                        rbFail.Checked = true;
                        break;
                }
            }

            if (AppointmentDate != null) // it will never be null here but just in case of changes
            {
                if (DateTime.Now.Subtract(((DateTime)AppointmentDate)) > new TimeSpan(0, 0, 0))
                {
                    mtbAppointmentDate.Enabled = false;
                    rbNotTacken.Checked = false;
                    rbNotTacken.Enabled = false;
                    rbFail.Enabled = true;
                    rbPass.Enabled = true;
                }
                else // the appoint date is in the fetuer // 
                {
                    rbNotTacken.Checked = true;
                    rbFail.Enabled = false;
                    rbPass.Enabled = false;
                }
            }
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

        private void txtbNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtbNotes.Text.Length >= 500)
            {
                txtbNotes.Text = txtbNotes.Text.Substring(0, 500);
            }            
        }

        private Test _ToTest()
        {
            if (lblTestID.Text != "??")
            {
                Test TestToEdit = null;

                switch(_TestType)
                {
                    case TestType.EyeTest:
                        TestToEdit = DVLDApp.MangeEyeTests.Find(Convert.ToInt32(lblTestID.Text));
                        break;
                    case TestType.DrivingTest:
                        TestToEdit = DVLDApp.MangeDrivingTests.Find(Convert.ToInt32(lblTestID.Text));
                        break;
                    default: // TestType.TheoreticalTest
                        TestToEdit = DVLDApp.MangeTheoreticalTests.Find(Convert.ToInt32(lblTestID.Text));
                        break;
                }

                if (TestToEdit == null)
                {
                    MessageBox.Show("Could Not find the Test to Edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                TestToEdit.AppointmentDate = Convert.ToDateTime(mtbAppointmentDate.Text);
                TestToEdit.PaidFees = Convert.ToDecimal(txtbPaidFees.Text);

                if (string.IsNullOrEmpty(txtbNotes.Text))
                    TestToEdit.Notes = null;
                else
                    TestToEdit.Notes = txtbNotes.Text;


                bool? Result = null;
                if (rbFail.Checked)
                    Result = false;
                else if (rbPass.Checked)
                    Result = true;

                if (TestToEdit.TestResult != Result)
                        TestToEdit.SetResult((bool)Result, DVLDApp.LogedInUser.UserID);

                return TestToEdit;
            }
            else
            {
                bool? Result = null;
                if (rbFail.Checked)
                    Result = false;
                else if (rbPass.Checked)
                    Result = true;
                
                int TestPersonID = -1;

                if (lblCreatedAcordingToApplication.Text != "??")
                {
                    TestPersonID = Convert.ToInt32(lblPersonID.Text);
                }
                else
                {
                    TestPersonID = (int)PersonID;
                }

                switch (_TestType)
                {
                    case TestType.EyeTest:
                        if (Result == null)
                            return (Test)new EyeTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text);
                        else
                            return (Test)new EyeTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    (bool)Result, string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text, DVLDApp.LogedInUser.UserID);
                    case TestType.DrivingTest:
                        if (Result == null)
                            return (Test)new DrivingTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text, (LicenseClass)(cbTestClass.SelectedIndex + 1));
                        else
                            return (Test)new DrivingTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    (bool)Result, string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text, DVLDApp.LogedInUser.UserID, (LicenseClass)(cbTestClass.SelectedIndex + 1));
                    default: // TestType.TheoreticalTest
                        if (Result == null)
                            return (Test)new TheoreticalTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text, (LicenseClass)(cbTestClass.SelectedIndex + 1));
                        else
                            return (Test)new TheoreticalTest(TestPersonID, Convert.ToDateTime(mtbAppointmentDate.Text),
                                    Convert.ToDecimal(txtbPaidFees.Text), Convert.ToInt32(lblAppointmentMadeByUserID.Text), _CreatedAcordingToApplication,
                                    (bool)Result, string.IsNullOrEmpty(txtbNotes.Text) ? null : txtbNotes.Text, DVLDApp.LogedInUser.UserID,(LicenseClass)(cbTestClass.SelectedIndex + 1));
                }


            }

        }

        private bool _AddNewTest(Test TestToSave)
        {
            switch (_TestType)
            {
                case TestType.EyeTest:
                    return DVLDApp.MangeEyeTests._Add((EyeTest)TestToSave);
                case TestType.DrivingTest:
                    return DVLDApp.MangeDrivingTests.Add((DrivingTest)TestToSave);
                default: // TestType.TheoreticalTest
                    return DVLDApp.MangeTheoreticalTests.Add((TheoreticalTest)TestToSave);
            }
        }
        
        private bool _UpdateTest(Test TestToSave)
        {
            switch (_TestType)
            {
                case TestType.EyeTest:
                    return DVLDApp.MangeEyeTests.Update((EyeTest)TestToSave);
                case TestType.DrivingTest:
                    return DVLDApp.MangeDrivingTests.Update((DrivingTest)TestToSave);
                default: // TestType.TheoreticalTest
                    return DVLDApp.MangeTheoreticalTests.Update((TheoreticalTest)TestToSave);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(lblTestID.Text == "??")
            {
                if(!DateTime.TryParse(mtbAppointmentDate.Text,out DateTime DateTimeResult))
                {
                    ErrorProvider.SetError(mtbAppointmentDate, "unvalid Date or Time.");
                    return;
                }

                if (DateTimeResult.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0))
                {
                    ErrorProvider.SetError(mtbAppointmentDate,"The Date could not be in the past.");
                    return;
                }
                
                if(string.IsNullOrEmpty(txtbPaidFees.Text))
                {
                    ErrorProvider.SetError(txtbPaidFees, "The Paid Fees Could Not be empty space.");
                    return;
                }
                
                if(!Decimal.TryParse(txtbPaidFees.Text, out Decimal DecimalResult))
                {
                    ErrorProvider.SetError(mtbAppointmentDate, "Invalid value.");
                    return;
                }

                if(DecimalResult < General.SettingsClass.DrivingTestFees && !rbNotTacken.Checked)
                {
                    ErrorProvider.SetError(mtbAppointmentDate, "The client has to pay the complete fee before\nHe/She can get his/her grade.");
                    return;
                }

                Test Test = _ToTest();

                if(_ReturnTheTestWithOutSavingIt)
                {
                    ReturnTestToSave.Invoke(this, Test);
                    this.Close();
                    return;
                }

                if (_AddNewTest(Test))
                {
                    MessageBox.Show("Added succesfuly.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Adding Failed.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Test Test = _ToTest();

                if (!_IsValidEditingTest(Test))
                    return;
                


                if (_UpdateTest(Test))
                {
                    MessageBox.Show("Updated succesfuly.", "success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Updating Failed.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool _IsValidEditingTest(Test EditedTestToSave)
        {
            Test OriginalTest;
            decimal RequiredFees;

            switch (_TestType)
            {
                case TestType.EyeTest:
                    OriginalTest = (Test)DVLDApp.MangeEyeTests.Find(Convert.ToInt32(lblTestID.Text));
                    RequiredFees = SettingsClass.EyeTestFees;
                    break;
                case TestType.DrivingTest:
                    OriginalTest = (Test)DVLDApp.MangeDrivingTests.Find(Convert.ToInt32(lblTestID.Text));
                    RequiredFees = SettingsClass.DrivingTestFees;
                    break;
                default: // TestType.TheoreticalTest
                    OriginalTest = (Test)DVLDApp.MangeTheoreticalTests.Find(Convert.ToInt32(lblTestID.Text));
                    RequiredFees = SettingsClass.TheoreticalTestFees;
                    break;
            }

            if (OriginalTest.AppointmentDate != EditedTestToSave.AppointmentDate)
            {
                if (OriginalTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0) && EditedTestToSave.AppointmentDate != OriginalTest.AppointmentDate)
                {
                    MessageBox.Show("You can not edit the Test Appointment Date if it was in the past.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (OriginalTest.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0, 0) && EditedTestToSave.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0, 0))
                {
                    MessageBox.Show("You can not set the Test Appointment Date to the past.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (OriginalTest.PaidFees > EditedTestToSave.PaidFees && EditedTestToSave.PaidFees < RequiredFees)
            {
                MessageBox.Show("The client can not retrieve any of the paid fees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (rbPass.Checked)
                if (EditedTestToSave.PaidFees < RequiredFees)
                {
                    ErrorProvider.SetError(txtbPaidFees, "The client should pay all the fees before getting his result.");
                    return false;
                }
            if (rbFail.Checked)
                if (EditedTestToSave.PaidFees < RequiredFees)
                {
                    ErrorProvider.SetError(txtbPaidFees, "The client should pay all the fees before getting his result.");
                    return false; 
                }


            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
