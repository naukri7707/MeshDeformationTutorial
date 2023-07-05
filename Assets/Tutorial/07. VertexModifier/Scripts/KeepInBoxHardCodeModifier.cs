using Naukri.MeshDeformation;
using UnityEngine;

public class KeepInBoxHardCodeModifier : VertexModifier
{
    private BoxCollider boxCollider;

    protected override void Initial(DeformableObject deformableObject)
    {
        boxCollider = deformableObject.GetComponent<BoxCollider>();
    }

    protected override void OnVertexModify(ref Vector3 current, VertexModifierArgs args)
    {
        var currentWorldPos = args.deformable.transform.TransformPoint(current);
        var closestWorldPos = boxCollider.ClosestPoint(currentWorldPos);
        if (currentWorldPos != closestWorldPos)
        {
            current = args.deformable.transform.InverseTransformPoint(closestWorldPos);
        }
    }
}
