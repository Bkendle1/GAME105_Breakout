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

    public static class CameraShake
    {

        private static bool _holdShake;
        private static float _fadeTimeCurrent = 0f, _tickAmount =0f;
        private static CameraShakeData _data = new CameraShakeData(), _settings = new CameraShakeData();
        private static Vector3 _shakeAmount, _influencePosition, _influenceRotation;
        private static Vector3 _defaultPosInfluence = new Vector3(0.01f, 0.01f,0.00f);
        private static Vector3 _deaultRotInfluence = new Vector3(0.00f, 0.00f, 0.0f);
        private static GameObject _shakerCamera = null;
     
        public static bool IsShaking { get; private set; } = false;
        #region public

        public static void ClearCamera()
        {
            
            _shakerCamera = null;
            
        }

        public static void  SetNewCamera( GameObject Object)
        {
            _shakerCamera = Object;
        }
        public static bool CreateShakeSetup(CameraShakeConfig Setting,  GameObject Object)
        {
            if (_shakerCamera != null)
                return false;
            //setup for shake by zering out vectors
            _shakerCamera = Object;
            _shakeAmount = Vector3.zero;

            _data = Setting.CameraShakeSettings;
            _settings = _data;

            CheckFadeInOrOut();

             _influencePosition = _defaultPosInfluence;
            _influenceRotation = _deaultRotInfluence;
           
            _tickAmount = Random.Range(-100, 100);

            return true;
        }

        public static bool PositionMovmentInfluence(Vector3 Amount)
        {
            if (_shakerCamera == null)
                return false;
            _influencePosition = Amount;
            return true;
        }

        public static bool RotationMovmentInfluence(Vector3 Amount)
        {
            if (_shakerCamera == null)
                return false;
            _influenceRotation = Amount;
            return true;
        }
        #endregion

        #region private
        private static Vector3 UpdateShake()
        {

            if (_data.TimeInFade > 0 && _holdShake)
            {
                if (_fadeTimeCurrent < 1)
                    _fadeTimeCurrent += Time.deltaTime / _data.TimeInFade;
                else if (_data.TimeOutfade > 0)
                    _holdShake = false;
            }

            if (!_holdShake)
                _fadeTimeCurrent -= Time.deltaTime / _data.TimeOutfade;
            else
                _tickAmount += Time.deltaTime * _data.Roughness;


            return ShakeAmount() * _data.Magitude * _fadeTimeCurrent;
        }

        private static Vector3 ShakeAmount()
        {

            _shakeAmount.x = Mathf.PerlinNoise(_tickAmount, 0) - 0.5f;
            _shakeAmount.y = Mathf.PerlinNoise(0, _tickAmount) - 0.5f;
            _shakeAmount.z = Mathf.PerlinNoise(_tickAmount, _tickAmount) - 0.5f;

            return _shakeAmount;
        }

        private static Vector3 VectorMulitplyBySecondVector(Vector3 source, Vector3 second)
        {
            source.x *= second.x;
            source.y *= second.y;
            source.z *= second.z;

            return source;
        }
        private static bool IsDoneShaking()
        {
            if (_fadeTimeCurrent > 0f || _holdShake)
                return true;
            else if (!_holdShake && _fadeTimeCurrent > 0f)
                return true;
            else if (_fadeTimeCurrent < 1f && _holdShake && _data.TimeInFade > 0f)
                return true;
            else
                return false;

        }

        private static void Reset()
        {
            _data = _settings;
            CheckFadeInOrOut();
            IsShaking = false;
        }

        private static void CheckFadeInOrOut()
        {
            if (_data.TimeInFade > 0f)
            {
                _holdShake = true;
                _fadeTimeCurrent = 0f;
            }
            else
            {
                _holdShake = false;
                _fadeTimeCurrent = 1f;
            }
        }



        #endregion

        public static IEnumerator CamerShake()
        {
            if (_shakerCamera == null)
                yield break;
            Debug.Log("Start shaking");
            IsShaking = true;
            Vector3 postion = Vector3.zero;
            Vector3 rotation = Vector3.zero;
            Vector3 camPos = _shakerCamera.transform.position;
            Quaternion camRot = _shakerCamera.transform.rotation;

            while (IsDoneShaking())
            {
                
                postion = VectorMulitplyBySecondVector(UpdateShake(), _influencePosition);
                rotation = VectorMulitplyBySecondVector(UpdateShake(), _influenceRotation);
                _shakerCamera.transform.localPosition += postion;
                _shakerCamera.transform.localEulerAngles += rotation ;
                yield return null;
            }
            while (Mathf.Abs(camPos.x +_shakerCamera.transform.position.x) <= 0.000000001f)
            {
                _shakerCamera.transform.rotation = Quaternion.Slerp(_shakerCamera.transform.rotation, camRot,4.5f *Time.deltaTime);
                _shakerCamera.transform.position = Vector3.Slerp(_shakerCamera.transform.position, camPos, 4.5f * Time.deltaTime);
               
                yield return null;
            }

         
            Reset();
            _shakerCamera.transform.rotation = camRot;
            _shakerCamera.transform.position = camPos;
            Debug.Log("finished shaking");
           
        }

    }
