using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public float xAnglePerSecond;

    public float yAnglePerSecond;

    public float zAnglePerSecond;

    private void Update()
    {
        transform.Rotate(
            xAnglePerSecond * Time.deltaTime,
            yAnglePerSecond * Time.deltaTime,
            zAnglePerSecond * Time.deltaTime
            );
    }
}