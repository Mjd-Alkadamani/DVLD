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
    public class DTUser
    {
        internal protected DTUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this._UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
        }

        internal int _UserID;
        public int UserID { get{ return _UserID; } }
        public int PersonID;
        public string UserName;
        public string Password;
        public bool IsActive;

    }

    public class AccessUserData
    {
        public static DTUser Find(string UserName)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                  " [UserID]" +
                  ",[PersonID]" +
                  ",[Password]" +
                  ",[IsActive]" +
                  " FROM [dbo].[Users]" +
                  " where UserName = @UserName";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserName", UserName);
            DTUser FindedUser = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedUser = new DTUser
                        ((int)Reader["UserID"], (int)Reader["PersonID"], UserName, (string)Reader["Password"], (bool)Reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedUser;

        }

        public static bool IsExistAndActiveByUserNameAndPassword(string UserName, string Password)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT IsFound = 1 " +
                  " FROM [dbo].[Users]" +
                  " where UserName = @UserName And PassWord = @PassWord and IsActive = @IsActive";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@PassWord", Password);
            Command.Parameters.AddWithValue("@IsActive", true);

            bool FindedUser = false;

            try
            {
                Connection.Open();
                Object IsFound = Command.ExecuteScalar();

                if (IsFound != null)
                {
                    FindedUser = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedUser;

        }

        public static bool IsExistAndActiveByUserIDAndPassword(string UserID, string Password)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT IsFound = 1 " +
                  " FROM [dbo].[Users]" +
                  " where UserID = @UserID And PassWord = @PassWord and IsActive = @IsActive";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@PassWord", Password);
            Command.Parameters.AddWithValue("@IsActive", true);

            bool FindedUser = false;

            try
            {
                Connection.Open();
                Object IsFound = Command.ExecuteScalar();

                if (IsFound != null)
                {
                    FindedUser = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedUser;

        }

        public static DTUser Find(int UserID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                  " [PersonID]" +
                  ",[UserName]" +
                  ",[Password]" +
                  ",[IsActive]" +
                  " FROM [dbo].[Users]" +
                  " where UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);
            DTUser FindedUser = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedUser = new DTUser
                        (UserID,
                        (int)Reader["PersonID"],
                        (string)Reader["UserName"],
                        (string)Reader["Password"],
                        (bool)Reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedUser;

        }

        public static bool IsExist(string UserName)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "select top 1 isExist = 1 from Users where  UserName =  @UserName";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserName", UserName);
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

        public static bool IsExist(int UserID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Users where  UserID =  @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@UserID", UserID);
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

            string Query = @"select top 1 isExist = 1 from Users where  PersonID =  @PersonID";

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
        
        public static DataTable ListAllUsers()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                  " [UserID]" +
                  ",[PersonID]" +
                  ",[UserName]" +
                  ",[Password]" +
                  ",[IsActive]" +
                  " FROM [dbo].[Users]";

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

        internal static int? _AddNewUser(ref DTUser UserToAdd)
        {
            if (string.IsNullOrEmpty(UserToAdd.UserName) || string.IsNullOrEmpty(UserToAdd.Password))
            { return null; }

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Users]" +
           " VALUES" +
           "( @PersonID" +
           ", @UserName" +
           ", @Password" +
           ", @IsActive);" +
           "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", UserToAdd.PersonID);
            Command.Parameters.AddWithValue("@UserName", UserToAdd.UserName);
            Command.Parameters.AddWithValue("@Password", UserToAdd.Password);
            Command.Parameters.AddWithValue("@IsActive", UserToAdd.IsActive);

            int? AddedID = -1;

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

            UserToAdd._UserID = AddedID ?? -1;
            return AddedID;

        }

        public static DTUser AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            DTUser NewUser = new DTUser(-1, PersonID, UserName, Password, IsActive);

            int? ID = _AddNewUser(ref NewUser);

            if (ID != null)
            {
                return NewUser;  // the ( _AddNewUser ) method will insert the new ID to the NewUser Object
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            if (!IsExist(UserID))
                return false;

            return UpdateUser(new DTUser(UserID, PersonID, UserName, Password, IsActive));
        }

        public static bool UpdateUser(DTUser UserToUpdate)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[Users]" +
              "SET " +
              "[PersonID]  = @PersonID" +
              ",[UserName] = @UserName" +
              ",[Password] = @Password" +
              ",[IsActive] = @IsActive" +
                   " WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", UserToUpdate.PersonID);
            Command.Parameters.AddWithValue("@UserName", UserToUpdate.UserName);
            Command.Parameters.AddWithValue("@Password", UserToUpdate.Password);
            Command.Parameters.AddWithValue("@IsActive", UserToUpdate.IsActive);

            Command.Parameters.AddWithValue("@UserID", UserToUpdate.UserID);

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

        public static bool DeleteUser(int UserIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "delete from [dbo].[Users]" +
                   "WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserIDToDelete);

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

        public static bool DeleteUser(string UserNameToDelete)
        {
        
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
              "delete from [dbo].[Users]" +
                   "WHERE UserName = @UserName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserNameToDelete);

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
