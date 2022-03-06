
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
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class AudioMusicPlayer : Singleton<AudioMusicPlayer>
{
    private AudioSource _audioSourceA, _audioSourceB;
    private AudioClip _musicClipA = null, _musicClipB = null;
    private Coroutine _fadeA = null, _fadeB = null;
    private float m_maxVolume = 1f;
    
    public bool IsCrossFadeHappening => _fadeA == null && _fadeB == null;
    public bool IsClipPlaying => _audioSourceA.isPlaying || _audioSourceB.isPlaying;

    protected override void Init()
    {

        _audioSourceA = GetComponent<AudioSource>();
        Assert.IsNotNull(_audioSourceA);
        _audioSourceB = gameObject.AddComponent<AudioSource>();
        _audioSourceB.loop = true;
        _audioSourceB.playOnAwake = false;
        m_maxVolume = GameSettings.MusicVolume;

    }


    #region public

    public void PlayMusicLooping(ref AudioClip clip)
    {
        SwapPlaybackAudioSource(ref clip);
        HaveMusicLoop();
    }

    public void PlayMusicOnce(ref AudioClip clip)
    {
        SwapPlaybackAudioSource(ref clip);
        StopMusicLooping();
    }

    public bool StartCrossFade(ref AudioClip clip, float maxVolume, float fadingTime, float delayUnitFade = 0)
    {
        //   if (_fadeA != null || _fadeB != null)
        //  return false;
        SetupFade(clip, maxVolume, fadingTime, delayUnitFade);

        return true;
    }

    public void FadeMusic(float maxVolume, float fadingTime, float delayUnitFade = 0)
    {
        
        if (_audioSourceA.isPlaying)
            _fadeA = StartCoroutine(
                CrossFade(_audioSourceA, _audioSourceA.volume, maxVolume, fadingTime, delayUnitFade));
        else if (_audioSourceB.isPlaying)
            _fadeB = StartCoroutine(
                CrossFade(_audioSourceB, _audioSourceB.volume, maxVolume, fadingTime, delayUnitFade));
    }


    public void HaveMusicLoop()
    {
        if (_audioSourceA.isPlaying)
            _audioSourceA.loop = true;
        else if (_audioSourceB.isPlaying)
            _audioSourceB.loop = true;
    }

    public void StopMusicLooping()
    {
        if (_audioSourceA.isPlaying)
            _audioSourceA.loop = false;
        else if (_audioSourceB.isPlaying)
            _audioSourceB.loop = false;
    }

    public void StopMusicPlaying()
    {
        if (_audioSourceA.isPlaying)
            _audioSourceA.Stop();
        else if (_audioSourceB.isPlaying)
            _audioSourceB.Stop();
    }


    public void StopFade()
    {
        if (_fadeA != null)
            StopCoroutine(_fadeA);
        if (_fadeB != null)
            StopCoroutine(_fadeB);
    }

    #endregion


    #region private

    private void SwapPlaybackAudioSource(ref AudioClip clip)
    {
        if (_audioSourceA.isPlaying)
        {
            _audioSourceA.Stop();
            _musicClipA = clip;
            _audioSourceB.clip = _musicClipA;
            _audioSourceB.Play();
        }
        else
        {
            _audioSourceB.Stop();
            _musicClipA = clip;
            _audioSourceA.clip = _musicClipB;
            _audioSourceA.Play();
        }
    }

    private void StopAudioA()
    {
        _audioSourceA.Stop();
    }

    private void StopAudioB()
    {
        _audioSourceB.Stop();
    }


    private void SetupFade(AudioClip clip, float maxVolume, float fadingTime, float delay)
    {
        if (_audioSourceA.isPlaying)
        {
            _audioSourceB.volume = 0f;
            _musicClipB = clip;
            _audioSourceB.clip = clip;
            _audioSourceB.Play();
            Debug.Log("Source B playing");
            _fadeB = StartCoroutine(CrossFade(_audioSourceB, _audioSourceB.volume, maxVolume, fadingTime, delay));
            _fadeA = StartCoroutine(CrossFade(_audioSourceA, _audioSourceA.volume, 0f, fadingTime, delay));
            Invoke("StopAudioA", fadingTime);
        }
        else
        {
            _audioSourceA.volume = 0f;
            _musicClipA = clip;
            _audioSourceA.clip = clip;
            _audioSourceA.Play();
            Debug.Log("Source A playing");
            _fadeB = StartCoroutine(CrossFade(_audioSourceA, _audioSourceA.volume, maxVolume, fadingTime, delay));
            _fadeA = StartCoroutine(CrossFade(_audioSourceB, _audioSourceB.volume, 0f, fadingTime, delay));
            Invoke("StopAudioB", fadingTime);
        }
    }

    #endregion

    IEnumerator CrossFade(AudioSource sourceToFade, float beginVolume, float endVolume, float duration, float delay)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);
        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;
            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(beginVolume, endVolume, elapsed / duration));
            if (sourceToFade.volume == endVolume)
            {
                StopFade();
                break;
            }

            yield return null;
        }
    }
}