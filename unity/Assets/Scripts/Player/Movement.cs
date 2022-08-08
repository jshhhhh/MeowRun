using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump
{
    private const float initialPower = 6f;
    private float maxPower;
    private float minPower;
    private float addPower;
    public float currentPower;
    public bool layeredJump = false; // jump twice
    public Jump(float _maxPower, float _minPower, float _addPower, float _currentPower)
    {
        this.maxPower = _maxPower;
        this.minPower = _minPower;
        this.addPower = _addPower;
        this.currentPower = _currentPower;
    }
    public float PowerControl
    {
        get
        {
            return currentPower;
        }
        set
        {
            currentPower = value;
        }
    }
}
public class Speed
{
    private const float initialSpeed = 3.5f;
    private float maxSpeed;
    private float minSpeed;
    private float addSpeed;
    public float currentSpeed;
    public Speed(float _maxSpeed, float _minSpeed, float _addSpeed, float _currentSpeed)
    {
        this.maxSpeed = _maxSpeed;
        this.minSpeed = _minSpeed;
        this.addSpeed = _addSpeed;
        this.currentSpeed = _currentSpeed;
    }
    public float SpeedControl
    {
        get
        {
            return currentSpeed;
        }
        set
        {
            currentSpeed = value;
        }
    }
}
