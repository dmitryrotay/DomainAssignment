using System;
using System.Linq;

namespace Domain.Properties.Matching.Agencies
{
    public class ContraryRealEstatePropertyMatcher : IAgencyPropertyMatcher
    {
        public string AgencyCode => "CRE";

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return string.Equals(GetStringWithReverseWordOrder(agencyProperty.Name), databaseProperty.Name,
                StringComparison.OrdinalIgnoreCase);
        }

        private string GetStringWithReverseWordOrder(string input)
        {
            var words = input.Split().Reverse();
            return string.Join(" ", words);
        }
    }
}
