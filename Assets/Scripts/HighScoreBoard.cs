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

public class HighScoreBoard : MonoBehaviour
{
    
    [SerializeField]
    private int m_scoreListMax = 10;
    
    private ScoreboardItem[] m_displayData;
    private BinaryHeap<ScoreboardItem> m_priorityQueue = new BinaryHeap<ScoreboardItem>();
  


    #region UNITYAPI

    private void Start()
    {
        if(!GameSettings.HasConfigLoaded)
            GameSettings.LoadData();
        SetupScoreBoard();
    }
    
    #endregion
    
    #region Public

    
    public void AddEntry(string name, int score)
    {
        m_priorityQueue.Push(new ScoreboardItem(name, score));

        GameSettings.GetScoreItems.Add(new ScoreItem(name, score));
        
        if (m_priorityQueue.Size <= m_scoreListMax || score > m_displayData[m_displayData.Length - 1].itemScore.Score)
            m_displayData = m_priorityQueue.Peek(GetDisplayAmount());
        
    }

    
    #endregion

    private int GetDisplayAmount()
    {
        return m_priorityQueue.Size < m_scoreListMax ? m_priorityQueue.Size : m_scoreListMax;
        
    }
    private void SetupScoreBoard()
    {
       

        foreach (ScoreItem ScoreItem in GameSettings.GetScoreItems)
            {
                Debug.Log(ScoreItem);
                m_priorityQueue.Push(new ScoreboardItem (ScoreItem.Name, ScoreItem.Score));
            }
        
        
      
        m_displayData = m_priorityQueue.Peek(GetDisplayAmount());
    }
}
public class ScoreboardItem : IComparable
{
    public ScoreItem itemScore;

    public ScoreboardItem(string n, int s)
    {
        itemScore.Name = n;
        itemScore.Score= s;
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
            Debug.LogAssertion("Object to compare is Null");
        ScoreboardItem other = obj as ScoreboardItem;
        if (other == null)
            Debug.LogAssertion("ScoreboardEntry.CompareTo not a ScoreboardEntry");
        
        
        return other.itemScore.Score.CompareTo(itemScore.Score);
    }
}
