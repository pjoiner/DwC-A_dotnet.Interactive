extern alias Core;

using Core::DwC_A;
using DwC_A.Config;
using DwC_A.Generator;
using DwC_A.Interactive.Extensions;
using DwC_A.Interactive.Mapping;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Events;
using System;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

namespace DwC_A.Interactive.Commands
{
    internal class DwcaCodegenCommand : Command
    {
        public DwcaCodegenCommand()
            : base("#!dwca-codegen", "Generate strongly typed class files for Darwin Core Archive")
        {
            var archivePathArg = new Argument<string>()
            {
                Name = "archivePath",
                Description = "Path to archive folder or zip file"
            };
            AddArgument(archivePathArg);

            var cfgOption = new Option<string>(
                aliases: new[] { "-c", "--configName" },
                description: "Name of configuration variable",
                getDefaultValue: () => ""
            );
            AddOption(cfgOption);

            System.CommandLine.Handler.SetHandler(this,async (context) =>
            {
                var archivePath = context.ParseResult.GetValueForArgument(archivePathArg);
                var configName = context.ParseResult.GetValueForOption(cfgOption);
                var archive = new ArchiveReader(archivePath);

                var csharpKernel = KernelInvocationContext.Current.HandlingKernel.FindKernelByName("csharp");
                var (success, value) = await csharpKernel.TryRequestValueAsync(configName);
                IGeneratorConfiguration config = success ? 
                    value.Value as IGeneratorConfiguration : 
                    null ?? 
                    new GeneratorConfigurationBuilder().Build();
                KernelInvocationContext.Current.Display($"Opening archive {archive.FileName} using configuration", new[] { "text/html" });
                KernelInvocationContext.Current.Display(config, new[] { "text/html" });

                await GenerateClass(KernelInvocationContext.Current, archive.CoreFile, config);
                foreach (var extension in archive.Extensions.GetFileReaders())
                {
                    await GenerateClass(KernelInvocationContext.Current, extension, config);
                }
            });
        }

        private static async Task GenerateClass(KernelInvocationContext context, 
            IFileReader fileReader, 
            IGeneratorConfiguration config)
        {
            var className = Path.GetFileNameWithoutExtension(fileReader.FileName);
            className = char.ToUpper(className[0]) + className[1..];
            context.Display($"Generating class {className}", new[] { "text/html" });
            var source = new ClassGenerator()
                .GenerateFile(fileReader.FileMetaData, config);
            var result = await context.HandlingKernel.SubmitCodeAsync(source);
            result.KernelEvents.Subscribe((ev) => { }, (ex) =>
            {
                context.Display(ex.Message, new[] { "text/plain" });
            });
            result.KernelEvents.Subscribe((ev) => 
            {
                if(ev is ErrorProduced error)
                {
                    context.Fail(context.Command, null, error.Message);
                }
                if (ev is CommandFailed failure)
                {
                    context.Fail(context.Command, null, failure.Message);
                }
            });
        }
    }
}
