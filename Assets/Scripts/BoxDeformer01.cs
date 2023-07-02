using Naukri.MeshDeformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxDeformer01 : MeshDeformer
{
    private BoxCollider boxCollider;

    protected override void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    protected override void OnDeform(DeformableObject deformable)
    {
        // 取得當前 mesh vertices 的副本
        var vertices = deformable.MeshFilter.mesh.vertices;
        // 取得 BoxCollider 六面的原點與法線
        var faceInfos = FaceInfo.GetFaceInfos(boxCollider);

        // 遍歷所有 vertex
        for (var i = 0; i < vertices.Length; i++)
        {
            var targetVector = vertices[i];

            // 將 targetVector 坐標系轉換為世界座標
            var targetPoint = deformable.transform.TransformPoint(targetVector);

            // 如果 vertex 在包圍體之內且在碰撞器內，嘗試移動 vertex 以產生形變
            if (boxCollider.bounds.Contains(targetPoint) && boxCollider.ClosestPoint(targetPoint) == targetPoint)
            {
                var closestDistance = float.MaxValue;
                var closestPoint = Vector3.zero;
                // 計算投影平面的法線
                foreach (var face in faceInfos)
                {
                    var originPoint = face.origin;

                    // 目標點之於平面原點的向量
                    var direction = targetPoint - originPoint;

                    var planeNormal = face.normal;
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
                    }
                }
                // 將 closestPoint 坐標系轉換為 deformable 的相對座標並儲存
                var closestVector = deformable.transform.InverseTransformPoint(closestPoint);
                vertices[i] = closestVector;
            }
        }
        // 更新 mesh.vertices 為形變後的 vertices 陣列
        deformable.MeshFilter.mesh.vertices = vertices;
    }
}
