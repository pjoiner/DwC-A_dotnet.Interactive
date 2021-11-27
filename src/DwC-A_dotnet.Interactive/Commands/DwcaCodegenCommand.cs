extern alias Core;

using Core::DwC_A;
using DwC_A.Config;
using DwC_A.Generator;
using DwC_A.Interactive.Mapping;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.ValueSharing;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

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

            Handler = CommandHandler.Create<KernelInvocationContext, string, string>(async (context, archivePath, configName) => {
                var archive = new ArchiveReader(archivePath);

                var csharpKernel = (ISupportGetValue)context.HandlingKernel.FindKernel("csharp");
                if (!csharpKernel.TryGetValue<IGeneratorConfiguration>(configName, out IGeneratorConfiguration config))
                {
                    config = new GeneratorConfigurationBuilder().Build();
                }
                context.Display($"Opening archive {archive.FileName} using configuration", new[] { "text/html" });
                context.Display(config, new[] { "text/html" });

                var classGenerator = new ClassGenerator();
                var className = Path.GetFileNameWithoutExtension(archive.CoreFile.FileName);
                className = char.ToUpper(className[0]) + className.Substring(1);
                context.Display($"Generating class {className}", new[] { "text/html" });
                var source = classGenerator.GenerateFile(archive.CoreFile.FileMetaData, config);
                context.Display(source, new[] { "text/plain" });
                var result = await context.HandlingKernel.SubmitCodeAsync(source);
                //TODO: How to get the compilatio result from here
                result.Display(new[] { "text/plain" });
            });
        }
    }
}
