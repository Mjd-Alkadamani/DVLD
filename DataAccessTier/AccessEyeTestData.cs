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
    public class DTEyeTest
    {
        internal protected DTEyeTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int AppointmentMadeByUserID,
            int TestApplicationID,bool? TestResult,string Notes,int? ResultAddedByUserID)
        {
            this._TestID = TestID;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentMadeByUserID = AppointmentMadeByUserID;
            this.PaidFees = PaidFees;
            this.TestApplicationID = TestApplicationID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.ResultAddedByUserID = ResultAddedByUserID;
        }

        internal int _TestID;
        public int TestID { get { return _TestID; } }
        public int PersonID;
        public DateTime AppointmentDate;
        public Decimal PaidFees;
        public int AppointmentMadeByUserID;
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
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1 " +
                " [TestID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[AppointmentMadeByUserID]" +
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
                        ((int)Reader["TestID"], PersonID, (DateTime)Reader["AppointmentDate"],
                         (decimal)Reader["PaidFees"], (int)Reader["AppointmentMadeByUserID"], (int)Reader["TestApplicationID"],
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

        public static DTEyeTest Find(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                " [PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[AppointmentMadeByUserID]" +
                ",[TestApplicationID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                  " FROM [dbo].[Eye Tests]" +
                      " where TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);
            DTEyeTest FindedEyeTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {


                    FindedEyeTest = new DTEyeTest
                        (TestID, (int)Reader["PersonID"], (DateTime)Reader["AppointmentDate"],
                         (decimal)Reader["PaidFees"], (int)Reader["AppointmentMadeByUserID"], (int)Reader["TestApplicationID"],
                        Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"], Reader["Notes"] == DBNull.Value ? null: (string)Reader["Notes"],
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
        
        public static DTEyeTest FindByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                " [TestID]" +
                ",[PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[AppointmentMadeByUserID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                  " FROM [dbo].[Eye Tests]" +
                      " where TestApplicationID = @TestApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestApplicationID", ApplicationID);
            DTEyeTest FindedEyeTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {


                    FindedEyeTest = new DTEyeTest
                        ((int)Reader["TestID"], (int)Reader["PersonID"], (DateTime)Reader["AppointmentDate"],
                         (decimal)Reader["PaidFees"], (int)Reader["AppointmentMadeByUserID"], ApplicationID,
                        Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"], Reader["Notes"] == DBNull.Value ? null: (string)Reader["Notes"],
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

        public static bool IsExist(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [Eye Tests] where  TestID =  @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
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

        public static bool IsExistByApplicationID(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [Eye Tests] where  TestID =  @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
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

        public static bool IsPassed(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 IsPassed = 1 from [Eye Tests] where  TestID =  @TestID and TestResult = 1 ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
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
        
        public static DateTime? GetAppointmentDate(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select [AppointmentDate] from [Eye Tests] where  TestID =  @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);

            DateTime? AppointmentDate = null;

            try
            {
                Connection.Open();

                object Object = Command.ExecuteScalar();

                if (Object != null)
                    AppointmentDate = Convert.ToDateTime(Object);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return AppointmentDate;

        }

        public static DataTable ListAllEyeTests()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                " [TestID]" +
                ",[PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[AppointmentMadeByUserID]" +
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

        public static DataTable ListAllPersonEyeTests(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                " [TestID]" +
                ",[PersonID]" +
                ",[AppointmentDate]" +
                ",[PaidFees]" +
                ",[AppointmentMadeByUserID]" +
                ",[TestApplicationID]" +
                ",[TestResult]" +
                ",[Notes]" +
                ",[ResultAddedByUserID]" +
                " FROM [dbo].[Eye Tests]" +
                " Where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            DataTable Table = new DataTable();

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

            if (EyeTestToAdd == null)
                return null;
            if (EyeTestToAdd.TestApplicationID == -1)
                return null;

            if (AccessApplicationData.CouldAttachTestOfTypeToApplication(EyeTestToAdd.TestApplicationID, TestType.EyeTest))
                return null;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
            "INSERT INTO [dbo].[Eye Tests]" +
                " VALUES" +
                "( @PersonID" +
                ", @AppointmentDate" +
                ", @PaidFees" +
                ", @AppointmentMadeByUserID" +
                ", @TestApplicationID" +
                ", @TestResult" +
                ", @Notes" +
                ", @ResultAddedByUserID);" +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", EyeTestToAdd.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", EyeTestToAdd.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", EyeTestToAdd.PaidFees);
            Command.Parameters.AddWithValue("@AppointmentMadeByUserID", EyeTestToAdd.AppointmentMadeByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", EyeTestToAdd.TestApplicationID);

            if (EyeTestToAdd.Notes != null)
                Command.Parameters.AddWithValue("@Notes", EyeTestToAdd.Notes);
            else
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            
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

            EyeTestToAdd._TestID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }
        
        public static DTEyeTest AddNewEyeTest(int PersonID, DateTime AppointmentDate, Decimal PaidFees, int AppointmentMadeByUserID
            ,int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            DTEyeTest EyeTest = new DTEyeTest(-1, PersonID, AppointmentDate, PaidFees, AppointmentMadeByUserID, TestApplicationID,
                TestResult, Notes, ResultAddedByUserID);

            int? NewID = _AddNewEyeTest(ref EyeTest);

            if (NewID != null)
                return EyeTest;    // the ( _AddNewEyeTest ) method will insert the new ID to the EyeTest Object
            else
                return null;
        }

        public static bool UpdateEyeTest(int TestID, int PersonID, DateTime AppointmentDate, Decimal PaidFees, int AppointmentMadeByUserID
            , int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID, int CurrentUserID)
        {
            if(!IsExist(TestID))
              return false;

            return UpdateEyeTest(new DTEyeTest(TestID, PersonID, AppointmentDate, PaidFees, AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID), CurrentUserID);
        }

        public static bool UpdateEyeTest(DTEyeTest EyeTestToUpdate ,int UserID)
        {
            if (EyeTestToUpdate == null)
                return false;

            if (EyeTestToUpdate.TestID < 0)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTEyeTest OldTest = Find(EyeTestToUpdate.TestID);

            if (OldTest == null)
                return false;

            if (OldTest.PersonID != EyeTestToUpdate.PersonID ||
                OldTest.TestApplicationID != EyeTestToUpdate.TestApplicationID)
                return false;

            if (OldTest.AppointmentDate != EyeTestToUpdate.AppointmentDate)
            {
                if (OldTest.TestResult != null)
                    return false;

                if (EyeTestToUpdate.AppointmentDate < DateTime.Now)
                    return false;

                EyeTestToUpdate.AppointmentMadeByUserID = UserID;

            }
            else
                EyeTestToUpdate.AppointmentMadeByUserID = OldTest.AppointmentMadeByUserID;


            if (OldTest.TestResult != EyeTestToUpdate.TestResult)
                EyeTestToUpdate.ResultAddedByUserID = UserID;
            else
                EyeTestToUpdate.ResultAddedByUserID = OldTest.ResultAddedByUserID;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Eye Tests]" +
              "SET" +
              " [PersonID] = @PersonID" +
              ",[AppointmentDate] = @AppointmentDate" +
              ",[PaidFees] = @PaidFees" +
              ",[AppointmentMadeByUserID] = @AppointmentMadeByUserID" +
              ",[TestApplicationID] = @TestApplicationID" +
              ",[TestResult] = @TestResult" +
              ",[Notes] = @Notes" +
              ",[ResultAddedByUserID] = @ResultAddedByUserID" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", EyeTestToUpdate.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", EyeTestToUpdate.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", EyeTestToUpdate.PaidFees);
            Command.Parameters.AddWithValue("@AppointmentMadeByUserID", EyeTestToUpdate.AppointmentMadeByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", EyeTestToUpdate.TestApplicationID);

            if (EyeTestToUpdate.TestResult != null)
                Command.Parameters.AddWithValue("@TestResult", EyeTestToUpdate.TestResult);
            else
                Command.Parameters.AddWithValue("@TestResult", DBNull.Value);
            
            if (EyeTestToUpdate.Notes != null)
            Command.Parameters.AddWithValue("@Notes", EyeTestToUpdate.Notes);
            else
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);

            if (EyeTestToUpdate.ResultAddedByUserID != null)
                Command.Parameters.AddWithValue("@ResultAddedByUserID", EyeTestToUpdate.ResultAddedByUserID);
            else
                Command.Parameters.AddWithValue("@ResultAddedByUserID", DBNull.Value);



            Command.Parameters.AddWithValue("@TestID", EyeTestToUpdate.TestID);

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

        public static bool UpdatePaiedFee(int EyeTestIDToUpdate, decimal NewPaidFees)
        {

            if (EyeTestIDToUpdate < 0)
                return false;

            if (NewPaidFees < 0)
                return false;

            DTEyeTest OldTest = Find(EyeTestIDToUpdate);

            if (OldTest == null)
                return false;

            if (OldTest.TestResult != null)
                return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Eye Tests]" +
              "SET" +
              " [PaidFees] = @PaidFees" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PaidFees", NewPaidFees);

            Command.Parameters.AddWithValue("@TestID", EyeTestIDToUpdate);

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
        
        public static bool UpdateAppointmentDate(int EyeTestIDToUpdate, DateTime NewAppointmentDate, int UserID)
        {
            if (EyeTestIDToUpdate < 0)
                return false;

            if (NewAppointmentDate < DateTime.Now)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTEyeTest OldTest = Find(EyeTestIDToUpdate);

            if (OldTest == null)
                return false;

            if (OldTest.AppointmentDate == NewAppointmentDate)
                return false;

            if (OldTest.TestResult != null)
                return false;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Eye Tests]" +
              " SET" +
              " [AppointmentDate] = @AppointmentDate" +
              ",[ResultAddedByUserID] = @ResultAddedByUserID" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@AppointmentDate", NewAppointmentDate);
            Command.Parameters.AddWithValue("@ResultAddedByUserID", UserID);

            Command.Parameters.AddWithValue("@TestID", EyeTestIDToUpdate);

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

        public static bool UpdateTestResult(int EyeTestIDToUpdate, bool TestResult, int UserID)
        {
            if (EyeTestIDToUpdate < 0)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTEyeTest OldTest = Find(EyeTestIDToUpdate);

            if (OldTest == null)
                return false;

            if (OldTest.AppointmentDate > DateTime.Now)
                return false;

            if (OldTest.TestResult != null)
                return false;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[Eye Tests]" +
              " SET" +
              " [TestResult] = @TestResult" +
              ",[ResultAddedByUserID] = @ResultAddedByUserID" +

                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@ResultAddedByUserID", UserID);

            Command.Parameters.AddWithValue("@TestID", EyeTestIDToUpdate);

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

        public static bool DeleteEyeTest(int TestIDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "delete from [dbo].[Eye Tests]" +
                   "WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestIDToDelete);

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
