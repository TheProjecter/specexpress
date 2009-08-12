using System;
using NUnit.Framework;
using SpecExpress.Rules.DateValidators;
using SpecExpress.Test.Entities;
using SpecExpress.Rules;

namespace SpecExpress.Test.RuleValidatorTests.DateTimeTests
{
    [TestFixture]
    public class DateTimeValidatorTests : SpecificationBase<CalendarEvent>
    {
        [TestCase("1/1/2009", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueIsBefore")]
        [TestCase("1/1/2010", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueIsAfter")]
        [TestCase("12/1/2009", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueEquals")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 PM", Result = false, TestName = "DateTimePropertyValueIsBefore")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 AM", Result = true, TestName = "DateTimePropertyValueIsAfter")]
        [TestCase("12/1/2009 1:00 AM", "12/1/2009 1:00 AM", Result = false, TestName = "DateTimePropertyValueEquals")]
        public bool GreaterThan_IsValid(string propertyValue, string end)
        {
            DateTime propertyValueDateTime = DateTime.Parse(propertyValue);
            DateTime endDateTime = DateTime.Parse(end);

            //Create Validator
            var validator = new GreaterThan<CalendarEvent>(endDateTime);
            // Build context for CalendarEvent containing a StartDate of propertyValue.
            RuleValidatorContext<CalendarEvent, DateTime> context = BuildContextForCalendarEventEndDate("Test Event", DateTime.Now, propertyValueDateTime);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("1/1/2009", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueIsBefore")]
        [TestCase("1/1/2010", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueIsAfter")]
        [TestCase("12/1/2009", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueEquals")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 PM", Result = false, TestName = "DateTimePropertyValueIsBefore")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 AM", Result = true, TestName = "DateTimePropertyValueIsAfter")]
        [TestCase("12/1/2009 1:00 AM", "12/1/2009 1:00 AM", Result = true, TestName = "DateTimePropertyValueEquals")]
        public bool GreaterThanEqualTo_IsValid(string propertyValue, string end)
        {
            DateTime propertyValueDateTime = DateTime.Parse(propertyValue);
            DateTime endDateTime = DateTime.Parse(end);

            //Create Validator
            var validator = new GreaterThanEqualTo<CalendarEvent>(endDateTime);
            // Build context for CalendarEvent containing a StartDate of propertyValue.
            RuleValidatorContext<CalendarEvent, DateTime> context = BuildContextForCalendarEventEndDate("Test Event", DateTime.Now, propertyValueDateTime);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("1/1/2009", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueIsBefore")]
        [TestCase("1/1/2010", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueIsAfter")]
        [TestCase("12/1/2009", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueEquals")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 PM", Result = true, TestName = "DateTimePropertyValueIsBefore")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 AM", Result = false, TestName = "DateTimePropertyValueIsAfter")]
        [TestCase("12/1/2009 1:00 AM", "12/1/2009 1:00 AM", Result = false, TestName = "DateTimePropertyValueEquals")]
        public bool LessThan_IsValid(string propertyValue, string before)
        {
            DateTime propertyValueDateTime = DateTime.Parse(propertyValue);
            DateTime beforeDateTime = DateTime.Parse(before);

            //Create Validator
            var validator = new LessThan<CalendarEvent>(beforeDateTime);
            // Build context for CalendarEvent containing a StartDate of propertyValue.
            RuleValidatorContext<CalendarEvent, DateTime> context = BuildContextForCalendarEventStartDate("Test Event", propertyValueDateTime, DateTime.Now);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        [TestCase("1/1/2009", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueIsBefore")]
        [TestCase("1/1/2010", "12/1/2009", Result = false, TestName = "DateOnlyPropertyValueIsAfter")]
        [TestCase("12/1/2009", "12/1/2009", Result = true, TestName = "DateOnlyPropertyValueEquals")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 PM", Result = true, TestName = "DateTimePropertyValueIsBefore")]
        [TestCase("12/1/2009 9:00 AM", "12/1/2009 1:00 AM", Result = false, TestName = "DateTimePropertyValueIsAfter")]
        [TestCase("12/1/2009 1:00 AM", "12/1/2009 1:00 AM", Result = true, TestName = "DateTimePropertyValueEquals")]
        public bool LessThanEqualTo_IsValid(string propertyValue, string before)
        {
            DateTime propertyValueDateTime = DateTime.Parse(propertyValue);
            DateTime beforeDateTime = DateTime.Parse(before);

            //Create Validator
            var validator = new LessThanEqualTo<CalendarEvent>(beforeDateTime);
            // Build context for CalendarEvent containing a StartDate of propertyValue.
            RuleValidatorContext<CalendarEvent, DateTime> context = BuildContextForCalendarEventStartDate("Test Event", propertyValueDateTime, DateTime.Now);

            //Validate the validator only, return true of no error returned
            return validator.Validate(context) == null;
        }

        public RuleValidatorContext<CalendarEvent, System.DateTime> BuildContextForCalendarEventStartDate(string subject, DateTime startDate, DateTime endDate)
        {
            var calendarEvent = new CalendarEvent() {Subject = subject, StartDate = startDate, EndDate = endDate};
            var context = new RuleValidatorContext<CalendarEvent,DateTime>(calendarEvent, "StartDate", calendarEvent.StartDate, null, null);

            return context;
        }

        public RuleValidatorContext<CalendarEvent, System.DateTime> BuildContextForCalendarEventEndDate(string subject, DateTime startDate, DateTime endDate)
        {
            var calendarEvent = new CalendarEvent() { Subject = subject, StartDate = startDate, EndDate = endDate };
            var context = new RuleValidatorContext<CalendarEvent, DateTime>(calendarEvent, "EndDate", calendarEvent.EndDate, null, null);

            return context;
        }

    }
}