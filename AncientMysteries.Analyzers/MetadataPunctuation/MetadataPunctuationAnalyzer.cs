using AncientMysteries.SourceGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace AncientMysteries.Analyzers.MetadataPunctuation
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MetadataPunctuationAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0006";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_FixMetadataPunctuation_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_FixMetadataPunctuation_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_FixMetadataPunctuation_Description), Res.ResourceManager, typeof(Res));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            //Debugger.Launch();
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.Attribute);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (AttributeSyntax)context.Node;
            if (node.Name.ToString() == "MetaInfo" && node.ArgumentList is AttributeArgumentListSyntax argList)
            {
                var args = argList.Arguments;
                if (args.Count < 3) return;
                if (
                    args[0].Expression is not MemberAccessExpressionSyntax langExp ||
                    //args[1].Expression is not LiteralExpressionSyntax nameExp ||
                    args[2].Expression is not LiteralExpressionSyntax descriptionExp)
                    return;
                if (!Enum.TryParse(langExp.OperatorToken.GetNextToken().ToString(), out Lang lang)) return;
                //string? name = nameExp.Token.Value as string;
                string? description = descriptionExp.Token.Value as string;

                /*
                if (TryProcessText(name, lang, out _, true))
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, nameExp.GetLocation()));
                }
                */
                if (TryProcessText(description, lang, null))
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, descriptionExp.GetLocation()));
                }
            }
        }

        public static char[] englishPeriodIgnores = new[] { '?', '!', '？', '！', '」', ' ' };
        public static char[] chinesePeriodIgnores = new[] { '?', '!', '？', '！', '」', ' ' };
        public static bool TryProcessText(string? text, Lang lang, StringBuilder? processedText)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var span = text!.AsSpan();
            bool result = false;
            if (processedText is not null)
            {
                processedText.Clear().Append(text);
            }
            switch (lang)
            {
                #region English
                case Lang.english:
                    {
                        if (span.Contains('，'))
                        {
                            result = true;
                            processedText?.Replace('，', ',');
                        }

                        if (span.Contains('。'))
                        {
                            result = true;
                            processedText?.Replace('。', '.');
                        }

                        if (span.Contains('？'))
                        {
                            result = true;
                            processedText?.Replace('？', '?');
                        }

                        if (span.Contains('！'))
                        {
                            result = true;
                            processedText?.Replace('！', '!');
                        }
                        TryInsertPeriods(span, '.', englishPeriodIgnores);

                        break;
                    }
                #endregion
                #region Chinese
                case Lang.tchinese:
                case Lang.schinese:
                    {
                        if (span.Contains(','))
                        {
                            result = true;
                            processedText?.Replace(',', '，');
                        }

                        if (span.Contains('.', out int i) && i != 0 && !int.TryParse(span[i - 1].ToString(), out _))
                        {
                            result = true;
                            processedText?.Replace('.', '。');
                        }

                        if (span.Contains('?'))
                        {
                            result = true;
                            processedText?.Replace('?', '？');
                        }

                        if (span.Contains('!'))
                        {
                            result = true;
                            processedText?.Replace('!', '！');
                        }

                        TryInsertPeriods(span, '。', chinesePeriodIgnores);
                        break;
                    }
                #endregion
                default: goto case Lang.english;
            }
            return result;
            void TryInsertPeriods(ReadOnlySpan<char> span, char value, ReadOnlySpan<char> ignores)
            {
                if (span.Contains('\n'))
                {
                    int offset = 0;
                    foreach (var item in span.Split('\n'))
                    {
                        if (item.IsEmpty)
                        {
                            continue;
                        }
                        char last = item[^1];
                        if (last != value && !ignores.Contains(last))
                        {
                            result = true;
                            offset += item.Length;
                            processedText?.Insert(offset, value);
                        }
                        offset += 2; // \n and .
                    }
                }
                else
                {
                    char last = span[^1];
                    if (last != value && !ignores.Contains(last))
                    {
                        result = true;
                        processedText?.Append(value);
                    }
                }
            }
        }
    }
}
