using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Drawing;
using DataAccessTier;
using General;

namespace BusinessTier
{
    public static class DVLDApp
    {


        public static void LoadTheDBConnection()
        {
            InitializingDate.LoadTheDBConnection();
        }

        public static bool LogInWith(string UserNameOrID, string PassWord)
        {
            if (AccessUserData.IsExistAndActiveByUserNameAndPassword(UserNameOrID, PassWord))
            {
                _LogedInUser = AccessUserData.Find(UserNameOrID).ToUser();
                return true;
            }
            else if (AccessUserData.IsExistAndActiveByUserIDAndPassword(UserNameOrID, PassWord))
                if (int.TryParse(UserNameOrID, out int ID))
                {
                    _LogedInUser = AccessUserData.Find(ID).ToUser();
                    return true;
                }
                else
                    return false;
            else
                return false;

        }
        public static bool LogOut()
        {
            if (IsLogedin())
            {
                _LogedInUser = null;
                return true;
            }
            else
                return false;
        }
        public static bool IsLogedin()
        {
            return _LogedInUser == null;
        }


        public static User LogedInUser { get { return _LogedInUser; } }
        private static User _LogedInUser;


        public static class MangePeople
        {

            internal static Person _Add(Person PersonToAdd)
            {
                if (PersonToAdd == null)
                    return null;

                if (PersonToAdd.PersonID != -1)
                    return null;

                if (PersonToAdd.NationalityCountryID < 0)
                    return null;

                if (!AccessCountriesData.IsExist(PersonToAdd.NationalityCountryID))
                    return null;

                if (string.IsNullOrEmpty(PersonToAdd.NationalNo) ||
                    string.IsNullOrEmpty(PersonToAdd.FirstName) ||
                    string.IsNullOrEmpty(PersonToAdd.SecondName) ||
                    string.IsNullOrEmpty(PersonToAdd.LastName) ||
                    string.IsNullOrEmpty(PersonToAdd.Address) ||
                    string.IsNullOrEmpty(PersonToAdd.Phone) ||
                    string.IsNullOrEmpty(PersonToAdd.Email) ||
                    string.IsNullOrEmpty(PersonToAdd.ImagePath))
                    return null;


                if (PersonToAdd.DateOfBirth.AddYears(18) > DateTime.Now)
                    return null;


                Person P = AccessPersonData.AddNewPerson(PersonToAdd.NationalNo, PersonToAdd.FirstName, PersonToAdd.SecondName,
                    PersonToAdd.ThirdName, PersonToAdd.LastName, PersonToAdd.DateOfBirth, PersonToAdd.Gendor, PersonToAdd.Address, PersonToAdd.Phone,
                    PersonToAdd.Email, PersonToAdd.NationalityCountryID, PersonToAdd.ImagePath).ToPerson();

                return P;

            }

            public static bool Update(Person PersonToUpdate)
            {

                if (PersonToUpdate.PersonID == -1)
                    return false;

                Person OldPerson = AccessPersonData.Find(PersonToUpdate.PersonID).ToPerson();

                if (OldPerson == null)
                    return false;

                if (OldPerson.NationalNo != PersonToUpdate.NationalNo ||
                     OldPerson.NationalityCountryID != PersonToUpdate.NationalityCountryID)
                    return false;

                if (PersonToUpdate.DateOfBirth.AddYears
                     (SettingsClass.PeopleSettings.MinimumAgeForPersonOnTheSystem) > DateTime.Now)
                    return false;

                return AccessPersonData.UpdatePerson(PersonToUpdate.PersonID, PersonToUpdate.NationalNo, PersonToUpdate.FirstName,
                    PersonToUpdate.SecondName, PersonToUpdate.ThirdName, PersonToUpdate.LastName, PersonToUpdate.DateOfBirth, PersonToUpdate.Gendor,
                    PersonToUpdate.Address, PersonToUpdate.Phone, PersonToUpdate.Email, PersonToUpdate.NationalityCountryID, PersonToUpdate.ImagePath);
            }

            public static string GetPersonImagePath(int PersonIDToFind)
            {
                return AccessPersonData.GetPersonImagePath(PersonIDToFind);
            }

            public static Person Find(int PersonID)
            {
                var Person = AccessPersonData.Find(PersonID);

                if (Person == null)
                    return null;
                else
                    return Person.ToPerson();
            }

            public static DataTable ListAllPeople()
            {
                return AccessPersonData.ListAllPeople();
            }

            public static bool IsExist(int PersonIDToFind)
            {
                return AccessPersonData.IsExist(PersonIDToFind);
            }

            public static bool IsExist(string NationalNumber,int NationalityCountryID)
            {
                return AccessPersonData.IsExist(NationalNumber, NationalityCountryID);
            }

        }

        public static class MangeUsers
        {
            public static User Add(User UserToAdd)
            {
                if (UserToAdd == null)
                    return null;

                if (UserToAdd.UserID != -1)
                    return null;

                if (string.IsNullOrEmpty(UserToAdd.UserName))
                    return null;

                if (string.IsNullOrEmpty(UserToAdd.Password))
                    return null;

                if (UserToAdd.Password.Length < 5)
                    return null;

                if (UserToAdd.PersonID < 0)
                    return null;

                if (!AccessPersonData.IsExist(UserToAdd.PersonID))
                    return null;

                if (AccessUserData.IsExistByPersonID(UserToAdd.PersonID))
                    return null;


                UserToAdd.IsActive = true;


                User U = AccessUserData.AddNewUser
                    (UserToAdd.PersonID, UserToAdd.UserName, UserToAdd.Password, UserToAdd.IsActive).ToUser();

                return U;

            }

            public static User Add(User UserToAdd,Person PersonToAdd)
            {
                if (UserToAdd == null || PersonToAdd == null)
                    return null;

                Person AddedPerson = MangePeople._Add(PersonToAdd);

                if (AddedPerson == null)
                    return null;

                UserToAdd.PersonID = AddedPerson.PersonID;

                User AddedUser = Add(UserToAdd);

                if (AddedUser == null)
                    AccessPersonData.DeletePerson(AddedPerson.PersonID);

                return AddedUser;
            }

            public static bool Update(User UserToUpdate)
            {
                if (UserToUpdate == null)
                    return false;

                if (UserToUpdate.UserID < 0)
                    return false;

                User OldUser = AccessUserData.Find(UserToUpdate.UserID).ToUser();

                if (OldUser == null)
                    return false;

                if (OldUser.PersonID != UserToUpdate.PersonID)
                    return false;

                if (OldUser.Password == UserToUpdate.Password &&
                    OldUser.UserName == UserToUpdate.UserName &&
                    OldUser.IsActive == UserToUpdate.IsActive)
                    return false;

                return AccessUserData.UpdateUser(UserToUpdate.UserID, UserToUpdate.PersonID, UserToUpdate.UserName, UserToUpdate.Password, UserToUpdate.IsActive);
            }

            public static User Find(int UserID)
            {
                var User = AccessUserData.Find(UserID);

                if (User == null)
                    return null;
                else
                    return User.ToUser();
            }

            public static DataTable ListAllUsers()
            {
                return AccessUserData.ListAllUsers();
            }

            public static bool IsExist(int UserIDToFind)
            {
                return AccessUserData.IsExist(UserIDToFind);
            }

            public static bool IsExist(string UserName)
            {
                return AccessUserData.IsExist(UserName);
            }

            public static bool IsExistByPersonID(int PersonID)
            {
                return AccessUserData.IsExistByPersonID(PersonID);
            }

        }
        
        public static class MangeApplications
        {
            public static class Fees
            {
                public static decimal? GetToatlApplicationFees(ApplicationType Type)
                {
                    decimal? ApplicationFees = SettingsClass.Application.GetBaseApplicationFees(Type);

                    if (ApplicationFees == null)
                        return null;

                    return ApplicationFees;
                }

                public static decimal GetIssuingNewLicenseApplicationTotalFees(LicenseClass Class)
                {
                    return SettingsClass.Application.GetBaseApplicationFees(ApplicationType.LicenseIssuance) 
                        + SettingsClass.License.WhatIsTheFeeForLicense(Class);
                }

                public static decimal? GetReleaseLicenseApplicationTotalFees(int LicenseIDToRelease)
                {
                    decimal ApplicationFees = SettingsClass.Application.GetBaseApplicationFees(ApplicationType.ReleaseLicense);

                    decimal? FineFee = AccessDetainingLicenseRecordData.HowMuchTheFineFeeForLicense(LicenseIDToRelease);

                    if (FineFee == null)
                        return -1;
                    else
                        ApplicationFees += (Decimal)FineFee;

                    return ApplicationFees;
                }

            }

            public static class Add
            {
                private static Application _Application(Application ApplicationToAdd, LicenseClass? Class = null, int? TheDetainedLicenseID = null)
                {
                    if (ApplicationStatus.Canceled == ApplicationToAdd.ApplicationStatus)
                        return null;

                    bool? DoesHaveApplicationOfType =
                        AccessApplicationData.DoesPersonHaveActiveAppicationOfType(ApplicationToAdd.ApplicantPersonID, ApplicationToAdd.ApplicationType);

                    if (DoesHaveApplicationOfType == null)
                        return null;

                    if (DoesHaveApplicationOfType == true)
                    {
                        switch (ApplicationToAdd.ApplicationType)
                        {
                            case ApplicationType.ReleaseLicense:
                                if (TheDetainedLicenseID == null)
                                    return null;

                                DetainLicenseRecord DetainingRecord = AccessDetainingLicenseRecordData.FindLastDetainingOfLicense((int)TheDetainedLicenseID).ToDetainLicenseRecord();

                                if (DetainingRecord.IsReleased)
                                    return null;

                                if (DetainingRecord.ReleaseApplicationID != null)
                                    if (!SettingsClass.Application.IsExpired((DateTime)AccessApplicationData.GetApplicationCreationDate((int)DetainingRecord.ReleaseApplicationID)))
                                        return null;                               
                                break;

                            case ApplicationType.LicenseIssuance:
                                if (Class == null)
                                    return null;

                                LocalDrivingLicenseApplication LocalDrivingLicenseApplication = 
                                    AccessLocalDrivingLicenseApplicationData.FindLeatestActiveLocalAppOfClassForPersonID
                                       (ApplicationToAdd.ApplicantPersonID, (LicenseClass)Class).ToLocalDrivingLicenseApplication();

                                if (LocalDrivingLicenseApplication != null)
                                {
                                    Application OldApplication = AccessApplicationData.Find(LocalDrivingLicenseApplication.ApplicationID).ToApplication();

                                    if (!OldApplication.IsExpired)
                                        return null;
                                }
                                else
                                    return null;

                                DateTime? PersonBirthDate = AccessPersonData.GetPersonBirthDate(ApplicationToAdd.ApplicantPersonID);

                                if (PersonBirthDate == null)
                                    return null;
                                else if (!SettingsClass.License.IsDriverOledEnough((DateTime)PersonBirthDate, (LicenseClass)Class))
                                    return null;
                                break;
                            
                            default:
                                return null;
                        }
                    }

                    decimal TotalApplicationFees;

                    switch (ApplicationToAdd.ApplicationType)
                    {
                        case ApplicationType.LicenseIssuance:
                            if (Class == null)
                                return null;
                            TotalApplicationFees = Fees.GetIssuingNewLicenseApplicationTotalFees((LicenseClass)Class);
                            break;

                        case ApplicationType.ReleaseLicense:
                            if (TheDetainedLicenseID == null)
                                return null;
                            decimal? ReleaseApplicationFee =
                             Fees.GetReleaseLicenseApplicationTotalFees((int)TheDetainedLicenseID);
                            if (ReleaseApplicationFee == null)
                                return null;
                            else
                                TotalApplicationFees = (decimal)ReleaseApplicationFee;
                            break;

                        default:
                            TotalApplicationFees = (decimal)Fees.GetToatlApplicationFees(ApplicationToAdd.ApplicationType);
                            break;
                    }

                    // we can not take more than the applicetion cost
                    if (TotalApplicationFees > ApplicationToAdd.PaidFees)
                        return null;

                    Application AddedApplication = DataAccessTier.AccessApplicationData.AddNewApplication(ApplicationToAdd.ApplicationID,
                        DateTime.Now, ApplicationToAdd.ApplicationType, ApplicationToAdd.ApplicationStatus,
                       DateTime.Now, ApplicationToAdd.PaidFees,
                        DVLDApp.LogedInUser.PersonID).ToApplication();

                    return AddedApplication;
                }

                /* How To Use This Method --> Check the documentation */
                public static Application IssuingLocalLicenseApplication(Application ApplicationToAdd, LicenseClass Class, EyeTest EyeTest,
                    DrivingTest DrivingTest, TheoreticalTest TheoreticalTest,string IdentificationPhotoPath,
                    string DrivingCourseCertificatePhotoPath)
                {
                    if (ApplicationToAdd == null ||
                        EyeTest == null ||
                        DrivingTest == null ||
                        TheoreticalTest == null ||
                        string.IsNullOrEmpty(IdentificationPhotoPath) ||
                        string.IsNullOrEmpty(DrivingCourseCertificatePhotoPath) ||
                        ApplicationToAdd.ApplicationID != -1)
                            return null;

                    if (!GeneralFunctions.IsValidImage(IdentificationPhotoPath))
                        return null;
                    if (!GeneralFunctions.IsValidImage(DrivingCourseCertificatePhotoPath))
                        return null;

                    DateTime? PersonBirthDate = AccessPersonData.GetPersonBirthDate(ApplicationToAdd.ApplicantPersonID);

                    if (PersonBirthDate == null)
                        return null;

                    if (AccessLicenseData.DoseHaveActiveLicense(ApplicationToAdd.ApplicantPersonID, Class))
                        return null;

                    if (!SettingsClass.License.IsDriverOledEnough((DateTime)PersonBirthDate, Class))
                        return null;

                    int? DriverID = AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID);
                    if (DriverID == null)
                    {
                        if (AccessLicenseData.FindTheActiveLicense((int)DriverID, Class) == null)
                            return null;
                    }
                    else
                        return null;

                    if (EyeTest.TestID != -1)
                    {
                        EyeTest = DataAccessTier.AccessEyeTestData.Find(EyeTest.TestID).ToEyeTest();
                        if (EyeTest.IsExpired)
                            return null;
                        if (EyeTest.TestResult == false)
                            return null;
                        if (EyeTest.DidTheTestPassWithoutAttending() == true)
                            return null;
                    }
                    if (TheoreticalTest.TestID != -1)
                    {
                        TheoreticalTest = AccessTheoreticalTestData.Find(TheoreticalTest.TestID).ToTheoreticalTest();
                        if (TheoreticalTest.IsExpired)
                            return null;
                        if (TheoreticalTest.TestResult == false)
                            return null;
                        if (TheoreticalTest.DidTheTestPassWithoutAttending() == true)
                            return null;
                    }
                    if (DrivingTest.TestID != -1)
                    {
                        DrivingTest = DataAccessTier.AccessDrivingTestData.Find(DrivingTest.TestID).ToDrivingTest();
                        if (DrivingTest.IsExpired)
                            return null;
                        if (DrivingTest.TestResult == false)
                            return null;
                        if (DrivingTest.DidTheTestPassWithoutAttending() == true)
                            return null;
                    }

