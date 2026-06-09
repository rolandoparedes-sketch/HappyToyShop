using HappyToyShop.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [FoldoutGroup("GameSettings")]
    [SerializeField] private MyQueue<int> monthDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private int daysInMonth = 30;
    [FoldoutGroup("GameSettings")]
    [SerializeField] private MyQueue<Months> Months = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private MyQueue<Days> weekDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private MyQueue<SpecialDay> specialDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private bool todayIsSpecial = false;
    [SerializeField] private SpecialDay currentEvent;

    public Action OnNextDay;
    public Action OnWeekComplete;


    public struct SpecialDay
    {
        public int Day;
        public string EventText;
        public DayEvents DayEvents;
    }
    void Start()
    {
        DaysInitializer();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DaysInitializer()
    {
        for (int i = 1; i <= daysInMonth; i++)
        {
            monthDays.Enqueue(i);
        }

        weekDays.Enqueue(Days.Monday);
        weekDays.Enqueue(Days.Tuesday);
        weekDays.Enqueue(Days.Wednesday);
        weekDays.Enqueue(Days.Thursday);
        weekDays.Enqueue(Days.Friday);
        weekDays.Enqueue(Days.Saturday);
        weekDays.Enqueue(Days.Sunday);
        Months.Enqueue(global::Months.January);
        Months.Enqueue(global::Months.February);
        Months.Enqueue(global::Months.March);
        Months.Enqueue(global::Months.April);
        Months.Enqueue(global::Months.May);
        Months.Enqueue(global::Months.June);
        Months.Enqueue(global::Months.July);
        Months.Enqueue(global::Months.August);
        Months.Enqueue(global::Months.September);
        Months.Enqueue(global::Months.October);
        Months.Enqueue(global::Months.November);
        Months.Enqueue(global::Months.December);


        specialDays.Enqueue(new SpecialDay()
        {
            Day = 2,
            EventText = "Hay algo afuera...",
            DayEvents = DayEvents.MysteryVisitor

        });

        specialDays.Enqueue(new SpecialDay()
        {
            Day = 4,
            EventText = "Escuchaste eso?...",
            DayEvents = DayEvents.MysterySounds
        });

        specialDays.Enqueue(new SpecialDay()
        {
            Day = 7,
            EventText = "Alguien entró al local...",
            DayEvents = DayEvents.HorrorDay
        });
    }


    [Button]
    public void NextDay()
    {
       /* if (weekDays.Peek() == Days.Sunday)
        {
            Debug.Log("Semana Completada");
            OnWeekComplete?.Invoke();
            return;
        }
       */

        // Mover el día del calendario y semana actual al final de la cola para avanzar al siguiente día
        int finishedDay = monthDays.Dequeue();

        monthDays.Enqueue(finishedDay);

        Debug.Log("Día " + finishedDay + " finalizado");


        Days WeekDay = weekDays.Dequeue();

        weekDays.Enqueue(WeekDay);


        Debug.Log(weekDays.Peek() + ", "+ Months.Peek() + " " + monthDays.Peek());

        OnNextDay?.Invoke();


        // Avanzar el mes si se ha completado el número de días en el mes

        if (finishedDay == MonthDays.Count) 
        {
            Months month = Months.Dequeue();
            Months.Enqueue(month);

            Debug.Log("Month completed");
        }

        // Verificar si el día actual es un día especial
        if (specialDays.Count > 0 && specialDays.Peek().Day == monthDays.Peek())
        {
            Debug.Log("Día " + monthDays.Peek() + " es un día especial");

            currentEvent = specialDays.Dequeue();

            //Debug.Log(currentEvent.EventText);

            // Llamar a ParanormalSuccess3D para activar eventos paranormales especiales en funcion del dayEvent del día especial
            switch (currentEvent.DayEvents)
            {
                case DayEvents.MysteryVisitor:
                    // Activar evento de visitante misterioso
                    break;
                case DayEvents.MysterySounds:
                    // Activar evento de sonidos misteriosos
                    break;
                case DayEvents.HorrorDay:
                    // Activar todos los eventos paranormales del juego
                    break;
            }

            todayIsSpecial = true;
        }
        else
        {
            todayIsSpecial = false;
        }


    }

    [Button]
    public void LookDay()
    {
        Debug.Log("Día actual: " + monthDays.Peek());

    }
    [Button]
    public void LookText()
    {
        Debug.Log("Texto actual: " + specialDays.Peek().EventText);

    }

    [Button]
    public void Clear()
    {
        monthDays.Clear();
    }
    [Button]
    public void Count()
    {
        Debug.Log("Días restantes: " + monthDays.Count);
    }
    [Button]
    public void AddSpecialDay(int day, string eventText, DayEvents dayEvents)
    {
        specialDays.Enqueue(new SpecialDay()
        {
            Day = day,
            EventText = eventText,
            DayEvents = dayEvents
        });
    }
    #region Getters
    public MyQueue<int> MonthDays => monthDays;
    public MyQueue<Months> YearMonths => Months;
    public MyQueue<Days> WeekDays => weekDays;

    public int DaysInMonth => daysInMonth;
    public MyQueue<SpecialDay> SpecialDays => specialDays;

    public SpecialDay CurrentEvent => currentEvent;
    public bool TodayIsSpecial => todayIsSpecial;

    #endregion
}
