using DwC_A;
using DwC_A.Interactive.Extensions;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class FileReaderExtensionTests
    {
        [Fact]
        public void ShouldNotThrowOnDuplicates()
        {
            using(var archive = new ArchiveReader("./Resources/whales"))
            {
                var dynCore = archive.CoreFile.ToDynamic();
                Assert.NotNull(dynCore.First().language1);
            }
        }
    }
}
