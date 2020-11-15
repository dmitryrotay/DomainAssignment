namespace Domain.Properties.Matching
{
    public interface IAgencyPropertyMatcher
    {
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}
