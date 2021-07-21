#define AllowTabFormat

using System.IO;
using System.Runtime.CompilerServices;

namespace AncientMysteries.SourceGenerator
{
    public static class Helper
    {
        private static readonly string[] _tabLevelsCache = new string[8];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string? Tab(int count)
        {
#if AllowTabFormat
            return _tabLevelsCache[count] ??= new string(' ', count * 4);
#else
            return null;
#endif
        }

        private static string? _projectLocation;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetProjectLocaltion(this in GeneratorExecutionContext context)
        {
            return _projectLocation ?? GetLocation(in context);
            [MethodImpl(MethodImplOptions.NoInlining)]
            static string GetLocation(in GeneratorExecutionContext context)
            {
                foreach (var file in context.AdditionalFiles)
                {
                    if (Path.GetFileName(file.Path) == "_projLocator")
                    {
                        return _projectLocation = Path.GetDirectoryName(file.Path);
                    }
                }
                throw new Exception("Unable to locate the project\nAdd '_projLocator' file to Project AdditionalFiles");
            }
        }
    }
}