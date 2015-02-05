using aPC.Common.Entities;

namespace aPC.Chromesthesia.Lights
{
  public interface ICompositeLightBuilder
  {
    Light BuildCompositeLight(Light firstLight, Light secondLight, int firstLightPercentage);
  }
}