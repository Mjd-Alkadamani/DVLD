using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Generale;

namespace DataAccessTier
{
    public class DTDriver
    {

        internal protected DTDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime DriverDate)
        {
            this._DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = DriverDate;

        }

        internal int _DriverID;
        public int DriverID { get { return _DriverID; } }
        public int PersonID;
        public int CreatedByUserID;
        public DateTime CreatedDate;

    }

    public class AccessDriverData
    {
        public static DTDriver Find(int DriverID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                      + " [PersonID]"
                      + ",[CreatedByUserID]"
                      + ",[CreatedDate]"
                      + " FROM[dbo].[Drivers]"
                      + " where [DriverID] = @DriverID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            DTDriver FindedDriver = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedDriver = new DTDriver
                        (DriverID,
                        (int)Reader["PersonID"],
                        (int)Reader["CreatedByUserID"],
                        (DateTime)Reader["CreatedDate"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedDriver;

        }

        public static DTDriver FindByPersonID(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                      + "[DriverID]"
                      + ",[CreatedByUserID]"
                      + ",[CreatedDate]"
                      + " FROM[dbo].[Drivers]"
                      + " where [PersonID] = @PersonID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            DTDriver FindedDriver = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedDriver = new DTDriver
                        ((int)Reader["DriverID"],
                        PersonID,
                        (int)Reader["CreatedByUserID"],
                        (DateTime)Reader["CreatedDate"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedDriver;

        }
        
        public static int? GetDriverID(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                      + "[DriverID]"
                      + " FROM[dbo].[Drivers]"
                      + " where [PersonID] = @PersonID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            int? FindedID = null;

            try
            {
                Connection.Open();
                object IDObject = Command.ExecuteScalar();

                if(IDObject != null)
                {
                    FindedID = (int)IDObject;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedID;

        }

        public static bool IsExist(int DriverID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Drivers where DriverID =  @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
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

        public static bool IsExistByPersonID(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Drivers where PersonID =  @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
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

        public static string GetDriverImagePath(int DriverID)
        {
            int? DriverPersonID = GetDriverPersonID(DriverID);

            if (DriverPersonID == null)
                return null;
            else
                return AccessPersonData.GetPersonImagePath((int)DriverPersonID);
        }

        public static bool DoesDriverHaveLicenseOfClass(int DriverID, LicenseClass LicenseClass)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Drivers where DriverID =  @DriverID and LicenseClass = @LicenseClass ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass.ToDBValue());
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

        public static int? GetDriverPersonID(int DriverID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT top 1"
                      + " [PersonID]"
                      + " FROM[dbo].[Drivers]"
                      + " where [DriverID] = @DriverID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            int? DriverPersonID = null;

            try
            {
                Connection.Open();
                object ID = Command.ExecuteScalar();

                if (ID != null)
                {
                    DriverPersonID = (int)ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            if (DriverPersonID == null)
                return null;
            else
                return DriverPersonID;
        }

        public static DateTime? GetDriverBirthDate(int DriverID)
        {
            int? DriverPersonID = GetDriverPersonID(DriverID);

            if (DriverPersonID == null)
                return null;
            else
                return AccessPersonData.GetPersonBirthDate((int)DriverPersonID);
        }

        public static DataTable ListAllDrivers()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT[DriverID] "
                      + ",[PersonID]"
                      + ",[CreatedByUserID]"
                      + ",[CreatedDate]"
                      + " FROM[dbo].[Drivers]";

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

        private static int? _AddNewDriver(ref DTDriver DriverToAdd)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Drivers]" +
           " VALUES" +
           "( @PersonID" +
           ", @CreatedByUserID" +
           ", @CreatedDate );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", DriverToAdd.PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", DriverToAdd.CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", DriverToAdd.CreatedDate);

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

            DriverToAdd._DriverID = AddedID ?? -1;
            return AddedID;

        }

        public static DTDriver AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreateDate)
        {
            DTDriver NewDriver = new DTDriver(-1, PersonID, CreatedByUserID, CreateDate);

            int? NewID = _AddNewDriver(ref NewDriver);

            if (NewID != null)
                return NewDriver; // the ( _AddNewDriver ) method will insert the new ID to the Driver Object
            else
                return null;

        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime DriverDate)
        {

            if (!IsExist(DriverID))
                return false;

            return UpdateDriver(new DTDriver(DriverID, PersonID, CreatedByUserID, DriverDate));
        }

        public static bool UpdateDriver(DTDriver DriverToUpdate)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[Drivers] " +
              "SET" +
              " [PersonID]  = @PersonID" +
              ",[CreatedByUserID]  = @CreatedByUserID" +
              ",[CreatedDate]  = @CreatedDate" +
                   " WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", DriverToUpdate.PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", DriverToUpdate.CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", DriverToUpdate.CreatedDate);

            Command.Parameters.AddWithValue("@DriverID", DriverToUpdate.DriverID);

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

        public static bool DeleteDriver(int DriverIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "delete from [dbo].[Drivers]" +
                   "WHERE DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverIDToDelete);

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
