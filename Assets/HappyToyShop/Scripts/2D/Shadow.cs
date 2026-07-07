using System.Collections;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 5f;
    public Transform Target;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public IEnumerator Dissapear()
    {
        yield return new WaitForSeconds (0.3f);

        this.gameObject.SetActive (false);
    }
}
