using System;
using Domain.Properties.Matching.Agencies;
using Xunit;

namespace Domain.Properties.Tests.Agencies
{
    public class LocationRealEstatePropertyMatcherTests
    {
        private const decimal BaseLatitude = -33.8684401m;
        private const decimal BaseLongitude = 151.192646m;

        private const decimal DegreesInThresholdDistance = (decimal)1 /
                                                           LocationRealEstatePropertyMatcher.MetersInOneDegree *
                                                           LocationRealEstatePropertyMatcher.DistanceThresholdMeters;

        [Fact]
        public void IsMatch_Returns_False_If_Coordinates_Equal_And_Agency_Codes_Dont_Match()
        {
            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = Constants.AgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.False(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_True_If_Only_Latitude_Changes_And_Latitude_Within_Threshold()
        {
            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude + DegreesInThresholdDistance,
                Longitude = BaseLongitude
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_False_If_Only_Latitude_Changes_And_Latitude_Not_Within_Threshold()
        {
            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude + DegreesInThresholdDistance + 1,
                Longitude = BaseLongitude
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.False(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_True_If_Only_Longitude_Changes_And_Latitude_Within_Threshold()
        {
            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude + DegreesInThresholdDistance
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_False_If_Only_Longitude_Changes_And_Latitude_Not_Within_Threshold()
        {
            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude + DegreesInThresholdDistance + 1
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.False(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_True_If_Both_Coordinates_Change_And_Distance_Within_Threshold()
        {
            decimal squareSideFromDiagonal = DegreesInThresholdDistance / (decimal)Math.Sqrt(2);

            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude + squareSideFromDiagonal,
                Longitude = BaseLongitude + squareSideFromDiagonal
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_False_If_Both_Coordinates_Change_And_Distance_Not_Within_Threshold()
        {
            decimal squareSideFromDiagonal = (DegreesInThresholdDistance + 1) / (decimal)Math.Sqrt(2);

            var agencyProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude,
                Longitude = BaseLongitude
            };

            var databaseProperty = new Property
            {
                AgencyCode = LocationRealEstatePropertyMatcher.LreAgencyCode,
                Latitude = BaseLatitude + squareSideFromDiagonal,
                Longitude = BaseLongitude + squareSideFromDiagonal
            };

            var matcher = new LocationRealEstatePropertyMatcher();

            Assert.False(matcher.IsMatch(agencyProperty, databaseProperty));
        }
    }
}
