using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigBallPowerUp : MonoBehaviour
{
    [SerializeField] private Ball _ballPlayer;
    [SerializeField] private float newScale = 10f;
    [SerializeField] private AudioClip _growSFX;
    [SerializeField] private AudioClip _revertToNormalSFX;

    [SerializeField] private float _duration = 10f; 
    private Vector3 _ogSize;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _ogSize = _ballPlayer.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ballPlayer.transform.localScale = Vector3.one * newScale;
            _audioSource.PlayOneShot(_growSFX);
            _meshRenderer.enabled = false;
            Invoke("RevertToNormalSize", _duration);
            Destroy(gameObject, _duration + _revertToNormalSFX.length);
        }
    }
    
    private void RevertToNormalSize()
    {
        _ballPlayer.transform.localScale = _ogSize;
        _audioSource.PlayOneShot(_revertToNormalSFX);
    }
}
