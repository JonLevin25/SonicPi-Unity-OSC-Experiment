using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class SubtractiveShadowFX : OscUnityProperty<Color>
    {
        protected override void PropSetter(Color value)
        {
            RenderSettings.subtractiveShadowColor = value;
            prop = value;
        }

        protected override Color GetPropSourceValue() => RenderSettings.subtractiveShadowColor;
        protected override (bool success, Color value) ExtractValue(OSCValue msg) => msg.ExtractColor();
    }
}