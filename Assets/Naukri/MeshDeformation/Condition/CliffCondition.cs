using UnityEngine;

namespace Naukri.MeshDeformation.Condition
{
    public class CliffCondition : ShaderPassLayerCondition
    {
        public float triggerAngle;

        protected override bool OnEvaluation(Args args)
        {
            var minAngle = CalculateMinAngle(args.triangle.a, args.triangle.b, args.triangle.c);
            return minAngle < triggerAngle;
        }

        private float CalculateMinAngle(Vector3 a, Vector3 b, Vector3 c)
        {
            var ab = (b - a).normalized;
            var ac = (c - a).normalized;
            var bc = (c - b).normalized;

            var angleA = Mathf.Acos(Vector3.Dot(ab, ac)) * Mathf.Rad2Deg;
            var angleB = Mathf.Acos(Vector3.Dot(-ab, bc)) * Mathf.Rad2Deg;
            var angleC = 180 - angleA - angleB;

            var minAngle = Mathf.Min(angleA, angleB, angleC);
            return minAngle;
        }
    }
}
