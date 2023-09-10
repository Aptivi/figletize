// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
// Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;

namespace Figletize;

/// <summary>
/// Type for exceptions raised by Figletize.
/// </summary>
public sealed class FigletizeException : Exception
{
    /// <summary>
    /// Constructs a new Figletize exception.
    /// </summary>
    /// <param name="message">A message explaining the exception.</param>
    public FigletizeException(string message) : base(message)
    { }
}
