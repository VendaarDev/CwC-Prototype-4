using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
  public float rotationSpeed;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
    }
}
