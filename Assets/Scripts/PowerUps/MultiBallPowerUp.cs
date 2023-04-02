using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MultiBallPowerUp : MonoBehaviour
{
    [SerializeField] private CloneBall _cloneBall;
    private Ball _ballPlayer;
    private Rigidbody rb;
    
    private void Start()
    {
        _ballPlayer = FindObjectOfType<Ball>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_cloneBall, _ballPlayer.transform.position, _ballPlayer.transform.rotation);
            Instantiate(_cloneBall, _ballPlayer.transform.position, _ballPlayer.transform.rotation);
        }
    }
}
