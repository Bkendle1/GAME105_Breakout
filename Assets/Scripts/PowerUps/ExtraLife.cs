using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    private Rigidbody rb;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            Debug.Log("heart powerup paused");
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
            Debug.Log("heart powerup playing");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioClip);
            GameManager.Instance.UpdateLives(1);
            //Delete children first so audio can play
            var children = GetComponentsInChildren<MeshRenderer>();
            foreach (var child in children)
            {
                child.enabled = false;
            }
            Destroy(gameObject, _audioClip.length);
        }
    }
}
