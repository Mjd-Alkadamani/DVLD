using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessTier;
using General;

namespace BusinessTier
{
    public static class DVLDApp
    {
        public static void LoadTheDBConnection()
        {
            AccessUserData.LoadTheDBConnection();
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


        private static User _LogedInUser;

        public static User LogedInUser { get { return _LogedInUser; } }


        public static class MangePeople
        {

            public static bool Add(Person PersonToAdd)
            {

                if (PersonToAdd.PersonID != -1)
                { return false; }

                if (!string.IsNullOrEmpty(PersonToAdd.NationalNo) && !string.IsNullOrEmpty(PersonToAdd.FirstName) && !string.IsNullOrEmpty(PersonToAdd.SecondName) &&
                    !string.IsNullOrEmpty(PersonToAdd.LastName) && !string.IsNullOrEmpty(PersonToAdd.Address) && !string.IsNullOrEmpty(PersonToAdd.Phone) && !string.IsNullOrEmpty(PersonToAdd.ImagePath))
                { return false; }

                else
                {
                    DataAccessTier.DTPerson P = AccessPersonData.AddNewPerson(PersonToAdd.NationalNo, PersonToAdd.FirstName, PersonToAdd.SecondName,
                        PersonToAdd.ThirdName, PersonToAdd.LastName, PersonToAdd.DateOfBirth, PersonToAdd.Gendor, PersonToAdd.Address, PersonToAdd.Phone,
                        PersonToAdd.Email, PersonToAdd.NationalityCountryID, PersonToAdd.ImagePath);

                    return P != null;
                }
            }

            public static bool Update(Person PersonToUpdate)
            {

                if (PersonToUpdate.PersonID == -1)
                    return false;

                if (!AccessPersonData.IsExist(PersonToUpdate.PersonID))
                { return false; }

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

            public static bool IsExist(string NationalNumber)
            {
                return AccessPersonData.IsExist(NationalNumber);
            }

        }

        public static class MangeUsers
        {
            public static bool Add(User UserToAdd)
            {

                if (UserToAdd.UserID != -1)
                { return false; }

                if (string.IsNullOrEmpty(UserToAdd.UserName) || string.IsNullOrEmpty(UserToAdd.Password))
                { return false; }

                else
                {
                    DataAccessTier.DTUser U = AccessUserData.AddNewUser(UserToAdd.PersonID, UserToAdd.UserName, UserToAdd.Password, UserToAdd.IsActive);

                    return U != null;
                }
            }

            public static bool Update(User UserToUpdate)
            {
                if (UserToUpdate.UserID == -1)
                    return false;

                if (!AccessUserData.IsExist(UserToUpdate.UserID))
                { return false; }

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

            public static bool IsExist(string NationalNumber)
            {
                return AccessUserData.IsExist(NationalNumber);
            }

            public static bool IsExistByPersonID(int PersonID)
            {
                return AccessUserData.IsExistByPersonID(PersonID);
            }

            public static bool DeleteUser(int UserIDToDelete)
            {
                return AccessUserData.DeleteUser(UserIDToDelete);
            }

        }



        public static class MangeApplications
        {
            /*public static class Update -- To Be Done --
              {
                
                   checking if the appliction is expired
                   if (DateTime.Now.Subtract(ApplicationToAdd.ApplicationDate) > new TimeSpan(SettingsClass.ApplicationExpirationPeriod, 0, 0, 0))
                   return false;
                 
              }*/

            static class Fees
            {
                public static decimal? GetToatlApplicationFees(ApplicationType Type)
                {
                    decimal? ApplicationFees = SettingsClass.Application.GetLocalLicenseIssuingFees(Type);

                    if (ApplicationFees == null)
                        return null;
                    else
                        ApplicationFees += SettingsClass.Application.BaseApplicationFees;

                    
                    return ApplicationFees;
                }

                public static decimal GetIssuingNewLicenseApplicationTotalFees(LicenseClass Class)
                {
                    return SettingsClass.Application.BaseApplicationFees + SettingsClass.License.WhatIsTheFeeForLicense(Class);
                }

                public static decimal GetReleaseLicenseApplicationTotalFees(int LicenseIDToRelease)
                {
                    decimal ApplicationFees = SettingsClass.Application.BaseApplicationFees;

                    decimal? FineFee = AccessDetainedLicenseData.HowMuchTheFineFeeForLicense(LicenseIDToRelease);

                    if (FineFee == null)
                        return -1;
                    else
                        ApplicationFees += (Decimal)FineFee;

                    return ApplicationFees;
                }

            }

            public static class Add
            {
                internal static int _Application(Application ApplicationToAdd,LicenseClass? Class = null, int? DetainedLicenseID = null)
                {
                    if (ApplicationStatus.Canceled == ApplicationToAdd.ApplicationStatus)
                        return -1;

                    if (AccessApplicationData.FindDosePersonHaveVliedAppicationOfType(ApplicationToAdd.ApplicantPersonID, ApplicationToAdd.ApplicationType) != true)
                        return -1;
                    else if (ApplicationToAdd.ApplicationType == ApplicationType.LicenseIssuance) 
                    {
                        DateTime? PersonBirthDate = AccessPersonData.GetPersonBirthDate(ApplicationToAdd.ApplicantPersonID);

                        if (PersonBirthDate == null)
                            return -1;
                        else if (!SettingsClass.License.IsDriverOledEnough((DateTime)PersonBirthDate, (LicenseClass)Class)) 
                            return -1;
                    }
                    decimal TotalApplicationFees;

                    switch (ApplicationToAdd.ApplicationType)
                    {
                        case ApplicationType.LicenseIssuance:
                            if (Class == null)
                                return -1;
                            TotalApplicationFees = Fees.GetIssuingNewLicenseApplicationTotalFees((LicenseClass)Class);
                            break;
                        case ApplicationType.ReleaseLicense:
                            if (DetainedLicenseID == null)
                                return -1;
                            TotalApplicationFees = Fees.GetReleaseLicenseApplicationTotalFees((int)DetainedLicenseID);
                            break;
                        default:
                            TotalApplicationFees = (decimal)Fees.GetToatlApplicationFees(ApplicationToAdd.ApplicationType);
                            break;
                    }

                    // we can not take more than the applicetion cost
                    if (TotalApplicationFees > ApplicationToAdd.PaidFees) 
                        return -1;

                    var AddedApplication = DataAccessTier.AccessApplicationData.AddNewApplication(ApplicationToAdd.ApplicationID,
                        DateTime.Now, ApplicationToAdd.ApplicationType, ApplicationToAdd.ApplicationStatus,
                       DateTime.Now, ApplicationToAdd.PaidFees,
                        DVLDApp.LogedInUser.PersonID);

                    return AddedApplication == null ? -1 : AddedApplication.ApplicationID;
                }

                /*
                    License Issuance -- done
                    Retake Test -- done
                    Renew Driving License -- done
                    Missing Replacement -- doe
                    Damaged Replacement -- done
                    Release License -- done
                    Issuing International License --done
                */

                
                /* How To Use This Method --> Check the documentation */
                public static int IssuingLocalLicenseApplication(Application ApplicationToAdd, LicenseClass Class, EyeTest EyeTest, DrivingTest DrivingTest, TheoreticalTest TheoreticalTest)
                {
                    if (ApplicationToAdd == null ||
                        EyeTest == null ||
                        DrivingTest == null ||
                        TheoreticalTest == null ||
                        ApplicationToAdd.ApplicationID != -1)  
                        return -1;
                    
                    DateTime? PersonBirthDate = AccessPersonData.GetPersonBirthDate(ApplicationToAdd.ApplicantPersonID);

                    if (PersonBirthDate == null)
                        return -1;

                    if (!SettingsClass.License.IsDriverOledEnough((DateTime)PersonBirthDate, Class))
                        return -1;

                    int? DriverID = AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID);
                    if (DriverID == null)
                    {
                        if (AccessLicenseData.FindTheActiveLicense((int)DriverID, Class) == null)
                            return -1;
                    }
                    else
                        return -1;

                    if (EyeTest.TestID != -1)
                    { 
                        EyeTest = DataAccessTier.AccessEyeTestData.Find(EyeTest.TestID).ToEyeTest();
                        if (EyeTest.IsExpired)
                            return -1;
                        if (EyeTest.TestResult == false)
                            return -1;
                        if (EyeTest.DidTheTestPassWithoutAttending() == true)
                            return -1;
                    }
                    if (TheoreticalTest.TestID != -1)
                    {
                        TheoreticalTest = AccessTheoreticalTestData.Find(TheoreticalTest.TestID).ToTheoreticalTest();
                        if (TheoreticalTest.IsExpired)
                            return -1;
                        if (TheoreticalTest.TestResult == false)
                            return -1;
                        if (TheoreticalTest.DidTheTestPassWithoutAttending() == true)
                            return -1;
                    }
                    if (DrivingTest.TestID != -1)
                    {
                        DrivingTest = DataAccessTier.AccessDrivingTestData.Find(DrivingTest.TestID).ToDrivingTest();
                        if (DrivingTest.IsExpired)
                            return -1;
                        if (DrivingTest.TestResult == false)
                            return -1;
                        if (DrivingTest.DidTheTestPassWithoutAttending() == true)
                            return -1;
                    }

                    if (ApplicationToAdd.ApplicationType != ApplicationType.LicenseIssuance)
                        return -1;

                    if (ApplicationToAdd.ApplicantPersonID != EyeTest.PersonID ||
                         ApplicationToAdd.ApplicantPersonID != DrivingTest.PersonID ||
                          ApplicationToAdd.ApplicantPersonID != TheoreticalTest.PersonID)
                            return -1;

                    if (Class!= DrivingTest.TestClass ||
                         Class != TheoreticalTest.TestClass)
                            return -1;

                    int DoesApplicationSucceeded = _Application(ApplicationToAdd);

                    if (DoesApplicationSucceeded == -1)
                        return -1;
                                        
                    int? DoesEyeTestSucceeded = null;
                    int? DoesTheoreticalTestSucceeded = null;
                    int? DoesDrivingTestSucceeded = null;

                    if (EyeTest.TestID == -1)
                    {
                        DoesEyeTestSucceeded = MangeEyeTests._Add(new EyeTest(EyeTest.PersonID, EyeTest.AppointmentDate, EyeTest.PaidFees, LogedInUser.UserID, DoesApplicationSucceeded, EyeTest.Notes));

                        if (DoesEyeTestSucceeded == -1)
                        {
                            DataAccessTier.AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                            return -1;
                        }
                    }

                    if (DrivingTest.TestID == -1)
                    {
                        DoesDrivingTestSucceeded = MangeDrivingTests._Add(new DrivingTest(DrivingTest.PersonID, DrivingTest.AppointmentDate, DrivingTest.PaidFees, LogedInUser.UserID, DoesApplicationSucceeded, DrivingTest.Notes, DrivingTest.TestClass));

                        if (DoesDrivingTestSucceeded == -1)
                        {
                            if (DoesEyeTestSucceeded != null)
                                DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                            DataAccessTier.AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                            return -1;
                        }
                    }

                    if (TheoreticalTest.TestID == -1)
                    {
                        DoesTheoreticalTestSucceeded = MangeTheoreticalTests._Add(new TheoreticalTest(TheoreticalTest.PersonID, TheoreticalTest.AppointmentDate, TheoreticalTest.PaidFees, LogedInUser.UserID, DoesApplicationSucceeded, TheoreticalTest.Notes, TheoreticalTest.TestClass));

                        if (DoesTheoreticalTestSucceeded == -1)
                        {
                            if(DoesEyeTestSucceeded != null)
                                DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                            if (DoesDrivingTestSucceeded != null)
                                DataAccessTier.AccessDrivingTestData.DeleteTest((int)DoesDrivingTestSucceeded);
                            DataAccessTier.AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                            return -1;
                        }
                    }

                    int DoesLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                        (new LocalDrivingLicenseApplication(DoesApplicationSucceeded,Class
                          , DoesEyeTestSucceeded != null ?(int)DoesEyeTestSucceeded:EyeTest.TestID,
                          DoesTheoreticalTestSucceeded != null ? (int)DoesTheoreticalTestSucceeded:TheoreticalTest.TestID,
                          DoesDrivingTestSucceeded != null?(int)DoesDrivingTestSucceeded:DrivingTest.TestID));

                    if (DoesLocalApplicationSucceeded == -1)
                    {
                        if (DoesEyeTestSucceeded != null)
                            DataAccessTier.AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                        if (DoesDrivingTestSucceeded != null)
                            DataAccessTier.AccessDrivingTestData.DeleteTest((int)DoesDrivingTestSucceeded);
                        if (DoesTheoreticalTestSucceeded != null)
                            DataAccessTier.AccessTheoreticalTestData.DeleteTest((int)DoesTheoreticalTestSucceeded);

                        DataAccessTier.AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                        return -1;
                    }

                    return DoesApplicationSucceeded;
                }

                public static int RetakeTestApplication(Application ApplicationToAdd, TestType Type, Test TestToSave)
                {
                    if (ApplicationType.RetakeTest != ApplicationToAdd.ApplicationType)
                        return -1;

                    if (ApplicationStatus.Completed != ApplicationToAdd.ApplicationStatus)
                        return -1;

                    if (ApplicationToAdd.ApplicantPersonID != TestToSave.PersonID)
                        return -1;

                    ApplicationToAdd.LastStatusDate = DateTime.Now;
                    
                    int DoesApplicationAddingSucceed;

                    DoesApplicationAddingSucceed = MangeApplications.Add._Application(ApplicationToAdd);

                    if (DoesApplicationAddingSucceed == -1)
                        return -1;

                    int DoesTestAddingSucceed;

                    switch (Type)
                    {
                        case TestType.EyeTest:
                            DoesTestAddingSucceed = MangeEyeTests._Add(new EyeTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, DoesApplicationAddingSucceed, TestToSave.Notes)) ;

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return -1;
                            }
                            break;
                        case TestType.DrivingTest:
                            DoesTestAddingSucceed = MangeDrivingTests._Add(new DrivingTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, DoesApplicationAddingSucceed, TestToSave.Notes,((DrivingTest)TestToSave).TestClass));

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return -1;
                            }
                            break;
                        default: // TestType.TheoreticalTest
                            DoesTestAddingSucceed = MangeTheoreticalTests._Add(new TheoreticalTest(TestToSave.PersonID, TestToSave.AppointmentDate, TestToSave.PaidFees, LogedInUser.UserID, DoesApplicationAddingSucceed, TestToSave.Notes, ((DrivingTest)TestToSave).TestClass));

                            if (DoesTestAddingSucceed == -1)
                            {
                                DataAccessTier.AccessApplicationData.DeleteApplication(DoesTestAddingSucceed);
                                return -1;
                            }
                            break;
                    }



                    return DoesTestAddingSucceed;
                }

