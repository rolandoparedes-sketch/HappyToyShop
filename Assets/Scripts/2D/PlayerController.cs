using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;

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

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
