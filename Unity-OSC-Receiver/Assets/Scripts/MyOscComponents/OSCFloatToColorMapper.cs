using System;
using System.Linq;
using extOSC;
using NaughtyAttributes;
using UnityEngine;

namespace MyOscComponents
{
    
    public class OSCFloatToColorMapper : OscTriggeredEffect
    {
        [Header("Input")]
        [SerializeField] private float inpMin;
        [SerializeField] private float inpMax = 1f;

        [Header("Output")]
        [SerializeField] private Color outMin = Color.black;
        [SerializeField] private Color outMax = Color.white;
        [SerializeField] private bool clamp;

        [SerializeField] private OscTriggeredEffect[] targets;

        [Header("Debug")]
        [ShowNonSerializedField] private float lastInput;
        [ShowNonSerializedField] private Color lastOutput;

        public override void HandleValue(OSCValue val)
        {
            var (success, floatVal) = val.ExtractFloat(castOtherNumbers: true, mapBoolTo01: true);
            lastInput = floatVal;
            if (!success) return;

            
            Func<Color, Color, float, Color> colorLerp = clamp switch
            {
                true => Color.Lerp,
                false => Color.LerpUnclamped,
            };

            var t = Mathf.InverseLerp(inpMin, inpMax, floatVal);
            var targetColor = colorLerp(outMin, outMax, t);
            lastOutput = targetColor;
            
            var finalVal = new OSCValue(OSCValueType.Color, targetColor);
            foreach (var target in targets.Where(x => x != null))
            {
                target.HandleValue(finalVal);
            }
        }
    }
}