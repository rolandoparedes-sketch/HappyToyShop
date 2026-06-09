
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShelfStorage
{
    public int ShelfID;
    public ToyData data;
    public int CurrentAmount;
    public int MaxCapacity = 18;

    
    

}
public class WarehouseSystem : MonoBehaviour
{
    public List<ShelfStorage> Shelfs;

    private void Awake()
    {
    }
    void Start()
    {

        AplicarDataEnCadaAlmacen();
      
    }
    

    void Update()
    {

    }
    [Button]
    public bool AddToyToShelf(int ShelfID, int Amount)
    {
        if (Shelfs[ShelfID].CurrentAmount + Amount > Shelfs[ShelfID].MaxCapacity)
        {
            Debug.Log("You can't exceed the maximum capacity, upgrade the store before buying more");
            return false;
        }
        else
        {
            Shelfs[ShelfID].CurrentAmount += Amount;
            Debug.Log("You added " + Amount + " " + Shelfs[ShelfID].data.EntityName + " to the warehouse, now you have " + Shelfs[ShelfID].CurrentAmount);
            return true;
        }
    }


    public void AplicarDataEnCadaAlmacen()
    {

        Shelfs.SetLength(GameManager2D.instance.FactorySystem.toyDataBase.toyDataBase.Count);

        for (int i = 0; i < Shelfs.Count; i++)
        {
            if (Shelfs[i] == null)
            {
                Shelfs[i] = new ShelfStorage();
            }

            Shelfs[i].data = GameManager2D.instance.FactorySystem.toyDataBase.GetToy(i);
        }
        for (int i = 0; i < Shelfs.Count; i++)
        {

            Shelfs[i].ShelfID = Shelfs[i].data.ID;
        }

    }
    
    
}
