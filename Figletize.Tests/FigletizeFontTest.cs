// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Figletize.Utilities;
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
    public void RenderByFontInstance()
    {
        Test(FigletizeFonts.TryGetByName("standard"), "Hello, World!", null,
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        Test(FigletizeFonts.TryGetByName("threepoint"), "Hello, World!", null,
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        Test(FigletizeFonts.TryGetByName("ogre"), "Hello, World!", null,
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        Test(FigletizeFonts.TryGetByName("rectangles"), "Hello, World!", null,
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        Test(FigletizeFonts.TryGetByName("slant"), "Hello, World!", null,
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        Test(FigletizeFonts.TryGetByName("slant"), "H.W", null,
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        Test(FigletizeFonts.TryGetByName("impossible"), "Figletize", null,
            @"         _        _          _              _             _          _          _           _                 _      ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \       /\ \       /\ \               /\ \    ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \      \ \ \     /  \ \             /  \ \   ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \     /\ \_\ __/ /\ \ \           / /\ \ \  ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \   / /\/_//___/ /\ \ \         / / /\ \_\ ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ / / /   \___\/ / / /        / /_/_ \/_/ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_// / /          / / /        / /____/\    ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /      / / /          / / /    _   / /\____\/    ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /   ___/ / /__         \ \ \__/\_\ / / /______    ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /   /\__\/_/___\         \ \___\/ // / /_______\   ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/    \/_________/          \/___/_/ \/__________/   ");

        Test(FigletizeFonts.TryGetByName("graffiti"), "Hello, World!", null,
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
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                if (expected[i] == actual[i])
                    continue;
                if (expected[i].Length != actual[i].Length)
                {
                    _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                    _output.WriteLine("Actual:\n" + output);
                    Assert.Fail($"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");
                }

                for (var x = 0; x < expected[i].Length; x++)
                {
                    if (expected[i][x] != actual[i][x])
                    {
                        _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                        _output.WriteLine("Actual:\n" + output);
                        Assert.Fail($"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                    }
                }
            }
        }
    }

    [Fact]
    public void RenderByFigletTools()
    {
        TestFont(FigletizeFonts.TryGetByName("standard"), "Hello, World!",
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        TestFont(FigletizeFonts.TryGetByName("threepoint"), "Hello, World!",
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        TestFont(FigletizeFonts.TryGetByName("ogre"), "Hello, World!",
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        TestFont(FigletizeFonts.TryGetByName("rectangles"), "Hello, World!",
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        TestFont(FigletizeFonts.TryGetByName("slant"), "Hello, World!",
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        TestFont(FigletizeFonts.TryGetByName("slant"), "H.W",
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        TestFont(FigletizeFonts.TryGetByName("impossible"), "Figletize",
            @"         _        _          _              _             _          _          _           _                 _      ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \       /\ \       /\ \               /\ \    ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \      \ \ \     /  \ \             /  \ \   ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \     /\ \_\ __/ /\ \ \           / /\ \ \  ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \   / /\/_//___/ /\ \ \         / / /\ \_\ ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ / / /   \___\/ / / /        / /_/_ \/_/ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_// / /          / / /        / /____/\    ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /      / / /          / / /    _   / /\____\/    ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /   ___/ / /__         \ \ \__/\_\ / / /______    ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /   /\__\/_/___\         \ \___\/ // / /_______\   ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/    \/_________/          \/___/_/ \/__________/   ");

        TestFont(FigletizeFonts.TryGetByName("graffiti"), "Hello, World!",
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        Test("standard", "Hello, World!",
            @"  _   _      _ _         __        __         _     _ _ ",
            @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
            @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
            @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
            @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
            @"                     |/                                 ");

        Test("threepoint", "Hello, World!",
            @"|_| _ || _    \    / _  _| _||",
            @"| |(/_||(_),   \/\/ (_)| |(_|.");

        Test("ogre", "Hello, World!",
            @"            _ _          __    __           _     _   _ ",
            @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
            @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
            @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
            @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
            @"                    |/                                  ");

        Test("rectangles", "Hello, World!",
            @"                                            __ ",
            @" _____     _ _          _ _ _         _   _|  |",
            @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
            @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
            @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
            @"                  |_|                          ");

        Test("slant", "Hello, World!",
            @"    __  __     ____           _       __           __    ____",
            @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
            @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
            @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
            @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
            @"                     |/                                      ");

        Test("slant", "H.W",
            @"    __  ___       __",
            @"   / / / / |     / /",
            @"  / /_/ /| | /| / / ",
            @" / __  /_| |/ |/ /  ",
            @"/_/ /_/(_)__/|__/   ");

        Test("impossible", "Figletize",
            @"         _        _          _              _             _          _          _           _                 _      ",
            @"        /\ \     /\ \       /\ \           _\ \          /\ \       /\ \       /\ \       /\ \               /\ \    ",
            @"       /  \ \    \ \ \     /  \ \         /\__ \        /  \ \      \_\ \      \ \ \     /  \ \             /  \ \   ",
            @"      / /\ \ \   /\ \_\   / /\ \_\       / /_ \_\      / /\ \ \     /\__ \     /\ \_\ __/ /\ \ \           / /\ \ \  ",
            @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/     / / /\ \_\   / /_ \ \   / /\/_//___/ /\ \ \         / / /\ \_\ ",
            @"    / /_/_ \/_// / /    / / / ______   / / /         / /_/_ \/_/  / / /\ \ \ / / /   \___\/ / / /        / /_/_ \/_/ ",
            @"   / /____/\  / / /    / / / /\_____\ / / /         / /____/\    / / /  \/_// / /          / / /        / /____/\    ",
            @"  / /\____\/ / / /    / / /  \/____ // / / ____    / /\____\/   / / /      / / /          / / /    _   / /\____\/    ",
            @" / / /   ___/ / /__  / / /_____/ / // /_/_/ ___/\ / / /______  / / /   ___/ / /__         \ \ \__/\_\ / / /______    ",
            @"/ / /   /\__\/_/___\/ / /______\/ //_______/\__\// / /_______\/_/ /   /\__\/_/___\         \ \___\/ // / /_______\   ",
            @"\/_/    \/_________/\/___________/ \_______\/    \/__________/\_\/    \/_________/          \/___/_/ \/__________/   ");

        Test("graffiti", "Hello, World!",
            @"  ___ ___         .__  .__               __      __            .__       .___._.",
            @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
            @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
            @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
            @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
            @"       \/       \/                   )/        \/                         \/  \/");

        void TestFont(FigletizeFont font, string s, params string[] expected) =>
            Test(font.Name, s, expected);

        void Test(string fontName, string s, params string[] expected)
        {
            var output = FigletTools.RenderFiglet(s, fontName);
            var actual = output.Split(new[] { "\n" }, StringSplitOptions.None);
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                if (expected[i] == actual[i])
                    continue;
                if (expected[i].Length != actual[i].Length)
                {
                    _output.WriteLine("Expected:\n" + string.Join("\n", expected));
                    _output.WriteLine("Actual:\n" + output);
                    Assert.Fail($"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");
                }

                for (var x = 0; x < expected[i].Length; x++)
                {
                    if (expected[i][x] != actual[i][x])
                    {
                        _output.WriteLine("Expected:\n" + string.Join("\n", expected));
                        _output.WriteLine("Actual:\n" + output);
                        Assert.Fail($"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                    }
                }
            }
        }
    }
}
