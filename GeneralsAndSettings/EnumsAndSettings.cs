using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class SettingsClass
    {
        public static class Application
        {
            public readonly static decimal BaseApplicationFees = 5;

            private readonly static decimal _DamageRepalcementAdditionalApplicationFee = 20;
            private readonly static decimal _IssuingInternationalLicenseAdditionalApplicationFee = 20;
            private readonly static decimal _MissingReplacementAdditionalApplicationFee = 20;
            private readonly static decimal _RenewDrivingLicenseAdditionalApplicationFee = 10;
            private readonly static decimal _RetakeTestAdditionalApplicationFee = 0;

            public static decimal? GetLocalLicenseIssuingFees (ApplicationType Type)
            {
                switch (Type)
                {
                    case ApplicationType.DamagedReplacement:
                        return _DamageRepalcementAdditionalApplicationFee;
                    case ApplicationType.IssuingInternationalLicense:
                        return _IssuingInternationalLicenseAdditionalApplicationFee;
                    case ApplicationType.MissingReplacement:
                        return _MissingReplacementAdditionalApplicationFee;
                    case ApplicationType.RenewDrivingLicense:
                        return _RenewDrivingLicenseAdditionalApplicationFee;
                    case ApplicationType.RetakeTest:
                        return _RetakeTestAdditionalApplicationFee;
                    default:
                        return null;
                }
            }

            public readonly static TimeSpan ApplicationExpirationPeriod = new TimeSpan(180, 0, 0, 0);
        }

        public static class Test
        {
            public readonly static decimal EyeTestFees = 30;
            public readonly static decimal DrivingTestFees = 30;
            public readonly static decimal TheoreticalTestFees = 30;

            // // Expiry period // ( in Days )

            public readonly static TimeSpan MaximumPeriodToSetTestResulte = new TimeSpan(7,0,0,0,0);
            public readonly static TimeSpan EyeTestExpirationPeriod = new TimeSpan(180,0,0,0);
            public readonly static TimeSpan DrivingTestExpirationPeriod = new TimeSpan(180, 0, 0, 0);
            public readonly static TimeSpan TheoreticalTestExpirationPeriod = new TimeSpan(180, 0, 0, 0);
        }

        public static class License
        {
            private readonly static int _MotorcyclesLicenseYears = 1;
            private readonly static int _LargeMotorcyclesLicenseYears = 1;
            private readonly static int _RegularCarLicenseYears = 1;
            private readonly static int _PublicVehiclesLicenseYears = 1;
            private readonly static int _AgriculturalVehiclesLicenseYears = 1;
            private readonly static int _BusesLicenseYears = 1;
            private readonly static int _HeavyVhiclesLicenseYears = 1;

            public static int HowManyYears(LicenseClass Class)
            {
                switch (Class)
                {
                    case LicenseClass.Motorcycles:
                        return MotorcyclesLicenseYears;
                    case LicenseClass.LargeMotorcycles:
                        return LargeMotorcyclesLicenseYears;
                    case LicenseClass.RegularCar:
                        return RegularCarLicenseYears;
                    case LicenseClass.PublicVehicles:
                        return PublicVehiclesLicenseYears;
                    case LicenseClass.AgriculturalVehicles:
                        return AgriculturalVehiclesLicenseYears;
                    case LicenseClass.Buses:
                        return BusesLicenseYears;
                    default: // LicenseClass.HeavyVhicles 
                        return HeavyVhiclesLicenseYears;
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
            public readonly static bool _AllowInternationalLicenseExpirationDateToByPassLocalOne = false;

            public readonly static int InternationalLicenseYears = 1;

            public readonly static decimal InternationalLicenseFee = 1;

            // // // // // 
        }

    }

}

namespace General
{

    public enum LicenseClass { Motorcycles = 1, LargeMotorcycles = 2, RegularCar = 3, PublicVehicles = 4, AgriculturalVehicles = 5, Buses = 6, HeavyVhicles = 7 }
    public enum ApplicationType { LicenseIssuance = 1, RetakeTest = 2, RenewDrivingLicense = 3, MissingReplacement = 4, DamagedReplacement = 5, ReleaseLicense = 6, IssuingInternationalLicense = 7 }
    public enum TestType { TheoreticalTest = 0, DrivingTest = 1, EyeTest = 2 };
    public enum Gendor { Femail = 0, Mail = 1 };
    public enum IssueReason { NewIssuance = 1, Renewal = 2, DamagedReplacement = 3, LostReplacement = 4 };
    public enum ApplicationStatus { New = 1, Canceled = 2, Completed = 3 };
}
