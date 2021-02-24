using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;
using System.Threading.Tasks;
using DwC_A_dotnet.Interactive.Formatters;
using DwC_A;

namespace DwC_A_dotnet.Interactive
{
    public class DwCKernelExtension : IKernelExtension
    {
        public Task OnLoadAsync(Kernel kernel)
        {
            Formatter.Register<DwC_A.Meta.Archive>(ArchiveMetaData.Register, "text/html");
            Formatter.Register<IFileReader>(FileReaderMetaData.Register, "text/html");

            return Task.CompletedTask;
        }
    }
}
