using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using General;


namespace DataAccessTier
{
    
    internal static class ApplicationTypeExtentions
    {
        internal static int ToDBValue (this ApplicationType App )
        { return (int)App; }

        internal static ApplicationType ToApplicationType (this int RowDatabaseData )
        {
            switch(RowDatabaseData)
            {
                case 1:
                    return ApplicationType.LicenseIssuance;
                case 2:
                    return ApplicationType.RetakeTest;
                case 3:
                    return ApplicationType.RenewDrivingLicense;
                case 4:
                    return ApplicationType.MissingReplacement;
                case 5:
                    return ApplicationType.DamagedReplacement;
                case 6:
                    return ApplicationType.ReleaseLicense;
                default: //7
                    return ApplicationType.IssuingInternationalLicense;


            }
            
        }

    }
    
    internal static class ApplicationStatusExtentions
    {
        internal static int ToDBValue (this ApplicationStatus App )
        { return (int)App; }

        internal static ApplicationStatus ToApplicationStatus(this byte RowDatabaseData )
        {
            switch(RowDatabaseData)
            {
                case 1:
                    return ApplicationStatus.New;
                case 2:
                    return ApplicationStatus.Canceled;
                default: //3
                    return ApplicationStatus.Completed;
            }            
        }

    }

    public class DTApplication
    {
  
        internal protected DTApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, ApplicationType ApplicationTypeID, ApplicationStatus ApplicationStatus,
            DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this._ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

        }

