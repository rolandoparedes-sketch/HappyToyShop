using Sirenix.OdinInspector;
using System;
using System.Collections;
using Unity.Cinemachine;
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
    [SerializeField] private float timeDontMove = 2.5f;



    private bool CanMove = false;

    public event Action OnEnterDoor;

    private void Awake()
    {
        inputs = new();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(WaitForPlay(timeDontMove));
    }
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputs.Player.Jump.performed += ctx => ScenesManager.instance.ChangeMode3D();





    }
    private void OnDisable()
    {
        inputs.Disable();

        inputs.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        inputs.Player.Jump.performed -= ctx => ScenesManager.instance.ChangeMode3D();






    }
    void Update()
    {

    }
    private void FixedUpdate()
    {

        MovementMechanics();
        

    }
    public void MovementMechanics()
    {
        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = moveInput * PlayerController2D.instance.playerStats.moveSpeed;
    }
    
    public void MethodWaitForPlay(float time)
    {
        StartCoroutine(WaitForPlay(time));
    }
    public IEnumerator WaitForPlay(float time)
    {
        CanMove = false;
        yield return new WaitForSeconds(time);
        CanMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Puerta"))
        {
            OnEnterDoor?.Invoke();
            Debug.Log("Entro");
        }
    }


}
