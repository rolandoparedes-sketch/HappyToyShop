
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;



public class WarehouseSystem : MonoBehaviour
{
    public List<ShelfStorage> Shelfs;

    private void Awake()
    {
        GameManager2D.instance.SetWarehouseSystem(this);
    }
    void Start()
    {

        ApplyDataToEachShelf();

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
    
    
}