                public static int RenewDrivingLicenseApplication(Application ApplicationToAdd,int LicenseIDToRenew,EyeTest EyeTest)
                { 
                    if (ApplicationToAdd == null ||
                         LicenseIDToRenew < 0 ||
                          EyeTest == null ||
                          ApplicationToAdd.ApplicationID != -1)
                        return -1;

                    if (EyeTest.TestID != -1)
                    {
                        EyeTest = DataAccessTier.AccessEyeTestData.Find(EyeTest.TestID).ToEyeTest();
                        if (EyeTest.IsExpired)
                            return -1;
                        if (EyeTest.TestResult == false)
                            return -1;
                        if (EyeTest.DidTheTestPassWithoutAttending() == true)
                            return -1;
                    }
                    
                    if (EyeTest.PersonID != ApplicationToAdd.ApplicantPersonID)
                        return -1;

                    License LicenseToRenew = AccessLicenseData.Find(LicenseIDToRenew).ToLicense();

                    if (AccessDetainedLicenseData.IsLicenseCurrenlyDetained(LicenseToRenew.LicenseID))
                        return -1;

                    if (LicenseToRenew.IsActive == false)
                        return -1;

                    int DoesApplicationSucceeded = _Application(ApplicationToAdd);

                    if (DoesApplicationSucceeded == -1)
                        return -1;

                    int? DoesEyeTestSucceeded = null;

                    if (EyeTest.TestID == -1)
                    {
                        DoesEyeTestSucceeded = MangeEyeTests._Add(EyeTest);

                        if (DoesEyeTestSucceeded == -1)
                        {
                            AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                            return -1;
                        }
                    }

                    int DoesLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                        (new LocalDrivingLicenseApplication(DoesApplicationSucceeded, LicenseToRenew.LicenseClass, EyeTest.TestID != -1 ? EyeTest.TestID : DoesEyeTestSucceeded, null, null));
                    
                    if(DoesLocalApplicationSucceeded == -1)
                    {
                        if (DoesEyeTestSucceeded != null)
                            AccessEyeTestData.DeleteEyeTest((int)DoesEyeTestSucceeded);
                        AccessApplicationData.DeleteApplication(DoesApplicationSucceeded);
                        return -1;
                    }

                    return DoesApplicationSucceeded;

                }

