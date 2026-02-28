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
    public class DTCountry
    {
        internal protected DTCountry(int CountryID, string CountryName)
        {
            this._CountryID = CountryID;
            this.CountryName = CountryName;
        }

        internal int _CountryID;
        public int CountryID { get { return _CountryID; } }
        public string CountryName;


    }

    public class AccessCountriesData
    {
        /*public static Country Find(string CountryName)
        {
            // not complited no need //

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                  " [CountryID]" +
                  ",[CountryName]" +
                  " FROM [dbo].[Countries]" +
                  " where CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryName", CountryName);
            Country FindedCountry = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedCountry = new Country
                        ((int)Reader["CountryID"], (int)Reader["PersonID"], (string)Reader["CountryName"], (string)Reader["Password"], (bool)Reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedCountry;

        }
        */
        public static DTCountry Find(int CountryID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                  " [CountryName]" +
                  " FROM [dbo].[Countries]" +
                  " where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            DTCountry FindedCountry = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedCountry = new DTCountry
                        ( CountryID,
                         (string)Reader["CountryName"]
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

            return FindedCountry;

        }

        /*public static bool IsExist(string CountryName)
        {
            // not complited no need //

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "select top 1 isExist = 1 from Countries where  CountryName =  @CountryName";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryName", CountryName);
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
        */
        public static bool IsExist(int CountryID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Countries where  CountryID =  @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
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

        public static DataTable ListAllCountries()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                  " [CountryID]" +
                  ",[CountryName]" +
                  " FROM [dbo].[Countries]";

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

        private static int? _AddNewCountry(ref DTCountry CountryToAdd)
        {
            if (string.IsNullOrEmpty(CountryToAdd.CountryName))
                return null;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Countries]" +
           " VALUES" +
           "( @CountryName )" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryToAdd.CountryName);


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

            CountryToAdd._CountryID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }
        
        public static DTCountry AddNewCountry(string CountryName)
        {
            DTCountry NewCountry = new DTCountry(-1, CountryName);

            int? NewID = _AddNewCountry(ref NewCountry);

            if (NewID != null)
                return NewCountry;    // the ( _AddNewCountry ) method will insert the new ID to the Country Object
            else
                return null;



        }

        public static bool UpdateCountry (int CountryID, string CountryName)
        {
            if (IsExist(CountryID))
                return false;

            return UpdateCountry(new DTCountry(CountryID, CountryName));
        }

        public static bool UpdateCountry(DTCountry CountryToUpdate)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Countries]" +
              " SET" +
              " [CountryName] = @CountryName" +
                   " WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryToUpdate.CountryName);

            Command.Parameters.AddWithValue("@CountryID", CountryToUpdate.CountryID);

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

        public static bool DeleteCountry(int CountryIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "delete from [dbo].[Countries]" +
                   "WHERE CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryID", CountryIDToDelete);

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

        /*public static bool DeleteCountry(string CountryNameToDelete)
        {
            // not complited no need //

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
              "delete from [dbo].[Countries]" +
                   "WHERE CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryNameToDelete);

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
        */
    }


}