                    if (ApplicationToAdd.ApplicationType != ApplicationType.LicenseIssuance)
                        return null;

                    if (ApplicationToAdd.ApplicantPersonID != EyeTest.PersonID ||
                         ApplicationToAdd.ApplicantPersonID != DrivingTest.PersonID ||
                          ApplicationToAdd.ApplicantPersonID != TheoreticalTest.PersonID)
                        return null;

                    if (Class != DrivingTest.TestClass ||
                         Class != TheoreticalTest.TestClass)
                        return null;

                    Application AddedAppliction = _Application(ApplicationToAdd, Class);

                    if (AddedAppliction == null)
                        return null;

                    int? DoesEyeTestSucceeded = null;
                    int? DoesTheoreticalTestSucceeded = null;
                    int? DoesDrivingTestSucceeded = null;

                    if (EyeTest.TestID == -1)
                    {
                        DoesEyeTestSucceeded = MangeEyeTests._Add(new EyeTest(EyeTest.PersonID, EyeTest.AppointmentDate, EyeTest.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, EyeTest.Notes));

                        if (DoesEyeTestSucceeded == -1)
                        {
                            DataAccessTier.AccessApplicationData.DeleteApplication(AddedAppliction.ApplicationID);
                            return null;
                        }
                    }

                    if (DrivingTest.TestID == -1)
                    {
                        DoesDrivingTestSucceeded = MangeDrivingTests._Add(new DrivingTest(DrivingTest.PersonID, DrivingTest.AppointmentDate, DrivingTest.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, DrivingTest.Notes, DrivingTest.TestClass));

                        if (DoesDrivingTestSucceeded == -1)
                        {
                            if (DoesEyeTestSucceeded != null)
                                DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                            DataAccessTier.AccessApplicationData.DeleteApplication(AddedAppliction.ApplicationID);
                            return null;
                        }
                    }

                    if (TheoreticalTest.TestID == -1)
                    {
                        DoesTheoreticalTestSucceeded = MangeTheoreticalTests._Add(new TheoreticalTest(TheoreticalTest.PersonID, TheoreticalTest.AppointmentDate, TheoreticalTest.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, TheoreticalTest.Notes, TheoreticalTest.TestClass));

                        if (DoesTheoreticalTestSucceeded == -1)
                        {
                            if (DoesEyeTestSucceeded != null)
                                DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                            if (DoesDrivingTestSucceeded != null)
                                DataAccessTier.AccessDrivingTestData.DeleteTest((int)DoesDrivingTestSucceeded);
                            DataAccessTier.AccessApplicationData.DeleteApplication(AddedAppliction.ApplicationID);
                            return null;
                        }
                    }

                    int DoesLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                        (new LocalDrivingLicenseApplication(AddedAppliction.ApplicationID, Class
                          , DoesEyeTestSucceeded != null ? (int)DoesEyeTestSucceeded : EyeTest.TestID,
                          DoesTheoreticalTestSucceeded != null ? (int)DoesTheoreticalTestSucceeded : TheoreticalTest.TestID,
                          DoesDrivingTestSucceeded != null ? (int)DoesDrivingTestSucceeded : DrivingTest.TestID,IdentificationPhotoPath,DrivingCourseCertificatePhotoPath));

                    if (DoesLocalApplicationSucceeded == -1)
                    {
                        if (DoesEyeTestSucceeded != null)
                            DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                        if (DoesDrivingTestSucceeded != null)
                            DataAccessTier.AccessDrivingTestData.DeleteTest((int)DoesDrivingTestSucceeded);
                        if (DoesTheoreticalTestSucceeded != null)
                            DataAccessTier.AccessTheoreticalTestData.DeleteTest((int)DoesTheoreticalTestSucceeded);

                        DataAccessTier.AccessApplicationData.DeleteApplication(AddedAppliction.ApplicationID);
                        return null;
                    }

                    return AddedAppliction;
                }

