using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement2D))]

[RequireComponent(typeof(PlayerAnimations2D))]
[RequireComponent (typeof(PlayerStats2D))]
[RequireComponent (typeof(PlayerMechanics2D))]
public class PlayerController2D : MonoBehaviour
{
    [FoldoutGroup("References")]
    public static PlayerController2D instance;
    [FoldoutGroup("References")]
    public PlayerMovement2D playerMovement;
    [FoldoutGroup("References")]
    public PlayerAnimations2D playerAnimations;
    [FoldoutGroup("References")]
    public PlayerStats2D playerStats; 
    [FoldoutGroup("References")]
    public PlayerMechanics2D playerMechanics;

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
        playerMovement = GetComponent<PlayerMovement2D>();
        playerAnimations = GetComponent<PlayerAnimations2D>();
        playerStats = GetComponent<PlayerStats2D>();
        playerMechanics = GetComponent<PlayerMechanics2D>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
