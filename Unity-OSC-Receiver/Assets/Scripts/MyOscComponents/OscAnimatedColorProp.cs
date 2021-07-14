using System;
using DG.Tweening;
using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public abstract class OscAnimatedColorProp : OscUnityProperty<Color>
    {
        [Header("Animation")]
        [SerializeField] private Ease easing = Ease.Linear;
        [SerializeField] private Vector2 durationRange = new Vector2(0.5f, 1f);
        
        private Tween _anim;

        protected abstract void PropSetterInternal(Color val);
        
        protected override (bool success, Color value) ExtractValue(OSCValue value)
            => value.ExtractColor();
        
        protected override void PropSetter(Color value)
        {
            if (!Application.isPlaying) PropSetterInternal(value);
            _anim?.Kill();
            _anim = Animate(value, durationRange.RandomBetween(), easing);
        }
        
        private Tween Animate(Color targetColor, float dur, Ease ease)
        {
            return DOTween
                .To(() => Prop, PropSetterInternal, targetColor, dur)
                .SetEase(ease);
        }
    }
}