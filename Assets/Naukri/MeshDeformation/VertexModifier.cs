using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class VertexModifier : DeformableObjectModifier
    {
        internal void ModifyVertex(ref Vector3 current, VertexModifierArgs args)
        {
            OnVertexModify(ref current, args);
        }

        protected abstract void OnVertexModify(ref Vector3 current, VertexModifierArgs args);
    }

    public abstract class VertexModifier<TParameters> : VertexModifier, IWithParameter<TParameters>
    {
        private TParameters _parameters;

        public TParameters parameters => _parameters;

        internal override void InitialImpl(DeformableObject deformableObject)
        {
            if (deformableObject.parameters is TParameters parameters)
            {
                _parameters = parameters;
            }
            else
            {
                throw new System.Exception($"{deformableObject.name} required parameter {typeof(TParameters).Name}");
            }
            base.InitialImpl(deformableObject);
        }
    }
}