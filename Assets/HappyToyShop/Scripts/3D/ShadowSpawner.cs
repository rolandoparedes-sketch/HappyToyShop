using HappyToyShop.Collections;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{


    public Transform Target;




    [SerializeField] private ShadowFollower followerPrefab;
    [SerializeField] private MyQueue<ShadowFollower> ShadowFollower = new();

    [SerializeField] private ShadowScream screamPrefab;
    [SerializeField] private MyQueue<ShadowScream> ShadowScream = new();



    [SerializeField] private int size = 5;
    public Action<ShadowFollower> OnShadowFollowerDisappear;


    public Action<ShadowScream> OnShadowScreamDisappear;

    void Start()
    {
        CreatPoolFollower(size);

        CreatPoolScream(size);
    }

    void Update()
    {
        
    }


    public Transform GetTarget()
    {
        return Target;
    }

    [Button]
    public void CreatPoolFollower(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            ShadowFollower shadowFollower = Instantiate(followerPrefab, transform);

            shadowFollower.gameObject.SetActive(false);
            ShadowFollower.Enqueue(shadowFollower);

        }
    }
    [Button]
    public void CreatPoolScream(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            ShadowScream shadowScream = Instantiate(screamPrefab, transform);

            shadowScream.gameObject.SetActive(false);
            ShadowScream.Enqueue(shadowScream);

        }
    }


    [Button]
    private void ReturnFollowerToPool(ShadowFollower customer)
    {


        ShadowFollower.Enqueue(customer);
        customer.gameObject.SetActive(false);



    }
    [Button]
    private void ReturnScreamToPool(ShadowScream customer)
    {


        ShadowScream.Enqueue(customer);
        customer.gameObject.SetActive(false);



    }
    private void OnEnable()
    {
        OnShadowFollowerDisappear += ReturnFollowerToPool;

        OnShadowScreamDisappear += ReturnScreamToPool;

    }

    public ShadowFollower NextFollower()
    {
        if (ShadowFollower.Count == 0)
            return null;

        return ShadowFollower.Dequeue();
    }

    public ShadowScream NextScream()
    {
        if (ShadowScream.Count == 0)
            return null;

        return ShadowScream.Dequeue();
    }
    private void OnDisable()
    {
        OnShadowFollowerDisappear -= ReturnFollowerToPool;

        OnShadowScreamDisappear -= ReturnScreamToPool;
    }


    public void SpawnFollower()
    {
        ShadowFollower shadowFollower = NextFollower();

        if (shadowFollower == null)
        {
            ExpandFollowerPool(size);
            Debug.Log("No hay ShadowFollowers disponibles en el pool.");
            return;
        }

        shadowFollower.gameObject.SetActive(true);

        Vector3 SpawnShadowFollower = GameManager3D.instance.ShadowSpawner.Target.position - GameManager3D.instance.ShadowSpawner.Target.forward * 5f; 

        shadowFollower.transform.position = SpawnShadowFollower;
           
        shadowFollower.target = Target;
        
    }

    public void SpawnScream()
    {
        ShadowScream shadowScream = NextScream();
        if (shadowScream == null)
        {
            ExpandScreamPool(size);
            Debug.Log("No hay ShadowScreams disponibles en el pool.");
            return;
        }
        shadowScream.gameObject.SetActive(true);
        Vector3 SpawnShadowScream = GameManager3D.instance.ShadowSpawner.Target.position - GameManager3D.instance.ShadowSpawner.Target.forward * 5f; 
        shadowScream.transform.position = SpawnShadowScream;
           
        shadowScream.target = Target;

    }

    private void ExpandFollowerPool(int quantity)
    {
        
        for (int i = 0; i < quantity; i++)
        {
            ShadowFollower shadowFollower = Instantiate(followerPrefab, transform);

            shadowFollower.gameObject.SetActive(false);
            ShadowFollower.Enqueue(shadowFollower);

        }
    }
    private void ExpandScreamPool(int quantity)
    {

        for (int i = 0; i < quantity; i++)
        {
            ShadowScream shadowScream = Instantiate(screamPrefab, transform);

            shadowScream.gameObject.SetActive(false);
            ShadowScream.Enqueue(shadowScream);

        }
    }

}
