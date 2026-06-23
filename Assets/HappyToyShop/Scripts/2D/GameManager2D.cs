using Sirenix.OdinInspector;
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
    [FoldoutGroup("References")]
    [SerializeField] private CustomerManager customerManager;
    [FoldoutGroup("References")]
    [SerializeField] private DataGame dataGame;
    [FoldoutGroup("References")]
    [SerializeField] private SoundManager soundManager;
    [FoldoutGroup("References")]
    [SerializeField] private CameraSystem cameraSystem;
    [FoldoutGroup("References")]
    [SerializeField] private FurnitureManager furnitureManager;
    private void Awake()
    {
        Debug.Log(gameObject.name);
        if (instance == null)
        {
          //  instance = this;
          //  Debug.Log("Instance Created");
        }
        else
        {
            Debug.Log("F");
            Destroy(gameObject);
            
        }
    }
    public void SetMoneySystem(MoneySystem moneySystem)
    { 
        this.moneySystem = moneySystem; 
    } 
    public void SetDayManager(DayManager dayManager)
    {
        this.dayManager = dayManager;
    }

    public void SetFactorySytem(FactorySystem factorySystem)
    {
        this.factorySystem = factorySystem;
    }

    public void SetWarehouseSystem(WarehouseSystem warehouseSystem)
    {
        this.warehouseSystem = warehouseSystem;
    }

    public void SetCustomerManager(CustomerManager customerManager)
    {
        this.customerManager = customerManager;
    }

    public void SetSoundManager(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public void SetCameraSystem(CameraSystem cameraSystem)
    {
        this.cameraSystem = cameraSystem;
    }

    public void SetFurnitureManager(FurnitureManager furnitureManager)
    {
        this.furnitureManager = furnitureManager;
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

    public CustomerManager CustomerManager => customerManager;

    public DataGame DataGame => dataGame;
    public SoundManager SoundManager => soundManager;
    public CameraSystem CameraSystem => cameraSystem;

    public FurnitureManager FurnitureManager => furnitureManager;
}
