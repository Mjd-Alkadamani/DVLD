using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generale
{
    public enum LicenseClass { Motorcycles = 1, LargeMotorcycles = 2, RegularCar = 3, PublicVehicles = 4, AgriculturalVehicles = 5, Buses = 6, HeavyVhicles = 7 }
    public enum ApplicationType { LicenseIssuance = 1, RetakeTest = 2, RenewDrivingLicense = 3, MissingReplacement = 4, DamagedReplacement = 5, ReleaseLicense = 6, IssuingInternationalLicense = 7 }
    public enum TestType { TheoreticalTest = 0, DrivingTest = 1 };
    public enum Gendor { Femail = 0, Mail = 1 };
    public enum IssueReason { NewIssuance = 1, Renewal = 2, DamagedReplacement = 3, LostReplacement = 4 };
    public enum ApplicationStatus { New = 1, Canceled = 2, Completed = 3 };

    public class SettingsClass
    {
        // // License // //

        private static int _MotorcyclesLicenseYears = 1;
        private static int _LargeMotorcyclesLicenseYears = 1;
        private static int _RegularCarLicenseYears = 1;
        private static int _PublicVehiclesLicenseYears = 1;
        private static int _AgriculturalVehiclesLicenseYears = 1;
        private static int _BusesLicenseYears = 1;
        private static int _HeavyVhiclesLicenseYears = 1;

        // // InterNational License // //

        // when "= false" the international license expiration date can not be after Local on expiration date //
        private static bool _AllowInternationalLicenseExpirationDateToByPassLocalOne = false;
        public static bool AllowInternationalLicenseExpirationDateToByPassLocalOne { get { return _AllowInternationalLicenseExpirationDateToByPassLocalOne; } }


        private static int _InternationalMotorcyclesLicenseYears = 1;
        private static int _InternationalLargeMotorcyclesLicenseYears = 1;
        private static int _InternationalRegularCarLicenseYears = 1;
        private static int _InternationalPublicVehiclesLicenseYears = 1;
        private static int _InternationalAgriculturalVehiclesLicenseYears = 1;
        private static int _InternationalBusesLicenseYears = 1;
        private static int _InternationalHeavyVhiclesLicenseYears = 1;

        // // minemum age (More or Equal) by integer numbers of years // //

        private static int _MotorcyclesLicenseMinAge = 18;
        private static int _LargeMotorcyclesLicenseMinAge = 18;
        private static int _RegularCarLicenseMinAge = 18;
        private static int _PublicVehiclesLicenseMinAge = 18;
        private static int _AgriculturalVehiclesLicenseMinAge = 18;
        private static int _BusesLicenseMinAge = 18;
        private static int _HeavyVhiclesLicenseMinAge = 18;

        // // InterNational License  fees  // //

        private static decimal _InternationalMotorcyclesLicenseFee = 1;
        private static decimal _InternationalLargeMotorcyclesLicenseFee = 1;
        private static decimal _InternationalRegularCarLicenseFee = 1;
        private static decimal _InternationalPublicVehiclesLicenseFee = 1;
        private static decimal _InternationalAgriculturalVehiclesLicenseFee = 1;
        private static decimal _InternationalBusesLicenseFee = 1;
        private static decimal _InternationalHeavyVhiclesLicenseFee = 1;

        // // // // // 

        public static decimal GetInternationalLicensIssuanceFees(LicenseClass Class)
        {
            switch (Class)
            {
                case LicenseClass.Motorcycles:
                    return _InternationalMotorcyclesLicenseFee;
                case LicenseClass.LargeMotorcycles:
                    return _InternationalLargeMotorcyclesLicenseFee;
                case LicenseClass.RegularCar:
                    return _InternationalRegularCarLicenseFee;
                case LicenseClass.PublicVehicles:
                    return _InternationalPublicVehiclesLicenseFee;
                case LicenseClass.AgriculturalVehicles:
                    return _InternationalAgriculturalVehiclesLicenseFee;
                case LicenseClass.Buses:
                    return _InternationalBusesLicenseFee;
                default: // LicenseClass.HeavyVhicles 
                    return _InternationalHeavyVhiclesLicenseFee;
            }
        }

       public static int HowManyYears(LicenseClass Class)
        {
            switch(Class)
            {
                case LicenseClass.Motorcycles:
                    return _MotorcyclesLicenseYears;
                case LicenseClass.LargeMotorcycles:
                    return _LargeMotorcyclesLicenseYears;
                case LicenseClass.RegularCar:
                    return _RegularCarLicenseYears;
                case LicenseClass.PublicVehicles:
                    return _PublicVehiclesLicenseYears;
                case LicenseClass.AgriculturalVehicles:
                    return _AgriculturalVehiclesLicenseYears;
                case LicenseClass.Buses:
                    return _BusesLicenseYears;
                default: // LicenseClass.HeavyVhicles 
                    return _HeavyVhiclesLicenseYears;
            }
        }
        
        public static int HowManyYearsForInternationalLicense(LicenseClass Class)
        {
            switch(Class)
            {
                case LicenseClass.Motorcycles:
                    return _InternationalMotorcyclesLicenseYears;
                case LicenseClass.LargeMotorcycles:
                    return _InternationalLargeMotorcyclesLicenseYears;
                case LicenseClass.RegularCar:
                    return _InternationalRegularCarLicenseYears;
                case LicenseClass.PublicVehicles:
                    return _InternationalPublicVehiclesLicenseYears;
                case LicenseClass.AgriculturalVehicles:
                    return _InternationalAgriculturalVehiclesLicenseYears;
                case LicenseClass.Buses:
                    return _InternationalBusesLicenseYears;
                default: // LicenseClass.HeavyVhicles 
                    return _InternationalHeavyVhiclesLicenseYears;
            }
        }

        public static bool IsDriverOledEnough(DateTime DriverBirthDay,LicenseClass Class)
        {
            switch (Class)
            {
                case LicenseClass.Motorcycles:
                    return (DateTime.Now - DriverBirthDay).Days >= _MotorcyclesLicenseMinAge * 365;
                case LicenseClass.LargeMotorcycles:
                    return (DateTime.Now - DriverBirthDay).Days >=  _LargeMotorcyclesLicenseMinAge * 365;
                case LicenseClass.RegularCar:
                    return (DateTime.Now - DriverBirthDay).Days >= _RegularCarLicenseMinAge * 365;
                case LicenseClass.PublicVehicles:
                    return (DateTime.Now - DriverBirthDay).Days >= _PublicVehiclesLicenseMinAge * 365;
                case LicenseClass.AgriculturalVehicles:
                    return (DateTime.Now - DriverBirthDay).Days >= _AgriculturalVehiclesLicenseMinAge * 365;
                case LicenseClass.Buses:
                    return (DateTime.Now - DriverBirthDay).Days >= _BusesLicenseMinAge * 365;
                default: // LicenseClass.HeavyVhicles 
                    return (DateTime.Now - DriverBirthDay).Days >= _HeavyVhiclesLicenseMinAge * 365;
            }
        }


        public static string _DataAccessString = @"Server=.;Database=MjdDVLD;User Id=sa;Password=12345;";
        public static string DataAccessString { get { return _DataAccessString; } }

        public static string _ImageLocation = @"C:\DVLDImges\";
        public static string ImageLocation { get { return _ImageLocation; } }
            
        public static string _MaleErrorImage = @"C:\DVLDImges\MaleErrorImge.jpg";
        public static string MaleErrorImagePath { get { return _MaleErrorImage; } }
            
        public static string _FemaleErrorImage = @"C:\DVLDImges\FemaleErrorImge.jpg";
        public static string FemaleErrorImagePath { get { return _FemaleErrorImage; } }
    }

}
