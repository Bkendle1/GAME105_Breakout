using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    private Rigidbody rb;
    private Ball _ballPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _ballPlayer = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            rb.isKinematic = true;
            Debug.Log("Bomb paused");
        }
        else
        {
            rb.isKinematic = false;
            Debug.Log("Bomb resumed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateLives(-1);
            _ballPlayer.Death();
        }
    }
}