                public static Application RetakeTestApplication(Application ApplicationToAdd, TestType Type, Test TestToSave)
                {
                    if (ApplicationType.RetakeTest != ApplicationToAdd.ApplicationType)
                        return null;

                    if (ApplicationStatus.Completed != ApplicationToAdd.ApplicationStatus)
                        return null;

                    if (ApplicationToAdd.ApplicantPersonID != TestToSave.PersonID)
                        return null;

                    ApplicationToAdd.LastStatusDate = DateTime.Now;

                    Application AddedAppliction
                        = MangeApplications.Add._Application(ApplicationToAdd);

                    if (AddedAppliction == null)
                        return null;

                    int DoesTestAddingSucceed;

                    switch (Type)
                    {
                        case TestType.EyeTest:
                            DoesTestAddingSucceed = MangeEyeTests._Add(new EyeTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, TestToSave.Notes));

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return null;
                            }
                            break;
                        case TestType.DrivingTest:
                            DoesTestAddingSucceed = MangeDrivingTests._Add(new DrivingTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, TestToSave.Notes, ((DrivingTest)TestToSave).TestClass));

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return null;
                            }
                            break;
                        default: // TestType.TheoreticalTest
                            DoesTestAddingSucceed = MangeTheoreticalTests._Add(new TheoreticalTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, AddedAppliction.ApplicationID, TestToSave.Notes, ((DrivingTest)TestToSave).TestClass));

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return null;
                            }
                            break;
                    }



                    return AddedAppliction;
                }

                public static Application RenewDrivingLicenseApplication(Application ApplicationToAdd, int LicenseIDToRenew, EyeTest EyeTest)
                {
                    if (ApplicationToAdd == null ||
                         LicenseIDToRenew < 0 ||
                          EyeTest == null ||
                          ApplicationToAdd.ApplicationID != -1)
                        return null;

                    if (EyeTest.TestID != -1)
                    {
                        EyeTest = DataAccessTier.AccessEyeTestData.Find(EyeTest.TestID).ToEyeTest();
                        if (EyeTest.IsExpired)
                            return null;
                        if (EyeTest.TestResult == false)
                            return null;
                        if (EyeTest.DidTheTestPassWithoutAttending() == true)
                            return null;
                    }

                    if (EyeTest.PersonID != ApplicationToAdd.ApplicantPersonID)
                        return null;

                    License LicenseToRenew = AccessLicenseData.Find(LicenseIDToRenew).ToLicense();

                    if (AccessDetainingLicenseRecordData.IsLicenseCurrenlyDetained(LicenseToRenew.LicenseID))
                        return null;

                    if (LicenseToRenew.IsActive == false)
                        return null;

                    Application AddedApplication = _Application(ApplicationToAdd);

                    if (AddedApplication == null)
                        return null;

                    int? DoesEyeTestSucceeded = null;

                    if (EyeTest.TestID == -1)
                    {
                        DoesEyeTestSucceeded = MangeEyeTests._Add(EyeTest);

                        if (DoesEyeTestSucceeded == -1)
                        {
                            AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                            return null;
                        }
                    }

                    int DoesLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                        (new LocalDrivingLicenseApplication(AddedApplication.ApplicationID, LicenseToRenew.LicenseClass, EyeTest.TestID != -1 ? EyeTest.TestID : DoesEyeTestSucceeded, null, null, null, null));

                    if (DoesLocalApplicationSucceeded == -1)
                    {
                        if (DoesEyeTestSucceeded != null)
                            AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                        AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                        return null;
                    }

                    return AddedApplication;

                }

                /* How To Use This Method --> Check the documentation */
                public static Application MissingReplacementApplication(Application ApplicationToAdd, int LicenseIDToReplaceBasedOn)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1)
                        return null;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDToReplaceBasedOn).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return null;

                    if (!LicenseToReplaceBasedOn.IsActive)
                        return null;
                    if (LicenseToReplaceBasedOn.IsExpired)
                        return null;

                    Application AddedApplication = _Application(ApplicationToAdd);

                    if (AddedApplication == null)
                        return null;

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                       (new LocalDrivingLicenseApplication
                        (AddedApplication.ApplicationID, LicenseToReplaceBasedOn.LicenseClass, null, null, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                        return null;
                    }

                    return AddedApplication;
                }

                public static Application DamagedReplacementApplication(Application ApplicationToAdd, int LicenseIDToReplaceBasedOn)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1 ||
                        LicenseIDToReplaceBasedOn < 0)
                        return null;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDToReplaceBasedOn).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return null;

                    if (!LicenseToReplaceBasedOn.IsActive)
                        return null;
                    if (LicenseToReplaceBasedOn.IsExpired)
                        return null;

                    Application AddedApplication = _Application(ApplicationToAdd);

                    if (AddedApplication == null)
                        return null;

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                       (new LocalDrivingLicenseApplication
                        (AddedApplication.ApplicationID, LicenseToReplaceBasedOn.LicenseClass, null, null, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                        return null;
                    }

                    return AddedApplication;
                }

                public static Application ReleaseLicenseApplication(Application ApplicationToAdd, int LicenseIDAskForRelease)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1 ||
                        LicenseIDAskForRelease < 0)
                        return null;
                      
                    if (!AccessDetainingLicenseRecordData.IsLicenseCurrenlyDetained(LicenseIDAskForRelease))
                        return null;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDAskForRelease).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return null;

                    Application AddedApplication = _Application(ApplicationToAdd, null, LicenseIDAskForRelease);

                    if (AddedApplication == null)
                        return null;

                    bool DoseDetainingRecordsSucceeded =
                        AccessDetainingLicenseRecordData.AttachToAppliceation(AccessDetainingLicenseRecordData.FindLastDetainingOfLicense(LicenseIDAskForRelease).DetainID,AddedApplication.ApplicationID);

                    if(!DoseDetainingRecordsSucceeded)
                    {
                        AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                        return null;
                    }

                    return AddedApplication;
                }

                public static Application IssuingInternationalLicenseApplication(Application ApplicationToAdd, EyeTest EyeTestToAdd)
                {
                    if (ApplicationToAdd == null ||
                        EyeTestToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1)
                        return null;

                    if (EyeTestToAdd.TestID != -1)
                    {
                        EyeTestToAdd = AccessEyeTestData.Find(EyeTestToAdd.TestID).ToEyeTest();

                        if (EyeTestToAdd.IsExpired)
                            return null;
                        if (EyeTestToAdd.TestResult == false)
                            return null;
                        if (EyeTestToAdd.DidTheTestPassWithoutAttending() == true)
                            return null;
                    }

                    if (EyeTestToAdd.PersonID != ApplicationToAdd.ApplicantPersonID)
                        return null;


                    int? DriverID = AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID);

                    if (DriverID == null)
                        return null;
                    if (DriverID == -1)
                        return null;

                    if (!AccessLicenseData.DoseHaveActiveLicense((int)DriverID, LicenseClass.RegularCar))
                        return null;

                    Application AddedApplication = _Application(ApplicationToAdd);

                    if (AddedApplication == null)
                        return null;

                    int? DoseEyeTestSucceeded = null;

                    if (EyeTestToAdd.TestID == -1)
                    {
                        DoseEyeTestSucceeded = MangeEyeTests._Add(EyeTestToAdd);
                        if (DoseEyeTestSucceeded == -1)
                        {
                            AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                            return null;
                        }
                    }

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add(new LocalDrivingLicenseApplication
                        (AddedApplication.ApplicationID, LicenseClass.RegularCar, (DoseEyeTestSucceeded != null) ? DoseEyeTestSucceeded : EyeTestToAdd.TestID, null, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        if (DoseEyeTestSucceeded != null)
                            AccessEyeTestData.DeleteEyeTest(EyeTestToAdd.TestID);
                        AccessApplicationData.DeleteApplication(AddedApplication.ApplicationID);
                        return null;
                    }

                    return AddedApplication;
                }

            }

            public static bool IsTheApplicationValidToAttachLicense(int ApplicationID)
            {

                Application FindedApplication =
                    AccessApplicationData.Find(ApplicationID).ToApplication();

                if (FindedApplication == null)
                    return false;

                if (FindedApplication.ApplicationType != ApplicationType.LicenseIssuance)
                    return false;

                if (DateTime.Now.Subtract(FindedApplication.ApplicationDate) > SettingsClass.Application.ApplicationExpirationPeriod)
                    return false;

                if (FindedApplication.ApplicationStatus != ApplicationStatus.Completed)
                    return false;


                LocalDrivingLicenseApplication FindedLocalApplication =
                    AccessLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID).ToLocalDrivingLicenseApplication();

                if (FindedLocalApplication == null)
                    return false;

                if (FindedLocalApplication.EyeTestID == null || FindedLocalApplication.DrivingTestID == null || FindedLocalApplication.TheoritecalTestID == null)
                    return false;


                EyeTest EyeTest = MangeEyeTests.Find((int)FindedLocalApplication.EyeTestID);
                if (EyeTest == null)
                    return false;
                
                DrivingTest DrivingTest = MangeDrivingTests.Find((int)FindedLocalApplication.DrivingTestID);
                if (DrivingTest == null)
                    return false;
                
                TheoreticalTest TheoreticalTest = MangeTheoreticalTests.Find((int)FindedLocalApplication.TheoritecalTestID);
                if (TheoreticalTest == null)
                    return false;


                if (EyeTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(EyeTest.AppointmentDate) > SettingsClass.TestInfos.Eye.EyeTestExpirationPeriod)
                    return false;

                if (EyeTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;



                if (DrivingTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(DrivingTest.AppointmentDate) > SettingsClass.TestInfos.Driving.ExpirationPeriod)
                    return false;

                if (DrivingTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;

                if (DrivingTest.TestClass != FindedLocalApplication.LicenseClass)
                    return false;


                if (TheoreticalTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(TheoreticalTest.AppointmentDate) > SettingsClass.TestInfos.Theoretical.TheoreticalTestExpirationPeriod)
                    return false;

                if (TheoreticalTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;

                if (TheoreticalTest.TestClass != FindedLocalApplication.LicenseClass)
                    return false;

                return true;
            }

            public static bool UpdateApplicationFee(int ApplicationIDToUpdate, decimal NewPaiedFee)
            {
                if (ApplicationIDToUpdate > -1)
                    return false;
                if (NewPaiedFee < 0)
                    return false;

                Application OldApplication = 
                    AccessApplicationData.Find(ApplicationIDToUpdate).ToApplication();

                if (OldApplication == null)
                    return false;
                if (OldApplication.IsExpired)
                    return false;
                if (OldApplication.ApplicationStatus != ApplicationStatus.New)
                    return false;


                decimal? ApplicationCost;

                switch (OldApplication.ApplicationType)
                {
                    case ApplicationType.LicenseIssuance:
                        
                        LicenseClass? Class =
                            AccessLocalDrivingLicenseApplicationData.FindClassByApplicationID(OldApplication.ApplicationID);
                        
                        if (Class == null)
                            return false;

                        ApplicationCost = 
                            Fees.GetIssuingNewLicenseApplicationTotalFees((LicenseClass)Class);

                        if (NewPaiedFee < OldApplication.PaidFees)
                        {
                            if (OldApplication.PaidFees < ApplicationCost)
                                return false;
                            if (NewPaiedFee != ApplicationCost)
                                return false;
                        }

                        if (NewPaiedFee > ApplicationCost)
                            return false;
                        break;

                    case ApplicationType.ReleaseLicense:

                        ApplicationCost = Fees.GetReleaseLicenseApplicationTotalFees
                            (AccessDetainingLicenseRecordData.FindByApplicationID(ApplicationIDToUpdate).LicenseID);

                        if (ApplicationCost == null)
                            return false;

                        if (NewPaiedFee < OldApplication.PaidFees)
                        {
                            if (OldApplication.PaidFees < ApplicationCost)
                                return false;
                            if (NewPaiedFee != ApplicationCost)
                                return false;
                        }

                        if (NewPaiedFee > ApplicationCost)
                            return false;
                        break;

                    default:
                        ApplicationCost = Fees.GetToatlApplicationFees                            
                            ((ApplicationType)AccessApplicationData.GetApplicationType(ApplicationIDToUpdate));

                        if (ApplicationCost == null)
                            return false;

                        if (NewPaiedFee < OldApplication.PaidFees)
                        {
                            if (OldApplication.PaidFees < ApplicationCost)
                                return false;
                            if (NewPaiedFee != ApplicationCost)
                                return false;
                        }

                        if (NewPaiedFee > ApplicationCost)
                            return false;
                        break;

                }

                return (bool)AccessApplicationData.UpdateApplicationPaiedFee(ApplicationIDToUpdate, NewPaiedFee);
            }

            public static bool CanceleApplication(int ApplicationIDToCancele)
            {
                if (ApplicationIDToCancele > -1)
                    return false;

                Application OregenalApplication =
                    AccessApplicationData.Find(ApplicationIDToCancele).ToApplication();
                if (OregenalApplication == null)
                    return false;
                if (OregenalApplication.IsExpired)
                    return false;
                if (OregenalApplication.ApplicationStatus != ApplicationStatus.New)
                    return false;                

                return (bool)AccessApplicationData.CanceleApplication(ApplicationIDToCancele);
            }

            private static bool _CompleteApplication(int ApplicationIDToComplete)
            {
                Application ApplicationToComplete = AccessApplicationData.Find(ApplicationIDToComplete).ToApplication();

                if (ApplicationToComplete == null)
                    return false;
                if (ApplicationToComplete.IsExpired)
                    return false;
                if (ApplicationToComplete.ApplicationStatus != ApplicationStatus.New)
                    return false;

                switch (ApplicationToComplete.ApplicationType)
                {
                    case ApplicationType.LicenseIssuance:
                        LicenseClass? Class =
                            AccessLocalDrivingLicenseApplicationData.FindClassByApplicationID(ApplicationToComplete.ApplicationID);
                        if (Class == null)
                            return false;
                        if (ApplicationToComplete.PaidFees != Fees.GetIssuingNewLicenseApplicationTotalFees((LicenseClass)Class))
                            return false;
                        break;

                    case ApplicationType.ReleaseLicense:
                        
                        decimal? ReleaseApplicationFee = Fees.GetReleaseLicenseApplicationTotalFees
                            (AccessDetainingLicenseRecordData.FindByApplicationID(ApplicationIDToComplete).LicenseID);

                        if (ReleaseApplicationFee == null)
                            return false;

                        if (ApplicationToComplete.PaidFees != (decimal)ReleaseApplicationFee) 
                            return false;
                        break;

                    default:
                        if (ApplicationToComplete.PaidFees != Fees.GetToatlApplicationFees((ApplicationType)AccessApplicationData.GetApplicationType(ApplicationIDToComplete)))
                            return false;
                        break;

                }

                return AccessApplicationData.CompleteApplication(ApplicationIDToComplete) == true;
            }

            /*
                // PaidFees and is the Only thing that can be Updated any way throuw this function //
            public static bool Update(Application ApplicationToUpdate)
            {                                  
                if (ApplicationToUpdate == null)
                    return false;
                if (ApplicationToUpdate.ApplicationID == -1)
                    return false;
                if (!AccessApplicationData.IsExist(ApplicationToUpdate.ApplicationID))
                    return false;


                Application OregenalApplication = AccessApplicationData.Find(ApplicationToUpdate.ApplicationID).ToApplication();

                if (OregenalApplication.IsExpired)
                    return false;
                if (OregenalApplication.ApplicationStatus != ApplicationToUpdate.ApplicationStatus)
                    return false;
                if (OregenalApplication.ApplicationStatus != ApplicationStatus.New)
                    return false;

                if (OregenalApplication.ApplicationStatus == ApplicationStatus.New &&
                     ApplicationToUpdate.ApplicationStatus == ApplicationStatus.Completed)
                    return false;

                if (OregenalApplication.ApplicantPersonID != ApplicationToUpdate.ApplicantPersonID ||
                    OregenalApplication.CreatedByUserID != ApplicationToUpdate.CreatedByUserID ||
                    OregenalApplication.ApplicationDate != ApplicationToUpdate.ApplicationDate ||
                    OregenalApplication.ApplicationType != ApplicationToUpdate.ApplicationType)
                    return false;

                switch (OregenalApplication.ApplicationType)
                {
                    case ApplicationType.LicenseIssuance:
                        LicenseClass? Class =
                            AccessLocalDrivingLicenseApplicationData.FindClassByApplicationID(OregenalApplication.ApplicationID);
                        if (Class == null)
                            return false;
                        if (ApplicationToUpdate.PaidFees > Fees.GetIssuingNewLicenseApplicationTotalFees((LicenseClass)Class))
                            return false;
                        break;

                    case ApplicationType.ReleaseLicense:
                        if (ApplicationToUpdate.PaidFees > Fees.GetReleaseLicenseApplicationTotalFees
                            (AccessDetainingLicenseRecordData.FindByApplicationID(OregenalApplication.ApplicationID).LicenseID))
                            return false;
                        break;

                    default:
                        if (ApplicationToUpdate.PaidFees > Fees.GetToatlApplicationFees(ApplicationToUpdate.ApplicationType))
                            return false;
                        break;

                }



                return AccessApplicationData.UpdateApplication(ApplicationToUpdate.ApplicationID, ApplicationToUpdate.ApplicantPersonID,
                    ApplicationToUpdate.ApplicationDate, ApplicationToUpdate.ApplicationType, ApplicationToUpdate.ApplicationStatus,
                    ApplicationToUpdate.LastStatusDate, ApplicationToUpdate.PaidFees, ApplicationToUpdate.CreatedByUserID);
            }
            */

            public static Application Find(int ApplicationID)
            {
                var Application = AccessApplicationData.Find(ApplicationID);

                if (Application == null)
                    return null;
                else
                    return Application.ToApplication();
            }

            public static int GetPersonID(int ApplicationID)
            {
                return AccessApplicationData.GetPersonID(ApplicationID);
            }

            public static DataTable ListAllApplications()
            {
                return AccessApplicationData.ListAllApplications();
            }

            public static DataTable GetAllPersonApplications(int PersonID, ApplicationType? TypeSpecific = null)
            {
                return AccessApplicationData.GetAllPersonApplications(PersonID, TypeSpecific);
            }

            public static DateTime? GetApplicationCreationDate(int applicationID)
            {
                return DataAccessTier.AccessApplicationData.GetApplicationCreationDate(applicationID);
            }

            public static bool? IsExpired(int applicationID, ApplicationType? TypeSpecific = null)
            {
                DateTime? ApplicationDate = GetApplicationCreationDate(applicationID);

                if (ApplicationDate == null)
                    return null;

                return (DateTime.Now.Subtract((DateTime)ApplicationDate) >
                           SettingsClass.Application.ApplicationExpirationPeriod);

            }

            public static bool IsExist(int ApplicationIDToFind)
            {
                return AccessApplicationData.IsExist(ApplicationIDToFind);
            }

            /*public static bool DeleteApplication(int ApplicationIDToFind)
            {
                return AccessApplicationData.DeleteApplication(ApplicationIDToFind);
            } You can not just Delete Applications around
            */

            public static bool CouldCreateAnEyeTest(int ApplicationID)
            {
                if (ApplicationID < 0)
                    return false;

                if (!AccessApplicationData.IsExist(ApplicationID))
                    return false;

                DTApplication Application = AccessApplicationData.Find(ApplicationID);

                ApplicationType Type = (ApplicationType)AccessApplicationData.GetApplicationType(ApplicationID);

                if (Type == ApplicationType.RetakeTest)
                {
                    //AccessEyeTestData.
                }
                if (Type == ApplicationType.LicenseIssuance)
                {

                }

                return false;
            }

        }

        public static class MangeCountries
        {
            public static Country Add(Country CountryToAdd)
            {

                if (CountryToAdd.CountryID != -1)
                    return null;

                if (!string.IsNullOrEmpty(CountryToAdd.CountryName))
                    return null;

                else
                {
                    Country U = AccessCountriesData.AddNewCountry(CountryToAdd.CountryName).ToCountry();

                    return U;
                }
            }

            public static bool Update(Country CountryToUpdate)
            {
                if (CountryToUpdate == null)
                    return false;

                if (CountryToUpdate.CountryID == -1)
                    return false;

                if (!AccessCountriesData.IsExist(CountryToUpdate.CountryID))
                    return false;

                return AccessCountriesData.UpdateCountry(CountryToUpdate.CountryID, CountryToUpdate.CountryName);
            }

            public static Country Find(int CountryID)
            {
                var Country = AccessCountriesData.Find(CountryID);

                if (Country == null)
                    return null;
                else
                    return Country.ToCountry();
            }

            public static DataTable ListAllCountries()
            {
                return AccessCountriesData.ListAllCountries();
            }

            public static bool IsExist(int CountryIDToFind)
            {
                return AccessCountriesData.IsExist(CountryIDToFind);
            }

        }

        public static class MangeDetainingRecords
        {
            public static DetainLicenseRecord Add(DetainLicenseRecord DetainedLicenseToAdd)
            {
                if (DetainedLicenseToAdd == null)
                    return null;

                if (DetainedLicenseToAdd.DetainID != -1)
                    return null;

                if (DetainedLicenseToAdd.FineFees < 0)
                    return null;

                if (null != DetainedLicenseToAdd.ReleasedByUserID ||
                    null != DetainedLicenseToAdd.ReleaseDate ||
                    null != DetainedLicenseToAdd.ReleaseApplicationID)
                    return null;

                if (AccessLicenseData.IsExist(DetainedLicenseToAdd.LicenseID))
                    return null;

                if (AccessDetainingLicenseRecordData.IsLicenseCurrenlyDetained(DetainedLicenseToAdd.LicenseID))
                    return null;


                DetainedLicenseToAdd.CreatedByUserID = LogedInUser.UserID;
                DetainedLicenseToAdd.DetainDate = DateTime.Now;

                    DataAccessTier.DTDetainLicenseRecord U = AccessDetainingLicenseRecordData.AddNewDetainedLicense
                        (DetainedLicenseToAdd.LicenseID, DetainedLicenseToAdd.DetainDate, DetainedLicenseToAdd.FineFees, DetainedLicenseToAdd.CreatedByUserID
                        , DetainedLicenseToAdd.ReleaseDate, DetainedLicenseToAdd.ReleasedByUserID, DetainedLicenseToAdd.ReleaseApplicationID);
                
                return U == null ? null : U.ToDetainLicenseRecord();
                
            }

            public static bool UpdateFineFee(int DetainedRecordIDToUpdate,decimal NewFineFee)
            {
                if (DetainedRecordIDToUpdate == -1)
                    return false;

                DetainLicenseRecord Record = AccessDetainingLicenseRecordData.Find(DetainedRecordIDToUpdate).ToDetainLicenseRecord();

                if (Record == null)
                    return false;

                if (Record.IsReleased)
                    return false;

                return AccessDetainingLicenseRecordData.UpdateFineFee(DetainedRecordIDToUpdate, NewFineFee);
            }

            public static bool UpdateLicenseFineFee(int DetainedLicenseID, decimal NewFineFee)
            {
                if (DetainedLicenseID == -1)
                    return false;

                DetainLicenseRecord DetainedRecordToUpdate =
                    AccessDetainingLicenseRecordData.FindLastDetainingOfLicense(DetainedLicenseID).ToDetainLicenseRecord();

                if (DetainedRecordToUpdate == null)
                    return false;

                return UpdateFineFee(DetainedRecordToUpdate.DetainID, NewFineFee);
            }

            public static bool ReleaseLicenseWithDetainingRecord(int DetainedRecordID)
            {
                if (DetainedRecordID < 0)
                    return false;

                DetainLicenseRecord RecordToRelease = AccessDetainingLicenseRecordData.Find(DetainedRecordID).ToDetainLicenseRecord();

                if (RecordToRelease == null)
                    return false;

                // Nothing to update
                if (RecordToRelease.IsReleased)
                    return false; 

                // Have Create an application for it first 
                if (RecordToRelease.ReleaseApplicationID == null)
                    return false;

                Application ApplicationToReleaseBaisedOn =
                    AccessApplicationData.Find((int)RecordToRelease.ReleaseApplicationID).ToApplication();

                if (ApplicationToReleaseBaisedOn == null) // It won't be --> just in case
                    return false;

                if (ApplicationType.ReleaseLicense
                     != ApplicationToReleaseBaisedOn.ApplicationType) 
                    return false;   

                if (ApplicationStatus.New !=
                     ApplicationToReleaseBaisedOn.ApplicationStatus)
                    return false;

                if (ApplicationToReleaseBaisedOn.PaidFees
                     != SettingsClass.Application.GetBaseApplicationFees(ApplicationType.ReleaseLicense)
                     + RecordToRelease.FineFees)
                    return false;

                if (ApplicationToReleaseBaisedOn.IsExpired)
                    return false;

                return AccessDetainingLicenseRecordData.ReleaseDetainingRecord(DetainedRecordID, LogedInUser.UserID);
            }

            public static bool ReleaseLicenseWithID(int DetainedLicenseID)
            {

                if (DetainedLicenseID == -1)
                    return false;

                DetainLicenseRecord DetainedRecordToRelease =
                    AccessDetainingLicenseRecordData.FindLastDetainingOfLicense(DetainedLicenseID).ToDetainLicenseRecord();

                if (DetainedRecordToRelease == null)
                    return false;

                return ReleaseLicenseWithDetainingRecord(DetainedLicenseID);
            }

            public static DetainLicenseRecord Find(int DetainID)
            {
                var DetainedLicense = AccessDetainingLicenseRecordData.Find(DetainID);

                if (DetainedLicense == null)
                    return null;
                else
                    return DetainedLicense.ToDetainLicenseRecord();
            }

            public static DataTable ListAllDetainedLicenses()
            {
                return AccessDetainingLicenseRecordData.ListAllDetainingRecords();
            }

            public static bool IsExist(int DetainIDToFind)
            {
                return AccessDetainingLicenseRecordData.IsExist(DetainIDToFind);
            }            
        }

        public static class MangeDrivers
        {
            public static Driver Add(Driver DriverToAdd)
            {
                if (DriverToAdd == null)
                    return null;

                if (DriverToAdd.DriverID < 0)
                    return null;

                if (DriverToAdd.PersonID < 0)
                    return null;

                if (!AccessPersonData.IsExist(DriverToAdd.PersonID))
                    return null;

                if (AccessDriverData.IsExistByPersonID(DriverToAdd.PersonID))
                    return null;
                    

                Driver U = AccessDriverData.AddNewDriver
                    (DriverToAdd.PersonID, LogedInUser.UserID, DateTime.Now).ToDriver();

                return U;

            }

            public static Driver Add(Driver DriverToAdd, Person PersonToAdd)
            {
                if (DriverToAdd == null || PersonToAdd == null)
                    return null;

                Person AddedPerson = MangePeople._Add(PersonToAdd);

                if (AddedPerson == null)
                    return null;
                
                DriverToAdd.PersonID = AddedPerson.PersonID;

                Driver AddedDriver = Add(DriverToAdd);

                if (AddedDriver == null)
                    AccessPersonData.DeletePerson(AddedPerson.PersonID);

                return AddedDriver;
            }

            public static int GetPersonID(int DriverID)
            {
                int? PersonID = AccessDriverData.GetDriverPersonID(DriverID);
                return PersonID == null ? -1 : (int)PersonID;
            }

            public static Driver Find(int DriverID)
            {
                var Driver = AccessDriverData.Find(DriverID);

                if (Driver == null)
                    return null;
                else
                    return Driver.ToDriver();
            }

            public static DateTime? GetDriverBirthDate(int DriverID)
            {
                DateTime? BirthDate = AccessDriverData.GetDriverBirthDate(DriverID);

                if (BirthDate == null)
                    return null;
                else
                    return (DateTime)BirthDate;
            }

            public static Driver FindByPersonID(int PersonID)
            {
                var Driver = AccessDriverData.FindByPersonID(PersonID);

                if (Driver == null)
                    return null;
                else
                    return Driver.ToDriver();
            }

            public static bool DoesDriverHaveLicenseOfClass(int DriverID, LicenseClass LicenseClass)
            {
                return AccessDriverData.DoesDriverHaveLicenseOfClass(DriverID, LicenseClass);
            }

            public static int? GetDriverID(int PersonID)
            {
                return AccessDriverData.GetDriverID(PersonID);
            }

            public static DataTable ListAllDrivers()
            {
                return AccessDriverData.ListAllDrivers();
            }

            public static bool IsExist(int DriverIDToFind)
            {
                return AccessDriverData.IsExist(DriverIDToFind);
            }

            public static bool IsExistByPersonID(int PersonIDToFind)
            {
                return AccessDriverData.IsExistByPersonID(PersonIDToFind);
            }

            public static string GetDriverImagePath(int DriverIDToFind)
            {
                return AccessDriverData.GetDriverImagePath(DriverIDToFind);
            }
        }

        public static class MangeInternationalLicenses
        {
            public static InternationalLicense Add(InternationalLicense InternationalLicenseToAdd)
            {
                if (InternationalLicenseToAdd == null)
                    return null;

                if (InternationalLicenseToAdd.InternationalLicenseID != -1)
                    return null;

                Application ApplicationToCreateBasedOn =
                    AccessApplicationData.Find(InternationalLicenseToAdd.ApplicationID).ToApplication(); 

                if (ApplicationToCreateBasedOn == null)
                    return null;

                if (ApplicationToCreateBasedOn.ApplicantPersonID
                     != AccessDriverData.GetDriverPersonID(InternationalLicenseToAdd.DriverID))
                    return null;

                if (ApplicationType.IssuingInternationalLicense
                     != ApplicationToCreateBasedOn.ApplicationType)
                    return null;

                if (ApplicationStatus.New 
                    != ApplicationToCreateBasedOn.ApplicationStatus)
                    return null;

                if (ApplicationToCreateBasedOn.IsExpired)
                    return null;

                if (ApplicationToCreateBasedOn.PaidFees
                     != MangeApplications.Fees.GetToatlApplicationFees(ApplicationType.IssuingInternationalLicense))
                    return null;

                License LocalLicnseToCreateBasedOn =
                    AccessLicenseData.Find(InternationalLicenseToAdd.IssuedUsingLocalLicenseID).ToLicense();

                if (LocalLicnseToCreateBasedOn == null)
                    return null;

                // If it is detained it won't be active
                if (!LocalLicnseToCreateBasedOn.IsActive)
                    return null;

                if (LicenseClass.RegularCar
                    != LocalLicnseToCreateBasedOn.LicenseClass)
                    return null;

                if (LocalLicnseToCreateBasedOn.IsExpired)
                    return null;

                InternationalLicenseToAdd.CreatedByUserID = LogedInUser.UserID;
                InternationalLicenseToAdd.IssueDate = DateTime.Now;
                InternationalLicenseToAdd.IsActive = true;

                if (DateTime.Now.Subtract(LocalLicnseToCreateBasedOn.ExpirationDate) 
                    < SettingsClass.InternationalLicense.InternationalLicenseYears)
                    
                    if (SettingsClass.InternationalLicense.AllowInternationalLicenseExpirationDateToByPassLocalOne)
                        InternationalLicenseToAdd.ExpirationDate =
                            DateTime.Now + SettingsClass.InternationalLicense.InternationalLicenseYears;
                    else
                        InternationalLicenseToAdd.ExpirationDate =
                            LocalLicnseToCreateBasedOn.ExpirationDate;


                int? OldInternationalLicenseID =
                    AccessInternationalLicenseData.DoesDriverHaveActiveLicense(InternationalLicenseToAdd.InternationalLicenseID);

                InternationalLicense U = AccessInternationalLicenseData.AddNewInternationalLicense
                    (InternationalLicenseToAdd.ApplicationID, InternationalLicenseToAdd.DriverID,
                        InternationalLicenseToAdd.IssuedUsingLocalLicenseID, InternationalLicenseToAdd.IssueDate,
                        InternationalLicenseToAdd.ExpirationDate, InternationalLicenseToAdd.IsActive,
                        InternationalLicenseToAdd.CreatedByUserID).ToInternationalLicense();

                if (U != null)
                {
                    AccessApplicationData.CompleteApplication(InternationalLicenseToAdd.ApplicationID);

                    if (OldInternationalLicenseID != -1 && OldInternationalLicenseID != null)
                        AccessInternationalLicenseData.DeactivateLicense((int)OldInternationalLicenseID);
                }

                return U;
            }

            public static bool UpdateAcvtiveStatus(int InternationalLicenseIDToUpdate,bool ActiveStatus)
            {
                if (InternationalLicenseIDToUpdate < 0)
                    return false;

                if (!AccessInternationalLicenseData.IsExist(InternationalLicenseIDToUpdate))
                    return false;
                
                return AccessInternationalLicenseData.UpdateAcvtiveStatus(InternationalLicenseIDToUpdate,ActiveStatus);
            }

            public static bool UpdateInternationalLicenseExpirationDate(int InternationalLicenseIDToUpdate, DateTime ExpirationDate)
            {
                if (InternationalLicenseIDToUpdate < 0)
                    return false;

                if (!AccessInternationalLicenseData.IsExist(InternationalLicenseIDToUpdate))
                    return false;

                return AccessInternationalLicenseData.UpdateExpirationDate(InternationalLicenseIDToUpdate, ExpirationDate);
            }

            public static InternationalLicense Find(int InternationalLicenseID)
            {
                var InternationalLicense = AccessInternationalLicenseData.Find(InternationalLicenseID);

                if (InternationalLicense == null)
                    return null;
                else
                    return InternationalLicense.ToInternationalLicense();
            }

            public static InternationalLicense FindByApplicationID(int ApplicationID)
            {
                var InternationalLicense = AccessInternationalLicenseData.FindByApplicationID(ApplicationID);

                if (InternationalLicense == null)
                    return null;
                else
                    return InternationalLicense.ToInternationalLicense();
            }

            public static DataTable ListAllInternationalLicenses()
            {
                return AccessInternationalLicenseData.ListAllInternationalLicenses();
            }

            public static bool IsExist(int InternationalLicenseIDToFind)
            {
                return AccessInternationalLicenseData.IsExist(InternationalLicenseIDToFind);
            }

            public static DateTime GetInternationalLicenseExpiretionDate(int LocalLicenseID)
            {
                return AccessInternationalLicenseData.GetInternationalLicenseExpiretionDate(LocalLicenseID);
            }

        }

        public static class MangeLicenses
        {
            public static License Add(License LicenseToAdd)
            {
                if (LicenseToAdd == null)
                    return null;

                if (LicenseToAdd.LicenseID != -1)
                    return null;

                Person DriversPersonalInfo = AccessPersonData.FindByDriverID(LicenseToAdd.DriverID).ToPerson();

                if (!SettingsClass.License.IsDriverOledEnough(DriversPersonalInfo.DateOfBirth, LicenseToAdd.LicenseClass))
                    return null;

                if (AccessDriverData.DoesDriverHaveLicenseOfClass(LicenseToAdd.DriverID, LicenseToAdd.LicenseClass))
                    return null;


                LocalDrivingLicenseApplication LocalApplicationToAddBasedOn = 
                    AccessLocalDrivingLicenseApplicationData.Find(LicenseToAdd.LocalDrivingLicenseApplicationID).ToLocalDrivingLicenseApplication();

                if (LocalApplicationToAddBasedOn == null)
                    return null;

                if (LocalApplicationToAddBasedOn.LicenseClass != LicenseToAdd.LicenseClass)
                    return null;


                if (LocalApplicationToAddBasedOn.EyeTestID == null ||
                     LocalApplicationToAddBasedOn.DrivingTestID == null ||
                      LocalApplicationToAddBasedOn.TheoritecalTestID == null ||
                       string.IsNullOrEmpty(LocalApplicationToAddBasedOn.IdentificationPhotoPath) ||
                        string.IsNullOrEmpty(LocalApplicationToAddBasedOn.DrivingCourseCertificatePhotoPath))
                    return null;


                Application ApplicationToAddBasedOn = 
                    AccessApplicationData.Find(LocalApplicationToAddBasedOn.ApplicationID).ToApplication();

                if (ApplicationToAddBasedOn == null)
                    return null;

                if (ApplicationType.IssuingInternationalLicense
                     != ApplicationToAddBasedOn.ApplicationType)
                    return null;

                if (ApplicationStatus.New
                     != ApplicationToAddBasedOn.ApplicationStatus)
                    return null;

                if (ApplicationToAddBasedOn.ApplicantPersonID
                    != DriversPersonalInfo.PersonID)
                    return null;

                if (ApplicationToAddBasedOn.PaidFees 
                     != MangeApplications.Fees.GetIssuingNewLicenseApplicationTotalFees(LicenseToAdd.LicenseClass))
                    return null;

                if (ApplicationToAddBasedOn.IsExpired)
                    return null;

                // // Tests // //

                EyeTest EyeTest = AccessEyeTestData.Find((int)LocalApplicationToAddBasedOn.EyeTestID).ToEyeTest();
                DrivingTest DrivingTest = AccessDrivingTestData.Find((int)LocalApplicationToAddBasedOn.DrivingTestID).ToDrivingTest();
                TheoreticalTest TheoreticalTest = AccessTheoreticalTestData.Find((int)LocalApplicationToAddBasedOn.TheoritecalTestID).ToTheoreticalTest();


                if (EyeTest == null ||
                    DrivingTest == null ||
                    TheoreticalTest == null)
                    return null;


                    // Eye //
                if (EyeTest.IsExpired)
                    return null;

                if (!EyeTest.IsFeesPaied)
                    return null;

                if (EyeTest.TestResult != true)
                    return null;


                    // Driving //
                if (DrivingTest.IsExpired)
                    return null;

                if (!DrivingTest.IsFeesPaied)
                    return null;

                if (DrivingTest.TestResult != true)
                    return null;

                if (DrivingTest.TestClass 
                    != LicenseToAdd.LicenseClass)
                    return null;

                        // theoretical //
                if (TheoreticalTest.IsExpired)
                    return null;

                if (!TheoreticalTest.IsFeesPaied)
                    return null;

                if (TheoreticalTest.TestResult != true)
                    return null;

                if (TheoreticalTest.TestClass 
                    != LicenseToAdd.LicenseClass)
                    return null;

                    // ID & Course Copies // 
                if (!GeneralFunctions.IsValidImage(LocalApplicationToAddBasedOn.IdentificationPhotoPath))
                    return null;

                if (!GeneralFunctions.IsValidImage(LocalApplicationToAddBasedOn.DrivingCourseCertificatePhotoPath))
                    return null;

                License U = AccessLicenseData.AddNewLicense
                    (LicenseToAdd.LocalDrivingLicenseApplicationID, LicenseToAdd.DriverID,
                    LicenseToAdd.LicenseClass, DateTime.Now,
                    DateTime.Now + SettingsClass.License.HowMuchTimeToExpier(LicenseToAdd.LicenseClass),
                    LicenseToAdd.Notes,true, LicenseToAdd.IssueReason,
                    LogedInUser.UserID).ToLicense();

                return U;

            }

            /*public static bool UpdateMote(License LicenseToUpdate) <every Editable thing can be independently>
            {
                if (LicenseToUpdate.LicenseID == -1)
                    return false;

                if (!AccessLicenseData.IsExist(LicenseToUpdate.LicenseID))
                { return false; }

                DTLicense OldLicense = AccessLicenseData.Find(LicenseToUpdate.LicenseID);

                if (OldLicense.LocalDrivingLicenseApplicationID != LicenseToUpdate.LocalDrivingLicenseApplicationID ||
                     OldLicense.DriverID != LicenseToUpdate.DriverID ||
                      OldLicense.LicenseClass != LicenseToUpdate.LicenseClass ||
                       OldLicense.IssueDate != LicenseToUpdate.IssueDate ||
                        OldLicense.ExpirationDate != LicenseToUpdate.ExpirationDate ||
                         OldLicense.IssueReason != LicenseToUpdate.IssueReason ||
                          OldLicense.IsActive != LicenseToUpdate.IsActive)
                    return false;

                if (OldLicense.Notes == LicenseToUpdate.Notes)
                { return true; }


                return AccessLicenseData.UpdateLicense(LicenseToUpdate.LicenseID, LicenseToUpdate.LocalDrivingLicenseApplicationID, LicenseToUpdate.DriverID,
                    LicenseToUpdate.LicenseClass, LicenseToUpdate.IssueDate, LicenseToUpdate.ExpirationDate, LicenseToUpdate.Notes,
                    LicenseToUpdate.IsActive, LicenseToUpdate.IssueReason, LicenseToUpdate.CreatedByUserID);
            }
            */
            
            public static bool UpdateAcvtiveStatus(int LicenseIDToUpdate, bool ActiveStatus)
            {
                if (LicenseIDToUpdate < 0)
                    return false;

                if (!AccessLicenseData.IsExist(LicenseIDToUpdate))
                    return false;

                return AccessLicenseData.UpdateActivationStatus(LicenseIDToUpdate, ActiveStatus);
            }

            public static bool UpdateNote(int LicenseIDToUpdate, string NewNote)
            {
                if (LicenseIDToUpdate < 0)
                    return false;

                if (!AccessLicenseData.IsExist(LicenseIDToUpdate))
                    return false;

                return AccessLicenseData.UpdateNote(LicenseIDToUpdate, NewNote);
            }

            public static bool UpdateExpirationDate(int LicenseIDToUpdate, DateTime NewDate)
            {
                if (LicenseIDToUpdate < 0)
                    return false;

                if (!AccessLicenseData.IsExist(LicenseIDToUpdate))
                    return false;

                return AccessLicenseData.UpdateExpirationDate(LicenseIDToUpdate, NewDate);
            }

            public static License Find(int LicenseID)
            {
                DTLicense License = AccessLicenseData.Find(LicenseID);

                if (License == null)
                    return null;
                else
                    return License.ToLicense();
            }

            public static License FindLastActiveLicense(int DriverID, LicenseClass LicenseClass)
            {
                DTLicense License = AccessLicenseData.FindTheActiveLicense(DriverID, LicenseClass);

                return License == null ? null : License.ToLicense();
            }

            public static bool DoesDriverHaveLicenseOfClass(int LicenseID, LicenseClass LicenseClass)
            {
                return AccessDriverData.DoesDriverHaveLicenseOfClass(LicenseID, LicenseClass);
            }

            public static DataTable ListAllLicenses()
            {
                return AccessLicenseData.ListAllLicenses();
            }

            public static bool IsExist(int LicenseIDToFind)
            {
                return AccessLicenseData.IsExist(LicenseIDToFind);
            }

            public static bool IsCurrentlyDetained(int LicenseIDToCheck)
            {
                return DataAccessTier.AccessDetainingLicenseRecordData.IsLicenseCurrenlyDetained(LicenseIDToCheck);
            }

            public static DateTime GetExpirationDateOfLicense(int LicenseID)
            {
                return AccessLicenseData.GetExpirationDateOfLicense(LicenseID);
            }
       
        }

        public static class MangeLocalDrivingLicenseApplications
        {
            internal static int _Add(LocalDrivingLicenseApplication LocalAppToAdd)
            {
                Application MainApplication = AccessApplicationData.Find(LocalAppToAdd.ApplicationID).ToApplication();

                if (MainApplication == null)
                    return -1;                

                if (ApplicationStatus.New != MainApplication.ApplicationStatus)
                    return -1;

                if (MainApplication.IsExpired)
                    return -1;

                if (LocalAppToAdd.EyeTestID != null)
                {
                    EyeTest EyeTest =
                        AccessEyeTestData.Find((int)LocalAppToAdd.EyeTestID).ToEyeTest();

                    if (EyeTest == null)
                        return -1;

                    if (EyeTest.IsExpired)
                        return -1;
                }

                if (LocalAppToAdd.DrivingTestID != null)
                {
                    DrivingTest DrivingTest =
                        AccessDrivingTestData.Find((int)LocalAppToAdd.DrivingTestID).ToDrivingTest();

                    if (DrivingTest == null)
                        return -1;

                    if (DrivingTest.IsExpired)
                        return -1;
                }

                if (LocalAppToAdd.TheoritecalTestID != null)
                {
                    TheoreticalTest TheoritecalTest =
                        AccessTheoreticalTestData.Find((int)LocalAppToAdd.TheoritecalTestID).ToTheoreticalTest();

                    if (TheoritecalTest == null)
                        return -1;

                    if (TheoritecalTest.IsExpired)
                        return -1;
                }

                LocalDrivingLicenseApplication U = AccessLocalDrivingLicenseApplicationData.AddNew
                    (LocalAppToAdd.ApplicationID, LocalAppToAdd.LicenseClass,LocalAppToAdd.EyeTestID,
                      LocalAppToAdd.TheoritecalTestID,LocalAppToAdd.DrivingTestID, LocalAppToAdd.IdentificationPhotoPath,
                        LocalAppToAdd.DrivingCourseCertificatePhotoPath).ToLocalDrivingLicenseApplication();

                return U == null ? -1 : U.LocalDrivingLicenseApplicationID;

            }

            public static bool Update(LocalDrivingLicenseApplication LocalAppToUpdate)
            {
                if (LocalAppToUpdate == null)
                    return false;

                if (LocalAppToUpdate.LocalDrivingLicenseApplicationID < 0)
                    return false;

                LocalDrivingLicenseApplication OldLocalApp =
                    AccessLocalDrivingLicenseApplicationData.Find(LocalAppToUpdate.LocalDrivingLicenseApplicationID).ToLocalDrivingLicenseApplication();


            if (OldLocalApp.ApplicationID != LocalAppToUpdate.ApplicationID ||
                 OldLocalApp.LicenseClass != LocalAppToUpdate.LicenseClass)
                    return false;

                Application MainApplication = AccessApplicationData.Find(OldLocalApp.ApplicationID).ToApplication();

                if (MainApplication == null)
                    return false;

                if (ApplicationStatus.New
                     != MainApplication.ApplicationStatus)
                    return false;

                if (LocalAppToUpdate.CheckMainApplicationAndNulls(MainApplication.ApplicationType)!= true)
                    return false;


                EyeTest EyeTest = null;
                DrivingTest DrivingTest = null;
                TheoreticalTest TheoreticalTest = null;

                if (OldLocalApp.EyeTestID != LocalAppToUpdate.EyeTestID)
                    if (LocalAppToUpdate.EyeTestID != null)
                    {
                        EyeTest OldEyeTest = null;
                        if (OldLocalApp.EyeTestID != null)
                        {
                            OldEyeTest = AccessEyeTestData.Find((int)OldLocalApp.EyeTestID).ToEyeTest();

                            switch (OldEyeTest.TestResult)
                            {
                                case false:
                                    break;

                                case true:
                                    if (OldEyeTest.IsExpired)
                                        return false;
                                    break;

                                default: // null
                                    if (OldEyeTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte < DateTime.Now)
                                        return false;
                                    break;
                            }                            
                        }

                        EyeTest = AccessEyeTestData.Find((int)LocalAppToUpdate.EyeTestID).ToEyeTest();

                        if (EyeTest == null)
                            return false;

                        if (EyeTest.PersonID != MainApplication.ApplicantPersonID)
                            return false;

                        switch (EyeTest.TestResult)
                        {
                            case false:
                                return false;

                            case true:
                                if (!OldEyeTest.IsExpired)
                                    return false;
                                break;

                            default: // null
                                if (OldEyeTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte > DateTime.Now)
                                    return false;
                                break;
                        }
                    }

                if (OldLocalApp.DrivingTestID != LocalAppToUpdate.DrivingTestID)
                    if (LocalAppToUpdate.DrivingTestID != null)
                    {
                        DrivingTest OldDrivingTest = null;
                        
                        if (OldLocalApp.DrivingTestID != null)
                        {
                            OldDrivingTest = AccessDrivingTestData.Find((int)OldLocalApp.DrivingTestID).ToDrivingTest();

                            switch (OldDrivingTest.TestResult)
                            {
                                case false:
                                    break;

                                case true:
                                    if (!OldDrivingTest.IsExpired)
                                        return false;
                                    break;

                                default: // null
                                    if (OldDrivingTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte < DateTime.Now)
                                        return false;
                                    break;
                            }
                        }

                        DrivingTest = AccessDrivingTestData.Find((int)LocalAppToUpdate.DrivingTestID).ToDrivingTest();
                        
                        if (DrivingTest == null)
                            return false;

                        if (DrivingTest.PersonID != MainApplication.ApplicantPersonID)
                            return false;

                        if (DrivingTest.TestClass != LocalAppToUpdate.LicenseClass)
                            return false;

                        switch (DrivingTest.TestResult)
                        {
                            case false:
                                return false;

                            case true:
                                if (OldDrivingTest.IsExpired)
                                    return false;
                                break;

                            default: // null
                                if (OldDrivingTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte > DateTime.Now)
                                    return false;
                                break;
                        }


                    }

                if (OldLocalApp.TheoritecalTestID != LocalAppToUpdate.TheoritecalTestID)
                    if (LocalAppToUpdate.TheoritecalTestID != null)
                    {
                        TheoreticalTest OldTheoreticalTest = null;

                        if (OldLocalApp.TheoritecalTestID != null)
                        {
                            OldTheoreticalTest =
                                AccessTheoreticalTestData.Find((int)OldLocalApp.TheoritecalTestID).ToTheoreticalTest();

                            switch (OldTheoreticalTest.TestResult)
                            {
                                case false:
                                    break;

                                case true:
                                    if (!OldTheoreticalTest.IsExpired)
                                        return false;
                                    break;

                                default: // null
                                    if (OldTheoreticalTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte < DateTime.Now)
                                        return false;
                                    break;
                            }
                        }

                        TheoreticalTest = AccessTheoreticalTestData.Find((int)LocalAppToUpdate.TheoritecalTestID).ToTheoreticalTest();

                        if (TheoreticalTest == null)
                            return false;

                        if (TheoreticalTest.PersonID != MainApplication.ApplicantPersonID)
                            return false;

                        if (TheoreticalTest.TestClass != LocalAppToUpdate.LicenseClass)
                            return false;


                        switch (TheoreticalTest.TestResult)
                        {
                            case false:
                                return false;

                            case true:
                                if (OldTheoreticalTest.IsExpired)
                                    return false;
                                break;

                            default: // null
                                if (OldTheoreticalTest.AppointmentDate + SettingsClass.TestInfos.MaximumPeriodToSetTestResulte > DateTime.Now)
                                    return false;
                                break;
                        }
                    }

                if (!GeneralFunctions.IsValidImage(LocalAppToUpdate.IdentificationPhotoPath))
                    return false;

                if (!GeneralFunctions.IsValidImage(LocalAppToUpdate.DrivingCourseCertificatePhotoPath))
                    return false;
                
                return AccessLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                    (LocalAppToUpdate.LocalDrivingLicenseApplicationID, LocalAppToUpdate.ApplicationID,
                      LocalAppToUpdate.LicenseClass, LocalAppToUpdate.EyeTestID,
                       LocalAppToUpdate.TheoritecalTestID, LocalAppToUpdate.DrivingTestID,
                        LocalAppToUpdate.IdentificationPhotoPath, LocalAppToUpdate.DrivingCourseCertificatePhotoPath);
            }

            public static LocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
            {
                var LocalDrivingLicenseApplication = AccessLocalDrivingLicenseApplicationData.Find(LocalDrivingLicenseApplicationID);

                if (LocalDrivingLicenseApplication == null)
                    return null;
                else
                    return LocalDrivingLicenseApplication.ToLocalDrivingLicenseApplication();
            }

            public static LicenseClass? FindApplicationClass(int LocalDrivingLicenseApplicationID)
            {
                LicenseClass? Class = AccessLocalDrivingLicenseApplicationData.FindClass(LocalDrivingLicenseApplicationID);

                if (Class == null)
                    return null;
                else
                    return Class;
            }

            public static LocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
            {
                var LocalDrivingLicenseApplication = AccessLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID);

                if (LocalDrivingLicenseApplication == null)
                    return null;
                else
                    return LocalDrivingLicenseApplication.ToLocalDrivingLicenseApplication();
            }

            public static DataTable ListAllLocalDrivingLicenseApplications()
            {
                return AccessLocalDrivingLicenseApplicationData.ListAllLocalDrivingLicenseApplications();
            }

            public static bool IsExist(int LocalDrivingLicenseApplicationIDToFind)
            {
                return AccessLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationIDToFind);
            }

            public static bool IsExistByApplicationID(int ApplicationID)
            {
                return AccessLocalDrivingLicenseApplicationData.IsExistByApplicationID(ApplicationID);
            }
            
        }

        public static class MangeEyeTests
        {
            public static bool _CouldAttachEyeTestToApplicationID(int ApplicationID)
            {
                if (!AccessApplicationData.IsExist(ApplicationID))
                { return false; }

                ApplicationType Type = (ApplicationType)AccessApplicationData.GetApplicationType(ApplicationID);

                if (Type == ApplicationType.LicenseIssuance)
                {
                    if (AccessEyeTestData.IsExistByApplicationID(ApplicationID))
                        return false;

                    return true;
                }
                else if (Type == ApplicationType.RetakeTest)
                {
                    if (AccessEyeTestData.IsExistByApplicationID(ApplicationID) ||
                        AccessTheoreticalTestData.IsExist(ApplicationID) ||
                        AccessDrivingTestData.IsExist(ApplicationID))
                        return false;
                    else
                        return true;
                }
                else if(Type == ApplicationType.RenewDrivingLicense)
                {
                    if (!AccessEyeTestData.IsExistByApplicationID(ApplicationID))
                        return true;
                    else
                        return false;

                }

                return false;
            }

            public static bool CouldAttachEyeTestToApplicationID(int ApplicationID)
            {
                return _CouldAttachEyeTestToApplicationID(ApplicationID);
            }

            internal static int _Add(EyeTest EyeTestToAdd)
            {
                if (EyeTestToAdd == null)
                    return -1;

                if (EyeTestToAdd.TestID == -1)
                    return -1;

                if (EyeTestToAdd.TestResult != null)
                    return -1;

                if (EyeTestToAdd.ResultAddedByUserID != null)
                    return -1;

                // we can not tack more then the cost of the test //
                if (EyeTestToAdd.PaidFees > MangeEyeTests.GetTestFee(EyeTestToAdd.AppointmentDate))
                    return -1;

                if (EyeTestToAdd.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0))
                    return -1;

                if (EyeTestToAdd.PersonID != AccessApplicationData.GetPersonID(EyeTestToAdd.TestApplicationID))
                    return -1;

                DataAccessTier.DTEyeTest U = AccessEyeTestData.AddNewEyeTest
                    (EyeTestToAdd.PersonID, EyeTestToAdd.AppointmentDate, EyeTestToAdd.PaidFees, LogedInUser.UserID
                    , EyeTestToAdd.TestApplicationID, EyeTestToAdd.TestResult, EyeTestToAdd.Notes, EyeTestToAdd.ResultAddedByUserID);

                return U == null ? -1 : U.TestID;
            }

            public static bool Update(EyeTest EyeTestToUpdate)
            {
                if (EyeTestToUpdate == null)
                    return false;

                if (EyeTestToUpdate.TestID == -1)
                    return false;

                DTEyeTest OldTest = AccessEyeTestData.Find(EyeTestToUpdate.TestID);

                if (OldTest == null)
                    return false;

                if (OldTest.PersonID != EyeTestToUpdate.PersonID ||
                     OldTest.TestApplicationID != EyeTestToUpdate.TestApplicationID ||
                      OldTest.AppointmentMadeByUserID != EyeTestToUpdate.AppointmentMadeByUserID)
                    return false;

                if ((EyeTestToUpdate.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0)
                      || OldTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0))
                       && OldTest.AppointmentDate != EyeTestToUpdate.AppointmentDate)
                    return false;

                if (OldTest.TestResult != null
                    && EyeTestToUpdate.TestResult != OldTest.TestResult)
                    return false;

                decimal TestFee = MangeEyeTests.GetTestFee(EyeTestToUpdate.AppointmentDate);

                if (OldTest.TestResult != EyeTestToUpdate.TestResult)
                     EyeTestToUpdate.SetResult((bool)EyeTestToUpdate.TestResult, LogedInUser.UserID);

                if (EyeTestToUpdate.PaidFees > TestFee)
                    return false;

                if (EyeTestToUpdate.PaidFees < OldTest.PaidFees && EyeTestToUpdate.PaidFees < TestFee)
                    return false;


                if (EyeTestToUpdate.TestResult != null
                     && EyeTestToUpdate.PaidFees < General.SettingsClass.TestInfos.Eye.GetCashedFeeIfInDateRange(DateTime.Now))
                    return false;

                if (EyeTestToUpdate.ResultAddedByUserID != null && EyeTestToUpdate.PaidFees != TestFee)
                    return false;


                return AccessEyeTestData.UpdateEyeTest(EyeTestToUpdate.TestID, EyeTestToUpdate.PersonID,
                    EyeTestToUpdate.AppointmentDate, EyeTestToUpdate.PaidFees, EyeTestToUpdate.AppointmentMadeByUserID
                    , EyeTestToUpdate.TestApplicationID, EyeTestToUpdate.TestResult, EyeTestToUpdate.Notes, EyeTestToUpdate.ResultAddedByUserID,LogedInUser.UserID);
            }

            public static bool UpdatePaidFees(int EyeTestIDToUpdate, decimal NewPaidFees)
            {
                if (EyeTestIDToUpdate < 0)
                    return false;

                if (NewPaidFees < 0)
                    return false;

                EyeTest OldEyeTest = AccessEyeTestData.Find(EyeTestIDToUpdate).ToEyeTest();

                if (OldEyeTest == null)
                    return false;

                if (OldEyeTest.PaidFees == NewPaidFees)
                    return false;

                if (OldEyeTest.AppointmentDate < DateTime.Now)
                    return false;

                if (OldEyeTest.PaidFees > NewPaidFees)
                {
                    decimal Cost = MangeEyeTests.GetTestFee(OldEyeTest.AppointmentDate);

                    if (OldEyeTest.PaidFees < Cost)
                        return false;
                    if (NewPaidFees != Cost)
                 
                        return false;
                }

                return AccessEyeTestData.UpdatePaiedFee(EyeTestIDToUpdate, NewPaidFees);
            }

            public static bool UpdateAppointmentDate(int EyeTestIDToUpdate, DateTime NewAppointmentDate)
            {
                if (EyeTestIDToUpdate < 0)
                    return false;

                if (NewAppointmentDate < DateTime.Now)
                    return false;

                EyeTest OldEyeTest = AccessEyeTestData.Find(EyeTestIDToUpdate).ToEyeTest();

                if (OldEyeTest == null)
                    return false;

                if (OldEyeTest.TestResult != null)
                    return false;

                if (OldEyeTest.AppointmentDate < DateTime.Now)
                    return false;

                return AccessEyeTestData.UpdateAppointmentDate(EyeTestIDToUpdate, NewAppointmentDate,LogedInUser.UserID);
            }

            public static bool UpdateTestResult(int EyeTestIDToUpdate, bool NewResult)
            {
                if (EyeTestIDToUpdate < 0)
                    return false;

                EyeTest OldEyeTest = AccessEyeTestData.Find(EyeTestIDToUpdate).ToEyeTest();

                if (OldEyeTest == null)
                    return false;

                if (OldEyeTest.TestResult != null)
                    return false;

                if (OldEyeTest.AppointmentDate > DateTime.Now)
                    return false;

                if (OldEyeTest.DidTheTestPassWithoutAttending() == true)
                    return false;


                return AccessEyeTestData.UpdateTestResult(EyeTestIDToUpdate, NewResult, LogedInUser.UserID);
            }

            public static EyeTest Find(int TestID)
            {
                var EyeTest = AccessEyeTestData.Find(TestID);

                if (EyeTest == null)
                    return null;
                else
                    return EyeTest.ToEyeTest();
            }

            public static EyeTest FindByApplicationID(int ApplicationID)
            {
                var EyeTest = AccessEyeTestData.FindByApplicationID(ApplicationID);

                if (EyeTest == null)
                    return null;
                else
                    return EyeTest.ToEyeTest();
            }

            public static DateTime? GetAppointmentDate(int TestID)
            {
                var EyeTest = AccessEyeTestData.GetAppointmentDate(TestID);

                if (EyeTest == null)
                    return null;
                else
                    return EyeTest;
            }

            public static bool? IsExpired(int TestID)
            {
                DateTime? TestDate = GetAppointmentDate(TestID);

                if (TestDate == null)
                    return null;

                return (DateTime.Now.Subtract((DateTime)TestDate) >
                            SettingsClass.TestInfos.Eye.EyeTestExpirationPeriod);
            }

            public static DataTable ListAllEyeTests()
            {
                return AccessEyeTestData.ListAllEyeTests();
            }

            public static DataTable ListAllPersonEyeTests(int PersonID)
            {
                return AccessEyeTestData.ListAllPersonEyeTests(PersonID);
            }

            public static bool IsExist(int TestIDToFind)
            {
                return AccessEyeTestData.IsExist(TestIDToFind);
            }

            public static bool IsPassed(int TestIDToFind)
            {
                return AccessEyeTestData.IsPassed(TestIDToFind);
            }
        
            public static decimal GetTestFee(DateTime TestDate)
            {
                decimal? CashedFee =
                    SettingsClass.TestInfos.Eye.GetCashedFeeIfInDateRange(TestDate);

                if (CashedFee != null)
                    return (decimal)CashedFee;

                decimal? Fee = AccessTestsCost.Eye.GetFee(TestDate);

                if (Fee == null) // Will never just in case //
                    return -1;
                else
                    return (decimal)Fee;
            }
        }

        public static class MangeDrivingTests
        {
            public static bool _CouldAttachDrivingTestToApplicationID(int ApplicationID, LicenseClass TestClass)
            {
                if (!AccessApplicationData.IsExist(ApplicationID))
                    return false;

                ApplicationType Type = (ApplicationType)AccessApplicationData.GetApplicationType(ApplicationID);

                if (Type == ApplicationType.LicenseIssuance)
                {
                    if (AccessDrivingTestData.IsExistByApplicationID(ApplicationID))
                        return false;
                    if (AccessLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID).LicenseClass != TestClass)
                        return false;

                    return true;
                }
                else if (Type == ApplicationType.RetakeTest)
                {
                    if (AccessEyeTestData.IsExistByApplicationID(ApplicationID) || AccessDrivingTestData.IsExist(ApplicationID) || AccessTheoreticalTestData.IsExist(ApplicationID))
                        return false;
                    else
                        return true;
                }

                return false;
            }

            public static bool CouldAttachDrivingTestToApplicationID(int ApplicationID, LicenseClass TestClass)
            {
                return _CouldAttachDrivingTestToApplicationID(ApplicationID, TestClass);
            }

            internal static int _Add(DrivingTest TestToAdd)
            {
                if (TestToAdd == null)
                    return -1;

                if (TestToAdd.TestID != -1)
                    return -1;

                if (TestToAdd.TestResult != null)
                    return -1;

                if (TestToAdd.ResultAddedByUserID != null)
                    return -1;

                // we can not tack more then the cost of the test //
                if (TestToAdd.PaidFees > MangeDrivingTests.GetTestFee(TestToAdd.AppointmentDate, TestToAdd.TestClass))
                    return -1;

                if (DateTime.Now.Subtract(TestToAdd.AppointmentDate) > new TimeSpan(0, 0, 0, 0))
                    return -1;

                if (TestToAdd.PersonID != AccessApplicationData.GetPersonID(TestToAdd.TestApplicationID))
                    return -1;

                DataAccessTier.DTDrivingTest U = AccessDrivingTestData.AddNewTest
                    (TestToAdd.PersonID, TestToAdd.AppointmentDate, TestToAdd.PaidFees,
                        LogedInUser.UserID, TestToAdd.TestApplicationID, TestToAdd.TestResult,
                        TestToAdd.Notes, TestToAdd.ResultAddedByUserID, TestToAdd.TestClass);

                return U == null ? -1 : U.TestID;
            }

            public static bool Update(DrivingTest TestToUpdate)
            {
                if (TestToUpdate == null)
                    return false;

                if (TestToUpdate.TestID == -1)
                    return false;

                DTDrivingTest OldTest = AccessDrivingTestData.Find(TestToUpdate.TestID);

                if (OldTest == null)
                    return false;

                if (OldTest.PersonID != TestToUpdate.PersonID ||
                    OldTest.TestApplicationID != TestToUpdate.TestApplicationID ||
                    OldTest.TestClass != TestToUpdate.TestClass ||
                    OldTest.AppointmentMadeByUserID != TestToUpdate.AppointmentMadeByUserID)
                    return false;

                if (!(TestToUpdate.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0)
                        && OldTest.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0))
                        && OldTest.AppointmentDate != TestToUpdate.AppointmentDate)
                    return false;

                if (OldTest.TestResult != null
                     && TestToUpdate.TestResult != OldTest.TestResult)
                    return false;

                if (OldTest.TestResult != TestToUpdate.TestResult)
                    TestToUpdate.SetResult((bool)TestToUpdate.TestResult, LogedInUser.UserID);

                decimal TestFees = MangeDrivingTests.GetTestFee(TestToUpdate.AppointmentDate, TestToUpdate.TestClass);

                if (TestToUpdate.PaidFees > TestFees)
                    return false;

                if (TestToUpdate.PaidFees < OldTest.PaidFees && TestToUpdate.PaidFees < TestFees)
                    return false;

                if (TestToUpdate.TestResult != null
                    && TestToUpdate.PaidFees != TestFees)
                    return false;

                return AccessDrivingTestData.UpdateTest(TestToUpdate.TestID, TestToUpdate.PersonID,
                    TestToUpdate.AppointmentDate, TestToUpdate.PaidFees, TestToUpdate.AppointmentMadeByUserID, TestToUpdate.TestApplicationID,
                    TestToUpdate.TestResult, TestToUpdate.Notes, TestToUpdate.ResultAddedByUserID, TestToUpdate.TestClass, LogedInUser.UserID);
            }

            public static bool UpdatePaidFees(int TestIDToUpdate, decimal NewPaidFees)
            {
                if (TestIDToUpdate < 0)
                    return false;

                if (NewPaidFees < 0)
                    return false;

                DrivingTest OldTest = AccessDrivingTestData.Find(TestIDToUpdate).ToDrivingTest();

                if (OldTest == null)
                    return false;

                if (OldTest.PaidFees == NewPaidFees)
                    return false;

                if (OldTest.AppointmentDate < DateTime.Now)
                    return false;

                if (OldTest.PaidFees > NewPaidFees)
                {
                    decimal Cost = MangeDrivingTests.GetTestFee(OldTest.AppointmentDate,OldTest.TestClass);

                    if (OldTest.PaidFees < Cost)
                        return false;
                    if (NewPaidFees != Cost)

                        return false;
                }

                return AccessDrivingTestData.UpdatePaiedFee(TestIDToUpdate, NewPaidFees);
            }

            public static bool UpdateAppointmentDate(int TestIDToUpdate, DateTime NewAppointmentDate)
            {
                if (TestIDToUpdate < 0)
                    return false;

                if (NewAppointmentDate < DateTime.Now)
                    return false;

                DrivingTest OldTest = AccessDrivingTestData.Find(TestIDToUpdate).ToDrivingTest();

                if (OldTest == null)
                    return false;

                if (OldTest.TestResult != null)
                    return false;

                if (OldTest.AppointmentDate < DateTime.Now)
                    return false;

                return AccessDrivingTestData.UpdateAppointmentDate(TestIDToUpdate, NewAppointmentDate, LogedInUser.UserID);
            }

            public static bool UpdateTestResult(int TestIDToUpdate, bool NewResult)
            {
                if (TestIDToUpdate < 0)
                    return false;

                DrivingTest OldTest = AccessDrivingTestData.Find(TestIDToUpdate).ToDrivingTest();

                if (OldTest == null)
                    return false;

                if (OldTest.TestResult != null)
                    return false;

                if (OldTest.AppointmentDate > DateTime.Now)
                    return false;

                if (OldTest.DidTheTestPassWithoutAttending() == true)
                    return false;


                return AccessDrivingTestData.UpdateTestResult(TestIDToUpdate, NewResult, LogedInUser.UserID);
            }

            public static DrivingTest Find(int TestID)
            {
                var TestData = AccessDrivingTestData.Find(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData.ToDrivingTest();
            }

            public static DrivingTest FindByApplicationID(int TestID)
            {
                var TestData = AccessDrivingTestData.FindByApplicationID(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData.ToDrivingTest();
            }

            public static DateTime? GetAppointmentDate(int TestID)
            {
                var TestData = AccessDrivingTestData.GetAppointmentDate(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData;
            }

            public static bool? IsExpired(int TestID)
            {
                DateTime? TestDate = GetAppointmentDate(TestID);

                if (TestDate == null)
                    return null;

                return (DateTime.Now.Subtract((DateTime)TestDate) >
                            SettingsClass.TestInfos.Driving.ExpirationPeriod);
            }

            public static DataTable ListAllTests()
            {
                return AccessDrivingTestData.ListAllTests();
            }

            public static DataTable ListAllPersonDrivingTests(int PersonID)
            {
                return AccessDrivingTestData.ListAllPersonDrivingTests(PersonID);
            }

            public static bool IsExist(int TestIDToFind)
            {
                return AccessDrivingTestData.IsExist(TestIDToFind);
            }

            public static bool IsExistByApplicationID(int TestApplicationIDToFind)
            {
                return AccessDrivingTestData.IsExistByApplicationID(TestApplicationIDToFind);
            }

            public static bool IsPassed(int TestIDToFind)
            {
                return AccessDrivingTestData.IsPassed(TestIDToFind);
            }

            public static bool DeleteTest(int TestIDToFind)
            {
                return AccessDrivingTestData.DeleteTest(TestIDToFind);
            }

            public static decimal GetTestFee(DateTime TestDate, LicenseClass Class)
            {
                decimal? CashedFee =
                    SettingsClass.TestInfos.Driving.GetCashedFeeIfInDateRange(TestDate, Class);

                if (CashedFee != null) 
                    return (decimal)CashedFee;

                decimal? Fee = AccessTestsCost.Driving.GetFee(TestDate, Class);

                if (Fee == null) // will never just in case 
                    return -1;
                else
                    return (decimal)Fee;
            }
        }

        public static class MangeTheoreticalTests
        {
            public static bool _CouldAttachTheoreticalTestToApplicationID(int ApplicationID, LicenseClass TestClass)
            {
                if (!AccessApplicationData.IsExist(ApplicationID))
                { return false; }

                ApplicationType Type = (ApplicationType)AccessApplicationData.GetApplicationType(ApplicationID);

                if (Type == ApplicationType.LicenseIssuance)
                {
                    if (AccessTheoreticalTestData.IsExistByApplicationID(ApplicationID))
                        return false;
                    if (AccessTheoreticalTestData.FindByApplicationID(ApplicationID).TestClass != TestClass)
                        return false;

                    return true;
                }
                else if (Type == ApplicationType.RetakeTest)
                {
                    if (AccessEyeTestData.IsExistByApplicationID(ApplicationID) ||
                        AccessTheoreticalTestData.IsExist(ApplicationID) ||
                        AccessDrivingTestData.IsExist(ApplicationID))
                        return false;
                    else
                        return true;
                }

                return false;
            }

            public static bool CouldAttachTheoreticalTestToApplicationID(int ApplicationID, LicenseClass TestClass)
            {
                return _CouldAttachTheoreticalTestToApplicationID(ApplicationID, TestClass);
            }

            internal static int _Add(TheoreticalTest TestToAdd)
            {
                if (TestToAdd == null)
                    return -1;

                if (TestToAdd.TestID != -1)
                    return -1;

                if (TestToAdd.TestResult != null)
                    return -1;

                if (TestToAdd.ResultAddedByUserID != null)
                    return -1;

                // we can not tack more then the cost of the test //
                if (TestToAdd.PaidFees >
                     General.SettingsClass.TestInfos.Theoretical.GetCashedFeeIfInDateRange(TestToAdd.AppointmentDate,TestToAdd.TestClass))
                    return -1;

                if (DateTime.Now.Subtract(TestToAdd.AppointmentDate) > new TimeSpan(0, 0, 0, 0))
                    return -1;

                if (TestToAdd.PersonID != AccessApplicationData.GetPersonID(TestToAdd.TestApplicationID))
                    return -1;

                DataAccessTier.DTTheoreticalTest U = AccessTheoreticalTestData.AddNewTest
                    (TestToAdd.PersonID, TestToAdd.AppointmentDate, TestToAdd.PaidFees,
                        LogedInUser.UserID, TestToAdd.TestApplicationID, TestToAdd.TestResult,
                        TestToAdd.Notes, TestToAdd.ResultAddedByUserID, TestToAdd.TestClass);

                return U == null ? -1 : U.TestID;

            }

            public static bool Update(TheoreticalTest TestToUpdate)
            {
                if (TestToUpdate == null)
                    return false;

                if (TestToUpdate.TestID == -1)
                    return false;

                DTTheoreticalTest OldTest = AccessTheoreticalTestData.Find(TestToUpdate.TestID);

                if (OldTest == null)
                    return false;

                if (OldTest.PersonID != TestToUpdate.PersonID ||
                     OldTest.TestClass != TestToUpdate.TestClass ||
                      OldTest.TestApplicationID != TestToUpdate.TestApplicationID ||
                       OldTest.AppointmentMadeByUserID != TestToUpdate.AppointmentMadeByUserID)
                    return false;

                if (!(TestToUpdate.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0)
                        && OldTest.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0))
                         && OldTest.AppointmentDate != TestToUpdate.AppointmentDate)
                    return false;

                if (OldTest.TestResult != null
                     && TestToUpdate.TestResult != OldTest.TestResult)
                    return false;

                decimal TestFee = MangeTheoreticalTests.GetTestFee
                    (TestToUpdate.AppointmentDate, TestToUpdate.TestClass);

                if (TestToUpdate.PaidFees > TestFee)
                    return false;
               
                if (TestToUpdate.PaidFees < OldTest.PaidFees && TestToUpdate.PaidFees < TestFee)
                    return false;

                if (TestToUpdate.TestResult != null
                     && TestToUpdate.PaidFees != TestFee)
                    return false;

                if (TestToUpdate.TestResult != OldTest.TestResult)
                    TestToUpdate.SetResult((bool)TestToUpdate.TestResult, LogedInUser.UserID);

                return AccessTheoreticalTestData.UpdateTest(TestToUpdate.TestID, TestToUpdate.PersonID,
                    TestToUpdate.AppointmentDate, TestToUpdate.PaidFees, TestToUpdate.AppointmentMadeByUserID, TestToUpdate.TestApplicationID,
                    TestToUpdate.TestResult, TestToUpdate.Notes, TestToUpdate.ResultAddedByUserID, TestToUpdate.TestClass,LogedInUser.UserID);
            }

            public static bool UpdatePaidFees(int TestIDToUpdate, decimal NewPaidFees)
            {
                if (TestIDToUpdate < 0)
                    return false;

                if (NewPaidFees < 0)
                    return false;

                TheoreticalTest OldTest = AccessTheoreticalTestData.Find(TestIDToUpdate).ToTheoreticalTest();

                if (OldTest == null)
                    return false;

                if (OldTest.PaidFees == NewPaidFees)
                    return false;

                if (OldTest.AppointmentDate < DateTime.Now)
                    return false;

                if (OldTest.PaidFees > NewPaidFees)
                {
                    decimal Cost = MangeTheoreticalTests.GetTestFee(OldTest.AppointmentDate, OldTest.TestClass);

                    if (OldTest.PaidFees < Cost)
                        return false;
                    if (NewPaidFees != Cost)

                        return false;
                }

                return AccessTheoreticalTestData.UpdatePaiedFee(TestIDToUpdate, NewPaidFees);
            }

            public static bool UpdateAppointmentDate(int TestIDToUpdate, DateTime NewAppointmentDate)
            {
                if (TestIDToUpdate < 0)
                    return false;

                if (NewAppointmentDate < DateTime.Now)
                    return false;

                TheoreticalTest OldTest = AccessTheoreticalTestData.Find(TestIDToUpdate).ToTheoreticalTest();

                if (OldTest == null)
                    return false;

                if (OldTest.TestResult != null)
                    return false;

                if (OldTest.AppointmentDate < DateTime.Now)
                    return false;

                return AccessTheoreticalTestData.UpdateAppointmentDate(TestIDToUpdate, NewAppointmentDate, LogedInUser.UserID);
            }

            public static bool UpdateTestResult(int TestIDToUpdate, bool NewResult)
            {
                if (TestIDToUpdate < 0)
                    return false;

                TheoreticalTest OldTest = AccessTheoreticalTestData.Find(TestIDToUpdate).ToTheoreticalTest();

                if (OldTest == null)
                    return false;

                if (OldTest.TestResult != null)
                    return false;

                if (OldTest.AppointmentDate > DateTime.Now)
                    return false;

                if (OldTest.DidTheTestPassWithoutAttending() == true)
                    return false;


                return AccessTheoreticalTestData.UpdateTestResult(TestIDToUpdate, NewResult, LogedInUser.UserID);
            }

            public static TheoreticalTest Find(int TestID)
            {
                var TestData = AccessTheoreticalTestData.Find(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData.ToTheoreticalTest();
            }

            public static TheoreticalTest FindByApplicationID(int TestID)
            {
                var TestData = AccessTheoreticalTestData.FindByApplicationID(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData.ToTheoreticalTest();
            }

            public static DateTime? GetAppointmentDate(int TestID)
            {
                DateTime? AppointmentDate = AccessTheoreticalTestData.GetAppointmentDate(TestID);

                if (AppointmentDate == null)
                    return null;
                else
                    return AppointmentDate;
            }

            public static bool? IsExpired(int TestID)
            {
                TheoreticalTest TestDate = AccessTheoreticalTestData.Find(TestID).ToTheoreticalTest();

                if (TestDate == null)
                    return null;

                return (DateTime.Now.Subtract(TestDate.AppointmentDate) >
                            SettingsClass.TestInfos.Theoretical.TheoreticalTestExpirationPeriod);
            }

            public static DataTable ListAllTests()
            {
                return AccessTheoreticalTestData.ListAllTests();
            }

            public static DataTable ListAllPersonTheoreticalTests(int PersonID)
            {
                return AccessTheoreticalTestData.ListAllPersonTheoreticalTests(PersonID);
            }


            public static bool IsExist(int TestIDToFind)
            {
                return AccessTheoreticalTestData.IsExist(TestIDToFind);
            }

            public static bool IsExistByApplicationID(int TestApplicationIDToFind)
            {
                return AccessTheoreticalTestData.IsExistByApplicationID(TestApplicationIDToFind);
            }

            public static bool IsPassed(int TestIDToFind)
            {
                return AccessTheoreticalTestData.IsPassed(TestIDToFind);
            }

            public static bool DeleteTest(int TestIDToFind)
            {
                return AccessTheoreticalTestData.DeleteTest(TestIDToFind);
            }

            public static decimal GetTestFee(DateTime TestDate, LicenseClass Class)
            {
                decimal? CashedFee =
                    SettingsClass.TestInfos.Theoretical.GetCashedFeeIfInDateRange(TestDate, Class);

                if (CashedFee != null)
                    return (decimal)CashedFee;

                decimal? Fee = AccessTestsCost.Theoretical.GetFee(TestDate, Class);

                if (Fee == null) // will never --> just in case
                    return -1;
                else
                    return (decimal)Fee;
            }
        }

        // the complaxe Function will be out here. //

        // this funtion needs to be fucking deleted and handeled //
        /*public static bool EditLocalLicenseApplication(Application ApplicationToEdit, LocalDrivingLicenseApplication LocalLicenseApplication)
        {
            if (!MangeApplications.IsExist(ApplicationToEdit.ApplicationID))
                return false;

            LocalDrivingLicenseApplication OldLocalLicenseAplication = MangeLocalDrivingLicenseApplications.Find(LocalLicenseApplication.LocalDrivingLicenseApplicationID);

            if (OldLocalLicenseAplication.ApplicationID != LocalLicenseApplication.ApplicationID)
                return false;

            if (ApplicationToEdit.ApplicationID != LocalLicenseApplication.ApplicationID)
                return false;

            Application OldAppliction = MangeApplications.Find(ApplicationToEdit.ApplicationID);

            if (OldAppliction.ApplicantPersonID != ApplicationToEdit.ApplicantPersonID ||
                OldAppliction.ApplicationType != ApplicationToEdit.ApplicationType ||
                OldAppliction.ApplicationDate != ApplicationToEdit.ApplicationDate ||
                OldAppliction.CreatedByUserID != ApplicationToEdit.CreatedByUserID ||
                OldAppliction.PaidFees > ApplicationToEdit.PaidFees ||
                (OldAppliction.ApplicationStatus == ApplicationStatus.Canceled && ApplicationToEdit.ApplicationStatus != ApplicationStatus.Canceled) ||
                (OldAppliction.ApplicationStatus != ApplicationStatus.New && ApplicationToEdit.ApplicationStatus == ApplicationStatus.New)
               )
                return false;

            if (OldLocalLicenseAplication.LicenseClass != LocalLicenseApplication.LicenseClass)
                return false;


            if (LocalLicenseApplication.EyeTestID != null)
                if (!MangeEyeTests.IsExist(Convert.ToInt32(LocalLicenseApplication.EyeTestID)))
                    return false;

            if (LocalLicenseApplication.DrivingTestID != null)
                if (!MangeDrivingTests.IsExist(Convert.ToInt32(LocalLicenseApplication.DrivingTestID)))
                    return false;

            if (LocalLicenseApplication.TheoritecalTestID != null)
                if (!MangeTheoreticalTests.IsExist(Convert.ToInt32(LocalLicenseApplication.TheoritecalTestID)))
                    return false;


            if (OldAppliction.ApplicationStatus == ApplicationStatus.New && ApplicationToEdit.ApplicationStatus != ApplicationStatus.Completed)
            {
                if (ApplicationToEdit.PaidFees < SettingsClass.GetApplicationFees(ApplicationToEdit.ApplicationType))
                    return false;

                // check the Test Result if it is faild //
                if (LocalLicenseApplication.EyeTestID == null || LocalLicenseApplication.DrivingTestID == null || LocalLicenseApplication.TheoritecalTestID == null)
                    return false;



                if (!MangeEyeTests.IsPassed((int)LocalLicenseApplication.EyeTestID) || MangeDrivingTests.IsPassed((int)LocalLicenseApplication.DrivingTestID) || MangeTheoreticalTests.IsPassed((int)LocalLicenseApplication.TheoritecalTestID))
                    return false;

                // check the Tests dates if it is expired //
                if (DateTime.Now.Subtract((DateTime)MangeEyeTests.GetAppointmentDate((int)LocalLicenseApplication.EyeTestID)) > new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0))
                    return false;

                if (DateTime.Now.Subtract((DateTime)MangeDrivingTests.GetAppointmentDate((int)LocalLicenseApplication.DrivingTestID)) > new TimeSpan(SettingsClass.DrivingTestExpirationPeriod, 0, 0, 0))
                    return false;

                if (DateTime.Now.Subtract((DateTime)MangeTheoreticalTests.GetAppointmentDate((int)LocalLicenseApplication.TheoritecalTestID)) > new TimeSpan(SettingsClass.TheoreticalTestExpirationPeriod, 0, 0, 0))
                    return false;
            }

            if (!AccessApplicationData.UpdateApplication
                (ApplicationToEdit.ApplicationID, ApplicationToEdit.ApplicantPersonID, ApplicationToEdit.ApplicationDate,
                ApplicationToEdit.ApplicationType, ApplicationToEdit.ApplicationStatus,
                ApplicationToEdit.ApplicationStatus != OldAppliction.ApplicationStatus ? DateTime.Now : ApplicationToEdit.LastStatusDate,
                ApplicationToEdit.PaidFees, ApplicationToEdit.CreatedByUserID))
                return false;

            if (!AccessLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalLicenseApplication.LocalDrivingLicenseApplicationID, LocalLicenseApplication.ApplicationID, LocalLicenseApplication.LicenseClass, LocalLicenseApplication.EyeTestID, LocalLicenseApplication.TheoritecalTestID, LocalLicenseApplication.DrivingTestID))
            {
                AccessApplicationData.UpdateApplication(OldAppliction.ApplicationID, OldAppliction.ApplicantPersonID, OldAppliction.ApplicationDate,
                OldAppliction.ApplicationType, OldAppliction.ApplicationStatus, OldAppliction.LastStatusDate,
                OldAppliction.PaidFees, OldAppliction.CreatedByUserID);

                return false;
            }

            return true;
        }
        */
    }

}


