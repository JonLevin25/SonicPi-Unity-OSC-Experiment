using extOSC;
using UnityEngine;

namespace MyOscComponents.EditorNS
{
    public class AmbientLightingFX : OscUnityProperty<Color>
    {
        protected override void PropSetter(Color value)
        {
            RenderSettings.ambientLight = value;
            prop = value;
        }

        protected override Color GetPropSourceValue() => RenderSettings.ambientLight;
        protected override (bool success, Color value) ExtractValue(OSCValue value)
            => value.ExtractColor();
    }
}