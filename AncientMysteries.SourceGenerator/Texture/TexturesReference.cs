using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AncientMysteries.SourceGenerator
{
    [Generator]
    public class TexturesReference : _BaseSourceGenerator
    {
        public override string UniqueName => "TexturesReference";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            sb.AppendLine("public static partial class AMTexs {");
            foreach (var file in context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    string filename = Path.GetFileName(file.Path);
                    string filenameNoExt = Path.GetFileNameWithoutExtension(file.Path);
                    string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
                    sb.AppendLine($"public const string {fieldName} = \"{filename}\";");
                }
            }
            sb.AppendLine("}");
        }
    }
}
