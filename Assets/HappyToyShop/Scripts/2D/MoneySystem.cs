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
    private void Awake()
    {
        GameManager2D.instance.SetMoneySystem(this);
    }
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
        if (Amount <= 0)
        {
            Debug.Log("You need to buy at least 1 toy");
            return;
        }

        var factory = GameManager2D.instance.FactorySystem;
        var warehouse = GameManager2D.instance.WarehouseSystem;


        if (CheckIfThereIsEnoughMoney(ShelfID, Amount))
        {


            factory.TakeToy(ShelfID);

            GameManager2D.instance.WarehouseSystem.AddToyToShelf(ShelfID, Amount);
        }

    }
    public bool CheckIfThereIsEnoughMoney(int ShelfID, int Amount)
    {
        var factory = GameManager2D.instance.FactorySystem;

        ToyData data = factory.TakeToy(ShelfID);
       

        if (CurrentMoney < data.FactoryCost)
        {
            Debug.Log("Try to buy " + Amount + " " + data.EntityName + " : " + data.Description);
            Debug.Log("Insufficient funds, you need: " + (data.FactoryCost - CurrentMoney) + " to buy");
            return false;
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
