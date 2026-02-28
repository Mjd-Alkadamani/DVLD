using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generale;

namespace DataAccessTier
{
    public static class GendorExtintion
    {
        public static string ToDBValue(this Gendor gendor)
        {
            switch(gendor)
            {
                case Gendor.Femail:
                    return "F";
                default:
                    return "M";
            }
        }
        
        public static Gendor ToGendor(this char gendor)
        {
            switch(gendor)
            {
                case 'F': //Femail
                    return Gendor.Femail;
                default:            //Mail
                    return Gendor.Mail;
            }
        }

    }

    public class DTPerson
    {
        protected internal DTPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
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

    public class AccessPersonData
    {
        public static DTPerson Find(string NationalNo)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT [PersonID]" +
                  ",[FirstName]" +
                  ",[SecondName]" +
                  ",[ThirdName]" +
                  ",[LastName]" +
                  ",[DateOfBirth]" +
                  ",[Gendor]" +
                  ",[Address]" +
                  ",[Phone]" +
                  ",[Email]" +
                  ",[NationalityCountryID]" +
                  ",[ImagePath]" +
                  " FROM[dbo].[People]" +
                  " where NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            DTPerson FindedPerson = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedPerson = new DTPerson((int)Reader["PersonId"], NationalNo, (string)Reader["FirstName"],
                        (string)Reader["SecondName"], Reader["ThirdName"] == DBNull.Value ? "" : (string)Reader["ThirdName"],
                        (string)Reader["LastName"], (DateTime)Reader["DateOfBirth"], ((string)Reader["Gendor"])[0].ToGendor(),
                        (string)Reader["Address"], (string)Reader["Phone"], Reader["Email"] == DBNull.Value ? "" : (string)Reader["Email"],
                        (int)Reader["NationalityCountryID"], (string)Reader["ImagePath"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedPerson;

        }

