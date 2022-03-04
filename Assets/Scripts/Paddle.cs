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

public class Paddle : MonoBehaviour,IHandlerInput
{
    #region UnityAPI

    
    private void Start()
    {
        Setup();
    }

 

    #endregion

    private void AxisXMovement(float value)
    {
        //TODO:Paddle Movement Here
    }
    private void AxisYMovement(float value)
    {
        //TODO:Paddle Movement Here
    }

    private void FireBall()
    {
        //TODO:Fire Ball into feild;
    }

    private void Fire()
    {
        //TODO: Fire Weapon pickup if your game has firing in it 
    }

    #region Interfaces
    
    public void Setup()
    {
        Debug.Log("Adding Paddle Input");
        InputController.Instance.AxisX += AxisXMovement;
        InputController.Instance.AxisY += AxisYMovement;
        InputController.Instance.JumpPressed += FireBall;
        InputController.Instance.FirePressed += Fire;
        InputController.Instance.CleanUp += CleanUp;
        InputController.Instance.SetHandler = this.GetComponent<IHandlerInput>();
    }

    public void CleanUp()
    {
        Debug.Log("Removing Paddle Input");
        InputController.Instance.AxisX -= AxisXMovement;
        InputController.Instance.AxisY -= AxisYMovement;
        InputController.Instance.JumpPressed -= FireBall;
        InputController.Instance.FirePressed -= Fire;
        InputController.Instance.CleanUp -= CleanUp;
        
    } 
    #endregion
}
