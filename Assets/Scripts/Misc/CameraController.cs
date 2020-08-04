using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    public Vector3 distanceToTarget;

    private void LateUpdate()
    {
       transform.LookAt(followTarget.transform);
    }

    private void Update()
    {
        transform.position = followTarget.transform.position + followTarget.transform.rotation*distanceToTarget;
    }
}
