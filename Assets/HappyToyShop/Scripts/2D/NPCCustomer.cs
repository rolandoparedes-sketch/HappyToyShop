using Sirenix.OdinInspector;
using UnityEngine;
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
    [SerializeField] private bool Received;


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
        isAngry= true;
        animator.SetBool("Angry", isAngry);
        moveSpeed = AngryMoveSpeed;
    }
    public void SetNormalState()
    {
        isAngry = false;
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

            GameManager2D.instance.CustomerManager.CustomerQueue.RemoveWaitingCustomer(this);

            SetTarget(GameManager2D.instance.CustomerManager.CustomerQueue.ExitTarget);
            CustomerAttended(CustomerExitReason.Timeout);
        }

        if(patience <= maxPatience * 0.3 && !attended)
        {
            GetAngry();
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


            Debug.Log(dataBase.GetDialogue(typeDialogue));

            return;
        }
        if (DayManager.LookSpecialDay() > 3 && value > 5)
        {

            typeDialogue = TypeDialogue.Strange;




            Debug.Log(dataBase.GetDialogue(typeDialogue));

            return;
        }



        typeDialogue = TypeDialogue.Normal;



        Debug.Log(dataBase.GetDialogue(typeDialogue));

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
        }

        if (other.gameObject.CompareTag("Exit") && !isWaiting)
        {
            LeaveStore();
        }

       
    }


}
