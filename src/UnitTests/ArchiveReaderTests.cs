using DwC_A;
using DwC_A.Interactive.Formatters;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace UnitTests
{
    [UsesVerify]
    public class ArchiveReaderTests : IClassFixture<VerifyFixture>
    {
        readonly string whalesArchive = "./Resources/whales";

        [Fact]
        public async Task ShouldPrintArchiveMetaDataTable()
        {
            using var archive = new ArchiveReader(whalesArchive);
            using var writer = new StringWriter();
            ArchiveMetaData.Register(archive.MetaData, writer);
            var actual = writer.ToString();
            await Verify(actual);
        }
    }
}
