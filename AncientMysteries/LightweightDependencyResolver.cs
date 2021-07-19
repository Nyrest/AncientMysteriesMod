namespace AncientMysteries
{
    public static class LightweightDependencyResolver
    {
        public static Assembly ModResolve(object sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly is null) goto DefaultBehavior;
            var referenceName = new AssemblyName(args.Name);
            string sourceRoot = args.RequestingAssembly.Location;
            if(string.IsNullOrEmpty(sourceRoot))
            {
                sourceRoot = Path.GetDirectoryName(sourceRoot);
            }
            else FixModLocation(ref sourceRoot, args);
            if (sourceRoot is null) goto DefaultBehavior;

            foreach (var dllFile in Directory.EnumerateFiles(sourceRoot, "*.dll", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var dllName = AssemblyName.GetAssemblyName(dllFile);
                    if (AssemblyName.ReferenceMatchesDefinition(referenceName, dllName))
                    {
                        return Assembly.LoadFile(dllFile);
                    }
                }
                catch { }
            }

        DefaultBehavior:
            return ManagedContent.ResolveModAssembly(sender, args);

            static void FixModLocation(ref string sourceRoot, ResolveEventArgs args)
            {
                foreach (var mod in ModLoader.accessibleMods)
                {
                    var configuration = mod.configuration;
                    if (configuration.assembly == args.RequestingAssembly)
                    {
                        sourceRoot = configuration.directory + Path.DirectorySeparatorChar;
                        return;
                    }
                }
            }
        }
    }
}