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
}
