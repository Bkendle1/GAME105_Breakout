
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

namespace UI
{
    public abstract class Fade : MonoBehaviour
    {

        protected Coroutine _fade = null;

      
        #region public

        public virtual bool IsFading()
        {
            if (_fade == null)
                return false;

            return true;
        }


        public virtual void StopFade()
        {
            if (_fade == null)
                return;
            StopCoroutine(_fade);
            _fade = null;
        }

        #endregion

        #region protected

        protected virtual void ObjectToFade(float amount)
        {

        }

        protected virtual IEnumerator FadeObject(float time, float target)
        {
            float timeFinish = Time.time + time;
            float amount = target / time;
            while (timeFinish > Time.time)
            {
                ObjectToFade(amount);
                yield return null;
            }

            _fade = null;
        }
        #endregion
    }
}