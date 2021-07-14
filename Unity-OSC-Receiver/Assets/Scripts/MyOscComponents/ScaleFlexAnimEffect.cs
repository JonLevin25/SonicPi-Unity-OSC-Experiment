using System;
using DG.Tweening;
using extOSC;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyOscComponents
{
    [Serializable]
    public class AnimEasing
    {
        [SerializeField] private AnimationCurve[] animCurves; 
        [SerializeField] private Ease[] eases;

        public AnimEasing(Ease ease)
        {
            eases = new[] {ease};
        }

        public (Ease, AnimationCurve) GetEase()
        {
            var easeCount = eases.Length;
            var animCount = animCurves.Length;

            if (easeCount == animCount && animCount == 0) return default;
            
            var i = Random.Range(0, animCount + easeCount);
            var useEase = i < easeCount;
            return useEase
                ? (eases[i - animCount], null)
                : (Ease.Unset, animCurves[i - easeCount]);
        }
    }
    
    public class ScaleFlexAnimEffect : OscTriggeredEffect
    {
        [SerializeField] private float minScaleTarget = 1f;
        [SerializeField] private float maxScaleTarget = 1.5f;

        private Vector3 _baseScale;
        
        [Header("Animation")]
        [SerializeField] private AnimEasing inEasing = new AnimEasing(Ease.Linear);
        [SerializeField] private AnimEasing outEasing = new AnimEasing(Ease.Linear);
        
        [SerializeField] private Vector2 durationRange = new  Vector2(0.5f, 1f);
        [SerializeField] private Vector2 delayRange;
        
        private Tween _anim;

        private void Start()
        {
            _baseScale = transform.localScale;
        }

        public override void HandleValue(OSCValue val)
        {
            var scale = Random.Range(minScaleTarget, maxScaleTarget) * _baseScale;
            _anim = Animate(scale, _baseScale, durationRange.RandomBetween());
        }

        private Tween Animate(Vector3 targetScale, Vector3 baseScale, float dur)
        {
            var halfTime = dur * 0.5f;
            return DOTween.Sequence()
                .SetDelay(delayRange.RandomBetween())
                .Append(transform.DOScale(targetScale, halfTime).SetEase(inEasing))
                .Append(transform.DOScale(baseScale, halfTime).SetEase(outEasing));
        }
    }
}