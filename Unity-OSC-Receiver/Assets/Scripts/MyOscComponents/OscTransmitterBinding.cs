using System;
using extOSC;
using UniRx;
using UnityEngine;

namespace MyOscComponents
{
    public class OscTransmitterBinding : MonoBehaviour, IOscBinding
    {
        [SerializeField] private string address;
        [SerializeField] private OSCTransmitter transmitter;
    
        private readonly ReactiveProperty<OSCMessage> _messageProp = new ReactiveProperty<OSCMessage>();
        private OSCBind _binding;


        public string Address
        {
            get => address;
            set => address = value;
        }

        private OSCTransmitter Transmitter => transmitter ??= FindObjectOfType<OSCTransmitter>();

        private void Reset()
        {
            var trans= FindObjectsOfType<OSCTransmitter>();
            if (trans.Length == 1) transmitter = trans[0];
        }
        
        public bool SendMessage(params OSCValue[] values)
        {
            try
            {
                var msg = new OSCMessage(Address, values);
                Transmitter.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}