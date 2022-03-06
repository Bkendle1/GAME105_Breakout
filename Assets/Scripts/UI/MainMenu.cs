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
using UnityEngine.Assertions;

public class MainMenu : MonoBehaviour
{
    [Header("Menu")] [SerializeField] private GameObject m_mainMenu, m_optionMenu;

    [Header("First UI elemtent in Menu")] [SerializeField]
    private GameObject m_mainFirstItem, m_optionFirstItem;


    #region UnityAPI

    private void Start()
    { 
        NullChecks();
        Invoke("TurnOnMainMenu", 0.5f);
      
       
    }

    #endregion

    #region Public

    public void TurnOnOptionsMenu()
    {
        m_optionMenu.SetActive(true);
        m_mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_optionFirstItem);
    }

    public void TurnOnMainMenu()
    {
        m_optionMenu.SetActive(false);
        m_mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_mainFirstItem);
    }

    #endregion

    #region private

    private void NullChecks()
    {
        Assert.IsNotNull(m_mainMenu);
        Assert.IsNotNull(m_optionMenu);
        Assert.IsNotNull(m_mainFirstItem);
        Assert.IsNotNull(m_optionFirstItem);
    }

    #endregion
}