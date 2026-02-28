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
    public class DTInternationalLicense
    {
        internal protected DTInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
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

        internal int _InternationalLicenseID;
        public int InternationalLicenseID { get { return _InternationalLicenseID; } }

        public int ApplicationID;
        public int DriverID;
        public int IssuedUsingLocalLicenseID;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public bool IsActive;
        public int CreatedByUserID;

    }

    public class AccessInternationalLicenseData
    {
        public static DTInternationalLicense Find(int InternationalLicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[ApplicationID]"
              + ",[DriverID]"
              + ",[IssuedUsingLocalLicenseID]"
              + ",[IssueDate]"
              + ",[ExpirationDate]"
              + ",[IsActive]"
              + ",[CreatedByUserID]"
              + " FROM[dbo].[InternationalLicenses]"
                    + " where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            DTInternationalLicense FindedInternationalLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedInternationalLicense = new DTInternationalLicense
                     (InternationalLicenseID,
                       (int)Reader["ApplicationID"],
                       (int)Reader["DriverID"],
                       (int)Reader["IssuedUsingLocalLicenseID"],
                       (DateTime)Reader["IssueDate"],
                       (DateTime)Reader["ExpirationDate"] ,
                       (bool)Reader["IsActive"] ,
                       (int)Reader["CreatedByUserID"]
                     );
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedInternationalLicense;

        }
        
        public static DTInternationalLicense FindByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
              + "[InternationalLicenseID]"
              + ",[DriverID]"
              + ",[IssuedUsingLocalLicenseID]"
              + ",[IssueDate]"
              + ",[ExpirationDate]"
              + ",[IsActive]"
              + ",[CreatedByUserID]"
              + " FROM[dbo].[InternationalLicenses]"
                    + " where ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            DTInternationalLicense FindedInternationalLicense = null;

            try
            {
                Connection.Open();
                
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedInternationalLicense = new DTInternationalLicense
                     ((int)Reader["InternationalLicenseID"],
                       ApplicationID,
                       (int)Reader["DriverID"],
                       (int)Reader["IssuedUsingLocalLicenseID"],
                       (DateTime)Reader["IssueDate"],
                       (DateTime)Reader["ExpirationDate"] ,
                       (bool)Reader["IsActive"] ,
                       (int)Reader["CreatedByUserID"]
                     );
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedInternationalLicense;

        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "select top 1 isExist = 1 from InternationalLicenses where  LicenseID =  @LicenseID and ReleaseDate = null";

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

        public static bool IsExist(int InternationalLicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from InternationalLicenses where  InternationalLicenseID =  @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
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

        public static DataTable ListAllInternationalLicenses()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
      + "[InternationalLicenseID]"
      + ",[ApplicationID]"
      + ",[DriverID]"
      + ",[IssuedUsingLocalLicenseID]"
      + ",[IssueDate]"
      + ",[ExpirationDate]"
      + ",[IsActive]"
      + ",[CreatedByUserID]"
         + " FROM [dbo].[InternationalLicenses]";

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

        private static int? _AddNewInternationalLicense(ref DTInternationalLicense InternationalLicenseToAdd)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[InternationalLicenses]" +
           " VALUES " +
           "( @ApplicationID" +
           ", @DriverID" +
           ", @IssuedUsingLocalLicenseID" +
           ", @IssueDate" +
           ", @ExpirationDate" +
           ", @IsActive" +
           ", @CreatedByUserID );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", InternationalLicenseToAdd.ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", InternationalLicenseToAdd.DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", InternationalLicenseToAdd.IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", InternationalLicenseToAdd.IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", InternationalLicenseToAdd.ExpirationDate);
            Command.Parameters.AddWithValue("@IsActive", InternationalLicenseToAdd.IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", InternationalLicenseToAdd.CreatedByUserID);

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

            InternationalLicenseToAdd._InternationalLicenseID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }

        public static DTInternationalLicense AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
            , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            DTInternationalLicense NewLicense = new DTInternationalLicense(-1, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            int? ID = _AddNewInternationalLicense(ref NewLicense);

            if (ID != null)
            {
                return NewLicense; // the ( _AddNewInternationalLicense ) method will insert the new ID to the NewLicense Objedct
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateInternationalLicense (int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID
            , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            if (!IsExist(InternationalLicenseID))
                return false;

            return UpdateInternationalLicense(new DTInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID));
        }

        public static bool UpdateInternationalLicense(DTInternationalLicense InternationalLicenseToUpdate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[InternationalLicenses]" +
              "SET" +
              " [ApplicationID]            = @ApplicationID" +
              ",[DriverID]           = @DriverID" +
              ",[IssuedUsingLocalLicenseID]            = @IssuedUsingLocalLicenseID" +
              ",[IssueDate]             = @IssueDate" +
              ",[IsActive]              = @IsActive" +
              ",[CreatedByUserID]                = @CreatedByUserID" +
                   " WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", InternationalLicenseToUpdate.ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", InternationalLicenseToUpdate.DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", InternationalLicenseToUpdate.IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", InternationalLicenseToUpdate.IssueDate);
            Command.Parameters.AddWithValue("@IsActive", InternationalLicenseToUpdate.IsActive);
            Command.Parameters.AddWithValue("@CreatedByUserID", InternationalLicenseToUpdate.CreatedByUserID);

            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseToUpdate.InternationalLicenseID);

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

        public static bool DeleteInternationalLicense(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "delete from [dbo].[InternationalLicenses] " +
                   " WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@InternationalLicenseID", IDToDelete);

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

        public static DateTime GetInternationalLicenseExpiretionDate(DTLicense LocalLicense)
        {
            if(DataAccessSettings.AllowInternationalLicenseExpirationDateToByPassLocalOne)
            {
                return DateTime.Now + new TimeSpan(DataAccessSettings.HowManyYearsForInternationalLicense() * 364, 0, 0,0);
            }


            DateTime LocalLicenseExpirationDate = AccessLicenseData.GetExpirationDateOfLicense(LocalLicense);

            DateTime InternationalLicenseExpirationDate = DateTime.Now + new TimeSpan(DataAccessSettings.HowManyYearsForInternationalLicense() * 364, 0, 0,0);

            if (LocalLicenseExpirationDate > InternationalLicenseExpirationDate)
            {
                return InternationalLicenseExpirationDate;
            }
            else
            {
                return LocalLicenseExpirationDate;
            }
        }
        
        public static DateTime GetInternationalLicenseExpiretionDate(int LocalLicenseID)
        {
            DTLicense License = AccessLicenseData.Find(LocalLicenseID);

            if (License != null)
                return GetInternationalLicenseExpiretionDate(License);
            else
                return DateTime.Now;
        }


        public static bool DoesHaveActiveInternationalLicenseOfTheSameClass(int DriverID, LicenseClass LicenseClass)

        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from InternationalLicenses inner join "
                        + "Licenses on InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID "
                        + "Where InternationalLicenses.DriverID = @DriverID and LicenseClass =@LicenseClass and InternationalLicenses.IsActive = @IsActive";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
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

        public static LicenseClass? GetInternationalLicenseClass(int InternationalLicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                @"select LicenseClass from InternationalLicenses inner join "
                    + "Licenses on InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID "
                    + "Where InternationalLicenses.InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            LicenseClass? FindedInternationalLicenseClass = null;

            try
            {
                Connection.Open();
                object Class = Command.ExecuteScalar();

                FindedInternationalLicenseClass = (LicenseClass)Class;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedInternationalLicenseClass;

        }

        // Use it only if very necessary 
        public static LicenseClass? GetInternationalLicenseClassByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select Licenses.LicenseClass "
                           +"from(InternationalLicenses inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID) "
                           +"inner join Licenses on InternationalLicenses.IssuedUsingLocalLicenseID = Licenses.LicenseID "
                           + "Where Applications.ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            LicenseClass? FindedInternationalLicenseClass = null;

            try
            {
                Connection.Open();
                object Class = Command.ExecuteScalar();

                FindedInternationalLicenseClass = (LicenseClass)Class;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedInternationalLicenseClass;

        }

        public static bool SetActivationSatuts(int InternationalLicenseID, bool ActivationSatut)

        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE [dbo].[InternationalLicenses]" +
              "SET " +
              "[IsActive] = @IsActive" +
                   " WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@IsActive", ActivationSatut);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

    }

}
