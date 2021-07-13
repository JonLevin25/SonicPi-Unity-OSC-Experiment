using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class SubtractiveShadowFX : OscUnityProperty<Color>
    {
        protected override void PropSetter(Color value) => RenderSettings.subtractiveShadowColor = value;
        protected override Color PropDefaultValueGetter() => RenderSettings.subtractiveShadowColor;
        protected override (bool success, Color value) GetValueFromMsg(OSCMessage msg) => ExtractColor(msg);
    }
}