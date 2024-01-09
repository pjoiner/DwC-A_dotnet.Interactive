using DwC_A.Config;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Interactive.Mapping
{
    public class GeneratorConfigurationBuilder
    {
        private const string ExtensionNamespace = "DwC_A.Extensions";
        private const string SystemNamespace = "System";
        private const string CoreNamespace = "DwC_A";
        private const string AllTerms = "*";

        internal class GeneratorConfiguration : IGeneratorConfiguration
        {
            private readonly Dictionary<string, PropertyConfiguration> properties = new()
            {
                { AllTerms, new PropertyConfiguration() }
            };
            private readonly HashSet<string> usings = new(new[] { SystemNamespace });
            public string Namespace { get; set; } = "";
            public string Output { get; set; } = "";
            public bool PascalCase { get; set; } = true;
            public bool MapMethod { get; set; } = true;
            public IDictionary<string, PropertyConfiguration> Properties => properties;
            public TermAttributeType TermAttribute { get; set; } = TermAttributeType.none;
            public IList<string> Usings => usings.ToList();
            public PropertyConfiguration GetPropertyConfiguration(string term)
            {
                return Properties.TryGetValue(term, out PropertyConfiguration value) ? value :
                    Properties[AllTerms];
            }
            internal void AddUsing(string namespaceName)
            {
                usings.Add(namespaceName);
            }
            internal void AddProperty(string term, string typeName, bool include = true, string propertyName = null)
            {
                properties.Remove(term);
                properties.Add(term, new PropertyConfiguration()
                {
                    TypeName = typeName,
                    PropertyName = propertyName,
                    Include = include
                });
            }
        }

        private readonly GeneratorConfiguration config = new();

        public GeneratorConfigurationBuilder WithNamespace(string namespaceName)
        {
            config.Namespace = namespaceName;
            return this;
        }

        public GeneratorConfigurationBuilder WithPascalCase(bool pascalCase)
        {
            config.PascalCase = pascalCase;
            return this;
        }

        public GeneratorConfigurationBuilder WithTermAttribute(TermAttributeType termAttribute)
        {
            config.TermAttribute = termAttribute;
            return this;
        }

        public GeneratorConfigurationBuilder AddUsing(string usingNamespace)
        {
            config.AddUsing(usingNamespace);
            return this;
        }

        public GeneratorConfigurationBuilder WithOutput(string output)
        {
            config.Output = output;
            return this;
        }

        public GeneratorConfigurationBuilder WithMapMethod(bool mapMethod)
        {
            config.MapMethod = mapMethod;
            if (mapMethod)
            {
                config.AddUsing(CoreNamespace);
                config.AddUsing(ExtensionNamespace);
            }
            return this;
        }

        public GeneratorConfigurationBuilder AddProperty(string term, string typeName, bool include = true, string propertyName = null)
        {
            config.AddProperty(term, typeName, include, propertyName);
            return this;
        }

        public IGeneratorConfiguration Build()
        {
            return config;
        }

    }
}
