using Microsoft.DotNet.Interactive;
using System;
using System.CommandLine;

namespace DwC_A.Interactive.Commands
{
    internal static class TermsCommandFactory
    {
        public static Command Create()
        {
            var termsCmd = new Command("#!terms", "Display Darwin Core standard terms");
            termsCmd.SetHandler((KernelInvocationContext invocationContext) =>
            {
                var defaultTerms = new DefaultTerms();
                invocationContext.Display(defaultTerms);
            });
            return termsCmd;
        }
    }
}
