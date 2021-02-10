using Microsoft.CodeAnalysis;

namespace AncientMysteries.SourceGenerator
{
    public abstract class _BaseGenerator : ISourceGenerator
    {
        public abstract string UniqueName { get; }

        public abstract void Execute(GeneratorExecutionContext context);

        public abstract void Initialize(GeneratorInitializationContext context);
    }
}
