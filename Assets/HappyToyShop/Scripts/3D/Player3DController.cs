using Sirenix.OdinInspector;
using UnityEngine;
[RequireComponent(typeof(Player3DMovement))]
[RequireComponent(typeof(Player3DInventory))]
[RequireComponent(typeof(Player3DState))]
public class Player3DController : MonoBehaviour
{
    public static Player3DController instance;
    [FoldoutGroup("References")]
    [SerializeField] private Player3DMovement movement3D;
    [FoldoutGroup("References")]
    [SerializeField] private Player3DInventory inventory3D;
    [FoldoutGroup("References")]
    [SerializeField] private Player3DState state3D;
    


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        movement3D = GetComponent<Player3DMovement>();
        inventory3D = GetComponent<Player3DInventory>();
        state3D = GetComponent<Player3DState>();

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
