using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public Transform _camera;  
    public float offset = 1f; 

    void Update()
    {
        
        transform.position = new Vector3(_camera.position.x - offset, _camera.position.y, transform.position.z);
    }
}

