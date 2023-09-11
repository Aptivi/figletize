// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using Figletize;

internal class Program
{
    private static int Main(string[] args)
    {
        // Parse the font from args
        string fontName = "standard";
        if (args.Length > 0)
            fontName = args[0].Trim();

        // Check the font
        var fontInstance = FigletizeFonts.TryGetByName(fontName);
        if (fontInstance is null)
        {
            Console.Error.WriteLine($"Font {fontName} not found. Exiting...");
            return 1;
        }

        // Main loop
        while (true)
        {
            Console.WriteLine("Press ENTER in its own line to exit.");
            Console.Write("Write your message: ");

            var message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
                break;

            try
            {
                Console.WriteLine(fontInstance.Render(message));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Exception: {e}");
            }
        }
        return 0;
    }
}
