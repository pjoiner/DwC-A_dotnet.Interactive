extern alias Core;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;
using System.Threading.Tasks;
using Core.DwC_A;
using Core.DwC_A.Meta;
using DwC_A.Interactive.Formatters;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Collections.Generic;

namespace DwC_A.Interactive
{
    public class DwCKernelExtension : IKernelExtension
    {
        public Task OnLoadAsync(Kernel kernel)
        {
            Formatter.Register<ArchiveReader>(ArchiveMetaData.RegisterForArchiveReader, "text/html");
            Formatter.Register<Archive>(ArchiveMetaData.Register, "text/html");
            Formatter.Register<IFileReader>(FileReaderMetaData.Register, "text/html");
            Formatter.Register<DefaultTerms>(TermsFormatter.Register, "text/html");
            Formatter.Register<IEnumerable<IRow>>(RowFormatter.Register, "text/html");

            var termsCommand = new Command("#!terms", "Display Darwin Core standard terms")
            {
                Handler = CommandHandler.Create((KernelInvocationContext invocationContext) =>
                {
                    var defaultTerms = new DefaultTerms();
                    invocationContext.Display(defaultTerms);
                })
            };

            kernel.AddDirective(termsCommand);

            return Task.CompletedTask;
        }
    }
}
