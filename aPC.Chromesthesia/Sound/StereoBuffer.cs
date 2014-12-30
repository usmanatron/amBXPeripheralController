using System;

namespace aPC.Chromesthesia.Sound
{
  /// <summary>
  /// Handles a buffer in Stereo
  /// </summary>
  public class StereoBuffer
  {
    public byte[] LeftChannel;
    public byte[] RightChannel;
    public int frames;

    private int leftChannelCount;
    private int rightChannelCount;

    public StereoBuffer(int count)
    {
      if (!IsDivisibleBy8(count))
      {
        throw new ArgumentException("Buffer must be divisible by 8");
      }

      LeftChannel = new byte[count / 2];
      RightChannel = new byte[count / 2];
      leftChannelCount = 0;
      rightChannelCount = 0;
      frames = count / (2 * sizeof(float));
    }

    private bool IsDivisibleBy8(int value)
    {
      return value % 8 == 0;
    }

    public void AddToLeftChannel(byte value)
    {
      LeftChannel[leftChannelCount] = value;
      leftChannelCount++;
    }

    public void AddToRightChannel(byte value)
    {
      RightChannel[rightChannelCount] = value;
      rightChannelCount++;
    }
  }
}