// // Structures and Casting Extentions // //
namespace BusinessTier
{
    // // // // // // Final Structures // // // // //
    public class Country
    {
        internal Country(int CountryID, string CountryName)
        {
            this._CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public Country(string CountryName)
        {
            this._CountryID = -1;
            this.CountryName = CountryName;
        }

        private int _CountryID;
        public int CountryID { get { return _CountryID; } }
        public string CountryName;

    }

    public class DetainLicenseRecord
    {
        internal DetainLicenseRecord(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
             DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            this._DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
        }

        public DetainLicenseRecord(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID)
        {
            this._DetainID = -1;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
        }

        public void Release(DateTime ReleaseDate, int ReleasedByUserID)
        {
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
        }

        private int _DetainID;
        public int DetainID { get { return _DetainID; } }
        public int LicenseID;
        public DateTime DetainDate;
        public decimal FineFees;
        public int CreatedByUserID;

        private DateTime? _ReleaseDate = null;
        private int? _ReleasedByUserID = null;
        private int? _ReleaseApplicationID = null;

        public DateTime? ReleaseDate { get { return _ReleaseDate; } }
        public int? ReleasedByUserID { get { return _ReleasedByUserID; } }
        public int? ReleaseApplicationID { get { return _ReleaseApplicationID; } }

