using aPC.Chromesthesia.Sound.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Logging
{
  internal class PitchResultSummaryWriter
  {
    private ConcurrentQueue<PitchResultSummary> summaryData;
    private const string frequencyFormat = "0000.000";
    private const string amplitudeFormat = "0.00000";
    private const string empty = "       ";

    public PitchResultSummaryWriter()
    {
      summaryData = new ConcurrentQueue<PitchResultSummary>();
      ThreadPool.QueueUserWorkItem(_ => WriteBackground());
    }

    public void Enqueue(PitchResultSummary summary)
    {
      summaryData.Enqueue(summary);
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

        PitchResultSummary item;
        if (!summaryData.TryDequeue(out item))
        {
          Thread.Sleep(ChromesthesiaConfig.PitchSummaryWriterSleepInterval);
          continue;
        }

        WriteInternal(item);
      }
    }

    private void WriteInternal(PitchResultSummary summary)
    {
      var leftPeakPitchNonZero = ToNonZeroString(summary.leftResult.PeakPitch.averageFrequency, frequencyFormat);
      var rightPeakPitchNonZero = ToNonZeroString(summary.rightResult.PeakPitch.averageFrequency, frequencyFormat);
      var leftTotalAmp = ToNonZeroString(summary.leftResult.TotalAmplitude, amplitudeFormat);
      var rightTotalAmp = ToNonZeroString(summary.rightResult.TotalAmplitude, amplitudeFormat);

      if (leftPeakPitchNonZero != empty || rightPeakPitchNonZero != empty || leftTotalAmp != empty || rightTotalAmp != empty)
      {
        Console.WriteLine("{0} -- {1} : {2} <- PP | MA -> {3} : {4}", summary.time, leftPeakPitchNonZero, rightPeakPitchNonZero, leftTotalAmp, rightTotalAmp);
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