using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aPC.Chromesthesia.Communication;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Client;
using aPC.Common.Entities;
using aPC.Server;
using aPC.Server.Communication;
using aPC.Server.Engine;
using NAudio.Wave;
using System.Numerics;

namespace aPC.Chromesthesia
{
  class ChromesthesiaTask
  {
    private NotificationClient client;
    private ServerTask task;
    private WasapiLoopbackCapture waveIn;
    private bool UseWCF = false;
    private int warmupTicker;

    public void Run()
    {
      if (UseWCF)
      {
        client = new NotificationClient(new HostnameAccessor());
      }
      else
      {
        ServerUp();
      }

      waveIn = new WasapiLoopbackCapture();
      waveIn.DataAvailable += InputBufferToFileCallback;
      waveIn.StartRecording();

      Console.WriteLine(waveIn.WaveFormat.BitsPerSample);
      Console.WriteLine(waveIn.WaveFormat.AverageBytesPerSecond);
      Console.WriteLine(waveIn.WaveFormat.Channels);
      Console.WriteLine(waveIn.WaveFormat.SampleRate);
      Console.WriteLine(waveIn.WaveFormat.Encoding);

      while (true)
      {
        Thread.Sleep(10000);
      }

    }
    
    private void ServerUp()
    {
      var lAccessor = new SceneAccessor();
      var lInitialEvent = lAccessor.GetScene("Server_Startup");
      var lInitialScene = lAccessor.GetScene("Rainbow");
      var lStatus = new SceneStatus(lInitialScene.SceneType);

      task = new ServerTask(lInitialScene,
        lInitialEvent,
        lStatus,
        new NotificationService(),
        new EngineManager());

      ThreadPool.QueueUserWorkItem(_ => task.Run());
    }
    
    private float Normailise(float val)
    {
      float ret = val/300f;

      return ret > 1 ? 1 : ret;
    }

    public void InputBufferToFileCallback(object sender, WaveInEventArgs e)
    {
      //qqUMI - The Buffer is probably Time / Amplitude.  Need FFT to turn into Freq / Amplitude (which'll give better colours?
      // http://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/a41b5b12-b97b-4da8-ba31-739818db668d/wasapi-loopback-and-wmp-visualization-plugins?forum=windowspro-audiodevelopment
      //e.Buffer.Count is 35280 - 0.1 seconds

      // http://naudio.codeplex.com/workitem/16401
      var transformedData = TransformData(e.Buffer, e.BytesRecorded);

      var section = BuildLightSectionFromData(transformedData);
      var frames = new FrameBuilder().AddFrame().WithLightSection(section).WithFrameLength(20).WithRepeated(true).Build();

      var scene = new amBXScene()
                  {
                    Frames = frames,
                    SceneType = eSceneType.Desync
                  };

      if (UseWCF)
      {
        client.PushCustomScene(scene);
      }
      else
      {
        if (warmupTicker > 100)
        {
          task.Update(scene);
        }
        else
        {
          warmupTicker++;
        }
      }

      if (transformedData.Item1.Count > 3600)
      {
        var result = string.Format("{0} | {1} | {2}", transformedData.Item1[500], transformedData.Item1[2000], transformedData.Item1[3600]);
        Console.WriteLine(result);
      }
    }

    private Tuple<List<Single>, List<Single>> TransformData(byte[] buffer, int bytesRecorded)
    {
      Int32 sample_count = bytesRecorded / (waveIn.WaveFormat.BitsPerSample / 8);
      var isLeft = true;

      var leftData = new List<Single>();
      var rightData = new List<Single>();

      for (int i = 0; i < sample_count; ++i)
      {
        var data = BitConverter.ToSingle(buffer, i * 4);

        if (isLeft)
        {
          leftData.Add(data);
        }
        else
        {
          rightData.Add(data);
        }
        isLeft = !isLeft;
      }

      return new Tuple<List<float>, List<float>>(leftData, rightData);
    }

    private LightSection BuildLightSectionFromData(Tuple<List<Single>, List<Single>> transformedData)
    {
      // transformedData.Count is 35280/8 = 4410
      var leftData  = transformedData.Item1;
      var rightData = transformedData.Item2;

      var NWData = MergeLists(leftData, rightData, 0.75f, 0.25f).ToList();
      var WData = MergeLists(leftData, rightData, 0.5f, 0.5f).ToList();
      var NEData = MergeLists(leftData, rightData, 0.25f, 0.75f).ToList();

      var W = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(leftData, 400, 50)), Green = Normailise(AverageFromSubset(leftData, 2000, 50)), Red = Normailise(AverageFromSubset(leftData, 3600, 50)) };//5880, 10))};
      var NW = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(NWData, 400, 50)), Green = Normailise(AverageFromSubset(NWData, 2000, 50)), Red = Normailise(AverageFromSubset(NWData, 3600, 50)) };//11760, 10)) };
      var N = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(WData, 400, 50)), Green = Normailise(AverageFromSubset(WData, 2000, 50)), Red = Normailise(AverageFromSubset(WData, 3600, 50)) };//17640, 10)) };
      var NE = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(NEData, 400, 50)), Green = Normailise(AverageFromSubset(NEData, 2000, 50)), Red = Normailise(AverageFromSubset(NEData, 3600, 50)) };//23520, 10)) };
      var E = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(rightData, 400, 50)), Green = Normailise(AverageFromSubset(rightData, 2000, 50)), Red = Normailise(AverageFromSubset(rightData, 3600, 50)) };//29399, 10)) };

      return new LightSectionBuilder().WithFadeTime(18)
          .WithLightInDirection(eDirection.West, W)
          .WithLightInDirection(eDirection.NorthWest, NW)
          .WithLightInDirection(eDirection.North, N)
          .WithLightInDirection(eDirection.NorthEast, NE)
          .WithLightInDirection(eDirection.East, E)
          .Build();
    }

    private IEnumerable<Single> MergeLists(IEnumerable<Single> firstList, IEnumerable<Single> secondList, float firstPercentage, float secondPercentage)
    {
      if (firstList.Count() != secondList.Count())
      {
        throw new ArgumentException();
      }

      var mergedList = new List<Single>();

      for (int i = 1; i < firstList.Count(); i++)
      {
        mergedList.Add((firstList.ElementAt(i) + secondList.ElementAt(i)) / 2);
      }

      return mergedList;
    }

    private float AverageFromSubset(List<Single> array, int midpoint, int width)
    {
      if (width == 1)
      {
        return array[midpoint];
      }

      if (width % 2 != 0)
      {
        throw new ArgumentException("Width needs to be even!");
      }

      var startIndex = midpoint - (width/2);
      var subset = array.Skip(startIndex).Take(width).Select(val => val).ToList();

      return subset.Sum()/(float) subset.Count();
    }

    private string ByteArrayToString(byte[] val)
    {
      var ret = new StringBuilder();
      foreach (var b in val)
      {
        ret.Append(b + ",");
      }
      return ret.ToString();
    }

    private void GetSources()
    {
      var sources = new List<WaveInCapabilities>();

      for (int i = 0; i < WaveIn.DeviceCount; i++)
      {
        sources.Add(WaveIn.GetCapabilities(i));
      }
    }
  }
}