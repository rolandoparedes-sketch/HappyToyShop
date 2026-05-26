using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerEconomicSystem : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    [Button]
    public void BuyDozen(int ShelfID)
    {
        var factory = GameManager.instance.factorySystem;
        var warehouse = GameManager.instance.warehouseSystem;


        if (TryToBuyDozen(ShelfID))
        {


            factory.TakeToy(ShelfID);

            int tempAmount = warehouse.Shelfs[ShelfID].CurrentAmount;



            if (tempAmount + 12 > warehouse.Shelfs[ShelfID].MaxCapacity)
            {

                Debug.Log("No puedes sobrepasar la capacidad maxima, mejora la tienda antes de comprar más");
                return;
            }
            else
            {

                Debug.Log("Compra Exitosa: La docena de " + factory.TakeToy(ShelfID).EntityName + " llegarán en la noche");

                warehouse.Shelfs[ShelfID].CurrentAmount += 12;
            }
        }

    }
    [Button]
    public void BuyHalfDozen(int ShelfID)
    {

        var factory = GameManager.instance.factorySystem;
        var warehouse = GameManager.instance.warehouseSystem;

        if (TryToBuyDozen(ShelfID))
        {

            factory.TakeToy(ShelfID);

            int tempAmount = warehouse.Shelfs[ShelfID].CurrentAmount;



            if (tempAmount + 6 > warehouse.Shelfs[ShelfID].MaxCapacity)
            {

                Debug.Log("No puedes sobrepasar la capacidad maxima, mejora la tienda antes de comprar más");
                return;
            }
            else
            {

                Debug.Log("Compra Exitosa: La media docena de " + factory.TakeToy(ShelfID).EntityName + " llegarán en la noche");

                warehouse.Shelfs[ShelfID].CurrentAmount += 6;
            }
        }
    }
    public bool TryToBuyDozen(int ShelfID)
    {
        var factory = GameManager.instance.factorySystem;

        ToyData data = factory.TakeToy(ShelfID);
        if (GameManager.instance.CurrentMoney < data.DozenCost)
        {
            Debug.Log("IntentandoComprar una docena de " + data.EntityName);
            Debug.Log("Dinero Insuficiente, Necesitas: "+ (data.DozenCost - GameManager.instance.CurrentMoney) + " para comprar");
            return false;
        }
        else
        {

            GameManager.instance.CurrentMoney -= data.DozenCost;
            return true;

        }

    }
    public bool TryToBuyHalfDozen(int ShelfID)
    {
        var factory = GameManager.instance.factorySystem;

        ToyData data = factory.TakeToy(ShelfID);
        if (GameManager.instance.CurrentMoney < data.HalfDozenCost)
        {
            
            Debug.Log("IntentandoComprar media docena de " + data.EntityName);
            Debug.Log("Dinero Insuficiente, Necesitas: "+ (data.HalfDozenCost - GameManager.instance.CurrentMoney)+ " para comprar");
            return false;
        }
        else
        {

            GameManager.instance.CurrentMoney -= data.HalfDozenCost;
            return true;

        }

    }
}
