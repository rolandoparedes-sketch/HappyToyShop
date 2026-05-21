using Sirenix.OdinInspector;
using UnityEngine;

public class FactorySystem : MonoBehaviour
{

    [SerializeField] private ToyDataBase toyDataBase;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    [Button]
    public ToyData TakeToy(int toyID)
    {
        ToyData toy = toyDataBase.GetToy(toyID);
        return toy;
    }
    public void BuyDozen(int toyID)
    {
        ToyData toy = TakeToy(toyID);

        

    }
    public void BuyHalfDozen(int toyID)
    {
        
    }
}
