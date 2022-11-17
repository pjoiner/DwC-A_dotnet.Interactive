using Microsoft.DotNet.Interactive;
using System;
using System.CommandLine;

namespace DwC_A.Interactive.Commands
{
    internal class TermsCommand : Command
    {
        public TermsCommand() : base("#!terms", "Display Darwin Core standard terms")
        {
            System.CommandLine.Handler.SetHandler(this, (invocationContext) =>
            {
                var defaultTerms = new DefaultTerms();
                KernelInvocationContext.Current.Display(defaultTerms);
            });
        }
    }
}
