using UnityEngine;

public class MyQueue<T>
{
    #region Properties/Privates
    private QueueNode<T> head = null;
    private QueueNode<T> tail = null;
    public int count { get; private set; } = 0;
    #endregion


    #region Methods
    public void Enqueue(T value)
    {

        QueueNode<T> newNode = new(value);
        count++;
        if (head == null && tail == null)
        {
            head = newNode;
            tail = newNode;
            return;
        }
        tail.SetNext(newNode);
        tail = newNode;
    }

    public T Dequeue()
    {
        if(head == null)
        {
            Clear();
            throw new System.InvalidOperationException("Queue is empty");
            
        }
        
        T value = head.Value;
        head = head.Next;
        count--;
        return value;

    }
    public T Peek()
    {
        if (head == null)
        {
            Clear();
            throw new System.InvalidOperationException("Queue is empty");
        }
        return head.Value;
    }
    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
    }
    #endregion

    #region Getters
    public int Count => count;
    #endregion
}
