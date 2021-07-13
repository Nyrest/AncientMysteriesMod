using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace AncientMysteries.SourceGenerator.Generators
{
    [Generator]
    public class AutoNest : _BaseSourceGenerator
    {
        public override string Using => string.Empty;

        public override string UniqueName => "AutoNest";

        public override void Generate(GeneratorExecutionContext context, StringBuilder sb)
        {
            string loc = Helper.GetProjectLocaltion(context);
            foreach (var item in Directory.GetFiles(loc, "*.cs"))
            {
                if (Path.GetFileNameWithoutExtension(item).IndexOf('.') != -1)
                {

                }
            }
        }

        public static string GetFullNameWithoutExtension(string filename)
        {
            // array will be optimized to static by compiler
            int i = filename.AsSpan().LastIndexOfAny(new char[] { '.', Path.DirectorySeparatorChar });
            if (i == -1 || filename[i] != '.') Debugger.Launch();
            {

            }
            return null;
        }
    }
}
