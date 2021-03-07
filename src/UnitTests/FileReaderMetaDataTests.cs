using DwC_A;
using DwC_A.Interactive.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class FileReaderMetaDataTests
    {
        string whalesArchive = "./Resources/whales";

        [Fact]
        public void ShouldPrintFileReaderTable()
        {
            var expected = "<b>FileName</b>: taxon.txt<br/><b>RowType</b>: http://rs.tdwg.org/dwc/terms/Taxon<table><thead><tr><td>Index</td><td>Name</td><td>Term</td><td>Vocabulary</td></tr></thead><tbody><tr><td><div class=\"dni-plaintext\">0</div></td><td>taxonID</td><td>http://rs.tdwg.org/dwc/terms/taxonID</td><td><div class=\"dni-plaintext\">&lt;null&gt;</div></td></tr><tr><td><div class=\"dni-plaintext\">1</div></td><td>vernacularName</td><td>http://rs.tdwg.org/dwc/terms/vernacularName</td><td><div class=\"dni-plaintext\">&lt;null&gt;</div></td></tr><tr><td><div class=\"dni-plaintext\">2</div></td><td>language</td><td>http://purl.org/dc/terms/language</td><td><div class=\"dni-plaintext\">&lt;null&gt;</div></td></tr><tr><td><div class=\"dni-plaintext\">3</div></td><td>language</td><td>http://example.com/language</td><td><div class=\"dni-plaintext\">&lt;null&gt;</div></td></tr></tbody></table>";
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
