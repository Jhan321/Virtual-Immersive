using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour
{
    public float speed = 5;
    void Update()
    {
        transform.Rotate(Vector3.forward * speed);
    }
}
