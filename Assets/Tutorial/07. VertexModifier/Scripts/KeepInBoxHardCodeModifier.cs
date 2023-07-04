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
        var closestPoint = boxCollider.ClosestPoint(current);
        if (closestPoint != current)
        {
            current = args.deformable.transform.InverseTransformPoint(closestPoint);
        }
    }
}
