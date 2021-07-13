using System;
using extOSC;
using UniRx;
using UnityEngine;

namespace MyOscComponents
{
    public class OscTransmitterBinding : MonoBehaviour
    {
        [SerializeField] private string address;
        [SerializeField] private OSCTransmitter transmitter;
    
        private readonly ReactiveProperty<OSCMessage> _messageProp = new ReactiveProperty<OSCMessage>();
    
        private OSCBind _binding;
        private OSCTransmitter Transmitter => transmitter ??= FindObjectOfType<OSCTransmitter>();

        public bool SendMessage(OSCMessage msg)
        {
            try
            {
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