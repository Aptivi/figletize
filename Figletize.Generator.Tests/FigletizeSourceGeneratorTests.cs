﻿// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Xunit.Sdk;

namespace Figletize.Generator.Tests;

public class FigletizeSourceGeneratorTests
{
    private readonly ImmutableArray<MetadataReference> _references;
    private const string Header =
        """
        // Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
        // Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

        // <auto-generated>
        //     This code was generated by Figletize.Generator.
        //
        //     Changes to this file may cause incorrect behavior and will be lost if
        //     the code is regenerated.
        // </auto-generated>

        """;

    public FigletizeSourceGeneratorTests()
    {
        _references = GetReferences().ToImmutableArray();

        static IEnumerable<MetadataReference> GetReferences()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                {
                    yield return MetadataReference.CreateFromFile(assembly.Location);
                }
            }
        }
    }

    [Fact]
    public void SimpleCase_Class()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_StaticClass()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal static partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_GlobalNamespace()
    {
        string source = """
            [GenerateFigletizeText("Member", "stacey", "Figletize")]
            internal static partial class DemoUsage
            {
            }
            """;
        string expected =
            $$"""
            {{Header}}
                partial class DemoUsage
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }

            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void MultipleStrings()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("HelloWorldString", "blocks", "Hello world")]
                [GenerateFigletizeText("FigletizeString", "stacey", "Figletize")]
                internal static partial class DemoUsage
                {
                }
            }
            """;
        string expected =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string HelloWorldString { get; } = @" .----------------.  .----------------.  .----------------.  .----------------.  .----------------.   .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
            | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
            | |  ____  ____  | || |  _________   | || |   _____      | || |   _____      | || |     ____     | | | | _____  _____ | || |     ____     | || |  _______     | || |   _____      | || |  ________    | |
            | | |_   ||   _| | || | |_   ___  |  | || |  |_   _|     | || |  |_   _|     | || |   .'    `.   | | | ||_   _||_   _|| || |   .'    `.   | || | |_   __ \    | || |  |_   _|     | || | |_   ___ `.  | |
            | |   | |__| |   | || |   | |_  \_|  | || |    | |       | || |    | |       | || |  /  .--.  \  | | | |  | | /\ | |  | || |  /  .--.  \  | || |   | |__) |   | || |    | |       | || |   | |   `. \ | |
            | |   |  __  |   | || |   |  _|  _   | || |    | |   _   | || |    | |   _   | || |  | |    | |  | | | |  | |/  \| |  | || |  | |    | |  | || |   |  __ /    | || |    | |   _   | || |   | |    | | | |
            | |  _| |  | |_  | || |  _| |___/ |  | || |   _| |__/ |  | || |   _| |__/ |  | || |  \  `--'  /  | | | |  |   /\   |  | || |  \  `--'  /  | || |  _| |  \ \_  | || |   _| |__/ |  | || |  _| |___.' / | |
            | | |____||____| | || | |_________|  | || |  |________|  | || |  |________|  | || |   `.____.'   | | | |  |__/  \__|  | || |   `.____.'   | || | |____| |___| | || |  |________|  | || | |________.'  | |
            | |              | || |              | || |              | || |              | || |              | | | |              | || |              | || |              | || |              | || |              | |
            | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
             '----------------'  '----------------'  '----------------'  '----------------'  '----------------'   '----------------'  '----------------'  '----------------'  '----------------'  '----------------' ";
                    public static string FigletizeString { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;

        ValidateOutput(source, expected);
    }

    [Fact]
    public void SimpleCase_TwoClasses_SameNamespace()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal partial class Demo1
                {
                }

                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal partial class Demo2
                {
                }
            }
            """;
        string expected1 =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class Demo1
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;
        string expected2 =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class Demo2
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;

        ValidateOutput(source, expected1, expected2);
    }

    [Fact]
    public void SimpleCase_TwoClasses_DifferentNamespace()
    {
        string source =
            """
            namespace Test.Namespace1
            {
                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal partial class Demo
                {
                }
            }

            namespace Test.Namespace2
            {
                [GenerateFigletizeText("Member", "stacey", "Figletize")]
                internal partial class Demo
                {
                }
            }
            """;
        string expected1 =
            $$"""
            {{Header}}
            namespace Test.Namespace1
            {
                partial class Demo
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;
        string expected2 =
            $$"""
            {{Header}}
            namespace Test.Namespace2
            {
                partial class Demo
                {
                    public static string Member { get; } = @"______________________   _________________________________
            7     77  77     77  7   7     77      77  77     77     7
            |  ___!|  ||   __!|  |   |  ___!!__  __!|  |!___  ||  ___!
            |  __| |  ||  !  7|  !___|  __|_  7  7  |  ||   __!|  __|_
            |  7   |  ||     ||     7|     7  |  |  |  ||     7|     7
            !__!   !__!!_____!!_____!!_____!  !__!  !__!!_____!!_____!";
                }
            }
            """;

        ValidateOutput(source, expected1, expected2);
    }

    [Fact]
    public void InvalidFontName()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "unknown-font", "Bar")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.UnknownFontNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("A font with name 'unknown-font' was not found", diagnostic.GetMessage());
    }

    [Fact]
    public void InvalidMemberName()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("With Space", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.InvalidMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("The string 'With Space' is not a valid member name", diagnostic.GetMessage());
    }

    [Fact]
    public void DuplicateMemberName_SamePart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());

        string expected =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Foo { get; } = @"_____________________
            7     77     77     7
            |  ___!|  7  ||  7  |
            |  __| |  |  ||  |  |
            |  7   |  !  ||  !  |
            !__!   !_____!!_____!";
                }
            }
            """;

        Assert.Equal(expected, compilation.SyntaxTrees.Last().ToString(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void MemberAlreadyExists_SamePart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                    public static void Foo() {}
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());
    }

    [Fact]
    public void MemberAlreadyExists_DifferentPart()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }

                internal static partial class DemoUsage
                {
                    public static void Foo() {}
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());
    }

    [Fact]
    public void DuplicateMemberName_DifferentParts()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }

                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());

        string expected =
            $$"""
            {{Header}}
            namespace Test.Namespace
            {
                partial class DemoUsage
                {
                    public static string Foo { get; } = @"_____________________
            7     77     77     7
            |  ___!|  7  ||  7  |
            |  __| |  |  ||  |  |
            |  7   |  !  ||  !  |
            !__!   !_____!!_____!";
                }
            }
            """;

        Assert.Equal(expected, compilation.SyntaxTrees.Last().ToString(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void TypeIsNotPartial()
    {
        string source =
            """
            namespace Test.Namespace
            {
                [GenerateFigletizeText("Foo", "stacey", "Foo")]
                internal class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.TypeIsNotPartialDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Type 'DemoUsage' must be partial", diagnostic.GetMessage());
    }

    [Fact]
    public void NestedTypeIsNotSupported()
    {
        string source =
            """
            namespace Test.Namespace
            {
                internal partial class Outer
                {
                    [GenerateFigletizeText("Foo", "stacey", "Foo")]
                    internal partial class Inner
                    {
                    }
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(FigletizeSourceGenerator.NestedTypeIsNotSupportedDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Unable to generate Figletize text for nested type 'Inner'. Generation is only supported for non-nested types.", diagnostic.GetMessage());
    }

    private void ValidateOutput(string source, params string[] outputs)
    {
        var (compilation, diagnostics) = RunGenerator(source);

        ValidateNoErrors(diagnostics);

        Assert.Equal(
            new[] { source, FigletizeSourceGenerator.AttributeSource }.Concat(outputs),
            compilation.SyntaxTrees.Select(tree => tree.ToString()),
            NewlineIgnoreComparer.Instance);
    }

    private (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) RunGenerator(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var compilation = CSharpCompilation.Create(
            "testAssembly",
            new SyntaxTree[] { syntaxTree },
            _references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        ISourceGenerator generator = new FigletizeSourceGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);

        driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out var outputCompilation,
            out var generateDiagnostics);

        return (outputCompilation, generateDiagnostics);
    }

    private static void ValidateNoErrors(ImmutableArray<Diagnostic> diagnostics)
    {
        var errors = diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToList();

        if (errors.Any())
        {
            throw new XunitException(
                string.Join(
                    Environment.NewLine,
                    errors.Select(error => error.GetMessage())));
        }
    }

    private sealed class NewlineIgnoreComparer : IEqualityComparer<string>
    {
        public static NewlineIgnoreComparer Instance { get; } = new();

        public bool Equals(string x, string y)
        {
            return StringComparer.Ordinal.Equals(Normalize(x), Normalize(y));
        }

        public int GetHashCode(string obj)
        {
            return StringComparer.Ordinal.GetHashCode(Normalize(obj));
        }

        [return: NotNullIfNotNull("s")]
        private static string Normalize(string s) => s?.Replace("\r", "");
    }
}
