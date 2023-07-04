using UnityEngine;

namespace Naukri.MeshDeformation.Modifier
{
    public class SpacingModifier : VertexModifier
    {
        public float spacing = 0.001F;

        protected override void OnVertexModify(ref Vector3 current, VertexModifierArgs args)
        {
            var newPos = args.newPosition;
            var oldPos = args.oldPosition;

            //  取得加入間距的座標
            var direction = (newPos - oldPos).normalized;
            current += (direction * spacing);
        }
    }
}