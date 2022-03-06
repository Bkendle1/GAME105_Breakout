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
using UnityEngine.SceneManagement;

public class InputController : Singleton<InputController>
{
        public Action FirePressed;
        public Action JumpPressed;
        public Action PausePressed;
        public Action CleanUp;
        public Action<float> AxisX;
        public Action<float> AxisY;
        
        [SerializeField]
        private float m_deadZone = 0.1f;
        private IHandlerInput m_IHandlerInput =null ;
   
        private string Horizontal = "Horizontal", Vertical = "Vertical", Fire = "Fire1", Jump = "Jump", Pause ="Pause";
        public IHandlerInput SetHandler
        {
            set => m_IHandlerInput = value;
        }

        #region  UnityAPI
        void OnEnable()
        {
            
            SceneManager.activeSceneChanged += CleanUpOnSceneChange;
            GameManager.Instance.GameResumed += GameResumed;
            UpdateDeadZone(GameSettings.DeadZoneValue);
        }

        private void OnDisable()
        { 
            CleanUp?.Invoke();
            SceneManager.activeSceneChanged -= CleanUpOnSceneChange;
            GameManager.Instance.GameResumed -= GameResumed;
            
        }

      
        void Update()
        {
            UpdateAxis();
            UpdateButtons();
        }
        #endregion

        #region  Public

        

        
        public void GameResumed()
        {
            m_IHandlerInput.Setup();
        }
        
        public void UpdateDeadZone(float value)
        {
            m_deadZone = value;
        }
        private void UpdateAxis()
        {
          
            if(Input.GetAxis(Horizontal) >= m_deadZone || Input.GetAxis(Horizontal) <= m_deadZone)
                AxisX?.Invoke(Input.GetAxis(Horizontal) );
            if(Input.GetAxis(Vertical) >= m_deadZone || Input.GetAxis(Vertical) <= m_deadZone)
                AxisY?.Invoke(Input.GetAxis(Horizontal) );
        }
        #endregion

        #region private

        

       
        private void UpdateButtons()
        {
            if (Input.GetButtonDown(Pause))
                GamePaused();
            if(Input.GetButtonDown(Jump))
                JumpPressed?.Invoke();
            if(Input.GetButtonDown(Fire))
                FirePressed?.Invoke();
        }

        private void GamePaused()
        {
            m_IHandlerInput?.CleanUp();

            PausePressed?.Invoke();
        }
        private void CleanUpOnSceneChange(Scene current, Scene next)
        {
           CleanUp?.Invoke();
           m_IHandlerInput = null;
        }
        #endregion
}
