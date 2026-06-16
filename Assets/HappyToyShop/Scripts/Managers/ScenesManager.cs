using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    public Animator FadeAnim;

    [SerializeField] private float fadeDuration = 1f;

    public Action OnChangeScene;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        FadeAnim = GetComponentInChildren<Animator>();



    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (GameManager2D.instance != null)
            GameManager2D.instance.DayManager.OnWeekComplete += YouWon;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (GameManager2D.instance != null)
            GameManager2D.instance.DayManager.OnWeekComplete -= YouWon;
    }
    public void Play()
    {
        Debug.Log("Play");
        StartCoroutine(SceneLoad("2D"));
    }
    public void YouWon()
    {
        Debug.Log("Ganaste");
        StartCoroutine(SceneLoad("Win"));
    }
    public void Quit()
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
        GameManager2D.instance.DayManager.NextDay();

    }
    public IEnumerator SceneLoad(string scene)
    {
        FadeAnim.SetTrigger("Fade");

        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(scene);



    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnChangeScene?.Invoke();
    }
    
}