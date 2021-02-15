using AncientMysteries.SourceGenerator.Utilities;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace AncientMysteries.SourceGenerator
{
    [Generator]
    public class TextureOptimizer : ISourceGenerator
    {
        private static readonly MD5 _md5Provider = MD5.Create();

        public void Execute(GeneratorExecutionContext context)
        {
            using var texturesHashsStream = new FileStream(context.GetProjectLocaltion() + "/../.textureHashes", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            var toProcessList = ReadList(texturesHashsStream, in context, out bool needRefresh);
            if (toProcessList.Count > 0)
            {
                DoProcess(context.GetProjectLocaltion() + "/../Tools/oxipng.exe", toProcessList);
                WriteList(texturesHashsStream, in context);
                return;
            }
            else if (needRefresh)
            {
                WriteList(texturesHashsStream, in context);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<string> ReadList(Stream stream, in GeneratorExecutionContext context, out bool needRefresh)
        {
            stream.Position = 0;
            needRefresh = false;
            Dictionary<string, byte[]> hashDict = new Dictionary<string, byte[]>(100);
            BinFlow flow = new(stream);
            while (flow.TryReadString(out string name) && flow.TryReadBytes(out byte[] hash))
            {
                hashDict.Add(name, hash);
            }
            List<string> result = new List<string>();
            int totalFileCount = 0;
            foreach (var file in context.AdditionalFiles.OrderBy(x => Path.GetFileName(x.Path)))
            {
                if (Path.GetExtension(file.Path).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    var name = Path.GetFileName(file.Path);
                    var md5 = _md5Provider.ComputeHash(File.ReadAllBytes(file.Path));
                    if (!hashDict.TryGetValue(name, out byte[] expectedHash) || !md5.AsSpan().SequenceEqual(expectedHash))
                    {
                        result.Add(file.Path);
                        needRefresh = true;
                        totalFileCount++;
                    }
                }
            }
            if (!needRefresh)
                needRefresh = totalFileCount != hashDict.Count;
            return result;
        }

        public static void WriteList(Stream stream, in GeneratorExecutionContext context)
        {
            stream.Position = 0;
            BinFlow flow = new(stream);
            foreach (var file in context.AdditionalFiles.OrderBy(x => Path.GetFileName(x.Path)))
            {
                if (Path.GetExtension(file.Path).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    flow.WriteString(Path.GetFileName(file.Path));
                    var md5 = _md5Provider.ComputeHash(File.ReadAllBytes(file.Path));
                    flow.WriteBytes(md5);
                }
            }
            flow.MarkHereAsEnd();
        }

        public static void DoProcess(string exe, List<string> list)
        {
            StringBuilder sbMini = SBPool.RentMini();
            foreach (var item in list)
            {
                sbMini.Append(item);
                sbMini.Append(' ');
            }
            using var process = Process.Start(new ProcessStartInfo(exe, "-o max -i 0 --strip all " + sbMini.ToString())
            {
                
            });
            sbMini.ReturnMini();
            process.WaitForExit();
        }

        public void Initialize(GeneratorInitializationContext context) { }
    }
}
