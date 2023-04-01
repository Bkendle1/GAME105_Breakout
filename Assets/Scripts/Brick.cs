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

public class Brick : PoolObject, IHit
{
    [SerializeField] private BrickProp m_brickProperties;
    private AudioSource m_audioSource;
    private MeshRenderer m_meshRender;
    private MeshFilter m_meshFilter;
    private int m_hitpoints;

    private Pooling m_deathEffectPool;
    private string poolName;

    #region UnityAPI

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_meshRender = GetComponent<MeshRenderer>();
        m_meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
       

        NullChecks();
        SetupBrick();
    }

    #endregion

    #region Public

    public override void ReturnToPool()
    {
        GameManager.Instance.UpdateScore(m_brickProperties.GetScoreValue);
        base.ReturnToPool();
        
    }

    #endregion

    #region Interfaces

    public void BeenHit()
    {
        //TODO: Make Brick getting hit JUICY!
        m_audioSource.PlayOneShot(m_brickProperties.GetHitSFX, GameSettings.SFXVolumeGet);
        //if hitpoints are greater than 1, destroy else decrement
        m_hitpoints--;
        if (m_hitpoints <= 1)
        {
            GameManager.Instance.UpdateScore(m_brickProperties.GetScoreValue);
            m_deathEffectPool.Get(this.transform.localPosition, this.transform.localRotation);
            GameManager.Instance.BrickCount(-1);
            Destroy(this.gameObject);
        }
    }

    public void BeenHit(GameObject HitByObject)
    {
    }

    #endregion


    
    private void SetupBrick()
    {
        m_meshRender.material = m_brickProperties.GetBrickMaterial;
        m_meshFilter.mesh = m_brickProperties.GetBrickMesh;
        m_hitpoints = m_brickProperties.GetHitPoints;
        if (!PoolManager.DoesPoolExist(m_brickProperties.GetDeathParticle.gameObject.name))
        {
            PoolManager.CreatePool(m_brickProperties.GetDeathParticle.gameObject.name, m_brickProperties.GetDeathParticle, 3);
        }
        m_deathEffectPool = PoolManager.GetPool(m_brickProperties.GetDeathParticle.gameObject.name);

    }

    private void NullChecks()
    {
        Assert.IsNotNull(m_audioSource);
        Assert.IsNotNull(m_meshRender);
        Assert.IsNotNull(m_meshFilter);
        Assert.IsNotNull(m_brickProperties);
        m_brickProperties.NullChecks();
    }
}