using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MyOscComponents
{
    public static class Extensions
    {
        public static float RandomBetween(this Vector2 vec)
        {
            return Random.Range(vec[0], vec[1]);
        }
        
        public static int RandomBetween(this Vector2Int vec)
        {
            return Random.Range(vec[0], vec[1]);
        }

        public static T RandomMember<T>(this IReadOnlyList<T> collection)
        {
            var i = Random.Range(0, collection.Count);
            return collection[i];
        }

        public static Tween SetEase(this Tween tween, AnimEasing animEase)
        {
            var (ease, animCurve) = animEase.GetEase();
            return ease != Ease.Unset
                ? tween.SetEase(ease)
                : tween.SetEase(animCurve);
        }
    }
}