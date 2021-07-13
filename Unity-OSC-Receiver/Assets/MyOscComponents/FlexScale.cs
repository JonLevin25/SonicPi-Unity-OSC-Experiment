using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using extOSC;
using UnityEngine;

public class FlexScale : MonoBehaviour
{
    [SerializeField] private OSCReceiver receiver;
    [SerializeField] private string oscPath;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float duration = 0.25f;
    private Tweener _runningTween;

    // Start is called before the first frame update
    void Start()
    {
        receiver.Bind(oscPath, OnBase);
    }

    private void OnBase(OSCMessage arg0)
    {
        if (_runningTween.IsActive()) _runningTween.Kill();
        _runningTween = target.DOScale(targetScale, duration).SetLoops(2, LoopType.Yoyo);
    }

}
