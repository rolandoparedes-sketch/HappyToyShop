using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent (typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("References")]
    public static PlayerController instance;
    [FoldoutGroup("References")]
    public PlayerMovement playerMovement;
    [FoldoutGroup("References")]
    public PlayerAnimations playerAnimations;
    [FoldoutGroup("References")]
    public PlayerStats playerStats;

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
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerStats = GetComponent<PlayerStats>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
