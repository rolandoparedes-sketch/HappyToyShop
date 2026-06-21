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
    public Image ToyCustomer;
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

        ShelfStorage.OnTakeToy += ChangeUIinHand;

        GameManager2D.instance.CustomerQueue.OnCustomerReceived += ChangePedidoUI;
    }
    private void ChangePedidoUI(NPCCustomer customer)
    {
        var customerWaiting = GameManager2D.instance.CustomerQueue.CustomerWaiting;

        if (customerWaiting.Count == 0)
        {

            ToyCustomer.gameObject.SetActive(false);
            return;
        }


        ToyCustomer.gameObject.SetActive(true);


        int iDPedido = customer.IdPedido;


        ToyData toydata = GameManager2D.instance.FactorySystem.TakeToy(iDPedido);

        ToyCustomer.sprite = toydata.Icon;

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
        var dayManager = GameManager2D.instance.DayManager;

        if (dayManager.TodayIsSpecial)
        {
            Day.text = dayManager.CurrentEvent.EventText;
            return;
        }

        Day.text = dayManager.CurrentWeekDay + ", " + dayManager.CurrentMonth + " " + dayManager.CurrentDay;
    }
}
