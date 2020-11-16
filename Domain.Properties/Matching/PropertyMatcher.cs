namespace Domain.Properties.Matching
{
    public class PropertyMatcher : IPropertyMatcher
    {
        private readonly IAgencyPropertyMatcherSelector _propertyMatcherSelector;

        public PropertyMatcher(IAgencyPropertyMatcherSelector propertyMatcherSelector)
        {
            _propertyMatcherSelector = propertyMatcherSelector;
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            var matcher = _propertyMatcherSelector.GetMatcher(agencyProperty.AgencyCode);

            return matcher.IsMatch(agencyProperty, databaseProperty);
        }
    }
}
