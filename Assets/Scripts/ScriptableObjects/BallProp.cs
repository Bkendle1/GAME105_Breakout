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
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "BallObject", menuName = "ScriptableObjects/Ball", order = 1)]
public class BallProp : ScriptableObject
{
    [SerializeField] private AudioClip m_deathSFX,m_wallHitSFX;
    [SerializeField] private Material m_ballMaterial;
    [SerializeField] private Mesh m_ballMesh;
    [SerializeField] private GameObject m_deathParticle;
    [SerializeField] private float m_launchSpeedMin, m_launchSpeedMax, m_launchAngleMin, m_launchAngleMax, m_bounceSpeed;
    
    public float GetBallBounceSpeed => m_bounceSpeed;
    public float GetLaunchSpeedMin => m_launchSpeedMin;
    public float GetLaunchSpeedMax => m_launchSpeedMax;
    public float GetLaunchAngleMin => m_launchAngleMin;
    public float GetLaunchAngleMax => m_launchAngleMax;
    public ref AudioClip GetDeatSFX => ref m_deathSFX;
    public ref AudioClip GetWallHitSFX => ref m_wallHitSFX;
    public ref Material GetBallMaterial => ref m_ballMaterial;
    public ref Mesh GetBallMesh => ref m_ballMesh;

    public void NullChecks()
    {
        Assert.IsNotNull(m_deathSFX);
        Assert.IsNotNull(m_wallHitSFX);
        Assert.IsNotNull(m_deathParticle);
        Assert.IsNotNull(m_ballMaterial);
        Assert.IsNotNull(m_ballMesh);
    }
}
