using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aPC.Client.Morse
{
  public class ArgumentReader
  {
    public ArgumentReader(string xiArguments)
    {
      if (string.IsNullOrEmpty(xiArguments))
      {
        throw new UsageException("No arguments specified");
      }

      mSwitches = new List<string>();
      mSettings = new Settings();

      SplitArguments(xiArguments);
    }

    /// <remarks>
    ///   We expect the argument string to always have the message right at the end.
    /// </remarks>
    private void SplitArguments(string xiArguments)
    {
      var lReadingMessage = false;
      var lMessage = "";

      foreach (var lArgument in xiArguments.Split(' '))
      {
        if (lReadingMessage)
        {
          lMessage += " " + lArgument;
        }
        else if (lArgument.ToLower().StartsWith(@"/m"))
        {
          lReadingMessage = true;
          lMessage = lArgument.Remove(0, 3);
        }
        else if (lArgument.StartsWith(@"/"))
        {
          mSwitches.Add(lArgument);
        }
      }

      mMessage = lMessage;
    }

    public Settings Read()
    {
      ReadMessage();
      ReadSwitches();
      return mSettings;
    }

    private void ReadMessage()
    {
      if (!IsMessageValid())
      {
        throw new UsageException("Invalid message specified: " + mMessage);
      }

      mSettings.Message = mMessage;
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
      if (string.IsNullOrEmpty(mMessage))
      {
        return false;
      }

      var lMatch = mMorsePattern.Match(mMessage);

      if (!lMatch.Success || lMatch.Length != mMessage.Length)
      {
        return false;
      }
      return true;
    }

    private void ReadSwitches()
    {
      foreach (var lSwitch in mSwitches)
      {
        try
        {
          ReadSwitch(lSwitch);
        }
        catch (Exception e)
        {
          throw new UsageException("Error when reading Switch " + lSwitch + " : " + e);
        }
      }
    }

    private void ReadSwitch(string xiSwitch)
    {
      switch (xiSwitch.Substring(0, 2).ToLower())
      {
        case @"/d":
          mSettings.RepeatMessage = true;
          break;
        case @"/r":
          mSettings.RumblesEnabled = true;
          break;
        case @"/l":
          mSettings.LightsEnabled = false;
          break;
        case @"/c":
          mSettings.Colour = ParseColour(xiSwitch);
          break;
        case @"/u":
          mSettings.UnitLength = ParseUnitLength(xiSwitch);
          break;
      }
    }

    private Light ParseColour(string xiArgument)
    {
      var lLightComponents = xiArgument
        .Remove(0, 3).Split(',')
        .Select(colour => float.Parse(colour)).ToList();

      if (lLightComponents.Count != 3 || 
          lLightComponents.Any(colour => IsOutOfRange(colour)))
      {
        throw new ArgumentOutOfRangeException();
      }

      return new Light()
      {
        Red = lLightComponents[0],
        Green = lLightComponents[1],
        Blue = lLightComponents[2]
      };
    }

    private bool IsOutOfRange(float xiColour)
    {
      return xiColour < 0 || xiColour > 1;
    }

    private int ParseUnitLength(string xiArgument)
    {
      var lLength = xiArgument.Remove(0, 3);
      return int.Parse(lLength);
    }

    private Settings mSettings;
    protected List<string> mSwitches;
    protected string mMessage;

    private Regex mMorsePattern = new Regex(@"[\w\s\.\,\?\'\!\/\(\)\&\:\;\=\+\""\$\@]+", RegexOptions.Compiled);
  }
}
