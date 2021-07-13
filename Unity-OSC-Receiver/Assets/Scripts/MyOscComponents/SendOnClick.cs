using extOSC;
using UnityEngine;

public class SendOnClick : MonoBehaviour
{
    [SerializeField] private OSCTransmitter transmitter;
    [SerializeField] private string oscAdress;

    private void OnMouseUpAsButton()
    {
        Debug.Log($"Sending OSC ({oscAdress}).");
        transmitter.Send(new OSCMessage(oscAdress));
    }
}
