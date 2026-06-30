using Sirenix.OdinInspector;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    [FoldoutGroup("MoneySystem")]
    public float CurrentMoney;
    [FoldoutGroup("MoneySystem")]
    public float DailySalesGoal;
    [FoldoutGroup("MoneySystem")]
    public float WeekSalesGoal;
    void Start()
    {
        LoadData();
    }

    void Update()
    {

    }
    public void LoadData()
    {
        CurrentMoney = GameManager2D.instance.DataGame.money;
    }
    [Button]
    public void Buy(int ShelfID, int Amount)
    {
        if (Amount <= 0) // 1
        {
            Debug.Log("You need to buy at least 1 toy"); //1
            return; //+1
        }

        var factory = GameManager2D.instance.FactorySystem; //1
        var warehouse = GameManager2D.instance.WarehouseSystem; // 1


        if (CheckIfThereIsEnoughMoney(ShelfID, Amount)) //+1
        {


            factory.TakeToy(ShelfID); // +1

            GameManager2D.instance.WarehouseSystem.AddToyToShelf(ShelfID, Amount); //+1
        }

    }
    public bool CheckIfThereIsEnoughMoney(int ShelfID, int Amount)
    {
        var factory = GameManager2D.instance.FactorySystem; //1

        ToyData data = factory.TakeToy(ShelfID); //1
       

        if (CurrentMoney < data.FactoryCost) //1
        {
            Debug.Log("Try to buy " + Amount + " " + data.EntityName + " : " + data.Description); //1
            Debug.Log("Insufficient funds, you need: " + (data.FactoryCost - CurrentMoney) + " to buy");//1
            return false; //1
        }
        else
        {
            if (GameManager2D.instance.WarehouseSystem.CheckIfThereIsEnoughSpace(ShelfID, Amount))
            {

                CurrentMoney -= data.FactoryCost * Amount;

                Debug.Log("Purchased " + Amount + " " + data.EntityName + " for " + (data.FactoryCost * Amount) + " dollars");

                return true;
            }
            else
            {

                return false;
            }

        }



    }
}
