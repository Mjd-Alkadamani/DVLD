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
    public class DTDetainedLicense
    {
        internal protected DTDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
             DateTime?  ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            this._DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
        }

        public void SetReleaseInfo(DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this._ReleaseDate = ReleaseDate;
            this._ReleasedByUserID = ReleasedByUserID;
            this._ReleaseApplicationID = ReleaseApplicationID;
        }

        internal int _DetainID;
        public int DetainID { get { return _DetainID; } }
        public int LicenseID;
        public DateTime DetainDate;
        public decimal FineFees;
        public int CreatedByUserID;

        private DateTime? _ReleaseDate = null;
        private int? _ReleasedByUserID = null;
        private int? _ReleaseApplicationID = null;

        public DateTime?  ReleaseDate { get { return _ReleaseDate; } }
        public int? ReleasedByUserID { get { return _ReleasedByUserID; } }
        public int? ReleaseApplicationID { get { return _ReleaseApplicationID; } }

        // Releasing Data validation (all nulls or all with values)
        public bool IsValidReleasing
        {
            get
            {
                if (!((ReleasedByUserID == null) == (ReleaseDate == null) == (ReleaseApplicationID == null)))
                    return true;
                else
                    return false;
            }
        }
    
    }

    public class AccessDetainedLicenseData
    {
        public static DTDetainedLicense FindLastDetainingOfLicense(int LicenseID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1"
                  + " [DetainID]"
                  + ",[DetainDate]"
                  + ",[FineFees]"
                  + ",[CreatedByUserID]"
                  + ",[ReleaseDate]"
                  + ",[ReleasedByUserID]"
                  + ",[ReleaseApplicationID]"
                  + "FROM [dbo].[DetainedLicenses]"
                  + " where LicenseID = @LicenseID"
                  + " order by DetainDate desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            DTDetainedLicense FindedDetainedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedDetainedLicense = new DTDetainedLicense
                     ((int)Reader["DetainID"],
                       LicenseID,
                       (DateTime)Reader["DetainDate"],
                       (decimal)Reader["FineFees"],
                       (int)Reader["CreatedByUserID"],
                       Reader["ReleaseDate"] == DBNull.Value ? null : (DateTime?)Reader["ReleaseDate"],
                       Reader["ReleasedByUserID"] == DBNull.Value ? null : (int?)Reader["ReleasedByUserID"],
                       Reader["ReleaseApplicationID"] == DBNull.Value ? null : (int?)Reader["ReleaseApplicationID"]
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

            return FindedDetainedLicense;

        }

        public static DTDetainedLicense Find(int DetainID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [LicenseID]"
                  + ",[DetainDate]"
                  + ",[FineFees]"
                  + ",[CreatedByUserID]"
                  + ",[ReleaseDate]"
                  + ",[ReleasedByUserID]"
                  + ",[ReleaseApplicationID]"
                  + "FROM [dbo].[DetainedLicenses]"
                      + " where DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);
            DTDetainedLicense FindedDetainedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedDetainedLicense = new DTDetainedLicense
                     ( DetainID,
                       (int)Reader["LicenseID"],
                       (DateTime)Reader["DetainDate"],
                       (decimal)Reader["FineFees"],
                       (int)Reader["CreatedByUserID"],
                       Reader["ReleaseDate"] == DBNull.Value ? null : (DateTime?)Reader["ReleaseDate"],
                       Reader["ReleasedByUserID"] == DBNull.Value ? null : (int?)Reader["ReleasedByUserID"],
                       Reader["ReleaseApplicationID"] == DBNull.Value ? null : (int?)Reader["ReleaseApplicationID"]
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

            return FindedDetainedLicense;

        }

        public static bool IsExist(int DetainID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from DetainedLicenses where  DetainID =  @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);
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

        public static DataTable ListAllDetainingRecords()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT" +
                        " [DetainID]"
                      + ",[LicenseID]"
                      + ",[DetainDate]"
                      + ",[FineFees]"
                      + ",[CreatedByUserID]"
                      + ",[ReleaseDate]"
                      + ",[ReleasedByUserID]"
                      + ",[ReleaseApplicationID]"
                      + " FROM[dbo].[DetainedLicenses]";

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

        private static int? _AddNewDetainedLicense(ref DTDetainedLicense DetainedLicenseToAdd)
        {
            if (DetainedLicenseToAdd.IsValidReleasing)
                return null;
            if (IsLicenseCurrenlyDetained(DetainedLicenseToAdd.LicenseID))
                return null;

            if (DetainedLicenseToAdd.ReleaseApplicationID == null)
                if (!AccessLicenseData.DeactivateLicense(DetainedLicenseToAdd.LicenseID))
                    return null;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[DetainedLicenses]" +
           " VALUES" +
           "( @LicenseID" +
           ", @DetainDate" +
           ", @FineFees" +
           ", @CreatedByUserID" +
           ", @ReleaseDate" +
           ", @ReleasedByUserID" +
           ", @ReleaseApplicationID );" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", DetainedLicenseToAdd.LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainedLicenseToAdd.DetainDate);
            Command.Parameters.AddWithValue("@FineFees", DetainedLicenseToAdd.FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", DetainedLicenseToAdd.CreatedByUserID);

            if (DetainedLicenseToAdd.ReleaseDate == null)
                Command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleaseDate", DetainedLicenseToAdd.ReleaseDate);

            if (DetainedLicenseToAdd.ReleasedByUserID == null)
                Command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleasedByUserID", DetainedLicenseToAdd.ReleasedByUserID);

            if (DetainedLicenseToAdd.ReleaseApplicationID == null)
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DetainedLicenseToAdd.ReleaseApplicationID);



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

            DetainedLicenseToAdd._DetainID = (AddedID == null) ? -1 : (int)AddedID;

            if (AddedID == null)
                if (DetainedLicenseToAdd.ReleaseApplicationID == null)
                    if (!AccessLicenseData.ActivateLicense(DetainedLicenseToAdd.LicenseID))
                        AccessLicenseData.ActivateLicense(DetainedLicenseToAdd.LicenseID);

            return AddedID;

        }

        // (ReleaseDate & ReleasedByUserID & ReleaseApplicationID) //
        //       are ether -All Null- or -All not Null-            //

        public static DTDetainedLicense AddNewDetainedLicense (int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID)
        {
            DTDetainedLicense NewLicense = new DTDetainedLicense(-1, LicenseID, DetainDate, FineFees,
                CreatedByUserID, null, null, null);

            int? ID = _AddNewDetainedLicense( ref NewLicense);

            if (ID != null)
            { 
                return NewLicense; // the ( _AddNewDetainedLicense ) method will insert the new ID to the NewLicense Objedct
            }
            else 
            {
                return null;
            }
        }
        
        public static DTDetainedLicense AddNewDetainedLicense (int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            DTDetainedLicense NewLicense = new DTDetainedLicense(-1, LicenseID, DetainDate, FineFees, CreatedByUserID,
                ReleaseDate, ReleasedByUserID, ReleaseApplicationID);

            int? ID = _AddNewDetainedLicense(ref NewLicense);

            if (ID != null)
            {
                return NewLicense;  // the ( _AddNewDetainedLicense ) method will insert the new ID to the NewLicense Object
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
             DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {            
            return UpdateDetainedLicense(new DTDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                    ReleaseDate, ReleasedByUserID, ReleaseApplicationID));

        }

        public static bool UpdateDetainedLicense(DTDetainedLicense DetainedLicenseToUpdate)
        {
            if (!DetainedLicenseToUpdate.IsValidReleasing) 
                return false;

            if (DetainedLicenseToUpdate.ReleaseApplicationID != null)
                if (!AccessLicenseData.ActivateLicense(DetainedLicenseToUpdate.LicenseID))
                    return false;

            DTDetainedLicense DetainedRecordToEdit = Find(DetainedLicenseToUpdate.DetainID);

            if (DetainedRecordToEdit == null)
                return false;

            if (DetainedRecordToEdit.ReleaseApplicationID != null &&
                DetainedLicenseToUpdate.ReleaseApplicationID == null)
                return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[DetainedLicenses]" +
              "SET " +
              ",[LicenseID]            = @LicenseID" +
              ",[DetainDate]           = @DetainDate" +
              ",[FineFees]            = @FineFees" +
              ",[CreatedByUserID]             = @CreatedByUserID" +
              ",[ReleaseDate]               = @ReleaseDate" +
              ",[ReleasedByUserID]              = @ReleasedByUserID" +
              ",[ReleaseApplicationID]                = @ReleaseApplicationID" +
                   " WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LicenseID", DetainedLicenseToUpdate.LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainedLicenseToUpdate.DetainDate);
            Command.Parameters.AddWithValue("@FineFees", DetainedLicenseToUpdate.FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", DetainedLicenseToUpdate.CreatedByUserID);

            if (DetainedLicenseToUpdate.ReleaseApplicationID == null)
                Command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleaseDate", DetainedLicenseToUpdate.ReleaseApplicationID);

            if (DetainedLicenseToUpdate.ReleasedByUserID == null)
                Command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleasedByUserID", DetainedLicenseToUpdate.ReleasedByUserID);

            if (DetainedLicenseToUpdate.ReleaseApplicationID == null)
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ReleaseApplicationID", DetainedLicenseToUpdate.ReleaseApplicationID);


            Command.Parameters.AddWithValue("@DetainID", DetainedLicenseToUpdate.DetainID);

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

            if (DoesUpdateSucceded == false)
                if (DetainedLicenseToUpdate.ReleaseApplicationID != null)
                    if (!AccessLicenseData.DeactivateLicense(DetainedLicenseToUpdate.LicenseID))
                        AccessLicenseData.DeactivateLicense(DetainedLicenseToUpdate.LicenseID);

            return DoesUpdateSucceded;
        }

        public static bool DeleteDetainedLicense(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "delete from [dbo].[DetainedLicenses]" +
                   "WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@DetainID", IDToDelete);

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

        public static bool IsLicenseCurrenlyDetained(int LicenseIDToToCheck)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 IsExist =1 from DetainedLicenses where LicenseID = @LicenseID and ReleaseApplicationID is null"+
            " order by DetainDate desc;"; // we shoule never find more that one record, but -just in case-

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseIDToToCheck);
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

        public static decimal? HowMuchTheFineFeeForLicense(int LicenseIDToToCheck)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 [FineFees] from DetainedLicenses where LicenseID = @LicenseID and ReleaseApplicationID is null"+
                " order by DetainDate desc;"; // we shoule never find more that one record, but -just in case-

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseIDToToCheck);
            decimal? FineFee = null;

            try
            {
                Connection.Open();

                object Exist = Command.ExecuteScalar();

                if (Exist != null)
                    FineFee = (Decimal)Exist;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FineFee;

        }



    }

}
