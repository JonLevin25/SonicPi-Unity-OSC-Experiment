using extOSC;
using UniRx;
using UnityEngine;

namespace MyOscComponents
{
    public class OscReceiverBinding : MonoBehaviour, IOscBinding
    {
        [SerializeField] private string address;
        [SerializeField] private OSCReceiver receiver;
    
        private readonly ReactiveProperty<OSCMessage> _messageProp = new ReactiveProperty<OSCMessage>();
    
        private OSCBind _binding;
        private OSCReceiver Receiver => receiver ??= FindObjectOfType<OSCReceiver>();

        public IReadOnlyReactiveProperty<OSCMessage> MessageProp => _messageProp;
        
        public string Address
        {
            get => address;
            set
            {
                address = value;
                UpdateBinding();
            }
        }

        private void OnValidate() => UpdateBinding();

        private void UpdateBinding()
        {
            if (_binding != null) Receiver.Unbind(_binding);
            _binding = new OSCBind(Address, OnOSCMessageInternal);
        }
        private void Awake() => UpdateBinding();

        private void OnOSCMessageInternal(OSCMessage msg) => _messageProp.Value = msg;
    }
}