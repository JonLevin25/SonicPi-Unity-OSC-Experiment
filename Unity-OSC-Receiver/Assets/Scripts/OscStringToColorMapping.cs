using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using extOSC;
using MyOscComponents;
using UnityEngine;

public class OscStringToColorMapping : OscTriggeredEffect
{
    [Serializable]
    public class StringColorMapping : ISerializationCallbackReceiver
    {
        [HideInInspector] [SerializeField] private string _name;
        [SerializeField] private string input;
        [SerializeField] private Color output;

        public string Input => input;
        public Color Output => output;

        public void OnBeforeSerialize()
        {
            _name = $"{input} -> Color";
        }

        public void OnAfterDeserialize() { }
    }

    [SerializeField] private bool ignoreInputCase = true;
    [SerializeField] private List<StringColorMapping> mappings;

    [Space]
    [SerializeField] private List<OscTriggeredEffect> targets;
    
    public override void HandleValue(OSCValue val)
    {
        var (success, str) = val.ExtractString();
        if (!success) return;

        var (mapSuccess, mappedVal) = MappedStr(str);
        if (!mapSuccess) return;
        
        var finalVal = new OSCValue(OSCValueType.Color, mappedVal);
        foreach (var effect in targets)
        {
            effect.HandleValue(finalVal);
        }
    }

    private (bool success, Color result) MappedStr(string str)
    {
        if (ignoreInputCase) str = str.ToLowerInvariant();
        var mapping = mappings.FirstOrDefault(map =>
        {
            var key = ignoreInputCase ? map.Input.ToLowerInvariant() : map.Input;
            return key == str;
        });

        return mapping != null ? (true, mapping.Output) : default;
    }
}
