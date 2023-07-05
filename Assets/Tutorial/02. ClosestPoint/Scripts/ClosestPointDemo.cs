using UnityEngine;

public class ClosestPointDemo : MonoBehaviour
{
    public Transform target;

    public Transform closest;

    private BoxCollider boxCollider;

    public BoxCollider BoxCollider
    {
        get
        {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }
            return boxCollider;
        }
    }

    private void OnDrawGizmos()
    {
        var targetPoint = target.position;
        // 取得 targetPoint 與碰撞器表面最近的點
        var closestPoint = BoxCollider.ClosestPoint(targetPoint);

        closest.position = closestPoint;

        // 繪製平面
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.center, BoxCollider.size);

        // 繪製連線
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPoint);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(targetPoint, closestPoint);
    }
}
