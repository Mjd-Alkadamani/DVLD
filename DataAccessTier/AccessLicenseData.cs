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

    internal static class LicenseClassExtentions
    {
        internal static int ToDBValue(this LicenseClass License)
        { return (int)License; }

        internal static LicenseClass ToLicenseClass(this int RowDatabaseData)
        {
            switch (RowDatabaseData)
            {
                case 1:
                    return LicenseClass.Motorcycles;
                case 2:
                    return LicenseClass.LargeMotorcycles;
                case 3:
                    return LicenseClass.RegularCar;
                case 4:
                    return LicenseClass.PublicVehicles;
                case 5:
                    return LicenseClass.AgriculturalVehicles;
                case 6:
                    return LicenseClass.Buses;
                default: //7
                    return LicenseClass.HeavyVhicles;


            }

        }

    }

    internal static class IssueReasonExtentions
    {
        internal static int ToDBValue(this IssueReason IssueReason)
        { return (int)IssueReason; }

        internal static IssueReason ToIssueReason(this byte RowDatabaseData)
        {
            switch (RowDatabaseData)
            {
                case 1:
                    return IssueReason.NewIssuance;
                case 2:
                    return IssueReason.Renewal;
                case 3:
                    return IssueReason.DamagedReplacement;
                default: // 4
                    return IssueReason.LostReplacement;
            }
        }

    }

    public class DTLicense
    {
        internal protected DTLicense(int LicenseID, int LocalDrivingLicenseApplicationID, int DriverID, LicenseClass LicenseClass
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
            this.CreatedByUserID = CreatedByUserID;
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
        public int CreatedByUserID;

    }

    public class AccessLicenseData
    {
        public static DTLicense Find(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[LocalDrivingLicenseApplicationID]"
                  + ",[DriverID]"
                  + ",[LicenseClass]"
                  + ",[IssueDate]"
                  + ",[ExpirationDate]"
                  + ",[Notes]"
                  + ",[IsActive]"
                  + ",[IssueReason]"
                  + ",[CreatedByUserID]"
                    + " FROM[dbo].[Licenses]"
                    + " where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            DTLicense FindedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLicense = new DTLicense
                     (LicenseID,
                       (int)Reader["LocalDrivingLicenseApplicationID"],
                       (int)Reader["DriverID"],
                       ((int)Reader["LicenseClass"]).ToLicenseClass(),
                       (DateTime)Reader["IssueDate"],
                       (DateTime)Reader["ExpirationDate"],
                       (Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"]),
                       (bool)Reader["IsActive"],
                       ((byte)Reader["IssueReason"]).ToIssueReason(),
                       (int)Reader["CreatedByUserID"]
                     );
                    //new DTLicense( LicenseID,(int)Reader["LocalDrivingLicenseApplicationID"],(int)Reader["DriverID"],LicenseClass.Buses,(DateTime)Reader["IssueDate"],(DateTime)Reader["ExpirationDate"],(string)Reader["Notes"],(bool)Reader["IsActive"],IssueReason.DamagedReplacement,(int)Reader["CreatedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedLicense;

        }

        public static DTLicense FindTheActiveLicense(int DriverID,LicenseClass LicenseClass)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[LicenseID]"
                  + ",[LocalDrivingLicenseApplicationID]"
                  + ",[IssueDate]"
                  + ",[ExpirationDate]"
                  + ",[Notes]"
                  + ",[IssueReason]"
                  + ",[CreatedByUserID]"
                    + " FROM[dbo].[Licenses]"
                    + " where DriverID = @DriverID and LicenseClass = @LicenseClass and IsActive = @IsActive";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass.ToDBValue());
            Command.Parameters.AddWithValue("@IsActive", true);
            DTLicense FindedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLicense = new DTLicense
                     ((int)Reader["LicenseID"],
                       (int)Reader["LocalDrivingLicenseApplicationID"],
                       DriverID,
                       LicenseClass,
                       (DateTime)Reader["IssueDate"],
                       (DateTime)Reader["ExpirationDate"],
                       (Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"]),
                       true,
                       ((byte)Reader["IssueReason"]).ToIssueReason(),
                       (int)Reader["CreatedByUserID"]
                     );
                    //new DTLicense( LicenseID,(int)Reader["LocalDrivingLicenseApplicationID"],(int)Reader["DriverID"],LicenseClass.Buses,(DateTime)Reader["IssueDate"],(DateTime)Reader["ExpirationDate"],(string)Reader["Notes"],(bool)Reader["IsActive"],IssueReason.DamagedReplacement,(int)Reader["CreatedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedLicense;

        }
        
        public static bool DoseHaveActiveLicense(int DriverID,LicenseClass LicenseClass)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1 DoseExist = 1"
                    + " FROM[dbo].[Licenses]"
                    + " where DriverID = @DriverID and LicenseClass = @LicenseClass and IsActive = @IsActive";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass.ToDBValue());
            Command.Parameters.AddWithValue("@IsActive", true);
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

        public static bool IsExist(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Licenses where  LicenseID =  @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

        public static DataTable ListAllLicenses()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[LicenseID]"
                  + ",[LocalDrivingLicenseApplicationID]"
                  + ",[DriverID]"
                  + ",[LicenseClass]"
                  + ",[IssueDate]"
                  + ",[ExpirationDate]"
                  + ",[Notes]"
                  + ",[IsActive]"
                  + ",[IssueReason]"
                  + ",[CreatedByUserID]"
                    + " FROM[dbo].[Licenses]";

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

        private static int? _AddNewLicense(ref DTLicense LicenseToAdd)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Licenses]" +
           " VALUES " +
           "( @LocalDrivingLicenseApplicationID" +
           ", @DriverID" +
           ", @LicenseClass" +
           ", @IssueDate" +
           ", @ExpirationDate" +
           ", @Notes" +
           ", @IsActive" +
           ", @IssueReason" +
           ", @CreatedByUserID );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseToAdd.LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@DriverID", LicenseToAdd.DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseToAdd.LicenseClass.ToDBValue());
            Command.Parameters.AddWithValue("@IssueDate", LicenseToAdd.IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", LicenseToAdd.ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", LicenseToAdd.Notes);
            Command.Parameters.AddWithValue("@IsActive", LicenseToAdd.IsActive);
            Command.Parameters.AddWithValue("@IssueReason", LicenseToAdd.IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", LicenseToAdd.CreatedByUserID);

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

            LicenseToAdd._LicenseID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }

        public static DTLicense AddNewLicense(int LocalDrivingLicenseApplicationID, int DriverID, LicenseClass LicenseClass
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, IssueReason IssueReason, int CreatedByUserID)
        {
            DTLicense NewLicense = new DTLicense(-1, LocalDrivingLicenseApplicationID, DriverID, LicenseClass,
                IssueDate, ExpirationDate, Notes, IsActive, IssueReason, CreatedByUserID);

            int? ID = _AddNewLicense(ref NewLicense);

            if (ID != null)
            {
                return NewLicense; // the ( _AddNewLicense ) method will insert the new ID to the NewLicense Objedct
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateLicense(int LicenseID, int LocalDrivingLicenseApplicationID, int DriverID, LicenseClass LicenseClass
            , DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, IssueReason IssueReason, int CreatedByUserID)
        {
            if (!IsExist(LicenseID))
                return false;

            return UpdateLicense(new DTLicense(LicenseID, LocalDrivingLicenseApplicationID, DriverID, LicenseClass
            , IssueDate, ExpirationDate, Notes, IsActive, IssueReason, CreatedByUserID));
        }

        public static bool UpdateLicense(DTLicense LicenseToUpdate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Licenses]" +
              "SET " +
              " [LocalDrivingLicenseApplicationID]   = @LocalDrivingLicenseApplicationID" +
              ",[DriverID]        = @DriverID" +
              ",[LicenseClass]    = @LicenseClass" +
              ",[IssueDate]       = @IssueDate" +
              ",[ExpirationDate]  = @ExpirationDate" +
              ",[Notes]           = @Notes" +
              ",[IsActive]        = @IsActive" +
              ",[IssueReason]     = @IssueReason" +
              ",[CreatedByUserID] = @CreatedByUserID" + 
                       " WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseToUpdate.LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@DriverID", LicenseToUpdate.DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseToUpdate.LicenseClass.ToDBValue());
            Command.Parameters.AddWithValue("@IssueDate", LicenseToUpdate.IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", LicenseToUpdate.ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", LicenseToUpdate.Notes);
            Command.Parameters.AddWithValue("@IsActive", LicenseToUpdate.IsActive);
            Command.Parameters.AddWithValue("@IssueReason", LicenseToUpdate.IssueReason.ToDBValue());
            Command.Parameters.AddWithValue("@CreatedByUserID", LicenseToUpdate.CreatedByUserID);

            Command.Parameters.AddWithValue("@LicenseID", LicenseToUpdate.LicenseID);

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

        internal static bool DeactivateLicense(int LicenseIDToDeactivate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Licenses]" +
              "SET " +
              "[IsActive]        = @IsActive" +
                       " WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@IsActive", false);
            
            Command.Parameters.AddWithValue("@LicenseID", LicenseIDToDeactivate);

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

        internal static bool ActivateLicense(int LicenseIDToActivate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Licenses]" +
              "SET " +
              "[IsActive]        = @IsActive" +
                       " WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@IsActive", true);

            Command.Parameters.AddWithValue("@LicenseID", LicenseIDToActivate);

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

        public static bool DeleteLicense(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "delete from [dbo].[Licenses]" +
                   " WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", IDToDelete);

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

        public static DateTime GetExpirationDateOfLicense(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[ExpirationDate]"
                    + " FROM[dbo].[Licenses]"
                    + " where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            DateTime ExpirationDate = new DateTime(1,1,1);

            try
            {
                Connection.Open();
                Object ExpirationDateObject = Command.ExecuteScalar();

                if (ExpirationDateObject != null)
                {
                    ExpirationDate = (DateTime)ExpirationDateObject;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return ExpirationDate;

        }

        public static DateTime GetExpirationDateOfLicense(DTLicense License)
        {
            return GetExpirationDateOfLicense(License.LicenseID);
        }

        public static LicenseClass? GetLicenseClass(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[LicenseClass]"
                    + " FROM [dbo].[Licenses]"
                    + " where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            LicenseClass? FindedClass = null;

            try
            {
                Connection.Open();
                object LicenseClass = Command.ExecuteScalar();

                if(LicenseClass != null)
                FindedClass = (LicenseClass)LicenseClass;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedClass;

        }


    }

}
