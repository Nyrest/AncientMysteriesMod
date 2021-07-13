using System.IO;
using System.Collections.Generic;

namespace AncientMysteries.SourceGenerator.Generators
{
    [Generator]
    public class AutoNest : _BaseSourceGenerator
    {
        public override string Using => string.Empty;

        public override string UniqueName => "AutoNest";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            
        }
    }
}
