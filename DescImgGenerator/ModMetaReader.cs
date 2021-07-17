using System;

using Mono.Cecil;

namespace DescImgGenerator
{
    public record Item(LocalizedText name, LocalizedText description, SKBitmap? bitmap);

    public class LocalizedText
    {
        public Dictionary<Lang, string> localizations = new();

        private bool _englishOnly = true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetText(Lang lang)
        {
            if (_englishOnly) goto retEnglish;
            ref string result = ref CollectionsMarshal.GetValueRefOrNullRef(localizations, lang);
            if (!Unsafe.IsNullRef(ref result))
            {
                return result;
            }
        retEnglish:
            return GetEnglishText();
            [MethodImpl(MethodImplOptions.NoInlining)]
            string GetEnglishText() => localizations[Lang.english];
        }

        public LocalizedText AddText(Lang lang, string text)
        {
            if (string.IsNullOrEmpty(text)) return this;
            if (lang != Lang.english) _englishOnly = false;
            localizations[lang] = text;
            return this;
        }
    }

    public static unsafe class ModMetaReader
    {
        public static Item[] ModItems { get; private set; }

        public static AssemblyDefinition Asm { get; private set; }

        public static ModuleDefinition MainModule => Asm.MainModule;

        public static void LoadAssembly(string filename)
        {
            Asm = AssemblyDefinition.ReadAssembly(filename, new ReaderParameters()
            {
                ReadWrite = false,
            });
        }

        public static void ScanModItems()
        {
            List<Item> items = new(50);
            foreach (var type in MainModule.Types)
            {
                if (type.IsAbstract || type.IsValueType || !IsModItem(type, out CustomAttribute metaImageAttr)) continue;
                LocalizedText name = new(), description = new();

                foreach (var attr in type.CustomAttributes)
                {
                    var fullname = attr.AttributeType.FullName;
                    if (fullname.Equals("AncientMysteries.MetaInfoAttribute"))
                    {
                        var txtLang = (Lang)attr.ConstructorArguments[0].Value;
                        string txtName = (string)attr.ConstructorArguments[1].Value;
                        string txtDesc = (string)attr.ConstructorArguments[2].Value;
                        name.AddText(txtLang, txtName);
                        description.AddText(txtLang, txtDesc);
                    }
                }
                var metaImageAttrArgs = metaImageAttr.ConstructorArguments;
                var imgName = (string)metaImageAttrArgs[0].Value;
                var imgFrameWidth = (int)metaImageAttrArgs[1].Value;
                var imgFrameHeight = (int)metaImageAttrArgs[2].Value;
                var imgFrames = ((CustomAttributeArgument[])metaImageAttrArgs[3].Value).Select(x => (int)x.Value).ToArray();

                items.Add(new Item(name, description, GetItemBitmap(imgName, imgFrameWidth, imgFrameHeight, imgFrames)));
            }
            ModItems = items.ToArray();
            static bool IsModItem(TypeDefinition type, out CustomAttribute metaImageAttr)
            {
                Unsafe.SkipInit(out metaImageAttr);
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.FullName.Equals("AncientMysteries.MetaImageAttribute"))
                    {
                        metaImageAttr = attr;
                        return true;
                    }
                }
                return false;
            }
        }

        public static SKBitmap GetItemBitmap(string item, int frameWidth = -1, int frameHeight = -1, params int[] frames)
        {
            string filename = Path.GetFullPath("content\\" + item);
            var bitmap = SKBitmap.Decode(filename);
            return frameWidth == -1 ? bitmap : WithFrames(bitmap, frameWidth, frameHeight, frames);
            SKBitmap WithFrames(SKBitmap bitmap, int frameWidth = -1, int frameHeight = -1, params int[] frames)
            {
                return bitmap;
            }
        }
    }
}
