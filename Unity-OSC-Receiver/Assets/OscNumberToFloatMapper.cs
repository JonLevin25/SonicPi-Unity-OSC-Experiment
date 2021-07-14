using System;
using extOSC;
using MyOscComponents;
using UnityEngine;

public class OscNumberToFloatMapper : OscTriggeredEffect
{
    [SerializeField] private float inMin = 0;
    [SerializeField] private float inMax = 1;

    [Header("Output")]
    [SerializeField] private float outMin = 0;

    [SerializeField] private float outMax = 1;

    [Space]
    [SerializeField] private bool clamp = true;

    [SerializeField] private OscTriggeredEffect[] targets;

    public override void HandleValue(OSCValue val)
    {
        var mapped = MapValue(val);
        if (mapped == null) return;

        foreach (var target in targets)
        {
            target.HandleValue(mapped);
        }
    }

    private OSCValue MapValue(OSCValue input)
    {
        var (success, inVal) = input.ExtractFloat(castOtherNumbers: true, mapBoolTo01: true);
        if (!success) return null;

        var t = Mathf.InverseLerp(inMin, inMax, inVal);

        Func<float, float, float, float> lerpFunc = clamp switch
        {
            true => Mathf.Lerp,
            false => Mathf.LerpUnclamped,
            
        };

        var outVal = lerpFunc(outMin, outMax, t);
        return new OSCValue(OSCValueType.Float, outVal);
    }
}