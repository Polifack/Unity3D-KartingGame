﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class KartController : MonoBehaviour
{
    //Kark components
    public Rigidbody sphere;
    public KartVisual kartVisuals;

    //Ground raycasting parameters
    public LayerMask whatIsGround;
    public float groundRaycastLength;

    //Kart movement parameters
    public float kartWeight;
    public float acceleration;
    public float brakesStrength;
    public float gravity;
    public float steering;
    
    public float driftPositiveMod;
    public float driftNegativeMod;
    public float driftJumpStrength;
    
    public float driftTimeFirst;
    public float driftTimeSecond;
    public float driftTimeThird;

    public Color driftFirstColor;
    public Color driftSecondColor;
    public Color driftThirdColor;

    //Particles
    public KartParticles particles;

    //Kart movement dynamics
    private float speed;
    private float rotate;
    private float currentRotate;
    private float currentSpeed;
    private bool isGrounded;

    private bool drift = false;
    private float driftDirection;
    private int driftAccumulator;

    //Input data
    private float accelerationInput; //r2
    private float brakesInput; //l2
    private float axisInput; //axis




    //Drifting
    public void setDrifting(bool b){
        driftDirection = b ? Mathf.Sign(currentRotate):0;

        if (b && !drift)
        {
            driftAccumulator = 0;
            particles.doDriftParticles(driftDirection);
            sphere.AddForce(new Vector3(0, driftJumpStrength, 0), ForceMode.Impulse);
        }
        if (!b)
        {
            driftAccumulator = 0;
            particles.setParticleColor(Color.white);
            particles.stopDriftParticles();
        }
        
        drift = b;
    }
    public bool isDrifting() {
        driftDirection = 0;
        kartVisuals.setDriftingDirection(0);
        return drift; 
    }


    //Controls
    public void setupControls()
    {
        //Accelerate (R2)
        GameManager.instance.controls.Kart.Accelerate.performed += value =>
            accelerationInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Accelerate.canceled += value =>
            accelerationInput = value.ReadValue<float>();

        //Movement (Left Stick)
        GameManager.instance.controls.Kart.Movement.performed += value =>
            axisInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Movement.canceled += value =>
            axisInput = value.ReadValue<float>();

        //Brakes (L2)
        GameManager.instance.controls.Kart.Brakes.performed += value =>
            brakesInput = value.ReadValue<float>();
        GameManager.instance.controls.Kart.Brakes.canceled += value =>
            brakesInput = value.ReadValue<float>();
        
        //Drifting (Circle)
        GameManager.instance.controls.Kart.Drift.started += value =>
            setDrifting(true);
        GameManager.instance.controls.Kart.Drift.canceled += value =>
            setDrifting(false);
    }
    
    //Steering manager
    public void Steer(int direction, float ammount)
    {
        // Si estamos drifteando duplicamos la potencia del volante en la direccion del drifteo
        // y dividimos la potencia en la direccion contraria

        if (driftDirection!=0 && direction * driftDirection > 0)
        {
            ammount = ammount * driftPositiveMod;
        }
        else if (driftDirection!=0 && direction * driftDirection < 0)
        {
            ammount = ammount * driftNegativeMod;
        }
        

        rotate = (steering * direction) * ammount;
    }

    //Drift accumulator
    public void doDrift()
    {
        if (drift)
        {
            driftAccumulator += 1;

            if (driftAccumulator == driftTimeFirst)
            {
                Debug.Log("Changing color first!");
                particles.setParticleColor(driftFirstColor);
            }
            if (driftAccumulator == driftTimeSecond)
            {
                Debug.Log("Changing color second!");
                particles.setParticleColor(driftSecondColor);
            }
            if (driftAccumulator == driftTimeThird)
            {
                Debug.Log("Changing color third!");
                particles.setParticleColor(driftThirdColor);
            }
        }
    }


    //Unity
    private void Start()
    {
        setupControls();
    }

    private void Update()
    {
        //Update kart model position
        kartVisuals.setPosition(sphere.transform.position - new Vector3(0, 1.2f, 0));
        
        //Check for axis and compute steer rotation

        int steerDirection = (axisInput > 0) ? 1 : -1;
        float steerAmmount = Mathf.Abs(axisInput);
        Steer(steerDirection, steerAmmount);

        //Set the speed according to the acceleration value and the trigger pressure
        speed = accelerationInput * acceleration;
        speed -= brakesInput * brakesStrength;


         //Compute speed
         currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
         currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;

        //Check for ground slope
        RaycastHit hit;
        Physics.Raycast(sphere.transform.position, Vector3.down, out hit, groundRaycastLength, whatIsGround);
        isGrounded = (hit.collider != null);

        //Rotate kart model according to the slope
        kartVisuals.setSlopeRotation(hit.normal);
        kartVisuals.setDriftingDirection(driftDirection);

        //Rotate kart steering wheel
        kartVisuals.rotateSteeringWheel(currentRotate);
        kartVisuals.rotateWheels(currentSpeed, currentRotate);
    }

    private void FixedUpdate()
    {
        //Apply strengths to the sphere
        if (isGrounded)
            sphere.AddForce(kartVisuals.GetForwardVector() * currentSpeed, ForceMode.Acceleration);

        sphere.AddForce(Vector3.down * kartWeight * gravity, ForceMode.Acceleration);

        //Rotate the whole kart according to the axis
        if (currentSpeed!=0)
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0),
                    Time.deltaTime * 5f);

        //Accumulate the drift power
        doDrift();
    }
}
