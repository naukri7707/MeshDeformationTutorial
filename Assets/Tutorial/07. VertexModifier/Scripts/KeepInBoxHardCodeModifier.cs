using Naukri.MeshDeformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInBoxHardCodeModifier : VertexModifier
{
    private BoxCollider boxCollider;

    protected override void Initial()
    {
        boxCollider = target.GetComponent<BoxCollider>();
    }

    protected override void OnVertexModify(VertexModifierArgs args)
    {
        var newPoint = transform.TransformPoint(args.vector);
        var closestPoint = boxCollider.ClosestPoint(newPoint);
        if (closestPoint != newPoint)
        {
            args.vector = transform.InverseTransformPoint(closestPoint);
        }
    }
}
