using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Day;

    public Image ToyInHand;
    public Image ToyCustomer;


    public Image BarraPacking;
    public Image BarraContorno;
    public Image GiftBox;


    public GameObject PanelStock;

    public List<Image> ImagesShelfs;

    public TextMeshProUGUI PlayerDialgue;



    public Image CurrentCustomer;
    public TextMeshProUGUI CustomerDialogue;


    public TextMeshProUGUI Money;
    public TextMeshProUGUI DailyGoal;





    private void Awake()
    {


     





    }
    private void OnEnable()
    {

        ShelfStorage.OnTakeToy += ChangeUIinHand;

        GameManager2D.instance.CustomerManager.CustomerQueue.OnCustomerReceived += ChangePedidoUI;

        GameManager2D.instance.DayManager.OnDayComplete += RestockToys;

        GameManager2D.instance.FurnitureManager.AttentionTable.OnSell += ChangeMoneyUI;

        NPCCustomer.OnReceived += DialogueCustomerEnter;

       // GameManager2D.instance.FurnitureManager.AttentionTable.OnSell += DialogueCustomerExit;


        
    }

    private void DialogueCustomerEnter()
    {
        CurrentCustomer.gameObject.SetActive(true);
        CustomerDialogue.gameObject.SetActive(true);

        NPCCustomer currentCustomer = GameManager2D.instance.CustomerManager.CustomerQueue.CustomerWaiting.Peek();



        CurrentCustomer.sprite = currentCustomer.GetComponent<SpriteRenderer>().sprite;

        CustomerDialogue.text = "Hi :D";
        


    }
    public void DialogueCustomerExit()
    {

        NPCCustomer currentCustomer = GameManager2D.instance.CustomerManager.CustomerQueue.CustomerWaiting.Peek();



        CurrentCustomer.sprite = currentCustomer.GetComponent<SpriteRenderer>().sprite;

        CustomerDialogue.text = currentCustomer.Dialogue;
    }

    private void OnDisable()
    {
        
    }
    public void ChangeDialoguePlayer(string newText)
    {
        
        PlayerDialgue.text = newText;
    }
    private void RestockToys()
    {
        PlayerController2D.instance.playerMovement.GetComponent<PlayerMovement2D>().enabled = false;
        PanelStock.SetActive(true);
    }

    private void ChangePedidoUI(NPCCustomer customer)
    {
        var customerWaiting = GameManager2D.instance.CustomerManager.CustomerQueue.CustomerWaiting;

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
        InitializerUIGame();

    }
    public void InitializerUIGame()
    {
        DailyGoal.text = GameManager2D.instance.MoneySystem.DailySalesGoal.ToString();


        Money.text = GameManager2D.instance.MoneySystem.CurrentMoney.ToString();


    }
    public void ChangeMoneyUI()
    {
        Money.text = GameManager2D.instance.MoneySystem.CurrentMoney.ToString();
    }

    void Update()
    {
        UICargaPacking();
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

    public void UICargaPacking()
    {
        var PackingTable = GameManager2D.instance.FurnitureManager.PackingTable;

        BarraContorno.gameObject.SetActive(PackingTable.IsPacking);
        GiftBox.gameObject.SetActive(PackingTable.IsPacking);

        float percentage = PackingTable.Progress/PackingTable.TimeToPacking;

        BarraPacking.fillAmount = percentage;

    }
}
