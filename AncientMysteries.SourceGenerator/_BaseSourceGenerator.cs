using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AncientMysteries.SourceGenerator
{
    public abstract class _BaseSourceGenerator : _BaseGenerator
    {
        public virtual string Using => _CompileSettings.DefaultUsing + "\n";

        public override void Execute(GeneratorExecutionContext context)
        {
            var contentBuilder = SBPool.Rent();
            Generate(context, contentBuilder);
            if (contentBuilder.Length == 0) goto end;

            string source = @$"{Using}namespace {_CompileSettings.Namespace}
{{
{contentBuilder}
}}";
            // Wht not? Cuz it's fucking suffering that using SourceGenerator In NetFX
            context.AddSource(UniqueName + ".cs", SourceText.From(source, Encoding.UTF8));
        end:
            SBPool.Return(contentBuilder);
        }

        public abstract void Generate(GeneratorExecutionContext context, StringBuilder sb);

        public override void Initialize(GeneratorInitializationContext context) { }
    }
}
