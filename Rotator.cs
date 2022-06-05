using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [Header("Variables")]
    public Vector3 rotation;
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
