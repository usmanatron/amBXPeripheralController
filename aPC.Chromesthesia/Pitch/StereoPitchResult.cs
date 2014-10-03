namespace aPC.Chromesthesia.Pitch
{
  class StereoPitchResult
  {
    public PitchResult left { get; private set; }
    public PitchResult right { get; private set; }
    public int bytesRead { get; private set; }

    public StereoPitchResult(PitchResult leftResult, PitchResult rightResult, int bytesRead)
    {
      left = leftResult;
      right = rightResult;
      this.bytesRead = bytesRead;
    }
  }
}
