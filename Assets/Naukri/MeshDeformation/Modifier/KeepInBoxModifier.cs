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
            var closestPoint = parameters.BoxCollider.ClosestPoint(current);
            if (closestPoint != current)
            {
                current = args.deformable.transform.InverseTransformPoint(closestPoint);
            }
        }
    }
}