        public static DTPerson Find(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                  " [NationalNo]" +
                  ",[FirstName]" +
                  ",[SecondName]" +
                  ",[ThirdName]" +
                  ",[LastName]" +
                  ",[DateOfBirth]" +
                  ",[Gendor]" +
                  ",[Address]" +
                  ",[Phone]" +
                  ",[Email]" +
                  ",[NationalityCountryID]" +
                  ",[ImagePath]" +
                  " FROM[dbo].[People]" +
                  " where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            DTPerson FindedPerson = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {

                    FindedPerson = new DTPerson
                        ( PersonID,
                          (string)Reader["NationalNo"],
                          (string)Reader["FirstName"],
                          (string)Reader["SecondName"],
                          Reader["ThirdName"] == DBNull.Value ? "" : (string)Reader["ThirdName"],
                          (string)Reader["LastName"],
                          (DateTime)Reader["DateOfBirth"],
                          ((string)Reader["Gendor"])[0].ToGendor(),
                          (string)Reader["Address"],
                          (string)Reader["Phone"],
                          Reader["Email"] == DBNull.Value ? "" : (string)Reader["Email"],
                          (int)Reader["NationalityCountryID"],
                          (string)Reader["ImagePath"]
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

            return FindedPerson;

        }

        public static bool IsExist(string NationalNo)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "select top 1 isExist = 1 from People where  NationalNo =  @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
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

        public static bool IsExist(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from People where  PersonID =  @PersonID";

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

        public static DateTime? GetPersonBirthDate(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT top 1"
                      + " [DateOfBirth]"
                      + " FROM[dbo].[People]"
                      + " where [PersonID] = @PersonID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            DateTime? DateOfBirth = null;

            try
            {
                Connection.Open();
                object Date = Command.ExecuteScalar();

                if (Date != null)
                {
                    DateOfBirth = (DateTime)Date;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }


            return DateOfBirth;

        }

        public static string GetPersonImagePath(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT top 1"
                      + " [ImagePath]"
                      + " FROM[dbo].[People]"
                      + " where [PersonID] = @PersonID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            string ImagePath = null;

            try
            {
                Connection.Open();
                object Date = Command.ExecuteScalar();

                if (Date != null)
                {
                    ImagePath = (string)Date;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }


            return ImagePath;

        }

        public static DataTable ListAllPeople()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT [PersonID]" +
                  ",[NationalNo]" +
                  ",[FirstName]" +
                  ",[SecondName]" +
                  ",[ThirdName]" +
                  ",[LastName]" +
                  ",[DateOfBirth]" +
                  ",[Gendor]" +
                  ",[Address]" +
                  ",[Phone]" +
                  ",[Email]" +
                  ",[NationalityCountryID]" +
                  ",[ImagePath]" +
                  " FROM[dbo].[People]";

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

        internal static int? _AddNewPerson(ref DTPerson PersonToAdd)
        {
        if ( // chacking for Nan nullable String fileds //
             string.IsNullOrEmpty (PersonToAdd.NationalNo) ||
             string.IsNullOrEmpty (PersonToAdd.FirstName) ||
             string.IsNullOrEmpty (PersonToAdd.SecondName) ||
             string.IsNullOrEmpty (PersonToAdd.LastName) ||
             string.IsNullOrEmpty (PersonToAdd.Address) ||
             string.IsNullOrEmpty (PersonToAdd.Phone) ||
             string.IsNullOrEmpty (PersonToAdd.ImagePath)
            ) 
            { return null; }

        SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
                "INSERT INTO[dbo].[People]" +
           " VALUES" +
           "( @NationalNo" +
           ", @FirstName" +
           ", @SecondName" +
           ", @ThirdName" +
           ", @LastName" +
           ", @DateOfBirth" +
           ", @Gendor" +
           ", @Address" +
           ", @Phone" +
           ", @Email" +
           ", @NationalityCountryID" +
           ", @ImagePath);" +
           "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", PersonToAdd.NationalNo);
            Command.Parameters.AddWithValue("@FirstName", PersonToAdd.FirstName);
            Command.Parameters.AddWithValue("@SecondName", PersonToAdd.SecondName);

            if (string.IsNullOrEmpty(PersonToAdd.ThirdName))
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ThirdName", PersonToAdd.ThirdName);

            Command.Parameters.AddWithValue("@LastName", PersonToAdd.LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", PersonToAdd.DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", PersonToAdd.Gendor.ToDBValue());
            Command.Parameters.AddWithValue("@Address", PersonToAdd.Address);
            Command.Parameters.AddWithValue("@Phone", PersonToAdd.Phone);


            if (string.IsNullOrEmpty (PersonToAdd.Email))
                Command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Email", PersonToAdd.Email);

            Command.Parameters.AddWithValue("@NationalityCountryID", PersonToAdd.NationalityCountryID);
            Command.Parameters.AddWithValue("@ImagePath", PersonToAdd.ImagePath);

            int? AddedID = null;

            try
            {
                Connection.Open();

                object DoesSucceded = Command.ExecuteNonQuery();

                if (DoesSucceded != null)
                    AddedID = (int) DoesSucceded;

            }
            catch(Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            PersonToAdd._PersonID = AddedID ?? -1;
            return AddedID;

        }

        public static DTPerson AddNewPerson(string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, Gendor Gendor, string Adress,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            DTPerson NewPerson = new DTPerson(-1, NationalNo, FirstName
                , SecondName, ThirdName, LastName, DateOfBirth, Gendor, Adress
                , Phone, Email, NationalityCountryID, ImagePath);

            int? ID = _AddNewPerson(ref NewPerson);

            if (ID != null)
            {
                return NewPerson;  // the ( _AddNewPerson ) method will insert the new ID to the NewPerson Object
            }
            else
            {
                return null;
            }
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, Gendor Gendor, string Adress, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            if (!IsExist(PersonID))
                return false;

            return UpdatePerson(new DTPerson
                (PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Adress, Phone, Email,
                    NationalityCountryID, ImagePath));
        }

        public static bool UpdatePerson(DTPerson PersonToUpdate)
        {
         
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[People]" +
              "SET " +
              "[NationalNo]            = @NationalNo" +
              ",[FirstName]            = @FirstName" +
              ",[SecondName]           = @SecondName" +
              ",[ThirdName]            = @ThirdName" +
              ",[LastName]             = @LastName" +
              ",[DateOfBirth]          = @DateOfBirth" +
              ",[Gendor]               = @Gendor" +
              ",[Address]              = @Address" +
              ",[Phone]                = @Phone" +
              ",[Email]                = @Email" +
              ",[NationalityCountryID] = @NationalityCountryID" +
              ",[ImagePath]            = @ImagePath" +
                   " WHERE PersonID = @PersonId";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", PersonToUpdate.NationalNo);
            Command.Parameters.AddWithValue("@FirstName", PersonToUpdate.FirstName);
            Command.Parameters.AddWithValue("@SecondName", PersonToUpdate.SecondName);

            if (string.IsNullOrEmpty( PersonToUpdate.ThirdName))
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ThirdName", PersonToUpdate.ThirdName);

            Command.Parameters.AddWithValue("@LastName", PersonToUpdate.LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", PersonToUpdate.DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", PersonToUpdate.Gendor.ToDBValue());
            Command.Parameters.AddWithValue("@Address", PersonToUpdate.Address);
            Command.Parameters.AddWithValue("@Phone", PersonToUpdate.Phone);

            if (string.IsNullOrEmpty(PersonToUpdate.Email))
                Command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Email", PersonToUpdate.Email);

            Command.Parameters.AddWithValue("@NationalityCountryID", PersonToUpdate.NationalityCountryID);
            Command.Parameters.AddWithValue("@ImagePath", PersonToUpdate.ImagePath);
            Command.Parameters.AddWithValue("@PersonID", PersonToUpdate.PersonID);

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
       
        public static bool DeletePerson(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "delete from [dbo].[People]" +
                   "WHERE PersonID = @PersonId";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", IDToDelete);

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

        public static bool DeletePerson(string NationalNoToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
              "delete from [dbo].[People]" +
                   "WHERE NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNoToDelete);

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
