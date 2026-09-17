using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RotateTurretScript : MonoBehaviour
{
    float rotationX;
    public MinMax minMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        
        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX,minMax.minRotationX,minMax.maxRotationX);
        // TO consider the parent rotation;
        transform.rotation = Quaternion.Euler(-rotationX,transform.eulerAngles.y,transform.eulerAngles.z);
    }
}

[Serializable]
public class MinMax
{
    public float maxRotationX;
    public float minRotationX;
}
