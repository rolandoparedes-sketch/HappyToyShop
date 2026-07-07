using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
public class WarehouseSystem3D : MonoBehaviour
{
    public List<ShelfStorage3D> shelfStorage3D = new();


    void Start()
    {
        SetMaximCapacity();
    }

    void Update()
    {

    }
    [Button]
    public void Order()
    {
        shelfStorage3D = shelfStorage3D.OrderBy(shelf3D => shelf3D.toyData.ID).ToList();
    }

    public void SetMaximCapacity()
    {

        var DataGame = GameManager3D.instance.DataGame;
        for (int i = 0; i < shelfStorage3D.Count; i++)
        {
            shelfStorage3D[i].maxCapacity = DataGame.MaxCapacityShelf - DataGame.currentAmountInShelfs[i];
        }
    }
}
