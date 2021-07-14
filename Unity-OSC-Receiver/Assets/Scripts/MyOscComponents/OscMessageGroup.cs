using System.Collections.Generic;
using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class OscMessageGroup : OscTriggeredEffect
    {
        [SerializeField] private List<OscTriggeredEffect> effects;
        
        public override void HandleValue(OSCValue val)
        {
            foreach (var fx in effects)
            {
                fx.HandleValue(val);
            }
        }
    }
}