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
    public class DTDetainLicenseRecord
    {
        internal protected DTDetainLicenseRecord(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID,
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
                if (!((ReleasedByUserID == null) == (ReleaseDate == null)))
                    return true;
                else
                    return false;
            }
        }

        public bool IsReleased
        {
            get
            {
                if (ReleasedByUserID == null && ReleaseDate == null)
                    return true;
                else
                    return false;
            }
        }

    }

    public class AccessDetainingLicenseRecordData
    {
        public static DTDetainLicenseRecord FindLastDetainingOfLicense(int LicenseID)
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
            DTDetainLicenseRecord FindedDetainedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedDetainedLicense = new DTDetainLicenseRecord
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

        public static DTDetainLicenseRecord FindByApplicationID(int ReleaseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT top 1"
                  + " [DetainID]"
                  + ",[LicenseID]"
                  + ",[DetainDate]"
                  + ",[FineFees]"
                  + ",[CreatedByUserID]"
                  + ",[ReleaseDate]"
                  + ",[ReleasedByUserID]"
                  + "FROM [dbo].[DetainedLicenses]"
                  + " where ReleaseApplicationID = @ReleaseApplicationID"
                  + " order by DetainDate desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            DTDetainLicenseRecord FindedDetainedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    FindedDetainedLicense = new DTDetainLicenseRecord
                     ((int)Reader["DetainID"],
                       (int)Reader["LicenseID"],
                       (DateTime)Reader["DetainDate"],
                       (decimal)Reader["FineFees"],
                       (int)Reader["CreatedByUserID"],
                       Reader["ReleaseDate"] == DBNull.Value ? null : (DateTime?)Reader["ReleaseDate"],
                       Reader["ReleasedByUserID"] == DBNull.Value ? null : (int?)Reader["ReleasedByUserID"],
                       ReleaseApplicationID
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

        public static DTDetainLicenseRecord Find(int DetainID)
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
            DTDetainLicenseRecord FindedDetainedLicense = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedDetainedLicense = new DTDetainLicenseRecord
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

        private static int? _AddNewDetainedLicense(ref DTDetainLicenseRecord DetainedLicenseToAdd)
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

        //    (ReleaseDate & ReleasedByUserID)     //
       //      are both -Null- or -Not Null-      //

        public static DTDetainLicenseRecord AddNewDetainedLicense (int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID)
        {
            DTDetainLicenseRecord NewLicense = new DTDetainLicenseRecord(-1, LicenseID, DetainDate, FineFees,
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
        
        public static DTDetainLicenseRecord AddNewDetainedLicense (int LicenseID, DateTime DetainDate,
            decimal FineFees, int CreatedByUserID, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            DTDetainLicenseRecord NewLicense = new DTDetainLicenseRecord(-1, LicenseID, DetainDate, FineFees, CreatedByUserID,
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
            return _UpdateDetainingLicenseRecord(new DTDetainLicenseRecord(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                    ReleaseDate, ReleasedByUserID, ReleaseApplicationID));

        }

        private static bool _UpdateDetainingLicenseRecord(DTDetainLicenseRecord DetainedLicenseToUpdate)
        {
            if (DetainedLicenseToUpdate == null)
                return false;


            if (!DetainedLicenseToUpdate.IsValidReleasing) 
                return false;

            DTDetainLicenseRecord OldDetainedRecord = Find(DetainedLicenseToUpdate.DetainID);

            if (OldDetainedRecord == null)
                return false;

            if (OldDetainedRecord.ReleaseApplicationID != null &&
                DetainedLicenseToUpdate.ReleaseApplicationID == null)
                return false;

            // Be Carefull this check should be directly before executing the SQL Command
            if (!OldDetainedRecord.IsReleased && DetainedLicenseToUpdate.IsReleased)
                if (!AccessLicenseData.ActivateLicense(DetainedLicenseToUpdate.LicenseID))
                    return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[DetainedLicenses]" +
              "SET " +
              " [LicenseID] = @LicenseID" +
              ",[DetainDate] = @DetainDate" +
              ",[FineFees] = @FineFees" +
              ",[CreatedByUserID] = @CreatedByUserID" +
              ",[ReleaseDate] = @ReleaseDate" +
              ",[ReleasedByUserID] = @ReleasedByUserID" +
              ",[ReleaseApplicationID] = @ReleaseApplicationID" +
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

        public static bool UpdateFineFee(int DetainedRecordIDToUpdate, decimal NewFineFee)
        {
            if (DetainedRecordIDToUpdate < 0)
                return false;
            if (NewFineFee < 0)
                return false;

            DTDetainLicenseRecord RecordToUpdate = Find(DetainedRecordIDToUpdate);

            if (RecordToUpdate == null)
                return false;

            if (RecordToUpdate.IsReleased)
                return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
              "UPDATE[dbo].[DetainedLicenses]" +
              " SET" +
              " [FineFees] = @FineFees" +
                   " WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FineFees", NewFineFee);

            Command.Parameters.AddWithValue("@DetainID", DetainedRecordIDToUpdate);

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

        public static bool ReleaseDetainingRecord(int DetainedRecordIDToRelease,int ReleasedByUserID)
        {
            if (DetainedRecordIDToRelease < 0)
                return false;
            if (ReleasedByUserID < 0)
                return false;

            if (!AccessUserData.IsExist(ReleasedByUserID))
                return false;

            DTDetainLicenseRecord RecordToRelease = Find(DetainedRecordIDToRelease);

            if (RecordToRelease == null)
                return false;

            if (RecordToRelease.ReleaseApplicationID == null)
                return false;

            if (RecordToRelease.IsReleased)
                return false;


            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[DetainedLicenses]" +
              " SET " +
              " [ReleaseDate] = @ReleaseDate" +
              ",[ReleasedByUserID] = @ReleasedByUserID" +
                   " WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
           
            Command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

            Command.Parameters.AddWithValue("@DetainID", DetainedRecordIDToRelease);

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

            if (DoesUpdateSucceded == true)
                if (!AccessLicenseData.ActivateLicense(RecordToRelease.LicenseID))
                    AccessLicenseData.ActivateLicense(RecordToRelease.LicenseID);

            return DoesUpdateSucceded;
        }       

        public static bool AttachToAppliceation(int DetainingRecordToAttach, int ApplicationIDAttachTo)
        {
            // All vlidations here are data integrity related //

            DTDetainLicenseRecord RecordToAttachTo = Find(DetainingRecordToAttach);

            if (RecordToAttachTo == null)
                return false;

            if (RecordToAttachTo.IsReleased)
                return false;

            if (RecordToAttachTo.ReleaseApplicationID != null)
            {
                if (!SettingsClass.Application.IsExpired(
                        (DateTime)AccessApplicationData.GetApplicationCreationDate((int)RecordToAttachTo.ReleaseApplicationID)))
                    return false;
            }  

            DTApplication ApplicationToAttachWith = AccessApplicationData.Find(ApplicationIDAttachTo);

            if (ApplicationToAttachWith == null)
                return false;

            if (ApplicationType.ReleaseLicense != ApplicationToAttachWith.ApplicationTypeID)
                return false;

            if (ApplicationToAttachWith.IsExpired)
                return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[DetainedLicenses]" +
              "SET " +
              "[ReleaseApplicationID] = @ReleaseApplicationID" +
                   " WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ReleaseApplicationID", ApplicationIDAttachTo);
            
            Command.Parameters.AddWithValue("@DetainID", DetainingRecordToAttach);

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
