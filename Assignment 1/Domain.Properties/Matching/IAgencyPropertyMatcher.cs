namespace Domain.Properties.Matching
{
    public interface IAgencyPropertyMatcher
    {
        string AgencyCode { get; }
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}