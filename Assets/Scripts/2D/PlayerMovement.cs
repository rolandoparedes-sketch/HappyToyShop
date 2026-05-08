using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions inputs;

    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
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
