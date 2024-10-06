using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : IUpdateable
     
{
    public Camera cam = null;
    private float mouseSensitivity = 3.5f;
    private float mouseSmoothTime = 0.03f;

    private float cameraPitch = 0.0f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    public CameraController()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        Debug.Log("cam going");
    }

    public void PumpedFixedUpdate()
    {
 
    }

    public void PumpedUpdate()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        float tilt = cam.transform.localEulerAngles.y + currentMouseDelta.x * mouseSensitivity;

        cam.transform.localEulerAngles = new Vector3(cameraPitch, tilt, 0.0f);



        Debug.Log(currentMouseDelta.x * mouseSensitivity);


    }
}