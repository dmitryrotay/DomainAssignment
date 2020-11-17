using System;

namespace Domain.Properties.Matching.Agencies
{
    public class LocationRealEstatePropertyMatcher : IAgencyPropertyMatcher
    {
        public const int MetersInOneDegree = 111000;
        public const int DistanceThresholdMeters = 200;
        public const string LreAgencyCode = "LRE";

        public string AgencyCode => LreAgencyCode;

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            if (!string.Equals(databaseProperty.AgencyCode, AgencyCode, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var latitudeDifferenceMeters = (databaseProperty.Latitude - agencyProperty.Latitude) * MetersInOneDegree;
            var longitudeDifferenceMeters = (databaseProperty.Longitude - agencyProperty.Longitude) * MetersInOneDegree;
            var distance = Math.Sqrt(Math.Pow((double)latitudeDifferenceMeters, 2) +
                                     Math.Pow((double)longitudeDifferenceMeters, 2));

            return distance <= DistanceThresholdMeters;
        }
    }
}
