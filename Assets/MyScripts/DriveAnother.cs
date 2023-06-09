﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// A very simplistic car driving on the x-z plane.

public class DriveAnother : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float moveSpeed = 0.1f;
    public float rSpeed = 0.5f;

    public GameObject fuel;
    bool autoPilot = false;

    void Start()
    {

    }

    void AutoPilot()
    {
        CalculateAngle();
        transform.position += this.transform.up * moveSpeed * Time.deltaTime; //transform.up; = length 1
        //transform.position += new Vector3(0, 0.1f, 0);
    }

    void LateUpdate()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            autoPilot = !autoPilot;
        }

        if (CalculateDistance() < 3)
            autoPilot = false;

        if (autoPilot)
            AutoPilot();
    }

    private void CalculateAngle()
    {
        Vector3 tankFoward = transform.up;
        Vector3 fuelDirection = fuel.transform.position - transform.position;

        Debug.DrawRay(this.transform.position, tankFoward, Color.green, 2);
        Debug.DrawRay(this.transform.position, fuelDirection, Color.red, 2);

        //dot product
        float dot = tankFoward.x * fuelDirection.x + tankFoward.y * fuelDirection.y;
        float angle = Mathf.Acos(dot / (tankFoward.magnitude * fuelDirection.magnitude));

        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle: " + Vector3.Angle(tankFoward, fuelDirection));

        int clockwise = 1;
        if (Cross(tankFoward, fuelDirection).z < 0)
            clockwise = -1;

        if((angle * Mathf.Rad2Deg) > 10)
        this.transform.Rotate(0, 0, angle * Mathf.Rad2Deg * clockwise * rSpeed);
    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.x * w.z - v.z * w.x;
        float zMult = v.x * w.y - v.y * w.x;

        return (new Vector3(xMult, yMult, zMult));
    }

    private float CalculateDistance()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(fuel.transform.position.x - transform.position.x,2)
            + Mathf.Pow(fuel.transform.position.z - transform.position.z,2));

        Vector3 fuelPos = new Vector3(fuel.transform.position.x, 0, fuel.transform.position.z);
        Vector3 tankPos = new Vector3(transform.position.x, 0, transform.position.z);
        //from unity
        // float uDistance = Vector3.Distance(fuel.transform.position, transform.position);
        float uDistance = Vector3.Distance(fuelPos, tankPos);

        Vector3 tankPosToFuelPos = tankPos - fuelPos;

        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + uDistance);
        Debug.Log("Vector  Magnitude: " + tankPosToFuelPos.magnitude);
        Debug.Log("Vector  sqrMagnitude: " + tankPosToFuelPos.sqrMagnitude);

        return distance;
    }
}