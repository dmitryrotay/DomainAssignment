namespace Domain.Properties.Matching
{
    public interface IAgencyPropertyMatcher
    {
        string AgencyCode { get; set; }

        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}
