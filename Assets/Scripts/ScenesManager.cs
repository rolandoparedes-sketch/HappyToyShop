using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private Animator Anim;

    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        Anim = GetComponentInChildren<Animator>();
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
    }
    [Button]
    public void ChangeMode2D()
    {
        StartCoroutine(SceneLoad("2D"));
    }
    public IEnumerator SceneLoad(string scene)
    {
        Anim.SetTrigger("Fade");
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(scene);
    }


}