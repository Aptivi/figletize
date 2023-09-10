// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.IO.Compression;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Figletize.Tests;

public sealed class FigletizeFontParserTest
{
    private readonly ITestOutputHelper _output;

    public FigletizeFontParserTest(ITestOutputHelper output) => _output = output;

    [Fact]
    public void ParseAllEmbeddedFonts()
    {
        using var stream = typeof(FigletizeFonts).GetTypeInfo().Assembly.GetManifestResourceStream("Figletize.Fonts.zip");

        Assert.NotNull(stream);

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            _output.WriteLine($"Parsing: {entry.Name}");

            using var entryStream = entry.Open();

            FigletizeFontParser.Parse(entryStream);
        }
    }
}
