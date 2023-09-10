// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Reflection;

namespace Figletize;

/// <summary>
/// Collection of bundled fonts, ready for use.
/// </summary>
/// <remarks>
/// Fonts are lazily loaded upon property access. Only the fonts you use will be loaded.
/// <para />
/// Fonts are stored as an embedded ZIP archive within the assembly.
/// </remarks>
public static class FigletizeFonts
{
    private static readonly ConcurrentDictionary<string, FigletizeFont> _fontByName = new(StringComparer.Ordinal);
    private static readonly StringPool _stringPool = new();

#pragma warning disable CS1591
    public static FigletizeFont OneRow => GetByName("1row");
    public static FigletizeFont ThreeD => GetByName("3-d");
    public static FigletizeFont ThreeDDiagonal => GetByName("3d_diagonal");
    public static FigletizeFont ThreeByFive => GetByName("3x5");
    public static FigletizeFont FourMax => GetByName("4max");
    public static FigletizeFont FiveLineOblique => GetByName("5lineoblique");
    public static FigletizeFont Acrobatic => GetByName("acrobatic");
    public static FigletizeFont Alligator => GetByName("alligator");
    public static FigletizeFont Alligator2 => GetByName("alligator2");
    public static FigletizeFont Alligator3 => GetByName("alligator3");
    public static FigletizeFont Alpha => GetByName("alpha");
    public static FigletizeFont Alphabet => GetByName("alphabet");
    public static FigletizeFont Amc3Line => GetByName("amc3line");
    public static FigletizeFont Amc3Liv1 => GetByName("amc3liv1");
    public static FigletizeFont AmcAaa01 => GetByName("amcaaa01");
    public static FigletizeFont AmcNeko => GetByName("amcneko");
    public static FigletizeFont AmcRazor2 => GetByName("amcrazo2");
    public static FigletizeFont AmcRazor => GetByName("amcrazor");
    public static FigletizeFont AmcSlash => GetByName("amcslash");
    public static FigletizeFont AmcSlder => GetByName("amcslder");
    public static FigletizeFont AmcThin => GetByName("amcthin");
    public static FigletizeFont AmcTubes => GetByName("amctubes");
    public static FigletizeFont AmcUn1 => GetByName("amcun1");
    public static FigletizeFont Arrows => GetByName("arrows");
    public static FigletizeFont AsciiNewroman => GetByName("ascii_new_roman");
    public static FigletizeFont Avatar => GetByName("avatar");
    public static FigletizeFont B1FF => GetByName("B1FF");
    public static FigletizeFont Banner => GetByName("banner");
    public static FigletizeFont Banner3 => GetByName("banner3");
    public static FigletizeFont Banner3D => GetByName("banner3-D");
    public static FigletizeFont Banner4 => GetByName("banner4");
    public static FigletizeFont BarbWire => GetByName("barbwire");
    public static FigletizeFont Basic => GetByName("basic");
    public static FigletizeFont Bear => GetByName("bear");
    public static FigletizeFont Bell => GetByName("bell");
    public static FigletizeFont Benjamin => GetByName("benjamin");
    public static FigletizeFont Big => GetByName("big");
    public static FigletizeFont BigChief => GetByName("bigchief");
    public static FigletizeFont BigFig => GetByName("bigfig");
    public static FigletizeFont Binary => GetByName("binary");
    public static FigletizeFont Block => GetByName("block");
    public static FigletizeFont Blocks => GetByName("blocks");
    public static FigletizeFont Bolger => GetByName("bolger");
    public static FigletizeFont Braced => GetByName("braced");
    public static FigletizeFont Bright => GetByName("bright");
    public static FigletizeFont Broadway => GetByName("broadway");
    public static FigletizeFont BroadwayKB => GetByName("broadway_kb");
    public static FigletizeFont Bubble => GetByName("bubble");
    public static FigletizeFont Bulbhead => GetByName("bulbhead");
    public static FigletizeFont Caligraphy2 => GetByName("calgphy2");
    public static FigletizeFont Caligraphy => GetByName("caligraphy");
    public static FigletizeFont Cards => GetByName("cards");
    public static FigletizeFont CatWalk => GetByName("catwalk");
    public static FigletizeFont Chiseled => GetByName("chiseled");
    public static FigletizeFont Chunky => GetByName("chunky");
    public static FigletizeFont Coinstak => GetByName("coinstak");
    public static FigletizeFont Cola => GetByName("cola");
    public static FigletizeFont Colossal => GetByName("colossal");
    public static FigletizeFont Computer => GetByName("computer");
    public static FigletizeFont Contessa => GetByName("contessa");
    public static FigletizeFont Contrast => GetByName("contrast");
    public static FigletizeFont Cosmic => GetByName("cosmic");
    public static FigletizeFont Cosmike => GetByName("cosmike");
    public static FigletizeFont Crawford => GetByName("crawford");
    public static FigletizeFont Crazy => GetByName("crazy");
    public static FigletizeFont Cricket => GetByName("cricket");
    public static FigletizeFont Cursive => GetByName("cursive");
    public static FigletizeFont CyberLarge => GetByName("cyberlarge");
    public static FigletizeFont CyberMedium => GetByName("cybermedium");
    public static FigletizeFont CyberSmall => GetByName("cybersmall");
    public static FigletizeFont Cygnet => GetByName("cygnet");
    public static FigletizeFont DANC4 => GetByName("DANC4");
    public static FigletizeFont DancingFont => GetByName("dancingfont");
    public static FigletizeFont Decimal => GetByName("decimal");
    public static FigletizeFont DefLeppard => GetByName("defleppard");
    public static FigletizeFont Diamond => GetByName("diamond");
    public static FigletizeFont DietCola => GetByName("dietcola");
    public static FigletizeFont Digital => GetByName("digital");
    public static FigletizeFont Doh => GetByName("doh");
    public static FigletizeFont Doom => GetByName("doom");
    public static FigletizeFont DosRebel => GetByName("dosrebel");
    public static FigletizeFont DotMatrix => GetByName("dotmatrix");
    public static FigletizeFont Double => GetByName("double");
    public static FigletizeFont DoubleShorts => GetByName("doubleshorts");
    public static FigletizeFont DRPepper => GetByName("drpepper");
    public static FigletizeFont DWhistled => GetByName("dwhistled");
    public static FigletizeFont EftiChess => GetByName("eftichess");
    public static FigletizeFont EftiFont => GetByName("eftifont");
    public static FigletizeFont EftiPiti => GetByName("eftipiti");
    public static FigletizeFont EftiRobot => GetByName("eftirobot");
    public static FigletizeFont EftiItalic => GetByName("eftitalic");
    public static FigletizeFont EftiWall => GetByName("eftiwall");
    public static FigletizeFont EftiWater => GetByName("eftiwater");
    public static FigletizeFont Epic => GetByName("epic");
    public static FigletizeFont Fender => GetByName("fender");
    public static FigletizeFont Filter => GetByName("filter");
    public static FigletizeFont FireFontK => GetByName("fire_font-k");
    public static FigletizeFont FireFontS => GetByName("fire_font-s");
    public static FigletizeFont Flipped => GetByName("flipped");
    public static FigletizeFont FlowerPower => GetByName("flowerpower");
    public static FigletizeFont FourTops => GetByName("fourtops");
    public static FigletizeFont Fraktur => GetByName("fraktur");
    public static FigletizeFont FunFace => GetByName("funface");
    public static FigletizeFont FunFaces => GetByName("funfaces");
    public static FigletizeFont Fuzzy => GetByName("fuzzy");
    public static FigletizeFont Georgia16 => GetByName("georgi16");
    public static FigletizeFont Georgia11 => GetByName("Georgia11");
    public static FigletizeFont Ghost => GetByName("ghost");
    public static FigletizeFont Ghoulish => GetByName("ghoulish");
    public static FigletizeFont Glenyn => GetByName("glenyn");
    public static FigletizeFont Goofy => GetByName("goofy");
    public static FigletizeFont Gothic => GetByName("gothic");
    public static FigletizeFont Graceful => GetByName("graceful");
    public static FigletizeFont Gradient => GetByName("gradient");
    public static FigletizeFont Graffiti => GetByName("graffiti");
    public static FigletizeFont Greek => GetByName("greek");
    public static FigletizeFont HeartLeft => GetByName("heart_left");
    public static FigletizeFont HeartRight => GetByName("heart_right");
    public static FigletizeFont Henry3d => GetByName("henry3d");
    public static FigletizeFont Hex => GetByName("hex");
    public static FigletizeFont Hieroglyphs => GetByName("hieroglyphs");
    public static FigletizeFont Hollywood => GetByName("hollywood");
    public static FigletizeFont HorizontalLeft => GetByName("horizontalleft");
    public static FigletizeFont HorizontalRight => GetByName("horizontalright");
    public static FigletizeFont ICL1900 => GetByName("ICL-1900");
    public static FigletizeFont Impossible => GetByName("impossible");
    public static FigletizeFont Invita => GetByName("invita");
    public static FigletizeFont Isometric1 => GetByName("isometric1");
    public static FigletizeFont Isometric2 => GetByName("isometric2");
    public static FigletizeFont Isometric3 => GetByName("isometric3");
    public static FigletizeFont Isometric4 => GetByName("isometric4");
    public static FigletizeFont Italic => GetByName("italic");
    public static FigletizeFont Ivrit => GetByName("ivrit");
    public static FigletizeFont Jacky => GetByName("jacky");
    public static FigletizeFont Jazmine => GetByName("jazmine");
    public static FigletizeFont Jerusalem => GetByName("jerusalem");
    public static FigletizeFont Katakana => GetByName("katakana");
    public static FigletizeFont Kban => GetByName("kban");
    public static FigletizeFont Keyboard => GetByName("keyboard");
    public static FigletizeFont Knob => GetByName("knob");
    public static FigletizeFont Konto => GetByName("konto");
    public static FigletizeFont KontoSlant => GetByName("kontoslant");
    public static FigletizeFont Larry3d => GetByName("larry3d");
    public static FigletizeFont Lcd => GetByName("lcd");
    public static FigletizeFont Lean => GetByName("lean");
    public static FigletizeFont Letters => GetByName("letters");
    public static FigletizeFont LilDevil => GetByName("lildevil");
    public static FigletizeFont LineBlocks => GetByName("lineblocks");
    public static FigletizeFont Linux => GetByName("linux");
    public static FigletizeFont LockerGnome => GetByName("lockergnome");
    public static FigletizeFont Madrid => GetByName("madrid");
    public static FigletizeFont Marquee => GetByName("marquee");
    public static FigletizeFont MaxFour => GetByName("maxfour");
    public static FigletizeFont Merlin1 => GetByName("merlin1");
    public static FigletizeFont Merlin2 => GetByName("merlin2");
    public static FigletizeFont Mike => GetByName("mike");
    public static FigletizeFont Mini => GetByName("mini");
    public static FigletizeFont Mirror => GetByName("mirror");
    public static FigletizeFont Mnemonic => GetByName("mnemonic");
    public static FigletizeFont Modular => GetByName("modular");
    public static FigletizeFont Morse => GetByName("morse");
    public static FigletizeFont Morse2 => GetByName("morse2");
    public static FigletizeFont Moscow => GetByName("moscow");
    public static FigletizeFont Mshebrew210 => GetByName("mshebrew210");
    public static FigletizeFont Muzzle => GetByName("muzzle");
    public static FigletizeFont NancyJ => GetByName("nancyj");
    public static FigletizeFont NancyJFancy => GetByName("nancyj-fancy");
    public static FigletizeFont NancyJImproved => GetByName("nancyj-improved");
    public static FigletizeFont NancyJUnderlined => GetByName("nancyj-underlined");
    public static FigletizeFont Nipples => GetByName("nipples");
    public static FigletizeFont NScript => GetByName("nscript");
    public static FigletizeFont NTGreek => GetByName("ntgreek");
    public static FigletizeFont NVScript => GetByName("nvscript");
    public static FigletizeFont O8 => GetByName("o8");
    public static FigletizeFont Octal => GetByName("octal");
    public static FigletizeFont Ogre => GetByName("ogre");
    public static FigletizeFont OldBanner => GetByName("oldbanner");
    public static FigletizeFont OS2 => GetByName("os2");
    public static FigletizeFont Pawp => GetByName("pawp");
    public static FigletizeFont Peaks => GetByName("peaks");
    public static FigletizeFont PeaksSlant => GetByName("peaksslant");
    public static FigletizeFont Pebbles => GetByName("pebbles");
    public static FigletizeFont Pepper => GetByName("pepper");
    public static FigletizeFont Poison => GetByName("poison");
    public static FigletizeFont Puffy => GetByName("puffy");
    public static FigletizeFont Puzzle => GetByName("puzzle");
    public static FigletizeFont Pyramid => GetByName("pyramid");
    public static FigletizeFont Rammstein => GetByName("rammstein");
    public static FigletizeFont Rectangles => GetByName("rectangles");
    public static FigletizeFont RedPhoenix => GetByName("red_phoenix");
    public static FigletizeFont Relief => GetByName("relief");
    public static FigletizeFont Relief2 => GetByName("relief2");
    public static FigletizeFont Rev => GetByName("rev");
    public static FigletizeFont Reverse => GetByName("reverse");
    public static FigletizeFont Roman => GetByName("roman");
    public static FigletizeFont Rot13 => GetByName("rot13");
    public static FigletizeFont Rotated => GetByName("rotated");
    public static FigletizeFont Rounded => GetByName("rounded");
    public static FigletizeFont RowanCap => GetByName("rowancap");
    public static FigletizeFont Rozzo => GetByName("rozzo");
    public static FigletizeFont Runic => GetByName("runic");
    public static FigletizeFont Runyc => GetByName("runyc");
    public static FigletizeFont SantaClara => GetByName("santaclara");
    public static FigletizeFont SBlood => GetByName("sblood");
    public static FigletizeFont Script => GetByName("script");
    public static FigletizeFont ScriptSlant => GetByName("slscript");
    public static FigletizeFont SerifCap => GetByName("serifcap");
    public static FigletizeFont Shadow => GetByName("shadow");
    public static FigletizeFont Shimrod => GetByName("shimrod");
    public static FigletizeFont Short => GetByName("short");
    public static FigletizeFont Slant => GetByName("slant");
    public static FigletizeFont Slide => GetByName("slide");
    public static FigletizeFont Small => GetByName("small");
    public static FigletizeFont SmallCaps => GetByName("smallcaps");
    public static FigletizeFont IsometricSmall => GetByName("smisome1");
    public static FigletizeFont KeyboardSmall => GetByName("smkeyboard");
    public static FigletizeFont PoisonSmall => GetByName("smpoison");
    public static FigletizeFont ScriptSmall => GetByName("smscript");
    public static FigletizeFont ShadowSmall => GetByName("smshadow");
    public static FigletizeFont SlantSmall => GetByName("smslant");
    public static FigletizeFont TengwarSmall => GetByName("smtengwar");
    public static FigletizeFont Soft => GetByName("soft");
    public static FigletizeFont Speed => GetByName("speed");
    public static FigletizeFont Spliff => GetByName("spliff");
    public static FigletizeFont SRelief => GetByName("s-relief");
    public static FigletizeFont Stacey => GetByName("stacey");
    public static FigletizeFont Stampate => GetByName("stampate");
    public static FigletizeFont Stampatello => GetByName("stampatello");
    public static FigletizeFont Standard => GetByName("standard");
    public static FigletizeFont Starstrips => GetByName("starstrips");
    public static FigletizeFont Starwars => GetByName("starwars");
    public static FigletizeFont Stellar => GetByName("stellar");
    public static FigletizeFont Stforek => GetByName("stforek");
    public static FigletizeFont Stop => GetByName("stop");
    public static FigletizeFont Straight => GetByName("straight");
    public static FigletizeFont SubZero => GetByName("sub-zero");
    public static FigletizeFont Swampland => GetByName("swampland");
    public static FigletizeFont Swan => GetByName("swan");
    public static FigletizeFont Sweet => GetByName("sweet");
    public static FigletizeFont Tanja => GetByName("tanja");
    public static FigletizeFont Tengwar => GetByName("tengwar");
    public static FigletizeFont Term => GetByName("term");
    public static FigletizeFont Test1 => GetByName("test1");
    public static FigletizeFont Thick => GetByName("thick");
    public static FigletizeFont Thin => GetByName("thin");
    public static FigletizeFont ThreePoint => GetByName("threepoint");
    public static FigletizeFont Ticks => GetByName("ticks");
    public static FigletizeFont TicksSlant => GetByName("ticksslant");
    public static FigletizeFont Tiles => GetByName("tiles");
    public static FigletizeFont TinkerToy => GetByName("tinker-toy");
    public static FigletizeFont Tombstone => GetByName("tombstone");
    public static FigletizeFont Train => GetByName("train");
    public static FigletizeFont Trek => GetByName("trek");
    public static FigletizeFont Tsalagi => GetByName("tsalagi");
    public static FigletizeFont Tubular => GetByName("tubular");
    public static FigletizeFont Twisted => GetByName("twisted");
    public static FigletizeFont TwoPoint => GetByName("twopoint");
    public static FigletizeFont Univers => GetByName("univers");
    public static FigletizeFont UsaFlag => GetByName("usaflag");
    public static FigletizeFont Varsity => GetByName("varsity");
    public static FigletizeFont Wavy => GetByName("wavy");
    public static FigletizeFont Weird => GetByName("weird");
    public static FigletizeFont WetLetter => GetByName("wetletter");
    public static FigletizeFont Whimsy => GetByName("whimsy");
    public static FigletizeFont Wow => GetByName("wow");
#pragma warning restore CS1591

