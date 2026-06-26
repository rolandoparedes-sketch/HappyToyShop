using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelfUI : MonoBehaviour
{

    public int toyId;
    public int stock;
    public float price;
    

    public int maxCanBuy;

    public int buyAmount;


    public float timeToHideText = 2;
    Coroutine maxCoroutine;
    public TextMeshProUGUI maxFeedbackText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stockText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI costText;

    public Image Icon;

    public Button Increase;
    public Button Decrease;


    public void Start()
    {
        Refresh();
    }
   
    public void IncreaseAmount()
    {
        if ( buyAmount >= maxCanBuy)
        {

            ShowMaxMessage();
            Debug.Log("Max capacity reached");
            return;
        }


        buyAmount++;
        Refresh();
    }
    public void DecreaseAmount()
    {
        if (buyAmount <= 0)
            return;

        buyAmount--;
        Refresh();
    }

    void Refresh()
    {
        ShelfStorage shelf = GameManager2D.instance.WarehouseSystem.Shelfs[toyId];

        int maxCapacity = shelf.MaxCapacity;

        maxCanBuy = maxCapacity - stock;

        amountText.text = buyAmount.ToString();
        costText.text = "-$" + (buyAmount * price);

        stockText.text = stock.ToString();
    }
    public void Buy()
    {


        

        GameManager2D.instance.MoneySystem.Buy(toyId, buyAmount);

        stock += buyAmount;

        buyAmount = 0;
        Refresh();
        
        var data = GameManager2D.instance.DataGame;

       
    }
    private void ShowMaxMessage()
    {
        maxFeedbackText.gameObject.SetActive(true);

        if (maxCoroutine != null)
            StopCoroutine(maxCoroutine);

        maxCoroutine = StartCoroutine(HideMaxText());
    }
    private IEnumerator HideMaxText()
    {
        yield return new WaitForSeconds(timeToHideText);
        maxFeedbackText.gameObject.SetActive(false);
        maxCoroutine = null;
    }
    private void UpdateButtonsUI()
    {
        Increase.interactable = buyAmount < maxCanBuy;
        Decrease.interactable = buyAmount > 0;
    }

}
