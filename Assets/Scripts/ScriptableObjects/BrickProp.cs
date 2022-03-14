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

[CreateAssetMenu(fileName = "BrickObject", menuName = "ScriptableObjects/Brick", order = 3)]
public class BrickProp : ScriptableObject
{ 
    [SerializeField] private int m_scoreValue = 50, m_hitPoints =1;
    [SerializeField] private float m_powerUpDropChance = 20f;
    [Range(0.0f, 1f)] [SerializeField] private float m_screenShakehit = 0.05f;
    [SerializeField] private AudioClip m_hitSFX;
    [SerializeField] private Material m_brickMaterial;
    [SerializeField] private Mesh m_brickMesh;
    [SerializeField] private GameObject m_deathParticle;
    
    public int GetHitPoints => m_hitPoints;
    public int GetScoreValue => m_scoreValue;
    public float GetPowerUpDropChance=> m_powerUpDropChance;
    public float GetScreenShakeAmount=> m_screenShakehit;
    public ref AudioClip GetHitSFX => ref m_hitSFX;
    public ref Material GetBrickMaterial => ref m_brickMaterial;
    public ref Mesh GetBrickMesh => ref m_brickMesh;
    public  GameObject GetDeathParticle => m_deathParticle;
    public void NullChecks()
    {
        Assert.IsNotNull(m_hitSFX);
        Assert.IsNotNull(m_brickMaterial);
        Assert.IsNotNull(m_deathParticle);
        Assert.IsNotNull(m_brickMesh);
    
    }
}
