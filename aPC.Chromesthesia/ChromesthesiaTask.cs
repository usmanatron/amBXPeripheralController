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

namespace aPC.Chromesthesia
{
  class ChromesthesiaTask
  {
    private NotificationClient client;
    private ServerTask task;

    public void Run()
    {
      //client = new NotificationClient(new HostnameAccessor());
      // Go direct
      ServerUp();
      

      var waveIn = new WasapiLoopbackCapture();
      waveIn.DataAvailable += InputBufferToFileCallback;
      waveIn.StartRecording();

      Console.WriteLine(waveIn.WaveFormat.BitsPerSample);
      Console.WriteLine(waveIn.WaveFormat.AverageBytesPerSecond);
      Console.WriteLine(waveIn.WaveFormat.Channels);
      Console.WriteLine(waveIn.WaveFormat.SampleRate);
      Console.WriteLine(waveIn.WaveFormat.Encoding);

      filter = 1;

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

    private int filter;

    public void InputBufferToFileCallback(object sender, WaveInEventArgs e)
    {
      //qqUMI - The Buffer is probably Time / Amplitude.  Need FFT to turn into Freq / Amplitude (which'll give better colours?
      // http://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/a41b5b12-b97b-4da8-ba31-739818db668d/wasapi-loopback-and-wmp-visualization-plugins?forum=windowspro-audiodevelopment

      //filter++;
      
      //if (filter % 5 == 0)
      //{
      //  filter = 1;
      //}
      //else
      //{
      //  return;
      //}

      //Count is 35280



      var W  = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(e.Buffer, 3, 2)),  Green = Normailise(AverageFromSubset(e.Buffer, 35, 2)), Red = Normailise(AverageFromSubset(e.Buffer, 67, 2))};//5880, 10))};
      var NW = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(e.Buffer, 9, 2)),  Green = Normailise(AverageFromSubset(e.Buffer, 41, 2)), Red = Normailise(AverageFromSubset(e.Buffer, 73, 2))};//11760, 10)) };
      var N  = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(e.Buffer, 16, 2)), Green = Normailise(AverageFromSubset(e.Buffer, 48, 2)), Red = Normailise(AverageFromSubset(e.Buffer, 80, 2)) };//17640, 10)) };
      var NE = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(e.Buffer, 23, 2)), Green = Normailise(AverageFromSubset(e.Buffer, 55, 2)), Red = Normailise(AverageFromSubset(e.Buffer, 87, 2)) };//23520, 10)) };
      var E  = new Light { Intensity = 1, Blue = Normailise(AverageFromSubset(e.Buffer, 29, 2)), Green = Normailise(AverageFromSubset(e.Buffer, 61, 2)), Red = Normailise(AverageFromSubset(e.Buffer, 93, 2)) };//29399, 10)) };

      var section =
        new LightSectionBuilder().WithFadeTime(18)
          .WithLightInDirection(eDirection.West, W)
          .WithLightInDirection(eDirection.NorthWest, NW)
          .WithLightInDirection(eDirection.North, N)
          .WithLightInDirection(eDirection.NorthEast, NE)
          .WithLightInDirection(eDirection.East, E)
          .Build();

      var frames = new FrameBuilder()
        .AddFrame().WithLightSection(section).WithFrameLength(20).WithRepeated(true).Build();


      var scene = new amBXScene()
                  {
                    Frames = frames,
                    SceneType = eSceneType.Desync
                  };

      //client.PushCustomScene(scene);
      // Go direct:
      if (warmupTicker > 200)
      {
        task.Update(scene);
      }
      else
      {
        warmupTicker++;
      }

      var result = string.Format("{0} | {1} | {2}", e.Buffer[5880], e.Buffer[17640], e.Buffer[29400]);
      Console.WriteLine(result);
    }

    private float AverageFromSubset(byte[] array, int midpoint, int width)
    {
      if (width % 2 != 0)
      {
        throw new ArgumentException("Width needs to be even!");
      }

      var startIndex = midpoint - (width/2);

      var subset = array.Skip(startIndex).Take(width).Select(val => (int) val).ToList();

      return (float) subset.Sum()/(float) subset.Count();
    }

    private int warmupTicker;

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
