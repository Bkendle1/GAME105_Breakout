using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Vector3 = System.Numerics.Vector3;

public class ScaleTween : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("In OnEnable");
        transform.DOScale(UnityEngine.Vector3.one, .2f).SetEase(Ease.InOutSine);
    }


private void OnDisable()
    {
        Debug.Log("In OnDisable");
        transform.DOScale(UnityEngine.Vector3.zero, .2f).SetEase(Ease.InOutSine);
    }

}
