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

    //Animation parameters  
    public float slopeLerpingSpeed;

    //Sets kart rotation according to a slope
    public void setYRotation(Vector3 normalVector)
    {
        rotationY.up = Vector3.Lerp(
            rotationY.up, 
            normalVector, 
            Time.deltaTime * slopeLerpingSpeed);
        rotationY.Rotate(0, transform.eulerAngles.y, 0);
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
}
