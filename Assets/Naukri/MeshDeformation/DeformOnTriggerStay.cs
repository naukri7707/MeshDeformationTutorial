using Naukri.MeshDeformation;
using System.Collections.Generic;
using UnityEngine;

public class DeformOnTriggerStay : MonoBehaviour
{
    public enum Filter
    {
        None,
        TransformChanged,
    }

    public MeshDeformer deformer;

    public Filter filter = Filter.TransformChanged;

    public float distanceTolerance = 0.01F;

    public float angleTolerance = 0.1F;

    private TransformValue lastTransformValue;

    private Dictionary<DeformableObject, TransformValue> deformableLastTransformValues = new();

    protected virtual void Reset()
    {
        deformer = GetComponent<MeshDeformer>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (filter == Filter.TransformChanged)
        {
            if (TryGetDeformable(other, out var deformable))
            {
                deformer.Deform(deformable);
                // 記錄 Transform
                var transformValue = new TransformValue(transform);
                var deformableTransformValue = new TransformValue(deformable.transform);

                lastTransformValue = transformValue;
                deformableLastTransformValues.Add(deformable, deformableTransformValue);
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (filter == Filter.None)
        {
            if (TryGetDeformable(other, out var deformable))
            {
                deformer.Deform(deformable);
            }
        }
        else if (filter == Filter.TransformChanged)
        {
            if (TryGetDeformable(other, out var deformable))
            {
                var currentTransformValue = new TransformValue(transform);
                var lastTransformValue = this.lastTransformValue;

                var currentDeformableTransformValue = new TransformValue(deformable.transform);
                var lastDeformableTransformValue = deformableLastTransformValues[deformable];

                var isTransformChanged = IsTransformChanged(currentTransformValue, lastTransformValue);
                var istDeformableTransformChanged = IsTransformChanged(currentDeformableTransformValue, lastDeformableTransformValue);

                if (isTransformChanged || istDeformableTransformChanged)
                {
                    deformer.Deform(deformable);
                    this.lastTransformValue = currentTransformValue;
                    deformableLastTransformValues[deformable] = currentDeformableTransformValue;
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (filter == Filter.TransformChanged)
        {
            if (TryGetDeformable(other, out var deformable))
            {
                lastTransformValue = default;
                deformableLastTransformValues.Remove(deformable);
            }
        }
    }

    private bool TryGetDeformable(Collider other, out DeformableObject deformable)
    {
        return other.TryGetComponent(out deformable);
    }

    private bool IsTransformChanged(TransformValue oldValue, TransformValue newValue)
    {
        return Vector3.Distance(oldValue.position, newValue.position) > distanceTolerance
            || Quaternion.Angle(oldValue.rotation, newValue.rotation) > angleTolerance
            || oldValue.localScale != newValue.localScale;
    }
}
