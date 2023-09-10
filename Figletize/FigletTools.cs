// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Figletize.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Figletize
{
    /// <summary>
    /// Figlet tools
    /// </summary>
    public static class FigletTools
    {
        private readonly static Dictionary<string, string> cachedFiglets = new();
        private static readonly Dictionary<string, Func<object>> cachedGetters = new();

        /// <summary>
        /// The figlet fonts dictionary. It lists all the Figlet fonts supported by the Figgle library.
        /// </summary>
        public readonly static Dictionary<string, object> FigletFonts = GetProperties(typeof(FigletizeFonts));

        /// <summary>
        /// Gets the figlet lines
        /// </summary>
        /// <param name="Text">Text</param>
        /// <param name="FigletFont">Target figlet font</param>
        public static string[] GetFigletLines(string Text, FigletizeFont FigletFont)
        {
            Text = FigletFont.Render(Text);
            var TextLines = Text.SplitNewLines();
            List<string> lines = new(TextLines);

            // Try to trim from the top
            for (int line = 0; line < lines.Count; line++)
            {
                if (!string.IsNullOrWhiteSpace(lines[line]))
                    break;
                lines.RemoveAt(line);
            }

            // Try to trim from the bottom
            for (int line = lines.Count - 1; line > 0; line--)
            {
                if (!string.IsNullOrWhiteSpace(lines[line]))
                    break;
                lines.RemoveAt(line);
            }

            return lines.ToArray();
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
        /// Gets the figlet font from font name
        /// </summary>
        /// <param name="FontName">Font name. Consult <see cref="FigletFonts"/> for more info.</param>
        /// <returns>Figlet font instance of your font, or Small if not found</returns>
        public static FigletizeFont GetFigletFont(string FontName)
        {
            if (FigletFonts.ContainsKey(FontName))
            {
                return (FigletizeFont)FigletFonts[FontName];
            }
            else
            {
                return (FigletizeFont)FigletFonts["Small"];
            }
        }

        /// <summary>
        /// Gets the figlet font name from font
        /// </summary>
        /// <param name="Font">Font instance. Consult <see cref="FigletFonts"/> for more info.</param>
        /// <returns>Figlet font name of your font, or an empty string if not found</returns>
        public static string GetFigletFontName(FigletizeFont Font)
        {
            // We need to use the FigletFonts variable and scour through it to look for this specific copy.
            string figletFontName = "";
            foreach (string FigletFontToCompare in FigletFonts.Keys)
            {
                if (GetFigletFont(FigletFontToCompare) == Font)
                {
                    figletFontName = FigletFontToCompare;
                    break;
                }
            }

            // If we don't have the font in the supported fonts dictionary, return an empty string
            if (FigletizeFonts.TryGetByName(figletFontName) is null)
                return "";

            // Otherwise, return the name
            return figletFontName;
        }

        /// <summary>
        /// Renders the figlet font
        /// </summary>
        /// <param name="Text">Text to render</param>
        /// <param name="figletFontName">Figlet font name to render. Consult <see cref="FigletFonts"/> for more info.</param>
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
        /// <param name="FigletFont">Figlet font instance to render. Consult <see cref="FigletFonts"/> for more info.</param>
        /// <param name="Vars">Variables to use when formatting the string</param>
        public static string RenderFiglet(string Text, FigletizeFont FigletFont, params object[] Vars)
        {
            // Look at the Remarks section of GetFigletFontName to see why we're doing this.
            string figletFontName = GetFigletFontName(FigletFont);
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

        internal static Dictionary<string, object> GetProperties(Type VariableType)
        {
            // Get field for specified variable
            var Properties = VariableType.GetProperties();
            var PropertyDict = new Dictionary<string, object>();

            // Get the properties and get their values
            foreach (PropertyInfo VarProperty in Properties)
            {
                var PropertyValue = ExpressionGetPropertyValue(VarProperty);
                PropertyDict.Add(VarProperty.Name, PropertyValue);
            }
            return PropertyDict;
        }

        internal static string[] SplitNewLines(this string target) =>
            target.Replace(Convert.ToChar(13).ToString(), "")
               .Split(Convert.ToChar(10));

        private static object ExpressionGetPropertyValue(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            string cachedName = $"{propertyInfo.DeclaringType.FullName} - {propertyInfo.Name}";
            if (cachedGetters.ContainsKey(cachedName))
            {
                var cachedExpression = cachedGetters[cachedName];
                return cachedExpression();
            }

            var callExpr = Expression.Call(propertyInfo.GetGetMethod());
            var convExpr = Expression.Convert(callExpr, typeof(object));

            var expression = Expression.Lambda<Func<object>>(convExpr).Compile();

            cachedGetters.Add(cachedName, expression);
            return expression();
        }
    }
}
