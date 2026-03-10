using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessTier
{
    internal class DataAccessSettings
    {
        public static string DataAccessString { get { return _DataAccessString; } }
        private static string _DataAccessString = @"Server=.;Database=MjdDVLD;User Id=sa;Password=12345;";
    }

    internal static class DTFunctions
    {
        // Does NOT : Make new Directories, Override Files //
        // And you need to make sure that the OS give the app the roght permetions // 
        internal static bool? CopyFile(string FilePath, string FilePathToCopyTo)
        {
            if (!File.Exists(FilePath))
                return false;

            if (!Directory.Exists(FilePathToCopyTo.Substring(0, FilePathToCopyTo.Length - Path.GetFileName(FilePathToCopyTo).Length)))
                return false;

            if (File.Exists(FilePathToCopyTo))
                return false;

            try
            {
                File.Copy(FilePath, FilePathToCopyTo);
            }
            catch(Exception ex)
            {
                if (!File.Exists(FilePathToCopyTo))
                    return false;

                return null;
            }

            return true;
        }

        // this method takes the path of the photo and
        // finds the next name which is a hexDecimal number in string
        // Note : It creates an indexing file
        internal static string GetNextFileName(string Path)
        {
            string IndexingFileName = "IndexingFile";

            if (Path.EndsWith("/") || Path.EndsWith("\\"))
                Path = Path.Substring(0, Path.Length - 1);

            int NextName = 0;

            if(File.Exists(Path +"/"+IndexingFileName))
            {
                StreamReader Reader = new StreamReader(Path + "/" + IndexingFileName);

                NextName = Convert.ToInt32(Reader.ReadLine().Trim());
            }

            using (StreamWriter OpenedFile = new StreamWriter(Path + "/" + IndexingFileName, false))
            {
                NextName += 1;

                OpenedFile.WriteLine(NextName.ToString());
            }

            return NextName.ToString("X");
        }

    }

    public static class InitializingDate
    {
        public static void LoadTheDBConnection()
        {
            _ = Task.Run(() =>
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = "";

                SqlCommand Command = new SqlCommand(Query, Connection);

                try
                {
                    Connection.Open();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

            });

        }
    
    }
}
