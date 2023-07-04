using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class ShaderPassLayerCondition : DeformableObjectModifier
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
        internal bool Evaluation(Args args)
        {
            return OnEvaluation(args);
        }

        protected abstract bool OnEvaluation(Args args);
    }

    public abstract class ShaderPassLayerCondition<TParameters> : ShaderPassLayerCondition, IWithParameter<TParameters>
    {
        private TParameters _parameters;

        public TParameters parameters => _parameters;

        internal override void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            if (target.parameters is TParameters parameters)
            {
                _parameters = parameters;
            }
            else
            {
                throw new System.Exception($"{target.name} required parameter {typeof(TParameters).Name}");
            }
            Initial();
        }
    }
}