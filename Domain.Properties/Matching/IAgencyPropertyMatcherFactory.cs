namespace Domain.Properties.Matching
{
    public interface IAgencyPropertyMatcherFactory
    {
        IAgencyPropertyMatcher CreateMatcher(string agencyCode);
    }
}
