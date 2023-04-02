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

    private Rigidbody rb;
    private Vector3 _ogSize;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        //if i want to make multiple balls, then this would need to be
        //an array
        _ballPlayer = FindObjectOfType<Ball>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _ogSize = _ballPlayer.transform.localScale;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            Debug.Log("growth powerup paused");
            rb.isKinematic = true;
        }
        else
        {
            Debug.Log("growth powerup playing");
            rb.isKinematic = false;
        }
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
