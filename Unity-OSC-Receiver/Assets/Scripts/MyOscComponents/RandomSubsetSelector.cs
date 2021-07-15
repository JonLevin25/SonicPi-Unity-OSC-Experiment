using System;
using System.Collections.Generic;
using System.Linq;
using extOSC;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyOscComponents
{
    public class RandomSubsetSelector : OscTriggeredEffect
    {
        [SerializeField] private bool useAll;
        
        [DisableIf(nameof(useAll)), MinMaxSlider(0f, 1f)]
        [SerializeField] private Vector2 selectionPercent = new Vector2(0f, 1f);
        
        [DisableIf(nameof(getEffectsInChildrenOnAwake))]
        [SerializeField] private OscTriggeredEffect[] allEffects;

        [SerializeField] private bool getEffectsInChildrenOnAwake;
        private RandomSetSelector<OscTriggeredEffect> _randomSetSelector;

        protected override void Awake()
        {
            if (getEffectsInChildrenOnAwake)
            {
                allEffects = GetComponentsInChildren<OscTriggeredEffect>()
                    .Where(x => x != this).ToArray();
            }
            
            base.Awake();
        }
        
        protected override void Refresh()
        {
            base.Refresh();
            allEffects = allEffects.Where(x => x != null).ToArray();
            _randomSetSelector ??= new RandomSetSelector<OscTriggeredEffect>();
            _randomSetSelector.SetItems(allEffects);
        }

        public override void HandleValue(OSCValue val)
        {
            foreach (var effect in GetRandomSubset())
            {
                effect.HandleValue(val);
            }
        }

        private IEnumerable<OscTriggeredEffect> GetRandomSubset()
        {
            if (useAll) return allEffects;
            
            var percent = Random.Range(selectionPercent[0], selectionPercent[1]);
            var count = (int)(percent * _randomSetSelector.TotalCount);
            return _randomSetSelector.RandomSubset(count);
        }
    }
}