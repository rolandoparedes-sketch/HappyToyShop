using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelfUI : MonoBehaviour
{

    public int toyId;
    public int stock;
    public float price;

    public int buyAmount;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stockText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI costText;

    public Image Icon;

    public void Start()
    {
        Refresh();
    }
   
    public void IncreaseAmount()
    {
        ShelfStorage shelf = GameManager2D.instance.WarehouseSystem.Shelfs[toyId];

        int maxcapacity = shelf.MaxCapacity;

        if (stock + buyAmount >= maxcapacity)
        {

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

        amountText.text = buyAmount.ToString();
        costText.text = "-$" + (buyAmount * price);
    }
    public void Buy()
    {



        GameManager2D.instance.MoneySystem.Buy(toyId, buyAmount);

        var data = GameManager2D.instance.DataGame;

       
    }
}
