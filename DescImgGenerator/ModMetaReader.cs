using System;
using System.Threading.Tasks;

namespace DescImgGenerator
{


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
            Parallel.ForEach(MainModule.Types.Where(x => !x.IsAbstract && !x.IsValueType), type =>
            {
                if (!IsModItem(type, out CustomAttribute metaImageAttr)) return;
                LocalizedText name = new(), description = new();
                int order = 0;
                MetaType metaType = MetaType.Error;
                foreach (var attr in type.CustomAttributes)
                {
                    var fullname = attr.AttributeType.FullName;
                    switch (fullname)
                    {
                        case "AncientMysteries.MetaInfoAttribute":
                            {
                                var txtLang = (Lang)attr.ConstructorArguments[0].Value;
                                string txtName = (string)attr.ConstructorArguments[1].Value;
                                string txtDesc = (string)attr.ConstructorArguments[2].Value;
                                name.AddText(txtLang, txtName);
                                description.AddText(txtLang, txtDesc);
                                break;
                            }
                        case "AncientMysteries.MetaOrderAttribute":
                            {
                                order = (int)attr.ConstructorArguments[0].Value;
                                break;
                            }
                        case "AncientMysteries.MetaTypeAttribute":
                            {
                                metaType = (MetaType)attr.ConstructorArguments[0].Value;
                                break;
                            }
                        default:
                            break;
                    }
                }
                var metaImageAttrArgs = metaImageAttr.ConstructorArguments;
                var imgName = (string)metaImageAttrArgs[0].Value;
                var imgFrameWidth = (int)metaImageAttrArgs[1].Value;
                var imgFrameHeight = (int)metaImageAttrArgs[2].Value;
                var rawImgFrames = (CustomAttributeArgument[])metaImageAttrArgs[3].Value;
                var imgFrames = rawImgFrames.Length != 0 ? rawImgFrames.Select(x => (int)x.Value).ToArray() : new int[1] { 0 };
                lock (items)
                    items.Add(new Item(name, description, GetItemBitmap(imgName, imgFrameWidth, imgFrameHeight, imgFrames), order, metaType));
            });

            ModItems = items.ToArray();
            ModItems.AsSpan().Sort((x, y) => x.name.GetText(Lang.english).CompareTo(y.name.GetText(Lang.english)));
            ModItems.AsSpan().Sort();
            ModItems = ModItems.OrderBy(x => (int)x.metaType).ToArray();
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

        public static SKBitmap GetItemBitmap(string item, int frameWidth, int frameHeight, int[] frames)
        {
            string filename = Path.GetFullPath("content\\" + item);
            var bitmap = SKBitmap.Decode(filename);
            return frameWidth == -1 ? bitmap : WithFrames();

            SKBitmap WithFrames()
            {
                SKBitmap result = new((frameWidth + frameMargin) * frames.Length, frameHeight);
                SKCanvas canvas = new(result);
                for (int i = 0; i < frames.Length; i++)
                {
                    int frame = frames[i];
                    var sourceRect = GetFrame(bitmap, frameWidth, frameHeight, frame);
                    SKRect destRect = SKRect.Create((frameWidth + frameMargin) * i, 0, frameWidth, frameHeight);
                    canvas.DrawBitmap(bitmap, sourceRect, destRect, null);
                }
                canvas.Flush();
                return result;
                SKRect GetFrame(SKBitmap bitmap, int frameWidth, int frameHeight, int frame)
                {
                    if (frame == 0) return new SKRect(0, 0, frameWidth, frameHeight);
                    int row = (frameWidth * frame) / bitmap.Width;
                    int column = frame - row * (bitmap.Width / frameWidth);
                    return SKRect.Create(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
                }
            }
        }
    }
}