using MyOscComponents;
using UnityEngine;

public class CameraBackgroundSetter : OscAnimatedColorProp
{
    [SerializeField] protected Camera cam;

    protected override void OneTimeInit()
    {
        if (cam == null) cam = GetComponent<Camera>();
        base.OneTimeInit();
    }

    protected override void PropSetterInternal(Color val)
    {
        cam.backgroundColor = val;
        prop = val;
    }

    protected override Color GetPropSourceValue() => cam.backgroundColor;
}