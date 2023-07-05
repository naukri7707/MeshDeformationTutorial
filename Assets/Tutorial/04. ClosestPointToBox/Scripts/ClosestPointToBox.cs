using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClosestPointToBox : MonoBehaviour
{
    [Tooltip("要計算的目標點")]
    public Transform target;

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
        var closestPoint = Vector3.zero;
        // 判斷是否在包圍體之內
        var isInsideBounds = BoxCollider.bounds.Contains(targetPoint);

        // 如果在包圍體之內且在碰撞器內，使用 ProjectOnPlane 計算到碰撞器外框的最進點
        // 由於 '短路求值' 的特性，如果 isInsideBounds 為 false 則不會執行 'BoxCollider.ClosestPoint(targetPoint) == targetPoint' 此段程式碼
        if (isInsideBounds && BoxCollider.ClosestPoint(targetPoint) == targetPoint)
        {
            // 取得 BoxCollider 六面的原點與法線
            var faceInfos = Utility.GetBoxFaceInfos(boxCollider);

            var closestDistance = float.MaxValue;

            foreach (var faceInfo in faceInfos)
            {
                // 平面原點
                var originPoint = faceInfo.origin;

                // 目標點之於平面原點的向量
                var direction = targetPoint - originPoint;

                // 平面法線
                var planeNormal = faceInfo.normal;

                // 投影目標點至平面並取得向量 (相對於平面原點的位置)
                var projectedVector = Vector3.ProjectOnPlane(direction, planeNormal);

                // 投影點的世界座標
                var projectedPoint = originPoint + projectedVector;

                // 找到距離最近的點
                var distance = Vector3.Distance(targetPoint, projectedPoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = projectedPoint;
                }

                // 繪製連線
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(projectedPoint, 0.01F);
                Gizmos.DrawLine(targetPoint, projectedPoint);
            }
        }
        else
        {
            closestPoint = BoxCollider.ClosestPoint(targetPoint);
        }
        // 繪製目標點
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(target.position, 0.01F);
        // 繪製最短連線
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(closestPoint, 0.01F);
        Gizmos.DrawLine(targetPoint, closestPoint);
        // 繪製 BoxCollider 外輪廓
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
    }
}