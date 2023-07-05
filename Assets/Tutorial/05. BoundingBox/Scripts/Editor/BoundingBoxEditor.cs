using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoundingBox), true)]
public class BoundingBoxEditor : Editor
{
    private BoundingBox boundingBox;

    private void OnEnable()
    {
        boundingBox = serializedObject.targetObject as BoundingBox;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Eval"))
        {
            var startTime = 0F;
            var endTime = 0F;
            var elapsedTimeByDirectSum = 0F;
            var elapsedTimeWithFilterSum = 0F;

            var elapsedTimeByDirectBetter = 0;
            var elapsedTimeWithFilterBetter = 0;

            for (var i = 0; i < boundingBox.testTime; i++)
            {
                startTime = Time.realtimeSinceStartup;
                boundingBox.EvalCollider();
                endTime = Time.realtimeSinceStartup;
                var elapsedTimeByDirect = endTime - startTime;
                elapsedTimeByDirectSum += elapsedTimeByDirect;

                startTime = Time.realtimeSinceStartup;
                boundingBox.EvalBoundingBox();
                endTime = Time.realtimeSinceStartup;
                var elapsedTimeWithFilter = endTime - startTime;
                elapsedTimeWithFilterSum += elapsedTimeWithFilter;

                if (elapsedTimeByDirect < elapsedTimeWithFilter)
                {
                    elapsedTimeByDirectBetter++;
                }
                else
                {
                    elapsedTimeWithFilterBetter++;
                }
            }
            Debug.Log($@"測試回數：{boundingBox.testTime},   內部率：{boundingBox.innerRatio}, 每回向量數：{boundingBox.vertexPerTest}
-----------------------------------------------------------------------------
　　 | 總耗時 | 平均耗時 | 較優次數
直接 | {elapsedTimeByDirectSum:0.000} | {elapsedTimeByDirectSum / boundingBox.testTime:  0.000} | {elapsedTimeByDirectBetter}
過濾 | {elapsedTimeWithFilterSum:0.000} | {elapsedTimeWithFilterSum / boundingBox.testTime:  0.000} | {elapsedTimeWithFilterBetter}
");
        }

    }
}