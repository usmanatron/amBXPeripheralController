using System;

namespace aPC.Chromesthesia
{
  /// <summary>
  /// Handles a buffer in Stereo
  /// </summary>
  public class StereoBuffer
  {
    public byte[] LeftChannel;
    public byte[] RightChannel;
    public int frames;

    private int LeftChannelCount;
    private int RightChannelCount;

    public StereoBuffer(int count)
    {
      if (!IsDivisibleBy8(count))
      {
        throw new ArgumentException("Buffer must be divisible by 8");
      }

      LeftChannel = new byte[count / 2];
      RightChannel = new byte[count / 2];
      LeftChannelCount = 0;
      RightChannelCount = 0;
      frames = count / 8;
    }

    private bool IsDivisibleBy8(int value)
    {
      return value % 8 == 0;
    }

    public void AddToLeftChannel(byte value)
    {
      LeftChannel[LeftChannelCount] = value;
      LeftChannelCount++;
    }

    public void AddToRightChannel(byte value)
    {
      RightChannel[RightChannelCount] = value;
      RightChannelCount++;
    }
  }
}
