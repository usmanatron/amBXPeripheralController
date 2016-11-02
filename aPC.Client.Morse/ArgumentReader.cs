using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aPC.Client.Morse
{
  public class ArgumentReader
  {
    protected List<string> switches;
    protected string message;
    private readonly Regex morsePattern = new Regex(@"[\w\s\.\,\?\'\!\/\(\)\&\:\;\=\+\""\$\@]+", RegexOptions.Compiled);

    public ArgumentReader(string arguments)
    {
      if (string.IsNullOrEmpty(arguments))
      {
        throw new UsageException("No arguments specified");
      }

      switches = new List<string>();
      SplitArguments(arguments);
    }

    /// <remarks>
    ///   We expect the argument string to always have the message right at the end.
    /// </remarks>
    private void SplitArguments(string arguments)
    {
      var readingMessage = false;
      var message = "";

      foreach (var argument in arguments.Split(' '))
      {
        if (readingMessage)
        {
          message += " " + argument;
        }
        else if (argument.ToLower().StartsWith(@"/m"))
        {
          readingMessage = true;
          message = argument.Remove(0, 3);
        }
        else if (argument.StartsWith(@"/"))
        {
          switches.Add(argument);
        }
      }

      this.message = message;
    }

    public Settings Read()
    {
      ValidateMessage();
      var settings = new Settings(message);
      ReadSwitchesIntoSettings(settings);

      ThrowIfSettingsAreInvalid(settings);
      return settings;
    }

    private void ValidateMessage()
    {
      if (!IsMessageValid())
      {
        throw new UsageException("Invalid message specified: " + message);
      }
    }

    /// <summary>
    ///   Checks to ensure the message is present and
    ///   valid.
    /// </summary>
    /// <remarks>
    ///   The following characters are supported in Morse
    ///   code:
    ///   * The letters A-Z and numbers 0-9
    ///   * Space
    ///   * The following other symbols:
    ///     . , ? ' ! / ( ) & : ; = + _ " $ @
    /// </remarks>
    private bool IsMessageValid()
    {
      if (string.IsNullOrEmpty(message))
      {
        return false;
      }

      var match = morsePattern.Match(message);

      if (!match.Success || match.Length != message.Length)
      {
        return false;
      }
      return true;
    }

    private void ReadSwitchesIntoSettings(Settings settings)
    {
      foreach (var @switch in switches)
      {
        try
        {
          ReadSwitchIntoSettings(settings, @switch);
        }
        catch (Exception e)
        {
          throw new UsageException("Error when reading Switch " + @switch + " : " + e);
        }
      }
    }

    private void ReadSwitchIntoSettings(Settings settings, string @switch)
    {
      switch (@switch.Substring(0, 2).ToLower())
      {
        case @"/d":
          settings.RepeatMessage = true;
          break;

        case @"/r":
          settings.RumblesEnabled = true;
          break;

        case @"/l":
          settings.LightsEnabled = false;
          break;

        case @"/c":
          settings.Colour = ParseColour(@switch);
          break;

        case @"/u":
          settings.UnitLength = ParseUnitLength(@switch);
          break;
      }
    }

    private Light ParseColour(string argument)
    {
      var lightComponents = argument
        .Remove(0, 3).Split(',')
        .Select(float.Parse).ToList();

      if (lightComponents.Count != 3 || lightComponents.Any(IsOutOfRange))
      {
        throw new ArgumentOutOfRangeException();
      }

      return new Light
      {
        Red = lightComponents[0],
        Green = lightComponents[1],
        Blue = lightComponents[2],
        FadeTime = 10
      };
    }

    private bool IsOutOfRange(float colour)
    {
      return colour < 0 || colour > 1;
    }

    private int ParseUnitLength(string argument)
    {
      var lLength = argument.Remove(0, 3);
      return int.Parse(lLength);
    }

    private void ThrowIfSettingsAreInvalid(Settings settings)
    {
      if (!settings.LightsEnabled && !settings.RumblesEnabled)
      {
        throw new UsageException("Both lights and rumbles are disabled - nothing to show");
      }

      if (settings.Colour.Red == 0 &&
          settings.Colour.Green == 0 &&
          settings.Colour.Blue == 0)
      {
        throw new UsageException("Invalid colour specified");
      }
    }
  }
}