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

    internal static class TestTypeExtention
    {
        internal static string ToDBValue(this TestType type)
        {
            switch(type)
            {
                case TestType.TheoreticalTest:
                    return "T";
                default: // ( DrivingTest )
                    return "D";
            }
        }

        internal static TestType ToTestType(this string DBText)
        {
            switch (DBText[0])
            {
                case 'T':
                    return TestType.TheoreticalTest;
                default : // 'D'
                    return TestType.DrivingTest;
            }
        }


    }

    public class DTTest
    {
        internal protected DTTest(int TestID, TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            this._TestID = TestID;
            this.TestType = TestType;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.AppointmentMadeByUserID = AppointmentMadeByUserID;
            this.TestApplicationID = TestApplicationID;
            this._TestResult = TestResult;
            this._Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }

        // Any of them Cuold be Null
        public void SetReleaseInfo(bool TestResult, string Notes, int ResultAddedByUserID)
        {
            this._TestResult = TestResult;
            this._Notes = Notes;
            this._ResultAddedByUserID = ResultAddedByUserID;
        }


        internal int _TestID;
        public int TestID { get { return _TestID; } }
        public TestType TestType;
        public int LocalDrivingLicenseApplicationID;
        public DateTime AppointmentDate;
        public decimal PaidFees;
        public int AppointmentMadeByUserID;
        public int TestApplicationID;

        private bool? _TestResult = null;
        private string _Notes = null;
        private int? _ResultAddedByUserID = null;

        public bool? TestResult { get { return _TestResult; } }
        public string Notes { get { return _Notes; } }
        public int? ResultAddedByUserID { get { return _ResultAddedByUserID; } }
    }

    public class AccessTestData
    {
        public static DTTest FindLastTest(int LocalDrivingLicenseApplicationID, TestType Type)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query ="SELECT top 1"
                          + " [TestID]"
                          + ",[AppointmentDate]"
                          + ",[PaidFees]"
                          + ",[AppointmentMadeByUserID]"
                          + ",[TestApplicationID]"
                          + ",[TestResult]"
                          + ",[Notes]"
                          + ",[ResultAddedByUserID]"
                       + "FROM[dbo].[Tests]"
                       + " Order By [AppointmentDate] Asc"
                       + " Where [LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID"
                       + " And [TestType] = @Type";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@Type", Type.ToDBValue());
            DTTest FindedTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedTest = new DTTest
                     ((int)Reader["TestID"],
                      Type,
                      LocalDrivingLicenseApplicationID,
                      (DateTime)Reader["AppointmentDate"],
                      (decimal)Reader["PaidFees"],
                      (int)Reader["AppointmentMadeByUserID"],
                      (int)Reader["TestApplicationID"],
                      Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"],
                      Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"],
                      Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"]
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

        public static DTTest Find(int TestID)
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                  + " [TestType]"
                  + ",[LocalDrivingLicenseApplicationID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestApplicationID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                  + " FROM[dbo].[Tests]"
                      + " where TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestID", TestID);
            DTTest FindedTest = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedTest = new DTTest
                     (TestID,
                      ((string)Reader["TestType"]).ToTestType(),
                      (int)Reader["LocalDrivingLicenseApplicationID"], 
                      (DateTime)Reader["AppointmentDate"],
                      (decimal)Reader["PaidFees"],
                      (int)Reader["AppointmentMadeByUserID"],
                      (int)Reader["TestApplicationID"],
                      Reader["TestResult"] == DBNull.Value ? null : (bool?)Reader["TestResult"],
                      Reader["Notes"] == DBNull.Value ? null : (string)Reader["Notes"],
                      Reader["ResultAddedByUserID"] == DBNull.Value ? null : (int?)Reader["ResultAddedByUserID"]
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
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = @"select top 1 isExist = 1 from Tests where  TestID =  @TestID";

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

        public static DataTable ListAllTests()
        {
            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query = "SELECT"
                  + " [TestID]"
                  + ",[TestType]"
                  + ",[LocalDrivingLicenseApplicationID]"
                  + ",[AppointmentDate]"
                  + ",[PaidFees]"
                  + ",[AppointmentMadeByUserID]"
                  + ",[TestApplicationID]"
                  + ",[TestResult]"
                  + ",[Notes]"
                  + ",[ResultAddedByUserID]"
                    + " FROM[dbo].[Tests]";

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

        private static int? _AddNewTest(ref DTTest TestToAdd)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[Tests]" +
           " VALUES" +
           "( @TestType" +
           ", @LocalDrivingLicenseApplicationID" +
           ", @AppointmentDate" +
           ", @PaidFees" +
           ", @AppointmentMadeByUserID" +
           ", @TestApplicationID" +
           ", @TestResult" +
           ", @Notes" +
           ", @ResultAddedByUserID );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestType", TestToAdd.TestType.ToDBValue());
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestToAdd.LocalDrivingLicenseApplicationID);
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

            TestToAdd._TestID = AddedID ?? -1;
            return AddedID;

        }

        //        ( ResultAddedByUserID & TestResult )      //
        //       are ether -Both Null- or -Both not Null-   //

        public static DTTest AddNewTest(TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool TestResult, string Notes, int ResultAddedByUserID)
        {
            DTTest NewLicense = new DTTest(-1, TestType, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID);

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

        public static DTTest AddNewTest(TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            DTTest NewLicense = new DTTest(-1, TestType, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID);

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

        public static bool UpdateTest(int TestID, TestType TestType, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int AppointmentMadeByUserID, int TestApplicationID, bool? TestResult, string Notes, int? ResultAddedByUserID)
        {
            if (!IsExist(TestID))
                return false;

            return UpdateTest(new DTTest(TestID, TestType, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                AppointmentMadeByUserID, TestApplicationID, TestResult, Notes, ResultAddedByUserID));
        }

        public static bool UpdateTest(DTTest TestToUpdate)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "UPDATE[dbo].[Tests]" +
              "SET " +
              "[LocalDrivingLicenseApplicationID]              = @LocalDrivingLicenseApplicationID" +
              ",[AppointmentDate]             = @AppointmentDate" +
              ",[PaidFees]               = @PaidFees" +
              ",[AppointmentMadeByUserID]        = @AppointmentMadeByUserID" +
              ",[TestApplicationID]            = @TestApplicationID" +
              ",[TestResult]       = @TestResult" +
              ",[Notes]       = @Notes" +
              ",[ResultAddedByUserID]       = @ResultAddedByUserID" +
                   " WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestToUpdate.LocalDrivingLicenseApplicationID);
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

        public static bool DeleteTest(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(SettingsClass.DataAccessString);

            string Query =

              "delete from [dbo].[Tests]" +
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
