using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    public GameObject followTarget;
    public Vector3 distanceToTarget;

    public float minFov;
    public float maxFov;

    public static CameraController instance;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    private void LateUpdate()
    {
       transform.LookAt(followTarget.transform);
    }

    public void changeFov(float value)
    {
        value = Mathf.Clamp(value, minFov, maxFov);
        cam.fieldOfView = value;
    }

    private void Update()
    {
        transform.position = followTarget.transform.position + followTarget.transform.rotation*distanceToTarget;
    }
}
