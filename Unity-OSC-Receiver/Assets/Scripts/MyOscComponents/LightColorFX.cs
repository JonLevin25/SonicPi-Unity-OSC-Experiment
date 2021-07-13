using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class LightColorFX : OscUnityProperty<Color>
    {
        [SerializeField] private Light light;

        protected override void Init()
        {
            if (_wasInit) return;
            if (light == null) light = GetComponent<Light>();
            base.Init();
        }
        
        protected override void PropSetter(Color value)
            => light.color = value;

        protected override Color PropDefaultValueGetter() 
            => light == null ? default : light.color;

        protected override (bool success, Color value) GetValueFromMsg(OSCMessage msg)
            => ExtractColor(msg);
    }
}