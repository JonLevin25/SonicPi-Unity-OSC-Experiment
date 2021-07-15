using extOSC;
using MilkShake;
using NaughtyAttributes;
using UnityEngine;

namespace MyOscComponents
{
    public class OscCameraShakeEffect : OscTriggeredEffect
    {
        [SerializeField] private ShakePreset shakePreset;
        
        [SerializeField, DisableIf(nameof(UsingPreset))] private MilkShake.ShakeInstance shakeConfig;
        [SerializeField] private Shaker shaker;

        private bool UsingPreset() => shakePreset != null;

        public override void HandleValue(OSCValue val)
        {
            if (UsingPreset()) shaker.Shake(shakePreset);
            else shaker.Shake(shakeConfig.ShakeParameters);
        }
    }
}