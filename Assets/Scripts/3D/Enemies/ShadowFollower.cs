using MoreMountains.Tools;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.AI;

public class ShadowFollower : MonoBehaviour
{

    [FoldoutGroup("References")]
    public Transform target;

    private Vector3 lastPlayerPos;
    public Vector3 Offset;
    [FoldoutGroup("Effects")]
    public float fearIncrease = 10f;
    public MMFollowTarget FollowTarget;
    private void Awake()
    {

    }
    void Start()
    {
        //lastPlayerPos = target.position;

        FollowTarget = GetComponent<MMFollowTarget>();

        FollowTarget.Target = target;

        Offset = (target.forward * - 5);
        FollowTarget.Offset = Offset;
    }

    void Update()
    {
        transform.LookAt(target);
        // FollowPlayer();
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
        FirstPersonController player = GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>();

        player.currentCordure = Mathf.Max(player.currentCordure - fearIncrease, 0);

        player.UpdateFearState();

        Destroy(gameObject);

        GameManager.instance.paranormalSuccess.CanSpawnFollower = true;
    }
}
