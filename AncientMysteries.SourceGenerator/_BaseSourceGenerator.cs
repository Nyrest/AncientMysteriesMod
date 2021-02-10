using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace AncientMysteries.SourceGenerator
{
    public abstract class _BaseSourceGenerator : _BaseGenerator
    {
        public override void Execute(GeneratorExecutionContext context)
        {
            var contentBuilder = SBPool.Rent();
            Generate(context, contentBuilder);
            string source = @$"
{_CompileSettings.DefaultUsing}
namespace {_CompileSettings.Namespace}
{{
    {contentBuilder.ToString()}
}}
";
            context.AddSource(UniqueName + ".cs", SourceText.From(source, Encoding.UTF8));
        }

        public abstract void Generate(GeneratorExecutionContext context, StringBuilder sb);

        public override void Initialize(GeneratorInitializationContext context) { }
    }
}
