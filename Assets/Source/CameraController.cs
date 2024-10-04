using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : IUpdateable
     
{
    public Camera cam = null;
    //[SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

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

        cam.transform.localEulerAngles = Vector3.right * cameraPitch;

        cam.transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

        Debug.Log("all good boss");


    }
}