using Domain.Properties.Matching.Agencies;
using Xunit;

namespace Domain.Properties.Tests.Agencies
{
    public class OnlyTheBestRealEstatePropertyMatcherTests
    {
        [Fact]
        public void IsMatch_Compares_Name_And_Address_Ignoring_Case()
        {
            var agencyProperty = new Property
            {
                Name = "PROPERTY",
                Address = "ADDRESS 123"
            };

            var databaseProperty = new Property
            {
                Name = "Property",
                Address = "Address 123"
            };

            var matcher = new OnlyTheBestRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Compares_Name_And_Address_Only_By_Whitespaces_Letters_Or_Numbers()
        {
            var agencyProperty = new Property
            {
                Name = "*Super*-High! APARTMENTS (Sydney)",
                Address = "32 Sir John-Young Crescent, Sydney, NSW"
            };

            var databaseProperty = new Property
            {
                Name = "Super High Apartments, Sydney",
                Address = "32 Sir John Young Crescent, Sydney NSW"
            };

            var matcher = new OnlyTheBestRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }
    }
}
