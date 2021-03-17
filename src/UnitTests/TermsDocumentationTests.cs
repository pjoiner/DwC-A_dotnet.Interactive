using DwC_A.Interactive.Formatters;
using Xunit;

namespace UnitTests
{
    public class TermsDocumentationTests
    {
        [Fact]
        public void ShouldLoadTermsDescriptions()
        {
            var dictionary = TermsFormatter.LoadXmlDocumentation();
            Assert.NotEmpty(dictionary);
        }
    }
}
