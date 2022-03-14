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

        
        private static float m_shakeAmount =1f;
        private static bool _holdShake;
        private static GameObject m_shakerCamera = null;
        private static CameraShakeConfig m_shakeConfig = null;
        public static bool IsShaking { get; private set; } = false;
        
        
        public static void ClearCamera()
        {
            m_shakerCamera = null;
            
        }

        public static void  SetNewCamera(  GameObject Object)
        {
            m_shakerCamera = Object;
        }

        public static void CreateShakeSetup(CameraShakeConfig config,  GameObject Object)
        {
            m_shakerCamera = Object;
            m_shakeConfig = config;
        }

        public static void Shake(float amout)
        {
            m_shakeAmount = amout;
        }
        

        public static IEnumerator CamerShake()
        {
            if (!m_shakerCamera)
                yield break;
            Vector3 lastPos = Vector3.zero, lastRot = Vector3.zero;
            Vector3 previousPos, previousRot;
            float decreaseAmount = m_shakeAmount; 
            float shake = Mathf.Pow(decreaseAmount, m_shakeConfig.GetScaleExpontent);
            IsShaking = true;
            while (shake > 0)
            {
                previousPos = lastPos;
                previousRot = lastRot;
                lastPos.x = m_shakeConfig.GetMaxTranslationShake.x * (Mathf.PerlinNoise(0, Time.time * 25) * 2 - 1);
                lastPos.y = m_shakeConfig.GetMaxTranslationShake.y * (Mathf.PerlinNoise(1, Time.time * 25) * 2 - 1);
                lastPos.z = m_shakeConfig.GetMaxTranslationShake.z * (Mathf.PerlinNoise(2, Time.time * 25) * 2 - 1);
                lastPos *= shake;
                
                lastRot.x = m_shakeConfig.GetMaxTranslationShake.x * (Mathf.PerlinNoise(3, Time.time * 25) * 2 - 1);
                lastRot.y = m_shakeConfig.GetMaxTranslationShake.y * (Mathf.PerlinNoise(4, Time.time * 25) * 2 - 1);
                lastRot.z = m_shakeConfig.GetMaxTranslationShake.z * (Mathf.PerlinNoise(5, Time.time * 25) * 2 - 1);
                lastRot *= shake;
                
                m_shakerCamera.transform.localPosition += lastPos - previousPos;
                m_shakerCamera.transform.localRotation = Quaternion.Euler(m_shakerCamera.transform.localRotation.eulerAngles + lastRot - previousRot);

                decreaseAmount = Mathf.Clamp01(decreaseAmount - Time.deltaTime);
                shake = Mathf.Pow(decreaseAmount, m_shakeConfig.GetScaleExpontent);
                yield return new WaitForEndOfFrame();
            }
            IsShaking = false;
            m_shakerCamera.transform.localPosition -= lastPos;
            m_shakerCamera.transform.localRotation  = Quaternion.Euler(m_shakerCamera.transform.localRotation.eulerAngles + lastRot);
          

            
           
        }

    }
