using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Collider targetCollider;

    public int testTime = 100;

    [Range(0, 100)]
    public int innerRatio = 10;

    public int vertexPerTest = 100000;

    private void Reset()
    {
        targetCollider = GetComponent<Collider>();
    }

    private void OnDrawGizmos()
    {
        // 繪製 BoundingBox
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetCollider.bounds.center, targetCollider.bounds.size);
    }

    // 模擬通過 BoundingBox 篩選掉一部分 vertex
    public void EvalBoundingBox()
    {
        var innerPoint = targetCollider.bounds.center;
        var outerPoint = new Vector3(9999, 9999, 9999);
        var innerCount = vertexPerTest * innerRatio / 100;
        var outerCount = vertexPerTest - innerCount;


        for (var i = 0; i < innerCount; i++)
        {
            if (targetCollider.bounds.Contains(innerPoint))
            {
                if (targetCollider.ClosestPoint(innerPoint) == innerPoint)
                {

                }
            }
        }
        for (var i = 0; i < outerCount; i++)
        {
            if (targetCollider.bounds.Contains(outerPoint))
            {
                if (targetCollider.ClosestPoint(outerPoint) == outerPoint)
                {

                }
            }
        }
    }

    // 模擬不通過 BoundingBox 篩選掉一部分 vertex
    public void EvalCollider()
    {
        var innerPoint = targetCollider.bounds.center;
        var outerPoint = new Vector3(9999, 9999, 9999);
        var innerCount = vertexPerTest * innerRatio / 100;
        var outerCount = vertexPerTest - innerCount;

        for (var i = 0; i < innerCount; i++)
        {
            if (targetCollider.ClosestPoint(innerPoint) == innerPoint)
            {

            }
        }
        for (var i = 0; i < outerCount; i++)
        {
            if (targetCollider.ClosestPoint(outerPoint) == outerPoint)
            {

            }
        }
    }
}
