using Domain.Properties.Matching;
using Moq;
using Xunit;

namespace Domain.Properties.Tests
{
    public class AgencyPropertyMatcherSelectorTests
    {
        private const string AgencyCode = "AGENCY";

        [Fact]
        public void GetMatcher_Selects_Matcher_By_Code_From_Multiple_Matchers()
        {
            var matcher1 = new Mock<AgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_1");
            var matcher2 = new Mock<AgencyPropertyMatcher>();
            matcher2.SetupGet(matcher => matcher.AgencyCode).Returns(AgencyCode);
            var matcher3 = new Mock<AgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new [] { matcher1.Object, matcher2.Object, matcher3.Object });

            Assert.Equal(matcherSelector.GetMatcher(AgencyCode), matcher2.Object);
        }

        [Fact]
        public void GetMatcher_Throws_PropertyMatchException_On_Duplicate_Matcher()
        {
            var matcherMock = new Mock<AgencyPropertyMatcher>();
            matcherMock.SetupGet(matcher => matcher.AgencyCode).Returns(AgencyCode);

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcherMock.Object, matcherMock.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(AgencyCode));
        }

        [Fact]
        public void GetMatcher_Throws_PropertyMatchException_On_Missing_Matcher()
        {
            var matcher1 = new Mock<AgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_1");
            var matcher3 = new Mock<AgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcher1.Object, matcher3.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(AgencyCode));
        }
    }
}
