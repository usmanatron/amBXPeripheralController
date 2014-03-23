using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      Switches = new List<string>();
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
          Switches.Add(lArgument);
        }
      }

      Message = lMessage;
    }

    public Settings Read()
    {
      if (string.IsNullOrEmpty(Message))
      {
        throw new UsageException("Invalid Arguments");
      }

      var lSettings = new Settings();

      foreach (var lArgument in Switches)
      {
        switch (lArgument.ToLower())
        {
          case @"/d":
            lSettings.RepeatMessage = true;
            break;
          case @"/r":
            lSettings.RumblesEnabled = true;
            break;
          case @"/-l":
            lSettings.LightsEnabled = false;
            break;
        }
        
        if (lArgument.ToLower().StartsWith(@"/c"))
        {
          try
          {
            lSettings.Colour = ParseColour(lArgument);
          }
          catch (Exception e)
          {
            throw new UsageException("Exception when parsing light (" + lArgument + @"): " + e.Message);
          }
        }
      }

      return lSettings;
    }

    private Light ParseColour(string xiLight)
    {
      var lLightComponents = xiLight
        .Remove(0, 3).Split(',')
        .Select(colour => float.Parse(colour)).ToList();

      if (lLightComponents.Count != 3)
      {
        throw new ArgumentOutOfRangeException();
      }

      if (lLightComponents.Any(colour => IsOutOfRange(colour)))
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

    public List<string> Switches;
    public string Message;
  }
}
