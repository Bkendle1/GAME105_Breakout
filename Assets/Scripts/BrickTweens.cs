using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
using Vector3 = System.Numerics.Vector3;

public class BrickTweens : MonoBehaviour
{
    [SerializeField] private float _maxDuration = 2f;
    private UnityEngine.Vector3 _ogScale;
    private void Awake()
    {
        _ogScale = transform.localScale;
        transform.localScale = UnityEngine.Vector3.zero;
    }

    void Start()
    {
        var randDuration = Random.Range(1.5f, _maxDuration);
        transform.DOScale(_ogScale,randDuration).SetEase(Ease.OutBack);
    }
}
