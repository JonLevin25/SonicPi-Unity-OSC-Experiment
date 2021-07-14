using DG.Tweening;
using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscAnimatedFloatProp : OscUnityProperty<float>
    {
        [Header("Animation")]
        [SerializeField] private Ease easing = Ease.Linear;
        [SerializeField] private float duration;
        
        private Tween _anim;
        protected abstract void PropSetterInternal(float val);
        
        protected override (bool success, float value) ExtractValue(OSCValue value)
            => value.ExtractFloat();
        
        protected override void PropSetter(float value)
        {
            if (!Application.isPlaying) PropSetterInternal(value);
            _anim?.Kill();
            _anim = Animate(value, duration, easing);
        }
        
        private Tween Animate(float target, float dur, Ease ease)
        {
            return DOTween
                .To(() => Prop, PropSetterInternal, target, dur)
                .SetEase(ease);
        }
    }
}