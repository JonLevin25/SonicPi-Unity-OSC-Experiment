using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscUnityProperty<TProp> : OscTriggeredEffect
    {
        [SerializeField] protected TProp prop;
        
        protected bool _wasInit;
        
        public TProp Prop
        {
            get => prop;
            set
            {
                prop = value;
                PropSetter(value);
            }
        }

        private void Awake() => Init();
        private void OnValidate()
        {
            Init();
            PropSetter(prop);
        }

        protected abstract void PropSetter(TProp value);
        
        /// <summary>
        /// Implement this to get the default value before your prop is bound. <br />
        /// i.e for Ambient light, this would return RenderSettings.ambientColor
        /// </summary>
        protected abstract TProp PropDefaultValueGetter();

        protected abstract (bool success, TProp value) GetValueFromMsg(OSCMessage msg);

        protected virtual void Init()
        {
            if (_wasInit) return;
            prop = PropDefaultValueGetter();
            _wasInit = true;
        }

        public override void HandleMsg(OSCMessage msg)
        {
            var (success, val) = GetValueFromMsg(msg);
            if (!success) return;
            
            PropSetter(val);
        }
    }
}