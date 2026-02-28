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
    public class DTEyeTest
    {
        internal protected DTEyeTest(int EyeTestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID,
            int TestApplicationID,bool? TestResult,string Notes,int? ResultAddedByUserID)
        {
            this._EyeTestID = EyeTestID;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            this.TestApplicationID = TestApplicationID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.ResultAddedByUserID = ResultAddedByUserID;
        }

        internal int _EyeTestID;
        public int EyeTestID { get { return _EyeTestID; } }
        public int PersonID;
        public DateTime AppointmentDate;
        public Decimal PaidFees;
        public int CreatedByUserID;
        public int TestApplicationID;
        public bool? TestResult;
        public string Notes;
        public int? ResultAddedByUserID;
    }
    
    public class AccessEyeTestData
    {
        // returns the last EyeTest person tacked 
        public static DTEyeTest FindLastTestForPerson(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT top 1 " +
                " [EyeTestID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[CreatedByUserID]" +
                ",[TestApplicationID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                  " FROM [dbo].[Eye Tests]" +
                  " order by [AppointmentDate] desc" +
                  " where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            DTEyeTest FindedEyeTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedEyeTest = new DTEyeTest
                        ((int)Reader["EyeTestID"], PersonID, (DateTime)Reader["AppointmentDate"],
                         (decimal)Reader["PaidFees"], (int)Reader["CreatedByUserID"], (int)Reader["TestApplicationID"],
                         Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"], (string)Reader["Notes"],
                         Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedEyeTest;

        }

        public static DTEyeTest Find(int EyeTestID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                " [PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[CreatedByUserID]" +
                ",[TestApplicationID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                  " FROM [dbo].[Eye Tests]" +
                      " where EyeTestID = @EyeTestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@EyeTestID", EyeTestID);
            DTEyeTest FindedEyeTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {


                    FindedEyeTest = new DTEyeTest
                        (EyeTestID, (int)Reader["PersonID"], (DateTime)Reader["AppointmentDate"],
                         (decimal)Reader["PaidFees"], (int)Reader["CreatedByUserID"], (int)Reader["TestApplicationID"],
                        Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"], (string)Reader["Notes"],
                         Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedEyeTest;

        }

        public static bool IsExist(int EyeTestID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [Eye Tests] where  EyeTestID =  @EyeTestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@EyeTestID", EyeTestID);
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

        public static bool IsExistByApplicationID(int EyeTestID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [Eye Tests] where  EyeTestID =  @EyeTestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@EyeTestID", EyeTestID);
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


        public static DataTable ListAllEyeTests()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT" +
                " [EyeTestID]" +
                ",[PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[CreatedByUserID]" +
                ",[TestApplicationID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                " FROM [dbo].[Eye Tests]";

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

        internal static int? _AddNewEyeTest(ref DTEyeTest EyeTestToAdd)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
            "INSERT INTO [dbo].[Eye Tests]" +
                " VALUES" +
                "( @PersonID" +
                ", @AppointmentDate" +
                ", @PaidFees" +
                ", @CreatedByUserID" +
                ", @TestApplicationID" +
                ", @TestResult" +
                ", @Notes" +
                ", @ResultAddedByUserID);" +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", EyeTestToAdd.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", EyeTestToAdd.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", EyeTestToAdd.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", EyeTestToAdd.CreatedByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", EyeTestToAdd.TestApplicationID);
            Command.Parameters.AddWithValue("@Notes", EyeTestToAdd.Notes);

            if (EyeTestToAdd.TestResult != null)
                Command.Parameters.AddWithValue("@TestResult", EyeTestToAdd.TestResult);
            else
                Command.Parameters.AddWithValue("@TestResult", DBNull.Value);


            if (EyeTestToAdd.ResultAddedByUserID != null)
                Command.Parameters.AddWithValue("@ResultAddedByUserID", EyeTestToAdd.ResultAddedByUserID);
            else
                Command.Parameters.AddWithValue("@ResultAddedByUserID", DBNull.Value);

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

            EyeTestToAdd._EyeTestID = AddedID ?? -1;
            return AddedID;

        }
        
        public static DTEyeTest AddNewEyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            ,int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            DTEyeTest EyeTest = new DTEyeTest(-1, PersonID, AppointmentDate, PaidFees, CreatedByUserID, TestApplicationID,
                TestResult, Notes, ResultAddedByUserID);

            int? NewID = _AddNewEyeTest(ref EyeTest);

            if (NewID != null)
                return EyeTest;    // the ( _AddNewEyeTest ) method will insert the new ID to the EyeTest Object
            else
                return null;
        }

        public static bool UpdateEyeTest(int EyeTestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int CreatedByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            if(!IsExist(EyeTestID))
              return false;

            return UpdateEyeTest(new DTEyeTest(EyeTestID, PersonID, AppointmentDate, PaidFees, CreatedByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID));
        }

        public static bool UpdateEyeTest(DTEyeTest EyeTestToUpdate)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[Eye Tests]" +
              "SET" +
              " [PersonID]  = @PersonID" +
              ",[AppointmentDate] = @AppointmentDate" +
              ",[PaidFees] = @PaidFees" +
              ",[CreatedByUserID] = @CreatedByUserID" +
              ",[TestApplicationID] = @TestApplicationID" +
              ",[TestResult] = @TestResult" +
              ",[Notes] = @Notes" +
              ",[ResultAddedByUserID] = @ResultAddedByUserID" +
                   " WHERE EyeTestID = @EyeTestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", EyeTestToUpdate.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", EyeTestToUpdate.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", EyeTestToUpdate.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", EyeTestToUpdate.CreatedByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", EyeTestToUpdate.TestApplicationID);
            Command.Parameters.AddWithValue("@Notes", EyeTestToUpdate.Notes);

            if (EyeTestToUpdate.TestResult != null)
                Command.Parameters.AddWithValue("@TestResult", EyeTestToUpdate.TestResult);
            else
                Command.Parameters.AddWithValue("@TestResult", DBNull.Value);

            if (EyeTestToUpdate.ResultAddedByUserID != null)
                Command.Parameters.AddWithValue("@ResultAddedByUserID", EyeTestToUpdate.ResultAddedByUserID);
            else
                Command.Parameters.AddWithValue("@ResultAddedByUserID", DBNull.Value);



            Command.Parameters.AddWithValue("@EyeTestID", EyeTestToUpdate.EyeTestID);

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

        public static bool DeleteEyeTest(int EyeTestIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "delete from [dbo].[Eye Tests]" +
                   "WHERE EyeTestID = @EyeTestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@EyeTestID", EyeTestIDToDelete);

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
