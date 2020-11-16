using System;

namespace Domain.Properties.Matching.Agencies
{
    public class LocationRealEstatePropertyMatcher : IAgencyPropertyMatcher
    {
        public const int MetersInOneDegree = 111000;
        public const int DistanceThresholdMeters = 200;

        public string AgencyCode => "LRE";
        
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            var latitudeDifferenceMeters = (databaseProperty.Latitude - agencyProperty.Latitude) * MetersInOneDegree;
            var longitudeDifferenceMeters = (databaseProperty.Longitude - agencyProperty.Longitude) * MetersInOneDegree;
            var distance = Math.Sqrt(Math.Pow((double) latitudeDifferenceMeters, 2) +
                                     Math.Pow((double) longitudeDifferenceMeters, 2));

            return distance <= DistanceThresholdMeters;
        }
    }
}
