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
using UI;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(ImageFade))]
public class SaveWarning : MonoBehaviour
{
    [SerializeField] private float m_fadeTime = 1.2f;
    private ImageFade m_fader;

    #region UnityAPI

    private void Awake()
    {
        m_fader = GetComponent<ImageFade>();
    }

    private void Start()
    {
        
        NullCheck();
    }

    private void OnEnable()
    {
        GameSettings.SavingGame += TurnOnWarning;
    }

    private void OnDisable()
    {
        GameSettings.SavingGame -= TurnOnWarning;
    }

    #endregion

    #region public

    public void ShowWarning()
    {
        TurnOnWarning();
    }

    #endregion

    #region private

    private void TurnOnWarning()
    {
        Debug.Log("called");
        m_fader.StartFade(m_fadeTime, 1f);
        Invoke("TurnOffWarning", m_fadeTime + 4f);
    }

    private void TurnOffWarning()
    {
        m_fader.StartFade(m_fadeTime, -1f);
    }

    private void NullCheck()
    {
        Assert.IsNotNull(m_fader);
    }

    #endregion
}