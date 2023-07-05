using UnityEngine;

namespace Naukri.MeshDeformation.Modifier
{
    public class KeepInBoxModifier : VertexModifier<KeepInBoxModifier.IParameter>
    {
        public interface IParameter
        {
            BoxCollider BoxCollider { get; }
        }

        protected override void OnVertexModify(ref Vector3 current, VertexModifierArgs args)
        {
            var currentWorldPos = args.deformable.transform.TransformPoint(current);
            var closestWorldPos = parameters.BoxCollider.ClosestPoint(currentWorldPos);
            if (currentWorldPos != closestWorldPos)
            {
                current = args.deformable.transform.InverseTransformPoint(closestWorldPos);
            }
        }
    }
}