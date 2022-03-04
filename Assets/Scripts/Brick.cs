/* Copyright (c) 2020 Scott Tongue
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. THE SOFTWARE 
 * SHALL NOT BE USED IN ANY ABLEISM WAY.
 */

using System;
using UnityEngine;

public class Brick : MonoBehaviour, IHit
{
    [SerializeField]
    private int m_scoreValue = 50;

    [SerializeField] private AudioClip m_hitClip = null;
    private AudioSource m_audioSource = null;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        GameManager.Instance.UpdateScore(m_scoreValue);
    }

    #region  Interfaces
    public void BeenHit()
    {
        //TODO: Make Brick getting hit JUICY!
        m_audioSource.PlayOneShot(m_hitClip, GameSettings.SFXVolume);
    }

    public void BeenHit(GameObject HitByObject)
    {
        
    }
    #endregion
}
