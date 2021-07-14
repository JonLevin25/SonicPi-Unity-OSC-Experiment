using System;
using System.Collections.Generic;
using System.Linq;
using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public static class OscValueExtensions
    {
        public static (bool success, Color color) ExtractColor(this OSCValue value)
        {
            return value.Type == OSCValueType.Color
                ? (true, value.ColorValue)
                : default;
        }

        public static (bool success, float number) ExtractFloat(this OSCValue val, bool castOtherNumbers = true, bool mapBoolTo01 = false)
        {
            static float? Cast<T>(T val, bool condition = true)
            {
                if (!condition) return null;

                if (val is bool b) return b ? 1f : 0f;
                    
                return (float)(dynamic)val;
            }
            
            float? finalVal = val.Type switch
            {
                OSCValueType.Unknown => null,
                OSCValueType.Int => Cast(val.IntValue),
                OSCValueType.Long => Cast(val.LongValue),
                OSCValueType.True => Cast(val.BoolValue, mapBoolTo01),
                OSCValueType.False => Cast(val.BoolValue, mapBoolTo01),
                OSCValueType.Float => (float?) val.FloatValue,
                OSCValueType.Double => Cast(val.DoubleValue),
                OSCValueType.String => null,
                OSCValueType.Null => null,
                OSCValueType.Impulse => Cast(true, mapBoolTo01),
                OSCValueType.Char => null,
                OSCValueType.Color => null,
                OSCValueType.Blob => null,
                OSCValueType.TimeTag => null,
                OSCValueType.Midi => null,
                OSCValueType.Array => null,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (finalVal is { } value)
            {
                return (true, value);
            }

            return default;
        }

        public static OSCValue ExtractValue(this OSCMessage msg)
            => ExtractValues(msg, 1)?.FirstOrDefault();

        public static IEnumerable<OSCValue> ExtractValues(this OSCMessage msg, int expectedCount)
        {
            var valuesCount = msg.Values.Count;
            if (valuesCount < expectedCount)
            {
                Debug.LogError(
                    $"Osc Message ({msg}) had less values ({expectedCount}) than expected! ({valuesCount}). Cannot Parse");
                return default;
            }

            if (valuesCount > expectedCount)
            {
                Debug.LogWarning($"Osc Message ({msg}) had more values ({valuesCount}) than expected! (1)");
                return msg.Values.Take(expectedCount);
            }

            return msg.Values;
        }
    }
}