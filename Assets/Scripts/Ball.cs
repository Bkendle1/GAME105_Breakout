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
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, IDeath
{
    [SerializeField] private Paddle m_paddle;
    [Header("launch properties")]
    [Range(35, 90f)] [SerializeField] private float m_lauchAngleRangemax = 90f, m_lauchAngleRangemin = 45f;
    [Range(50, 90f)] [SerializeField] private float  m_launchSpeedmax = 90f, m_launchSpeedmin =50f;
    private Rigidbody m_rigidbody = null;
    private AudioSource m_audioSource;
    private MeshRenderer m_meshRender;
    private bool m_ballInPlay = false;
  
    public  bool IsBallInPlay
    {
        get { return m_ballInPlay; }
    }
    private void Start()
    {
        
        m_rigidbody = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        m_meshRender = GetComponent<MeshRenderer>();
        m_rigidbody.useGravity = false;
        NullChecks();
        ResetBall();
    }
    
    public void ResetBall()
    {
        m_rigidbody.isKinematic = true;
        m_rigidbody.velocity = Vector3.zero;
        transform.SetParent(m_paddle.GetPaddleBallSpawnPointTransform);
        transform.localPosition = Vector3.zero;
        m_meshRender.enabled = enabled;

    }


    public void SetVelocity(Vector2 vel)
    {
        
    }

    public void LaunchBall()
    {
        if(m_ballInPlay)
            return;
        m_ballInPlay = true;
        transform.SetParent(null);
        m_rigidbody.isKinematic = false;
        m_rigidbody.AddForce(RandomizeLaunchDirection(), -RandomizeLaunchSpeed(), 0.0f);
        
    }
    

    #region Interfaces

    public void Death()
    {
        GameManager.Instance.UpdateLives(-1);
        m_meshRender.enabled = false;
        
        
    }

    #endregion

    private float RandomizeLaunchDirection()
    {
        return Random.Range(m_lauchAngleRangemin, m_lauchAngleRangemax);
    }

    private float RandomizeLaunchSpeed()
    {
        return Random.Range(m_launchSpeedmin, m_launchSpeedmax);
    }
    
  
    private void NullChecks()
    {
        Assert.IsNotNull(m_paddle);
        Assert.IsNotNull(m_rigidbody);
        Assert.IsNotNull(m_audioSource);
        Assert.IsNotNull(m_meshRender);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IHit>() !=null)
        {
            other.gameObject.GetComponent<IHit>().BeenHit(); 
        }
    }
}
