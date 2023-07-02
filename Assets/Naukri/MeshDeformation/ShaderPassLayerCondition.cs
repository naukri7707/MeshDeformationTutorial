using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class ShaderPassLayerCondition : ScriptableObject
    {
        public class Args
        {
            public readonly Triangle triangle;
            public readonly Triangle originalTriangle;

            public Args(Triangle triangle, Triangle originalTriangle)
            {
                this.triangle = triangle;
                this.originalTriangle = originalTriangle;
            }
        }

        protected DeformableObject _target;

        protected DeformableObject target => _target;

        protected Transform transform => target.transform;

        protected GameObject gameobject => target.gameObject;

        internal void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            Initial();
        }

        protected virtual void Initial() { }

        internal bool Evaluation(Args args)
        {
            return OnEvaluation(args);
        }

        protected abstract bool OnEvaluation(Args args);
    }

}