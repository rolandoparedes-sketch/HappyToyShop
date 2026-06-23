using Sirenix.OdinInspector;
using UnityEngine;

public class FactorySystem : MonoBehaviour
{

    public ToyDataBase toyDataBase;
    private void Awake()
    {
        GameManager2D.instance.SetFactorySytem(this);
    }
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
}
