using System;
using System.Linq;
using extOSC;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyOscComponents
{
    public class RandomSubsetSelector : OscTriggeredEffect
    {
        [SerializeField, MinMaxSlider(0f, 1f)] private Vector2 selectionPercent = new Vector2(0f, 1f);
        
        [DisableIf(nameof(getEffectsInChildrenOnAwake))]
        [SerializeField] private OscTriggeredEffect[] allEffects;

        [SerializeField] private bool getEffectsInChildrenOnAwake;
        private RandomSetSelector<OscTriggeredEffect> _randomSetSelector;

        protected override void Awake()
        {
            if (getEffectsInChildrenOnAwake) allEffects = GetComponentsInChildren<OscTriggeredEffect>();
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
            var percent = Random.Range(selectionPercent[0], selectionPercent[1]);
            var count = (int)(percent * _randomSetSelector.TotalCount);
            foreach (var effect in _randomSetSelector.RandomSubset(count))
            {
                effect.HandleValue(val);
            }
        }
    }
}