using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class PowerBallPowerUp : MonoBehaviour
{
    [SerializeField] private float _duration = 10f;
    private Rigidbody rb;
    private Brick[] _bricks;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _bricks = FindObjectsOfType<Brick>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var brick in _bricks)
            {
                //save bricks current hit point values
                var currentHP = brick.m_hitpoints;
                //make all bricks one shot
                brick.m_hitpoints = 1;
                //revert bricks to the hitpoints they had before
                //making them 1 shot
                brick.m_hitpoints = currentHP;
                
            }
        }
    }
    
}
