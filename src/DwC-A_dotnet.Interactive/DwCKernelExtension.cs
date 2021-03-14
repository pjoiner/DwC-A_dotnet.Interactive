extern alias Core;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;
using System.Threading.Tasks;
using Core.DwC_A;
using Core.DwC_A.Meta;
using DwC_A.Interactive.Formatters;

namespace DwC_A.Interactive
{
    public class DwCKernelExtension : IKernelExtension
    {
        public Task OnLoadAsync(Kernel kernel)
        {
            Formatter.Register<Archive>(ArchiveMetaData.Register, "text/html");
            Formatter.Register<IFileReader>(FileReaderMetaData.Register, "text/html");
            Formatter.Register<DefaultTerms>(TermsFormatter.Register, "text/html");

            return Task.CompletedTask;
        }
    }
}
