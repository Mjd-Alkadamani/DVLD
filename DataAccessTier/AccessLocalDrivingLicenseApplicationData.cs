using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Generale;

namespace DataAccessTier
{
    public class DTLocalDrivingLicenseApplication
    {
        internal protected DTLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
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

    public class AccessLocalDrivingLicenseApplicationData
    {
        public static DTLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT "
                  + " [ApplicationID]"
                  + ",[LicenseClassID]"
                  + ",[EyeTestID]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            DTLocalDrivingLicenseApplication FindedLocalDrivingLicenseApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication
                     (LocalDrivingLicenseApplicationID,
                       (int)Reader["ApplicationID"],
                       ((int)Reader["LicenseClassID"]).ToLicenseClass(),
                       Reader["EyeTestID"] == DBNull.Value ? null : (int?)Reader["EyeTestID"]
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

            return FindedLocalDrivingLicenseApplication;

        }

        public static LicenseClass? GetLocalDrivingApplicationClass(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT "
                  + "[LicenseClassID]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            LicenseClass? FindedClass = null;

            try
            {
                Connection.Open();
                object LicenseClass = Command.ExecuteScalar();

                FindedClass = LicenseClass == null ? null : (LicenseClass?)LicenseClass; 
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


        public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from LocalDrivingLicenseApplications where  LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

        public static DataTable ListAllLocalDrivingLicenseApplications()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                  + " [LocalDrivingLicenseApplicationID]"
                  + ",[ApplicationID]"
                  + ",[LicenseClassID]"
                  + ",[EyeTestID]"
                    + " FROM[dbo].[LocalDrivingLicenseApplications]";

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

        private static int? _AddNewLocalDrivingLicenseApplication(ref DTLocalDrivingLicenseApplication LocalDrivingLicenseApplicationToAdd)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[LocalDrivingLicenseApplications]" +
           " VALUES " +
           "( @ApplicationID" +
           " ,@LicenseClass" +
           " ,@EyeTestID);" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationToAdd.ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClass", (LocalDrivingLicenseApplicationToAdd.LicenseClassID).ToDBValue());
            if (LocalDrivingLicenseApplicationToAdd.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@EyeTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@EyeTestID", LocalDrivingLicenseApplicationToAdd.EyeTestID);
            }

            int? AddedID = null;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteNonQuery();

                if (DoesSucceded != null)
                    AddedID = (int)DoesSucceded;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            LocalDrivingLicenseApplicationToAdd._LocalDrivingLicenseApplicationID = AddedID ?? -1;
            return AddedID;

        }

        public static DTLocalDrivingLicenseApplication AddNewLocalDrivingLicenseApplication(int ApplicationID, LicenseClass LicenseClassID,
            int? EyeTestID)
        {
            DTLocalDrivingLicenseApplication NewLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication(-1, ApplicationID,
                LicenseClassID, EyeTestID);

            int? ID = _AddNewLocalDrivingLicenseApplication(ref NewLocalDrivingLicenseApplication);

            if (ID != null)
            {
                return NewLocalDrivingLicenseApplication; // the ( _AddNewLocalDrivingLicenseApplication ) method will insert the new ID to the NewLocalDrivingLicenseApplication Objedct
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID)
        {
            if (!IsExist(LocalDrivingLicenseApplicationID))
                return false;

            return UpdateLocalDrivingLicenseApplication(new DTLocalDrivingLicenseApplication
                (LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, EyeTestID));
        }

        public static bool UpdateLocalDrivingLicenseApplication(DTLocalDrivingLicenseApplication LocalDrivingLicenseApplicationToUpdate)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[LocalDrivingLicenseApplications]" +
              "SET " +
              "[ApplicationID]   = @ApplicationID" +
              ",[LicenseClassID]        = @LicenseClassID" +
              ",[EyeTestID]    = @EyeTestID" +
                       " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", LocalDrivingLicenseApplicationToUpdate.ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LocalDrivingLicenseApplicationToUpdate.LicenseClassID);
            
            if (LocalDrivingLicenseApplicationToUpdate.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@EyeTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@EyeTestID", LocalDrivingLicenseApplicationToUpdate.EyeTestID);
            }

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationToUpdate.LocalDrivingLicenseApplicationID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "delete from [dbo].[LocalDrivingLicenseApplications]" +
                   " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", IDToDelete);

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

    }

}
