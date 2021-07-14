using System.Collections.Generic;
using System.Linq;
using extOSC;
using UniRx;
using UnityEngine;

namespace MyOscComponents
{
    public class OscReceiverBinding : MonoBehaviour
    {
        [SerializeField] private string address;
        [SerializeField] private OSCReceiver receiver;

        public List<OscTriggeredEffect> triggeredEffects;
    
        private OSCBind _binding;
        private OSCReceiver Receiver => receiver ??= FindObjectOfType<OSCReceiver>();

        
        public string Address
        {
            get => address;
            set
            {
                address = value;
                UpdateBinding();
            }
        }
        
        private void Reset()
        {
            var recvs= FindObjectsOfType<OSCReceiver>();
            if (recvs.Length == 1) receiver = recvs[0];
        }

        private void OnValidate() => UpdateBinding();

        private void UpdateBinding()
        {
            if (_binding != null) Receiver.Unbind(_binding);
            _binding = Receiver.Bind(Address, OnOSCMessageInternal);
        }
        private void Awake() => UpdateBinding();

        protected virtual void OnOSCMessageInternal(OSCMessage msg)
        {
            var value = msg.Values.FirstOrDefault() ?? new OSCValue(OSCValueType.Null, null);
            foreach (var effect in triggeredEffects)
            {
                effect.HandleValue(value);
            }
        }
    }
}