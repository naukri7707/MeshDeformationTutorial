using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace Naukri.MeshDeformation
{
    public abstract class VertexModifier : DeformableObjectModifier
    {
        internal void ModifyVertex(VertexModifierArgs args)
        {
            OnVertexModify(args);
        }

        protected abstract void OnVertexModify(VertexModifierArgs args);
    }

    public abstract class VertexModifier<TParameters> : VertexModifier, IWithParameter<TParameters>
    {
        private TParameters _parameters;

        public TParameters parameters => _parameters;

        internal override void InitialImpl(DeformableObject deformableObject)
        {
            _target = deformableObject;
            if (target.parameters is TParameters parameters)
            {
                this._parameters = parameters;
            }
            else
            {
                throw new System.Exception($"{target.name} required parameter {typeof(TParameters).Name}");
            }
            Initial();
        }
    }
}