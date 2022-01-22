extern alias Core;

using Core::DwC_A;
using DwC_A.Config;
using DwC_A.Generator;
using DwC_A.Interactive.Mapping;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Events;
using Microsoft.DotNet.Interactive.ValueSharing;
using System;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.Threading.Tasks;

namespace DwC_A.Interactive.Commands
{
    internal class DwcaCodegenCommand : Command
    {
        public DwcaCodegenCommand()
            : base("#!dwca-codegen", "Generate strongly typed class files for Darwin Core Archive")
        {
            AddArgument(new Argument<string>()
            {
                Name = "archivePath",
                Description = "Path to archive folder or zip file"
            });

            AddOption(new Option<string>(
                aliases: new[] {"-c", "--configName"},
                description: "Name of configuration variable",
                getDefaultValue: () => ""
            ));

            Handler = CommandHandler.Create<KernelInvocationContext, string, string>((Func<KernelInvocationContext, string, string, Task>)(async (context, archivePath, configName) =>
            {
                var archive = new ArchiveReader(archivePath);

                var csharpKernel = (ISupportGetValue)context.HandlingKernel.FindKernel("csharp");
                if (!csharpKernel.TryGetValue<IGeneratorConfiguration>(configName, out IGeneratorConfiguration config))
                {
                    config = new GeneratorConfigurationBuilder().Build();
                }
                context.Display($"Opening archive {archive.FileName} using configuration", new[] { "text/html" });
                context.Display(config, new[] { "text/html" });

                await GenerateClass(context, archive.CoreFile, config);
                foreach(var extension in archive.Extensions.GetFileReaders())
                {
                    await GenerateClass(context, extension, config);
                }
            }));
        }

        private static async Task GenerateClass(KernelInvocationContext context, 
            IFileReader fileReader, 
            IGeneratorConfiguration config)
        {
            var classGenerator = new ClassGenerator();
            var className = Path.GetFileNameWithoutExtension(fileReader.FileName);
            className = char.ToUpper(className[0]) + className.Substring(1);
            context.Display($"Generating class {className}", new[] { "text/html" });
            var source = classGenerator.GenerateFile(fileReader.FileMetaData, config);
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
