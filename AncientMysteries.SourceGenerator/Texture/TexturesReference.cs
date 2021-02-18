using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

namespace AncientMysteries.SourceGenerator
{
    [Generator]
    public class TexturesReference : _BaseSourceGenerator
    {
        public override string UniqueName => "TexturesReference";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            sb.AppendLine("public static partial class Texs");
            sb.Append(TabLevel(1));
            sb.AppendLine("{");
            foreach (var fullname in Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.png").OrderBy(x => Path.GetFileName(x)))
            {
                if (Path.GetExtension(fullname).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    string filename = Path.GetFileName(fullname);
                    string filenameNoExt = Path.GetFileNameWithoutExtension(fullname);
                    string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
                    if (char.IsNumber(fieldName[0]))
                        fieldName = "_" + fieldName;
                    sb.AppendLine(TabLevel(2) + $"public const string {fieldName} = \"{filename}\";");
                }
            }
            sb.Append(TabLevel(1));
            sb.AppendLine("}");
        }
    }
}
