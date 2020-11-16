using Domain.Properties.Matching;
using Moq;
using Xunit;

namespace Domain.Properties.Tests
{
    public class AgencyPropertyMatcherSelectorTests
    {
        [Fact]
        public void GetMatcher_Selects_Matcher_By_Code_From_Multiple_Matchers()
        {
            var matcher1 = new Mock<IAgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{Constants.AgencyPropertyCode}_1");
            var matcher2 = new Mock<IAgencyPropertyMatcher>();
            matcher2.SetupGet(matcher => matcher.AgencyCode).Returns(Constants.AgencyPropertyCode);
            var matcher3 = new Mock<IAgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{Constants.AgencyPropertyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new [] { matcher1.Object, matcher2.Object, matcher3.Object });

            Assert.Equal(matcherSelector.GetMatcher(Constants.AgencyPropertyCode), matcher2.Object);
        }

        [Fact]
        public void GetMatcher_Throws_PropertyMatchException_On_Duplicate_Matcher()
        {
            var matcherMock = new Mock<IAgencyPropertyMatcher>();
            matcherMock.SetupGet(matcher => matcher.AgencyCode).Returns(Constants.AgencyPropertyCode);

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcherMock.Object, matcherMock.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(Constants.AgencyPropertyCode));
        }

        [Fact]
        public void GetMatcher_Throws_PropertyMatchException_On_Missing_Matcher()
        {
            var matcher1 = new Mock<IAgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{Constants.AgencyPropertyCode}_1");
            var matcher3 = new Mock<IAgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{Constants.AgencyPropertyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcher1.Object, matcher3.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(Constants.AgencyPropertyCode));
        }
    }
}
