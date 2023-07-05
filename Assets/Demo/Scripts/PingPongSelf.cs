using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PingPongSelf : MonoBehaviour
{
    public Vector3 speed = Vector3.one * 5F;

    public Vector3 distance = Vector3.one * 10F;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        var offset = new Vector3
        {
            x = GetOffset(speed.x, distance.x),
            y = GetOffset(speed.y, distance.y),
            z = GetOffset(speed.z, distance.z),
        };

        var newPosition = startPosition + offset;

        transform.position = newPosition;
    }

    private float GetOffset(float speed, float distance)
    {
        if(distance == 0)
        {
            return 0;
        }

        if(distance > 0)
        {
            return Mathf.PingPong(Time.time * speed, distance);
        }
        else if(distance < 0)
        {
            return -Mathf.PingPong(Time.time * speed, -distance);
        }
        return 0;
    }

}
