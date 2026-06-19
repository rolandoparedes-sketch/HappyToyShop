using UnityEngine;

public class PlayerAnimations2D : MonoBehaviour
{
    public Animator animator;


    public bool isFacingUp;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        PlayerController2D.instance.playerMovement.OnMove += setMoveAnimation;
    }

    void Update()
    {

    }

    public void setMoveAnimation(Vector2 vector)
    {
        animator.SetFloat("Speed", vector.magnitude);

        if (vector.y != 0)
        {
            animator.SetFloat("Vertical", vector.y);
        }



    }
}
