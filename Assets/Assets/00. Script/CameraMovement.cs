using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 100f;
    [SerializeField]
    private Transform body;
    private float xRotation = 0f;

    [SerializeField]
    private float minClamp = -90f;
    [SerializeField]
    private float maxClamp = 90f;

    private void LateUpdate()
    {
        LookAt();
    }

    private void LookAt()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
