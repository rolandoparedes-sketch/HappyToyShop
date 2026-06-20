using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor.Animations;

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
    [SerializeField] private float patience = 30;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private float stopDistance = 0.05f;

    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool isMoving;
    [FoldoutGroup("NpcSettings")]
    [SerializeField] private bool exit;

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
    void Start()
    {
        //Initializer();

        

    }

    private void OnEnable()
    {
        //GameManager2D.instance.CustomerManager.OnChangeQueue += expandPatience;
    }
    void Update()
    {
       // NpcMovement();
        TimeToWait();
    }
    public void expandPatience()
    {
        patience += 5;
        Debug.Log("Paciencia aumentada");
    }
    public void TimeToWait()
    {
        if(isMoving || target == null)
            return;

        patience-= Time.deltaTime;

        if (patience <= 0)
        {
           // GameManager2D.instance.CustomerManager.OnCustomerAttended?.Invoke(this);
            
            //ExitStore(GameManager2D.instance.CustomerManager.ExitPoint);
        }


    }/*
    public void NpcMovement()
    {
        if (!isMoving || target == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        if (Vector2.Distance(transform.position, target.position) <= stopDistance)
        {
            transform.position = target.position;

            rb.linearVelocity = Vector2.zero;

            isMoving = false;

            if (exit)
            {
                LeaveStore();

                Debug.Log(target);
                return;
            }

            if (target.CompareTag("Atention"))
            {
                globoPedido.SetActive(true);
            }

            

        }

    }
    public void ExitStore(Transform exitTransform)
    {
        exit = true;

        target = exitTransform;
        
        isMoving= true;

        this.gameObject.GetComponent<Collider2D>().enabled = false;
        globoPedido.SetActive(false);



    }
    public void SetTarget(Transform targetPoint)
    {
        if (target == targetPoint)
            return;

        target = targetPoint;
        isMoving = true;

       
    }*/
    [Button]
    public void Initializer()
    {
        data = dataBase.GetDataNpc();
        //globoPedido.SetActive(false);

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


        if (DayManager.LookDay() > 5 && value > 9)
        {

            typeDialogue = TypeDialogue.Disturbing;




            Debug.Log(dataBase.GetDialogue(typeDialogue));

            return;
        }
        if (DayManager.LookDay() > 3 && value > 5)
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

        CustomerManager.OnCustomerLeft?.Invoke(this);
    }

}
