using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class LightIntensityFX : OscAnimatedFloatProp
    {
        [SerializeField] private Light light;

        protected override void OneTimeInit()
        {
            if (_wasInit) return;
            if (light == null) light = GetComponent<Light>();
            base.OneTimeInit();
        }
        
        protected override void PropSetterInternal(float value)
        {
            light.intensity = value;
            prop = value;
        }

        protected override float GetPropSourceValue() 
            => light == null ? default : light.intensity;
        

        protected override (bool success, float value) ExtractValue(OSCValue val) => val.ExtractFloat();
    }
}