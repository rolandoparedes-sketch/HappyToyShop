using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Day;
    private void Awake()
    {
        GameManager.instance.OnNextDay += UpdateDay;
    }

    private void UpdateDay()
    {
        Day.text = "Día " + GameManager.instance.Day.Peek();
      
    }

    void Start()
    {
        if(GameManager.instance.TurnDay)
        Day.text = "Día " + GameManager.instance.Day.Peek();
        else
        {
            Day.gameObject.SetActive(false);
        }

        if(!GameManager.instance.TurnDay && GameManager.instance.Day.Peek() ==2)
        {
                Day.text = "Hay algo afuera...";
        }



    }

    void Update()
    {
        
    }
}
