using Naukri.MeshDeformation;
using Naukri.MeshDeformation.Modifier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParameters : DeformableParameters, KeepInBoxModifier.IParameter
{
    [field: SerializeField]
    public BoxCollider BoxCollider { get; set; }
}
