using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform Player; 
    float MouseSensitivity = 150.0f;
    float XRotate = 0.0f;

     void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXAxis = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseYAxis = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        XRotate -= mouseYAxis;
        XRotate = Mathf.Clamp(XRotate, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotate, 0.0f, 0.0f);
        Player.Rotate(Vector3.up * mouseXAxis);
    }
}
