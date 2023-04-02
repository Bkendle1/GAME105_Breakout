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

using UnityEngine;
using UnityEngine.Assertions;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioClip m_gameOverVO;
    [SerializeField] private GameObject m_gameOverScreen, m_highScoreWin, m_tryAgainScreen;
    [SerializeField] private SceneSwap m_sceneSwaper;
    [SerializeField] private float m_timeToTitleScreen = 6f;

    #region UnityAPI

    private void OnEnable()
    {
        GameManager.Instance.EndGame += GameIsOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.EndGame -= GameIsOver;
    }

    #endregion

    #region private

    private void GameIsOver()
    {
        Debug.Log("In GameIsOver");
        m_gameOverScreen.SetActive(true);
        AudioUIplayer.Instance.PlayAudioClip(ref m_gameOverVO);
        if (GameManager.Instance.NewHighScore)
        {
            Invoke("GotNewHighScore", 3f);
        }
        else
        {
            Invoke("NoNewScore", 3f);
        }
    }

    private void GotNewHighScore()
    {
        m_gameOverScreen.SetActive(false);
        m_highScoreWin.SetActive(true);
        GameSettings.SaveData();
        Invoke("SwapScene", m_timeToTitleScreen);
    }

    private void NoNewScore()
    {
        m_gameOverScreen.SetActive(false);
        m_tryAgainScreen.SetActive(true);
        Invoke("SwapScene", m_timeToTitleScreen);
    }

    private void SwapScene()
    {
        m_sceneSwaper.ChangeScene();
    }


    private void NullCheck()
    {
        Assert.IsNotNull(m_gameOverVO);
        Assert.IsNotNull(m_gameOverScreen);
        Assert.IsNotNull(m_highScoreWin);
        Assert.IsNotNull(m_tryAgainScreen);
        Assert.IsNotNull(m_sceneSwaper);
    }

    #endregion
}