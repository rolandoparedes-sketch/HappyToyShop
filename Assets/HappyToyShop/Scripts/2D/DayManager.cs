using HappyToyShop.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
   // [FoldoutGroup("GameSettings")]
    //[SerializeField] private MyQueue<int> monthDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private int daysInMonth = 30;
  //  [FoldoutGroup("GameSettings")]
   // [SerializeField] private MyQueue<Months> months = new();
  //  [FoldoutGroup("GameSettings")]
    //[SerializeField] private MyQueue<Days> weekDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private MyQueue<SpecialDay> specialDays = new();
    [FoldoutGroup("GameSettings")]
    [SerializeField] private bool todayIsSpecial = false;
    [SerializeField] private SpecialDay currentEvent;

    public Action OnNextDay;
    public Action OnWeekComplete;

    public Action OnDayComplete;

    private int currentDay;
    public struct SpecialDay
    {
        public int Day;
        public string EventText;
        public DayEvents DayEvents;
    }



    void Start()
    {
        InitializeDaySystem();
        CustomerManager.OnCustomerLeft += CheckLastCustomer;

    }
    [Button]
    public void PassDay()
    {
        ScenesManager.instance.ChangeMode3D();
        UIManager.instance.PanelStock.SetActive(false);
    }


    private void CheckLastCustomer(NPCCustomer customer)
    {
        var customerSpawner = GameManager2D.instance.CustomerManager.CustomerSpawner;

        if (customerSpawner.CustomerSpawnedToday == customerSpawner.CustomerPerDay && customerSpawner.CurrentCustomers == 0)
        {

            OnDayComplete?.Invoke();
        }
    }

    public void InitializeDaySystem()
    {
        InitializeCalendar();
        FixSavedDay();
        FixSavedWeek();
        FixSavedMonth();
    }
    public void InitializeCalendar()
    {
        //monthDays.Clear();
      //  weekDays.Clear();
      //  months.Clear();
        specialDays.Clear();

       // InitializeMonthDays();
       // InitializeWeekDays();
       // InitializeMonths();
        InitializeSpecialDays();

    }
   /* private void InitializeMonthDays()
    {
        for (int i = 1; i <= daysInMonth; i++)
        {
            monthDays.Enqueue(i);
        }
    }*/
   /* private void InitializeWeekDays()
    {
        weekDays.Enqueue(Days.Monday);
        weekDays.Enqueue(Days.Tuesday);
        weekDays.Enqueue(Days.Wednesday);
        weekDays.Enqueue(Days.Thursday);
        weekDays.Enqueue(Days.Friday);
        weekDays.Enqueue(Days.Saturday);
        weekDays.Enqueue(Days.Sunday);
    }
    private void InitializeMonths()
    {
        months.Enqueue(Months.January);
        months.Enqueue(Months.February);
        months.Enqueue(Months.March);
        months.Enqueue(Months.April);
        months.Enqueue(Months.May);
        months.Enqueue(Months.June);
        months.Enqueue(Months.July);
        months.Enqueue(Months.August);
        months.Enqueue(Months.September);
        months.Enqueue(Months.October);
        months.Enqueue(Months.November);
        months.Enqueue(Months.December);
    }*/
    private void InitializeSpecialDays()
    {
        
        AddSpecialDay(2, "Hay algo afuera...", DayEvents.MysteryVisitor);
        AddSpecialDay(4, "Escuchaste eso?...", DayEvents.MysterySounds);
        AddSpecialDay(7, "Alguien entró al local...", DayEvents.HorrorDay);
    }


    #region Method Save and load
    private void FixSavedDay()
    {

        if (GameManager2D.instance.DataGame.day<0 || GameManager2D.instance.DataGame.weekDayIndex > daysInMonth)
        {
            Debug.Log("El número se de día se sale de los parametros establecidos");
        }
    }
    private void FixSavedWeek()
    {
        if (GameManager2D.instance.DataGame.weekDayIndex < 0 || GameManager2D.instance.DataGame.weekDayIndex > (int)Days.Sunday)
        {
            Debug.Log("El index del día de semana se sale de los parametros establecidos");
        }
      
    }
    private void FixSavedMonth()
    {
        if (GameManager2D.instance.DataGame.monthIndex < 0 || GameManager2D.instance.DataGame.monthIndex > (int)Months.December)
        {
            Debug.Log("El index del mes se sale de los parametros establecidos");
        }
    }
    /* private void SaveCurrentDay()
     {
         GameManager2D.instance.DataGame.day = monthDays.Peek();
     }
     private void SetCurrentDay(int day)
     {
         if (day < 1 || day > daysInMonth)
         {
             Debug.LogWarning("El día del mes se sale de los parametros establecidos: (día " + day +" )");
             SaveCurrentDay();
             return;
         }

         while (monthDays.Peek() != day)
         {
             RotateDay();
         }
     }
     private void RotateDay()
     {
         int currentDay = monthDays.Dequeue();
         monthDays.Enqueue(currentDay);
     }*/
    #endregion

    [Button]
    public void NextDay()
    {
        //int finishedDay = AdvanceToNextDay();

        //SaveCurrentDay();

        AdvanceNextDay();
        AdvanceWeekDay(); 
        CheckSpecialDay(); 

        OnNextDay?.Invoke();
    }
    private void AdvanceNextDay()
    {
        IncrementDay();
        ValidateDaysInMonthCycle();
    }

    private void IncrementDay()
    {
        GameManager2D.instance.DataGame.day++;
    }

    private void ValidateDaysInMonthCycle()
    {
        if(GameManager2D.instance.DataGame.day > daysInMonth)
        {

            ResetDay();
            AdvanceMonth();
        }
    }

    private void ResetDay()
    {
        GameManager2D.instance.DataGame.day = 1;
    }

    private void AdvanceWeekDay()
    {
        IncrementWeekIndex();
        ValidateWeekCycle();
    }
    private void IncrementWeekIndex()
    {
        GameManager2D.instance.DataGame.weekDayIndex++;
    }
    private void ValidateWeekCycle()
    {
        if (GameManager2D.instance.DataGame.weekDayIndex > (int)Days.Sunday)
        {
            ResetWeek();
            OnWeekComplete?.Invoke();
        }
    }
    private void ResetWeek()
    {
        GameManager2D.instance.DataGame.weekDayIndex = 0;
    }
    private void AdvanceMonth()
    {
        IncrementMonthIndex();
        ValidateMonthCycle();
    }
    private void ValidateMonthCycle()
    {
        if (GameManager2D.instance.DataGame.monthIndex > (int)Months.December)
        {
            ResetMonth();
        }
    }
    private void ResetMonth()
    {
        GameManager2D.instance.DataGame.monthIndex = 0;
    }
    private void IncrementMonthIndex()
    {
        GameManager2D.instance.DataGame.monthIndex++;
    }
    /* private int AdvanceToNextDay()
    {
        int finishedDay = monthDays.Dequeue();

        monthDays.Enqueue(finishedDay);

        Debug.Log($"Day {finishedDay} completed");

        return finishedDay;
    }
   private void AdvanceWeekDay()
    {
        Days weekDay = weekDays.Dequeue();

        weekDays.Enqueue(weekDay);
    }
    private void CheckMonthCompleted(int finishedDay)
    {
        if (finishedDay == daysInMonth)
        {
            AdvanceMonth();
        }
    }
    private void AdvanceMonth()
    {
        Months month = months.Dequeue();

        months.Enqueue(month);

        Debug.Log("Month completed");
    }*/
    private void CheckSpecialDay()
    {
        if (specialDays.Count > 0 && specialDays.Peek().Day == CurrentDay)
        {
            currentEvent = specialDays.Dequeue();

            HandleSpecialEvent(currentEvent.DayEvents);

            todayIsSpecial = true;
        }
        else
        {
            todayIsSpecial = false;
        }
    }
    private void HandleSpecialEvent(DayEvents dayEvent)
    {
        switch (dayEvent)
        {
            case DayEvents.MysteryVisitor:
                break;

            case DayEvents.MysterySounds:
                break;

            case DayEvents.HorrorDay:
                break;
        }
    }
    [Button]
    public int LookSpecialDay()
    {
        //Debug.Log("Current Day: " + monthDays.Peek());
        return specialDays.Peek().Day;

    }
    [Button]
    public void LookText()
    {
        Debug.Log("Current Text: " + specialDays.Peek().EventText);

    }

    [Button]
    public void Clear()
    {
        specialDays.Clear();
    }
    [Button]
    public void Count()
    {
        Debug.Log(specialDays.Count);
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
   // public MyQueue<int> MonthDays => monthDays;
   // public MyQueue<Months> YearMonths => months;
   // public MyQueue<Days> WeekDays => weekDays;

    public int DaysInMonth => daysInMonth;
    public MyQueue<SpecialDay> SpecialDays => specialDays;

    public SpecialDay CurrentEvent => currentEvent;
    public bool TodayIsSpecial => todayIsSpecial;
    public int CurrentDay => GameManager2D.instance.DataGame.day;
    public Days CurrentWeekDay => (Days)GameManager2D.instance.DataGame.weekDayIndex;
    public Months CurrentMonth => (Months)GameManager2D.instance.DataGame.monthIndex;
    #endregion
}
