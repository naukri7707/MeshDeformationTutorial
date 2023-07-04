using Naukri.MeshDeformation;
using Naukri.MeshDeformation.Modifier;
using UnityEngine;

public class MyParameters : DeformableParameters, KeepInBoxModifier.IParameter
{
    [field: SerializeField]
    public BoxCollider BoxCollider { get; set; }
}
