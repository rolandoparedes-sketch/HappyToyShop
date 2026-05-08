using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [FoldoutGroup("GameSettings")]
    public MyQueue<int> Day = new();
    [FoldoutGroup("GameSettings")]
    public int NumbersOfDays = 7;
  
    public Action OnNextDay;
    public bool TurnDay = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < NumbersOfDays; i++)
        {
            Day.Enqueue(i+1);

        }


    }


    void Update()
    {

    }
    [Button]
    public void NewDay(int day)
    {
        Day.Enqueue(day);
    }

    [Button]
    public void NextDay()
    {
        Debug.Log("Día " + Day.Dequeue() + " finalizado");
        
        OnNextDay?.Invoke();
        
        
    }

    [Button]
    public void Peek()
    {
        Debug.Log("Día actual: " + Day.Peek());
        
    }
    [Button]
    public void Clear()
    {
        Day.Clear();
    }
}
