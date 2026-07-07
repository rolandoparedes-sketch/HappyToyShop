using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager3D : MonoBehaviour
{
    public static GameManager3D instance;

    [FoldoutGroup("References")]
    [SerializeField]private ShadowSpawner shadowSpawner;


    [FoldoutGroup("References")]
    [SerializeField] private SoundManager soundManager;

    [FoldoutGroup("References")]
    [SerializeField] private ParanormalSuccess3D paranormalSuccess3D;

    [FoldoutGroup("References")]
    [SerializeField] private FactorySystem factorySystem;


    [FoldoutGroup("References")]
    [SerializeField] private DataGame dataGame;

    [FoldoutGroup("References")]
    [SerializeField] private RestockManager restockManager;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ShadowSpawner ShadowSpawner => shadowSpawner;
    public ParanormalSuccess3D ParanormalSuccess3D => paranormalSuccess3D;

    public SoundManager SoundManager => soundManager;
    public FactorySystem FactorySystem => factorySystem;
    public DataGame DataGame => dataGame;
    public RestockManager RestockManager => restockManager;
}
