using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private Animator anim;
    private Ball _ballPlayer;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        _ballPlayer = FindObjectOfType<Ball>();
    }

    private void VoiceLine()
    {
        Debug.Log("In VoiceLine");
    }

    private void DefeatAnim()
    {
        Debug.Log("In DefeatAnim");
        anim.SetBool("isDead", false);
    }

    private void WinAnim()
    {
        Debug.Log("In WinAnim");
        anim.SetBool("Won", false);
    }

    private void Update()
    {
        if (_ballPlayer.isDead)
        {
            anim.SetBool("isDead", true);
        }

        if (GameManager.Instance.GetGameState == GameState.ClearLevel)
        {
            anim.SetBool("Won", true);
        }
    }
}
