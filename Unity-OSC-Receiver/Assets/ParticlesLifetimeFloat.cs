using MyOscComponents;
using UnityEngine;

public class ParticlesLifetimeFloat : OscAnimatedFloatProp
{
    [SerializeField] private ParticleSystem particles;
    
    protected override float GetPropSourceValue() => particles.main.startLifetimeMultiplier;

    protected override void PropSetterInternal(float val)
    {
        prop = val;
        var main = particles.main;
        main.startLifetimeMultiplier = val;
    }
}
