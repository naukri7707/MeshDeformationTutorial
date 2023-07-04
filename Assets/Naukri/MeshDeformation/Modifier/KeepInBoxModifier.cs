using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naukri.MeshDeformation.Modifier
{
    public class KeepInBoxModifier : VertexModifier<KeepInBoxModifier.IParameter>
    {
        public interface IParameter
        {
            BoxCollider BoxCollider { get; }
        }

        protected override void OnVertexModify(VertexModifierArgs args)
        {
            var newPoint = transform.TransformPoint(args.vector);
            var closestPoint = parameters.BoxCollider.ClosestPoint(newPoint);
            if (closestPoint != newPoint)
            {
                args.vector = transform.InverseTransformPoint(closestPoint);
            }
        }
    }
}