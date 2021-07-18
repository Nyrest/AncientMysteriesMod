using System.IO;
using System.Collections.Generic;

namespace AncientMysteries.SourceGenerator.Generators
{
    [Generator]
    public class ContentReferencesGenerator : _BaseSourceGenerator
    {
        public override string Using => string.Empty;

        public override string UniqueName => "ContentReferences";

        public const string prefix_Texture = "tex_";

        public const string prefix_Sound = "snd_";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            sb.AppendLine($"{TabLevel(1)}internal static partial class ContentReferences");
            sb.Append(TabLevel(1));
            sb.AppendLine("{");
            StringBuilder allTexturesBuilder = new($"{TabLevel(2)}public static readonly string[] _AllTextures = new string[]\n{TabLevel(2)}{{");
            StringBuilder allSoundsBuilder = new($"{TabLevel(2)}public static readonly string[] _AllSounds = new string[]\n{TabLevel(2)}{{");
            int textureCount = 0;
            int soundCount = 0;
            foreach (var fullname in Directory.GetFiles(context.GetProjectLocaltion() + "/content", "*.*").OrderBy(x => Path.GetFileName(x)))
            {
                string filename = Path.GetFileName(fullname);
                string fieldName;
                switch (Path.GetExtension(fullname).ToLower())
                {
                    case ".png":
                        {
                            switch (filename)
                            {
                                case "preview.png": continue;
                                case "screenshot.png": continue;
                                default:
                                    break;
                            }
                            fieldName = GetFieldName(prefix_Texture, filename);
                            allTexturesBuilder.Append($"\n{TabLevel(2)}{fieldName},");
                            textureCount++;
                            break;
                        }
                    case ".wav":
                        {
                            fieldName = GetFieldName(prefix_Sound, filename);
                            allSoundsBuilder.Append($"\n{TabLevel(2)}{fieldName},");
                            soundCount++;
                            break;
                        }
                    default: continue;
                };
                sb.AppendLine($"{TabLevel(2)}public const string {fieldName} = \"{filename}\";");
            }
            allTexturesBuilder.AppendLine($"\n{TabLevel(2)}}};");
            allSoundsBuilder.AppendLine($"\n{TabLevel(2)}}};");
            sb.Append(allTexturesBuilder);
            sb.Append(allSoundsBuilder);
            sb.AppendLine($"{TabLevel(2)}public const int ModTextureCount = {textureCount.ToString()};");
            sb.AppendLine($"{TabLevel(2)}public const int ModSoundCount = {soundCount.ToString()};");
            sb.Append(TabLevel(1));
            sb.AppendLine("}");
        }

        //TODO: Optimize
        public static string GetFieldName(string filename)
        {
            string extension = Path.GetExtension(filename);
            string filenameNoExt = Path.GetFileNameWithoutExtension(filename);
            string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
            string prefix;
            switch (extension.ToLowerInvariant())
            {
                case ".png": prefix = prefix_Texture; break;
                case ".wav": prefix = prefix_Sound; break;
                default: throw new Exception("Unexpected File Extension: " + extension);
            }
            return prefix + fieldName;
        }

        public static string GetFieldName(string prefix, string filename)
        {
            string extension = Path.GetExtension(filename);
            string filenameNoExt = Path.GetFileNameWithoutExtension(filename);
            string fieldName = char.ToUpperInvariant(filenameNoExt[0]) + filenameNoExt.Substring(1).Replace(" ", null);
            return prefix + fieldName;
        }
    }
}
