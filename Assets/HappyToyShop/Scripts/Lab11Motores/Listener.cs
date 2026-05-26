using System;
using UnityEngine;

public class Listener : MonoBehaviour
{

    void Start()
    {
        ObserverPatternExamples.simpleAction += activarSonido; 
        
    }

    private void activarSonido()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
