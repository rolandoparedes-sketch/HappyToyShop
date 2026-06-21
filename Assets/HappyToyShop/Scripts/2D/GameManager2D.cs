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
    [FoldoutGroup("References")]
    [SerializeField] private CustomerManager customerManager;
    [FoldoutGroup("References")]
    [SerializeField] private DataGame dataGame;
    [FoldoutGroup("References")]
    [SerializeField] private SoundManager soundManager;
    [FoldoutGroup("References")]
    [SerializeField] private CameraSystem cameraSystem;
    [FoldoutGroup("References")]
    [SerializeField] private CustomerQueue customerQueue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    public CustomerManager CustomerManager => customerManager;

    public DataGame DataGame => dataGame;
    public SoundManager SoundManager => soundManager;
    public CameraSystem CameraSystem => cameraSystem;

    public CustomerQueue CustomerQueue => customerQueue;
}