                /* How To Use This Method --> Check the documentation */

                public static int MissingReplacementApplication(Application ApplicationToAdd,int LicenseIDToReplaceBasedOn)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1)
                        return -1;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDToReplaceBasedOn).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return -1;

                    if (!LicenseToReplaceBasedOn.IsActive)
                        return -1;
                    if (LicenseToReplaceBasedOn.IsExpired())
                        return -1;

                    int DoseApplicetionSuccesseeded = _Application(ApplicationToAdd);

                    if (DoseApplicetionSuccesseeded == -1)
                        return -1;

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                       (new LocalDrivingLicenseApplication
                        (DoseApplicetionSuccesseeded, LicenseToReplaceBasedOn.LicenseClass, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        AccessApplicationData.DeleteApplication(DoseApplicetionSuccesseeded);
                        return -1;
                    }

                    return DoseApplicetionSuccesseeded;
                }

                public static int DamagedReplacementApplication(Application ApplicationToAdd, int LicenseIDToReplaceBasedOn)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1 ||
                        LicenseIDToReplaceBasedOn < 0)
                        return -1;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDToReplaceBasedOn).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return -1;

                    if (!LicenseToReplaceBasedOn.IsActive)
                        return -1;
                    if (LicenseToReplaceBasedOn.IsExpired())
                        return -1;

                    int DoseApplicetionSuccesseeded = _Application(ApplicationToAdd);

                    if (DoseApplicetionSuccesseeded == -1)
                        return -1;

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                       (new LocalDrivingLicenseApplication
                        (DoseApplicetionSuccesseeded, LicenseToReplaceBasedOn.LicenseClass, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        AccessApplicationData.DeleteApplication(DoseApplicetionSuccesseeded);
                        return -1;
                    }

                    return DoseApplicetionSuccesseeded;
                }

                public static int ReleaseLicenseApplication(Application ApplicationToAdd, int LicenseIDAskForRelease)
                {
                    if (ApplicationToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1 ||
                        LicenseIDAskForRelease < 0)
                        return -1;

                    License LicenseToReplaceBasedOn = AccessLicenseData.Find(LicenseIDAskForRelease).ToLicense();

                    if (AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID) != LicenseToReplaceBasedOn.DriverID)
                        return -1;
                                       
                    int DoseApplicetionSuccesseeded = _Application(ApplicationToAdd);

                    if (DoseApplicetionSuccesseeded == -1)
                        return -1;

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add
                       (new LocalDrivingLicenseApplication
                        (DoseApplicetionSuccesseeded, LicenseToReplaceBasedOn.LicenseClass, null, null, null));

                    if (DoseLocalApplicationSucceeded == -1)
                    {
                        AccessApplicationData.DeleteApplication(DoseApplicetionSuccesseeded);
                        return -1;
                    }

                    return DoseApplicetionSuccesseeded;


                }

                public static int IssuingInternationalLicenseApplication(Application ApplicationToAdd, EyeTest EyeTestToAdd)
                {
                    if (ApplicationToAdd == null ||
                        EyeTestToAdd == null ||
                        ApplicationToAdd.ApplicationID == -1)
                        return -1;

                    if (EyeTestToAdd.TestID != -1)
                    {
                        EyeTestToAdd = AccessEyeTestData.Find(EyeTestToAdd.TestID).ToEyeTest();

                        if (EyeTestToAdd.IsExpired)
                            return -1;
                        if (EyeTestToAdd.TestResult == false)
                            return -1;
                        if (EyeTestToAdd.DidTheTestPassWithoutAttending() == true)
                            return -1;
                    }

                    if (EyeTestToAdd.PersonID != ApplicationToAdd.ApplicantPersonID)
                        return -1;


                    int? DriverID = AccessDriverData.GetDriverID(ApplicationToAdd.ApplicantPersonID);

                    if (DriverID == null)
                        return -1;
                    if (DriverID == -1)
                        return -1;

                    if (AccessLicenseData.DoseHaveActiveLicense((int)DriverID, LicenseClass.RegularCar))
                        return -1;

                    int DoseApplicationSucceeded = _Application(ApplicationToAdd);

                    if (DoseApplicationSucceeded == -1) 
                        return -1;

                    int? DoseEyeTestSucceeded = null;

                    if (EyeTestToAdd.TestID == -1)
                    {
                        DoseEyeTestSucceeded = MangeEyeTests._Add(EyeTestToAdd);
                        if (DoseEyeTestSucceeded == -1)
                        {
                            AccessApplicationData.DeleteApplication(DoseApplicationSucceeded);
                            return -1;
                        }
                    }

                    int DoseLocalApplicationSucceeded = MangeLocalDrivingLicenseApplications._Add(new LocalDrivingLicenseApplication
                        (DoseApplicationSucceeded, LicenseClass.RegularCar, (DoseEyeTestSucceeded != null) ? DoseEyeTestSucceeded : EyeTestToAdd.TestID, null, null));

                    if(DoseLocalApplicationSucceeded == -1)
                    {
                        if (DoseEyeTestSucceeded != null)
                            AccessEyeTestData.DeleteEyeTest(EyeTestToAdd.TestID);
                        AccessApplicationData.DeleteApplication(DoseApplicationSucceeded);
                        return -1;
                    }

                    return DoseApplicationSucceeded;
                }

            }

            private static bool _IsTheApplicationValidToAttachLicense (int ApplicationID)
            {
                if (!AccessApplicationData.IsExist(ApplicationID))
                    return false;

                Application FindedApplication = MangeApplications.Find(ApplicationID);

                if (!AccessLocalDrivingLicenseApplicationData.IsExistByApplicationID(ApplicationID))
                    return false;

                if (FindedApplication.ApplicationType != ApplicationType.LicenseIssuance)
                    return false;

                if (DateTime.Now.Subtract(FindedApplication.ApplicationDate) > new TimeSpan(SettingsClass.ApplicationExpirationPeriod, 0, 0, 0))
                    return false;

                if (FindedApplication.ApplicationStatus != ApplicationStatus.Completed)
                    return false;

                LocalDrivingLicenseApplication FindedLocalApplication = MangeLocalDrivingLicenseApplications.FindByApplicationID(ApplicationID);

                if (FindedLocalApplication.EyeTestID == null || FindedLocalApplication.DrivingTestID == null || FindedLocalApplication.TheoritecalTestID == null)
                    return false;

                if (!MangeEyeTests.IsExist((int)FindedLocalApplication.EyeTestID) || !MangeDrivingTests.IsExist((int)FindedLocalApplication.DrivingTestID) || !MangeTheoreticalTests.IsExist((int)FindedLocalApplication.TheoritecalTestID))
                    return false;

                EyeTest EyeTest = MangeEyeTests.Find((int)FindedLocalApplication.EyeTestID);

                if (EyeTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(EyeTest.AppointmentDate) > new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0))
                    return false;

                if (EyeTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;

                DrivingTest DrivingTest = MangeDrivingTests.Find((int)FindedLocalApplication.DrivingTestID);
                
                if (DrivingTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(DrivingTest.AppointmentDate) > new TimeSpan(SettingsClass.DrivingTestExpirationPeriod, 0, 0, 0))
                    return false;

                if (DrivingTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;

                if (DrivingTest.TestClass != FindedLocalApplication.LicenseClass)
                    return false;

                TheoreticalTest TheoreticalTest = MangeTheoreticalTests.Find((int)FindedLocalApplication.TheoritecalTestID);

                if (TheoreticalTest.TestResult != true)
                    return false;

                if (DateTime.Now.Subtract(TheoreticalTest.AppointmentDate) > new TimeSpan(SettingsClass.TheoreticalTestExpirationPeriod, 0, 0, 0))
                    return false;

                if (TheoreticalTest.PersonID != FindedApplication.ApplicantPersonID)
                    return false;

                if (TheoreticalTest.TestClass != FindedLocalApplication.LicenseClass)
                    return false;

                return true;
            }

            public static bool IsTheApplicationValidToAttachLicense(int ApplicationID)
            {
                return _IsTheApplicationValidToAttachLicense(ApplicationID);
            }

            // needs to be delete and handeled
            private static decimal _CalculateTotalApplicationFees(Application ApplicationToCalculatItsFees)
            {
                decimal TotalNeededFees = AccessApplicationData.GetApplicationFees(ApplicationToCalculatItsFees.ApplicationType);

                switch (ApplicationToCalculatItsFees.ApplicationType)
                {
                    case ApplicationType.DamagedReplacement:
                        break;
                    case ApplicationType.IssuingInternationalLicense:
                        TotalNeededFees += SettingsClass.GetInternationalLicenseIssuanceFees
                            ((LicenseClass)AccessInternationalLicenseData.GetInternationalLicenseClassByApplicationID(ApplicationToCalculatItsFees.ApplicationID));
                        break;
                    case ApplicationType.LicenseIssuance:
                        break;
                    case ApplicationType.MissingReplacement:
                        break;
                    case ApplicationType.ReleaseLicense:
                        break;
                    case ApplicationType.RenewDrivingLicense:
                        break;
                    case ApplicationType.RetakeTest:
                        break;
                }

                return TotalNeededFees;
            }

            public static bool Update(Application ApplicationToUpdate)
            {
                // fees LaststatusDate status // 
                if (ApplicationToUpdate.ApplicationID == -1)
                    return false;
                if (!AccessApplicationData.IsExist(ApplicationToUpdate.ApplicationID))
                    return false;

                var OregenalApplication = AccessApplicationData.Find(ApplicationToUpdate.ApplicationID);

                if (OregenalApplication.ApplicationStatus == ApplicationToUpdate.ApplicationStatus)
                { ApplicationToUpdate.LastStatusDate = OregenalApplication.LastStatusDate; }
                else if (OregenalApplication.ApplicationStatus == ApplicationStatus.Canceled)
                { ApplicationToUpdate.ApplicationStatus = ApplicationStatus.Canceled; }
                else if (OregenalApplication.ApplicationStatus == ApplicationStatus.Completed)
                { ApplicationToUpdate.ApplicationStatus = ApplicationStatus.Completed; }

                if (ApplicationToUpdate.PaidFees < _CalculateTotalApplicationFees(ApplicationToUpdate))
                    return false;

                // ApplicationDate and ApplicationType Should not be Edited And it can not be //                



                return AccessApplicationData.UpdateApplication(ApplicationToUpdate.ApplicationID, ApplicationToUpdate.ApplicantPersonID,
                    ApplicationToUpdate.ApplicationDate, ApplicationToUpdate.ApplicationType, ApplicationToUpdate.ApplicationStatus,
                    ApplicationToUpdate.LastStatusDate, ApplicationToUpdate.PaidFees, ApplicationToUpdate.CreatedByUserID);
            }

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
                            new TimeSpan(SettingsClass.ApplicationExpirationPeriod, 0, 0, 0));

            }


            public static bool IsExist(int ApplicationIDToFind)
            {
                return AccessApplicationData.IsExist(ApplicationIDToFind);
            }
            
            /*public static bool DeleteApplication(int ApplicationIDToFind)
            {
                return AccessApplicationData.DeleteApplication(ApplicationIDToFind);
            }
            */

            public static bool EditPaidFees(int ApplicationIDToEdit, decimal NewFees)
            {
                Application ApplicationToEdit = AccessApplicationData.Find(ApplicationIDToEdit).ToApplication();

                if (ApplicationToEdit == null)
                    return false;

                if (ApplicationToEdit.ApplicationStatus != ApplicationStatus.New)
                    return false;

                decimal NeededFee = _CalculateTotalApplicationFees(ApplicationToEdit);

                if (NewFees < NeededFee)
                    return false;

                return AccessApplicationData.EditPaidFees(ApplicationIDToEdit, NewFees);
            }

            public static decimal GetApplicationFees(ApplicationType Type)
            {
                return AccessApplicationData.GetApplicationFees(Type);
            }

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
            public static bool Add(Country CountryToAdd)
            {

                if (CountryToAdd.CountryID != -1)
                { return false; }

                if (!string.IsNullOrEmpty(CountryToAdd.CountryName))
                { return false; }

                else
                {
                    DataAccessTier.DTCountry U = AccessCountriesData.AddNewCountry(CountryToAdd.CountryName);

                    return U != null;
                }
            }

            public static bool Update(Country CountryToUpdate)
            {
                if (CountryToUpdate.CountryID == -1)
                    return false;

                if (!AccessCountriesData.IsExist(CountryToUpdate.CountryID))
                { return false; }

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

        public static class MangeDetainedLicenses
        {
            public static bool Add(DetainedLicense DetainedLicenseToAdd)
            {

                if (DetainedLicenseToAdd.DetainID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTDetainedLicense U = AccessDetainedLicenseData.AddNewDetainedLicense
                        (DetainedLicenseToAdd.LicenseID, DetainedLicenseToAdd.DetainDate, DetainedLicenseToAdd.FineFees, DetainedLicenseToAdd.CreatedByUserID
                        , DetainedLicenseToAdd.ReleaseDate, DetainedLicenseToAdd.ReleasedByUserID, DetainedLicenseToAdd.ReleaseApplicationID);

                    return U != null;
                }
            }

            public static bool Update(DetainedLicense DetainedLicenseToUpdate)
            {
                if (DetainedLicenseToUpdate.DetainID == -1)
                    return false;

                if (!AccessDetainedLicenseData.IsExist(DetainedLicenseToUpdate.DetainID))
                { return false; }

                return AccessDetainedLicenseData.UpdateDetainedLicense(DetainedLicenseToUpdate.DetainID, DetainedLicenseToUpdate.LicenseID,
                    DetainedLicenseToUpdate.DetainDate, DetainedLicenseToUpdate.FineFees, DetainedLicenseToUpdate.CreatedByUserID,
                    DetainedLicenseToUpdate.ReleaseDate, DetainedLicenseToUpdate.ReleasedByUserID, DetainedLicenseToUpdate.ReleaseApplicationID);
            }

            public static DetainedLicense Find(int DetainID)
            {
                var DetainedLicense = AccessDetainedLicenseData.Find(DetainID);

                if (DetainedLicense == null)
                    return null;
                else
                    return DetainedLicense.ToDetainedLicense();
            }

            public static DataTable ListAllDetainedLicenses()
            {
                return AccessDetainedLicenseData.ListAllDetainingRecords();
            }

            public static bool IsExist(int DetainIDToFind)
            {
                return AccessDetainedLicenseData.IsExist(DetainIDToFind);
            }

            public static bool DeleteDetainedLicense(int DetainIDToFind)
            {
                return AccessDetainedLicenseData.DeleteDetainedLicense(DetainIDToFind);
            }

        }

        public static class MangeDrivers
        {
            public static bool Add(Driver DriverToAdd)
            {
                DriverToAdd._CreatedDate = DateTime.Now;
                DriverToAdd._CreatedByUserID = LogedInUser.UserID;

                if (DriverToAdd.DriverID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTDriver U = AccessDriverData.AddNewDriver
                        (DriverToAdd.PersonID, DriverToAdd.CreatedByUserID, DriverToAdd.CreatedDate);

                    return U != null;
                }
            }

            public static bool Update(Driver DriverToUpdate)
            {
                if (DriverToUpdate.DriverID == -1)
                    return false;

                if (!AccessDriverData.IsExist(DriverToUpdate.DriverID))
                { return false; }

                return AccessDriverData.UpdateDriver(DriverToUpdate.DriverID, DriverToUpdate.PersonID, DriverToUpdate.CreatedByUserID,
                    DriverToUpdate.CreatedDate);
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

            public static bool DeleteDriver(int DriverIDToFind)
            {
                return AccessDriverData.DeleteDriver(DriverIDToFind);
            }

        }

        public static class MangeInternationalLicenses
        {
            public static bool Add(InternationalLicense InternationalLicenseToAdd)
            {

                if (InternationalLicenseToAdd.InternationalLicenseID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTInternationalLicense U = AccessInternationalLicenseData.AddNewInternationalLicense
                        (InternationalLicenseToAdd.ApplicationID, InternationalLicenseToAdd.DriverID,
                            InternationalLicenseToAdd.IssuedUsingLocalLicenseID, InternationalLicenseToAdd.IssueDate,
                            InternationalLicenseToAdd.ExpirationDate, InternationalLicenseToAdd.IsActive,
                            InternationalLicenseToAdd.CreatedByUserID);

                    return U != null;
                }
            }

            public static bool Update(InternationalLicense InternationalLicenseToUpdate)
            {
                if (InternationalLicenseToUpdate.InternationalLicenseID == -1)
                    return false;

                if (!AccessInternationalLicenseData.IsExist(InternationalLicenseToUpdate.InternationalLicenseID))
                { return false; }

                return AccessInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseToUpdate.InternationalLicenseID,
                    InternationalLicenseToUpdate.ApplicationID, InternationalLicenseToUpdate.DriverID, InternationalLicenseToUpdate.IssuedUsingLocalLicenseID,
                    InternationalLicenseToUpdate.IssueDate, InternationalLicenseToUpdate.ExpirationDate, InternationalLicenseToUpdate.IsActive, InternationalLicenseToUpdate.CreatedByUserID);
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

            public static bool DeleteInternationalLicense(int InternationalLicenseIDToFind)
            {
                return AccessInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseIDToFind);
            }

            public static DateTime GetInternationalLicenseExpiretionDate(int LocalLicenseID)
            {
                return AccessInternationalLicenseData.GetInternationalLicenseExpiretionDate(LocalLicenseID);
            }

            public static LicenseClass GetInternationalLicenseClass(int InternationalLicenseID)
            {
                return AccessInternationalLicenseData.GetInternationalLicenseClass(InternationalLicenseID).Value;
            }

            public static bool SetActivationSatuts(int InternationalLicenseIDToEdit, bool Status)
            {
                return AccessInternationalLicenseData.SetActivationSatuts(InternationalLicenseIDToEdit, Status);
            }
        }

        public static class MangeLicenses
        {
            public static bool Add(License LicenseToAdd)
            {

                if (LicenseToAdd.LicenseID != -1)
                    return false;

                if (AccessDriverData.DoesDriverHaveLicenseOfClass(LicenseToAdd.DriverID, LicenseToAdd.LicenseClass))
                    return false;


                DateTime? DriverBirthDate = AccessDriverData.GetDriverBirthDate(LicenseToAdd.DriverID);

                if (DriverBirthDate == null)
                    return false;

                if (!SettingsClass.IsDriverOledEnough((DateTime)DriverBirthDate, LicenseToAdd.LicenseClass))
                    return false;


                LicenseToAdd._CreatedByUserID = LogedInUser.UserID;


                DataAccessTier.DTLicense U = AccessLicenseData.AddNewLicense
                    (LicenseToAdd.LocalDrivingLicenseApplicationID, LicenseToAdd.DriverID,
                    LicenseToAdd.LicenseClass, LicenseToAdd.IssueDate,
                    LicenseToAdd.ExpirationDate, LicenseToAdd.Notes,
                    LicenseToAdd.IsActive, LicenseToAdd.IssueReason,
                    LicenseToAdd.CreatedByUserID);

                return U != null;

            }

            public static bool Update(License LicenseToUpdate)
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

                if(OldLicense.Notes == LicenseToUpdate.Notes )
                { return true; }
                

                return AccessLicenseData.UpdateLicense(LicenseToUpdate.LicenseID, LicenseToUpdate.LocalDrivingLicenseApplicationID, LicenseToUpdate.DriverID,
                    LicenseToUpdate.LicenseClass, LicenseToUpdate.IssueDate, LicenseToUpdate.ExpirationDate, LicenseToUpdate.Notes,
                    LicenseToUpdate.IsActive, LicenseToUpdate.IssueReason, LicenseToUpdate.CreatedByUserID);
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

            public static bool DeleteLicense(int LicenseIDToFind)
            {
                return AccessLicenseData.DeleteLicense(LicenseIDToFind);
            }

            public static bool IsCurrentlyDetained(int LicenseIDToCheck)
            {
                return DataAccessTier.AccessDetainedLicenseData.IsLicenseCurrenlyDetained(LicenseIDToCheck);
            }

        }

        public static class MangeLocalDrivingLicenseApplications
        {
            internal static int _Add(LocalDrivingLicenseApplication LocalDrivingLicenseApplicationToAdd)
            {
                Application MainApplication = AccessApplicationData.Find(LocalDrivingLicenseApplicationToAdd.ApplicationID).ToApplication();

                if (MainApplication == null)
                    return -1;

                if (MainApplication.ApplicationType != ApplicationType.LicenseIssuance)
                    return -1;
                
                if (MainApplication.IsExpired) 
                    return -1;

                if (MainApplication.ApplicationStatus != ApplicationStatus.New)
                    return -1;

                if (LocalDrivingLicenseApplicationToAdd.EyeTestID != null)
                {
                    EyeTest EyeTest =
                        AccessEyeTestData.Find((int)LocalDrivingLicenseApplicationToAdd.EyeTestID).ToEyeTest();

                    if (EyeTest == null)
                        return -1;
                    
                    if (EyeTest.IsExpired)
                        return -1;
                }
                if (LocalDrivingLicenseApplicationToAdd.DrivingTestID != null)
                {
                    DrivingTest DrivingTest =
                        AccessDrivingTestData.Find((int)LocalDrivingLicenseApplicationToAdd.DrivingTestID).ToDrivingTest();

                    if (DrivingTest == null)
                        return -1;

                    if (DrivingTest.IsExpired)
                        return -1;
                }
                if (LocalDrivingLicenseApplicationToAdd.TheoritecalTestID != null)
                {
                    TheoreticalTest TheoritecalTest =
                        AccessTheoreticalTestData.Find((int)LocalDrivingLicenseApplicationToAdd.TheoritecalTestID).ToTheoreticalTest();

                    if (TheoritecalTest == null)
                        return -1;

                    if (TheoritecalTest.IsExpired)
                        return -1;
                }
                
                DataAccessTier.DTLocalDrivingLicenseApplication U = AccessLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                    (LocalDrivingLicenseApplicationToAdd.ApplicationID, LocalDrivingLicenseApplicationToAdd.LicenseClass,
                        LocalDrivingLicenseApplicationToAdd.EyeTestID, LocalDrivingLicenseApplicationToAdd.TheoritecalTestID,
                        LocalDrivingLicenseApplicationToAdd.DrivingTestID);

                return U == null ? -1 : U.LocalDrivingLicenseApplicationID;

            }

            public static bool Update(LocalDrivingLicenseApplication LocalDrivingLicenseApplicationToUpdate)
            {
                if (LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID == -1)
                    return false;

                if (!AccessLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID))
                { return false; }

                return AccessLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                    (LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplicationToUpdate.ApplicationID,
                    LocalDrivingLicenseApplicationToUpdate.LicenseClass, LocalDrivingLicenseApplicationToUpdate.EyeTestID,
                    LocalDrivingLicenseApplicationToUpdate.TheoritecalTestID, LocalDrivingLicenseApplicationToUpdate.DrivingTestID);
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

            public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationIDToFind)
            {
                return AccessLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationIDToFind);
            }

            public static DateTime GetExpirationDateOfLicense(int LicenseID)
            {
                return AccessLicenseData.GetExpirationDateOfLicense(LicenseID);
            }

        }

        public static class MangeEyeTests
        {            
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
                if (EyeTestToAdd.PaidFees > General.SettingsClass.EyeTestFees) 
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

                if (EyeTestToUpdate.PaidFees < OldTest.PaidFees)
                    return false;

                if (EyeTestToUpdate.TestResult != null
                    && EyeTestToUpdate.PaidFees < General.SettingsClass.EyeTestFees)
                    return false;

                if (EyeTestToUpdate.TestResult != null && EyeTestToUpdate.ResultAddedByUserID == null)
                    return false;

                if (EyeTestToUpdate.TestResult != null && OldTest.TestResult == null && EyeTestToUpdate.ResultAddedByUserID != LogedInUser.UserID)
                    return false;


                return AccessEyeTestData.UpdateEyeTest(EyeTestToUpdate.TestID, EyeTestToUpdate.PersonID,
                    EyeTestToUpdate.AppointmentDate, EyeTestToUpdate.PaidFees, EyeTestToUpdate.AppointmentMadeByUserID
                    , EyeTestToUpdate.TestApplicationID, EyeTestToUpdate.TestResult, EyeTestToUpdate.Notes, EyeTestToUpdate.ResultAddedByUserID);
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
                            new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0));
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


            public static bool DeleteEyeTest(int TestIDToFind)
            {
                return AccessEyeTestData.DeleteEyeTest(TestIDToFind);
            }
        }

        public static class MangeDrivingTests
        {
            public static bool _CouldAttachDrivingTestToApplicationID(int ApplicationID ,LicenseClass TestClass)
            {
                if (!AccessApplicationData.IsExist(ApplicationID))
                { return false; }

                ApplicationType Type = (ApplicationType)AccessApplicationData.GetApplicationType(ApplicationID);

                if (Type == ApplicationType.LicenseIssuance)
                {
                    if (AccessDrivingTestData.IsExistByApplicationID(ApplicationID))
                        return false;
                    if (AccessLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID).LicenseClassID != TestClass)
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
                if (TestToAdd.PaidFees > General.SettingsClass.DrivingTestFees)
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
                if (TestToUpdate.TestID == -1)
                    return false;

                DTDrivingTest OldTest = AccessDrivingTestData.Find(TestToUpdate.TestID);

                if (OldTest == null)
                    return false;


                if (OldTest.PersonID != TestToUpdate.PersonID ||
                    OldTest.TestApplicationID != TestToUpdate.TestApplicationID ||
                    OldTest.AppointmentMadeByUserID != TestToUpdate.AppointmentMadeByUserID)
                    return false;

                if (!(TestToUpdate.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0)
                        && OldTest.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0))
                        &&  OldTest.AppointmentDate != TestToUpdate.AppointmentDate)
                    return false;

                if(OldTest.TestClass != TestToUpdate.TestClass && OldTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0))
                    return false;
                
                if (OldTest.TestResult != null
                    && TestToUpdate.TestResult != OldTest.TestResult)
                    return false;

                if (TestToUpdate.PaidFees < OldTest.PaidFees)
                    return false;

                if (TestToUpdate.TestResult != null
                    && TestToUpdate.PaidFees < General.SettingsClass.DrivingTestFees)
                    return false;

                if (TestToUpdate.TestResult != null && TestToUpdate.ResultAddedByUserID == null)
                    return false;

                if (TestToUpdate.TestResult != null && OldTest.TestResult == null && TestToUpdate.ResultAddedByUserID != LogedInUser.UserID)
                    return false;

                return AccessDrivingTestData.UpdateTest(TestToUpdate.TestID, TestToUpdate.PersonID,
                    TestToUpdate.AppointmentDate, TestToUpdate.PaidFees, TestToUpdate.AppointmentMadeByUserID, TestToUpdate.TestApplicationID,
                    TestToUpdate.TestResult, TestToUpdate.Notes, TestToUpdate.ResultAddedByUserID, TestToUpdate.TestClass);
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
                            new TimeSpan(SettingsClass.DrivingTestExpirationPeriod, 0, 0, 0));
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
                    if (AccessLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID).LicenseClassID != TestClass)
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
                if (TestToAdd.PaidFees > General.SettingsClass.TheoreticalTestFees)
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
                if (TestToUpdate.TestID == -1)
                    return false;

                DTTheoreticalTest OldTest = AccessTheoreticalTestData.Find(TestToUpdate.TestID);

                if (OldTest == null)
                    return false;

                if (OldTest.PersonID != TestToUpdate.PersonID ||
                    OldTest.TestApplicationID != TestToUpdate.TestApplicationID ||
                    OldTest.AppointmentMadeByUserID != TestToUpdate.AppointmentMadeByUserID)
                    return false;

                if (!(TestToUpdate.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0)
                        && OldTest.AppointmentDate.Subtract(DateTime.Now) > new TimeSpan(0, 0, 0))
                         && OldTest.AppointmentDate != TestToUpdate.AppointmentDate)
                    return false;

                if (OldTest.TestClass != TestToUpdate.TestClass && OldTest.AppointmentDate.Subtract(DateTime.Now) < new TimeSpan(0, 0, 0))
                    return false;

                if (OldTest.TestResult != null
                    && TestToUpdate.TestResult != OldTest.TestResult)
                    return false;

                if (TestToUpdate.PaidFees < OldTest.PaidFees)
                    return false;

                if (TestToUpdate.TestResult != null
                    && TestToUpdate.PaidFees < General.SettingsClass.TheoreticalTestFees)
                    return false;

                if (TestToUpdate.TestResult != null && TestToUpdate.ResultAddedByUserID == null)
                    return false;

                if (TestToUpdate.TestResult != null && OldTest.TestResult == null && TestToUpdate.ResultAddedByUserID != LogedInUser.UserID)
                    return false;

                return AccessTheoreticalTestData.UpdateTest(TestToUpdate.TestID, TestToUpdate.PersonID,
                    TestToUpdate.AppointmentDate, TestToUpdate.PaidFees, TestToUpdate.AppointmentMadeByUserID, TestToUpdate.TestApplicationID,
                    TestToUpdate.TestResult, TestToUpdate.Notes, TestToUpdate.ResultAddedByUserID, TestToUpdate.TestClass);
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
                DateTime? TestDate = GetAppointmentDate(TestID);

                if (TestDate == null)
                    return null;

                return (DateTime.Now.Subtract((DateTime)TestDate) >
                            new TimeSpan(SettingsClass.TheoreticalTestExpirationPeriod, 0, 0, 0))
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

        }

        // the complaxe Function will be out here. //


        public static bool EditLocalLicenseApplication(Application ApplicationToEdit, LocalDrivingLicenseApplication LocalLicenseApplication)
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

            if(!AccessLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalLicenseApplication.LocalDrivingLicenseApplicationID, LocalLicenseApplication.ApplicationID, LocalLicenseApplication.LicenseClass, LocalLicenseApplication.EyeTestID, LocalLicenseApplication.TheoritecalTestID, LocalLicenseApplication.DrivingTestID))
            {
                AccessApplicationData.UpdateApplication(OldAppliction.ApplicationID, OldAppliction.ApplicantPersonID, OldAppliction.ApplicationDate,
                OldAppliction.ApplicationType, OldAppliction.ApplicationStatus,OldAppliction.LastStatusDate,
                OldAppliction.PaidFees, OldAppliction.CreatedByUserID);

                return false;
            }

                return true;
        }

    }

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

    public class DetainedLicense
    {
        internal DetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
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

        public DetainedLicense(int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID)
        {
            this._DetainID = -1;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
        }

        public void SetReleaseInfo(DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
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

        public bool IsExpired { get { return (DateTime.Now.Subtract(ApplicationDate) > new TimeSpan(SettingsClass.ApplicationExpirationPeriod, 0, 0, 0)); } }
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

        public bool IsExpired()
        {
            return DateTime.Now.Subtract(ExpirationDate) < new TimeSpan(0, 0, 0, 0);
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
            LicenseClass LicenseClass, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this._LicenseClass = LicenseClass;
            this.EyeTestID = EyeTestID;
            this.DrivingTestID = DrivingTestID;
            this.TheoritecalTestID = TheoritecalTestID;
        }

        public LocalDrivingLicenseApplication(int ApplicationID,
            LicenseClass LicenseClass, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID)
        {
            this._LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = ApplicationID;
            this._LicenseClass = LicenseClass;
            this.EyeTestID = EyeTestID;
            this.DrivingTestID = DrivingTestID;
            this.TheoritecalTestID = TheoritecalTestID;

        }

        private int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }

        public int ApplicationID;
        private LicenseClass _LicenseClass;
        public LicenseClass LicenseClass { get { return _LicenseClass; } }
        public int? EyeTestID;
        public int? DrivingTestID;
        public int? TheoritecalTestID;
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
            else if (DateTime.Now.Subtract(AppointmentDate) < new TimeSpan(SettingsClass.MaximumPeriodToSetTestResulte, 0, 0, 0))
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

        public bool IsExpired { get { return DateTime.Now.Subtract((DateTime)AppointmentDate) >
                            new TimeSpan(SettingsClass.EyeTestExpirationPeriod, 0, 0, 0); } }
    }

    public class DrivingTest : Test
    {
        internal DrivingTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID,LicenseClass TestClass)
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
                            new TimeSpan(SettingsClass.DrivingTestExpirationPeriod, 0, 0, 0); } }

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
                            new TimeSpan(SettingsClass.TheoreticalTestExpirationPeriod, 0, 0, 0); } }

    }
     

    //////////////////////  Extentions  /////////////////////////

    public static class Casting
    {
        // // // // // // // // // // // // // // From DTeir Structures == To == > Final Structures  // // // // // // // // // // // // // // // // //

        public static Country ToCountry(this DTCountry Country)
        { return new Country(Country.CountryID, Country.CountryName); }
        public static DetainedLicense ToDetainedLicense(this DTDetainedLicense DetainedLicense)
        { return new DetainedLicense(DetainedLicense.DetainID, DetainedLicense.LicenseID, DetainedLicense.DetainDate, DetainedLicense.FineFees, DetainedLicense.CreatedByUserID, DetainedLicense.ReleaseDate, DetainedLicense.ReleasedByUserID, DetainedLicense.ReleaseApplicationID); }
        public static Application ToApplication(this DTApplication Application)
        { return new Application(Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID); }
        public static User ToUser(this DTUser User)
        { return new User(User.UserID,User.PersonID, User.UserName, User.Password, User.IsActive); }
        public static Person ToPerson(this DTPerson Person)
        { return new Person(Person.PersonID, Person.NationalNo, Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName, Person.DateOfBirth, Person.Gendor, Person.Address, Person.Phone, Person.Email, Person.NationalityCountryID, Person.ImagePath); }
        public static Driver ToDriver(this DTDriver Driver)
        { return new Driver(Driver.DriverID, Driver.PersonID, Driver.CreatedByUserID, Driver.CreatedDate); }
        public static EyeTest ToEyeTest(this DTEyeTest EyeTest)
        { return new EyeTest(EyeTest.TestID, EyeTest.PersonID, EyeTest.AppointmentDate, EyeTest.PaidFees, EyeTest.AppointmentMadeByUserID, EyeTest.TestApplicationID, EyeTest.TestResult,EyeTest.Notes, EyeTest.ResultAddedByUserID); }
        public static InternationalLicense ToInternationalLicense(this DTInternationalLicense InternationalLicense)
        { return new InternationalLicense(InternationalLicense.InternationalLicenseID, InternationalLicense.ApplicationID, InternationalLicense.DriverID, InternationalLicense.IssuedUsingLocalLicenseID, InternationalLicense.IssueDate, InternationalLicense.ExpirationDate, InternationalLicense.IsActive, InternationalLicense.CreatedByUserID); }
        public static License ToLicense(this DTLicense License)
        { return new License(License.LicenseID, License.LocalDrivingLicenseApplicationID, License.DriverID, License.LicenseClass, License.IssueDate, License.ExpirationDate, License.Notes, License.IsActive, License.IssueReason, License.CreatedByUserID); }
        public static LocalDrivingLicenseApplication ToLocalDrivingLicenseApplication(this DTLocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        { return new LocalDrivingLicenseApplication(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplication.ApplicationID, LocalDrivingLicenseApplication.LicenseClassID, LocalDrivingLicenseApplication.EyeTestID, LocalDrivingLicenseApplication.TheoritecalTestID, LocalDrivingLicenseApplication.DrivingTestID); }
        public static TheoreticalTest ToTheoreticalTest(this DTTheoreticalTest Test)
        { return new TheoreticalTest(Test.TestID, Test.PersonID, Test.AppointmentDate, Test.PaidFees, Test.AppointmentMadeByUserID, Test.TestApplicationID, Test.TestResult, Test.Notes, Test.ResultAddedByUserID,Test.TestClass); }
        public static DrivingTest ToDrivingTest(this DTDrivingTest Test)
        { return new DrivingTest(Test.TestID, Test.PersonID, Test.AppointmentDate, Test.PaidFees, Test.AppointmentMadeByUserID, Test.TestApplicationID, Test.TestResult, Test.Notes, Test.ResultAddedByUserID,Test.TestClass); }
    
    
    }

}
