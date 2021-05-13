using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float offsetX, offsetY;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;   
    }


    void LateUpdate()
    {
        Vector3 temp = transform.position;//current camera position
        temp.y = playerTransform.position.y;
        temp.x = playerTransform.position.x;
        temp.x += offsetX;
        temp.y += offsetY;
        transform.position = temp; // new camera position

    }
}