    private static FigletizeFont GetByName(string name)
    {
        return _fontByName.GetOrAdd(name, FontFactory);

        static FigletizeFont FontFactory(string name)
        {
            var font = ParseEmbeddedFont(name);

            if (font is null)
                throw new FigletizeException($"No embedded font exists with name \"{name}\".");

            return font;
        }
    }

    /// <summary>
    /// Attempts to load the font with specified name.
    /// </summary>
    /// <param name="name">the name of the font. Case-sensitive.</param>
    /// <returns>The font if found, otherwise <see langword="null"/>.</returns>
    public static FigletizeFont? TryGetByName(string name)
    {
        if (_fontByName.TryGetValue(name, out var font))
            return font;

        font = ParseEmbeddedFont(name);

        if (font is not null)
            _fontByName.TryAdd(name, font);

        return font;
    }

    private static FigletizeFont? ParseEmbeddedFont(string name)
    {
        using var stream = typeof(FigletizeFonts).GetTypeInfo().Assembly.GetManifestResourceStream("Figletize.Fonts.zip");

        if (stream is null)
            throw new FigletizeException("Unable to open embedded font archive.");

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        var entry = zip.GetEntry(name + ".flf");

        if (entry == null)
            return null;

        using var entryStream = entry.Open();

        return FigletizeFontParser.Parse(entryStream, _stringPool);
    }
}
