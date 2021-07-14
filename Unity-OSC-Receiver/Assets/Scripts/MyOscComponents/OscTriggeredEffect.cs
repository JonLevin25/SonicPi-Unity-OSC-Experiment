using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscTriggeredEffect : MonoBehaviour
    {
        protected bool _wasInit;

        protected virtual void Reset() => Refresh();

        protected virtual void Awake() => Refresh();

        protected virtual void OnValidate() => Refresh();


        protected virtual void OneTimeInit()
        {
            if (_wasInit) return;
            _wasInit = true;
        }

        protected virtual void Refresh()
        {
            OneTimeInit();
        }

        // Main logic - derived classes

        public abstract void HandleValue(OSCValue val);
    }
}