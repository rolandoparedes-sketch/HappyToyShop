using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Day;
    void Start()
    {
        Day.text = "Day: " + GameManager.instance.Day.Peek();
        
    }

    void Update()
    {
        
    }
}
