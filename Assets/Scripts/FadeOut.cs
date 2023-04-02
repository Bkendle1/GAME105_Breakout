using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private bool fadeOut = true;
    [SerializeField] private float _fadeSpeed = 2f;
    void Update()
    {
       //access object's color
       Color objectColor = GetComponent<Renderer>().material.color;
       //lower the alpha value of the object's color
       float fadeAmount = objectColor.a - (_fadeSpeed * Time.deltaTime);
            
       //recreate the object's color with the new lowered alpha value
       objectColor = new Color(objectColor.r, objectColor.b, objectColor.g, fadeAmount);
       //assign color with new alpha value to object
       GetComponent<Renderer>().material.color = objectColor;
            
       //stop loop once object is transparent because this repeats each frame
       if (objectColor.a <= 0) 
       {
           fadeOut = false; 
           Destroy(transform.parent.gameObject);
       }
    }
}
