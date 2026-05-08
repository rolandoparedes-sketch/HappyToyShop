using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private Animator FadeAnim;

    [SerializeField] private float fadeDuration = 2f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        FadeAnim = GetComponentInChildren<Animator>();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Salir()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }


    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    [Button]
    public void ChangeMode3D()
    {
        StartCoroutine(SceneLoad("3D"));

        GameManager.instance.TurnDay = false;
    }
    [Button]
    public void ChangeMode2D()
    {
        StartCoroutine(SceneLoad("2D"));
        GameManager.instance.NextDay();

        GameManager.instance.TurnDay = true;
    }
    public IEnumerator SceneLoad(string scene)
    {
        FadeAnim.SetTrigger("Fade");
        
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(scene);
    }


}