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
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : Singleton<InputController>
{
 
        public Action FirePressed;
        public Action JumpPressed;
        public Action PausePressed;
        public Action CleanUp;
        public Action<float> AxisX;
        public Action<float> AxisY;
        private PlayerInput m_input;
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
            m_input = new PlayerInput();
            m_input.Player.Enable();
            m_input.Player.Pause.performed += GamePaused;
            m_input.Player.Fire.performed += FireShoot;
            SceneManager.activeSceneChanged += CleanUpOnSceneChange;
            GameManager.Instance.GameResumed += GameResumed;
            UpdateDeadZone(GameSettings.DeadZoneValue);
        }

        private void OnDisable()
        { 
           
            CleanUp?.Invoke();
            SceneManager.activeSceneChanged -= CleanUpOnSceneChange;
            GameManager.Instance.GameResumed -= GameResumed;
            m_input.Player.Pause.performed -= GamePaused;
            m_input.Player.Fire.performed -= FireShoot;
        }

        private void OnDestroy()
        {
            m_input.Dispose();
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
          
            if(m_input.Player.Move.ReadValue<Vector2>().x >= m_deadZone || m_input.Player.Move.ReadValue<Vector2>().x  <= m_deadZone)
                AxisX?.Invoke(m_input.Player.Move.ReadValue<Vector2>().x  );
            if(m_input.Player.Move.ReadValue<Vector2>().y >= m_deadZone || m_input.Player.Move.ReadValue<Vector2>().y <= m_deadZone)
                AxisY?.Invoke(m_input.Player.Move.ReadValue<Vector2>().y );
        }
        #endregion

        #region private

        

       
        private void UpdateButtons()
        {
          
            if(Input.GetButtonDown(Jump))
                JumpPressed?.Invoke();
            if(Input.GetButtonDown(Fire))
                FirePressed?.Invoke();
        }

       
        private void GamePaused(InputAction.CallbackContext context)
        {
            m_IHandlerInput?.CleanUp();

            PausePressed?.Invoke();
        }

        private void FireShoot(InputAction.CallbackContext context)
        {
            FirePressed?.Invoke();
        }
        private void CleanUpOnSceneChange(Scene current, Scene next)
        {
           CleanUp?.Invoke();
           m_IHandlerInput = null;
        }
        
        #endregion
}
