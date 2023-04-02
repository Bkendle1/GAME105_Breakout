using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class SideToSide : MonoBehaviour
{
    [SerializeField] private float m_distance = 5f;
    [SerializeField] private float m_duration = 2f;
    private Tweener move;
    void Start()
    {
       move = transform.DOMoveX(m_distance, m_duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            Debug.Log("tween pause");
            move.Pause();
        }
        else
        {
            Debug.Log("tween play");
            move.Play();
        }
        
    }

}
