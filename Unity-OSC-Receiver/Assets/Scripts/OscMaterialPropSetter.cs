using MyOscComponents;
using NaughtyAttributes;
using UnityEngine;

public class OscMaterialPropSetter : OscAnimatedColorProp
{
    public enum KnownMaterialProp
    {
        Unset,
        Main,
    }

    [SerializeField] private Material material;

    
    [SerializeField] private KnownMaterialProp knownProp = KnownMaterialProp.Main;
    
    [HideIf(nameof(IsKnownProp))]
    [SerializeField, Label("Property")] private string stringProperty;

    private bool IsKnownProp() => knownProp != KnownMaterialProp.Unset;

    protected override Color GetPropSourceValue()
    {
        return knownProp == KnownMaterialProp.Main
            ? material.color
            : material.GetColor(stringProperty);
    }

    protected override void PropSetterInternal(Color val)
    {
        prop = val;
        
        if (knownProp == KnownMaterialProp.Main) material.color = val;
        else material.SetColor(stringProperty, val);
    }
}
