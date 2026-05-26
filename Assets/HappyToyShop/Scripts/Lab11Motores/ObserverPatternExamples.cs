using System;
using UnityEngine;

public class ObserverPatternExamples : MonoBehaviour
{
    public static event Action simpleAction;
    void Start()
    {
        

        simpleAction = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
