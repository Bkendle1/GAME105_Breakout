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

[RequireComponent((typeof(AudioSource)))]
public class AudioUIplayer : Singleton<AudioUIplayer>
{
    [SerializeField] private AudioClip m_moveSFX, m_selectSFX, m_cancelSFX;
    private AudioSource m_audioSource;
    private float m_volume = 1f;
    #region UnityAPI

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_volume = GameSettings.SFXVolumeGet;
        NullChecks();
    }

    private void OnEnable()
    {
        GameSettings.SoundUpdated += VolumeUpdate;
    }

    private void OnDisable()
    {
        GameSettings.SoundUpdated -= VolumeUpdate;
    }

    #endregion

    #region public

    public void PlaySelectEffect()
    {
        m_audioSource.PlayOneShot(m_selectSFX, m_audioSource.volume * m_volume);
    }

    public void PlayCancelEffect()
    {
        m_audioSource.PlayOneShot(m_cancelSFX, m_audioSource.volume * m_volume);
    }

    public void PlayMoveEffect()
    {
        m_audioSource.PlayOneShot(m_moveSFX, m_audioSource.volume * m_volume);
    }

    public void PlayAudioClip(ref AudioClip clip)
    {
        m_audioSource.PlayOneShot(clip, m_audioSource.volume * m_volume);
    }
    public void PlayAudioClip(ref AudioClip clip, float volume )
    {
        m_audioSource.PlayOneShot(clip, volume* m_volume);
    }

    #endregion

    #region private
    
    private void VolumeUpdate(float value)
    {
        m_volume = value;
    }
    private void NullChecks()
    {
        Assert.IsNotNull(m_audioSource);
        Assert.IsNotNull(m_moveSFX);
        Assert.IsNotNull(m_selectSFX);
        Assert.IsNotNull(m_cancelSFX);
    }

    #endregion
}