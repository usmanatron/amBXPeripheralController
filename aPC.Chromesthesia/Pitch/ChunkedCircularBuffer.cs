using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace aPC.Chromesthesia.Pitch
{
  /// <summary> qqUMI Update me!
  /// A circular buffer that expects updates in chunks (i.e. byte arrays).
  /// Upon initialisation, the chunk size and number of chunks to store are given.
  /// The number of chunks specifies the number of blocks to store, and these are 
  /// stored via WaveBuffer objects to allow for easy manipulation.
  /// </summary>
  class CircularWaveBuffer
  {
    private List<WaveBuffer> waveBuffers;
    private int numberOfChunks;
    private int bufferSize;

    public CircularWaveBuffer(int numberOfChunks, int bufferSize)
    {
      this.numberOfChunks = numberOfChunks;
      this.bufferSize = bufferSize;
      waveBuffers = new List<WaveBuffer>();
    }

    public void AddChunk(WaveBuffer buffer)
    {
      //TODO Consider doing something more elaborate to stop lots of GC activity.
      waveBuffers.Add(buffer);

      while (waveBuffers.Count >= numberOfChunks + 1)
      {
        waveBuffers.RemoveAt(0);
      }
    }

    public IEnumerable<float> GetFloatBuffer()
    {
      // The buffer we get is loads bigger, but the rest of it is empty and it's safe to truncate to the size we expect
      var floatBuffer = waveBuffers.SelectMany(chunk => chunk.FloatBuffer.Take(bufferSize));

      if (waveBuffers.Count < numberOfChunks)
      {
        var sampleCountToAdd = (numberOfChunks - waveBuffers.Count) * bufferSize;
        floatBuffer = Enumerable.Repeat(0f, sampleCountToAdd).Concat(floatBuffer);
      }

      return floatBuffer;
    }
  }
}
