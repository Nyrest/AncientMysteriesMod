#! "net5.0"
using System;
using System.IO;
using System.Runtime.CompilerServices;

static string loc = Path.GetFullPath("AncientMysteries\\");
Main();
public static void Main()
{
    StringBuilder props = new StringBuilder(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project>
  <ItemGroup>
", 512);
    foreach (var item in Directory.GetFiles(loc, "*.cs", SearchOption.AllDirectories))
    {
        var fullpath = item.AsSpan();
        if (IsNestable(fullpath, out var parentFullpath) && File.Exists(parentFullpath))
        {
            var relativeFileName = item.Substring(loc.Length);
            props.AppendLine($"    <Compile Update=\"{relativeFileName}\" DependentUpon=\"{Path.GetFileName(parentFullpath)}\"/>");
        }
    }
    props.Append(@"  </ItemGroup>
</Project>");
    File.WriteAllText(loc + Path.DirectorySeparatorChar + "AutoNest.generated.props", props.ToString());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static bool IsNestable(ReadOnlySpan<char> fullpath, out string parentFullpath)
{
    Unsafe.SkipInit(out parentFullpath);
    int rootPathLength = fullpath.LastIndexOf(Path.DirectorySeparatorChar) + 1;

    var filename = fullpath.Slice(rootPathLength);


    var filenameWithoutExt = filename.Slice(0, filename.Length - 3);

    int parentFilenameExtPos = filenameWithoutExt.IndexOf('.');
    bool result = parentFilenameExtPos != -1;
    if (!result) return false;
    parentFullpath = GetParentFullpath(fullpath, rootPathLength, parentFilenameExtPos);
    return true;
}

public static string GetParentFullpath(ReadOnlySpan<char> fullpath, int rootPathLength, int parentFilenameExtPos)
{
    Span<char> result = stackalloc char[rootPathLength + parentFilenameExtPos + 3];
    fullpath.Slice(0, rootPathLength).CopyTo(result);
    fullpath.Slice(rootPathLength, parentFilenameExtPos).CopyTo(result.Slice(rootPathLength));
    ".cs".AsSpan().CopyTo(result.Slice(rootPathLength + parentFilenameExtPos));
    return result.ToString();
}

public static ReadOnlySpan<char> GetFullNameWithoutExtension(ReadOnlySpan<char> fullpath)
{
    // array will be optimized to static by compiler
    int i = fullpath.LastIndexOfAny(new char[] { '.', Path.DirectorySeparatorChar });
    if (i == -1 || fullpath[i] != '.') Debugger.Launch();
    return fullpath.Slice(0, i);
}

public static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> fullpath)
{
    int i = fullpath.LastIndexOf(Path.DirectorySeparatorChar);
    if (i != -1)
    {
        return fullpath.Slice(i);
    }
    return fullpath;
}