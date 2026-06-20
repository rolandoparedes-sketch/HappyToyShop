using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI Day;

    public Image ToyInHand;
    private void Awake()
    {


     
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager2D.instance.DayManager.OnNextDay += UpdateDay;
        ShelfStorage.OnTakeToy += ChangeUIinHand;
    }

    private void ChangeUIinHand()
    {
        if(PlayerController2D.instance.playerMechanics.ToyData == null)
        {

            //ToyInHand.sprite = null;
            ToyInHand.gameObject.SetActive(false);
            return;
        }

        ToyInHand.gameObject.SetActive(true);
        ToyInHand.sprite = PlayerController2D.instance.playerMechanics.ToyData.Icon;
        
    }

    private void UpdateDay()
    {
        Day.text = "Día " + GameManager2D.instance.DayManager.MonthDays.Peek();
      
    }

    void Start()
    {
        ChangeMessage();


    }

    void Update()
    {
        
    }
    [Button]
    public void ChangeMessage()
    {

        var weekDays = GameManager2D.instance.DayManager.WeekDays;
        var yearMonths = GameManager2D.instance.DayManager.YearMonths;
        var monthDays = GameManager2D.instance.DayManager.MonthDays;

        Day.text = weekDays.Peek() + ", " + yearMonths.Peek() + " " + monthDays.Peek();

        var dayManager = GameManager2D.instance.DayManager;

        if (dayManager.TodayIsSpecial)
        {
            Day.text = dayManager.CurrentEvent.EventText;
            return;
        }

        Day.text = dayManager.WeekDays.Peek() + ", " + dayManager.YearMonths.Peek() + " " + dayManager.MonthDays.Peek();
    }
}