        internal int _ApplicationID;
        public int ApplicationID { get { return _ApplicationID; } }
        public int ApplicantPersonID;
        public DateTime ApplicationDate;
        public ApplicationType ApplicationTypeID;
        public ApplicationStatus ApplicationStatus; 
        public DateTime LastStatusDate;
        public decimal PaidFees;
        public int CreatedByUserID;

    }

    public class AccessApplicationData
    {

        internal static bool CouldAttachTestOfTypeToApplication(int ApplicationID,TestType Type)
        {
            switch (GetApplicationType(ApplicationID))
            { 
                case ApplicationType.LicenseIssuance:
                    switch(Type)
                    {
                        case TestType.EyeTest:
                            if (AccessEyeTestData.IsExistByApplicationID(ApplicationID))
                                return false;
                            else
                                return true;

                        case TestType.DrivingTest:
                            if(AccessDrivingTestData.IsExistByApplicationID(ApplicationID))
                                return false;
                            else
                                return true;

                        default:  //TestType.TheoreticalTest
                            if (AccessTheoreticalTestData.IsExistByApplicationID(ApplicationID))
                                return false;
                            else
                                return true;
                    }                   

                case ApplicationType.RetakeTest:
                    if (AccessEyeTestData.IsExistByApplicationID(ApplicationID) || AccessDrivingTestData.IsExistByApplicationID(ApplicationID) || AccessTheoreticalTestData.IsExistByApplicationID(ApplicationID))
                        return false;
                    else
                        return true;

                case ApplicationType.RenewDrivingLicense:
                    if (TestType.EyeTest != Type)
                        return false;
                    else
                        if (AccessEyeTestData.IsExistByApplicationID(ApplicationID))
                        return false;
                    else
                        return true;

                default:
                    return false;
                    
            }
        }

        public static DataTable GetAllPersonApplications(int PersonID,ApplicationType? TypeSpecific = null)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "select * from [Applications] " +
                            " where  [Applications].ApplicantPersonID  = " + PersonID.ToString() +
            (TypeSpecific != null ? "and [Applications].ApplicationTypeID = " + (int)TypeSpecific : "");


            DataTable Table = new DataTable();
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                Table.Load(Reader);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return Table;


        }

        public static DTApplication Find(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicantPersonID]"
              + ",[ApplicationDate]"
              + ",[ApplicationTypeID]"
              + ",[ApplicationStatus]"
              + ",[LastStatusDate]"
              + ",[PaidFees]"
              + ",[CreatedByUserID]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicationID] = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            DTApplication FindedApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedApplication = new DTApplication
                        (ApplicationID,
                        (int)Reader["ApplicantPersonID"],
                        (DateTime)Reader["ApplicationDate"],
                        ((int)Reader["ApplicationTypeID"]).ToApplicationType(),
                        ((byte)Reader["ApplicationStatus"]).ToApplicationStatus(),
                        (DateTime)Reader["LastStatusDate"],
                        (decimal)Reader["PaidFees"],
                        (int)Reader["CreatedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedApplication;

        }

        public static DTApplication FindLastPersonAppicationOfType(int PersonID, ApplicationType Type)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1 "
              + " [ApplicationID]"
              + ",[ApplicationDate]" 
              + ",[ApplicationStatus]"
              + ",[LastStatusDate]"
              + ",[PaidFees]"
              + ",[CreatedByUserID]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicantPersonID] = @ApplicantPersonID and [ApplicationTypeID] = @ApplicationTypeID"+
                " order by ApplicationDate Desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", Type.ToDBValue());
            DTApplication FindedApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedApplication = new DTApplication
                        ((int)Reader["ApplicationID"],
                        PersonID,
                        (DateTime)Reader["ApplicationDate"],
                        Type,
                        ((byte)Reader["ApplicationStatus"]).ToApplicationStatus(),
                        (DateTime)Reader["LastStatusDate"],
                        (decimal)Reader["PaidFees"],
                        (int)Reader["CreatedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedApplication;

        }

        public static bool? FindDosePersonHaveVliedAppicationOfType(int PersonID, ApplicationType Type)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1 "
              + " DoesExist = 1"
              + " FROM[dbo].[Applications]"
              + " where [ApplicantPersonID] = @ApplicantPersonID and [ApplicationTypeID] = @ApplicationTypeID" +
                " and [ApplicationDate] > @ApplicationDate order by ApplicationDate Desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", Type.ToDBValue());
            Command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now.Subtract(SettingsClass.Application.ApplicationExpirationPeriod));

            bool? IsExist = null;

            try
            {
                Connection.Open();

                object Exist = Command.ExecuteScalar();

                if (Exist != null)
                    IsExist = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsExist;

        }

        public static int GetPersonID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicantPersonID]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicationID] = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int? FindedID = null;

            try
            {
                Connection.Open();
                object IDObject = Command.ExecuteScalar();



                if (IDObject != null)
                {
                    FindedID = Convert.ToInt32(IDObject);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return (int)FindedID;

        }

        public static bool IsExist(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Applications where ApplicationID =  @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            bool IsExist = false;

            try
            {
                Connection.Open();

                object Exist = Command.ExecuteScalar();

                if (Exist != null)
                    IsExist = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsExist;

        }

        public static DataTable ListAllApplications()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT [ApplicationID]"
              + ",[ApplicantPersonID]"
              + ",[ApplicationDate]"
              + ",[ApplicationTypeID]"
              + ",[ApplicationStatus]"
              + ",[LastStatusDate]"
              + ",[PaidFees]"
              + ",[CreatedByUserID]"
              + " FROM[dbo].[Applications]";

            DataTable Table = new DataTable();
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                Table.Load(Reader);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return Table;
        }

        private static int? _AddNewApplication(ref DTApplication ApplicationToAdd)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Applications]" +
           " VALUES" +
           "( @ApplicantPersonID" +
           ", @ApplicationDate" +
           ", @ApplicationTypeID" +
           ", @ApplicationStatus" +
           ", @LastStatusDate" +
           ", @PaidFees" +
           ", @CreatedByUserID);" +
           "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationToAdd.ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationToAdd.ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationToAdd.ApplicationTypeID.ToDBValue());
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationToAdd.ApplicationStatus.ToDBValue());
            Command.Parameters.AddWithValue("@LastStatusDate", ApplicationToAdd.LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", ApplicationToAdd.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", ApplicationToAdd.CreatedByUserID);

            int? AddedID = null;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteScalar();

                if (DoesSucceded != null)
                    AddedID = Convert.ToInt32(DoesSucceded);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            ApplicationToAdd._ApplicationID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }

        public static DTApplication AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, ApplicationType ApplicationTypeID,
            ApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            DTApplication NewApplication = new DTApplication( -1, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            int? NewID = _AddNewApplication(ref NewApplication);

            if (NewID != null)
                return NewApplication; // the ( _AddNewApplication ) method will insert the new ID to the Application Object
            else
                return null;
            
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, ApplicationType ApplicationTypeID,
            ApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            if (!IsExist(ApplicationID))
                return false;

            return UpdateApplication(new DTApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
             ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID));

        }

        public static bool UpdateApplication(DTApplication ApplicationToUpdate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Applications] " +
              "SET" +
              " [ApplicantPersonID]  = @ApplicantPersonID" +
              ",[ApplicationDate]  = @ApplicationDate" +
              ",[ApplicationTypeID]  = @ApplicationTypeID" +
              ",[ApplicationStatus]  = @ApplicationStatus" +
              ",[LastStatusDate]  = @LastStatusDate" +
              ",[PaidFees]  = @PaidFees" +
              ",[CreatedByUserID]  = @CreatedByUserID" +
                   " WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationToUpdate.ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationToUpdate.ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationToUpdate.ApplicationTypeID.ToDBValue());
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationToUpdate.ApplicationStatus.ToDBValue());
            Command.Parameters.AddWithValue("@LastStatusDate", ApplicationToUpdate.LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", ApplicationToUpdate.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", ApplicationToUpdate.CreatedByUserID);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationToUpdate.ApplicationID);

            bool DoesUpdateSucceded = false;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteNonQuery();

                if (DoesSucceded != null)
                    DoesUpdateSucceded = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return DoesUpdateSucceded;
        }

        public static bool DeleteApplication(int ApplicationIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "delete from [dbo].[Applications]" +
                   "WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationIDToDelete);

            bool DoesDeletionSucceded = false;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteNonQuery();

                if (DoesSucceded != null)
                    DoesDeletionSucceded = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return DoesDeletionSucceded;
        }

        public static decimal GetApplicationFees(ApplicationType ApplicationType)

        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT ApplicationFees from ApplicationTypes where ApplicationTypeID =@ApplicationTypeID; ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationType.ToDBValue());
            decimal ApplicationFee = 0;

            try
            {
                Connection.Open();
                object Fee = Command.ExecuteScalar();

                if (Fee != null)
                {
                    ApplicationFee = (decimal)Fee;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationFee;

        }

        public static DateTime? GetApplicationCreationDate(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicationDate]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicationID] = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            DateTime? ApplicationDate = null;

            try
            {
                Connection.Open();
                object StatusObject = Command.ExecuteScalar();




                if (StatusObject != null)
                {
                    ApplicationDate = (DateTime)StatusObject;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationDate;

        }

        public static bool EditPaidFees(int ApplicationIDToEdit, decimal NewFees)

        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Applications] " +
              "SET" +
              "[PaidFees]  = @NewFees" +
                   " WHERE ApplicationID = @ApplicationIDToEdit";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NewFees", NewFees);

            Command.Parameters.AddWithValue("@ApplicationIDToEdit", ApplicationIDToEdit);

            bool DoesUpdateSucceded = false;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteNonQuery();

                if (DoesSucceded != null)
                    DoesUpdateSucceded = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return DoesUpdateSucceded;
        }

        public static ApplicationStatus? GetApplicationStatus(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicationStatus]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicationID] = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            ApplicationStatus? ApplicationStatus = null;

            try
            {
                Connection.Open();
                object StatusObject = Command.ExecuteScalar();




                if (StatusObject != null)
                {
                    ApplicationStatus = (ApplicationStatus)StatusObject;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationStatus;

        }

        public static ApplicationType? GetApplicationType(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicationTypeID]"
              + " FROM[dbo].[Applications]"
              + " where [ApplicationID] = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            ApplicationType? ApplicationType = null;

            try
            {
                Connection.Open();
                object TypeObject = Command.ExecuteScalar();




                if (TypeObject != null)
                {
                    ApplicationType = (ApplicationType)TypeObject;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ApplicationType;

        }
    
        
    }

}
