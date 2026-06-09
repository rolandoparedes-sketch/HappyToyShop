using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager2D : MonoBehaviour
{
    public static GameManager2D instance;
    [FoldoutGroup("References")]
    [SerializeField] private MoneySystem moneySystem;
    [FoldoutGroup("References")]
    [SerializeField] private DayManager dayManager;
    [FoldoutGroup("References")]
    [SerializeField] private FactorySystem factorySystem;
    [FoldoutGroup("References")]
    [SerializeField] private WarehouseSystem warehouseSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public MoneySystem MoneySystem => moneySystem;
    public DayManager DayManager => dayManager;
    public FactorySystem FactorySystem => factorySystem;
    public WarehouseSystem WarehouseSystem => warehouseSystem;
}
