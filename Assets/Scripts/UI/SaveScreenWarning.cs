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

[RequireComponent(typeof(SceneSwap))]
public class SaveScreenWarning : MonoBehaviour
{
    [SerializeField] private float m_warningScreenTime = 7f;
    [SerializeField] private SaveWarning m_saveWarning; 
    private SceneSwap m_sceneSwaper;

    #region UnityAPI

    private void Start()
    {
        m_sceneSwaper = GetComponent<SceneSwap>();
        NullCheck();
        GameSettings.LoadData();
        Invoke("SwapToTitle",m_warningScreenTime);
        m_saveWarning.ShowWarning();
    }

    #endregion

    #region private

    private void SwapToTitle()
    {
        m_sceneSwaper.ChangeScene();
    }

    private void NullCheck()
    {
        Assert.IsNotNull(m_sceneSwaper);
        Assert.IsNotNull(m_saveWarning);
    }
    
    #endregion
}
