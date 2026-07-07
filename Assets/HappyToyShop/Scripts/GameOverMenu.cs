using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
