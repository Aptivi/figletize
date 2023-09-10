// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.IO.Compression;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Figletize.Tests;

public sealed class FigletizeFontParserTest
{
    private readonly ITestOutputHelper _output;

    public FigletizeFontParserTest(ITestOutputHelper output) => _output = output;
}
