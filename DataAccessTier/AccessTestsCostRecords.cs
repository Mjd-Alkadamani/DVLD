using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using General;

namespace DataAccessTier
{
    public static class AccessTestsCost
    {
        public static class Theoretical
        {
            public static decimal? GetFee(DateTime TestDate,LicenseClass Class)
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = @"select Top 1 Fee from [TheoreticalTestCostRecords] where"
                    + " StartingFromDate < @StartingFromDate and Class = @Class"
                    + " order by StartingFromDate desc";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@StartingFromDate", TestDate);
                Command.Parameters.AddWithValue("@Class", Class);

                decimal? Fee = null;

                try
                {
                    Connection.Open();

                    object Object = Command.ExecuteScalar();

                    if (Object != null)
                        Fee = (decimal)Object;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

                return Fee;

            }

            // Fee - Date - Class (Orderd by Class asc) //
            public static DataTable GetAllLatestVlues()
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = "select TheoreticalTestCostRecords.* from TheoreticalTestCostRecords inner"
                    + " join(select Class, Max(StartingFromDate) as MaxDate from TheoreticalTestCostRecords group by Class) as F1"
                    + " On F1.Class = TheoreticalTestCostRecords.Class and F1.MaxDate = TheoreticalTestCostRecords.StartingFromDate"
                    + " Order by F1.Class asc";


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

        }

        public static class Driving
        {
            public static decimal? GetFee(DateTime TestDate,LicenseClass Class)
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = @"select Top 1 Fee from [DrivingTestCostRecords] where"
                    + " StartingFromDate < @StartingFromDate and Class = @Class"
                    + " order by StartingFromDate desc";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@StartingFromDate", TestDate);
                Command.Parameters.AddWithValue("@Class", Class);

                decimal? Fee = null;

                try
                {
                    Connection.Open();

                    object Object = Command.ExecuteScalar();

                    if (Object != null)
                        Fee = (decimal)Object;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

                return Fee;

            }

            // Fee - Date - Class (Orderd by Class asc) //
            public static DataTable GetAllLatestVlues()
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = "select DrivingTestCostRecords.* from DrivingTestCostRecords inner"
                    + " join(select Class, Max(StartingFromDate) as MaxDate from DrivingTestCostRecords group by Class) as F1"
                    + " On F1.Class = DrivingTestCostRecords.Class and F1.MaxDate = TheoreticalTestCostRecords.StartingFromDate"
                    + " Order by F1.Class asc";


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

        }

        public static class Eye
        {
            public static decimal? GetFee(DateTime TestDate)
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = @"select Top 1 Fee from [EyeTestCostRecords] where"
                    + " StartingFromDate < @StartingFromDate"
                    + " order by StartingFromDate desc";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.AddWithValue("@StartingFromDate", TestDate);

                decimal? Fee = null;

                try
                {
                    Connection.Open();

                    object Object = Command.ExecuteScalar();

                    if (Object != null)
                        Fee = (decimal)Object;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Connection.Close();
                }

                return Fee;

            }

            // Fee - Date (returns only one row)//
            public static DataTable GetAllLatestVlue()
            {
                SqlConnection Connection = new SqlConnection(DataAccessSettings.DataAccessString);

                string Query = "select Top 1 * from EyeTestCostRecords order by StartingFromDate desc";


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

        }
    }
}
