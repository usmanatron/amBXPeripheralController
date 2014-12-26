using aPC.Chromesthesia.Pitch;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.Chromesthesia
{
  internal class PitchResultSummaryWriter
  {
    private ConcurrentQueue<Tuple<PitchResult, PitchResult>> summaryData;
    private const string frequencyFormat = "0000.000";
    private const string amplitudeFormat = "0.00000";
    private const string empty = "       ";

    public PitchResultSummaryWriter()
    {
      summaryData = new ConcurrentQueue<Tuple<PitchResult, PitchResult>>();
      ThreadPool.QueueUserWorkItem(_ => WriteBackground());
    }

    public void Enqueue(PitchResult leftResult, PitchResult rightResult)
    {
      summaryData.Enqueue(new Tuple<PitchResult, PitchResult>(leftResult, rightResult));
    }

    public void WriteBackground()
    {
      while (true)
      {
        if (summaryData.IsEmpty)
        {
          Thread.Sleep(ChromesthesiaConfig.PitchSummaryWriterSleepInterval);
          continue;
        }

        Tuple<PitchResult, PitchResult> item;
        if (!summaryData.TryDequeue(out item))
        {
          Thread.Sleep(ChromesthesiaConfig.PitchSummaryWriterSleepInterval);
          continue;
        }

        WriteInternal(item.Item1, item.Item2);
      }
    }

    private void WriteInternal(PitchResult leftResult, PitchResult rightResult)
    {
      var leftPeakPitchNonZero = ToNonZeroString(leftResult.PeakPitch.averageFrequency, frequencyFormat);
      var rightPeakPitchNonZero = ToNonZeroString(rightResult.PeakPitch.averageFrequency, frequencyFormat);
      var leftTotalAmp = ToNonZeroString(leftResult.TotalAmplitude, amplitudeFormat);
      var rightTotalAmp = ToNonZeroString(rightResult.TotalAmplitude, amplitudeFormat);

      if (leftPeakPitchNonZero != empty || rightPeakPitchNonZero != empty || leftTotalAmp != empty || rightTotalAmp != empty)
      {
        Console.WriteLine("{0} : {1} <- PP | MA -> {2} : {3}", leftPeakPitchNonZero, rightPeakPitchNonZero, leftTotalAmp, rightTotalAmp);
      }
    }

    private string ToNonZeroString(float value, string format)
    {
      return value == 0
        ? empty
        : value.ToString(format);
    }
  }
}