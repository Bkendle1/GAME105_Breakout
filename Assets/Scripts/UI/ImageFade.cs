
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
using UnityEngine.UI;
namespace UI
{
    [RequireComponent(typeof(Image))]
    public class ImageFade : Fade
    {
        private Image _image;
        private void Awake()
        {
            _image = this.GetComponent<Image>();
        }
        #region public
        public void ResetAlpha(float alpha)
        {
            StopFade();
            Color color = _image.color;
            color.a = alpha;
            _image.color = color;
        }

        public void StartFade(float time, float alpha)
        {

            _fade = StartCoroutine(FadeObject(time, alpha));
        }
        #endregion

        protected override void ObjectToFade(float amount)
        {
            Color color = _image.color;
            color.a += amount * Time.deltaTime;
            _image.color = color;       
            
        }
    }
}
