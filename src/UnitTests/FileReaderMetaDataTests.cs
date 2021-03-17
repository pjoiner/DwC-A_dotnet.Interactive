using DwC_A;
using DwC_A.Interactive.Formatters;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class FileReaderMetaDataTests
    {
        string whalesArchive = "./Resources/whales";

        [Fact]
        public void ShouldPrintFileReaderTable()
        {
            var expected = File.ReadAllText("./Resources/html/filereader.html");
            using (var archive = new ArchiveReader(whalesArchive))
            {
                using (var writer = new StringWriter())
                {
                    FileReaderMetaData.Register(archive.CoreFile, writer);
                    var actual = writer.ToString();
                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}
