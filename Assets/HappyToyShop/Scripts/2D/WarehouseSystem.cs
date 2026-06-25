
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class WarehouseSystem : MonoBehaviour
{
    public List<ShelfStorage> Shelfs;


    public List<ShelfUI> ShelfUI;

    private void Awake()
    {
    }
    
    void Start()
    {

        ApplyDataToEachShelf();

    }

    private void OnEnable()
    {
        GameManager2D.instance.DayManager.OnDayComplete += TestOrderByAcending;
    }
    private void OnDisable()
    {

        GameManager2D.instance.DayManager.OnDayComplete -= TestOrderByAcending;
    }

    public void TestOrderByAcending()
    {
        Shelfs = Shelfs.OrderBy(Shelfs => Shelfs.CurrentAmount).ToList();
    }

    void Update()
    {

    }
    public bool CheckIfThereIsEnoughSpace(int ShelfID, int Amount)
    {
        if (Shelfs[ShelfID].CurrentAmount + Amount > Shelfs[ShelfID].MaxCapacity)
        {
            Debug.Log("You can't exceed the maximum capacity, upgrade the store before buying more");
            return false;
        }
        else
            return true;
    }
    public void AddToyToShelf(int ShelfID, int Amount)
    {

        Shelfs[ShelfID].CurrentAmount += Amount;

        GameManager2D.instance.DataGame.currentAmountInShelfs[ShelfID] += Amount;


        Debug.Log("You added " + Amount + " " + Shelfs[ShelfID].data.EntityName + " to the warehouse, now you have " + Shelfs[ShelfID].CurrentAmount);


    }

    [Button]
    public void ApplyDataToEachShelf()
    {


        for (int i = 0; i < Shelfs.Count; i++)
        {
            if (Shelfs[i] == null)
            {
                Debug.LogWarning("Falto asignar un alamacen en la posición número: " + i);
            }

            Shelfs[i].data = GameManager2D.instance.FactorySystem.toyDataBase.GetToy(i);
        }
        for (int i = 0; i < Shelfs.Count; i++)
        {

            Shelfs[i].ShelfID = Shelfs[i].data.ID;

            Shelfs[i].CurrentAmount = GameManager2D.instance.DataGame.currentAmountInShelfs[i];
        }

    }

    public void ApplyDataUIShelf()
    {
        for (int i = 0; i < Shelfs.Count; i++)
        {
            if (Shelfs[i] == null)
            {
                Debug.LogWarning("Falto asignar un alamacen en la posición número: " + i);
            }

            ShelfUI[i].toyId = Shelfs[i].data.ID;

            ShelfUI[i].name = Shelfs[i].data.EntityName;

            ShelfUI[i].stock = Shelfs[i].CurrentAmount;

            ShelfUI[i].stockText.text = Shelfs[i].CurrentAmount.ToString();

            ShelfUI[i].price = Shelfs[i].data.SalePrice;

            ShelfUI[i].Icon.GetComponent<SpriteRenderer>().sprite = Shelfs[i].data.Icon;
        }

    }
    
    
}
