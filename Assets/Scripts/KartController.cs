using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public Rigidbody sphere;
    public Transform kartXRotation;
    public Transform kartYRotation;
    public LayerMask ground;
    
    public float acceleration;
    public float gravity;
    public float steering;

    private float currentRotate;
    private float currentSpeed;
    
    //Input data
    private float accelerationInput; //r2
    private float brakesInput; //l2
    private float axisInput; //axis

    //Kart
    private float speed;
    private float rotate;

    public void Steer(int direction, float ammount)
    {
        rotate = (steering * direction) * ammount;
    }

    private void Start()
    {
        //Setup controls
        GameManager.instance.controls.Kart.Accelerate.performed += value =>
            accelerationInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Accelerate.canceled += value =>
            accelerationInput = value.ReadValue<float>();

        GameManager.instance.controls.Kart.Movement.performed += value =>
            axisInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Movement.canceled += value =>
            axisInput = value.ReadValue<float>();

        GameManager.instance.controls.Kart.Brakes.performed += value =>
            brakesInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Brakes.canceled += value =>
            brakesInput = value.ReadValue<float>();

    }

    private void Update()
    {
        //Set the global position according to the sphere position
        transform.position = sphere.transform.position - new Vector3(0, 1.2f, 0);

        //Set the speed according to the acceleration value and the trigger pressure
        speed = accelerationInput * acceleration;

        //Check for axis
        if (axisInput != 0) {
            int steerDirection = (axisInput > 0) ? 1 : -1;
            float steerAmmount = Mathf.Abs(axisInput);

            Steer(steerDirection, steerAmmount);
        }
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;

        //Check for ground slope
        RaycastHit hit;
        Physics.Raycast(kartYRotation.transform.position + Vector3.up, Vector3.down, out hit, 2f, ground);
        kartYRotation.up = Vector3.Lerp(kartYRotation.up, hit.normal, Time.deltaTime * 8.0f);
        kartYRotation.Rotate(0, transform.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        sphere.AddForce(kartXRotation.forward * currentSpeed, ForceMode.Acceleration);

        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);
    }
}
