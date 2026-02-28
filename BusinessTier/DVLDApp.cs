using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessTier;
using Generale;

namespace BusinessTier
{
    public static class DVLDApp
    {
        public static bool LogInWith(string UserNameOrID, string PassWord)
        {
            if (AccessUserData.IsExistAndActiveByUserNameAndPassword(UserNameOrID, PassWord))
            {
                _LogedInUser = AccessUserData.Find(UserNameOrID).ToUser();
                return true;
            }
            else if (AccessUserData.IsExistAndActiveByUserIDAndPassword(UserNameOrID, PassWord))
            {
                if (int.TryParse(UserNameOrID, out int ID))
                {
                    _LogedInUser = AccessUserData.Find(ID).ToUser();
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                return false;
            }

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
                    PersonToUpdate.SecondName, PersonToUpdate.ThirdName, PersonToUpdate.LastName, PersonToUpdate.DateOfBirth,PersonToUpdate.Gendor,
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
            private static decimal _CalculateTotalApplicationFees(Application ApplicationToCalculatItsFees)
            {
                decimal TotalNeededFees = AccessApplicationData.GetApplicationFees(ApplicationToCalculatItsFees.ApplicationTypeID);

                switch (ApplicationToCalculatItsFees.ApplicationTypeID)
                {
                    case ApplicationType.DamagedReplacement:
                        break;
                    case ApplicationType.IssuingInternationalLicense:
                        TotalNeededFees += SettingsClass.GetInternationalLicensIssuanceFees
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

            public static bool Add(Application ApplicationToAdd)
            {
                if (ApplicationToAdd.ApplicationID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTApplication U = AccessApplicationData.AddNewApplication
                        (ApplicationToAdd.ApplicantPersonID, ApplicationToAdd.ApplicationDate, ApplicationToAdd.ApplicationTypeID,
                            ApplicationToAdd.ApplicationStatus, ApplicationToAdd.LastStatusDate, ApplicationToAdd.PaidFees,
                            ApplicationToAdd.CreatedByUserID);

                    return U != null;
                }
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
                    ApplicationToUpdate.ApplicationDate, ApplicationToUpdate.ApplicationTypeID, ApplicationToUpdate.ApplicationStatus,
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

            public static DataTable ListAllApplications()
            {
                return AccessApplicationData.ListAllApplications();
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
                
                if(!AccessApplicationData.IsExist(ApplicationID))
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
                return AccessDetainedLicenseData.ListAllDetainedLicenses();
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

            public static Driver Find(int DriverID)
            {
                var Driver = AccessDriverData.Find(DriverID);

                if (Driver == null)
                    return null;
                else
                    return Driver.ToDriver();
            }
            
            public static Driver FindByPersonID(int PersonID)
            {
                var Driver = AccessDriverData.FindByPersonID(PersonID);

                if (Driver == null)
                    return null;
                else
                    return Driver.ToDriver();
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

        public static class MangeEyeTests
        {
            public static bool Add(EyeTest EyeTestToAdd)
            {

                if (EyeTestToAdd.EyeTestID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTEyeTest U = AccessEyeTestData.AddNewEyeTest
                        (EyeTestToAdd.PersonID, EyeTestToAdd.AppointmentDate, EyeTestToAdd.PaidFees, EyeTestToAdd.CreatedByUserID
                        , EyeTestToAdd.TestApplicationID, EyeTestToAdd.TestResult, EyeTestToAdd.Notes, EyeTestToAdd.ResultAddedByUserID);

                    return U != null;
                }
            }

            public static bool Update(EyeTest EyeTestToUpdate)
            {
                if (EyeTestToUpdate.EyeTestID == -1)
                    return false;

                if (!AccessEyeTestData.IsExist(EyeTestToUpdate.EyeTestID))
                { return false; }

                return AccessEyeTestData.UpdateEyeTest(EyeTestToUpdate.EyeTestID, EyeTestToUpdate.PersonID,
                    EyeTestToUpdate.AppointmentDate, EyeTestToUpdate.PaidFees, EyeTestToUpdate.CreatedByUserID
                    , EyeTestToUpdate.TestApplicationID, EyeTestToUpdate.TestResult, EyeTestToUpdate.Notes, EyeTestToUpdate.ResultAddedByUserID);
            }

            public static EyeTest Find(int EyeTestID)
            {
                var EyeTest = AccessEyeTestData.Find(EyeTestID);

                if (EyeTest == null)
                    return null;
                else
                    return EyeTest.ToEyeTest();
            }

            public static DataTable ListAllEyeTests()
            {
                return AccessEyeTestData.ListAllEyeTests();
            }

            public static bool IsExist(int EyeTestIDToFind)
            {
                return AccessEyeTestData.IsExist(EyeTestIDToFind);
            }

            public static bool DeleteEyeTest(int EyeTestIDToFind)
            {
                return AccessEyeTestData.DeleteEyeTest(EyeTestIDToFind);
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

            public static bool SetActivationSatuts(int InternationalLicenseIDToEdit,bool Status)
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

                if (SettingsClass.IsDriverOledEnough((DateTime)DriverBirthDate, LicenseToAdd.LicenseClass)) 
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
            
            public static License FindLastActiveLicense(int DriverID,LicenseClass LicenseClass)
            {
                DTLicense License = AccessLicenseData.FindLastActiveLicense(DriverID, LicenseClass);

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

        }

        public static class MangeLocalDrivingLicenseApplications
        {
            public static bool Add(LocalDrivingLicenseApplication LocalDrivingLicenseApplicationToAdd)
            {

                if (LocalDrivingLicenseApplicationToAdd.LocalDrivingLicenseApplicationID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTLocalDrivingLicenseApplication U = AccessLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                        (LocalDrivingLicenseApplicationToAdd.ApplicationID, LocalDrivingLicenseApplicationToAdd.LicenseClassID,
                            LocalDrivingLicenseApplicationToAdd.EyeTestID);

                    return U != null;
                }
            }

            public static bool Update(LocalDrivingLicenseApplication LocalDrivingLicenseApplicationToUpdate)
            {
                if (LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID == -1)
                    return false;

                if (!AccessLocalDrivingLicenseApplicationData.IsExist(LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID))
                { return false; }

                return AccessLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                    (LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplicationToUpdate.ApplicationID,
                    LocalDrivingLicenseApplicationToUpdate.LicenseClassID, LocalDrivingLicenseApplicationToUpdate.EyeTestID);
            }

            public static LocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
            {
                var LocalDrivingLicenseApplication = AccessLocalDrivingLicenseApplicationData.Find(LocalDrivingLicenseApplicationID);

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

            public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationIDToFind)
            {
                return AccessLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationIDToFind);
            }

            public static DateTime GetExpirationDateOfLicense(int LicenseID)
            {
                return AccessLicenseData.GetExpirationDateOfLicense(LicenseID);
            }
            
        }

        public static class MangeTests
        {
            public static bool Add(Test TestToAdd)
            {

                if (TestToAdd.TestID != -1)
                { return false; }

                else
                {
                    DataAccessTier.DTTest U = AccessTestData.AddNewTest
                        (TestToAdd.TestType, TestToAdd.LocalDrivingLicenseApplicationID, TestToAdd.AppointmentDate, TestToAdd.PaidFees,
                            TestToAdd.AppointmentMadeByUserID, TestToAdd.TestApplicationID, TestToAdd.TestResult,
                            TestToAdd.Notes, TestToAdd.ResultAddedByUserID);

                    return U != null;
                }
            }

            public static bool Update(Test TestToUpdate)
            {
                if (TestToUpdate.TestID == -1)
                    return false;

                if (!AccessTestData.IsExist(TestToUpdate.TestID))
                { return false; }

                return AccessTestData.UpdateTest(TestToUpdate.TestID, TestToUpdate.TestType, TestToUpdate.LocalDrivingLicenseApplicationID,
                    TestToUpdate.AppointmentDate, TestToUpdate.PaidFees, TestToUpdate.AppointmentMadeByUserID, TestToUpdate.TestApplicationID,
                    TestToUpdate.TestResult, TestToUpdate.Notes, TestToUpdate.ResultAddedByUserID);
            }

            public static Test Find(int TestID)
            {
                var TestData = AccessTestData.Find(TestID);

                if (TestData == null)
                    return null;
                else
                    return TestData.ToTest();
            }

            public static DataTable ListAllTests()
            {
                return AccessTestData.ListAllTests();
            }

            public static bool IsExist(int TestIDToFind)
            {
                return AccessTestData.IsExist(TestIDToFind);
            }

            public static bool DeleteTest(int TestIDToFind)
            {
                return AccessTestData.DeleteTest(TestIDToFind);
            }

        }


        // the complaxe Function will be out here. //

        public static bool AddIssuingInternationalLicenseApplication(Application ApplicationToAdd,InternationalLicense InternationalLicenseToAdd,LicenseClass LicenseClass)
        {
            if (!AccessLicenseData.IsExist(InternationalLicenseToAdd.IssuedUsingLocalLicenseID))
            { return false; }

            if ((ApplicationToAdd.PaidFees) < SettingsClass.GetInternationalLicensIssuanceFees 
                ((LicenseClass)AccessLicenseData.GetLicenseClass(InternationalLicenseToAdd.IssuedUsingLocalLicenseID)) +
                AccessApplicationData.GetApplicationFees(ApplicationToAdd.ApplicationTypeID))  // International License Fee + Application Fee
            { return false; }

            if( AccessInternationalLicenseData.DoesHaveActiveInternationalLicenseOfTheSameClass(InternationalLicenseToAdd.DriverID, LicenseClass))
            { return false; }

            var Application = AccessApplicationData.AddNewApplication(ApplicationToAdd.ApplicantPersonID, DateTime.Now,
                ApplicationToAdd.ApplicationTypeID, ApplicationToAdd.ApplicationStatus, ApplicationToAdd.LastStatusDate, ApplicationToAdd.PaidFees,
                LogedInUser.UserID);
        
            if(Application == null)
                return false;

            return AccessInternationalLicenseData.AddNewInternationalLicense(Application.ApplicationID, InternationalLicenseToAdd.DriverID,
                InternationalLicenseToAdd.IssuedUsingLocalLicenseID, DateTime.Now, AccessInternationalLicenseData.GetInternationalLicenseExpiretionDate(InternationalLicenseToAdd.IssuedUsingLocalLicenseID),
                InternationalLicenseToAdd.IsActive, LogedInUser.UserID) != null;            
        }

        public static bool AddIssuingInternationalLicenseApplication(Application ApplicationToAdd, InternationalLicense InternationalLicenseToAdd)
        {
            return AddIssuingInternationalLicenseApplication
                (ApplicationToAdd, InternationalLicenseToAdd, (LicenseClass)AccessInternationalLicenseData.GetInternationalLicenseClass(InternationalLicenseToAdd.InternationalLicenseID));
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

        internal int _CountryID;
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

        internal int _DetainID;
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
        public ApplicationType ApplicationTypeID { get { return _ApplicationTypeID; } }
        private ApplicationType _ApplicationTypeID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }
        private int _CreatedByUserID;

        public ApplicationStatus ApplicationStatus;
        public DateTime LastStatusDate; //
        public decimal PaidFees;

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

        internal int _UserID;
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

        internal int _PersonID;
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
        
        public  Driver(int PersonID)
        {
            this._DriverID = -1;
            this.PersonID = PersonID;
        }

        internal int _DriverID;
        public int DriverID { get { return _DriverID; } }
        public int PersonID;

        internal int _CreatedByUserID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }
        
        internal DateTime _CreatedDate;
        public DateTime CreatedDate { get { return _CreatedDate; } }

    }

    public class EyeTest
    {
        internal EyeTest(int EyeTestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            this._EyeTestID = EyeTestID;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this.TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
    }
        
        public EyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID)
        {
            this._EyeTestID = -1;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this.TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this.Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }
        
        public EyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, string Notes)
        {
            this._EyeTestID = -1;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this.TestApplicationID = TestApplicationID;
            this._TestResult = null;
            this.Notes = Notes;
            this._ResultAddedByUserID = null;
        }

        public void SetResult(bool TestResult, int ResultAddedByUserID)
        {
            this._TestResult = TestResult;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        internal int _EyeTestID;
        public int EyeTestID { get { return _EyeTestID; } }
        public int PersonID;
        public DateTime AppointmentDate;
        public Decimal PaidFees;
        public int CreatedByUserID;
        public int TestApplicationID;

        public bool? TestResult { get { return _TestResult;  } }
        public bool? _TestResult;
        
        public string Notes;

        public int? ResultAddedByUserID { get { return _ResultAddedByUserID; } }
        public int? _ResultAddedByUserID;

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
        }

        internal int _InternationalLicenseID;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }

        public int ApplicationID;
        public int DriverID;
        public int IssuedUsingLocalLicenseID;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public bool IsActive;
        public int CreatedByUserID;
        public LicenseClass LicenseClass 
            { get { return AccessInternationalLicenseData.GetInternationalLicenseClass(_InternationalLicenseID).Value; } }


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

        internal int _LicenseID;
        public int LicenseID { get { return _LicenseID; } }

        public int LocalDrivingLicenseApplicationID;
        public int DriverID;
        public LicenseClass LicenseClass;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public string Notes;
        public bool IsActive;
        public IssueReason IssueReason;

        internal int _CreatedByUserID;
        public int CreatedByUserID { get { return _CreatedByUserID; } }

    }

    public class LocalDrivingLicenseApplication
    {
        internal LocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.EyeTestID = EyeTestID;
        }
        
        public LocalDrivingLicenseApplication(int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID)
        {
            this._LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.EyeTestID = EyeTestID;
        }

        internal int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }

        public int ApplicationID;
        public LicenseClass LicenseClassID;
        public int? EyeTestID;
    }

    public class Test
    {
        internal Test(int TestID, TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            this._TestID = TestID;
            this.TestType = TestType;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.AppointmentMadeByUserID = AppointmentMadeByUserID;
            this.TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this._Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        public Test(TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID)
        {
            this._TestID = -1;
            this.TestType = TestType;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.AppointmentMadeByUserID = AppointmentMadeByUserID;
            this.TestApplicationID = TestApplicationID;
        }

        // Any of them Cuold be Null
        public void SetReleaseInfo(bool TestResult, string Notes, int ResultAddedByUserID)
        {
            this._TestResult = TestResult;
            this._Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }


        internal int _TestID;
        public int TestID { get { return _TestID; } }
        public TestType TestType;
        public int LocalDrivingLicenseApplicationID;
        public DateTime AppointmentDate;
        public decimal PaidFees;
        public int AppointmentMadeByUserID;
        public int TestApplicationID;

        private bool? _TestResult = null;
        private string _Notes = null;
        private int? _ResultAddedByUserID = null;

        public bool? TestResult { get { return _TestResult; } }
        public string Notes { get { return _Notes; } }
        public int? ResultAddedByUserID { get { return _ResultAddedByUserID; } }
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
        { return new EyeTest(EyeTest.EyeTestID, EyeTest.PersonID, EyeTest.AppointmentDate, EyeTest.PaidFees, EyeTest.CreatedByUserID, EyeTest.TestApplicationID, EyeTest.TestResult,EyeTest.Notes, EyeTest.ResultAddedByUserID); }
        public static InternationalLicense ToInternationalLicense(this DTInternationalLicense InternationalLicense)
        { return new InternationalLicense(InternationalLicense.InternationalLicenseID, InternationalLicense.ApplicationID, InternationalLicense.DriverID, InternationalLicense.IssuedUsingLocalLicenseID, InternationalLicense.IssueDate, InternationalLicense.ExpirationDate, InternationalLicense.IsActive, InternationalLicense.CreatedByUserID); }
        public static License ToLicense(this DTLicense License)
        { return new License(License.LicenseID, License.LocalDrivingLicenseApplicationID, License.DriverID, License.LicenseClass, License.IssueDate, License.ExpirationDate, License.Notes, License.IsActive, License.IssueReason, License.CreatedByUserID); }
        public static LocalDrivingLicenseApplication ToLocalDrivingLicenseApplication(this DTLocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        { return new LocalDrivingLicenseApplication(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, LocalDrivingLicenseApplication.ApplicationID, LocalDrivingLicenseApplication.LicenseClassID, LocalDrivingLicenseApplication.EyeTestID); }
        public static Test ToTest(this DTTest Test)
        { return new Test(Test.TestID, Test.TestType, Test.LocalDrivingLicenseApplicationID, Test.AppointmentDate, Test.PaidFees, Test.AppointmentMadeByUserID, Test.TestApplicationID, Test.TestResult, Test.Notes, Test.ResultAddedByUserID); }
    }

}
