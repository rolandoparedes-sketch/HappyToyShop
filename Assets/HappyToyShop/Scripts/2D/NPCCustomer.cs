using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UI;

public class NPCCustomer : MonoBehaviour
{
    [SerializeField] private NpcData data;
    [SerializeField] private TypeDialogue typeDialogue;
    //[SerializeField] private Sprite spriteNpc;

    [SerializeField] private GameObject NpcModel;

    //[SerializeField] private Sprite spriteToy;
    [SerializeField] private GameObject globoPedido;
    [SerializeField] private GameObject pedido;
    [SerializeField] private int IDPedido;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Animator animator;
    [SerializeField] private float patience = 30;



    [SerializeField] private Transform exitPoint;


    public Rigidbody2D Rb;

    [SerializeField] private float stopDistance = 0.05f;

    private Transform target;
    private bool isMoving;
    private bool annoying;

    private bool enter;
    private bool exit;


    private Coroutine currentPatienceCoroutine;
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
        Rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //Initializer();

    }
    void Update()
    {
        if (!isMoving || target == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;

        Rb.linearVelocity = direction * moveSpeed;

        if (Vector2.Distance(transform.position, target.position) <= stopDistance)
        {
            transform.position = target.position;

            Rb.linearVelocity = Vector2.zero;

            isMoving = false;

            if (exit)
            {
                LeaveStore();
                return;
            }

            if (target.CompareTag("Atention"))
            {
                globoPedido.SetActive(true);
            }

            currentPatienceCoroutine = StartCoroutine(InitializerTimeToWait());
        }
    }
    public void ExitStore(Transform exitTransform)
    {
        exit = true;
        enter = false;

        target = exitTransform;
        isMoving = true;

        this.gameObject.GetComponent<Collider2D>().enabled = false;
        globoPedido.SetActive(false);

        if (currentPatienceCoroutine != null)
        {
            StopCoroutine(currentPatienceCoroutine);
            currentPatienceCoroutine = null;
        }
    }
    public void SetTarget(Transform targetPoint)
    {
        if (target == targetPoint)
            return;

        target = targetPoint;
        isMoving = true;

        globoPedido.SetActive(false);

        if (currentPatienceCoroutine != null)
        {
            StopCoroutine(currentPatienceCoroutine);
            currentPatienceCoroutine = null;
        }
    }
    public void SetExitPoint(Transform point)
    {
        exitPoint = point;
    }
    [Button]
    public void Initializer()
    {
        globoPedido.SetActive(false);

        int n = UnityEngine.Random.Range(0, 3);

        var factory = GameManager2D.instance.FactorySystem.toyDataBase;

        NpcModel.GetComponent<SpriteRenderer>().sprite = data.GetSpriteNpc(n);


        int n2 = UnityEngine.Random.Range(0, factory.toyDataBase.Count);

        ToyData toydata = factory.GetToy(n2);

        pedido.GetComponent<SpriteRenderer>().sprite = toydata.Icon;
        IDPedido = toydata.ID;

        var DayManager = GameManager2D.instance.DayManager;

        int value = UnityEngine.Random.Range(0, 10);


        if (DayManager.LookDay() > 5 && value > 9)
        {

            typeDialogue = TypeDialogue.Disturbing;




            Debug.Log(data.GetDialogue(typeDialogue));

            return;
        }
        if (DayManager.LookDay() > 3 && value > 5)
        {

            typeDialogue = TypeDialogue.Strange;




            Debug.Log(data.GetDialogue(typeDialogue));

            return;
        }



        typeDialogue = TypeDialogue.Normal;



        Debug.Log(data.GetDialogue(typeDialogue));

    }
    [Button]
    public void LeaveStore()
    {

        CustomerManager.OnCustomerLeft?.Invoke(this);
    }

    public IEnumerator InitializerTimeToWait()
    {
        Debug.Log($"{name}: paciencia iniciada");

        yield return new WaitForSeconds(patience);

        Debug.Log($"{name}: paciencia terminada");

        ExitStore(exitPoint);
    }
    public void ResetCustomer()
    {
        if (currentPatienceCoroutine != null)
        {
            StopCoroutine(currentPatienceCoroutine);
            currentPatienceCoroutine = null;
        }

        globoPedido.SetActive(false);

        target = null;

        isMoving = false;
        exit = false;
        annoying = false;

        Rb.linearVelocity = Vector2.zero;
        Rb.angularVelocity = 0f;
    }

}
