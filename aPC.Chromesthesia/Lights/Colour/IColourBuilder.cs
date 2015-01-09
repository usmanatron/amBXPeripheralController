using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  internal interface IColourBuilder
  {
    float GetValue(float frequency);
  }
}