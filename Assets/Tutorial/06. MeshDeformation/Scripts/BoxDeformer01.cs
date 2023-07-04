using Naukri.MeshDeformation;
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
        var faceInfos = Utility.GetBoxFaceInfos(boxCollider);

        // 遍歷所有 vertex
        for (var i = 0; i < vertices.Length; i++)
        {
            var targetDeformablePos = vertices[i];

            // 將 targetVector 坐標系轉換為世界座標
            var targetWorldPos = deformable.transform.TransformPoint(targetDeformablePos);

            // 如果 vertex 在包圍體之內且在碰撞器內，嘗試移動 vertex 以產生形變
            if (boxCollider.bounds.Contains(targetWorldPos) && boxCollider.ClosestPoint(targetWorldPos) == targetWorldPos)
            {
                var closestDistance = float.MaxValue;
                var closestWorldPos = Vector3.zero;
                // 計算投影平面的法線
                foreach (var (faceOriginWorldPos, faceNormal) in faceInfos)
                {
                    // 目標點之於平面原點的位置
                    var targetPlanePos = targetWorldPos - faceOriginWorldPos;


                    // 投影目標點至平面並取得向量 (相對於平面原點的位置)
                    var projectedPlanePos = Vector3.ProjectOnPlane(targetPlanePos, faceNormal);

                    // 取得投影點的世界座標
                    var projectedWorldPos = faceOriginWorldPos + projectedPlanePos;

                    var distance = Vector3.Distance(targetWorldPos, projectedWorldPos);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestWorldPos = projectedWorldPos;
                    }
                }

                // 如果距離超過閾值才進行位移
                if (closestDistance > modifyDistanceThreshold)
                {
                    // 將 closestPoint 坐標系轉換為 deformable 的相對座標並儲存
                    var closestDeformablePos = deformable.transform.InverseTransformPoint(closestWorldPos);

                    vertices[i] = closestDeformablePos;
                }
            }
        }
        // 更新 mesh.vertices 為形變後的 vertices 陣列
        deformable.MeshFilter.mesh.vertices = vertices;
    }
}
