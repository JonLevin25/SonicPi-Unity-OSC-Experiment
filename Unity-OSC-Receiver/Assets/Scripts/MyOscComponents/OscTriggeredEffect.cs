using System.Collections.Generic;
using System.Linq;
using extOSC;
using UniRx;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscTriggeredEffect : MonoBehaviour
    {
        private List<OscReceiverBinding> _triggeredBy;
        public IReadOnlyList<OscReceiverBinding> TriggeredBy => _triggeredBy;

        private CompositeDisposable _subscriptions;
        private void Start() => UpdateTriggerings();

        // Main logic - derived classes
        public abstract void HandleMsg(OSCMessage msg);

        
        public void Add(OscReceiverBinding binding)
        {
            if (_triggeredBy.Contains(binding)) return;
            _triggeredBy.Add(binding);
            UpdateTriggerings();
        }

        public void Remove(OscReceiverBinding binding)
        {
            if (!_triggeredBy.Contains(binding)) return;
            _triggeredBy.Remove(binding);
            UpdateTriggerings();
        }

        private void UpdateTriggerings()
        {
            // Remove from no-longer relevant
            _subscriptions?.Dispose();
            
            var subscriptionDisposables = _triggeredBy.Select(recv => recv.MessageProp.Subscribe());
            _subscriptions = new CompositeDisposable(subscriptionDisposables);
        }

        #region Helpers

        protected static (bool success, Color color) ExtractColor(OSCMessage msg)
        {
            var value = ExtractValue(msg);
            return value.Type == OSCValueType.Color
                ? (true, value.ColorValue)
                : default;
        }

        private static OSCValue ExtractValue(OSCMessage msg)
            => ExtractValues(msg, 1)?.FirstOrDefault();

        private static IEnumerable<OSCValue> ExtractValues(OSCMessage msg, int expectedCount)
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

        #endregion
    }
}