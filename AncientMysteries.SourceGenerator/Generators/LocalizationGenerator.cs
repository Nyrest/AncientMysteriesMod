using System.Collections.Generic;
using System.Threading.Tasks;

namespace AncientMysteries.SourceGenerator.Generators
{
    [Generator]
    public class LocalizationGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // retrieve the receiver
            if (context.SyntaxContextReceiver is not LocalizationSyntaxReceiver receiver) return;
            object addSourceLock = new();
            List<(string hintName, SourceText sourceText)> sources = new(50);
            Parallel.ForEach(receiver.localizations, info =>
            {
                var sb = SBPool.Rent();
                sb.AppendLine("namespace " + info.Symbol.ContainingNamespace.ToDisplayString() + "{");

                sb.AppendLine("partial class " + info.Symbol.Name + "{");

                Build(sb, BuildType.Name, info);
                Build(sb, BuildType.Description, info);

                sb.AppendLine("}}");

                // return sb soon as possible
                string rawSource = sb.ToStringAndReturn();

                var hintName = $"{info.Symbol.ToDisplayString()}_LocalizableImpl.cs";
                var sourceText = SourceText.From(rawSource, Encoding.UTF8);

                lock (addSourceLock)
                    sources.Add((hintName, sourceText));
            });
            foreach (var (hintName, sourceText) in sources)
            {
                context.AddSource(hintName, sourceText);
            }
        }

        private static void Build(StringBuilder sb, BuildType type, LocalizedClass localized)
        {
            string methodName = type switch
            {
                BuildType.Name => "GetLocalizedName",
                BuildType.Description => "GetLocalizedDescription",
                _ => throw new NotImplementedException(),
            };
            sb.AppendLine(@$"{Tab(2)}public override string {methodName}(Lang lang) => lang switch {{");
            foreach (var info in localized.Infos.OrderBy(x => x.Lang == Lang.english ? 1 : 0))
            {
                if (type == BuildType.Name && info.Name is null) continue;
                if (type == BuildType.Description && info.Description is null) continue;

                string langCase = info.Lang == Lang.english ? "_" : $"{nameof(Lang)}.{info.Lang.ToString()}";
                sb.Append(Tab(3));
                sb.Append(langCase);
                sb.Append(" => ");
                sb.Append('\"');
                sb.Append((type switch
                {
                    BuildType.Name => info.Name,
                    BuildType.Description => info.Description,
                    _ => throw new NotImplementedException(),
                })?.Replace("\n", "\\n")); // Unescape
                sb.Append('\"');
                sb.AppendLine(",");
            }
            sb.Append(Tab(2));
            sb.AppendLine("};");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new LocalizationSyntaxReceiver());
        }

        private enum BuildType
        {
            Name,
            Description,
        }
    }
}