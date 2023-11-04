// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Figletize.Utilities;
using System;
using System.Collections.Generic;

namespace Figletize
{
    /// <summary>
    /// Figlet tools
    /// </summary>
    public static class FigletTools
    {
        private readonly static Dictionary<string, string> cachedFiglets = new();
        private readonly static Dictionary<string, FigletizeFont> cachedFigletFonts = new();

        /// <summary>
        /// Gets the figlet lines
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static string[] GetFigletLines(string Text, FigletizeFont FigletFont)
        {
            Text = FigletFont.Render(Text);
            return Text.SplitNewLines();
        }

        /// <summary>
        /// Gets the figlet text height
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static int GetFigletHeight(string Text, FigletizeFont FigletFont) =>
            GetFigletLines(Text, FigletFont).Length;

        /// <summary>
        /// Gets the figlet text width
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static int GetFigletWidth(string Text, FigletizeFont FigletFont) =>
            GetFigletLines(Text, FigletFont)[0].Length;

        /// <summary>
        /// Gets the figlet fonts
        /// </summary>
        /// <returns>List of supported Figlet fonts</returns>
        public static Dictionary<string, FigletizeFont> GetFigletFonts()
        {
            Dictionary<string, FigletizeFont> fonts = new();
            if (cachedFigletFonts.Count > 0)
            {
                // Fetch the cached version
                fonts = new(cachedFigletFonts);
                return fonts;
            }

            // Now, populate through all the built-in fonts
            foreach (string fontName in FigletizeFonts._builtinFonts)
            {
                var font = FigletizeFonts.TryGetByName(fontName);
                if (font is not null)
                {
                    fonts.Add(fontName, font);
                    cachedFigletFonts.Add(fontName, font);
                }
            }
            return fonts;
        }

        /// <summary>
        /// Gets the figlet font from font name
        /// </summary>
        /// <param name="FontName">Font name. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <returns>Figlet font instance of your font, or Small if not found</returns>
        public static FigletizeFont GetFigletFont(string FontName)
        {
            var figletFonts = GetFigletFonts();
            if (figletFonts.ContainsKey(FontName))
                return figletFonts[FontName];
            else
                return figletFonts["small"];
        }

        /// <summary>
        /// Renders the figlet font
        /// </summary>
        /// <param name="Text">Text to render</param>
        /// <param name="figletFontName">Figlet font name to render. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <param name="Vars">Variables to use when formatting the string</param>
        public static string RenderFiglet(string Text, string figletFontName, params object[] Vars)
        {
            var FigletFont = GetFigletFont(figletFontName);
            return RenderFiglet(Text, FigletFont, Vars);
        }

        /// <summary>
        /// Renders the figlet font
        /// </summary>
        /// <param name="Text">Text to render</param>
        /// <param name="FigletFont">Figlet font instance to render. Consult <see cref="GetFigletFonts()"/> for more info.</param>
        /// <param name="Vars">Variables to use when formatting the string</param>
        public static string RenderFiglet(string Text, FigletizeFont FigletFont, params object[] Vars)
        {
            // Get the figlet font name.
            string figletFontName = FigletFont.Name;
            if (string.IsNullOrEmpty(figletFontName))
                return "";

            // Now, render the figlet and add to the cache
            string cachedFigletKey = $"[{cachedFiglets.Count} - {figletFontName}] {Text}";
            string cachedFigletKeyToAdd = $"[{cachedFiglets.Count + 1} - {figletFontName}] {Text}";
            if (cachedFiglets.ContainsKey(cachedFigletKey))
                return cachedFiglets[cachedFigletKey];
            else
            {
                // Format string as needed
                if (!(Vars.Length == 0))
                    Text = string.Format(Text, Vars);

                // Write the font
                Text = string.Join("\n", GetFigletLines(Text, FigletFont));
                cachedFiglets.Add(cachedFigletKeyToAdd, Text);
                return Text;
            }
        }

        internal static string[] SplitNewLines(this string target) =>
            target.Replace(Convert.ToChar(13).ToString(), "")
               .Split(Convert.ToChar(10));
    }
}
