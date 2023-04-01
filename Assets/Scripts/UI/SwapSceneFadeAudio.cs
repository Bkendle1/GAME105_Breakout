using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneFadeAudio : MonoBehaviour
{
    [SerializeField] private SceneSwap m_swapper;
    [SerializeField] private float m_fadeTime = 2f;
    private bool m_hasBeenPressed = false;
    
    #region Unity

    private void Start()
    {
        AudioMusicPlayer.Instance.FadeMusic(1, m_fadeTime, 0);
    }

    #endregion
    
    #region Public

    public void OnClicked()
    {
        if (m_hasBeenPressed)
            return;
        m_hasBeenPressed = true;
        AudioMusicPlayer.Instance.FadeMusic(0, m_fadeTime, 0);
        Invoke("SwapScene", 2f);
        
    }
    #endregion
    
    #region Private

    private void SwapScene()
    {
        m_swapper.ChangeScene();
    }
    #endregion
}
