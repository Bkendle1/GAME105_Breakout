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

[RequireComponent(typeof(SFXPlayer))]
public class Paddle : MonoBehaviour,IHandlerInput
{
    [SerializeField] private PaddleProp m_paddleProperties;
    [Header("Bounds")] 
    [SerializeField] private float  m_leftBounds, m_rightBounds;
    [Header(("Ball"))]
    [SerializeField] private Ball m_gameBall;
    [SerializeField] private Transform m_ballSpawnPoint = null;
    [SerializeField] private float m_respawnTime = 3f;
    private float  m_speed, m_acceleration;
    private Vector3 m_location = Vector3.zero;
    private Vector3 m_startLocation;
    private MeshRenderer m_meshRender;
    private MeshFilter m_meshFilter;
    private SFXPlayer m_sfxPlayer;
    private Pooling m_deathPool = null;
    private bool m_isDead =false;
    public Transform GetPaddleTransform => this.transform;
    public Transform GetPaddleBallSpawnPointTransform => m_ballSpawnPoint.transform;

    #region UnityAPI

    private void Awake()
    {
        m_sfxPlayer = this.GetComponent<SFXPlayer>();
        m_meshRender = this.GetComponent<MeshRenderer>();
        m_meshFilter = this.GetComponent<MeshFilter>();
    }

    private void Start()
    {
        Setup();
        m_startLocation = this.transform.position;
        m_location = m_startLocation;
        GameManager.Instance.LiveLost += Death;
        GameManager.Instance.EndGame += FinalDeath;
        NullChecks();
        SetupPaddle();
    }



    private void OnDisable()
    {
        GameManager.Instance.LiveLost -= Death;
        GameManager.Instance.EndGame -= FinalDeath;
    }

    private void OnDestroy()
    {
        
        PoolManager.DeletePool(m_paddleProperties.GetDeathParticle.gameObject.name);
    }

    #endregion

    #region public

    public void SetPaddleSpeed(float value)
    {
        m_speed = value;
    }

    public void ChangePaddle(PaddleProp prop)
    {
        PoolManager.DeletePool(m_paddleProperties.GetDeathParticle.gameObject.name);
        m_paddleProperties = prop;
        
        SetupPaddle();
    }
    #endregion
    
    #region Private

    private void Death()
    {
        m_isDead = true;
        m_meshRender.enabled = false;
        m_deathPool.Get(this.transform.position, this.transform.rotation);
        Invoke("Respawn", m_respawnTime);
    }

    private void FinalDeath()
    {
        m_isDead = true;
        m_meshRender.enabled = false;
        CameraShake.Shake(1f);
        StartCoroutine(CameraShake.CamerShake());

    }

    private void AxisXMovement(float value)
    {
        if (m_isDead)
            return;
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
        if (m_gameBall.IsBallInPlay || m_isDead)
            return;
        m_gameBall.LaunchBall();
    }

    private void Fire()
    {
        //TODO: Fire Weapon pickup if your game has firing in it 
    }

    private void SetupPaddle()
    {
        PoolManager.CreatePool(m_paddleProperties.GetDeathParticle.gameObject.name, m_paddleProperties.GetDeathParticle, 3);
        m_deathPool = PoolManager.GetPool(m_paddleProperties.GetDeathParticle.gameObject.name);
        m_speed = m_paddleProperties.GetDefaultSpeed;
        m_acceleration = m_paddleProperties.GetAcceleration;
        m_meshFilter.mesh = m_paddleProperties.GetPaddleMesh;
        m_meshRender.material = m_paddleProperties.GetPaddleMaterial;
        
    }

    private void Respawn()
    {
        Debug.Log("resapwned");
        m_isDead = false;
        m_location = m_startLocation;
        transform.SetPositionAndRotation(m_location, Quaternion.identity);
        m_speed = m_paddleProperties.GetDefaultSpeed;
        transform.position = m_startLocation;
        m_meshRender.enabled = true;
        m_gameBall.ResetBall();
    }

    private void NullChecks()
    {
        Assert.IsNotNull(GameManager.Instance);
        Assert.IsNotNull(InputController.Instance);
        Assert.IsNotNull(m_sfxPlayer);
        Assert.IsNotNull(m_gameBall);
        Assert.IsNotNull(m_ballSpawnPoint);
        Assert.IsNotNull(m_meshRender);
        Assert.IsNotNull(m_paddleProperties);
        m_paddleProperties.NullChecks();
    }
    
    #endregion
    
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
