using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecExpress.Quickstart.Domain.Entities;
using SpecExpress.Quickstart.Domain.Factories;
using SpecExpress.Quickstart.Domain.Values;

namespace SpecExpress.Quickstart.Tests
{
    public class ProviderTestDataFactory
    {
        public static Provider GetBlankProvider()
        {
            return new Provider(0);
        }
        
        public static Provider GetValidProvider()
        {
            return new Provider(100)
                               {  
                                   FirstName = "John",
                                   MiddleInitial = "A",
                                   LastName = "Smith",
                                   Gender = Gender.Male,
                                   ProviderType = ProviderType.Doctor,
                                   Locations = new List<Location>() {GetValidLocation()},
                                   Specialties = GetValidSpecialities(),
                                   Code = 50,
                                   StartDate = new DateTime(2001, 1,1)
                               };
        }

        public static Location GetValidLocation()
        {
            return new Location()
                       {
                           Street1 = "123 Main St.",
                           Street2 = "Suite 100",
                           City = "Anytown",
                           State = new State(1, "TX", "Texas"),
                           ZipCode = "12345",
                           ZipCodePlus = "1234",
                           Website = "www.doctor.com",
                           PhoneNumber = "(123) 456-7890",
                           Schedule = GetValidLocationSchedule()
                       };
        }

        public static List<LocationSchedule> GetValidLocationSchedule()
        {
            return new List<LocationSchedule>()
                       {
                           new LocationSchedule() {Open = 8, Close = 17.5, Day = DayOfWeek.Monday}
                       };
        }

        public static List<Specialty> GetValidSpecialities()
        {
            return SpecialtyFactory.GetSpecialties().Take(2).ToList();
        }
    }
}
