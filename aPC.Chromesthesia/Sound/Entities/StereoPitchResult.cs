namespace aPC.Chromesthesia.Sound.Entities
{
  internal class StereoPitchResult
  {
    public PitchResult Left { get; private set; }

    public PitchResult Right { get; private set; }

    public int bytesRead { get; private set; }

    public StereoPitchResult(PitchResult leftResult, PitchResult rightResult, int bytesRead)
    {
      Left = leftResult;
      Right = rightResult;
      this.bytesRead = bytesRead;
    }
  }
}