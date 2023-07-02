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
            var faces = GetFaceInfo();

            var closestDistance = float.MaxValue;
            var closestPoint = Vector3.zero;
            var closestIndex = -1;
            // 計算投影平面的法向量
            for (int i = 0; i < faces.Length; i++)
            {
                var originPoint = faces[i].origin;

                // 目標點之於平面原點的向量
                var direction = targetPoint - originPoint;

                var planeNormal = faces[i].normal;
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


    public Face[] GetFaceInfo()
    {

        var center = boxCollider.bounds.center;
        var extents = boxCollider.bounds.extents;

        var faces = new Face[6];

        faces[0] = new Face(center + new Vector3(extents.x, 0, 0), transform.right);
        faces[1] = new Face(center + new Vector3(-extents.x, 0, 0), -transform.right);
        faces[2] = new Face(center + new Vector3(0, extents.y, 0), transform.up);
        faces[3] = new Face(center + new Vector3(0, -extents.y, 0), -transform.up);
        faces[4] = new Face(center + new Vector3(0, 0, extents.z), transform.forward);
        faces[5] = new Face(center + new Vector3(0, 0, -extents.z), -transform.forward);

        return faces;
    }
}