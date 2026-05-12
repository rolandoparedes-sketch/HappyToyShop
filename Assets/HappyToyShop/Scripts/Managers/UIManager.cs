using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI Day;
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
       
        else if (!GameManager.instance.TurnDay && GameManager.instance.SpecialDay)
        {
            if (GameManager.instance.TextEvents.Count <= 0)
            {
                return;
            }

            Day.text = GameManager.instance.TextEvents.Peek();
        }
        else
        {
            Day.gameObject.SetActive(false);
        }


    }

    void Update()
    {
        
    }
}
