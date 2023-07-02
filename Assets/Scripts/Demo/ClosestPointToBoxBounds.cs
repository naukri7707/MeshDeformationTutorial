using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClosestPointToBoxBounds : MonoBehaviour
{
    public BoxCollider boxCollider;

    [Tooltip("要計算的目標點")]
    public Transform target;

    private void OnDrawGizmos()
    {
        var targetPoint = target.position;
        var isInsideCollider = boxCollider.bounds.Contains(targetPoint);

        if (isInsideCollider)
        {
            var FaceInfos = GetFaceInfoInfo();

            var closestDistance = float.MaxValue;
            var closestPoint = Vector3.zero;
            var closestIndex = -1;
            // 計算投影平面的法向量
            for (int i = 0; i < FaceInfos.Length; i++)
            {
                var originPoint = FaceInfos[i].origin;

                // 目標點之於平面原點的向量
                var direction = targetPoint - originPoint;

                var planeNormal = FaceInfos[i].normal;
                planeNormal.Normalize();

                // 投影目標點至平面並取得向量 (相對於平面原點的位置)
                var projectedVector = Vector3.ProjectOnPlane(direction, planeNormal);

                // 更新投影點的位置
                var projectedPoint = originPoint + projectedVector;

                var distance = Vector3.Distance(targetPoint, projectedPoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = projectedPoint;
                    closestIndex = i;
                }

                // 繪製連線
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(projectedPoint, 0.01F);
                Gizmos.DrawLine(targetPoint, projectedPoint);
            }
           
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(closestPoint, 0.01F);
            Gizmos.DrawLine(targetPoint, closestPoint);
        }
        else
        {
            var closestPoint = boxCollider.ClosestPointOnBounds(targetPoint);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(closestPoint, 0.01F);
            Gizmos.DrawLine(targetPoint, closestPoint);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(target.position, 0.01F);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }


    public FaceInfo[] GetFaceInfoInfo()
    {

        var center = boxCollider.bounds.center;
        var extents = boxCollider.bounds.extents;

        var FaceInfos = new FaceInfo[6];

        FaceInfos[0] = new FaceInfo(center + new Vector3(extents.x, 0, 0), transform.right);
        FaceInfos[1] = new FaceInfo(center + new Vector3(-extents.x, 0, 0), -transform.right);
        FaceInfos[2] = new FaceInfo(center + new Vector3(0, extents.y, 0), transform.up);
        FaceInfos[3] = new FaceInfo(center + new Vector3(0, -extents.y, 0), -transform.up);
        FaceInfos[4] = new FaceInfo(center + new Vector3(0, 0, extents.z), transform.forward);
        FaceInfos[5] = new FaceInfo(center + new Vector3(0, 0, -extents.z), -transform.forward);

        return FaceInfos;
    }
}