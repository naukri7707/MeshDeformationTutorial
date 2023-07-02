using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshHelper
{
    public class KeepInBoxModifier : VertexModifier
    {
        private BoxCollider boxCollider;

        protected override void Initial()
        {
            boxCollider = target.GetComponent<BoxCollider>();
        }

        protected override void OnVertexModify(VertexModifyArgs args)
        {
            var newPoint = transform.TransformPoint(args.vector);
            var closestPoint = boxCollider.ClosestPoint(newPoint);
            if (closestPoint != newPoint)
            {
                args.vector = transform.InverseTransformPoint(closestPoint);
            }
        }
    }
}