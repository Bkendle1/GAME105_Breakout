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
using System.ComponentModel.Design;
using System.Net.Mail;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;


[RequireComponent(typeof(SFXPlayer))]
[RequireComponent(typeof(Rigidbody))]
public class CloneBall : MonoBehaviour, IDeath
{
    //[SerializeField] private Paddle m_paddle;
    [SerializeField] private BallProp m_ballProperties;
    [Range(0.0f, 1f)]
    [SerializeField] private float m_deathScreenShake = 0.5f, m_wallHitShake = 0.1f;
    [SerializeField] private int startingPool = 10;
    private Rigidbody m_rigidbody = null;
    private SFXPlayer m_sfxPlayer = null;
    private MeshRenderer m_meshRender;
    private MeshFilter m_meshFilter;
    //private bool m_ballInPlay = false;
    private Vector3 m_velocityAtPause = Vector3.zero;
    private Pooling m_deathPool = null;
    private TrailRenderer m_trail;
    private SphereCollider _sphereCollider;

    //public bool IsBallInPlay => m_ballInPlay;

    #region UnityAPI

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_meshRender = GetComponent<MeshRenderer>();
        m_meshFilter = GetComponent<MeshFilter>();
        m_trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        m_sfxPlayer = GetComponent<SFXPlayer>();
        //m_paddle = FindObjectOfType<Paddle>();
        m_rigidbody.useGravity = false;
        _sphereCollider = GetComponent<SphereCollider>();
        NullChecks();
        SetupBallSettings();
        //ResetBall();
    }

    private void OnEnable()
    {
        GameManager.Instance.GamePaused += FreezeOnPausedGame;
        GameManager.Instance.GameResumed += UnFreezeOnResumeGame;
        //Launch clone ball once spawned 
        LaunchBall();
    }

    private void OnDisable()
    {
        GameManager.Instance.GamePaused -= FreezeOnPausedGame;
        GameManager.Instance.GameResumed -= UnFreezeOnResumeGame;
    }

    private void OnDestroy()
    {
        PoolManager.DeletePool(m_ballProperties.GetDeathParticles.gameObject.name);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IHit>() != null)
        {
            other.gameObject.GetComponent<IHit>().BeenHit();
            
        }
        else
        {
             m_sfxPlayer.PlayAudioClip(ref m_ballProperties.GetWallHitSFX);
             CameraShake.Shake(m_wallHitShake);
             StartCoroutine(CameraShake.CamerShake());
             
        }
    }

    #endregion

    #region Public

    public void UpdateBallProperties(BallProp prop)
    {
        m_ballProperties = prop;
        PoolManager.DeletePool(m_ballProperties.GetDeathParticles.gameObject.name);
        SetupBallSettings();
    }

    public void ResetBall()
    {
        //m_ballInPlay = false;
        //m_rigidbody.isKinematic = true;
        //m_rigidbody.velocity = Vector3.zero;
        //transform.SetParent(m_paddle.GetPaddleBallSpawnPointTransform);
        //this line prevents the transforms of the paddle from distorting the transforms of the ball
        //putting the ball into a parent object and then putting that parent object into the spawn point
        //protects the ball's original transforms
        //transform.parent.SetParent(m_paddle.GetPaddleBallSpawnPointTransform);
        
        //transform.localPosition = Vector3.zero;
        //m_meshRender.enabled = enabled;
        //m_trail.enabled = false;
    }


    public void SetVelocity(Vector2 vel)
    {
    }

    public void LaunchBall()
    {
        //if (m_ballInPlay)
            //return;
        //m_ballInPlay = true;
        //we need to null out the ball's parent instead of the ball
        //transform.parent.SetParent(null);
        //transform.SetParent(null);
        m_rigidbody.isKinematic = false;
        m_rigidbody.AddForce(RandomizeLaunchDirection(), -RandomizeLaunchSpeed(), 0.0f);
        m_trail.enabled = true;
    }

    #endregion


    #region private

    private void SetupBallSettings()
    {
        
        PoolManager.CreatePool(m_ballProperties.GetDeathParticles.gameObject.name, m_ballProperties.GetDeathParticles, startingPool);
        m_deathPool = PoolManager.GetPool(m_ballProperties.GetDeathParticles.gameObject.name);
        m_meshRender.material = m_ballProperties.GetBallMaterial;
        m_meshFilter.mesh = m_ballProperties.GetBallMesh;
    }

    private float RandomizeLaunchDirection()
    {
        return Random.Range(m_ballProperties.GetLaunchAngleMin, m_ballProperties.GetLaunchAngleMax);
    }

    private float RandomizeLaunchSpeed()
    {
        return Random.Range(m_ballProperties.GetLaunchSpeedMin, m_ballProperties.GetLaunchSpeedMax);
    }


    private void NullChecks()
    {
        //Assert.IsNotNull(m_paddle);
        Assert.IsNotNull(m_rigidbody);
        Assert.IsNotNull(m_sfxPlayer);
        Assert.IsNotNull(m_meshRender);
        Assert.IsNotNull(m_meshFilter);
        Assert.IsNotNull(m_ballProperties);
        m_ballProperties.NullChecks();
    }


    private void FreezeOnPausedGame()
    {
        m_velocityAtPause = m_rigidbody.velocity;
        m_rigidbody.isKinematic = true;
    }

    private void UnFreezeOnResumeGame()
    {
        m_rigidbody.isKinematic = false;
        m_rigidbody.velocity =  m_velocityAtPause;
    }

    #endregion

    #region Interfaces

    public void Death()
    {
        //if (!m_ballInPlay)
            //return;
        //m_ballInPlay = false;
        //GameManager.Instance.UpdateLives(-1);
        //m_rigidbody.isKinematic = true;
        //m_rigidbody.velocity = Vector3.zero;
        //m_meshRender.enabled = false;
        //Destroy clone ball when out of bounds

        m_meshRender.enabled = false;
        m_trail.enabled = false;
        _sphereCollider.enabled = false;
        m_deathPool.Get(this.transform.position, this.transform.rotation );
        m_sfxPlayer.PlayAudioClip(ref m_ballProperties.GetDeatSFX);
        CameraShake.Shake(m_deathScreenShake);
        StartCoroutine(CameraShake.CamerShake());
        Destroy(gameObject,m_ballProperties.GetDeatSFX.length);
        
    }

    #endregion
}