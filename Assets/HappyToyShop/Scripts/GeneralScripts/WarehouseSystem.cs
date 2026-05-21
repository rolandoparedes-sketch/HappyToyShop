
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

        Shelfs.SetLength(GameManager.instance.factorySystem.toyDataBase.toyDataBase.Count);
    }
    void Start()
    {

        
        AplicarDataEnCadaAlmacen();
        AplicarID();
    }
    

    void Update()
    {

    }
    [Button]
    public void AplicarID()
    {

        for (int i = 0; i < Shelfs.Count; i++)
        {

            Shelfs[i].ShelfID = Shelfs[i].data.ID;
        }
    }
    [Button]
    public void AplicarDataEnCadaAlmacen()
    {
        for (int i = 0; i < Shelfs.Count; i++)
        {
            Debug.Log(i);
            Shelfs[i].data = GameManager.instance.factorySystem.toyDataBase.GetToy(i);
        }
    }
}
