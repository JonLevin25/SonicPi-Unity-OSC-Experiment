using extOSC;
using UnityEngine;

namespace MyOscComponents.EditorNS
{
    public class AmbientLightingFX : OscUnityProperty<Color>
    {
        protected override void PropSetter(Color value) => RenderSettings.ambientLight = value;
        protected override Color PropDefaultValueGetter() => RenderSettings.ambientLight;
        protected override (bool success, Color value) GetValueFromMsg(OSCMessage msg) => ExtractColor(msg);
    }
}