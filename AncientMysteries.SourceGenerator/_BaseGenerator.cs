using System.Runtime.CompilerServices;

namespace AncientMysteries.SourceGenerator
{


    public abstract class _BaseGenerator : ISourceGenerator
    {
        private static string[] _tabLevelsCacheAfter3 = new string[8];
        private const string tab = "    ";

        public abstract string UniqueName { get; }

        public abstract void Execute(GeneratorExecutionContext context);

        public abstract void Initialize(GeneratorInitializationContext context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string TabLevel(int level) => level switch
        {
            0 => string.Empty,
            1 => tab,
            2 => tab + tab,
            3 => tab + tab + tab,
            _ => _tabLevelsCacheAfter3[level - 4] ??= new string(' ', level * 4),
        };
    }
}
