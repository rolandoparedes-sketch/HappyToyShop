using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.AI;

public class ShadowFollower : MonoBehaviour
{

    [FoldoutGroup("References")]
    public Transform target;

    private Vector3 lastPlayerPos;

    [FoldoutGroup("Effects")]
    public float fearIncrease = 10f;
    private void Awake()
    {

    }
    void Start()
    {
        lastPlayerPos = target.position;
    }

    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {


        Vector3 newPos = target.position - lastPlayerPos;

        transform.position += newPos;


        transform.LookAt(target);
        lastPlayerPos = target.position;
    }
    public void ShadowDetected()
    {
        Debug.Log(GameManager.instance.paranormalSuccess.Player);
        FirstPersonController player = GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>();

        player.currentCordure = Mathf.Max(player.currentCordure - fearIncrease, 0);

        player.UpdateFearState();

        Destroy(gameObject);

        GameManager.instance.paranormalSuccess.CanSpawnFollower = true;
    }
}
