using System;
using System.Linq;

namespace Domain.Properties.Matching.Agencies
{
    public class OnlyTheBestRealEstatePropertyMatcher : IAgencyPropertyMatcher
    {
        public string AgencyCode => "OTBRE";

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            return EqualWithoutPunctuation(agencyProperty.Address, databaseProperty.Address) &&
                   EqualWithoutPunctuation(agencyProperty.Name, databaseProperty.Name);
        }

        private bool EqualWithoutPunctuation(string string1, string string2)
        {
            return string.Equals(StripPunctuation(string1), StripPunctuation(string2), StringComparison.OrdinalIgnoreCase);
        }

        private string StripPunctuation(string input)
        {
            var chars = input.Where(char.IsLetterOrDigit).Select(ch => ch).ToArray();

            return new string(chars);
        }
    }
}
