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

public class GameManager : Singleton<GameManager>
{
    public Action GamePaused;
    public Action GameResumed;
    public Action LiveLost;
    public Action EndGame;
    public bool NewHighScore => m_newHighScore;
    public bool IsGamePaused => m_isGamePaused;
    public GameState GetGameState => m_gameState;

    [SerializeField] private UIText m_scoreUI = null, m_livesUI = null, m_levelUI = null;

    private int m_score = 0, m_lives = 3, m_highScore = 0, m_level = 1;
    private bool m_isGamePaused = false, m_newHighScore = false;
    private GameState m_gameState = GameState.Playing;
    private float ignoreBrickCount = 0f;

    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject nextLevel;
    private SceneSwap _sceneSwap;
    
    #region UnityAPI

    private void Start()
    {
        InputController.Instance.PausePressed += InputPausedCalled;
        NullChecks();
        m_livesUI.UpdateUI(m_lives);
        m_levelUI.UpdateUI(m_level);
        //store the value of indestructible bricks
        ignoreBrickCount = GameObject.FindGameObjectsWithTag("IgnoreBrick").Length;
        _sceneSwap = GetComponent<SceneSwap>();
    }

    private void OnDestroy()
    {
        InputController.Instance.PausePressed -= InputPausedCalled;
    }

    #endregion

    #region Public

    public void PauseGame(bool DoGamePaused)
    {
        if (DoGamePaused)
        {
            GamePaused?.Invoke();
        }
        else
        {
            GameResumed?.Invoke();
        }

        m_isGamePaused = DoGamePaused;
        Debug.Log("GamePaused:" + DoGamePaused);
    }

    public void UpdateScore(int value)
    {
        m_score += value;
        m_scoreUI.UpdateUI(m_score);
    }

    public void BrickCount(int value)
    {
        int totalCount = FindObjectsOfType<Brick>().Length;
        //if the new total count of brick is 0, game is complete
        if (totalCount + value <= (0 + ignoreBrickCount))
        {
            LevelClear();
        }
    }
    
    public void UpdateLives(int value)
    {
        if (m_lives + value <= 0)
        {
            //this is in case you lose your last life right
            //after you win
            if (GetGameState == GameState.ClearLevel)
            {
                return;
            }
            GameOver();
            return;
        }
        
        if (value < 0)
            LiveLost?.Invoke();

       

        m_lives += value;
        m_livesUI.UpdateUI(m_lives);
    }

    public void LevelClear()
    {
        //TODO Check if game is cleared
        //This is where you would load the next level or the next set of bricks
        //save score to load into next scene
        m_gameState = GameState.ClearLevel;
        if (m_gameState == GameState.ClearLevel)
        {
            Debug.Log("Level Cleared");
            m_level++;
            m_levelUI.UpdateUI(m_level);
            NextLevel();
        }

        if (m_level >= 3)
        {
            _sceneSwap.ChangeScene(3);
        }
        
    }

    public void GameOver()
    {
        m_gameState = GameState.GameOver;
        CheckForNewHighScore();
        EndGame?.Invoke();
        
    }

    #endregion


    #region Private

    private void NextLevel()
    {
        currentLevel.SetActive(false);
        m_gameState = GameState.Playing;
        Debug.Log("Next level set");
        nextLevel.SetActive(true);
        int newBrickCount = FindObjectsOfType<Brick>().Length;
        BrickCount(newBrickCount);
    }
    
    private void CheckForNewHighScore()
    {
        if (m_highScore < m_score)
        {
            m_newHighScore = true;
            PlayerPrefs.SetInt("highScore", m_score);
        }
        else
        {
            m_newHighScore = false;
        }
    }

    private void InputPausedCalled()
    {
        if (m_gameState == GameState.GameOver)
            return;
        if (m_isGamePaused)
            PauseGame(false);
        else
            PauseGame(true);
    }

    private void NullChecks()
    {
        Assert.IsNotNull(m_scoreUI);
        Assert.IsNotNull(m_livesUI);
    }

    #endregion
}

public enum GameState
{
    Playing,
    GameOver,
    ClearLevel
}