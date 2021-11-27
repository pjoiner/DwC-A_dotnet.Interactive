using Microsoft.DotNet.Interactive;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace DwC_A.Interactive.Commands
{
    internal class TermsCommand : Command
    {
        public TermsCommand() : base("#!terms", "Display Darwin Core standard terms")
        {
            Handler = CommandHandler.Create((KernelInvocationContext invocationContext) =>
            {
                var defaultTerms = new DefaultTerms();
                invocationContext.Display(defaultTerms);
            });
        }
    }
}
