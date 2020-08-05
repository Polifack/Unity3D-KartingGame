using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartVisual : MonoBehaviour
{
    //Rotation spheres
    public Transform rotationX;
    public Transform rotationY;

    //Kart model
    public GameObject kartModel;
    public GameObject steeringWheel;
    public GameObject[] backWheels;
    public GameObject[] frontWheels;

    //Animation parameters  
    public float slopeLerpingSpeed;
    public float driftingLerpingSpeed;

    public float driftKartRotation;

    public float steeringWheelRotationPower;
    public float frontWheelRotationPower;
    

    //Sets kart rotation according to a slope
    public void setSlopeRotation(Vector3 normalVector)
    {
        rotationY.up = Vector3.Lerp(
            rotationY.up, 
            normalVector, 
            Time.deltaTime * slopeLerpingSpeed);
        rotationY.Rotate(0, transform.eulerAngles.y, 0);
    }

    public void setDriftingDirection(float direction)
    {
        Debug.Log(direction);
        
        rotationX.localEulerAngles = (direction != 0) ?
            new Vector3(0, direction*driftKartRotation, 0) :
            new Vector3(0, 0, 0);
        Debug.Log(rotationX.localEulerAngles);
    }

    //Returns the vector that identifies where the kart is looking at
    public Vector3 GetForwardVector()
    {
        return rotationX.forward;
    }

    //Sets the position for the kart model
    public void setPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    //Rotates the steering wheel
    public void rotateSteeringWheel(float ammount)
    {
        steeringWheel.transform.eulerAngles = new Vector3(steeringWheel.transform.eulerAngles.x, steeringWheel.transform.eulerAngles.y, ammount* steeringWheelRotationPower);
    }

    public void rotateWheels(float speed, float rotation)
    {
        foreach (GameObject go in frontWheels)
        {
            Vector3 rotationVector = new Vector3(0, rotation, go.transform.localEulerAngles.z+ speed);
            go.transform.localRotation = Quaternion.Euler(rotationVector);
        }
        foreach (GameObject go in backWheels)
        {
            Vector3 rotationVector = new Vector3(0, (0), go.transform.localEulerAngles.z + speed);
            go.transform.localRotation = Quaternion.Euler(rotationVector);
        }
    }

}
