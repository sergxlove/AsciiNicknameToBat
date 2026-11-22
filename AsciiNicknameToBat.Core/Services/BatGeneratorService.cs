using System.Text;
using AsciiNicknameToBat.Core.Models;

namespace AsciiNicknameToBat.Core
{
    public static class BatGeneratorService
    {
        private const int Height = 6;

        public static string GenerateArt(string text)
        {
            var lines = new List<StringBuilder>();
            for (int i = 0; i < Height; i++)
                lines.Add(new StringBuilder());

            foreach (char c in text.ToLower())
            {
                string[]? glyph = null;

                if (!BlockFontEN.Font.TryGetValue(c, out glyph) &&
                    !BlockFontDigits.Font.TryGetValue(c, out glyph) &&
                    !BlockFontSymbols.Font.TryGetValue(c, out glyph))
                {
                    glyph = new string[] { "  ", "  ", "  ", "  ", "  ", "  " };
                }

                int glyphWidth = 0;
                foreach (var line in glyph)
                    if (line.Length > glyphWidth) glyphWidth = line.Length;

                for (int row = 0; row < Height; row++)
                {
                    string glyphLine = (row < glyph.Length) ? glyph[row] : "";
                    lines[row].Append(glyphLine.PadRight(glyphWidth)).Append(' ');
                }
            }

            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine(line.ToString().TrimEnd());

            return sb.ToString();
        }

        public static string Generate(string text)
        {
            string art = GenerateArt(text);

            var batContent = new StringBuilder();
            batContent.AppendLine("@echo off");
            batContent.AppendLine("chcp 65001 >nul");
            batContent.AppendLine($"title {text}");
            batContent.AppendLine("echo.");

            foreach (var line in art.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                    batContent.AppendLine($"echo {line}");
            }

            batContent.AppendLine("echo.");
            batContent.AppendLine("pause");

            string fileName = $"{text.ToLower()}.bat";
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            File.WriteAllText(fullPath, batContent.ToString(), new UTF8Encoding(false));

            return fullPath;
        }

        public static string GetAvailableCharacters()
        {
            var chars = new StringBuilder();

            chars.AppendLine("--- English Letters (a-z) ---");
            chars.AppendLine(string.Join(" ", BlockFontEN.Font.Keys));
            chars.AppendLine();

            chars.AppendLine("--- Digits (0-9) ---");
            chars.AppendLine(string.Join(" ", BlockFontDigits.Font.Keys));
            chars.AppendLine();

            chars.AppendLine("--- Special Symbols ---");
            chars.AppendLine(string.Join(" ", BlockFontSymbols.Font.Keys));

            return chars.ToString();
        }
    }
}