using DwC_A.Interactive.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
