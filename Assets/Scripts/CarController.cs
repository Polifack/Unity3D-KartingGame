using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody rigidBody;
    public SphereCollider sphereCollider;

    public float turnSpeed;
    public float maxEnginePower;

    public float enginePowerIncrease;
    public float enginePowerDecrease;

    public float brakesPower;

    public GameObject kartModel;

    private float _currentSpeed;
    private float _gravity = 9.8f;
    private float _rotationAngle;
    private Quaternion _kartRotation;

    //Input data
    private float accelerationInput; //r2
    private float brakesInput; //l2
    private float axisInput; //axis

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

    private void doSteeringWheel()
    {
        _rotationAngle += Time.deltaTime * turnSpeed * axisInput;
        Quaternion steeringWheelRotation = Quaternion.AngleAxis(_rotationAngle, Vector3.up);

        transform.rotation = steeringWheelRotation;
    }

    private void doAcceleration()
    {
        if (accelerationInput != 0)
        {
            Debug.Log("Accelerating: "+accelerationInput);
            _currentSpeed = Mathf.Clamp(_currentSpeed + (Time.deltaTime * accelerationInput * enginePowerIncrease), 0, maxEnginePower);
        }
        if (brakesInput != 0)
        {
            Debug.Log("Braking: "+brakesInput);

            if (_currentSpeed == 0)
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - (Time.deltaTime * brakesInput * brakesPower), -maxEnginePower, 0);
            }
            else
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - (Time.deltaTime * brakesInput * brakesPower), 0, maxEnginePower);
            }
        }
        if (accelerationInput == 0 && brakesInput == 0)
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed - (Time.deltaTime * enginePowerDecrease), 0, maxEnginePower);
        }

    }

    public void doMovement()
    {
        //Speed
        rigidBody.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);

        //Gravity
        rigidBody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
    }

    public void doRaycast()
    {
        RaycastHit ray;       
        if (Physics.Raycast(transform.position, Vector3.down, out ray, 1f))
        {
            if (ray.normal != Vector3.up)
            {
                Debug.Log("Rotation on slope");
                Quaternion slopeRotation = Quaternion.LookRotation(Vector3.Cross(transform.forward, ray.normal));
                transform.rotation = slopeRotation;
            }      
        }
    }

    public void kartFollowCollider()
    {
        kartModel.transform.position = transform.position - new Vector3(0, 1f, 0);
        kartModel.transform.rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        doMovement();
        doAcceleration();
        doSteeringWheel();
        doRaycast();
    }

    private void LateUpdate()
    {
        kartFollowCollider();
    }
}
