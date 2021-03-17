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
            var expected = File.ReadAllText("./Resources/html/archive.html");
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
