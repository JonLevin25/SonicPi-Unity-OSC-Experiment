using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscUnityProperty<TProp> : OscTriggeredEffect
    {
        [SerializeField] protected TProp prop;
        [SerializeField] private bool bindSourceToThisOnValidate = true;

        public TProp Prop
        {
            get => prop;
            set => PropSetter(value);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (bindSourceToThisOnValidate) PropSetter(prop);
        }

        protected abstract void PropSetter(TProp value);
        protected abstract (bool success, TProp value) ExtractValue(OSCValue value);

        /// <summary>
        /// Implement this to get the default value before your prop is bound. <br />
        /// i.e for Ambient light, this would return RenderSettings.ambientColor
        /// </summary>
        protected abstract TProp GetPropSourceValue();

        protected override void OneTimeInit()
        {
            base.OneTimeInit();
            prop = GetPropSourceValue();
        }

        public override void HandleValue(OSCValue val)
        {
            var (success, finalVal) = ExtractValue(val);
            if (!success) return;
            PropSetter(finalVal);
        }
    }
}