using aPC.Common.Client;

namespace aPC.Client.Morse
{
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string description)
      : base(description)
    {
    }

    protected override string Usage()
    {
      return @"
Usage: aPC.Client.Morse [Arguments] /M:message

Displays a message in Morse code using the lights.

Defaults:
* Lights are enabled.  Rumble is Disabled
* The default colour is white.
* The message is sent only once.

Arguments:

/D       : Repeat the message forever
/C:R,G,B : Override the colour used to display the message.
           Colours for R, G and B (between 0 and 1) must be
           specified int he order denoted and be separated
           by commas.  For example: /C:1,1,0
/R       : Include rumble support
/L       : Disable light support
/M       : The message to display.  All international Morse
           code characters are supported (excluding prosigns).

The following characters are supported in messages:
 * The characters A-Z
 * The numbers 0-9
 * Spaces
 * The following other characters
   . , ? ' ! / ( ) & : ; = + _ "" $ @

Examples:
aPC.Client.Morse.exe /R /C:1,0,0 /M:Single Red Message With Rumble
aPC.Client.Morse.exe /D /-L /R /M:Repeated Rumble-Only Message

";
    }
  }
}