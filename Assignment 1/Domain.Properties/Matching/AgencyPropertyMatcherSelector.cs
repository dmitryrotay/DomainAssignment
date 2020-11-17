using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Properties.Matching
{
    public class AgencyPropertyMatcherSelector : IAgencyPropertyMatcherSelector
    {
        private readonly IEnumerable<IAgencyPropertyMatcher> _propertyMatchers;
        
        public AgencyPropertyMatcherSelector(IEnumerable<IAgencyPropertyMatcher> propertyMatchers)
        {
            _propertyMatchers = propertyMatchers;
        }

        public IAgencyPropertyMatcher GetMatcher(string agencyCode)
        {
            try
            {
                return _propertyMatchers.Single(propertyMatcher => propertyMatcher.AgencyCode == agencyCode);
            }
            catch (InvalidOperationException e)
            {
                var message = $"Unable to find a matcher for agency with code '{agencyCode}'.\n" +
                              "Check that you have correctly registered a single matcher service for the agency.";
                throw new PropertyMatchException(message, e);
            }
        }
    }
}
