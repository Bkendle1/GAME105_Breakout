using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioClip);
            Debug.Log(_audioClip);
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
