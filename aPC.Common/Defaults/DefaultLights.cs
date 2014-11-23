using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultLights
  {
    public static readonly Light White = new Light { Intensity = 1, Red = 1, Green = 1, Blue = 1 };
    public static readonly Light Red = new Light { Intensity = 1, Red = 1, Green = 0, Blue = 0 };
    public static readonly Light Green = new Light { Intensity = 1, Red = 0, Green = 1, Blue = 0 };
    public static readonly Light Yellow = new Light { Intensity = 1, Red = 1, Green = 1, Blue = 0 };
    public static readonly Light Orange = new Light { Intensity = 1, Red = 1, Green = 0.5f, Blue = 0 };
    public static readonly Light Off = new Light { Intensity = 0, Red = 0, Green = 0, Blue = 0 };
    public static readonly Light Blue = new Light { Intensity = 1, Red = 0, Green = 0, Blue = 1 };
    public static readonly Light SoftPurple = new Light { Intensity = 0.5f, Red = 0.5f, Green = 0, Blue = 0.5f };
    public static readonly Light SoftPink = new Light { Intensity = 0.5f, Red = 0.5f, Green = 0, Blue = 1f };
    public static readonly Light StrongPurple = new Light { Intensity = 1f, Red = 1f, Green = 0.5f, Blue = 0.5f };
    public static readonly Light SoftYellow = new Light { Intensity = 0.75f, Red = 1f, Green = 1f, Blue = 0 };
    public static readonly Light JiraBlue = new Light { Intensity = 1f, Red = 0.33f, Green = 0.66f, Blue = 1 };
    public static readonly Light Indigo = new Light { Intensity = 1f, Red = 0.3f, Green = 0, Blue = 0.5f };
    public static readonly Light Violet = new Light { Intensity = 1f, Red = 0.56f, Green = 0, Blue = 1 };
  }
}