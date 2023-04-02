using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    private Rigidbody rb;
    private Ball _ballPlayer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(_audioClip);
            Destroy(gameObject,_audioClip.length);    
            _ballPlayer.Death();
        }
    }
}
