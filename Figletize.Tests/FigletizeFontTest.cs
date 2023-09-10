// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Figletize.Tests;

public class FigletizeFontTest
{
    private readonly ITestOutputHelper _output;
    public FigletizeFontTest(ITestOutputHelper output) => _output = output;

    [Fact]
    public void Render()
    {
        Test(FigletizeFonts.Standard, "Hello, World!", null,
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        Test(FigletizeFonts.ThreePoint, "Hello, World!", null,
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.",
            @"                              ");

        Test(FigletizeFonts.Ogre, "Hello, World!", null,
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        Test(FigletizeFonts.Rectangles, "Hello, World!", null,
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        Test(FigletizeFonts.Slant, "Hello, World!", null,
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        Test(FigletizeFonts.Slant, "H.W", null,
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ",
            @"                    ");

        Test(FigletizeFonts.Impossible, "Figletize", null,
            @"         _        _          _              _              _             _      ",
            @"        /\ \     /\ \       /\ \           /\ \           _\ \          /\ \    ",
            @"       /  \ \    \ \ \     /  \ \         /  \ \         /\__ \        /  \ \   ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /\ \_\       / /_ \_\      / /\ \ \  ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/      / / /\/_/     / / /\ \_\ ",
            @"    / /_/_ \/_// / /    / / / ______   / / / ______   / / /         / /_/_ \/_/ ",
            @"   / /____/\  / / /    / / / /\_____\ / / / /\_____\ / / /         / /____/\    ",
            @"  / /\____\/ / / /    / / /  \/____ // / /  \/____ // / / ____    / /\____\/    ",
            @" / / /   ___/ / /__  / / /_____/ / // / /_____/ / // /_/_/ ___/\ / / /______    ",
            @"/ / /   /\__\/_/___\/ / /______\/ // / /______\/ //_______/\__\// / /_______\   ",
            @"\/_/    \/_________/\/___________/ \/___________/ \_______\/    \/__________/   ",
            @"                                                                                ");

        Test(FigletizeFonts.Graffiti, "Hello, World!", null,
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        void Test(FigletizeFont font, string s, int? smushOverride = null, params string[] expected)
        {
            var output = font.Render(s, smushOverride);
            var actual = output.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            actual = actual.Take(Math.Max(0, actual.Length - 1)).ToArray();
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                if (expected[i] == actual[i])
                    continue;
                if (expected[i].Length != actual[i].Length)
                {
                    _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                    _output.WriteLine("Actual:\n" + output);
                    Assert.True(false, $"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");
                }

                for (var x = 0; x < expected[i].Length; x++)
                {
                    if (expected[i][x] != actual[i][x])
                    {
                        _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                        _output.WriteLine("Actual:\n" + output);
                        Assert.True(false, $"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                    }
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(EnumerateAllFonts))]
    public void RenderStressTest(string name, FigletizeFont font)
    {
        _ = name; // suppress warning about unused parameter -- it's useful when debugging test failures

        _ = font.Render("Hello, World!");
    }

    public static IEnumerable<object[]> EnumerateAllFonts()
    {
        using var stream = typeof(FigletizeFonts).GetTypeInfo().Assembly.GetManifestResourceStream("Figletize.Fonts.zip");

        if (stream is null)
            throw new FigletizeException("Unable to open embedded font archive.");

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        StringPool stringPool = new();

        foreach (var entry in zip.Entries)
        {
            using var entryStream = entry.Open();

            yield return new object[] { entry.Name, FigletizeFontParser.Parse(entryStream, stringPool) };
        }
    }
}
