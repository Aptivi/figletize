﻿// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Figletize.Utilities;
using Xunit;

namespace Figletize.Tests;

public class StringPoolTest
{
    [Fact]
    public void PoolsReferences()
    {
        var pool = new StringPool();

        var s1 = "s";
        var s2 = "S".ToLower();

        Assert.NotSame(s1, s2);
        Assert.Equal(s1, s2);

        Assert.Same(s1, pool.Pool(s1));
        Assert.Same(s1, pool.Pool(s1));
        Assert.Same(s1, pool.Pool(s2));
        Assert.Same(s1, pool.Pool(s2));
    }
}
