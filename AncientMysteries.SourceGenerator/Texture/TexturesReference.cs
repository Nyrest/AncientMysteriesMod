using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace AncientMysteries.SourceGenerator
{
    [Generator]
    public class TexturesReference : _BaseSourceGenerator
    {
        public override string Using => string.Empty;

        public override string UniqueName => "TexturesReference";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            sb.AppendLine("public static partial class Texs");
            sb.Append(TabLevel(1));
            sb.AppendLine("{");
            StringBuilder allTexturesBuilder = new($"{TabLevel(2)}public static readonly string[] _AllTextures = new string[]\n{TabLevel(2)}{{");
            List<string> fieldNameList = new(Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.png").Length);
            foreach (var fullname in Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.png").OrderBy(x => Path.GetFileName(x)))
            {
                if (Path.GetExtension(fullname).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    string filename = Path.GetFileName(fullname);
                    string filenameNoExt = Path.GetFileNameWithoutExtension(fullname);
                    string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
                    if (char.IsNumber(fieldName[0]))
                        fieldName = "_" + fieldName;
                    sb.AppendLine(TabLevel(2) + $"public const string t_{fieldName} = \"{filename}\";");
                    allTexturesBuilder.Append($"\n{TabLevel(2)}t_{fieldName},");
                }
            }
            allTexturesBuilder.AppendLine($"\n{TabLevel(2)}}};");
            sb.Append(allTexturesBuilder);
            sb.Append(TabLevel(1));
            sb.AppendLine("}");
        }
    }
}
