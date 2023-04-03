using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class ScaleTween : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("In OnEnable");
        LeanTween.scale(gameObject, UnityEngine.Vector3.one, .2f);    }
    
    private void OnDisable()
    {
        Debug.Log("In OnDisable");
        LeanTween.scale(gameObject, UnityEngine.Vector3.zero, .2f);
    }

}
