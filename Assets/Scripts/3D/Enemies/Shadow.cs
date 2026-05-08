using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Shadow : MonoBehaviour
{

    [FoldoutGroup("StatsSettings")]
    public Transform target;

    public Vector3 lastPlayerPos;
    public float distanceBehind = 5f;
    public float followSpeed = 5f;

    private void Awake()
    {
    }
    void Start()
    {
    }

    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 moveDir = target.position - lastPlayerPos;

        if (moveDir.magnitude > 0.01f)
        {
            Vector3 behindPos = target.position - target.forward * distanceBehind;

            transform.position = Vector3.Lerp(
                transform.position,
                behindPos,
                followSpeed * Time.deltaTime
            );
        }

        lastPlayerPos = target.position;
        //transform.LookAt(target);
    }
}
