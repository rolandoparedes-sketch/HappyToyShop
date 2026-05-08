using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Shadow : MonoBehaviour
{

    [FoldoutGroup("StatsSettings")]
    public Transform target;

    private Vector3 lastPlayerPos;

    [FoldoutGroup("StatsSettings")]
    public float distanceBehind = 5f;
    [FoldoutGroup("StatsSettings")]
    public float rotationSpeed = 5f;

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
                rotationSpeed * Time.deltaTime
            );
        }

        lastPlayerPos = target.position;
        transform.LookAt(target);
    }
    public void ShadowDetected()
    {
        Destroy(gameObject);
    }
}
