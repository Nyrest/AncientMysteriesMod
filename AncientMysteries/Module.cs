using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries
{
    public static class Module
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            var _AssemblyResolve = typeof(AppDomain).GetField("_AssemblyResolve", BindingFlags.Instance | BindingFlags.NonPublic);
            var handlers = (ResolveEventHandler)_AssemblyResolve.GetValue(AppDomain.CurrentDomain);
            _AssemblyResolve.SetValue(AppDomain.CurrentDomain, Delegate.Combine(new ResolveEventHandler(CurrentDomain_AssemblyResolve), handlers));
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.RequestingAssembly is null) goto DefaultBehavior;
            var referenceName = new AssemblyName(args.Name);
            string sourceRoot = args.RequestingAssembly.Location;
            if (!string.IsNullOrWhiteSpace(sourceRoot))
            {
                sourceRoot = Path.GetDirectoryName(sourceRoot);
            }
            else FixModLocation(ref sourceRoot, args);
            if (sourceRoot is null) goto DefaultBehavior;

            #region Search .dll

            try
            {
                foreach (var dllFile in Directory.EnumerateFiles(sourceRoot, "*.dll", SearchOption.TopDirectoryOnly))
                {
                    if (TryLoad(dllFile, out Assembly result))
                        return result;
                }
            }
            catch { }

            #endregion Search .dll

            #region Freaks

            try
            {
                foreach (var dllFile in Directory.EnumerateFiles(sourceRoot, "*.*", SearchOption.TopDirectoryOnly))
                {
                    if (TryLoad(dllFile, out Assembly result))
                        return result;
                }
            }
            catch { }

            #endregion Freaks

            bool TryLoad(string filename, out Assembly assembly)
            {
                try
                {
                    var dllName = AssemblyName.GetAssemblyName(filename);
                    if (AssemblyName.ReferenceMatchesDefinition(referenceName, dllName))
                    {
                        assembly = Assembly.LoadFile(filename);
                        return true;
                    }
                }
                catch { }
                assembly = null;
                return false;
            }

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
        DefaultBehavior:
            return null;
        }
    }
}
