using System.Linq;

namespace aPC.Chromesthesia
{
  internal class FloatDataStereoSplitter
  {
    /// <summary>
    /// Splits a given buffer into stereo.
    /// The data is assumed to be floats => 4 bytes per sample, interleaved Left, Right.
    /// </summary>
    public StereoBuffer Split(byte[] buffer)
    {
      var isLeftChannel = true;
      var stereoBuffer = new StereoBuffer(buffer.Count());

      for (int i = 0; i < buffer.Count(); i = i + 4)
      {
        if (isLeftChannel)
        {
          stereoBuffer.AddToLeftChannel(buffer[i]);
          stereoBuffer.AddToLeftChannel(buffer[i + 1]);
          stereoBuffer.AddToLeftChannel(buffer[i + 2]);
          stereoBuffer.AddToLeftChannel(buffer[i + 3]);
        }
        else
        {
          stereoBuffer.AddToRightChannel(buffer[i]);
          stereoBuffer.AddToRightChannel(buffer[i + 1]);
          stereoBuffer.AddToRightChannel(buffer[i + 2]);
          stereoBuffer.AddToRightChannel(buffer[i + 3]);
        }

        isLeftChannel = !isLeftChannel;
      }

      return stereoBuffer;
    }
  }
}