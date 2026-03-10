using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class SettingsClass
    {
        
        // all pathes should end wit "\"
        public static class Paths
        {
            public static class ProfilePhotos
            {
                public static string ProfileImagesPath { get { return _ProfileImagesPath; } }
                private static string _ProfileImagesPath = @"C:\DVLDImges\ProfilePhotos\";

                public readonly static string FemaleProfileErrorImagePath = @"C:\DVLDImges\ProfilePhotos\FemaleErrorImge.jpg";
                public readonly static string MaleProfileErrorImagePath = @"C:\DVLDImges\ProfilePhotos\MaleErrorImge.jpg";
            }

            public static class DrivingCourseCertificatesCopes
            {
                public static string ImagesPath { get { return _ImagesPath; } }
                private static string _ImagesPath = @"C:\DVLDImges\DrivingCourseCertificatesCopes\";
            }

            public static class IdentificationsCopes
            {
                public static string ImagesPath { get { return _ImagesPath; } }
                private static string _ImagesPath = @"C:\DVLDImges\IdentificationsCopes\";
            }

        }

        public static class PeopleSettings
        {            
            public static int MinimumAgeForPersonOnTheSystem; // Years

            public static class Drivers
            {

            }
            public static class Users
            {

            }
        }

        public static class Application
        {
            private readonly static decimal _BaseApplicationFees = 5;

            private readonly static decimal _LicenseIssuanceAdditionalApplicationFee = 0;
            private readonly static decimal _DamageRepalcementAdditionalApplicationFee = 20;
            private readonly static decimal _IssuingInternationalLicenseAdditionalApplicationFee = 20;
            private readonly static decimal _MissingReplacementAdditionalApplicationFee = 20;
            private readonly static decimal _RenewDrivingLicenseAdditionalApplicationFee = 10;
            private readonly static decimal _ReleaseLicenseAdditionalApplicationFee = 0;
            private readonly static decimal _RetakeTestAdditionalApplicationFee = 0;

            public static decimal GetBaseApplicationFees (ApplicationType Type)
            {
                decimal BaseCost = _BaseApplicationFees;

                switch (Type)
                {
                    case ApplicationType.LicenseIssuance:
                        BaseCost += _LicenseIssuanceAdditionalApplicationFee;
                        break;

                    case ApplicationType.DamagedReplacement:
                        BaseCost += _DamageRepalcementAdditionalApplicationFee;
                        break;

                    case ApplicationType.IssuingInternationalLicense:
                        BaseCost += _IssuingInternationalLicenseAdditionalApplicationFee;
                        break;

                    case ApplicationType.MissingReplacement:
                        BaseCost += _MissingReplacementAdditionalApplicationFee;
                        break;
                    
                    case ApplicationType.RenewDrivingLicense:
                        BaseCost += _RenewDrivingLicenseAdditionalApplicationFee;
                        break;
                    
                    case ApplicationType.RetakeTest:
                        BaseCost += _RetakeTestAdditionalApplicationFee;
                        break;

                    case ApplicationType.ReleaseLicense:
                        BaseCost += _ReleaseLicenseAdditionalApplicationFee;
                        break;
                }

                return BaseCost;
            }

            public readonly static TimeSpan ApplicationExpirationPeriod = new TimeSpan(180, 0, 0, 0);

            public static bool IsExpired(DateTime ApplicationDate)
            {
                return DateTime.Now.Subtract(ApplicationDate) > SettingsClass.Application.ApplicationExpirationPeriod;
            }
        }

        public static class TestInfos
        {

            public static class Eye
            {
                private readonly static decimal _CashedFees = 30;
                private readonly static DateTime _FeeAppliesAfter = new DateTime(2026, 1, 1);

                public static decimal? GetCashedFeeIfInDateRange(DateTime TestDate)
                {
                    if (TestDate < _FeeAppliesAfter)
                        return null;
                    else
                        return _CashedFees;
                }

                public readonly static TimeSpan EyeTestExpirationPeriod = new TimeSpan(180, 0, 0, 0);
            }

            public static class Driving
            {
                public readonly static TimeSpan ExpirationPeriod = new TimeSpan(180, 0, 0, 0);

                // Latest Test Fees will be cashed here With its Dates // 

                private static decimal _MotorcyclesTestFee = 30;
                private static DateTime _MotorcyclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _LargeMotorcyclesTestFee = 30;
                private static DateTime _LargeMotorcyclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _RegularCarTestFee = 30;
                private static DateTime _RegularCarFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _PublicVehiclesTestFee = 30;
                private static DateTime _PublicVehiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _AgriculturalVehiclesTestFee = 30;
                private static DateTime _AgriculturalVehiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _BusesTestFee = 30;
                private static DateTime _BusesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _HeavyVhiclesTestFee = 30;
                private static DateTime _HeavyVhiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                public static decimal? GetCashedFeeIfInDateRange(DateTime TestDate, LicenseClass Class)
                {
                    switch (Class)
                    {
                        case LicenseClass.Motorcycles:
                            if (TestDate < _MotorcyclesFeeAppliesAfter)
                                return null;
                            else
                                return _MotorcyclesTestFee;

                        case LicenseClass.LargeMotorcycles:
                            if (TestDate < _LargeMotorcyclesFeeAppliesAfter)
                                return null;
                            else
                                return _LargeMotorcyclesTestFee;

                        case LicenseClass.RegularCar:
                            if (TestDate < _RegularCarFeeAppliesAfter)
                                return null;
                            else
                                return _RegularCarTestFee;

                        case LicenseClass.PublicVehicles:
                            if (TestDate < _PublicVehiclesFeeAppliesAfter)
                                return null;
                            else
                                return _PublicVehiclesTestFee;

                        case LicenseClass.AgriculturalVehicles:
                            if (TestDate < _AgriculturalVehiclesFeeAppliesAfter)
                                return null;
                            else
                                return _AgriculturalVehiclesTestFee;

                        case LicenseClass.Buses:
                            if (TestDate < _BusesFeeAppliesAfter)
                                return null;
                            else
                                return _BusesTestFee;

                        default: // LicenseClass.HeavyVhicles
                            if (TestDate < _HeavyVhiclesFeeAppliesAfter)
                                return null;
                            else
                                return _HeavyVhiclesTestFee;


                    }
                }

            }

            public static class Theoretical
            {
                public readonly static TimeSpan TheoreticalTestExpirationPeriod = new TimeSpan(180, 0, 0, 0);

                // Latest Test Fees will be cashed here With its Dates // 

                private static decimal _MotorcyclesTestFee = 30;
                private static DateTime _MotorcyclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _LargeMotorcyclesTestFee = 30;
                private static DateTime _LargeMotorcyclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _RegularCarTestFee = 30;
                private static DateTime _RegularCarFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _PublicVehiclesTestFee = 30;
                private static DateTime _PublicVehiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _AgriculturalVehiclesTestFee = 30;
                private static DateTime _AgriculturalVehiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _BusesTestFee = 30;
                private static DateTime _BusesFeeAppliesAfter = new DateTime(2026, 1, 1);

                private static decimal _HeavyVhiclesTestFee = 30;
                private static DateTime _HeavyVhiclesFeeAppliesAfter = new DateTime(2026, 1, 1);

                public static decimal? GetCashedFeeIfInDateRange(DateTime TestDate, LicenseClass Class)
                {
                    switch (Class)
                    {
                        case LicenseClass.Motorcycles:
                            if (TestDate < _MotorcyclesFeeAppliesAfter)
                                return null;
                            else
                                return _MotorcyclesTestFee;

                        case LicenseClass.LargeMotorcycles:
                            if (TestDate < _LargeMotorcyclesFeeAppliesAfter)
                                return null;
                            else
                                return _LargeMotorcyclesTestFee;

                        case LicenseClass.RegularCar:
                            if (TestDate < _RegularCarFeeAppliesAfter)
                                return null;
                            else
                                return _RegularCarTestFee;

                        case LicenseClass.PublicVehicles:
                            if (TestDate < _PublicVehiclesFeeAppliesAfter)
                                return null;
                            else
                                return _PublicVehiclesTestFee;

                        case LicenseClass.AgriculturalVehicles:
                            if (TestDate < _AgriculturalVehiclesFeeAppliesAfter)
                                return null;
                            else
                                return _AgriculturalVehiclesTestFee;

                        case LicenseClass.Buses:
                            if (TestDate < _BusesFeeAppliesAfter)
                                return null;
                            else
                                return _BusesTestFee;

                        default: // LicenseClass.HeavyVhicles
                            if (TestDate < _HeavyVhiclesFeeAppliesAfter)
                                return null;
                            else
                                return _HeavyVhiclesTestFee;


                    }
                }


            }

            // General Test information //
            public readonly static TimeSpan MaximumPeriodToSetTestResulte = new TimeSpan(7,0,0,0,0);

        }

        public static class License
        {
            private static readonly TimeSpan _MotorcyclesLicenseYears = new TimeSpan(365,0,0,0);
            private static readonly TimeSpan _LargeMotorcyclesLicenseYears = new TimeSpan(365, 0, 0, 0);
            private static readonly TimeSpan _RegularCarLicenseYears = new TimeSpan(365, 0, 0, 0);
            private static readonly TimeSpan _PublicVehiclesLicenseYears = new TimeSpan(365, 0, 0, 0);
            private static readonly TimeSpan _AgriculturalVehiclesLicenseYears = new TimeSpan(365, 0, 0, 0);
            private static readonly TimeSpan _BusesLicenseYears = new TimeSpan(365, 0, 0, 0);
            private static readonly TimeSpan _HeavyVhiclesLicenseYears = new TimeSpan(365, 0, 0, 0);

            public static TimeSpan HowMuchTimeToExpier(LicenseClass Class)
            {
                switch (Class)
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

            // // minemum age (More or Equal) by integer numbers of years // //

            private readonly static int _MotorcyclesLicenseMinAge = 18;
            private readonly static int _LargeMotorcyclesLicenseMinAge = 21;
            private readonly static int _RegularCarLicenseMinAge = 18;
            private readonly static int _PublicVehiclesLicenseMinAge = 21;
            private readonly static int _AgriculturalVehiclesLicenseMinAge = 21;
            private readonly static int _BusesLicenseMinAge = 21;
            private readonly static int _HeavyVhiclesLicenseMinAge = 21;
            
            public static bool IsDriverOledEnough(DateTime DriverBirthDay, LicenseClass Class)
            {
                switch (Class)
                {
                    case LicenseClass.Motorcycles:
                        return (DateTime.Now - DriverBirthDay).Days >= _MotorcyclesLicenseMinAge * 365;
                    case LicenseClass.LargeMotorcycles:                
                        return (DateTime.Now - DriverBirthDay).Days >= _LargeMotorcyclesLicenseMinAge * 365;
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
            

            // // fees for each License Class // //
            private readonly static decimal _MotorcyclesLicenseIssuanceFee = 15;
            private readonly static decimal _LargeMotorcyclesLicenseIssuanceFee = 30;
            private readonly static decimal _RegularCarLicenseIssuanceFee = 20;
            private readonly static decimal _PublicVehiclesLicenseIssuanceFee = 200;
            private readonly static decimal _AgriculturalVehiclesLicenseIssuanceFee = 50;
            private readonly static decimal _BusesLicenseIssuanceFee = 250;
            private readonly static decimal _HeavyVhiclesLicenseIssuanceFee = 300;

            public static decimal WhatIsTheFeeForLicense(LicenseClass Class)
            {
                switch (Class)
                {
                    case LicenseClass.Motorcycles:
                        return _MotorcyclesLicenseIssuanceFee;
                    case LicenseClass.LargeMotorcycles:
                        return _LargeMotorcyclesLicenseIssuanceFee;
                    case LicenseClass.RegularCar:
                        return _RegularCarLicenseIssuanceFee;
                    case LicenseClass.PublicVehicles:
                        return _PublicVehiclesLicenseIssuanceFee;
                    case LicenseClass.AgriculturalVehicles:
                        return _AgriculturalVehiclesLicenseIssuanceFee;
                    case LicenseClass.Buses:
                        return _BusesLicenseIssuanceFee;
                    default: // LicenseClass.HeavyVhicles 
                        return _HeavyVhiclesLicenseIssuanceFee;
                }
            }



        }

        public static class InternationalLicense
        {
            // when "= false" the international license expiration date can not be after Local on expiration date //
            public readonly static bool AllowInternationalLicenseExpirationDateToByPassLocalOne = false;

            public readonly static TimeSpan InternationalLicenseYears = new TimeSpan(365, 0, 0, 0);

            public readonly static decimal InternationalLicenseFee = 1;

            // // // // // 
        }

    }

}

namespace General
{
    public static class GeneralFunctions
    {
        public static bool IsValidImage(string Path)
        {
            if (!File.Exists(Path))
                return false;

            try
            {
                using (var Img = Image.FromFile(Path))
                {
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
        // Enums //
namespace General
{

    public enum LicenseClass { Motorcycles = 1, LargeMotorcycles = 2, RegularCar = 3, PublicVehicles = 4, AgriculturalVehicles = 5, Buses = 6, HeavyVhicles = 7 }
    public enum ApplicationType { LicenseIssuance = 1, RetakeTest = 2, RenewDrivingLicense = 3, MissingReplacement = 4, DamagedReplacement = 5, ReleaseLicense = 6, IssuingInternationalLicense = 7 }
    public enum TestType { TheoreticalTest = 0, DrivingTest = 1, EyeTest = 2 };
    public enum Gendor { Femail = 0, Mail = 1 };
    public enum IssueReason { NewIssuance = 1, Renewal = 2, DamagedReplacement = 3, LostReplacement = 4 };
    public enum ApplicationStatus { New = 1, Canceled = 2, Completed = 3 };
}
