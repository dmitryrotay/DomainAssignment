﻿using Domain.Properties.Matching.Agencies;
using Xunit;

namespace Domain.Properties.Tests.Agencies
{
    public class ContraryRealEstatePropertyMatcherTests
    {
        private const string PropertyName = "The Summit Apartments";
        private const string ReversedPropertyName = "Apartments Summit The";

        [Fact]
        public void IsMatch_Returns_False_When_Property_Names_Are_Equal()
        {
            var agencyProperty = new Property
            {
                Name = PropertyName
            };

            var databaseProperty = new Property
            {
                Name = PropertyName
            };

            var matcher = new ContraryRealEstatePropertyMatcher();

            Assert.False(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Returns_True_When_Property_Names_Match_If_Words_Are_Reversed()
        {
            var agencyProperty = new Property
            {
                Name = PropertyName
            };

            var databaseProperty = new Property
            {
                Name = ReversedPropertyName
            };

            var matcher = new ContraryRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }

        [Fact]
        public void IsMatch_Compares_Property_Names_Ignoring_Case()
        {
            var agencyProperty = new Property
            {
                Name = PropertyName.ToUpper()
            };

            var databaseProperty = new Property
            {
                Name = ReversedPropertyName.ToLower()
            };

            var matcher = new ContraryRealEstatePropertyMatcher();

            Assert.True(matcher.IsMatch(agencyProperty, databaseProperty));
        }
    }
}
