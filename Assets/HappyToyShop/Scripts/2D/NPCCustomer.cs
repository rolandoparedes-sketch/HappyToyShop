using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class NPCCustomer : MonoBehaviour
{


    [FoldoutGroup("References")]
    [SerializeField] private NpcData data;
    [FoldoutGroup("References")]
    [SerializeField] private NpcDataBase dataBase;
    [FoldoutGroup("References")]
    [SerializeField] private TypeDialogue typeDialogue;
    //[SerializeField] private Sprite spriteNpc;
    [FoldoutGroup("References")]
    [SerializeField] private GameObject NpcModel; 
    [FoldoutGroup("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    //[SerializeField] private Sprite spriteToy;
    [FoldoutGroup("References")]
    [SerializeField] private GameObject globoPedido;
    [FoldoutGroup("References")]
    [SerializeField] private GameObject pedido;

    [FoldoutGroup("References")]
    [SerializeField] private Animator animator;
    [FoldoutGroup("References")]
    [SerializeField] private Rigidbody2D rb;


    [FoldoutGroup("References")]
    [SerializeField] private Transform target;



    [FoldoutGroup("NpcSettings")]
    [SerializeField] private string entityName;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private string dialogue;
    [FoldoutGroup("NpcSettings")]
    [SerializeField] private int idPedido;
    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float moveSpeed;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float baseMoveSpeed;


    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float AngryMoveSpeed;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float maxPatience = 30;
    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float patience;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float stopDistance = 0.05f;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool isWaiting;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool attended;


    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool isAngry;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool isDesperate;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool Received;


    public static event Action OnReceived;
    /* public NPC(NpcDataDialogues dialogues, TypeDialogue typeDialogue, Sprite sprite, Animator animator, float patience)
     {
         Dialogues = dialogues;
         this.typeDialogue = typeDialogue;
         this.sprite = sprite;
         this.animator = animator;
         Patience = patience;

     }*/
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        spriteRenderer = NpcModel.GetComponent<SpriteRenderer>();

    }
    private void OnEnable()
    {
        ResetState();
    }

    private void OnDisable()
    {
        GameManager2D.instance.CustomerManager.CustomerQueue.RemoveWaitingCustomer(this);


    }
    void Start()
    {

    }

    
    void Update()
    {
        TimeToWait();


        NpcMovement();
    }
    public void ResetState()
    {
        Received = false;
        isAngry = false;
        patience = maxPatience;

        isWaiting = false;
        target = null;

        animator.speed = 1f;

        isDesperate = false;
        spriteRenderer.color = Color.white;


        globoPedido.SetActive(false);

        GetComponent<Collider2D>().isTrigger = false;
        SetNormalState();
    }

    private void NpcMovement()
    {
        if (target == null || isWaiting)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= stopDistance)
        {
            isWaiting = true;

        }

        animator.SetBool("IsWaiting", isWaiting);

    }
    public void GetAngry()
    {
        if (isAngry) return;

        isAngry = true;
        animator.SetBool("Angry", true);
        moveSpeed = AngryMoveSpeed;
        animator.speed = 1.5f;
        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Voice, 3);
    }
    public void SetNormalState()
    {
        isAngry = false;
        isDesperate = false;
        spriteRenderer.color = Color.white;
        animator.SetBool("Angry", isAngry);
        moveSpeed = baseMoveSpeed;
    }
    public void CustomerReceived()
    {
        globoPedido.gameObject.SetActive(true);

    }
    public void CustomerAttended(CustomerExitReason reason)
    {
        if (reason == CustomerExitReason.Served)
        {
            SetNormalState();
        }
        else if (reason == CustomerExitReason.Timeout)
        {
            GetAngry();
        }
        isWaiting = false;
        attended = true;

        globoPedido.SetActive(false);

        SetTarget(GameManager2D.instance.CustomerManager.CustomerQueue.ExitTarget);
        GetComponent<Collider2D>().isTrigger = true;

        GameManager2D.instance.CustomerManager.CustomerQueue.OnCustomerReceived?.Invoke(this);

    }
    public void ExpandPatience(float amount)
    {
        patience = Mathf.Min(patience + amount, maxPatience);
        Debug.Log("Paciencia aumentada");
    }
    public void TimeToWait()
    {
        if (ParanormalSuccess2D.paranormalSuccessActive)
            return;

        if(!isWaiting || target == null)
            return;
        if(Received)
        {

            patience -= Time.deltaTime;
        }
        else
        {

            patience -= 0.4f*Time.deltaTime;
        }
            

        if (patience <= 0)
        {
            isWaiting = false;

            animator.speed = 1f;
            GameManager2D.instance.CustomerManager.CustomerQueue.RemoveWaitingCustomer(this);

            SetTarget(GameManager2D.instance.CustomerManager.CustomerQueue.ExitTarget);
            CustomerAttended(CustomerExitReason.Timeout);
        }

        if(patience <= maxPatience * 0.5 && !attended && !isAngry)
        {
            GetAngry();
        }

        if (patience <= maxPatience * 0.3f && !attended && !isDesperate)
        {
            isDesperate = true;

            spriteRenderer.color = new Color(1f, 0.6f, 0.6f);

            animator.speed = 2f;
            GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Voice, 2);
        }

    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isWaiting = false;
    }
    [Button]
    public void Initializer()
    {
        data = dataBase.GetDataNpc();
        globoPedido.SetActive(false);

        int n = UnityEngine.Random.Range(0, 3);

        var factory = GameManager2D.instance.FactorySystem.toyDataBase;

        NpcModel.GetComponent<SpriteRenderer>().sprite = data.Icon;

        animator.runtimeAnimatorController = data.Anim;

        int n2 = UnityEngine.Random.Range(0, factory.toyDataBase.Count);

        ToyData toydata = factory.GetToy(n2);

        pedido.GetComponent<SpriteRenderer>().sprite = toydata.Icon;
        idPedido = toydata.ID;

        var DayManager = GameManager2D.instance.DayManager;

        int value = UnityEngine.Random.Range(0, 10);


        if (DayManager.LookSpecialDay() > 5 && value > 9)
        {

            typeDialogue = TypeDialogue.Disturbing;


            dialogue = dataBase.GetDialogue(typeDialogue);

            Debug.Log(dialogue);

            return;
        }
        if (DayManager.LookSpecialDay() > 3 && value > 5)
        {

            typeDialogue = TypeDialogue.Strange;




            dialogue = dataBase.GetDialogue(typeDialogue);

            Debug.Log(dialogue);

            return;
        }



        typeDialogue = TypeDialogue.Normal;


        dialogue = dataBase.GetDialogue(typeDialogue);

        Debug.Log(dialogue);

    }
    [Button]
    public void LeaveStore()
    {

        CustomerSpawner.OnCustomerLeft?.Invoke(this);
    }

    public NpcData DataNpc => data;
    public int IdPedido => idPedido;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attention"))
        {
            Received =true;
            GameManager2D.instance.CustomerManager.CustomerQueue.AddWaitingCustomer(this);

            GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Voice, 4);
            OnReceived?.Invoke();
        }

        if (other.gameObject.CompareTag("Exit") && !isWaiting)
        {
            LeaveStore();
        }

       
    }

    public string Dialogue => dialogue;
}
