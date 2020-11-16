namespace Domain.Properties.Matching
{
    public interface IAgencyPropertyMatcherSelector
    {
        IAgencyPropertyMatcher GetMatcher(string agencyCode);
    }
}
