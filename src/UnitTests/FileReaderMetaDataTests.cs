using DwC_A;
using DwC_A.Interactive.Formatters;

namespace UnitTests
{
    [UsesVerify]
    public class FileReaderMetaDataTests : IClassFixture<VerifyFixture>
    {
        readonly string whalesArchive = "./Resources/whales";

        [Fact]
        public async Task ShouldPrintFileReaderTable()
        {
            using var archive = new ArchiveReader(whalesArchive);
            using var writer = new StringWriter();
            FileReaderMetaData.Register(archive.CoreFile, writer);
            var actual = writer.ToString();
            await Verify(actual);
        }
    }
}