        public bool IsReleased
        {
            get
            {
                if (ReleasedByUserID == null && ReleaseDate == null)
                    return true;
                else
                    return false;
            }
        }


    }

    public class Application
    {

        internal Application(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, ApplicationType ApplicationTypeID,
            ApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this._ApplicationID = ApplicationID;
            this._ApplicantPersonID = ApplicantPersonID;
            this._ApplicationDate = ApplicationDate;
            this._ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this._CreatedByUserID = CreatedByUserID;

        }

        public Application(int ApplicantPersonID, DateTime ApplicationDate, ApplicationType ApplicationTypeID,
            ApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this._ApplicationID = -1;
            this._ApplicantPersonID = ApplicantPersonID;
            this._ApplicationDate = ApplicationDate;
            this._ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this._CreatedByUserID = CreatedByUserID;
        }

        public int ApplicationID { get { return _ApplicationID; } }
        private int _ApplicationID;
        public int ApplicantPersonID { get { return _ApplicantPersonID; } }
        private int _ApplicantPersonID;
        public DateTime ApplicationDate { get { return _ApplicationDate; } }
        private DateTime _ApplicationDate;
        public ApplicationType ApplicationType { get { return _ApplicationTypeID; } }
        private ApplicationType _ApplicationTypeID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }
        private int _CreatedByUserID;

