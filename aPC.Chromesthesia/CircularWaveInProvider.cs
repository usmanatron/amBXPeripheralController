using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;


namespace aPC.Chromesthesia
{

  /// <summary>
  /// Buffered WaveProvider taking source data from WaveIn
  /// </summary>
  /// <remarks>
  ///   This is an almost exact analogue of WaveInProvider from the NAudio library.
  ///   The only difference is that we allow the BufferedWave/provider to dump samples if it gets too large
  ///   TODO: Work out if theres a better way to do this!
  /// </remarks>
  public class CircularWaveInProvider : IWaveProvider
  {
    IWaveIn waveIn;
    BufferedWaveProvider bufferedWaveProvider;

    /// <summary>
    /// Creates a new WaveInProvider
    /// n.b. Should make sure the WaveFormat is set correctly on IWaveIn before calling
    /// </summary>
    /// <param name="waveIn">The source of wave data</param>
    public CircularWaveInProvider(IWaveIn waveIn)
    {
      this.waveIn = waveIn;
      waveIn.DataAvailable += waveIn_DataAvailable;
      bufferedWaveProvider = new BufferedWaveProvider(this.WaveFormat) { DiscardOnBufferOverflow = true };
    }

    void waveIn_DataAvailable(object sender, WaveInEventArgs e)
    {
      bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
    }

    /// <summary>
    /// Reads data from the WaveInProvider
    /// </summary>
    public int Read(byte[] buffer, int offset, int count)
    {
      return bufferedWaveProvider.Read(buffer, 0, count);
    }

    /// <summary>
    /// The WaveFormat
    /// </summary>
    public WaveFormat WaveFormat
    {
      get { return waveIn.WaveFormat; }
    }
  }
}

