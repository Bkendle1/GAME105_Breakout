/* Copyright (c) 2022 Scott Tongue
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
using UnityEngine.Assertions;

[RequireComponent((typeof(SFXPlayer)))]
public class AudioUIplayer : Singleton<AudioUIplayer>
{
    [SerializeField] private AudioClip m_moveSFX, m_selectSFX, m_cancelSFX;
    private SFXPlayer m_sfxPlayer;
   
    #region UnityAPI
    
    void Start()
    {
        NullChecks();
    }

   

    #endregion

    #region public

    public void PlaySelectEffect()
    {
        m_sfxPlayer.PlayAudioClip(ref m_selectSFX);
    }

    public void PlayCancelEffect()
    {
        m_sfxPlayer.PlayAudioClip(ref m_cancelSFX);
    }

    public void PlayMoveEffect()
    {
        m_sfxPlayer.PlayAudioClip(ref m_moveSFX);
    }

    public void PlayAudioClip(ref AudioClip clip)
    {
        m_sfxPlayer.PlayAudioClip(ref clip);
    }
    public void PlayAudioClip(ref AudioClip clip, float volume )
    {
        m_sfxPlayer.PlayAudioClip(ref clip, volume);
    }

    #endregion


    #region  overides

    protected override void Init()
    {
        m_sfxPlayer = GetComponent<SFXPlayer>();
    }

    #endregion

    #region private

    private void NullChecks()
    {
        Assert.IsNotNull(m_moveSFX);
        Assert.IsNotNull(m_selectSFX);
        Assert.IsNotNull(m_cancelSFX);
    }

    #endregion
}