using DG.Tweening;
using extOSC;
using UnityEngine;

namespace MyOscComponents
{
    public class ScaleFlexAnimEffect : OscTriggeredEffect
    {
        [SerializeField] private float minScaleTarget = 1f;
        [SerializeField] private float maxScaleTarget = 1.5f;

        private Vector3 _baseScale;
        
        [Header("Animation")]
        [SerializeField] private Ease easing = Ease.Linear;
        [SerializeField] private float duration;
        
        private Tween _anim;

        private void Start()
        {
            _baseScale = transform.localScale;
        }

        public override void HandleValue(OSCValue val)
        {
            var scale = Random.Range(minScaleTarget, maxScaleTarget);
            _anim = Animate(scale, _baseScale, duration, easing);
        }

        private Tween Animate(float targetScale, Vector3 baseScale, float dur, Ease ease)
        {
            var halfTime = dur * 0.5f;
            return DOTween.Sequence()
                .Append(transform.DOScale(targetScale, halfTime))
                .Append(transform.DOScale(_baseScale, halfTime))
                .SetEase(ease);
        }
    }
}