using Sirenix.OdinInspector;
using System;
using HappyToyShop.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [FoldoutGroup("References")]
    public static GameManager instance;
    [FoldoutGroup("References")]
    public ParanormalSuccess3D paranormalSuccess;
    [FoldoutGroup("References")]
    public Transform Player;
    [FoldoutGroup("References")]
    public MusicDatabase MusicDatabase;
    [FoldoutGroup("References")]
    public MusicPool MusicPool;
    [FoldoutGroup("GameSettings")]
    public MyQueue<int> Day = new();
    [FoldoutGroup("GameSettings")]
    public int NumbersOfDays = 7;
  
    public Action OnNextDay;

    public Action OnWeekComplete;
    [FoldoutGroup("GameSettings")]
    public bool TurnDay = true;
    [FoldoutGroup("GameSettings")]
    public List<int> SpecialDays = new();
    [FoldoutGroup("GameSettings")]

    public MyQueue<string> TextEvents = new();

    public bool SpecialDay { get; private set; } = false;
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

        SpecialDays = new List<int>() { 2, 4, 7};

        NewText("InitalTextVoid");

        NewText("Hay algo afuera...");

        NewText("Escuchaste eso?...");

        NewText("Alguien entro al local...");

    }


    void Update()
    {

    }
    [Button]
    public void NewText(string text)
    {
        TextEvents.Enqueue(text);
    }

    [Button]
    public void NextDay()
    {
        if (Day.Count <= 1)
        {
            Debug.Log("Semana Completada");
            OnWeekComplete?.Invoke();
            return;
        }
        Debug.Log("Día " + Day.Dequeue() + " finalizado");
        
        OnNextDay?.Invoke();
        
        if(SpecialDays.Contains(Day.Peek()))
        {
            SpecialDay = true;
            Debug.Log("Día " + Day.Peek() + " es un día especial");
            TextEvents.Dequeue();

        }
        else
        {
            SpecialDay = false;
        }


    }

    [Button]
    public void LookDay()
    {
        Debug.Log("Día actual: " + Day.Peek());
        
    }
    public void LookText()
    {
        Debug.Log("Texto actual: " + TextEvents.Peek());

    }

    [Button]
    public void Clear()
    {
        Day.Clear();
    }
    [Button]
    public void Count()
    {
        Debug.Log("Días restantes: " + Day.Count);
    }
}
