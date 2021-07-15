using System.IO;
using System.Collections.Generic;

namespace AncientMysteries.SourceGenerator.Generators
{
    [Generator]
    public class TexturesReference : _BaseSourceGenerator
    {
        public override string Using => string.Empty;

        public override string UniqueName => "TextureReferences";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            sb.AppendLine($"{TabLevel(1)}internal static partial class Texs");
            sb.Append(TabLevel(1));
            sb.AppendLine("{");
            StringBuilder allTexturesBuilder = new($"{TabLevel(2)}public static readonly string[] _AllTextures = new string[]\n{TabLevel(2)}{{");
            List<string> fieldNameList = new(Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.png").Length);
            int count = 0;
            foreach (var fullname in Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.png").OrderBy(x => Path.GetFileName(x)))
            {
                string filename = Path.GetFileName(fullname);
                switch (filename)
                {
                    case "preview.png": continue;
                    case "screenshot.png": continue;
                    default:
                        break;
                }
                string fieldName = GetFieldName(filename);
                sb.AppendLine(TabLevel(2) + $"public const string {fieldName} = \"{filename}\";");
                allTexturesBuilder.Append($"\n{TabLevel(2)}{fieldName},");
                count++;
            }
            allTexturesBuilder.AppendLine($"\n{TabLevel(2)}}};");
            sb.Append(allTexturesBuilder);
            sb.AppendLine($"{TabLevel(2)}public const int ModTextureCount = {count.ToString()};");
            sb.Append(TabLevel(1));
            sb.AppendLine("}");
        }

        //TODO: Optimize
        public static string GetFieldName(string filename)
        {
            string filenameNoExt = Path.GetFileNameWithoutExtension(filename);
            string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
            if (char.IsNumber(fieldName[0]))
                fieldName = "_" + fieldName;
            return "t_" + fieldName;
        }
    }
}
