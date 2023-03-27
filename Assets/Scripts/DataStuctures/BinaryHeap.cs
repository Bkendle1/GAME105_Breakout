/*
 *
 *
 * Binary Heap
 * Datastructures class 2012
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public delegate int Compare(IComparable x, IComparable y);

public class BinaryHeap<T> where T : IComparable
{
    protected Compare m_comp;   
    protected List<T> m_dataList = new List<T>();

    public  int Size => m_dataList.Count;
    public bool IsEmpty => m_dataList.Count == 0;
    
    
    public BinaryHeap()
    {
        m_comp = (x, y) => x.CompareTo(y);
    }


    #region Public

    public void Push(T data)
    {
        m_dataList.Add(data);
        int index = Size - 1;
        
        while (index > 0)
        {
            int parent = IndexParent(index);

            if (m_comp(m_dataList[index], m_dataList[parent]) < 0)
            {
                SwapIndexes(index, parent);
                index = parent;
            }
            else
            {
                break;
            }
        }
    }
    
    public void Push(T[] dataArray)
    {
        foreach (T data in dataArray)
        {
            Push(data);
        }
    }

    public T Pop()
    {
        if (IsEmpty)
        {
            Debug.LogError("Called Pop on Empty BinaryHeap");
           
        }

        int index = 0;
        T returnData = m_dataList[index];
        SwapIndexes(0, Size - 1);
        m_dataList.RemoveAt(Size - 1);

        while (index < Size)
        {
            int childIndex = ChildIndexMin(index);

            if (childIndex >= 0 && m_comp(m_dataList[childIndex], m_dataList[index]) < 0)
            {
                SwapIndexes(index, childIndex);
                index = childIndex;
            }
            else break;
        }

        return returnData;
    }
    
    public T[] Pop(int numberToPop)
    {
        T[] returnData = new T[numberToPop];
        for (int i = 0; i < numberToPop; ++i)
        {
            returnData[i] = Pop();
        }
        return returnData;
    }

    public T[] PopAll()
    {
        return Pop(Size);
    }
    

    public T Peek()
    {
        if (IsEmpty)
        {
            Debug.LogError("Called Peek on Empty BinaryHeap");
        }

        return m_dataList[0];
    }
    
    public T[] Peek(int numberToPeek)
    {
        T[] returnData = new T[numberToPeek];
        for (int i = 0; i < numberToPeek; ++i)
        {
            returnData[i] = Pop();
        }

        Push(returnData);
        return returnData;
    }
    
    public T[] PeekAll()
    {
        List<T> original = new List<T>(m_dataList);
        int n = Size;
        T[] returnData = new T[n];
        
        for (int i = 0; i < n; ++i)
        {
            returnData[i] = Pop();
        }
        m_dataList = original;

        return returnData;
    }

    
    
    #endregion
    
    #region Protected
    
    protected int IndexParent(int i)
    {
        return (i + 1) / 2 - 1;
    }

    protected int ChildIndexMin(int i)
    {
        int rightChild= (i + 1) * 2;
        int leftChild = rightChild - 1;

        if (leftChild >= Size) 
            return -1;
        else if (rightChild < Size && m_comp(m_dataList[rightChild], m_dataList[leftChild]) < 0) 
            return rightChild;
        else 
            return leftChild;
    }
    protected void SwapIndexes(int i, int j)
    {
        T tempData = m_dataList[i];
        m_dataList[i] = m_dataList[j];
        m_dataList[j] = tempData;
    }
    #endregion
}
