using UnityEngine;

public class BuffPickUp : MonoBehaviour
{
    public BuffType buffType;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Collision");
        if (other.TryGetComponent(out BuffManager buffManager))
        {
            Buff buff = BuffFactory.CreateBuff(buffType);

            buffManager.AddBuff(buff);

            Debug.Log("Collision2");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject,1);

        }
    }

}