        public ApplicationStatus ApplicationStatus;
        public DateTime LastStatusDate; //
        public decimal PaidFees;

        public bool IsExpired { get { return (DateTime.Now.Subtract(ApplicationDate) > SettingsClass.Application.ApplicationExpirationPeriod); } }
    }

    public class User
    {
        internal User(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this._UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
        }

        public User(int PersonID, string UserName, string Password, bool IsActive)
        {
            this._UserID = -1;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
        }

        private int _UserID;
        public int UserID { get { return _UserID; } }
        public int PersonID;
        public string UserName;
        public string Password;
        public bool IsActive;

    }

    public class Person
    {
        internal Person(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, Gendor Gendor, string Adress, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this._PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Adress;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
        }

        public Person(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, Gendor Gendor, string Adress, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this._PersonID = -1;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Adress;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
        }

        private int _PersonID;
        public int PersonID { get { return _PersonID; } }
        public string NationalNo;
        public string FirstName;
        public string SecondName;
        public string ThirdName;
        public string LastName;
        public DateTime DateOfBirth;
        public Gendor Gendor;
        public string Address;
        public string Phone;
        public string Email;
        public int NationalityCountryID;
        public string ImagePath;
    }

    public class Driver
    {

        internal Driver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this._DriverID = DriverID;
            this.PersonID = PersonID;
            this._CreatedByUserID = CreatedByUserID;
            this._CreatedDate = CreatedDate;

        }

