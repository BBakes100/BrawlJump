using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }
}
