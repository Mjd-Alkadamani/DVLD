using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTier
{
    internal class DataAccessSettings
    {
        private static string _DataAccessString = @"Server=.;Database=MjdDVLD;User Id=sa;Password=12345;";
        public static string DataAccessString { get { return _DataAccessString; } }

        public static string _ImageLocation = @"C:\DVLDImges\";
        public static string ImageLocation { get { return _ImageLocation; } }

        public static string _MaleErrorImage = @"C:\DVLDImges\MaleErrorImge.jpg";
        public static string MaleErrorImagePath { get { return _MaleErrorImage; } }

        public static string _FemaleErrorImage = @"C:\DVLDImges\FemaleErrorImge.jpg";
        public static string FemaleErrorImagePath { get { return _FemaleErrorImage; } }

    }
}
