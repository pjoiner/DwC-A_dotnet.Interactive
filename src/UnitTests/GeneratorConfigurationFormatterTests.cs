using DwC_A.Interactive.Formatters;
using DwC_A.Interactive.Mapping;
using DwC_A.Terms;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class GeneratorConfigurationFormatterTests
    {
        [Fact]
        public void ShouldFormatHeader()
        {
            var config = new GeneratorConfigurationBuilder()
                .AddProperty(Terms.decimalLatitude, "double", true, "Latitude")
                .Build();
            using var tw = new StringWriter();

            GeneratorConfigFormatter.Register(config, tw);

            var html = tw.ToString();
            Assert.NotNull(html);
        }
    }
}
