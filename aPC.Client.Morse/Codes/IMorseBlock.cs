namespace aPC.Client.Morse.Codes
{
  public interface IMorseBlock
  {
    /// <summary>
    ///   Length in units.
    /// </summary>
    int Length{ get; }

    /// <summary>
    ///   Denotes whether or not any signalling is active
    ///   (i.e. true implies the lights are on)
    /// </summary>
    bool Enabled { get; }
  }
}
