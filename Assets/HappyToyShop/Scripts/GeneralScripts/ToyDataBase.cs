using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToyDataBase", menuName = "Happy Toy Shop/ToyDataBase")]

public class ToyDataBase : SerializedScriptableObject
{
    public Dictionary<int, ToyData> toyDataBase = new();
   
    public ToyData GetToy(int toyID)
    {
        if (toyDataBase.TryGetValue(toyID, out ToyData toy))
        {
            return toy;
        }
        else
        {
            throw new System.Exception("No se encontró un juguete con ese ID");
        }
    }
}
