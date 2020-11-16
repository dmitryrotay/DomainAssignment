﻿using Domain.Properties.Matching;
using Moq;
using Xunit;

namespace Domain.Properties.Tests
{
    public class PropertyMatcherTests
    {
        private readonly Property _agencyProperty;
        private readonly Property _databaseProperty;
        private readonly Mock<IAgencyPropertyMatcher> _agencyMatcherMock;
        private readonly Mock<IAgencyPropertyMatcherSelector> _selectorMock;
        private readonly PropertyMatcher _matcher;

        public PropertyMatcherTests()
        {
            _agencyProperty = new Property { AgencyCode = Constants.AgencyPropertyCode };
            _databaseProperty = new Property { AgencyCode = "DATABASE" };

            _agencyMatcherMock = new Mock<IAgencyPropertyMatcher>();
            _agencyMatcherMock.Setup(agencyMatcher => agencyMatcher.IsMatch(It.IsAny<Property>(), It.IsAny<Property>()))
                .Returns(true);
            _selectorMock = new Mock<IAgencyPropertyMatcherSelector>();
            _selectorMock.Setup(selector => selector.GetMatcher(It.IsAny<string>()))
                .Returns(_agencyMatcherMock.Object);

            _matcher = new PropertyMatcher(_selectorMock.Object);
        }

        [Fact]
        public void IsMatch_Calls_Selector_Passing_AgencyProperty_AgencyCode()
        {
            _matcher.IsMatch(_agencyProperty, _databaseProperty);

            _selectorMock.Verify(selector => selector.GetMatcher(Constants.AgencyPropertyCode), Times.Once);
        }

        [Fact]
        public void IsMatch_Calls_AgencyPropertyMatcher_IsMatch_Passing_Properties_In_Correct_Order()
        {
            _matcher.IsMatch(_agencyProperty, _databaseProperty);

            _agencyMatcherMock.Verify(matcher => matcher.IsMatch(_agencyProperty, _databaseProperty), Times.Once);
        }
    }
}
