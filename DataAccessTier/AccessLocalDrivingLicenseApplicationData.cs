using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using General;

namespace DataAccessTier
{
    public class DTLocalDrivingLicenseApplication
    {
        internal protected DTLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID,
            string IdentificationPhotoFileName, string DrivingCourseCertificatePhotoFileName)
        {
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClassID;
            this.EyeTestID = EyeTestID;
            this.TheoritecalTestID = TheoritecalTestID;
            this.DrivingTestID = DrivingTestID;
            this.IdentificationPhotoFileName = IdentificationPhotoFileName;
            this.DrivingCourseCertificatePhotoFileName = DrivingCourseCertificatePhotoFileName;
        }

        internal int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID { get { return _LocalDrivingLicenseApplicationID; } }

        public int ApplicationID;
        public LicenseClass LicenseClass;
        public int? EyeTestID;
        public int? TheoritecalTestID;
        public int? DrivingTestID;
        public string IdentificationPhotoFileName;
        public string DrivingCourseCertificatePhotoFileName;

        // This method Checks if felid that are NOT null's valied or not
        public bool CheckMainApplicationAndNulls()
        {

            switch (AccessApplicationData.GetApplicationType(ApplicationID))
            {
                case ApplicationType.LicenseIssuance:
                    break;

                case ApplicationType.RenewDrivingLicense:
                    if (DrivingTestID != null ||
                         TheoritecalTestID != null ||
                          !string.IsNullOrEmpty(IdentificationPhotoFileName) ||
                           !string.IsNullOrEmpty(DrivingCourseCertificatePhotoFileName))
                        return false;
                    break;

                case ApplicationType.DamagedReplacement:
                    if (EyeTestID != null ||
                         DrivingTestID != null ||
                          TheoritecalTestID != null ||
                           !string.IsNullOrEmpty(IdentificationPhotoFileName) ||
                            !string.IsNullOrEmpty(DrivingCourseCertificatePhotoFileName))
                        return false;
                    break;

                case ApplicationType.MissingReplacement:
                    if (EyeTestID != null ||
                         DrivingTestID != null ||
                          TheoritecalTestID != null ||
                           !string.IsNullOrEmpty(IdentificationPhotoFileName) ||
                            !string.IsNullOrEmpty(DrivingCourseCertificatePhotoFileName))
                        return false;

                    break;
                default:
                    return false;
            }

            return true;
        }
   
    }

    public class AccessLocalDrivingLicenseApplicationData
    {
        public static DTLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + " [ApplicationID]"
                  + ",[LicenseClassID]"
                  + ",[EyeTestID]"
                  + ",[TheoreticalTestID]"
                  + ",[DrivingTestID]"
                  + ",[IdentificationPhotoFileName]" 
                  + ",[DrivingCourseCertificatePhotoFileName]" 
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            DTLocalDrivingLicenseApplication FindedLocalDrivingLicenseApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication
                     (LocalDrivingLicenseApplicationID,
                       (int)Reader["ApplicationID"],
                       ((int)Reader["LicenseClassID"]).ToLicenseClass(),
                       Reader["EyeTestID"] == DBNull.Value ? null : (int?)Reader["EyeTestID"],
                       Reader["TheoreticalTestID"] == DBNull.Value ? null : (int?)Reader["TheoreticalTestID"],
                       Reader["DrivingTestID"] == DBNull.Value ? null : (int?)Reader["DrivingTestID"],
                       Reader["IdentificationPhotoFileName"] == DBNull.Value ? null : (string)Reader["IdentificationPhotoFileName"],
                       Reader["DrivingCourseCertificatePhotoFileName"] == DBNull.Value ? null : (string)Reader["DrivingCourseCertificatePhotoFileName"]
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

            return FindedLocalDrivingLicenseApplication;

        }
        
        public static DTLocalDrivingLicenseApplication FindLeatestActiveLocalAppOfClassForPersonID(int PersonID, LicenseClass Class)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + " [LocalDrivingLicenseApplicationID]"
                  + ",[ApplicationID]"
                  + ",[EyeTestID]"
                  + ",[TheoreticalTestID]"
                  + ",[DrivingTestID]"
                  + ",[IdentificationPhotoFileName]"
                  + ",[DrivingCourseCertificatePhotoFileName]"
                  + " FROM [dbo].[LocalDrivingLicenseApplications] inner join [dbo].[Applications] on [LocalDrivingLicenseApplications].[ApplicationID]=[Applications].[ApplicationID] "
                  + " where LicenseClasses = @LicenseClasses and"
                  + " ApplicantPersonID = @ApplicantPersonID "
                  + " order by Applications.ApplicationDate desc";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseClasses", Class);
            Command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            DTLocalDrivingLicenseApplication FindedLocalDrivingLicenseApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication
                     ((int)Reader["LocalDrivingLicenseApplicationID"],
                       (int)Reader["ApplicationID"],
                       Class,
                       Reader["EyeTestID"] == DBNull.Value ? null : (int?)Reader["EyeTestID"],
                       Reader["TheoreticalTestID"] == DBNull.Value ? null : (int?)Reader["TheoreticalTestID"],
                       Reader["DrivingTestID"] == DBNull.Value ? null : (int?)Reader["DrivingTestID"],
                       Reader["IdentificationPhotoFileName"] == DBNull.Value ? null : (string)Reader["IdentificationPhotoFileName"],
                       Reader["DrivingCourseCertificatePhotoFileName"] == DBNull.Value ? null : (string)Reader["DrivingCourseCertificatePhotoFileName"]
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

            return FindedLocalDrivingLicenseApplication;

        }

        public static LicenseClass? FindClass(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                    + "[LicenseClassID]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            LicenseClass? Class = null;

            try
            {
                Connection.Open();
                Object Object = null;
                Object = Command.ExecuteScalar();


                if (Object != null)
                {
                    Class = (LicenseClass)Convert.ToInt32(Class);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return Class;

        }

        public static LicenseClass? FindClassByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                    + "[LicenseClassID]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            LicenseClass? Class = null;

            try
            {
                Connection.Open();
                Object Object = null;
                Object = Command.ExecuteScalar();


                if (Object != null)
                {
                    Class = (LicenseClass)Convert.ToInt32(Class);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return Class;

        }

        public static DTLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + " [LocalDrivingLicenseApplicationID]"
                  + ",[LicenseClassID]"
                  + ",[EyeTestID]"
                  + ",[TheoreticalTestID]"
                  + ",[DrivingTestID]"
                  + ",[IdentificationPhotoFileName]"
                  + ",[DrivingCourseCertificatePhotoFileName]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            DTLocalDrivingLicenseApplication FindedLocalDrivingLicenseApplication = null;

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();



                if (Reader.Read())
                {
                    FindedLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication
                     ((int)Reader["LocalDrivingLicenseApplicationID"],
                       ApplicationID,
                       ((int)Reader["LicenseClassID"]).ToLicenseClass(),
                       Reader["EyeTestID"] == DBNull.Value ? null : (int?)Reader["EyeTestID"],
                       Reader["TheoreticalTestID"] == DBNull.Value ? null : (int?)Reader["TheoreticalTestID"],
                       Reader["DrivingTestID"] == DBNull.Value ? null : (int?)Reader["DrivingTestID"],
                       Reader["IdentificationPhotoFileName"] == DBNull.Value ? null : (string)Reader["IdentificationPhotoFileName"],
                       Reader["DrivingCourseCertificatePhotoFileName"] == DBNull.Value ? null : (string)Reader["DrivingCourseCertificatePhotoFileName"]
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

            return FindedLocalDrivingLicenseApplication;

        }

        public static LicenseClass? GetLocalDrivingApplicationClass(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT "
                  + "[LicenseClassID]"
                    + " FROM [dbo].[LocalDrivingLicenseApplications]"
                    + " where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            LicenseClass? FindedClass = null;

            try
            {
                Connection.Open();
                object LicenseClass = Command.ExecuteScalar();

                FindedClass = LicenseClass == null ? null : (LicenseClass?)LicenseClass; 
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return FindedClass;

        }

        public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = @"select top 1 isExist = 1 from LocalDrivingLicenseApplications where  LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

            string Query = @"select top 1 isExist = 1 from LocalDrivingLicenseApplications where  ApplicationID =  @ApplicationID";

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

        public static DataTable ListAllLocalDrivingLicenseApplications()
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "SELECT"
                  + " [LocalDrivingLicenseApplicationID]"
                  + ",[ApplicationID]"
                  + ",[LicenseClassID]"
                  + ",[EyeTestID]"
                  + ",[TheoreticalTestID]"
                  + ",[DrivingTestID]"
                  + ",[IdentificationPhotoFileName]"
                  + ",[DrivingCourseCertificatePhotoFileName]"
                    + " FROM[dbo].[LocalDrivingLicenseApplications]";

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

        private static int? _AddNew(ref DTLocalDrivingLicenseApplication LocalAppToAdd)
        {
            if (LocalAppToAdd == null)
                return null;

            if (!LocalAppToAdd.CheckMainApplicationAndNulls())
                return null;            
            
            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =
                "INSERT INTO [dbo].[LocalDrivingLicenseApplications]" +
           " VALUES " +
           "( @ApplicationID" +
           " ,@LicenseClass" +
           " ,@EyeTestID" +
           ",@TheoreticalTestID" +
           ",@DrivingTestID" +
           ",@IdentificationPhotoFileName" +
           ",@DrivingCourseCertificatePhotoFileName);" +
           " SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", LocalAppToAdd.ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClass", (LocalAppToAdd.LicenseClass).ToDBValue());
            if (LocalAppToAdd.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@EyeTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@EyeTestID", LocalAppToAdd.EyeTestID);
            }

            if (LocalAppToAdd.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@TheoreticalTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@TheoreticalTestID", LocalAppToAdd.TheoritecalTestID);
            }

            if (LocalAppToAdd.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@DrivingTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@DrivingTestID", LocalAppToAdd.DrivingTestID);
            }

            if (LocalAppToAdd.IdentificationPhotoFileName == null)
            {
                Command.Parameters.AddWithValue("@IdentificationPhotoFileName", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@IdentificationPhotoFileName", LocalAppToAdd.IdentificationPhotoFileName);
            }

            if (LocalAppToAdd.DrivingCourseCertificatePhotoFileName == null)
            {
                Command.Parameters.AddWithValue("@DrivingCourseCertificatePhotoFileName", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@DrivingCourseCertificatePhotoFileName", LocalAppToAdd.DrivingCourseCertificatePhotoFileName);
            }

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

            LocalAppToAdd._LocalDrivingLicenseApplicationID = (AddedID == null) ? -1 : (int)AddedID;
            return AddedID;

        }

        public static DTLocalDrivingLicenseApplication AddNew(int ApplicationID, LicenseClass LicenseClassID,
            int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID,
            string IdentificationPhotoFilePath, string DrivingCourseCertificatePhotoFilePath)
        {
            // ID Copy
            if (string.IsNullOrEmpty(IdentificationPhotoFilePath))
                return null;

            if (!GeneralFunctions.IsValidImage(IdentificationPhotoFilePath))
                return null;

            string IDFileName = DTFunctions.GetNextFileName(SettingsClass.Paths.IdentificationsCopes.ImagesPath);

            if (DTFunctions.CopyFile
                (IdentificationPhotoFilePath, SettingsClass.Paths.IdentificationsCopes.ImagesPath + IDFileName) != true) 
                return null;

            //  Driving Course Copy
            if (string.IsNullOrEmpty(DrivingCourseCertificatePhotoFilePath))
                return null;

            if (!GeneralFunctions.IsValidImage(DrivingCourseCertificatePhotoFilePath))
                return null;

            string CoutseFileName = DTFunctions.GetNextFileName(SettingsClass.Paths.DrivingCourseCertificatesCopes.ImagesPath);

            if (DTFunctions.CopyFile
                (DrivingCourseCertificatePhotoFilePath, SettingsClass.Paths.DrivingCourseCertificatesCopes.ImagesPath + IDFileName) != true)
                return null;

            DTLocalDrivingLicenseApplication NewLocalDrivingLicenseApplication = new DTLocalDrivingLicenseApplication(-1, ApplicationID,
                LicenseClassID, EyeTestID, TheoritecalTestID, DrivingTestID,
                IDFileName, CoutseFileName);

            int? ID = _AddNew(ref NewLocalDrivingLicenseApplication);

            if (ID != null)
            {
                return NewLocalDrivingLicenseApplication; // the ( _AddNewLocalDrivingLicenseApplication ) method will insert the new ID to the NewLocalDrivingLicenseApplication Objedct
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID,
            LicenseClass LicenseClassID, int? EyeTestID, int? TheoritecalTestID, int? DrivingTestID
            , string IdentificationPhotoFilePath, string DrivingCourseCertificatePhotoFilePath)
        {
            // ID Copy
            if (string.IsNullOrEmpty(IdentificationPhotoFilePath))
                return false;

            if (!GeneralFunctions.IsValidImage(IdentificationPhotoFilePath))
                return false;

            string IDFileName = DTFunctions.GetNextFileName(SettingsClass.Paths.IdentificationsCopes.ImagesPath);

            if (DTFunctions.CopyFile
                (IdentificationPhotoFilePath, SettingsClass.Paths.IdentificationsCopes.ImagesPath + IDFileName) != true)
                return false;

            //  Driving Course Copy
            if (string.IsNullOrEmpty(DrivingCourseCertificatePhotoFilePath))
                return false;

            if (!GeneralFunctions.IsValidImage(DrivingCourseCertificatePhotoFilePath))
                return false;

            string CoutseFileName = DTFunctions.GetNextFileName(SettingsClass.Paths.DrivingCourseCertificatesCopes.ImagesPath);

            if (DTFunctions.CopyFile
                (DrivingCourseCertificatePhotoFilePath, SettingsClass.Paths.DrivingCourseCertificatesCopes.ImagesPath + IDFileName) != true)
                return false;

            return UpdateLocalDrivingLicenseApplication(new DTLocalDrivingLicenseApplication
                (LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, EyeTestID, TheoritecalTestID, DrivingTestID,
                IdentificationPhotoFilePath, DrivingCourseCertificatePhotoFilePath));
        }


        /*
         */

        public static bool UpdateLocalDrivingLicenseApplication(DTLocalDrivingLicenseApplication LocalAppToUpdate)
        {
            if (LocalAppToUpdate == null)
                return false;

            if (LocalAppToUpdate.LocalDrivingLicenseApplicationID < 0)
                return false;

            if (LocalAppToUpdate == null)
                return false;
            
            DTLocalDrivingLicenseApplication OldApp = Find(LocalAppToUpdate.ApplicationID);

            if (OldApp.ApplicationID != LocalAppToUpdate.ApplicationID ||
                 OldApp.LicenseClass != LocalAppToUpdate.LicenseClass)
                return false;


            if (!LocalAppToUpdate.CheckMainApplicationAndNulls())
                return false;

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query =

              "UPDATE[dbo].[LocalDrivingLicenseApplications]" +
              " SET" +
              " [ApplicationID]   = @ApplicationID" +
              ",[LicenseClassID]        = @LicenseClassID" +
              ",[EyeTestID]    = @EyeTestID" +
              ",[TheoreticalTestID]    = @TheoreticalTestID" +
              ",[DrivingTestID]    = @DrivingTestID" +
              ",[IdentificationPhotoFileName]    = @IdentificationPhotoFileName" +
              ",[DrivingCourseCertificatePhotoFileName]    = @DrivingCourseCertificatePhotoFileName" +
                    " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", LocalAppToUpdate.ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LocalAppToUpdate.LicenseClass);

            if (LocalAppToUpdate.EyeTestID == null)
            {
                Command.Parameters.AddWithValue("@EyeTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@EyeTestID", LocalAppToUpdate.EyeTestID);
            }

            if (LocalAppToUpdate.TheoritecalTestID == null)
            {
                Command.Parameters.AddWithValue("@TheoreticalTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@TheoreticalTestID", LocalAppToUpdate.TheoritecalTestID);
            }

            if (LocalAppToUpdate.DrivingTestID == null)
            {
                Command.Parameters.AddWithValue("@DrivingTestID", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@DrivingTestID", LocalAppToUpdate.DrivingTestID);
            }

            if (string.IsNullOrEmpty(LocalAppToUpdate.IdentificationPhotoFileName))
            {
                Command.Parameters.AddWithValue("@IdentificationPhotoFileName", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@IdentificationPhotoFileName", LocalAppToUpdate.IdentificationPhotoFileName);
            }

            if (string.IsNullOrEmpty(LocalAppToUpdate.DrivingCourseCertificatePhotoFileName))
            {
                Command.Parameters.AddWithValue("@DrivingCourseCertificatePhotoFileName", DBNull.Value);
            }
            else
            {
                Command.Parameters.AddWithValue("@DrivingCourseCertificatePhotoFileName", LocalAppToUpdate.DrivingCourseCertificatePhotoFileName);
            }

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppToUpdate.LocalDrivingLicenseApplicationID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int IDToDelete)
        {

            SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

            string Query = "delete from [dbo].[LocalDrivingLicenseApplications]" +
                   " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", IDToDelete);

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
