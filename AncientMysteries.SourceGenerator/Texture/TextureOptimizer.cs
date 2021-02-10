using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace AncientMysteries.SourceGenerator
{
    [Generator]
    public class TextureOptimizer : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            foreach (var item in context.AdditionalFiles)
            {
                ;
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