        public Driver(int PersonID)
        {
            this._DriverID = -1;
            this.PersonID = PersonID;
        }

        private int _DriverID;
        public int DriverID { get { return _DriverID; } }
        public int PersonID;

        private int _CreatedByUserID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }

        private DateTime _CreatedDate;
        public DateTime CreatedDate { get { return _CreatedDate; } }

    }

    public class InternationalLicense
    {
        internal InternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
            , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this._InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            SetLicenseClass();
        }

        public InternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
            , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this._InternationalLicenseID = -1;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            SetLicenseClass();
        }


        private LicenseClass _LicenseClass;

        private void SetLicenseClass()
        {
            _LicenseClass = AccessInternationalLicenseData.GetInternationalLicenseClass(_InternationalLicenseID).Value;
        }

        private int _InternationalLicenseID;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }

        public int ApplicationID;
        public int DriverID;
        public int IssuedUsingLocalLicenseID;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public bool IsActive;
        public int CreatedByUserID;

        public LicenseClass GetLicenseClass()
        { return _LicenseClass; }


    }

    public class License
    {
        internal License(int LicenseID, int LocalDrivingLicenseApplicationID, int DriverID, LicenseClass LicenseClass
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, IssueReason IssueReason, int CreatedByUserID)
        {
            this._LicenseID = LicenseID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this._CreatedByUserID = CreatedByUserID;
        }

        public License(int LocalDrivingLicenseApplicationID, int DriverID, LicenseClass LicenseClass
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, IssueReason IssueReason)
        {
            this._LicenseID = -1;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
        }

        public bool IsExpired
        {
            get
            {
                return DateTime.Now.Subtract(ExpirationDate) < new TimeSpan(0, 0, 0, 0);
            }
        }
        private int _LicenseID;
        public int LicenseID { get { return _LicenseID; } }

        public int LocalDrivingLicenseApplicationID;
        public int DriverID;
        public LicenseClass LicenseClass;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public string Notes;
        public bool IsActive;
        public IssueReason IssueReason;

        private int _CreatedByUserID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }

    }

    public class LocalDrivingLicenseApplication
    {
        internal LocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClass, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID,
            string IdentificationPhotoPath, string DrivingCourseCertificatePhotoPath)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this._LicenseClass = LicenseClass;
            this.EyeTestID = EyeTestID;
            this.DrivingTestID = DrivingTestID;
            this.TheoritecalTestID = TheoritecalTestID;
            this.IdentificationPhotoPath = IdentificationPhotoPath;
            this.DrivingCourseCertificatePhotoPath = DrivingCourseCertificatePhotoPath;
        }

        public LocalDrivingLicenseApplication(int ApplicationID,
            LicenseClass LicenseClass, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID,
            string IdentificationPhotoPath, string DrivingCourseCertificatePhotoPath)
        {
            this._LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = ApplicationID;
            this._LicenseClass = LicenseClass;
            this.EyeTestID = EyeTestID;
            this.DrivingTestID = DrivingTestID;
            this.TheoritecalTestID = TheoritecalTestID;
            this.IdentificationPhotoPath = IdentificationPhotoPath;
            this.DrivingCourseCertificatePhotoPath = DrivingCourseCertificatePhotoPath;
        }

        private int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }

        public int ApplicationID;
        private LicenseClass _LicenseClass;
        public LicenseClass LicenseClass { get { return _LicenseClass; } }
        public int? EyeTestID;
        public int? DrivingTestID;
        public int? TheoritecalTestID;
        public string IdentificationPhotoPath;
        public string DrivingCourseCertificatePhotoPath;

        public bool? CheckMainApplicationAndNulls(ApplicationType? Type = null)
        {
            ApplicationType TypeToCheck;

            if (Type == null)
            {
                ApplicationType? TempType;
                TempType = AccessApplicationData.GetApplicationType(ApplicationID);

                if (TempType == null)
                    return null;
                else
                    TypeToCheck = (ApplicationType)TempType;
            }
            else
                TypeToCheck = (ApplicationType)Type;

            switch (TypeToCheck)
            {
                case ApplicationType.LicenseIssuance:
                    break;

                case ApplicationType.RenewDrivingLicense:
                    if (DrivingTestID != null ||
                         TheoritecalTestID != null ||
                          !string.IsNullOrEmpty(IdentificationPhotoPath) ||
                           !string.IsNullOrEmpty(DrivingCourseCertificatePhotoPath))
                        return false;
                    break;

                case ApplicationType.DamagedReplacement:
                    if (EyeTestID != null ||
                         DrivingTestID != null ||
                          TheoritecalTestID != null ||
                           !string.IsNullOrEmpty(IdentificationPhotoPath) ||
                            !string.IsNullOrEmpty(DrivingCourseCertificatePhotoPath))
                        return false;
                    break;

                case ApplicationType.MissingReplacement:
                    if (EyeTestID != null ||
                         DrivingTestID != null ||
                          TheoritecalTestID != null ||
                           !string.IsNullOrEmpty(IdentificationPhotoPath) ||
                            !string.IsNullOrEmpty(DrivingCourseCertificatePhotoPath))
                        return false;

                    break;
                default:
                    return false;
            }

            return true;
        }

    }

    public class Test
    {
        protected Test()
        { }

        public void SetResult(bool TestResult, int ResultAddedByUserID)
        {
            this._TestResult = TestResult;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        public bool? DidTheTestPassWithoutAttending()
        {
            if (TestResult != null)
                return false;
            else if (DateTime.Now.Subtract(AppointmentDate) < SettingsClass.TestInfos.MaximumPeriodToSetTestResulte)
                return true;
            else if (DateTime.Now.Subtract(AppointmentDate) < new TimeSpan(0, 0, 0))
                return null;
            else
                return false;
        }

        public DateTime AppointmentDate;
        public Decimal PaidFees;
        public string Notes;

        public int TestID { get { return _TestID; } }
        protected int _TestID;

        public int PersonID { get { return _PersonID; } }
        protected int _PersonID;

        public int TestApplicationID { get { return _TestApplicationID; } }
        protected int _TestApplicationID;

        public int AppointmentMadeByUserID { get { return _AppointmentMadeByUserID; } }
        protected int _AppointmentMadeByUserID;

        public bool? TestResult { get { return _TestResult; } }
        protected bool? _TestResult;

        public int? ResultAddedByUserID { get { return _ResultAddedByUserID; } }
        protected int? _ResultAddedByUserID;
    }

    public class EyeTest : Test
    {
        internal EyeTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            this._TestID = TestID;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        public EyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        public EyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, string Notes)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = null;
            this.Notes = Notes;
            this._ResultAddedByUserID = null;
        }

        public bool IsExpired 
        {
            get 
            {
                return DateTime.Now.Subtract((DateTime)AppointmentDate) >
                                    SettingsClass.TestInfos.Eye.EyeTestExpirationPeriod; 
            } 
        }

        public bool IsFeesPaied
        {
            get
            {
                return DVLDApp.MangeEyeTests.GetTestFee(AppointmentDate) != PaidFees;
            }
        }

    }

    public class DrivingTest : Test
    {
        internal DrivingTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID, LicenseClass TestClass)
        {
            this._TestID = TestID;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
            this.TestClass = TestClass;
        }

        public DrivingTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID, LicenseClass TestClass)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
            this.TestClass = TestClass;
        }

        public DrivingTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, string Notes, LicenseClass TestClass)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = null;
            this.Notes = Notes;
            this._ResultAddedByUserID = null;
            this.TestClass = TestClass;
        }

        public LicenseClass TestClass;


        public bool IsExpired { get { return DateTime.Now.Subtract((DateTime)AppointmentDate) >
                            SettingsClass.TestInfos.Driving.ExpirationPeriod; } }

        public bool IsFeesPaied
        {
            get
            {
                return DVLDApp.MangeDrivingTests.GetTestFee(AppointmentDate,TestClass) == PaidFees;
            }
        }
    
    }

    public class TheoreticalTest : Test
    {
        internal TheoreticalTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID, LicenseClass TestClass)
        {
            this._TestID = TestID;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
            this.TestClass = TestClass;
        }

        public TheoreticalTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID, LicenseClass TestClass)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
            this.TestClass = TestClass;
        }

        public TheoreticalTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, string Notes, LicenseClass TestClass)
        {
            this._TestID = -1;
            this._PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this._AppointmentMadeByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this._TestApplicationID = TestApplicationID;
            this._TestResult = null;
            this.Notes = Notes;
            this._ResultAddedByUserID = null;
            this.TestClass = TestClass;
        }

        public LicenseClass TestClass;


        public bool IsExpired { get { return DateTime.Now.Subtract((DateTime)AppointmentDate) >
                            SettingsClass.TestInfos.Theoretical.TheoreticalTestExpirationPeriod; } }

        public bool IsFeesPaied
        {
            get
            {
                return DVLDApp.MangeTheoreticalTests.GetTestFee(AppointmentDate, TestClass) == PaidFees;
            }
        }

    }


    //////////////////////  Extentions  /////////////////////////

    public static class Casting
    {
        // // // // // // // // // // // // // // From DTeir Structures == To == > Final Structures  // // // // // // // // // // // // // // // // //

        public static Country ToCountry(this DTCountry Country)
        { return Country == null ? null : new Country(Country.CountryID, Country.CountryName); }
        public static DetainLicenseRecord ToDetainLicenseRecord(this DTDetainLicenseRecord DetainedLicenseRecord)
        { return DetainedLicenseRecord == null ? null : new DetainLicenseRecord(DetainedLicenseRecord.DetainID, DetainedLicenseRecord.LicenseID, DetainedLicenseRecord.DetainDate, DetainedLicenseRecord.FineFees, DetainedLicenseRecord.CreatedByUserID, DetainedLicenseRecord.ReleaseDate, DetainedLicenseRecord.ReleasedByUserID, DetainedLicenseRecord.ReleaseApplicationID); }
        public static Application ToApplication(this DTApplication Application)
        { return Application == null ? null : new Application(Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID); }
        public static User ToUser(this DTUser User)
        { return User == null ? null : new User(User.UserID, User.PersonID, User.UserName, User.Password, User.IsActive); }
        public static Person ToPerson(this DTPerson Person)
        { return Person == null ? null : new Person(Person.PersonID, Person.NationalNo, Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName, Person.DateOfBirth, Person.Gendor, Person.Address, Person.Phone, Person.Email, Person.NationalityCountryID, SettingsClass.Paths.ProfilePhotos.ProfileImagesPath + Person.ImageFileName); }
        public static Driver ToDriver(this DTDriver Driver)
        { return Driver == null ? null : new Driver(Driver.DriverID, Driver.PersonID, Driver.CreatedByUserID, Driver.CreatedDate); }
        public static EyeTest ToEyeTest(this DTEyeTest EyeTest)
        { return EyeTest == null ? null : new EyeTest(EyeTest.TestID, EyeTest.PersonID, EyeTest.AppointmentDate, EyeTest.PaidFees, EyeTest.AppointmentMadeByUserID, EyeTest.TestApplicationID, EyeTest.TestResult, EyeTest.Notes, EyeTest.ResultAddedByUserID); }
        public static InternationalLicense ToInternationalLicense(this DTInternationalLicense InternationalLicense)
        { return InternationalLicense == null ? null : new InternationalLicense(InternationalLicense.InternationalLicenseID, InternationalLicense.ApplicationID, InternationalLicense.DriverID, InternationalLicense.IssuedUsingLocalLicenseID, InternationalLicense.IssueDate, InternationalLicense.ExpirationDate, InternationalLicense.IsActive, InternationalLicense.CreatedByUserID); }
        public static License ToLicense(this DTLicense License)
        { return License == null ? null : new License(License.LicenseID, License.LocalDrivingLicenseApplicationID, License.DriverID, License.LicenseClass, License.IssueDate, License.ExpirationDate, License.Notes, License.IsActive, License.IssueReason, License.CreatedByUserID); }
        public static LocalDrivingLicenseApplication ToLocalDrivingLicenseApplication(this DTLocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        {
            if (LocalDrivingLicenseApplication == null)
                return null;

            string CourseCertificatePath = null;

            if (LocalDrivingLicenseApplication.DrivingCourseCertificatePhotoFileName != null)
                  CourseCertificatePath = SettingsClass.Paths.DrivingCourseCertificatesCopes.ImagesPath 
                    + LocalDrivingLicenseApplication.DrivingCourseCertificatePhotoFileName;

            string IDFilePath = null;

            if (LocalDrivingLicenseApplication.IdentificationPhotoFileName != null)
                IDFilePath = SettingsClass.Paths.IdentificationsCopes.ImagesPath 
                    + LocalDrivingLicenseApplication.DrivingCourseCertificatePhotoFileName;

            return new LocalDrivingLicenseApplication(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplication.ApplicationID, LocalDrivingLicenseApplication.LicenseClass, LocalDrivingLicenseApplication.EyeTestID, LocalDrivingLicenseApplication.TheoritecalTestID, LocalDrivingLicenseApplication.DrivingTestID,
               SettingsClass.Paths.IdentificationsCopes.ImagesPath + LocalDrivingLicenseApplication.IdentificationPhotoFileName,CourseCertificatePath);
        }        
        public static TheoreticalTest ToTheoreticalTest(this DTTheoreticalTest Test)
        { return Test == null ? null : new TheoreticalTest(Test.TestID, Test.PersonID, Test.AppointmentDate, Test.PaidFees, Test.AppointmentMadeByUserID, Test.TestApplicationID, Test.TestResult, Test.Notes, Test.ResultAddedByUserID, Test.TestClass); }
        public static DrivingTest ToDrivingTest(this DTDrivingTest Test)
        { return Test == null ? null : new DrivingTest(Test.TestID, Test.PersonID, Test.AppointmentDate, Test.PaidFees, Test.AppointmentMadeByUserID, Test.TestApplicationID, Test.TestResult, Test.Notes, Test.ResultAddedByUserID, Test.TestClass); }


    }

}