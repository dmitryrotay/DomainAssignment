using Domain.Properties.Matching;
using Moq;
using Xunit;

namespace Domain.Properties.Tests
{
    public class AgencyPropertyMatcherSelectorTest
    {
        private const string AgencyCode = "AGENCY";

        [Fact]
        public void Select_Matcher_By_Code_From_Multiple_Matchers()
        {
            var matcher1 = new Mock<IAgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_1");
            var matcher2 = new Mock<IAgencyPropertyMatcher>();
            matcher2.SetupGet(matcher => matcher.AgencyCode).Returns(AgencyCode);
            var matcher3 = new Mock<IAgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new [] { matcher1.Object, matcher2.Object, matcher3.Object });

            Assert.Equal(matcherSelector.GetMatcher(AgencyCode), matcher2.Object);
        }

        [Fact]
        public void Throws_PropertyMatchException_On_Duplicate_Matcher()
        {
            var matcherMock = new Mock<IAgencyPropertyMatcher>();
            matcherMock.SetupGet(matcher => matcher.AgencyCode).Returns(AgencyCode);

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcherMock.Object, matcherMock.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(AgencyCode));
        }

        [Fact]
        public void Throws_PropertyMatchException_On_Missing_Matcher()
        {
            var matcher1 = new Mock<IAgencyPropertyMatcher>();
            matcher1.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_1");
            var matcher3 = new Mock<IAgencyPropertyMatcher>();
            matcher3.SetupGet(matcher => matcher.AgencyCode).Returns($"{AgencyCode}_3");

            var matcherSelector = new AgencyPropertyMatcherSelector(new[] { matcher1.Object, matcher3.Object });

            Assert.Throws<PropertyMatchException>(() => matcherSelector.GetMatcher(AgencyCode));
        }
    }
}
