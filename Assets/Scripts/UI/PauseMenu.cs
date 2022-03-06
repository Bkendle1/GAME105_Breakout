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

using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_mainGameUI, m_pauseMenu, m_firstMenuItem;
     
    private int m_buttonListCount;
    
    
    #region UnityAPI

    private void Start()
    {
        GameManager.Instance.GameResumed += TurnBackOnGameUI;
        GameManager.Instance.GamePaused += TurnOnPauseMenu;
       
        TurnBackOnGameUI();
    }

    private void OnDisable()
    {
        GameManager.Instance.GameResumed -= TurnBackOnGameUI;
        GameManager.Instance.GamePaused -= TurnOnPauseMenu;
    }

    #endregion

    #region private

    private void TurnOnPauseMenu()
    {
        m_mainGameUI.SetActive(false);
        m_pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_firstMenuItem);
       
    }

    private void TurnBackOnGameUI()
    {
        m_mainGameUI.SetActive(true);
        m_pauseMenu.SetActive(false);
    }

    #endregion
}