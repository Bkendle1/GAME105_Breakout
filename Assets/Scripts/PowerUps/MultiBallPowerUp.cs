using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallPowerUp : MonoBehaviour
{
    private Ball _ballPlayer;
    [SerializeField] private CloneBall _cloneBall;
     
    private void Start()
    {
        _ballPlayer = FindObjectOfType<Ball>();
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
