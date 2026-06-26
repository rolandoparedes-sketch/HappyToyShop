
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class WarehouseSystem : MonoBehaviour
{
    public List<ShelfStorage> Shelfs;




    public List<ShelfUI> ShelfUI;

    private void Awake()
    {
    }
    
    void Start()
    {

       Invoke(nameof(ApplyDataToEachShelf), 1f);



    }

    private void OnEnable()
    {
        GameManager2D.instance.UIManager.OnRestock += ApplyDataUIShelf;
    }
    private void OnDisable()
    {

       GameManager2D.instance.UIManager.OnRestock -= ApplyDataUIShelf;

    }

    public void TestOrderByAcending()
    {

        Debug.Log("Ordenando Shelfs por CurrentAmount");
        Shelfs = Shelfs.OrderBy(Shelfs => Shelfs.CurrentAmount).ToList();
    }

    void Update()
    {

    }
    public bool CheckIfThereIsEnoughSpace(int ShelfID, int Amount)
    {
        if (Shelfs[ShelfID].CurrentAmount + Amount > Shelfs[ShelfID].MaxCapacity)
        {
            Debug.Log("You can't exceed the maximum capacity, upgrade the store before buying more, Shelf ID: " + ShelfID);

            Debug.Log("CurrentAmount: " + Shelfs[ShelfID].CurrentAmount);

            Debug.Log("CurrentID: " + Shelfs[ShelfID].ShelfID);
            Debug.Log("Amount to buy: " + Amount);
            Debug.Log(Shelfs[ShelfID].CurrentAmount + Amount);

            Debug.Log("MaxCapacity: " + Shelfs[ShelfID].MaxCapacity);
            return false;
        }
        else
        {
            return true;
        }
    }
     public void AddToyToShelf(int ShelfID, int Amount)
     {

         Shelfs[ShelfID].CurrentAmount += Amount;

         //GameManager2D.instance.DataGame.currentAmountInShelfs[ShelfID] += Amount;


         Debug.Log("You added " + Amount + " " + Shelfs[ShelfID].data.EntityName + " to the warehouse, now you have " + Shelfs[ShelfID].CurrentAmount);


     }
    [Button]
    public void ApplyDataToEachShelf()
    {

        Debug.Log("ApplyDataToEachShelf ejecutado");
        var data = GameManager2D.instance.DataGame;
        for (int i = 0; i < Shelfs.Count; i++)
        {
            if (Shelfs[i] == null)
            {
                Debug.LogWarning("Falto asignar un alamacen en la posición número: " + i);
            }

            Shelfs[i].data = GameManager2D.instance.FactorySystem.toyDataBase.GetToy(i);

            Shelfs[i].ShelfID = Shelfs[i].data.ID;

            Shelfs[i].CurrentAmount = GameManager2D.instance.DataGame.currentAmountInShelfs[i];

            Shelfs[i].MaxCapacity = data.MaxCapacityShelf;


            if (Shelfs[i].CurrentAmount > data.MaxCapacityShelf)
            {
                Shelfs[i].CurrentAmount = data.MaxCapacityShelf;
                Debug.LogWarning("El almacen " + i + " excedio la capacidad máxima.");
            }

        }

    }
    [Button]
    public void ApplyDataUIShelf()
    {
        Debug.Log("Data aplicada");

        var orderedShelfs = Shelfs.OrderBy(x => x.CurrentAmount).ToList();

        for (int i = 0; i < orderedShelfs.Count; i++)
        {
            ShelfStorage shelf = orderedShelfs[i];

            ShelfUI[i].toyId = shelf.ShelfID;

            ShelfUI[i].nameText.text = shelf.data.EntityName;

            ShelfUI[i].stock = shelf.CurrentAmount;
            ShelfUI[i].stockText.text = $"Stock: {shelf.CurrentAmount}";

            ShelfUI[i].price = shelf.data.SalePrice;

            ShelfUI[i].Icon.sprite = shelf.data.Icon;
        }
    }

}
