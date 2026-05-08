using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [FoldoutGroup("References")]
    public InputSystem_Actions inputs;
    [FoldoutGroup("References")]
    [SerializeField] private Rigidbody2D rb;

    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private Vector2 moveInput;
    [FoldoutGroup("ControllerSettings")]
    [SerializeField] private float moveSpeed = 5f;


    private void Awake()
    {
        inputs = new();
        rb = GetComponent<Rigidbody2D>();
    }
        void Start()
    {
        
    }
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;







    }
    private void OnDisable()
    {
        inputs.Disable();

        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;







    }
    void Update()
    {

    }
    private void FixedUpdate()
    {


        rb.linearVelocity = moveInput * moveSpeed;

    }
}
