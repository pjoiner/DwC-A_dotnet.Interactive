using Xunit;
using DwC_A;
using DwC_A.Interactive.Formatters;
using System.IO;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace UnitTests
{
    public class ArchiveReaderTests
    {
        string whalesArchive = "./Resources/whales";

        [Fact]
        public void ShouldPrintArchiveMetaDataTable()
        {
            var expected = "<table><thead><tr><td>File Type</td><td>File Name</td><td>Row Type</td></tr></thead><tbody><tr><td><b>CoreFile:</b></td><td>taxon.txt</td><td>http://rs.tdwg.org/dwc/terms/Taxon</td></tr><tr><td><b>Metadata:</b></td><td>eml.xml</td></tr></tbody></table>";
            using (var archive = new ArchiveReader(whalesArchive))
            {
                using(var writer = new StringWriter())
                {
                    ArchiveMetaData.Register(archive.MetaData, writer);
                    var actual = writer.ToString();
                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}
