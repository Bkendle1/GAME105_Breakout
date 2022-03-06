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

[RequireComponent(typeof(AudioSource))]
public class Paddle : MonoBehaviour,IHandlerInput
{
    [Header("Game Feel")] 
    [SerializeField] private float m_speedDefault = 5f, m_acceleration =1.2f;
    [Header("Bounds")] 
    [SerializeField] private float  m_leftBounds, m_rightBounds;
    [Header(("Ball"))]
    [SerializeField] private Ball m_gameBall;
    [SerializeField] private Transform m_ballSpawnPoint = null;
    
    private float  m_speed;
    private Vector3 m_location = Vector3.zero;
    private Vector3 m_startLocation;
    private MeshRenderer m_meshRender;
    private AudioSource m_audioSource;
    
    public Transform GetPaddleTransform
    {
        get { return this.transform; }
    }
    public Transform GetPaddleBallSpawnPointTransform
    {
        get { return m_ballSpawnPoint.transform; }
    }
    #region UnityAPI
    

    private void Start()
    {
       
        Setup();
        m_startLocation = this.transform.position;
        m_location = m_startLocation;
        m_speed = m_speedDefault;
        m_audioSource = this.GetComponent<AudioSource>();
        m_meshRender = this.GetComponent<MeshRenderer>();
        GameManager.Instance.LiveLost += Death;
        NullChecks();
    }

   

    private void OnDestroy()
    {
        GameManager.Instance.LiveLost -= Death;
    }

    #endregion

    public void Respawn()
    {
        transform.position  = m_startLocation;
        m_location = m_startLocation;
        transform.SetPositionAndRotation(m_location, Quaternion.identity);
        m_speed = m_speedDefault;
        m_meshRender.enabled = true;
        m_gameBall.ResetBall();
    }

    public void SetPaddleSpeed(float value)
    {
        m_speed = value;
    }

    private void Death()
    {
        m_meshRender.enabled = false;
    }

    private void AxisXMovement(float value)
    {
        m_location.x += value * m_speed * Time.deltaTime;
        m_location.x = Mathf.Clamp(m_location.x, m_leftBounds, m_rightBounds);
        transform.SetPositionAndRotation(m_location, Quaternion.identity);

    }
    private void AxisYMovement(float value)
    {
        //TODO:Paddle Movement Here
    }

    private void FireBall()
    {
        if (m_gameBall.IsBallInPlay)
            return;
        m_gameBall.LaunchBall();
    }

    private void Fire()
    {
        //TODO: Fire Weapon pickup if your game has firing in it 
    }

    private void NullChecks()
    {
        Assert.IsNotNull(GameManager.Instance);
        Assert.IsNotNull(InputController.Instance);
        Assert.IsNotNull(m_audioSource);
        Assert.IsNotNull(m_gameBall);
        Assert.IsNotNull(m_ballSpawnPoint);
        Assert.IsNotNull(m_meshRender);
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
