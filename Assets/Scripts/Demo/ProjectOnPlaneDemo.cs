﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectOnPlaneDemo : MonoBehaviour
{
    [Tooltip("要計算的目標點")]
    public Transform target;

    [Tooltip("投影平面面向")]
    public Transform faceTo;

    [Tooltip("投影到的點")]
    public Transform projected;

    private void OnDrawGizmos()
    {  
        // 目標點之於平面原點的向量
        var direction = target.position - transform.position;

        // 計算投影平面的法向量
        var planeNormal = transform.position - faceTo.position;

        // 投影目標點至平面並取得向量 (相對於平面原點的位置)
        var projectedVector = Vector3.ProjectOnPlane(direction, planeNormal);

        // 更新投影點的位置
        projected.position = transform.position + projectedVector;

        // 繪製平面
        Handles.color = Color.green;
        for (var i = 1; i <= 100; i++)
        {
            Handles.DrawWireDisc(transform.position, planeNormal, i * 0.01F);
        }

        // 繪製連線
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, faceTo.position);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(target.position, projected.position);
    }
}