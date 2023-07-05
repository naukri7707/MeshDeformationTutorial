using UnityEngine;

public class RotateSelfOnEditor : MonoBehaviour
{
    public bool enableAnimation;

    public float xAnglePerSecond;

    public float yAnglePerSecond;

    public float zAnglePerSecond;

    private void OnDrawGizmos()
    {
        if (enableAnimation)
        {
            transform.Rotate(
                xAnglePerSecond * Time.deltaTime,
                yAnglePerSecond * Time.deltaTime,
                zAnglePerSecond * Time.deltaTime
                );
#if UNITY_EDITOR
            UnityEditor.SceneView.RepaintAll();
#endif
        }
    }
}