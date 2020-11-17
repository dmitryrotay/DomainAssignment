using System;

namespace Domain.Properties.Matching
{
    public abstract class AgencyPropertyMatcher : IAgencyPropertyMatcher
    {
        public abstract string AgencyCode { get; }
        protected abstract bool IsAgencySpecificMatch(Property agencyProperty, Property databaseProperty);

        protected bool IsSameAgencyProperty(Property agencyProperty, Property databaseProperty)
        {
            return string.Equals(agencyProperty.AgencyCode, databaseProperty.AgencyCode, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return IsSameAgencyProperty(agencyProperty, databaseProperty) &&
                   IsAgencySpecificMatch(agencyProperty, databaseProperty);
        }
    }
}
