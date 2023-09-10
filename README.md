```                       
 _____ _         _     
|   __|_|___ ___| |___ 
|   __| | . | . | | -_|
|__|  |_|_  |_  |_|___|
        |___|___|      
```

[![Figletize Build Status](https://ci.appveyor.com/api/projects/status/2vvwieg2ou7pkhst?svg=true)](https://ci.appveyor.com/project/drewnoakes/figgle)
[![Figletize NuGet version](https://img.shields.io/nuget/v/Figletize)](https://www.nuget.org/packages/Figletize/)
[![Figletize NuGet download count](https://img.shields.io/nuget/dt/Figletize)](https://www.nuget.org/packages/Figletize/)

## ASCII banner generation for .NET

```c#
Console.WriteLine(
    FigletizeFonts.Standard.Render("Hello, World!"));
```

Produces...

```
  _   _      _ _         __        __         _     _ _
 | | | | ___| | | ___    \ \      / /__  _ __| | __| | |
 | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |
 |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|
 |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)
                     |/
```

Alternatively, use Figletize's source generator to generate output during compilation, so you don't need to ship Figletize binaries with your app. See below for details.

The library bundles 265 [FIGlet](http://www.figlet.org/) [fonts](http://www.jave.de/figlet/fonts.html). You can add your own if that's not enough! 

## Installation

Available via [NuGet](https://www.nuget.org/packages/Figletize/):

>Install-Package Figletize

Targets .NET Standard 2.0, so runs pretty much anywhere. If you require .NET Standard 1.3, use package version 0.4.1.

## Other samples

Using `FigletizeFonts.Graffiti`:

```
  ___ ___         .__  .__               __      __            .__       .___._.
 /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |
/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |
\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|
 \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __
       \/       \/                   )/        \/                         \/  \/
```

Using `FigletizeFonts.ThreePoint`:

```
|_| _ || _    \    / _  _| _||
| |(/_||(_),   \/\/ (_)| |(_|.
```

Using `FigletizeFonts.Ogre`:

```
            _ _          __    __           _     _   _ 
  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \
 / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /
/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ 
\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   
                    |/                                  
```

Using `FigletizeFonts.Rectangles`:

```
                                            __ 
 _____     _ _          _ _ _         _   _|  |
|  |  |___| | |___     | | | |___ ___| |_| |  |
|     | -_| | | . |_   | | | | . |  _| | . |__|
|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|
                  |_|                          
```

Using `FigletizeFonts.Slant`:

```
    __  __     ____           _       __           __    ____
   / / / /__  / / /___       | |     / /___  _____/ /___/ / /
  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / 
 / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  
/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   
                     |/                                      
```

## Loading external fonts

Figletize ships with a bunch of fonts (in the `FigletizeFonts` class). If you prefer, you can load your own.

Here's an example that loads a font from disk and renders some text with it:

```c#
using var fontStream = System.IO.File.OpenRead("myfont.flf");
var font = FigletizeFontParser.Parse(fontStream);
var text = font.Render("Hello World, from My Font");
```

## Using the Source Generator

If the text you want Figletize to render is known at compile time, this section is for you.

Instead of having Figletize render text at runtime, you can use Figletize's _source generator_ to
do the rendering at compile time. This has two benefits:

- Faster runtime performance, as no Figletize code is executed
- Less memory consumption, as no Figletize code is loaded
- Smaller application footprint, as you don't need to ship Figletize binaries

In your code:

```c#
using Figletize;

namespace MyNamespace
{
    [GenerateFigletizeText("HelloWorldString", "blocks", "Hello world")]
    internal partial class MyClass
    {
    }
}
```

By adding this attribute to a partial class, Figletize will automatically generate the corresponding
member in another part of the partial class, resembling:

```c#
namespace MyNamespace
{
    partial class MyClass
    {
        public static string HelloWorldString { get; } = "...rendered text here...";
    }
}
```
