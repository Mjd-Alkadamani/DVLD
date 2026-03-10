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

    public class DTTheoreticalTest
    {
        internal protected DTTheoreticalTest(int TestID, int PersonID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID, LicenseClass TestClass)
        {
            this._TestID = TestID;
            this.PersonID = PersonID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.AppointmentMadeByUserID = AppointmentMadeByUserID;
            this.TestApplicationID = TestApplicationID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.ResultAddedByUserID = ResultAddedByUserID;
            this.TestClass = TestClass;
        }

        // Any of them Cuold be Null
        public void SetReleaseInfo(bool TestResult, string Notes, int ResultAddedByUserID)
        {
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.ResultAddedByUserID = ResultAddedByUserID;
        }


        internal int _TestID;
        public int TestID { get { return _TestID; } }
        public int PersonID;
        public DateTime AppointmentDate;
        public decimal PaidFees;
        public int AppointmentMadeByUserID;
        public int TestApplicationID;

        public bool? TestResult = null;
        public string Notes = null;
        public int? ResultAddedByUserID = null;

        public LicenseClass TestClass;

    }

    public class AccessTheoreticalTestData
    {
        public static DTTheoreticalTest FindLastTest(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1"
                          + " [TestID]"
                          + ",[AppointmentDate]"
                          + ",[PaidFees]"
                          + ",[AppointmentMadeByUserID]"
                          + ",[TestApplicationID]"
                          + ",[TestResult]"
                          + ",[Notes]"
                          + ",[ResultAddedByUserID]"
                          + ",[TestClassID]"
                       + "FROM[dbo].[TheoreticalTests]"
                       + " Order By [AppointmentDate] Asc"
                       + " Where [PersonID] = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            DTTheoreticalTest FindedTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedTest = new DTTheoreticalTest
                     ((int)Reader["TestID"],
                      PersonID,
                      (DateTime)Reader["AppointmentDate"],
                      (decimal)Reader["PaidFees"],
                      (int)Reader["AppointmentMadeByUserID"],
                      (int)Reader["TestApplicationID"],
                      Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"],
                      Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"],
                      Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"],
                      ((int)Reader["TestClassID"]).ToLicenseClass()
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

            return FindedTest;

        }

        public static DTTheoreticalTest Find(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [PersonID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestApplicationID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                  + ",[TestClassID]"
                  + " FROM[dbo].[TheoreticalTests]"
                      + " where TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
            DTTheoreticalTest FindedTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedTest = new DTTheoreticalTest
                     (TestID,
                      (int)Reader["PersonID"],
                      (DateTime)Reader["AppointmentDate"],
                      (decimal)Reader["PaidFees"],
                      (int)Reader["AppointmentMadeByUserID"],
                      (int)Reader["TestApplicationID"],
                      Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"],
                      Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"],
                      Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"],
                      ((int)Reader["TestClassID"]).ToLicenseClass()
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

            return FindedTest;

        }

        public static DTTheoreticalTest FindByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [TestID]"
                  + ",[PersonID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                  + ",[TestClassID]"
                  + " FROM[dbo].[TheoreticalTests]"
                      + " where TestApplicationID = @TestApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestApplicationID", ApplicationID);
            DTTheoreticalTest FindedTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedTest = new DTTheoreticalTest
                     ((int)Reader["TestID"],
                      (int)Reader["PersonID"],
                      (DateTime)Reader["AppointmentDate"],
                      (decimal)Reader["PaidFees"],
                      (int)Reader["AppointmentMadeByUserID"],
                      ApplicationID,
                      Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"],
                      Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"],
                      Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"],
                      ((int)Reader["TestClassID"]).ToLicenseClass()

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

            return FindedTest;

        }

        public static bool IsExist(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [TheoreticalTests] where  TestID =  @TestID";

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

            string Query = @"select top 1 IsPassed = 1 from [TheoreticalTests] where  TestID =  @TestID and TestResult = 1";

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

        public static bool IsExistByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from [TheoreticalTests] where  TestApplicationID =  @ApplicationID";

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

        public static DateTime? GetAppointmentDate(int TestID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select [AppointmentDate] from [TheoreticalTests] where  TestID =  @TestID";

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


        public static DataTable ListAllTests()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [TestID]"
                  + ",[PersonID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestApplicationID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                  + ",[TestClassID]"
                    + " FROM[dbo].[TheoreticalTests]";

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

        public static DataTable ListAllPersonTheoreticalTests(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [TestID]"
                  + ",[PersonID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestApplicationID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                  + ",[TestClassID]"
                    + " FROM[dbo].[TheoreticalTests] Where [PersonID] = @PersonID ";

            DataTable Table = new DataTable();
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        private static int? _AddNewTest(ref DTTheoreticalTest TestToAdd)
        {
            if (TestToAdd == null)
                return null;
            if (TestToAdd.TestApplicationID == -1)
                return null;

            if (AccessApplicationData.CouldAttachTestOfTypeToApplication(TestToAdd.TestID, TestType.TheoreticalTest))
                return null;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[TheoreticalTests]" +
           " VALUES" +
           "( @PersonID" +
           ", @AppointmentDate" +
           ", @PaidFees" +
           ", @AppointmentMadeByUserID" +
           ", @TestApplicationID" +
           ", @TestResult" +
           ", @Notes" +
           ", @ResultAddedByUserID" +
           ", @TestClass );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", TestToAdd.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", TestToAdd.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", TestToAdd.PaidFees);
            Command.Parameters.AddWithValue("@AppointmentMadeByUserID", TestToAdd.AppointmentMadeByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", TestToAdd.TestApplicationID);

            if (TestToAdd.TestResult == null)
                Command.Parameters.AddWithValue("@TestResult", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@TestResult", TestToAdd.TestResult);


            if (string.IsNullOrEmpty(TestToAdd.Notes)) 
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", TestToAdd.Notes);


            if (TestToAdd.ResultAddedByUserID == null)
                Command.Parameters.AddWithValue("@ResultAddedByUserID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ResultAddedByUserID", TestToAdd.ResultAddedByUserID);

                Command.Parameters.AddWithValue("@TestClass", TestToAdd.TestClass);

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

            TestToAdd._TestID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }

        //        ( ResultAddedByUserID & TestResult )      //
        //       are ether -Both Null- or -Both not Null-   //

        public static DTTheoreticalTest AddNewTest(int PersonID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID,LicenseClass TestClass)
        {
            DTTheoreticalTest NewLicense = new DTTheoreticalTest(-1, PersonID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID, TestClass);

            int? ID = _AddNewTest(ref NewLicense);

            if (ID != null)
            {
                return NewLicense; // the ( _AddNewTest ) method will insert the new ID to the NewLicense Objedct
            }
            else
            {
                return null;
            }
        }

        public static DTTheoreticalTest AddNewTest(int PersonID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID, LicenseClass TestClass)
        {
            DTTheoreticalTest NewLicense = new DTTheoreticalTest(-1, PersonID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID, TestClass);

            int? ID = _AddNewTest(ref NewLicense);

            if (ID != null)
            {
                return NewLicense;  // the ( _AddNewTest ) method will insert the new ID to the NewLicense Object
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateTest(int TestID, int PersonID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID,
            LicenseClass TestClass, int CurrentUserID)
        {
            if (!IsExist(TestID))
                return false;

            return UpdateTest(new DTTheoreticalTest(TestID, PersonID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID, TestClass), CurrentUserID);
        }

        public static bool UpdateTest(DTTheoreticalTest TestToUpdate , int UserID)
        {
            if (TestToUpdate == null)
                return false;

            if (TestToUpdate.TestID < 0)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTTheoreticalTest OldTest = Find(TestToUpdate.TestID);

            if (OldTest == null)
                return false;

            if (OldTest.PersonID != TestToUpdate.PersonID ||
                 OldTest.TestClass != TestToUpdate.TestClass ||
                  OldTest.TestApplicationID != TestToUpdate.TestApplicationID)
                return false;

            if (OldTest.AppointmentDate != TestToUpdate.AppointmentDate)
            {
                if (OldTest.TestResult != null)
                    return false;

                if (TestToUpdate.AppointmentDate < DateTime.Now)
                    return false;

                TestToUpdate.AppointmentMadeByUserID = UserID;

            }
            else
                TestToUpdate.AppointmentMadeByUserID = OldTest.AppointmentMadeByUserID;

            if (OldTest.TestResult != TestToUpdate.TestResult)
                TestToUpdate.ResultAddedByUserID = UserID;
            else
                TestToUpdate.ResultAddedByUserID = OldTest.ResultAddedByUserID;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
              "UPDATE[dbo].[TheoreticalTests]" +
              "SET " +
              " [PersonID]               = @PersonID" +
              ",[AppointmentDate]        = @AppointmentDate" +
              ",[PaidFees]               = @PaidFees" +
              ",[AppointmentMadeByUserID] = @AppointmentMadeByUserID" +
              ",[TestApplicationID]      = @TestApplicationID" +
              ",[TestResult]             = @TestResult" +
              ",[Notes]                  = @Notes" +
              ",[ResultAddedByUserID]    = @ResultAddedByUserID" +
              ",[TestClassID]    = @TestClass" +
                   " WHERE TestID        = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", TestToUpdate.PersonID);
            Command.Parameters.AddWithValue("@AppointmentDate", TestToUpdate.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", TestToUpdate.PaidFees);
            Command.Parameters.AddWithValue("@AppointmentMadeByUserID", TestToUpdate.AppointmentMadeByUserID);
            Command.Parameters.AddWithValue("@TestApplicationID", TestToUpdate.TestApplicationID);

            if (TestToUpdate.TestResult == null)
                Command.Parameters.AddWithValue("@TestResult", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@TestResult", TestToUpdate.TestResult);


            if (string.IsNullOrEmpty(TestToUpdate.Notes))
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Notes", TestToUpdate.Notes);


            if (TestToUpdate.ResultAddedByUserID == null)
                Command.Parameters.AddWithValue("@ResultAddedByUserID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ResultAddedByUserID", TestToUpdate.ResultAddedByUserID);

                Command.Parameters.AddWithValue("@TestClass", TestToUpdate.TestClass);

            Command.Parameters.AddWithValue("@TestID", TestToUpdate.TestID);

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

        public static bool UpdatePaiedFee(int TestIDToUpdate, decimal NewPaidFees)
        {

            if (TestIDToUpdate < 0)
                return false;

            if (NewPaidFees < 0)
                return false;

            DTTheoreticalTest OldTest = Find(TestIDToUpdate);

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

            Command.Parameters.AddWithValue("@TestID", TestIDToUpdate);

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

        public static bool UpdateAppointmentDate(int TestIDToUpdate, DateTime NewAppointmentDate, int UserID)
        {
            if (TestIDToUpdate < 0)
                return false;

            if (NewAppointmentDate < DateTime.Now)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTTheoreticalTest OldTest = Find(TestIDToUpdate);

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
              ",[AppointmentMadeByUserID] = @AppointmentMadeByUserID" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@AppointmentDate", NewAppointmentDate);
            Command.Parameters.AddWithValue("@AppointmentMadeByUserID", UserID);

            Command.Parameters.AddWithValue("@TestID", TestIDToUpdate);

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

        public static bool UpdateTestResult(int TestIDToUpdate, bool TestResult, int UserID)
        {
            if (TestIDToUpdate < 0)
                return false;

            if (!AccessUserData.IsExist(UserID))
                return false;

            DTTheoreticalTest OldTest = Find(TestIDToUpdate);

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

            Command.Parameters.AddWithValue("@TestID", TestIDToUpdate);

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

        public static bool DeleteTest(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "delete from [dbo].[TheoreticalTests]" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestID", IDToDelete);

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

